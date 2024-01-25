using System;
using System.Collections.Generic;
using System.Windows.Forms;

using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfoSerialization;
using TicTacToe3D.GameInfoSerialization.Interfaces;
using TicTacToe3D.GameStages.GameStatisticsStage.Interfaces;

namespace TicTacToe3D.GameStages.GameStatisticsStage
{
    public partial class GameStatistics : UserControl, IGameStatistics
    {
        private readonly List<PlayerStatistics> _playersStatistics;

        private GameInformation _gameInfo;

        private const string _replayFilesFilter = "Game replay files";
        private const string _allFilesFilter = "All files";
        private const string _delimiter = "|";

        public GameStatistics()
        {
            InitializeComponent();

            _playersStatistics = new List<PlayerStatistics> {playerStatistics1, playerStatistics2};
        }



        #region IGameStatistics Members

        public void ShowStatisticsOnGame(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;

            IEnumerator<PlayerInformation> playersEnumerator = gameInfo.Players.GetEnumerator();

            playersEnumerator.Reset();

            foreach (PlayerStatistics playerStatistics in _playersStatistics)
            {
                if (!playersEnumerator.MoveNext())
                {
                    playerStatistics.Visible = false;
                }

                playerStatistics.ShowStatisticsOnPlayer(gameInfo, playersEnumerator.Current);
            }
        }

        public event EventHandler StatisticsConfirmed;

        public event EventHandler Exitting;

        #endregion

        private void HandleSaveReplayButtonClickEvent(object sender, EventArgs e)
        {
            IGameInfoSerializer gameInfoSerializer = GameInfoSerializerFactory.CreateGameInfoSerializer();
            string replayExtension = gameInfoSerializer.ReplayExtension;
            saveReplayDialog.DefaultExt = replayExtension;
            //"Game replay files|*." + replayExtension + "|All files|*.*";
            saveReplayDialog.Filter = _replayFilesFilter + _delimiter + "*." + replayExtension +
                                      _delimiter + _allFilesFilter + _delimiter + "*.*";

            if (saveReplayDialog.ShowDialog() == DialogResult.OK)
            {
                gameInfoSerializer.SaveReplay(saveReplayDialog.FileName, _gameInfo);

                saveReplayButton.Enabled = false;

                MessageBox.Show("Save successfull");
            }
        }

        private void HandleNextButtonClickEvent(object sender, EventArgs e)
        {
            if (StatisticsConfirmed != null)
            {
                StatisticsConfirmed(this, e);
            }
        }

        private void HandleExitButtonClickEvent(object sender, EventArgs e)
        {
            if (Exitting != null)
            {
                Exitting(this, e);
            }
        }
    }
}