using System;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a vertex on the plane.
    /// </summary>
    public class PlaneVertex : ICloneable
    {
        #region Constructors

        #endregion

        #region Fields

        private Vector2D textureCoordinates;

        private Color color;

        private int vertexIndex;

        private double perspectiveMultiplier = 1;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the index of the vertex in the parent graphical object mesh vertices.
        /// </summary>
        /// <value>The index of the vertex.</value>
        /// <remarks>
        /// We do not store the mesh vertex itself, as they may be shared between planes, 
        /// so we avoid recomputation of the same vectors during modelling-view step.
        /// </remarks>
        public int VertexIndex
        {
            get { return vertexIndex; }
            set { vertexIndex = value; }
        }

        /// <summary>
        /// Gets or sets the texture coordinates of the vertex.
        /// They should follow the .Net bitmap coordinates convention, 
        /// i.e. the origin of the coordinates should be in the top left corner of the texture, 
        /// and that the Y axis should go downwards.
        /// May be null, if the texture has not been specified for the plane.
        /// </summary>
        /// <value>The texture coordinates.</value>
        public Vector2D TextureCoordinates
        {
            get { return textureCoordinates; }
            set { textureCoordinates = value; }
        }

        /// <summary>
        /// Gets or sets the color of the vertex.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Gets or sets the perspective multiplier for the vertex 
        /// ( i.e. 1/the last coordinate of the corresponding mesh vertex after the perspective division step).
        /// </summary>
        /// <value>The perspective multiplier.</value>
        public double PerspectiveMultiplier
        {
            get { return perspectiveMultiplier; }
            set { perspectiveMultiplier = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            PlaneVertex clone = new PlaneVertex();

            clone.color = Color.FromArgb(color.R, color.G, color.B);
            if (textureCoordinates != null)
            {
                clone.textureCoordinates = textureCoordinates.Clone() as Vector2D;
            }
            clone.vertexIndex = vertexIndex;

            return clone;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}