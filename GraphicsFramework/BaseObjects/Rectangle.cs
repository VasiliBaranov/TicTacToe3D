using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;

namespace GraphicsFramework.BaseObjects
{
    /// <summary>
    /// Represents a rectangle
    /// </summary>
    public class Rectangle : GraphicalObject
    {
        #region Fields

        private readonly double halfHeight;
        private readonly double halfWidth;

        private readonly Bitmap image;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a rectangle in the XOY plane. Width is calculated along the OX axis.
        /// </summary>
        /// <remarks>
        /// TODO: Pass width, height; check unit tests after change.
        /// </remarks>
        public Rectangle(double halfHeight, double halfWidth)
        {
            this.halfHeight = halfHeight;
            this.halfWidth = halfWidth;

            AssignDefaultMesh();
        }

        public Rectangle(double halfHeight, double halfWidth, string texturePath)
        {
            this.halfHeight = halfHeight;
            this.halfWidth = halfWidth;
            image = new Bitmap(texturePath);

            AssignDefaultMesh();
        }

        public Rectangle(double halfHeight, double halfWidth, Bitmap texture)
        {
            this.halfHeight = halfHeight;
            this.halfWidth = halfWidth;

            image = texture;

            AssignDefaultMesh();
        }

        #endregion

        #region Private Methods

        /// <summary> 
        /// Initializes and assigns the default Mesh
        /// </summary> 
        private void AssignDefaultMesh()
        {
            MeshVertices = new List<AffinePoint>
                               {
                                   new AffinePoint(halfWidth, halfHeight, 0),
                                   new AffinePoint(halfWidth, -halfHeight, 0),
                                   new AffinePoint(- halfWidth, -halfHeight, 0),
                                   new AffinePoint(- halfWidth, halfHeight, 0),
                               };


            Plane plane = new Plane
                              {
                                  Normal = new AffinePoint(0, 0, 1),
                                  ParentObject = this,
                                  PlaneVertices = new List<PlaneVertex>
                                                      {
                                                          new PlaneVertex {VertexIndex = 0, Color = Color.Black},
                                                          new PlaneVertex {VertexIndex = 1, Color = Color.Black},
                                                          new PlaneVertex {VertexIndex = 2, Color = Color.Black},
                                                          new PlaneVertex {VertexIndex = 3, Color = Color.Black},
                                                      }
                              };

            if (image != null)
            {
                AssignTextureDetails(plane);
            }

            Planes = new List<Plane> { plane };
        }

        private void AssignTextureDetails(Plane plane)
        {
            plane.Texture = new Texture { Bitmap = image };
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            //Assign texture coordinates. Remember that texture coordinates are expected in the bitmap image format
            //(i.e. OY goes downwards, the origin of the coordinates is in the top left corner).

            foreach (PlaneVertex planeVertex in plane.PlaneVertices)
            {
                AffinePoint point = plane.ParentObject.MeshVertices[planeVertex.VertexIndex];

                double imageXCoordinate = (Math.Sign(point.X) + 1) / 2 * imageWidth;
                double imageYCoordinate = -(Math.Sign(point.Y) - 1) / 2 * imageHeight;

                planeVertex.TextureCoordinates = new Vector2D(imageXCoordinate, imageYCoordinate);
            }
        }

        #endregion
    }
}