using TicTacToe3D.GameStages.GameBuildingStage;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage
{
    public static class GameBuildingStageFactory
    {
        public static IWorkflowStep CreateGameBuildingStage()
        {
            return new GameBuildingDirector();
        }
    }
}