using System;

namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Provides utility and extension methods for linear algebra classes.
    /// </summary>
    public static class LinearAlgebraExtensions
    {
        #region Fields

        private const double epsilon = 1e-3;

        #endregion

        /// <summary>
        /// Checks whether the matrices are almost equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this Matrix x, Matrix y)
        {
            for (int i = 0; i < x.NumberOfRows; i++)
            {
                for (int j = 0; j < x.NumberOfColumns; j++)
                {
                    if (!AlmostEqual(x[i, j], y[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the vectors are almost equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this Vector x, Vector y)
        {
            for (int i = 0; i < x.Dimensionality; i++)
            {
                if (!AlmostEqual(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the affine points are almost equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this AffinePoint x, AffinePoint y)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!AlmostEqual(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks whether the angles are almost equal.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="beta">The beta.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this Angle alpha, Angle beta)
        {
            bool anglesAreEqual = AlmostEqual(alpha.Value, beta.Value);
            return anglesAreEqual;
        }

        /// <summary>
        /// Checks whether the directions are almost equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this Direction x, Direction y)
        {
            return AlmostEqual(x.AngleX, y.AngleX) &&
                   AlmostEqual(x.AngleY, y.AngleY) &&
                   AlmostEqual(x.AngleZ, y.AngleZ);
        }

        /// <summary>
        /// Checks whether the numbers are almost equal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool AlmostEqual(this double x, double y)
        {
            bool valuesAreEqual = Math.Abs(x - y) < epsilon;
            return valuesAreEqual;
        }

        public static double Interpolate(double start, double end, double fraction)
        {
            return start + (end - start) * fraction;
        }
    }
}
