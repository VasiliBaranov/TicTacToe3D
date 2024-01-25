using System;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game rules
    /// </summary>
    [Serializable]
    public class GameRules : ICloneable
    {
        private readonly int _numberOfElementsInLineToWin;
        private readonly Side _startingSide;
        private readonly bool _notStartingSideWillHaveATurnAfterTheStartingSideVictory;

        /// <summary>
        /// Gets the number of elements to be placed in line to win
        /// </summary>
        public int NumberOfElementsInLineToWin
        {
            get
            {
                return _numberOfElementsInLineToWin;
            }
        }

        /// <summary>
        /// Gets the starting side
        /// </summary>
        public Side StartingSide
        {
            get
            {
                return _startingSide;
            }
        }

        /// <summary>
        /// Indicates, whether a not starting side will have a turn after the starting side victory
        /// </summary>
        public bool NotStartingSideWillHaveATurnAfterTheStartingSideVictory
        {
            get
            {
                return _notStartingSideWillHaveATurnAfterTheStartingSideVictory;
            }
        }

        public GameRules(int numberOfElementsInLineToWin, Side startingSide, bool notStartingSideWillHaveATurnAfterTheStartingSideVictory)
        {
            _numberOfElementsInLineToWin = numberOfElementsInLineToWin;
            _startingSide = startingSide;
            _notStartingSideWillHaveATurnAfterTheStartingSideVictory = notStartingSideWillHaveATurnAfterTheStartingSideVictory;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new GameRules(_numberOfElementsInLineToWin, _startingSide, _notStartingSideWillHaveATurnAfterTheStartingSideVictory);
        }

        #endregion
    }
}
