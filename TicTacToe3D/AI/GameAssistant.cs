using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;

namespace TicTacToe3D.AI
{
    public static class GameAssistant
    {
        private const string _argumentNullExceptionText = "Argument is null";


        /// <summary>
        /// Gets winner of the game according to the game information. 
        /// Clears the winningCells list and stores there those cells that determine the winner.
        /// </summary>
        /// <param name="gameInfo">Game info according to which we should determine a winner</param>
        /// <param name="winningCells">List of cells that determine the winner (will be filled by the method)</param>
        /// <returns>The winner of the game. Null if there are no winners</returns>
        //this method will be removed, as it doesn't work with multiple winners
        public static PlayerInformation GetWinner(GameInformation gameInfo, List<Cell> winningCells)
        {
            if (gameInfo == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }
            foreach (PlayerInformation player in gameInfo.Players)
            {
                if (SideWins(gameInfo.GameField, gameInfo.GameRules, player.Side, winningCells))
                {
                    return player;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether the player with the given side wins.
        /// Clears the winningCells list and stores there those cells that determine the winner.
        /// </summary>
        /// <param name="gameField">Game field to examine</param>
        /// <param name="gameRules">Game rules to use</param>
        /// <param name="side">Side of the player</param>
        /// <param name="winningCells">List of cells that determine the winner (will be filled by the method)</param>
        /// <returns>True if the player of the given side wins, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Argument is null</exception>
        public static bool SideWins(IGameField gameField, GameRules gameRules, Side side, List<Cell> winningCells)
        {
            if (gameField == null || gameRules == null || winningCells == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }

            foreach (Cell direction in GetAvailableDirectionsWithoutOppositeDirections())
            {
                if (SideWinsOnTheDirection(gameField, gameRules, side, winningCells, direction))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the side wins on the direction.
        /// Clears the winningCells list and stores there those cells that determine the winner
        /// </summary>
        /// <param name="gameField">Game field to be examined</param>
        /// <param name="gameRules">Game rules to use</param>
        /// <param name="side">Side to examine</param>
        /// <param name="winningCells">List of cells that determine the winner (will be filled by the method)</param>
        /// <param name="directionCell">Direction to examine</param>
        /// <returns>True, if the side wins on the direction, false otherwise</returns>
        private static bool SideWinsOnTheDirection(IGameField gameField, GameRules gameRules,
            Side side, List<Cell> winningCells, Cell directionCell)
        {
            List<Cell> availableStartingCells = GetGameFieldCells(gameField);

            //while there are cells to start with
            while (availableStartingCells.Count > 0)
            {
                //removes all the unsuitable cells until it encounters a cell of the given side
                if (gameField.GetCellState(availableStartingCells[0]) != (CellState)side)
                {
                    availableStartingCells.RemoveAt(0);
                    continue;
                }

                //then gets cells of the same type on the direction, starting from the first available cell
                List<Cell> candidateCells = 
                    GetCellsOfTheSameTypeOnTheDirectionStartingFromCell(gameField, availableStartingCells[0], directionCell);

                //and gets cells of the same type on the opposite direction, starting from the first available cell
                List<Cell> candidateCellsOnTheOppositeDirection = 
                    GetCellsOfTheSameTypeOnTheDirectionStartingFromCell(gameField, availableStartingCells[0], -directionCell);

                //here we remove the first available cell from the opposite direction list, 
                //as it has already been added to the first list
                candidateCellsOnTheOppositeDirection.Remove(availableStartingCells[0]);

                //unite both lists
                candidateCells.AddRange(candidateCellsOnTheOppositeDirection);

                //if they contain enough cells
                if (candidateCells.Count == gameRules.NumberOfElementsInLineToWin)
                {
                    //assign winning cells
                    winningCells = candidateCells;
                    return true;
                }

                //here we remove those cells, that have already been examined
                foreach (Cell cell in candidateCells)
                {
                    availableStartingCells.Remove(cell);
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a list of game field cells on the basis of game field default enumerator
        /// </summary>
        /// <param name="gameField">Game field to be used</param>
        /// <returns></returns>
        private static List<Cell> GetGameFieldCells(IGameField gameField)
        {
            List<Cell> gameFieldCells = new List<Cell>();

            foreach (Cell cell in gameField)
            {
                gameFieldCells.Add(cell);
            }

            return gameFieldCells;
        }

        /// <summary>
        /// Gets cells of the same type on the direction starting from cell. 
        /// Method determines the cell state of the starting cell and moves in the given direction from the starting cell, 
        /// until it reaches either a cell with different state or a game field boundary.
        /// </summary>
        /// <param name="gameField">Game filed to be examined</param>
        /// <param name="startingCell">Inclusive starting cell</param>
        /// <param name="directionCell">Direction cell</param>
        /// <returns>List of cells, allocated on the direction, which possess the same state, as the starting cell. 
        /// The list includes the starting cell</returns>
        private static List<Cell> GetCellsOfTheSameTypeOnTheDirectionStartingFromCell(IGameField gameField, Cell startingCell, Cell directionCell)
        {
            Cell currentCell = startingCell;
            List<Cell> equalTypeCells = new List<Cell>();

            while (gameField.Contains(currentCell))
            {
                if (gameField.GetCellState(currentCell) == gameField.GetCellState(startingCell))
                {
                    equalTypeCells.Add(currentCell);
                    currentCell = currentCell + directionCell;
                }
                else 
                {
                    break;
                }
            }

            return equalTypeCells;
        }

        /// <summary>
        /// Gets available directions in the game field of 3 dimensions. 
        /// </summary>
        /// <returns>List of cells representing these directions. 
        /// There are no cells in the list which can be derived from the existing cells by inversion</returns>
        public static List<Cell> GetAvailableDirectionsWithoutOppositeDirections()
        {
            List<Cell> availableDirections = new List<Cell>(10);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        if ((i == -1) ||
                            (i == 0 && j == -1) ||
                            (i == 0 && j == 0 && k == -1) ||
                            (i == 0 && j == 0 && k == 0))
                        {
                            continue;
                        }

                        availableDirections.Add(new Cell(i, j, k));
                    }
                }

            }
            return availableDirections;
        }

        /// <summary>
        /// Get empty cells, present in the game field
        /// </summary>
        /// <param name="gameField">Game field to be examined</param>
        /// <returns>List of empty cells, present in the game field</returns>
        /// <exception cref="ArgumentNullException">Game field is null</exception>
        public static List<Cell> GetEmptyCells(IGameField gameField)
        {
            if (gameField == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }

            List<Cell> emptyCells = new List<Cell>();

            foreach (Cell cell in gameField)
            {
                if (gameField.GetCellState(cell) == CellState.Empty)
                {
                    emptyCells.Add(cell);
                }
            }

            return emptyCells;
        }

        /// <summary>
        /// Determines whether the game field is empty
        /// </summary>
        /// <param name="gameField">Game field to be examined</param>
        /// <returns>True, if game field is empty, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Game field is null</exception>
        public static bool GameFieldIsEmpty(IGameField gameField)
        {
            if (gameField == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }

            GameFieldParameters gameFieldParameters = gameField.GameFieldParameters;
            int overallCellNumber = gameFieldParameters.SizeAlongX * gameFieldParameters.SizeAlongY * gameFieldParameters.SizeAlongZ;

            return GetEmptyCells(gameField).Count == overallCellNumber;
        }

        /// <summary>
        /// Determines whether the game field has empty cells
        /// </summary>
        /// <param name="gameField">Game field to be examined</param>
        /// <returns>True, if game field has empty cells, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Game field is null</exception>
        public static bool HasEmptyCells(IGameField gameField)
        {
            if (gameField == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }

            return GetEmptyCells(gameField).Count > 0;
        }
    }
}
