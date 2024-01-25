using System;
using System.Collections.Generic;

namespace TicTacToe3D.GameInfo.Interfaces
{
    /// <summary>
    /// Represents a game field
    /// </summary>
    public interface IGameField: IEnumerable<Cell>, ICloneable
    {
        #region Methods

        /// <summary>
        /// Gets a cell state of a cell
        /// </summary>
        /// <param name="cell">Cell to be examined</param>
        /// <returns>Cell state of the given cell</returns>
        CellState GetCellState(Cell cell);

        /// <summary>
        /// Sets a cell state of a cell
        /// </summary>
        /// <param name="cell">Cell which state will be modified</param>
        /// <param name="cellState">Cell state to set</param>
        void SetCellState(Cell cell, CellState cellState);

        /// <summary>
        /// Determines whether the game field contains any cell with such coordinates
        /// </summary>
        /// <param name="cell">Cell to be examined</param>
        /// <returns>True if a cell with the same coordinates exists is present the game field, false otherwise</returns>
        bool Contains(Cell cell);

        #endregion

        #region Properties

        /// <summary>
        /// Game field parameters, such as game field size, minimal cell coordinates, etc.
        /// </summary>
        GameFieldParameters GameFieldParameters
        {
            get;
        }

        #endregion
    }
}