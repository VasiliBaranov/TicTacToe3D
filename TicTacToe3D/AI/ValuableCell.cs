using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.AI
{
    /// <summary> 
    /// Repersents game cell with a value
    /// </summary>
    [Serializable]
    public class ValuableCell : Cell, IComparable<ValuableCell>
    {

        #region Fields

        /// <summary> 
        /// The value of the cell for the turn: the more,the better
        /// </summary>
        private int _value;

        #endregion

        /// <summary> 
        /// The value of the cell for the turn: the more,the better
        /// </summary>
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary> 
        /// Creates a new valuable cell by its value and coordinates
        /// </summary>
        public ValuableCell(int value, int i, int j, int k)
            : base(i, j, k)
        {
            _value = value;
        }

        /// <summary> 
        /// Creates a new valuable cell by its value and coordinates of some othe cell
        /// </summary>
        public ValuableCell(int value, Cell cell)
            : base(cell.I, cell.J, cell.K)
        {
            _value = value;
        }

        /// <summary>
        /// Compares the valuable cell to the instance
        /// </summary>
        /// <param name="valuableCell">Cell being compaerd</param>
        /// <returns>1 if the value of the instance is bigger than that of the compared cell,
        /// -1 if the value of the instance is less than that of the compared cell,
        /// 0 if two values are equal</returns>
        public int CompareTo(ValuableCell valuableCell)
        {
            return Math.Sign(this._value - valuableCell._value);
        }
    }
}
