using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer.Events
{
    /// <summary>
    /// Provides data for the TurnMade Event of a IPlayer.
    /// </summary>
    public class TurnMadeEventArgs : EventArgs
    {
        private readonly Cell _recentlyModifiedCell;

        /// <summary>
        /// Gets a cell, recently modified by the player
        /// </summary>
        public Cell RecentlyModifiedCell
        {
            get
            {
                return _recentlyModifiedCell;
            }
        }

        /// <summary>
        /// Initializes a new instance of the TurnMadeEventArgs using the specified cell
        /// </summary>
        /// <param name="recentlyModifiedCell">Cell, recently modified by the player</param>
        public TurnMadeEventArgs(Cell recentlyModifiedCell)
        {
            _recentlyModifiedCell = recentlyModifiedCell;
        }
    }
}