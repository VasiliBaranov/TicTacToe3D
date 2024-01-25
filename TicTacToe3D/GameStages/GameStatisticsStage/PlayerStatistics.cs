using System.Windows.Forms;

using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameStatisticsStage.Interfaces;

namespace TicTacToe3D.GameStages.GameStatisticsStage
{
    public partial class PlayerStatistics : UserControl, IPlayerStatistics
    {
        private const string _playerWonText = "Won!";
        private const string _playerLostText = "Lost";


        public PlayerStatistics()
        {
            InitializeComponent();
        }

        #region IPlayerStatistics Members

        public void ShowStatisticsOnPlayer(GameInformation gameInfo, PlayerInformation playerInfo)
        {
            playerNameTextBox.Text = playerInfo.Name;

            playerSideLabel.Text = playerInfo.Side.ToString();

            playerVictoryLabel.Text = PlayerWon(gameInfo, playerInfo) ? _playerWonText : _playerLostText;
        }

        #endregion

        private static bool PlayerWon(GameInformation gameInfo, PlayerInformation playerInfo)
        {
            foreach (PlayerInformation winner in gameInfo.Winners)
            {
                if (playerInfo.Equals(winner))
                {
                    return true;
                }
            }

            return false;
        }
    }
}