using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameStage;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.GameStage
{
    public static class GameStageFactory
    {
        public static IWorkflowStep CreateGameStage(GameInformation gameInformation)
        {
            return new GameStageDirector(gameInformation);
        }
    }
}