using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameStages.GameStatisticsStage.Interfaces
{
    public interface IGameStatistics
    {
        void ShowStatisticsOnGame(GameInformation gameInfo);

        event EventHandler StatisticsConfirmed;

        event EventHandler Exitting;
    }
}