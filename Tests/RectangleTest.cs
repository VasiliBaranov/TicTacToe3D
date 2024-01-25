using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using NUnit.Framework;
using System.Linq;
using Rectangle=GraphicsFramework.BaseObjects.Rectangle;

namespace Tests
{
    [TestFixture]
    public class RectangleTest
    {
        private Rectangle rectangle;
        private const int halfHeight = 100;
        private const int halfWidth = 50;

        [TestFixtureSetUp]
        public void CreateRectangle()
        {
            Bitmap texture = new Bitmap(2 * halfWidth, 2 * halfHeight);
            rectangle = new Rectangle(halfHeight, halfWidth, texture);
        }

        [Test]
        public void TestRectangleVerticesPositions()
        {
            List<AffinePoint> actualVertices = rectangle.Planes[0].MeshVertices;

            List<AffinePoint> expectedVertices = new List<AffinePoint>
                                                     {
                                                         new AffinePoint(halfWidth, halfHeight, 0),
                                                         new AffinePoint(halfWidth, -halfHeight, 0),
                                                         new AffinePoint(- halfWidth, -halfHeight, 0),
                                                         new AffinePoint(- halfWidth, halfHeight, 0),
                                                     };

            for (int i = 0; i < expectedVertices.Count; i++)
            {
                Assert.AreEqual(true, expectedVertices[i].AlmostEqual(actualVertices[i]));
            }
        }

        [Test]
        public void TestRectangleTextureCoordinates()
        {
            List<Vector2D> actualTextureCoordinates =
                rectangle.Planes[0].PlaneVertices.Select(vertex => vertex.TextureCoordinates).ToList();

            //texture coordinates should follow the bitmap coordinates convention
            List<Vector2D> expectedTextureCoordinates = new List<Vector2D>
                                                     {
                                                         new Vector2D(2 * halfWidth, 0), //top right
                                                         new Vector2D(2 * halfWidth, 2 * halfHeight), //bottom right
                                                         new Vector2D(0, 2 * halfHeight), //bottom left
                                                         new Vector2D(0, 0), //top left
                                                     };

            for (int i = 0; i < expectedTextureCoordinates.Count; i++)
            {
                Assert.AreEqual(true, expectedTextureCoordinates[i].AlmostEqual(actualTextureCoordinates[i]));
            }
        }
    }
}
