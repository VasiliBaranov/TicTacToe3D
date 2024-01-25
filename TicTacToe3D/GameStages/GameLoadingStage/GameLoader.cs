using System;
using System.Windows.Forms;

using TicTacToe3D.GameInfoSerialization;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfoSerialization.Interfaces;
using TicTacToe3D.GameStages.GameLoadingStage.Event;
using TicTacToe3D.GameStages.GameLoadingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameLoadingStage
{
    public partial class GameLoader : UserControl, IGameLoader
    {

        private const string _unfinishedGameFilesFilter = "Unfinished game files";
        private const string _allFilesFilter = "All files";
        private const string _delimiter = "|";

        private GameInformation _gameInfo;

        public GameLoader()
        {
            InitializeComponent();
        }

        #region IGameLoader Members

        public event EventHandler<GameContinuingEventArgs> GameContinuing;

        public event EventHandler Exitting;

        public event EventHandler GoingBack;

        #endregion

        private void HandleLoadGameButtonClick(object sender, EventArgs e)
        {
            if (LoadUnfinishedGame())
            {
                //show game info

                playButton.Enabled = true;
            }
        }

        private void HandleExitButtonClick(object sender, EventArgs e)
        {
            if (Exitting != null)
            {
                Exitting(this, e);
            }
        }

        private void HandleBackButtonClick(object sender, EventArgs e)
        {
            if (GoingBack != null)
            {
                GoingBack(this, e);
            }
        }

        private void HandlePlayButtonClick(object sender, EventArgs e)
        {
            if (GameContinuing != null)
            {
                GameContinuing(this, new GameContinuingEventArgs(_gameInfo));
            }
        }

        //may be i should implement it as a command (see gang of four design patterns)
        private bool LoadUnfinishedGame()
        {
            IGameInfoSerializer gameInfoSerializer = GameInfoSerializerFactory.CreateGameInfoSerializer();

            string unfinishedGameExtension = gameInfoSerializer.UnfinishedGameExtension;
            loadUnfinishedGameDialog.DefaultExt = unfinishedGameExtension;

            //"Unfinished game files|*." + unfinishedGameExtension + "|All files|*.*";
            loadUnfinishedGameDialog.Filter = _unfinishedGameFilesFilter + _delimiter + "*." + unfinishedGameExtension +
                                              _delimiter + _allFilesFilter + _delimiter + "*.*";

            if (loadUnfinishedGameDialog.ShowDialog() == DialogResult.OK)
            {
                _gameInfo = gameInfoSerializer.LoadUnfinishedGame(loadUnfinishedGameDialog.FileName);

                return true;
            }
            return false;
        }
    }
}