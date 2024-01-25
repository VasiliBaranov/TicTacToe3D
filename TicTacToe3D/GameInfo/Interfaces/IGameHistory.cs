using System;
using System.Collections.Generic;

namespace TicTacToe3D.GameInfo.Interfaces
{
    /// <summary>
    /// Represents a game history, i.e. a collection of turns
    /// </summary>
    public interface IGameHistory : IEnumerable<Turn>, ICloneable
    {
        /// <summary>
        /// Gets or sets game start time and date
        /// </summary>
        DateTime GameStart
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets game end time and date
        /// </summary>
        DateTime GameEnd
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a turn to the game history
        /// </summary>
        /// <param name="turn">A turn to be added</param>
        void AddTurn(Turn turn);

        /// <summary>
        /// Appends an existing history to the current history
        /// </summary>
        /// <param name="gameHistory">Existing game history</param>
        void AppendHistory(IGameHistory gameHistory);
    }
}