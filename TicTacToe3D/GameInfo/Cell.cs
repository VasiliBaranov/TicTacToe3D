using System;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents a game field cell
    /// </summary>
    [Serializable]
    public class Cell : ICloneable
    {
        #region Fields

        private int _i;
        private int _j;
        private int _k;

        #endregion

        #region Properties

        /// <summary>
        /// Coordinate I (along x axis)
        /// </summary>
        public int I
        {
            get
            {
                return _i;
            }
            set
            {
                _i = value;
            }
        }

        /// <summary>
        /// Coordinate J (along y axis)
        /// </summary>
        public int J
        {
            get
            {
                return _j;
            }
            set
            {
                _j = value;
            }
        }

        /// <summary>
        /// Coordinate K (along z axis)
        /// </summary>
        public int K
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value;
            }
        }
        #endregion

        public Cell(int i, int j, int k)
        {
            _i = i;
            _j = j;
            _k = k;
        }

        /// <summary>
        /// Calculates a new cell with coordinates equal to the sum of the corresponding coordinates of the cells
        /// </summary>
        /// <param name="cell1">The first cell</param>
        /// <param name="cell2">The second cell</param>
        /// <returns>A new cell with coordinates equal to the sum of the corresponding coordinates of the cells</returns>
        public static Cell operator +(Cell cell1, Cell cell2)
        {
            Cell result = new Cell(cell1._i + cell2._i, cell1._j + cell2._j, cell1._k + cell2._k);
            return result;
        }

        /// <summary>
        /// Calculates a new cell with coordinates equal to the difference of the corresponding coordinates of the cells
        /// </summary>
        /// <param name="cell1">The first cell</param>
        /// <param name="cell2">The second cell</param>
        /// <returns>A new cell with coordinates equal to the difference of the corresponding coordinates of the cells</returns>
        public static Cell operator -(Cell cell1, Cell cell2)
        {
            Cell result = new Cell(cell1._i - cell2._i, cell1._j - cell2._j, cell1._k - cell2._k);
            return result;
        }

        /// <summary>
        /// Calculates a new cell with coordinates opposite to the corresponding coordinates of the cell passed
        /// </summary>
        /// <param name="cell">Cell to be reversed</param>
        /// <returns>A new cell with coordinates opposite to the corresponding coordinates of the cell passes</returns>
        public static Cell operator -(Cell cell)
        {
            Cell result = new Cell(-cell._i, -cell._j, -cell._k);
            return result;
        }

        /// <summary>
        /// Calculates a new cell with coordinates multiplied by a number
        /// </summary>
        /// <param name="a"></param>
        /// <param name="cell">Cell to be used</param>
        /// <returns>A new cell with coordinates  multiplied by a number</returns>
        public static Cell operator *(int a, Cell cell)
        {
            Cell result = new Cell(a * cell._i, a * cell._j, a * cell._k);
            return result;
        }

        /// <summary>
        /// Determines if the cell is equal to the current instance
        /// </summary>
        /// <param name="obj">Cell comparant</param>
        /// <returns>True if both cells have equal coordinates, otherwise false</returns>
        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;

            if (cell == null)
            {
                return false;
            }

            return cell._i == this._i &&
                   cell._j == this._j &&
                   cell._k == this._k;
        }

        public override int GetHashCode()
        {
            return _i ^ _j ^ _k;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Cell(_i, _j, _k);
        }

        #endregion
    }
}
