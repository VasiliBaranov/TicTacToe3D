using System.Collections.Generic;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.AI.Interfaces
{
    /// <summary>
    /// Represents some basic logic for the game
    /// </summary>
    public interface IGameAssistant
    {
        /// <summary>
        /// Determines if the player of the given side wins
        /// </summary>
        /// <param name="side">Side of the player</param>
        /// <returns>true if such a player wins, false otherwise</returns>
        bool IsWinner(Side side);

        Side GetWinner();

        List<Cell> GetEmptyCells();


    }
}