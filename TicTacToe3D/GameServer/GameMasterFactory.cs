using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.GameMasterSteps;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer
{
    /// <summary>
    /// Represents a method for creating instances of a game master
    /// </summary>
    public static class GameMasterFactory
    {
        /// <summary>
        /// Returns a new game master instance
        /// </summary>
        /// <param name="gameInfo">Game information to initialize game master with</param>
        /// <returns>A new game master instance</returns>
        public static IGameMaster CreateGameMaster(GameInformation gameInfo)
        {
            ExtendedGameInfo extendedGameInfo = new ExtendedGameInfo(gameInfo);
            GameMaster gameMaster = new GameMaster(extendedGameInfo, new PreparationStep(extendedGameInfo));

            HistoryTracker historyTracker = new HistoryTracker();
            gameMaster.AddGameObserver(historyTracker);

            return gameMaster;
        }
    }
}
