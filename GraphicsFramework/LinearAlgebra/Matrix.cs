using System;
using System.Text;

namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents a matrix.
    /// </summary>
    public class Matrix : ICloneable
    {
        #region Fields

        private readonly int numberOfRows;

        private readonly int numberOfColumns;

        private readonly double[,] elements;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>The number of rows.</value>
        public int NumberOfRows
        {
            get
            {
                return numberOfRows;
            }
        }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>The number of columns.</value>
        public int NumberOfColumns
        {
            get
            {
                return numberOfColumns;
            }
        }

        /// <summary>
        /// Gets or sets the value within the specified row and column.
        /// </summary>
        /// <value></value>
        public double this[int row, int column]   // Indexer declaration
        {
            get
            {
                return elements[row, column];
            }
            set
            {
                elements[row, column] = value;
            }
        }

        /// <summary>
        /// Gets the trace of the matrix.
        /// </summary>
        /// <value>The trace.</value>
        public double Trace
        {
            get
            {
                if (numberOfRows != numberOfColumns)
                {
                    throw new InvalidOperationException("Trace is defined just for square matrices.");
                }

                double trace = 0;
                for (int i = 0; i < numberOfRows; i++)
                {
                    trace += elements[i, i];
                }
                return trace;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="numberOfRows">The number of rows.</param>
        /// <param name="numberOfColumns">The number of columns.</param>
        public Matrix(int numberOfRows, int numberOfColumns)
        {
            this.numberOfRows = numberOfRows;
            this.numberOfColumns = numberOfColumns;
            elements = new double[numberOfRows, numberOfColumns];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        public Matrix Transpose()
        {
            Matrix result = new Matrix(numberOfRows, numberOfColumns);

            int i;
            int j;

            for (i = 0; i < numberOfRows; i++)
            {
                for (j = 0; j < numberOfColumns; j++)
                {
                    result.elements[i, j] = elements[j, i];
                }
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    stringBuilder.AppendFormat("{0} ", elements[i, j].ToString("f2"));
                }
                stringBuilder.Append("\r\n");
            }
            return stringBuilder.ToString();
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
            Matrix clone = new Matrix(numberOfRows, numberOfColumns);

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    clone[i, j] = elements[i, j];
                }
            }
            return clone;
        }

        public Matrix GetSubMatrix(int rowsCount, int columnsCount)
        {
            Matrix matrix = new Matrix(rowsCount, columnsCount);
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    matrix.elements[i, j] = elements[i, j];
                }
            }
            return matrix;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>The result of the operator.</returns>
        static public Matrix operator *(Matrix A, Matrix B)
        {
            return Multiply(A, B);
        }

        /// <summary>
        /// Multiplies the specified matrices
        /// </summary>
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>The new instance.</returns>
        static public Matrix Multiply(Matrix A, Matrix B)
        {
            if (A.numberOfColumns != B.numberOfRows)
            {
                throw new ArgumentOutOfRangeException("A", "matrix dimensions must agree");
            }

            Matrix result = new Matrix(A.numberOfRows, B.numberOfColumns);

            for (int i = 0; i < A.numberOfRows; i++)
            {
                for (int j = 0; j < B.numberOfColumns; j++)
                {
                    result.elements[i, j] = 0;

                    for (int k = 0; k < A.numberOfColumns; k++)
                    {
                        result.elements[i, j] += (A.elements[i, k] * B.elements[k, j]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        /// <param name="dimensions">The dimensions.</param>
        /// <returns></returns>
        static public Matrix GetIdentity(int dimensions)
        {
            Matrix identity = new Matrix(dimensions, dimensions);
            for (int i = 0; i < 4; i++)
            {
                identity.elements[i, i] = 1;
            }
            return identity;
        }

        #endregion

    }
}