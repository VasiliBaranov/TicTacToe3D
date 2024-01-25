namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents a three-dimensional vector.
    /// </summary>
    public class Vector3D : Vector
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        public Vector3D() : base(3)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Vector3D(double x, double y, double z) :base(x,y,z)
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
            Vector3D clone = new Vector3D();
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
        public static Vector3D operator *(Matrix matrix, Vector3D vector)
        {
            return Multiply<Vector3D>(matrix, vector);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator *(double c, Vector3D x)
        {
            return Multiply<Vector3D>(c, x);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator +(Vector3D x, Vector3D y)
        {
            return Add<Vector3D>(x, y);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator -(Vector3D x, Vector3D y)
        {
            return Substract<Vector3D>(x, y);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator +(Vector3D x)
        {
            return (1) * x;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector3D operator -(Vector3D x)
        {
            return (-1) * x;
        }

        #endregion
    }
}
