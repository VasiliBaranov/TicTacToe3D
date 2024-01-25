using System;
using System.Linq;

namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents a vector.
    /// </summary>
    public class Vector : ICloneable
    {
        #region Fields

        private readonly int dimensionality;

        private readonly double[] elements;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the dimensionality.
        /// </summary>
        /// <value>The dimensionality.</value>
        public int Dimensionality
        {
            get 
            { 
                return dimensionality; 
            }
        }

        /// <summary>
        /// Gets or sets the value with the specified coordinate.
        /// </summary>
        /// <value></value>
        public double this[int coordinate]   // Indexer declaration
        {
            get
            {
                return elements[coordinate];
            }
            set
            {
                elements[coordinate] = value;
            }
        }

        /// <summary>
        /// Gets the length of the vector.
        /// </summary>
        /// <value>The length.</value>
        public double Length
        {
            get
            {
                //or use EnumerableExtension.Norm

                double length = 0;
                foreach (double element in elements)
                {
                    length += element*element;
                }
                length = Math.Sqrt(length);

                return length;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="dimensionality">The dimensionality.</param>
        public Vector(int dimensionality)
        {
            this.dimensionality = dimensionality;
            elements = new double[dimensionality];
        }

        public Vector(params double[] coordinates)
        {
            dimensionality = coordinates.Length;

            elements = new double[dimensionality];
            coordinates.CopyTo(elements, 0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual object Clone()
        {
            Vector clone = new Vector(dimensionality);
            CopyPropertiesFromThis(clone);
            return clone;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Copies the properties from this instance to the target vector.
        /// </summary>
        /// <param name="target">The target.</param>
        protected virtual void CopyPropertiesFromThis(Vector target)
        {
            for (int i = 0; i < dimensionality; i++)
            {
                target.elements[i] = elements[i];
            }
        }

        protected static void Multiply(Matrix matrix, Vector vector, Vector result)
        {
            if (matrix.NumberOfColumns != vector.dimensionality)
            {
                throw new ArgumentOutOfRangeException("matrix", "Matrix and vector dimensionalities must agree.");
            }

            for (int i = 0; i < matrix.NumberOfRows; i++)
            {
                result.elements[i] = 0;
                for (int j = 0; j < matrix.NumberOfColumns; j++)
                {
                    result.elements[i] += (matrix[i, j] * vector.elements[j]);
                }
            }
        }

        protected static T Multiply<T>(Matrix matrix, Vector vector) where T : Vector, new()
        {
            T result = new T();
            Multiply(matrix, vector, result);

            return result;
        }

        protected static void Multiply(double c, Vector vector, Vector result)
        {
            for (int i = 0; i < vector.dimensionality; i++)
            {
                result.elements[i] = c * vector.elements[i];
            }
        }

        protected static T Multiply<T>(double c, Vector vector) where T : Vector, new()
        {
            T result = new T();
            Multiply(c, vector, result);

            return result;
        }

        protected static void Add(Vector x, Vector y, Vector result)
        {
            if (x.dimensionality != y.dimensionality)
            {
                throw new ArgumentOutOfRangeException("x", "Vector dimensions must agree.");
            }

            int i;
            for (i = 0; i < x.dimensionality; i++)
            {
                result.elements[i] = x.elements[i] + y.elements[i];
            }
        }

        protected static T Add<T>(Vector x, Vector y) where T : Vector, new()
        {
            T result = new T();
            Add(x, y, result);

            return result;
        }

        protected static void Substract(Vector x, Vector y, Vector result)
        {
            if (x.dimensionality != y.dimensionality)
            {
                throw new ArgumentOutOfRangeException("x", "Vector dimensions must agree.");
            }

            int i;
            for (i = 0; i < x.dimensionality; i++)
            {
                result.elements[i] = x.elements[i] - y.elements[i];
            }
        }

        protected static T Substract<T>(Vector x, Vector y) where T : Vector, new()
        {
            T result = new T();
            Substract(x, y, result);

            return result;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vector">The vector.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator *(Matrix matrix, Vector vector)
        {
            Vector result = new Vector(matrix.NumberOfRows);
            Multiply(matrix, vector, result);
            return result;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator *(double c, Vector x)
        {
            Vector result = new Vector(x.dimensionality);
            Multiply(c, x, result);
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator +(Vector x, Vector y)
        {
            Vector result = new Vector(x.dimensionality);
            Add(x, y, result);
            return result;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator -(Vector x, Vector y)
        {
            Vector result = new Vector(x.dimensionality);
            Substract(x, y, result);
            return result;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator +(Vector x)
        {
            return (1) * x;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator -(Vector x)
        {
            return (-1) * x;
        }

        #endregion
    }
}