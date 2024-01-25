using TicTacToe3D.GameStages.MainGameMenuStage;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.MainGameMenuStage
{
    public static class MainGameMenuStageFactory
    {
        public static IWorkflowStep CreateMainGameMenuStage()
        {
            return new MainGameMenuDirector();
        }
    }
}