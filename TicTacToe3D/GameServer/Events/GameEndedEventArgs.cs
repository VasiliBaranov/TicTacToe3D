using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer.Events
{
    /// <summary>
    /// Provides data for the GameEnded event of a GameMaster.
    /// </summary>
    public class GameEndedEventArgs : EventArgs
    {
        private readonly GameInformation _gameInformation;

        /// <summary>
        /// Gets information about the ended game
        /// </summary>
        public GameInformation GameInformation
        {
            get
            {
                return _gameInformation;
            }
        }

        /// <summary>
        /// Initializes a new instance of the GameEndedEventArgs class using the specified game information
        /// </summary>
        /// <param name="gameInformation">Game information to be used</param>
        public GameEndedEventArgs(GameInformation gameInformation)
        {
            _gameInformation = gameInformation;
        }
    }
}