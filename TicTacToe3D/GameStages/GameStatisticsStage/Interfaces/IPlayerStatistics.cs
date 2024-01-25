using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameStages.GameStatisticsStage.Interfaces
{
    public interface IPlayerStatistics
    {
        void ShowStatisticsOnPlayer(GameInformation gameInfo, PlayerInformation playerInfo);
    }
}