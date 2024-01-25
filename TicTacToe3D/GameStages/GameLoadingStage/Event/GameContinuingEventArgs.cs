using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameStages.GameLoadingStage.Event
{
    /// <summary>
    /// Provides data for continuing an unfinished game
    /// </summary>
    public class GameContinuingEventArgs : EventArgs
    {
        private GameInformation _gameInfo;

        /// <summary>
        /// Gets file name (which doesn't contain folder info) to use to save replay
        /// </summary>
        public GameInformation GameInfo
        {
            get
            {
                return _gameInfo;
            }
        }

        /// <summary>
        /// Initializes a new instance of the GameContinuingEventArgs class using the specified game info
        /// </summary>
        /// <param name="gameInfo">File name to save to (should not contain path)</param>
        public GameContinuingEventArgs(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;
        }
    }
}