namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game status
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// Game has been finished successfully (i.e. one or two winners found)
        /// </summary>
        Finished = 0,

        /// <summary>
        /// Means that there are no free cells on the game field, and there are no winners
        /// </summary>
        NoPlaceAvailable = 1,

        /// <summary>
        /// Game has not been finished yet
        /// </summary>
        Unfinished = 2
    }
}
