using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameInfoSerialization.Interfaces
{
    /// <summary>
    /// Defines methods for game info serialization.
    /// </summary>
    public interface IGameInfoSerializer
    {
        /// <summary>
        /// Loads the unfinished game.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        GameInformation LoadUnfinishedGame(string path);

        /// <summary>
        /// Loads the replay.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        GameInformation LoadReplay(string path);

        /// <summary>
        /// Saves the unfinished game.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="gameInfo">The game info.</param>
        void SaveUnfinishedGame(string path, GameInformation gameInfo);

        /// <summary>
        /// Saves the replay.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="gameInfo">The game info.</param>
        void SaveReplay(string path, GameInformation gameInfo);

        /// <summary>
        /// Gets the replay extension.
        /// </summary>
        /// <value>The replay extension.</value>
        string ReplayExtension
        {
            get;
        }

        /// <summary>
        /// Gets the unfinished game extension.
        /// </summary>
        /// <value>The unfinished game extension.</value>
        string UnfinishedGameExtension
        {
            get;
        }
    }
}