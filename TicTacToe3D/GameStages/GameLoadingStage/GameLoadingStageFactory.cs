using TicTacToe3D.GameStages.GameLoadingStage;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.GameLoadingStage
{
    public static class GameLoadingStagFactory
    {
        public static IWorkflowStep CreateGameLoadingStage()
        {
            return new GameLoadingDirector();
        }
    }
}