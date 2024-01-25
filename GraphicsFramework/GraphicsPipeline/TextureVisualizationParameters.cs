using System;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Represents a class for storing and manipulating texture visualization parameters.
    /// These parameters change lineary while moving in the viewport along the displayed plane edge.
    /// </summary>
    public class TextureVisualizationParameters : ICloneable
    {
        #region Fields

        private double zIndex;

        private double horizontalTextureCoordinateNominator;

        private double horizontalTextureCoordinateDenominator;

        private double verticalTextureCoordinateNominator;

        private double verticalTextureCoordinateDenominator;

        private double preciseX;

        private double preciseY;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the z-index.
        /// </summary>
        /// <value>The z-index.</value>
        public double ZIndex
        {
            get { return zIndex; }
            set { zIndex = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal texture coordinate nominator.
        /// </summary>
        /// <value>The horizontal texture coordinate nominator.</value>
        public double HorizontalTextureCoordinateNominator
        {
            get { return horizontalTextureCoordinateNominator; }
            set { horizontalTextureCoordinateNominator = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal texture coordinate denominator.
        /// </summary>
        /// <value>The horizontal texture coordinate denominator.</value>
        public double HorizontalTextureCoordinateDenominator
        {
            get { return horizontalTextureCoordinateDenominator; }
            set { horizontalTextureCoordinateDenominator = value; }
        }

        /// <summary>
        /// Gets or sets the vertical texture coordinate nominator.
        /// </summary>
        /// <value>The vertical texture coordinate nominator.</value>
        public double VerticalTextureCoordinateNominator
        {
            get { return verticalTextureCoordinateNominator; }
            set { verticalTextureCoordinateNominator = value; }
        }

        /// <summary>
        /// Gets or sets the vertical texture coordinate denominator.
        /// </summary>
        /// <value>The vertical texture coordinate denominator.</value>
        public double VerticalTextureCoordinateDenominator
        {
            get { return verticalTextureCoordinateDenominator; }
            set { verticalTextureCoordinateDenominator = value; }
        }

        /// <summary>
        /// Gets or sets the precise X.
        /// </summary>
        /// <value>The precise X.</value>
        public double PreciseX
        {
            get { return preciseX; }
            set { preciseX = value; }
        }

        /// <summary>
        /// Gets or sets the precise Y.
        /// </summary>
        /// <value>The precise Y.</value>
        public double PreciseY
        {
            get { return preciseY; }
            set { preciseY = value; }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public int X
        {
            get { return (int)preciseX; }
            set { preciseX = value; }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public int Y
        {
            get { return (int)preciseY; }
            set { preciseY = value; }
        }

        /// <summary>
        /// Gets the precise texture coordinate X.
        /// </summary>
        /// <value>The precise texture coordinate X.</value>
        public double PreciseTextureCoordinateX
        {
            get
            {
                return horizontalTextureCoordinateNominator / horizontalTextureCoordinateDenominator;
            }
        }

        /// <summary>
        /// Gets the precise texture coordinate Y.
        /// </summary>
        /// <value>The precise texture coordinate Y.</value>
        public double PreciseTextureCoordinateY
        {
            get
            {
                return verticalTextureCoordinateNominator / verticalTextureCoordinateDenominator;
            }
        }

        /// <summary>
        /// Gets the texture coordinate X.
        /// </summary>
        /// <value>The texture coordinate X.</value>
        public int TextureCoordinateX
        {
            get
            {
                return (int) PreciseTextureCoordinateX;
            }
        }

        /// <summary>
        /// Gets the texture coordinate Y.
        /// </summary>
        /// <value>The texture coordinate Y.</value>
        public int TextureCoordinateY
        {
            get
            {
                return (int)PreciseTextureCoordinateY;
            }
        }

        /// <summary>
        /// Gets the texture coordinates.
        /// </summary>
        /// <value>The texture coordinates.</value>
        public Vector2D TextureCoordinates
        {
            get
            {
                return new Vector2D(PreciseTextureCoordinateX, PreciseTextureCoordinateY);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the specified increment.
        /// </summary>
        /// <param name="increment">The increment.</param>
        public void Add(TextureVisualizationParameters increment)
        {
            zIndex += increment.zIndex;

            horizontalTextureCoordinateNominator += increment.horizontalTextureCoordinateNominator;
            horizontalTextureCoordinateDenominator += increment.horizontalTextureCoordinateDenominator;

            verticalTextureCoordinateNominator += increment.verticalTextureCoordinateNominator;
            verticalTextureCoordinateDenominator += increment.verticalTextureCoordinateDenominator;

            preciseX += increment.preciseX;
            preciseY += increment.preciseY;
        }

        /// <summary>
        /// Substracts the specified decrement.
        /// </summary>
        /// <param name="decrement">The decrement.</param>
        public void Substract(TextureVisualizationParameters decrement)
        {
            zIndex -= decrement.zIndex;

            horizontalTextureCoordinateNominator -= decrement.horizontalTextureCoordinateNominator;
            horizontalTextureCoordinateDenominator -= decrement.horizontalTextureCoordinateDenominator;

            verticalTextureCoordinateNominator -= decrement.verticalTextureCoordinateNominator;
            verticalTextureCoordinateDenominator -= decrement.verticalTextureCoordinateDenominator;

            preciseX -= decrement.preciseX;
            preciseY -= decrement.preciseY;
        }

        /// <summary>
        /// Multiplies by the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        public void Multiply(double multiplier)
        {
            zIndex *= multiplier;

            horizontalTextureCoordinateNominator *= multiplier;
            horizontalTextureCoordinateDenominator *= multiplier;

            verticalTextureCoordinateNominator *= multiplier;
            verticalTextureCoordinateDenominator *= multiplier;

            preciseX *= multiplier;
            preciseY *= multiplier;
        }

        /// <summary>
        /// Divides by the specified denominator.
        /// </summary>
        /// <param name="denominator">The denominator.</param>
        public void Divide(double denominator)
        {
            Multiply(1 / denominator);
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            TextureVisualizationParameters clone =
                new TextureVisualizationParameters
                    {
                        zIndex = zIndex,

                        horizontalTextureCoordinateNominator = horizontalTextureCoordinateNominator,
                        horizontalTextureCoordinateDenominator = horizontalTextureCoordinateDenominator,

                        verticalTextureCoordinateNominator = verticalTextureCoordinateNominator,
                        verticalTextureCoordinateDenominator = verticalTextureCoordinateDenominator,

                        preciseX = preciseX,
                        preciseY = preciseY
                    };

            return clone;
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static TextureVisualizationParameters operator +(TextureVisualizationParameters a, TextureVisualizationParameters b)
        {
            TextureVisualizationParameters result = (TextureVisualizationParameters) a.Clone();

            result.Add(b);

            return result;
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static TextureVisualizationParameters operator -(TextureVisualizationParameters a, TextureVisualizationParameters b)
        {
            TextureVisualizationParameters result = (TextureVisualizationParameters)a.Clone();

            result.Substract(b);

            return result;
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>The result of the operator.</returns>
        public static TextureVisualizationParameters operator *(TextureVisualizationParameters a, double multiplier)
        {
            TextureVisualizationParameters result = (TextureVisualizationParameters)a.Clone();

            result.Multiply(multiplier);

            return result;
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="denominator">The denominator.</param>
        /// <returns>The result of the operator.</returns>
        public static TextureVisualizationParameters operator /(TextureVisualizationParameters a, double denominator)
        {
            TextureVisualizationParameters result = (TextureVisualizationParameters) a.Clone();

            result.Divide(denominator);

            return result;
        }

        #endregion
    }
}
