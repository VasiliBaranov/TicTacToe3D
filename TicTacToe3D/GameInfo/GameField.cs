using System;
using System.Collections.Generic;
using System.Collections;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents a game field
    /// </summary>
    [Serializable]
    public class GameField : IGameField
    {
        #region Fields

        private readonly GameFieldParameters _gameFieldParameters;
        private readonly CellState[, ,] _gameFieldCellStates;
        private readonly List<Cell> _cells = new List<Cell>();

        #endregion

        /// <summary>
        /// Creates a new game field with game field parameters, initializes all cell states as empty
        /// </summary>
        /// <param name="gameFieldParameters">game field parameters to be used</param>
        public GameField(GameFieldParameters gameFieldParameters)
        {
            _gameFieldParameters = gameFieldParameters;

            _gameFieldCellStates =
                new CellState[
                _gameFieldParameters.SizeAlongX,
                _gameFieldParameters.SizeAlongY,
                _gameFieldParameters.SizeAlongZ];

            FillInAvailableCellsList();

            AssignDefaultCellStates();
        }

        #region IGameField Members

        /// <summary>
        /// Gets a cell state of a cell
        /// </summary>
        /// <param name="cell">Cell to be examined</param>
        /// <returns>Cell state of the given cell</returns>
        public CellState GetCellState(Cell cell)
        {
            return _gameFieldCellStates[
                cell.I - _gameFieldParameters.MinCoordinateAlongX,
                cell.J - _gameFieldParameters.MinCoordinateAlongY,
                cell.K - _gameFieldParameters.MinCoordinateAlongZ];
        }

        /// <summary>
        /// Sets a cell state of a cell
        /// </summary>
        /// <param name="cell">Cell which state will be modified</param>
        /// <param name="cellState">Cell state to set</param>
        public void SetCellState(Cell cell, CellState cellState)
        {
            _gameFieldCellStates[
                cell.I - _gameFieldParameters.MinCoordinateAlongX,
                cell.J - _gameFieldParameters.MinCoordinateAlongY,
                cell.K - _gameFieldParameters.MinCoordinateAlongZ] = cellState;
        }

        /// <summary>
        /// Determines whether the game field contains any cells with such coordinates
        /// </summary>
        /// <param name="cell">Cell to be examined</param>
        /// <returns>True if a cell with the same coordinates exists is present the game field, false otherwise</returns>
        public bool Contains(Cell cell)
        {
            return
                cell.I >= _gameFieldParameters.MinCoordinateAlongX &&
                cell.J >= _gameFieldParameters.MinCoordinateAlongY &&
                cell.K >= _gameFieldParameters.MinCoordinateAlongZ &&

                cell.I < _gameFieldParameters.MinCoordinateAlongX + _gameFieldParameters.SizeAlongX &&
                cell.J < _gameFieldParameters.MinCoordinateAlongY + _gameFieldParameters.SizeAlongY &&
                cell.K < _gameFieldParameters.MinCoordinateAlongZ + _gameFieldParameters.SizeAlongZ;
        }

        /// <summary>
        /// Game field parameters, such as game field size, minimal cell coordinates, etc.
        /// </summary>
        public GameFieldParameters GameFieldParameters
        {
            get
            {
                return _gameFieldParameters;
            }
        }

        #endregion

        /// <summary>
        /// List of cells present in the field. Used just by the enumerator
        /// </summary>
        internal List<Cell> Cells
        {
            get
            {
                return new List<Cell>(_cells);
            }
        }

        private void FillInAvailableCellsList()
        {
            for (int i = _gameFieldParameters.MinCoordinateAlongX; i < _gameFieldParameters.SizeAlongX; i++)
            {
                for (int j = _gameFieldParameters.MinCoordinateAlongY; j < _gameFieldParameters.SizeAlongY; j++)
                {
                    for (int k = _gameFieldParameters.MinCoordinateAlongZ; k < _gameFieldParameters.SizeAlongZ; k++)
                    {
                        _cells.Add(new Cell(i, j, k));
                    }
                }
            }
        }

        private void AssignDefaultCellStates()
        {
            for (int i = 0; i < _gameFieldParameters.SizeAlongX; i++)
            {
                for (int j = 0; j < _gameFieldParameters.SizeAlongY; j++)
                {
                    for (int k = 0; k < _gameFieldParameters.SizeAlongZ; k++)
                    {
                        _gameFieldCellStates[i, j, k] = CellState.Empty;
                    }
                }
            }
        }

        #region IEnumerable<Cell> Members

        /// <summary>
        /// Gets an enumerator to enumerate through all the game field cells
        /// </summary>
        /// <returns>An enumerator to enumerate through all the game field cells</returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            return new GameFieldEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// Gets an enumerator to enumerate through all the game field cells
        /// </summary>
        /// <returns>An enumerator to enumerate through all the game field cells</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GameFieldEnumerator(this);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            GameFieldParameters gameFieldParametersCopy =
                (GameFieldParameters)_gameFieldParameters.Clone();

            GameField gameFieldCopy = new GameField(gameFieldParametersCopy);

            for (int i = 0; i < _gameFieldParameters.SizeAlongX; i++)
            {
                for (int j = 0; j < _gameFieldParameters.SizeAlongY; j++)
                {
                    for (int k = 0; k < _gameFieldParameters.SizeAlongZ; k++)
                    {
                        gameFieldCopy._gameFieldCellStates[i, j, k] = _gameFieldCellStates[i, j, k];
                    }
                }
            }

            return gameFieldCopy;
        }

        #endregion
    }
}
