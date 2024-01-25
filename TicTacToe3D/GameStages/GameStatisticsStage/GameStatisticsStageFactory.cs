using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe3D.GameStages.GameStatisticsStage;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStatisticsStage
{
    public static class GameStatisticsStageFactory
    {
        public static IWorkflowStep CreateGameStatisticsStage(GameInformation gameInfo)
        {
            return new GameStatisticsStageDirector(gameInfo);
        }
    }
}
