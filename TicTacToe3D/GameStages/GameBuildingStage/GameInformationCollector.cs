using System;
using System.Windows.Forms;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage
{
    public partial class GameInformationCollector : UserControl, IGameInformationCollector
    {
        private GameInformation _gameInfo=new GameInformation();

        public GameInformationCollector()
        {
            InitializeComponent();
            playerInfoListCollector.PlayerInfoListChanged += HandlePlayersInfoListChangedEvent;
        }

        #region IGameInformationCollector Members

        public GameInformation GameInformation
        {
            get 
            {
                return _gameInfo;
            }
        }

        public event EventHandler GameInfoCollectionFinished;

        public void StartInfoCollection()
        {
            _gameInfo = new GameInformation
                            {
                                GameType = GameType.SingleComputer,
                                GameStatus = GameStatus.Unfinished,
                                GameRules = new GameRules(3, Side.X, true),
                                GameField = new GameField(new GameFieldParameters(3, 3, 3)),
                                GameHistory = new GameHistory()
                            };

            playerInfoListCollector.StartInfoCollection();
            Enabled = true;
        }

        public void StopInfoCollection()
        {
            playerInfoListCollector.StartInfoCollection();
            Enabled = false;
        }

        #endregion

        private static void HandlePlayersInfoListChangedEvent(object sender, EventArgs e)
        {
            //_gameInfo.Players = playerInfoCollectorsDirector.PlayersInformationList;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            _gameInfo.Players = playerInfoListCollector.PlayerInfoList;
            if (GameInfoCollectionFinished != null)
            {
                GameInfoCollectionFinished(this, e);
            }
        }
    }
}