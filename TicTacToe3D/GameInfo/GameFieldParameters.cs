using System;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game fiels parameters
    /// </summary>
    [Serializable]
    public class GameFieldParameters : ICloneable
    {
        private readonly int _sizeAlongX;
        private readonly int _sizeAlongY;
        private readonly int _sizeAlongZ;

        private readonly int _minCoordinateAlongX;
        private readonly int _minCoordinateAlongY;
        private readonly int _minCoordinateAlongZ;

        /// <summary>
        /// Gets size in cells of the game field along the x axis
        /// </summary>
        public int SizeAlongX
        {
            get
            {
                return _sizeAlongX;
            }
        }

        /// <summary>
        /// Gets size in cells of the game field along the y axis
        /// </summary>
        public int SizeAlongY
        {
            get
            {
                return _sizeAlongY;
            }
        }

        /// <summary>
        /// Gets size in cells of the game field along the z axis
        /// </summary>
        public int SizeAlongZ
        {
            get
            {
                return _sizeAlongZ;
            }
        }

        /// <summary>
        /// Gets minimum possible x coordinate of a cell in the game field
        /// </summary>
        public int MinCoordinateAlongX
        {
            get
            {
                return _minCoordinateAlongX;
            }
        }

        /// <summary>
        /// Gets minimum possible y coordinate of a cell in the game field
        /// </summary>
        public int MinCoordinateAlongY
        {
            get
            {
                return _minCoordinateAlongY;
            }
        }

        /// <summary>
        /// Gets minimum possible z coordinate of a cell in the game field
        /// </summary>
        public int MinCoordinateAlongZ
        {
            get
            {
                return _minCoordinateAlongZ;
            }
        }

        /// <summary>
        /// Initializes a new instance of game field parameters according to the given game field size. 
        /// Automatically initializes all minimal cell coordinates to 0.
        /// </summary>
        /// <param name="sizeAlongX">Game field size along the x axis</param>
        /// <param name="sizeAlongY">Game field size along the y axis</param>
        /// <param name="sizeAlongZ">Game field size along the z axis</param>
        public GameFieldParameters(int sizeAlongX, int sizeAlongY, int sizeAlongZ)
        {
            _sizeAlongX = sizeAlongX;
            _sizeAlongY = sizeAlongY;
            _sizeAlongZ = sizeAlongZ;

            _minCoordinateAlongX = 0;
            _minCoordinateAlongY = 0;
            _minCoordinateAlongZ = 0;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new GameFieldParameters(_sizeAlongX, _sizeAlongY, _sizeAlongZ);
        }

        #endregion
    }
}
