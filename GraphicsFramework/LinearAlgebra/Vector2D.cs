namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents a two-dimensional vector.
    /// </summary>
    public class Vector2D : Vector
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        public Vector2D()
            : base(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2D(double x, double y)
            : base(x, y)
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
            Vector2D clone = new Vector2D();
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
        public static Vector2D operator *(Matrix matrix, Vector2D vector)
        {
            return Multiply<Vector2D>(matrix, vector);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator *(double c, Vector2D x)
        {
            return Multiply<Vector2D>(c, x);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator +(Vector2D x, Vector2D y)
        {
            return Add<Vector2D>(x, y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator -(Vector2D x, Vector2D y)
        {
            return Substract<Vector2D>(x, y);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator +(Vector2D x)
        {
            return (1) * x;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector2D operator -(Vector2D x)
        {
            return (-1) * x;
        }

        #endregion
    }
}
