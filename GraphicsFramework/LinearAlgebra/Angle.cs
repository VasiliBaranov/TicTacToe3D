namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents the angle in radians.
    /// </summary>
    public struct Angle
    {
        private double value;

        /// <summary>
        /// Gets or sets the value of the angle in radians.
        /// </summary>
        /// <value>The value.</value>
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> struct.
        /// </summary>
        /// <param name="value">The value in radians.</param>
        public Angle(double value)
        {
            this.value = value;
        }


        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns>The result of the operator.</returns>
        static public Angle operator *(double c, Angle alpha)
        {
            Angle result = new Angle(c * alpha.value);

            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <returns>The result of the operator.</returns>
        static public Angle operator +(Angle alpha, Angle beta)
        {

            Angle result = new Angle(alpha.value + beta.value);

            return result;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <returns>The result of the operator.</returns>
        static public Angle operator -(Angle alpha, Angle beta)
        {
            Angle result = new Angle(alpha.value - beta.value);

            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <returns>The result of the operator.</returns>
        static public Angle operator +(Angle alpha)
        {
            return (1) * alpha;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <returns>The result of the operator.</returns>
        static public Angle operator -(Angle alpha)
        {
            return (-1) * alpha;
        }
    }
}