using System;
using System.Drawing;
using System.Windows.Forms;
using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.GameInfoSerialization.Interfaces;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GraphicsAdapters.SimpleGraphics;
using TicTacToe3D.HumanParticipants.Interfaces;
using TicTacToe3D.Properties;
using TicTacToe3D.GameInfoSerialization;

namespace TicTacToe3D.HumanParticipants
{
    public /*abstract*/ partial class HumanParticipant : UserControl, IHumanParticipant
    {
        private SimpleGraphicsAdapter _artist;

        private IGameField _gameField;
        private GameInformation _gameInfo;

        private readonly Angle _cameraRotationAnglePerTimerInterval;
        private readonly Vector3D _cameraDisplacementVectorPerTimerInterval;

        private Angle _currentRotationAngle;
        private Axis _currentRotationAxis;
        private Vector3D _currentDisplacementVector;

        private readonly Settings _settings;

        private const string _unfinishedGameFilesFilter = "game files";
        private const string _allFilesFilter = "All files";
        private const string _delimiter = "|";

        private const string _saveSuccessfullText = "Save successfull";
        private const string _endOfGameText = "End of game";
        private const string _saveDialogText = "Do you want to save the game?";
        private const string _saveDialogCaption = "Saving game";


        public HumanParticipant()
        {
            InitializeComponent();

            KeyDown += HandleControlKeyDown;

            _settings = new Settings();

            timer.Interval = _settings.CameraTransformationIntervalForTimer;

            _cameraRotationAnglePerTimerInterval = new Angle(_settings.CameraRotationAnglePerTimerInterval);

            _cameraDisplacementVectorPerTimerInterval = new Vector3D();
            _cameraDisplacementVectorPerTimerInterval[0] = _settings.CameraDisplacementAlongXPerTimerInterval;
            _cameraDisplacementVectorPerTimerInterval[1] = _settings.CameraDisplacementAlongYPerTimerInterval;
            _cameraDisplacementVectorPerTimerInterval[2] = _settings.CameraDisplacementAlongZPerTimerInterval;
        }

        #region IParticipant Members

        public virtual void PrepareForGame(GameInformation gameInfo)
        {
            Graphics graphics = Graphics.FromHwnd(displayPanel.Handle);

            _gameInfo = gameInfo;
            _gameField = _gameInfo.GameField;

            _artist = new SimpleGraphicsAdapter(graphics, gameInfo.GameField);

            PrepareView();

            FillGameField();

            DisableAllGameFlowControlButtons();

            PreparedForGame(this, new EventArgs());
        }

        public virtual void UpdateGameInformation(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public virtual void StartGame()
        {

        }

        public virtual void ModifyCell(Cell cell, Side side)
        {
            _gameField.SetCellState(cell, (CellState)side);

            try
            {
                _artist.AddGameObject(side, cell);
            }
            //This exception may be thrown when game master calls this method for the object which has just made the turn,
            //And game oject has already been added during the decision of the human player
            catch (InvalidOperationException)
            {

            }

            _artist.Draw();
        }

        public virtual void HandleGameTermination(GameInformation gameInformation)
        {
            MessageBox.Show(_endOfGameText);

            DisableAllGameFlowControlButtons();

            GameTerminationHandled(this, new EventArgs());
        }

        public event EventHandler PreparedForGame;

        public event EventHandler GameTerminationHandled;

        public event EventHandler GameTerminating;

        public event EventHandler LeavingGame;

        #endregion

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
            DisableAllGameFlowControlButtons();

            //GameTerminating(this, new EventArgs());
        }

        private void HandleSaveButtonClick(object sender, EventArgs e)
        {
            SaveUnfinishedGame();
        }

        private void HandleDisplayPanelPaint(object sender, PaintEventArgs e)
        {
            if (_artist != null)
            {
                _artist.Draw();
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

            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(rotationAngle, rotationAxis);
            _artist.Draw();
        }

        /// <summary>
        /// Prepares the default view for the game field
        /// </summary>
        private void PrepareView()
        {
            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongX), Axis.X);

            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongY), Axis.Y);

            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(new Angle(_settings.DefaultCameraRotationAngleAlongZ), Axis.Z);

            _artist.Draw();
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

            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(_currentRotationAngle, _currentRotationAxis);
            _artist.Draw();

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

            _artist.MoveCamera(_currentDisplacementVector);
            _artist.Draw();

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
            _artist.RotateCameraRoundCenterOfCoordinatesRoundAxis(_currentRotationAngle, _currentRotationAxis);
            _artist.Draw();
        }

        private void HandleTimerTickForCameraDisplacement(object sender, EventArgs e)
        {
            _artist.MoveCamera(_currentDisplacementVector);
            _artist.Draw();
        }

        protected /*abstract*/virtual void DisableAllGameFlowControlButtons()
        {

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

        private void FillGameField()
        {
            CellState cellState;
            IGameField gameField = _gameInfo.GameField;
            foreach (Cell cell in gameField)
            {
                cellState = gameField.GetCellState(cell);
                if (cellState != CellState.Empty)
                {
                    _artist.AddGameObject((Side)cellState, cell);
                }
            }

            _artist.Draw();
        }
    }
}
