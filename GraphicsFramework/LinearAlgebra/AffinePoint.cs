using System;

namespace GraphicsFramework.LinearAlgebra
{
    /// <summary> 
    /// A point in the 3-dimensional affine world, the last element for points is usually 1, for vectors - 0.
    /// </summary>
    public class AffinePoint : ICloneable
    {
        #region Fields

        /// <summary> 
        /// An array of all the coordinates.
        /// </summary>
        private readonly Vector4D coordinates = new Vector4D();

        private const double eps = 1e-3;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the specified coordinate.
        /// </summary>
        /// <value></value>
        public double this[int coordinate]   // Indexer declaration
        {
            get
            {
                if (coordinate > 3)
                {
                    throw new ArgumentOutOfRangeException("coordinate", "should be less than 4");
                }
                return coordinates[coordinate];
            }
            set
            {
                if (coordinate > 3)
                {
                    throw new ArgumentOutOfRangeException("coordinate", "should be less than 4");
                }
                coordinates[coordinate] = value;
            }
        }

        /// <summary>
        /// Gets the dimensionality.
        /// </summary>
        /// <value>The dimensionality.</value>
        public int Dimensionality
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public double X
        {
            get
            {
                return coordinates[0];
            }
            set
            {
                coordinates[0] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public double Y
        {
            get
            {
                return coordinates[1];
            }
            set
            {
                coordinates[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        /// <value>The Z.</value>
        public double Z
        {
            get
            {
                return coordinates[2];
            }
            set
            {
                coordinates[2] = value;
            }
        }

        /// <summary>
        /// Gets or sets the W.
        /// </summary>
        /// <value>The W.</value>
        public double W
        {
            get
            {
                return coordinates[3];
            }
            set
            {
                coordinates[3] = value;
            }
        }

        public bool IsPoint
        {
            get
            {
                return Math.Abs(W - 1) < eps;
            }
        }

        public bool IsVector
        {
            get
            {
                return Math.Abs(W) < eps;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AffinePoint"/> class, 
        /// representing the point (not a vector) in the affine space.
        /// </summary>
        public AffinePoint()
        {
            coordinates[3] = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AffinePoint"/> class, 
        /// representing the point (not a vector) in the affine space.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public AffinePoint(double x, double y, double z)
        {
            coordinates[0] = x;
            coordinates[1] = y;
            coordinates[2] = z;
            coordinates[3] = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AffinePoint"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        protected AffinePoint(Vector4D coordinates)
        {
            this.coordinates = coordinates;
        }

        public AffinePoint(Vector3D coordinates, bool isVector)
        {
            for (int i = 0; i < 3; i++)
            {
                this.coordinates[i] = coordinates[i];
            }
            this.coordinates[3] = (isVector ? 0 : 1);
        }

        #endregion

        #region Public Methods

        public static AffinePoint operator +(AffinePoint x, AffinePoint y)
        {
            if (x.IsPoint && y.IsPoint)
            {
                throw new ArgumentException("Both parameters are points. At least one of them should be a vector.");
            }

            Vector4D result = x.coordinates + y.coordinates;

            return new AffinePoint(result);
        }

        public static AffinePoint operator -(AffinePoint x, AffinePoint y)
        {
            if (x.IsPoint && y.IsPoint)
            {
                throw new ArgumentException("Both parameters are points. At least one of them should be a vector.");
            }

            Vector4D result = x.coordinates - y.coordinates;

            return new AffinePoint(result);
        }

        /// <summary> 
        /// Multiplies a matrix(4 by 4) by the affine point.
        /// </summary>
        /// <param name="matrix"> The matrix to be multiplied</param>
        /// <param name="affinePoint">The displacementVector to be multiplied</param>
        /// <returns> The result of the multiplication</returns>
        public static AffinePoint operator *(Matrix matrix, AffinePoint affinePoint)
        {
            return affinePoint.MultiplyBy(matrix, true);
        }

        /// <summary>
        /// Multiplies the instance by the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="performPerspectiveDivision">
        /// Specifies whether to divide the returned vector coordinates by the last one.
        /// </param>
        /// <returns>A new object, the esult of the operation.</returns>
        public AffinePoint MultiplyBy(Matrix matrix, bool performPerspectiveDivision)
        {
            Vector vectorResult = matrix * coordinates;

            double multiplier;
            if (performPerspectiveDivision)
            {
                multiplier = Math.Abs(vectorResult[3]) > eps
                                 ? 1 / vectorResult[3]
                                 : 1;
            }
            else
            {
                multiplier = 1;
            }

            AffinePoint result = new AffinePoint();
            //normalizing the AffinePoint
            int i;
            for (i = 0; i < 4; i++)
            {
                result.coordinates[i] = vectorResult[i] * multiplier;
            }

            return result;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            AffinePoint clone = new AffinePoint(coordinates.Clone() as Vector4D);
            return clone;
        }

        #endregion
    }
}