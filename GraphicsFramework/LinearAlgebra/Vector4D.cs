namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents a four-dimensional vector.
    /// </summary>
    public class Vector4D : Vector
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4D"/> class.
        /// </summary>
        public Vector4D() : base(4)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            Vector4D clone = new Vector4D();
            CopyPropertiesFromThis(clone);
            return clone;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator *(Matrix matrix, Vector4D vector)
        {
            return Multiply<Vector4D>(matrix, vector);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator *(double c, Vector4D x)
        {
            return Multiply<Vector4D>(c, x);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator +(Vector4D x, Vector4D y)
        {
            return Add<Vector4D>(x, y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator -(Vector4D x, Vector4D y)
        {
            return Substract<Vector4D>(x, y);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator +(Vector4D x)
        {
            return (1) * x;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector4D operator -(Vector4D x)
        {
            return (-1) * x;
        }

        #endregion
    }
}
