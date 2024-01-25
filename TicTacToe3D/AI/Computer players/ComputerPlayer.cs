using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.AI
{
    //this class is almost a stub, as it works just with 3x3x3 game field and some other restrictions (see below)
    /// <summary>
    /// Computer player
    /// </summary>
    public abstract class ComputerPlayer : IPlayer
    {
        #region Fields

        private IList<PlayerInformation> _playerInfos;
        private PlayerInformation _currentPlayerInfo;
        private GameInformation _gameInfo;
        private List<Cell> _availableDirectionsWithoutOppositeDirections;

        #endregion

        protected GameInformation GameInfo
        {
            get
            {
                return _gameInfo;
            }
        }

        #region Constructors

        protected ComputerPlayer()
        {
            _playerInfos = new List<PlayerInformation>();
        }

        #endregion

        #region IPlayer Members

        public IList<PlayerInformation> AvailablePlayerInfos
        {
            get
            {
                return _playerInfos;
            }
            set
            {
                if (value.Count > 1)
                {
                    throw new NotImplementedException("this class doesn't support multiple players representation");
                }
                _playerInfos = value;
            }
        }

        public PlayerInformation CurrentPlayerInfo
        {
            get
            {
                return _currentPlayerInfo;
            }
            set
            {
                if (!_playerInfos.Contains(value))
                {
                    throw new InvalidOperationException("New player info isn't present in the available player infos list");
                }
                _currentPlayerInfo = value;
            }
        }

        public void MakeTurn()
        {
            Cell appropriateCell = ChooseTheMostAppropriateCell();

            TurnMade(this, new TurnMadeEventArgs(appropriateCell));
        }

        public void RollbackLastTurn(string message)
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public event EventHandler<TurnMadeEventArgs> TurnMade;

        public event EventHandler AvailablePlayerInfosChanged;

        #endregion

        #region IParticipant Members

        public void PrepareForGame(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;

            //this class works just with these game field parameters
            GameFieldParameters gameFieldParameters = _gameInfo.GameField.GameFieldParameters;
            if (
                gameFieldParameters.MinCoordinateAlongX != 0 ||
                gameFieldParameters.MinCoordinateAlongY != 0 ||
                gameFieldParameters.MinCoordinateAlongZ != 0 ||
                gameFieldParameters.SizeAlongX != 3 ||
                gameFieldParameters.SizeAlongY != 3 ||
                gameFieldParameters.SizeAlongZ != 3)
            {
                throw new NotImplementedException();
            }

            _availableDirectionsWithoutOppositeDirections = GetAvailableDirectionsWithoutOppositeDirections();

            PreparedForGame(this, new EventArgs());
        }

        public void UpdateGameInformation(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;
        }

        public void StartGame()
        {

        }

        public void ModifyCell(Cell cell, Side side)
        {
            _gameInfo.GameField.SetCellState(cell, (CellState)side);
        }

        public void HandleGameTermination(GameInformation gameInformation)
        {
            GameTerminationHandled(this, new EventArgs());
        }

        public event EventHandler PreparedForGame;

        public event EventHandler GameTerminationHandled;

        public event EventHandler GameTerminating;

        public event EventHandler LeavingGame;

        #endregion


        #region Addidtional Methods

        /// <summary>
        /// Chooses the most appropriate cell for the turn according to the current game information
        /// </summary>
        /// <returns>Cell which is the most appropriate candidate for turn</returns>
        private Cell ChooseTheMostAppropriateCell()
        {
            if (GameFieldIsEmpty())
            {
                return ChooseTheMostAppropriateCellWhenGameFieldIsEmpty();
            }

            List<Cell> emptyCells = GetEmptyCells();

            List<ValuableCell> evaluatedCells = EvaluateCells(emptyCells);

            return ChooseTheMostAppropriateCellFromEvaluatedCells(evaluatedCells);
        }

        /// <summary>
        /// Chooses the most appropriate cell for the turn among the list of evaluated cells
        /// </summary>
        /// <param name="evaluatedCells">List of evaluated cells to be the source of choice. 
        /// The more the value of a cell, the better</param>
        /// <returns>A cell which should be modified during the turn</returns>
        protected abstract Cell ChooseTheMostAppropriateCellFromEvaluatedCells(List<ValuableCell> evaluatedCells);

        /// <summary>
        /// Assesses the value of the given direction for the given cell for the current player side
        /// </summary>
        /// <param name="startingCell">Cell to start assessment from</param>
        /// <param name="directionCell">Cell which determines the direction of the assessment</param>
        /// <returns></returns>
        private int AssessDirectionAndOppositeDirectionForTheCell(Cell startingCell, Cell directionCell)
        {
            if (_gameInfo.GameField.GetCellState(startingCell) != CellState.Empty)
            {
                return 0;
            }


            int numberOfEmtyCellsOnTheDirection = 0;
            int numberOfXCellsOnTheDirection = 0;
            int numberOfOCellsOnTheDirection = 0;


            //moving forward along the direction
            GetNumberOfDifferentCellTypesOnTheDirection(startingCell, directionCell,
                ref numberOfEmtyCellsOnTheDirection, ref numberOfXCellsOnTheDirection, ref numberOfOCellsOnTheDirection);

            //moving backward along the direction
            GetNumberOfDifferentCellTypesOnTheDirection(startingCell, -directionCell,
                ref numberOfEmtyCellsOnTheDirection, ref numberOfXCellsOnTheDirection, ref numberOfOCellsOnTheDirection);

            //as the starting cell has been counted twice
            numberOfEmtyCellsOnTheDirection--;

            //there are less than NumberOfElementsInLineToWin cells on this line
            if ((numberOfOCellsOnTheDirection + numberOfEmtyCellsOnTheDirection + numberOfXCellsOnTheDirection)
                != _gameInfo.GameRules.NumberOfElementsInLineToWin)
            {
                return 0;
            }

            //actually evaluating            
            return AssessDirectionAndOppositeDirectionForTheCellByCellTypeNumbers(numberOfEmtyCellsOnTheDirection,
                numberOfXCellsOnTheDirection, numberOfOCellsOnTheDirection);
        }

        /// <summary>
        /// Assesses the value of the cells line for the current player side according to the 
        /// number of different cell types, present in this line
        /// </summary>
        /// <param name="numberOfEmptyCellsOnTheDirection">Number Of Empty Cells On The Direction</param>
        /// <param name="numberOfXCellsOnTheDirection">Number Of X Cells On The Direction</param>
        /// <param name="numberOfOCellsOnTheDirection">Number Of O Cells On The Direction</param>
        /// <returns></returns>
        private int AssessDirectionAndOppositeDirectionForTheCellByCellTypeNumbers(int numberOfEmptyCellsOnTheDirection,
            int numberOfXCellsOnTheDirection, int numberOfOCellsOnTheDirection)
        {
            int numberOfCurrentSideCells;
            int numberOfOppositeSideCells;

            //determine numberOfCurrentSideCells, numberOfOppositeSideCells
            if (_currentPlayerInfo.Side == Side.X)
            {
                numberOfCurrentSideCells = numberOfXCellsOnTheDirection;
                numberOfOppositeSideCells = numberOfOCellsOnTheDirection;
            }
            else
            {
                numberOfCurrentSideCells = numberOfOCellsOnTheDirection;
                numberOfOppositeSideCells = numberOfXCellsOnTheDirection;
            }

            GameRules gameRules = _gameInfo.GameRules;

            if (numberOfCurrentSideCells == 2)
            {
                if ((_currentPlayerInfo.Side == gameRules.StartingSide) && gameRules.NotStartingSideWillHaveATurnAfterTheStartingSideVictory)
                {
                    return 300;
                }
                else
                {
                    return 500;
                    //return 300;
                }
            }
            if (numberOfOppositeSideCells == 2)
            {
                if ((_currentPlayerInfo.Side == gameRules.StartingSide) && gameRules.NotStartingSideWillHaveATurnAfterTheStartingSideVictory)
                {
                    return 500;
                    //return 300;
                }
                else
                {
                    return 300;
                }
            }
            if (numberOfOppositeSideCells == 0 && numberOfCurrentSideCells == 1)
            {
                return 5;
            }
            if (numberOfEmptyCellsOnTheDirection == 3)
            {
                return 3;
            }
            if (numberOfOppositeSideCells == 1 && numberOfCurrentSideCells == 0)
            {
                return 1;
            }

            //will never be achieved
            return 0;
        }


        /// <summary>
        /// Gets the number of different cell types, starting from the startingCell and moving in the direction, defined by the
        /// directionCell (without moving in the opposite direction), until it reaches the boundaries of the game field
        /// </summary>
        /// <param name="startingCell">Inclusive starting cell</param>
        /// <param name="directionCell">Direction cell</param>
        /// <param name="numberOfEmptyCellsOnTheDirection">Number Of Empty Cells On The Direction</param>
        /// <param name="numberOfXCellsOnTheDirection">Number Of X Cells On The Direction</param>
        /// <param name="numberOfOCellsOnTheDirection">Number Of O Cells On The Direction</param>
        private void GetNumberOfDifferentCellTypesOnTheDirection(Cell startingCell, Cell directionCell,
            ref int numberOfEmptyCellsOnTheDirection, ref int numberOfXCellsOnTheDirection, ref int numberOfOCellsOnTheDirection)
        {
            IGameField gameField = _gameInfo.GameField;

            Cell currentCell = startingCell;

            //while currnet cell is in the game field
            while (gameField.Contains(currentCell))
            {
                CellState currentCellState = gameField.GetCellState(currentCell);
                if (currentCellState == CellState.Empty)
                {
                    numberOfEmptyCellsOnTheDirection++;
                }
                else if (currentCellState == CellState.O)
                {
                    numberOfOCellsOnTheDirection++;
                }
                else if (currentCellState == CellState.X)
                {
                    numberOfXCellsOnTheDirection++;
                }

                //move along the direction
                currentCell = currentCell + directionCell;
            }
        }

        /// <summary>
        /// Evaluates every cell in the list
        /// </summary>
        /// <param name="cells">List of cells to be evaluated</param>
        /// <returns>List of evaluated cells</returns>
        private List<ValuableCell> EvaluateCells(List<Cell> cells)
        {
            List<ValuableCell> evaluatedCells = new List<ValuableCell>();

            foreach (Cell cell in cells)
            {
                int cellValue = EvaluateCell(cell);
                evaluatedCells.Add(new ValuableCell(cellValue, cell));
            }

            return evaluatedCells;
        }

        /// <summary>
        /// Evaluates a cell according to its position in the game field and the situation on the game field
        /// </summary>
        /// <param name="cell">cell to be evaluated</param>
        /// <returns>Cell value</returns>
        private int EvaluateCell(Cell cell)
        {
            int cellValue = 0;
            foreach (Cell direction in _availableDirectionsWithoutOppositeDirections)
            {
                cellValue += AssessDirectionAndOppositeDirectionForTheCell(cell, direction);
            }
            return cellValue;
        }

        /// <summary>
        /// Chooses the most appropriate cell for the turn when game field is empty yet. 
        /// This method is used just for optimization, the same results are produced by general evaluation procedure
        /// </summary>
        /// <returns>Cell which is the most appropriate candidate for turn</returns>
        protected abstract Cell ChooseTheMostAppropriateCellWhenGameFieldIsEmpty();

        private List<Cell> GetAvailableDirectionsWithoutOppositeDirections()
        {
            return GameAssistant.GetAvailableDirectionsWithoutOppositeDirections();
        }

        private List<Cell> GetEmptyCells()
        {
            return GameAssistant.GetEmptyCells(_gameInfo.GameField);
        }

        private bool GameFieldIsEmpty()
        {
            return GameAssistant.GameFieldIsEmpty(_gameInfo.GameField);
        }

        #endregion
    }
}
