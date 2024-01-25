using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.GameInfoSerialization.Interfaces;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GraphicsAdapters;
using TicTacToe3D.GraphicsAdapters.AdvancedGraphics;
using TicTacToe3D.GraphicsAdapters.SimpleGraphics;
using TicTacToe3D.HumanParticipants.Interfaces;
using TicTacToe3D.Properties;
using TicTacToe3D.GameInfoSerialization;

namespace TicTacToe3D.HumanParticipants
{
    public partial class HumanPlayer : UserControl, IHumanPlayer
    {
        #region Constants

        private const string _unfinishedGameFilesFilter = "Unfinished game files";
        private const string _allFilesFilter = "All files";
        private const string _delimiter = "|";

        private const string _saveSuccessfullText = "Save successfull";
        private const string _endOfGameText = "Game over!";
        private const string _saveDialogText = "Do you want to save the game?";
        private const string _saveDialogCaption = "Saving game...";

        #endregion

        #region Fields

        private IGraphicsAdapter _graphicsAdapter;

        private PlayerInformation _currentPlayerInformation;

        private IGameField _gameField;
        private GameInformation _gameInfo;

        private readonly Angle _cameraRotationAnglePerTimerInterval;
        private readonly Vector3D _cameraDisplacementVectorPerTimerInterval;

        private Angle _currentRotationAngle;
        private Axis _currentRotationAxis;
        private Vector3D _currentDisplacementVector;

        private readonly Settings _settings;

        /// <summary>
        /// The index of the last appeared figure
        /// </summary>
        private Cell _lastShownCell = new Cell(0, 0, 0);

        #endregion


        #region Constructors

        public HumanPlayer()
        {
            InitializeComponent();

            AvailablePlayerInfos = new List<PlayerInformation>();

            KeyDown += HandleControlKeyDown;

            _settings = Settings.Default;

            timer.Interval = _settings.CameraTransformationIntervalForTimer;

            _cameraRotationAnglePerTimerInterval = new Angle(_settings.CameraRotationAnglePerTimerInterval);

            _cameraDisplacementVectorPerTimerInterval = new Vector3D();
            _cameraDisplacementVectorPerTimerInterval[0] = _settings.CameraDisplacementAlongXPerTimerInterval;
            _cameraDisplacementVectorPerTimerInterval[1] = _settings.CameraDisplacementAlongYPerTimerInterval;
            _cameraDisplacementVectorPerTimerInterval[2] = _settings.CameraDisplacementAlongZPerTimerInterval;
        }

        #endregion

        #region Properties

        public IList<PlayerInformation> AvailablePlayerInfos { get; set; }

        public PlayerInformation CurrentPlayerInfo
        {
            get
            {
                return _currentPlayerInformation;
            }
            set
            {
                _currentPlayerInformation = value;
            }
        }

        #endregion

        #region Events

        public event EventHandler<TurnMadeEventArgs> TurnMade;

        public event EventHandler AvailablePlayerInfosChanged;

        public event EventHandler PreparedForGame;

        public event EventHandler GameTerminationHandled;

        public event EventHandler GameTerminating;

        public event EventHandler LeavingGame;

        #endregion

        #region Public Methods

        public void PrepareForGame(GameInformation gameInfo)
        {
            Graphics graphics = Graphics.FromHwnd(displayPanel.Handle);

            _gameInfo = gameInfo;
            _gameField = _gameInfo.GameField;

            _graphicsAdapter = new SimpleGraphicsAdapter(graphics, gameInfo.GameField);
            //_graphicsAdapter = new AdvancedGraphicsAdapter(graphics, gameInfo.GameField);

            PrepareView();

            FillGameField();

            SetupCoordinateTrackBars();

            DisableAllButtons();

            PreparedForGame(this, new EventArgs());
        }

        public void UpdateGameInformation(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public void StartGame()
        {

        }

        public void ModifyCell(Cell cell, Side side)
        {
            _gameField.SetCellState(cell, (CellState)side);

            try
            {
                _graphicsAdapter.AddGameObject(side, cell);
            }
            //This exception may be thrown when game master calls this method for the object which has just made the turn,
            //And game oject has already been added during the decision of the human player
            catch (InvalidOperationException)
            {

            }

            _graphicsAdapter.Draw();
        }

        public void HandleGameTermination(GameInformation gameInformation)
        {
            MessageBox.Show(_endOfGameText);

            DisableAllButtons();

            GameTerminationHandled(this, new EventArgs());
        }

        public void MakeTurn()
        {
            beginThinkingButton.Enabled = true;
            beginThinkingButton.Focus();
        }

        public void RollbackLastTurn(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

        #region Private Methods

        private void HandleBeginThinkingButtonClick(object sender, EventArgs e)
        {
            turnGroupBox.Enabled = true;
            confirmButton.Enabled = true;
            beginThinkingButton.Enabled = false;
            confirmButton.Focus();

            foreach (Cell cell in _gameField)
            {
                if (_gameField.GetCellState(cell) == CellState.Empty)
                {
                    xTrackBar.Value = cell.I;
                    yTrackBar.Value = cell.J;
                    zTrackBar.Value = cell.K;

                    _graphicsAdapter.AddGameObject(_currentPlayerInformation.Side, cell);

                    _graphicsAdapter.Draw();

                    _lastShownCell = cell;

                    return;
                }
            }
        }

        private void HandleConfirmButtonClick(object sender, EventArgs e)
        {
            //while other players are thinking, 
            //this player should have no opportunity to manipulate
            DisableAllButtons();

            TurnMadeEventArgs eventArgs = new TurnMadeEventArgs(_lastShownCell);
            TurnMade(this, eventArgs);
        }

        private void HandleStopButtonClick(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(_saveDialogText,
                                                        _saveDialogCaption,
                                                        MessageBoxButtons.YesNoCancel,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
            if (dialogResult == DialogResult.Yes)
            {
                SaveUnfinishedGame();
            }

            //as some time may pass before game master will call HandleGameTermination method
            DisableAllButtons();

            GameTerminating(this, new EventArgs());
        }

        private void HandleSaveButtonClick(object sender, EventArgs e)
        {
            SaveUnfinishedGame();
        }

        private void HandleCoordinateTrackBarScroll(object sender, EventArgs e)
        {
            Cell destinationCell = new Cell(xTrackBar.Value, yTrackBar.Value, zTrackBar.Value);

            //make the last shown cell empty if it should not contain any game object,
            //but there is a probe game object (while the human player is choosing a cell for the turn)
            if (_gameField.GetCellState(_lastShownCell) == CellState.Empty)
            {
                _graphicsAdapter.RemoveGameObject(_lastShownCell);
            }

            _lastShownCell = destinationCell;

            //if the destination cell is empty
            if (_gameField.GetCellState(destinationCell) == CellState.Empty)
            {
                //add game object to this cell
                _graphicsAdapter.AddGameObject(_currentPlayerInformation.Side, destinationCell);

                //enable confirm button, as this turn is possible
                //Confirm button may be disabled if during the last scroll destinationCell wasn't empty
                //(see the next line)
                confirmButton.Enabled = true;
            }
            else
            {
                //disable confirm button to avoid inappropriate user actions
                confirmButton.Enabled = false;
            }

            _graphicsAdapter.Draw();
        }

        private void HandleDisplayPanelPaint(object sender, PaintEventArgs e)
        {
            if (_graphicsAdapter != null)
            {
                _graphicsAdapter.Draw();
            }
        }

        private void HandleControlKeyDown(object sender, KeyEventArgs e)
        {
            Angle rotationAngle;
            Axis rotationAxis;

            if (e.KeyCode == Keys.W)
            {
                rotationAngle = -_cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.X;
            }
            else if (e.KeyCode == Keys.S)
            {
                rotationAngle = _cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.X;
            }
            else if (e.KeyCode == Keys.A)
            {
                rotationAngle = -_cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.Z;
            }
            else if (e.KeyCode == Keys.D)
            {
                rotationAngle = _cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.Z;
            }
            else if (e.KeyCode == Keys.Z)
            {
                rotationAngle = -_cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.Y;
            }
            else if (e.KeyCode == Keys.V)
            {
                rotationAngle = _cameraRotationAnglePerTimerInterval;
                rotationAxis = Axis.Y;
            }
            else
            {
                return;
            }

            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(rotationAngle, rotationAxis);
            _graphicsAdapter.Draw();
        }

        /// <summary>
        /// Prepares the default view for the game field
        /// </summary>
        private void PrepareView()
        {
            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongX), Axis.X);

            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongY), Axis.Y);

            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongZ), Axis.Z);

            _graphicsAdapter.Draw();
        }

        private void HandleRotateButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (sender == rotateRightRoundYAxisButton)
            {
                _currentRotationAngle = -_cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.Y;
            }
            else if (sender == rotateLeftRoundYAxisButton)
            {
                _currentRotationAngle = _cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.Y;
            }


            else if (sender == rotateLeftRoundXAxisButton)
            {
                _currentRotationAngle = -_cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.X;
            }
            else if (sender == rotateRightRoundXAxisButton)
            {
                _currentRotationAngle = _cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.X;
            }


            else if (sender == rotateLeftRoundZAxisButton)
            {
                _currentRotationAngle = -_cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.Z;
            }
            else if (sender == rotateRightRoundZAxisButton)
            {
                _currentRotationAngle = _cameraRotationAnglePerTimerInterval;
                _currentRotationAxis = Axis.Z;
            }

            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(_currentRotationAngle, _currentRotationAxis);
            _graphicsAdapter.Draw();

            timer.Tick += HandleTimerTickForCameraRotation;
            timer.Start();
        }

        private void HandleRotateButtonMouseUp(object sender, MouseEventArgs e)
        {
            timer.Stop();
            timer.Tick -= HandleTimerTickForCameraRotation;
        }

        private void HandleZoomButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (sender == zoomInButton)
            {
                _currentDisplacementVector = -_cameraDisplacementVectorPerTimerInterval;
            }
            else if (sender == zoomOutButton)
            {
                _currentDisplacementVector = _cameraDisplacementVectorPerTimerInterval;
            }

            _graphicsAdapter.MoveCamera(_currentDisplacementVector);
            _graphicsAdapter.Draw();

            timer.Tick += HandleTimerTickForCameraDisplacement;
            timer.Start();
        }

        private void HandleZoomButtonMouseUp(object sender, MouseEventArgs e)
        {
            timer.Stop();
            timer.Tick -= HandleTimerTickForCameraDisplacement;
        }

        private void HandleTimerTickForCameraRotation(object sender, EventArgs e)
        {
            _graphicsAdapter.RotateCameraRoundCenterOfCoordinatesRoundAxis(_currentRotationAngle, _currentRotationAxis);
            _graphicsAdapter.Draw();
        }

        private void HandleTimerTickForCameraDisplacement(object sender, EventArgs e)
        {
            _graphicsAdapter.MoveCamera(_currentDisplacementVector);
            _graphicsAdapter.Draw();
        }

        private void DisableAllButtons()
        {
            turnGroupBox.Enabled = false;
            confirmButton.Enabled = false;
            beginThinkingButton.Enabled = false;
        }

        //may be i should implement it as a command (see gang of four design patterns)
        private void SaveUnfinishedGame()
        {
            IGameInfoSerializer gameInfoSerializer = GameInfoSerializerFactory.CreateGameInfoSerializer();

            string unfinishedGameExtension = gameInfoSerializer.UnfinishedGameExtension;

            saveUnfinishedGameDialog.DefaultExt = unfinishedGameExtension;
            //"Game replay files|*." + replayExtension + "|All files|*.*";
            saveUnfinishedGameDialog.Filter = _unfinishedGameFilesFilter + _delimiter + "*."
                                              + unfinishedGameExtension + _delimiter + _allFilesFilter + _delimiter + "*.*";

            if (saveUnfinishedGameDialog.ShowDialog() == DialogResult.OK)
            {
                gameInfoSerializer.SaveUnfinishedGame(saveUnfinishedGameDialog.FileName, _gameInfo);

                MessageBox.Show(_saveSuccessfullText);
            }
        }

        private void SetupCoordinateTrackBars()
        {
            xTrackBar.Minimum = 0; xTrackBar.Maximum = 2;
            yTrackBar.Minimum = 0; yTrackBar.Maximum = 2;
            zTrackBar.Minimum = 0; zTrackBar.Maximum = 2;
        }

        private void FillGameField()
        {
            CellState cellState;
            IGameField gameField = _gameInfo.GameField;
            foreach (Cell cell in gameField)
            {
                cellState = gameField.GetCellState(cell);
                if (cellState != CellState.Empty)
                {
                    _graphicsAdapter.AddGameObject((Side)cellState, cell);
                }
            }

            _graphicsAdapter.Draw();
        }

        #endregion
    }
}
