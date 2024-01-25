using System;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game turn
    /// </summary>
    [Serializable]
    public class Turn : ICloneable
    {
        private Cell _modifiedCell;
        private Side _modifierSide;

        /// <summary>
        /// Gets or sets the cell, which was modified in the turn
        /// </summary>
        public Cell ModifiedCell
        {
            get
            {
                return _modifiedCell;
            }
            set
            {
                _modifiedCell = value;
            }
        }

        /// <summary>
        /// Gets or sets the side of the player, who made the turn
        /// </summary>
        public Side ModifierSide
        {
            get
            {
                return _modifierSide;
            }
            set
            {
                _modifierSide = value;
            }
        }

        public Turn(Cell modifiedCell, Side modifierSide)
        {
            _modifiedCell = modifiedCell;
            _modifierSide = modifierSide;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Turn(_modifiedCell, _modifierSide);
        }

        #endregion
    }
}
