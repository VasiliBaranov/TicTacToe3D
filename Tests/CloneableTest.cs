using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CloneableTest
    {
        [Test]
        public void TestVectorClone()
        {
            const double initialValue = 1;

            Vector source = new Vector(3);
            source[0] = initialValue;

            Vector clone = source.Clone() as Vector;
            Assert.AreEqual(initialValue, clone[0]);

            clone[0] = 2;

            Assert.AreEqual(initialValue, source[0]);
        }

        [Test]
        public void TestVector3DClone()
        {
            const double initialValue = 1;

            Vector3D source = new Vector3D();
            source[0] = initialValue;

            Vector3D clone = source.Clone() as Vector3D;
            Assert.AreEqual(initialValue, clone[0]);

            clone[0] = 2;

            Assert.AreEqual(initialValue, source[0]);
        }

        [Test]
        public void TestMatrixClone()
        {
            const double initialValue = 1;

            Matrix source = new Matrix(3, 4);
            source[0, 0] = initialValue;

            Matrix clone = source.Clone() as Matrix;
            Assert.AreEqual(initialValue, clone[0, 0]);

            clone[0, 0] = 2;

            Assert.AreEqual(initialValue, source[0, 0]);
        }

        [Test]
        public void TestGraphicalObjectClone()
        {
            const double initialValue = 2;

            GraphicalObject source = new GraphicalObject();
            source.InitialModellingMatrix[0, 0] = initialValue;

            source.MeshVertices = new List<AffinePoint> {new AffinePoint(1, 1, 1), new AffinePoint(2, 2, 2)};
            source.Planes = new List<Plane>
                                {
                                    new Plane
                                        {
                                            Normal = new AffinePoint(1, 0, 0),
                                            ParentObject = source,
                                            Texture = new Texture(),
                                            PlaneVertices =
                                                new List<PlaneVertex>
                                                    {
                                                        new PlaneVertex
                                                            {
                                                                Color = Color.FromArgb(12, 34, 200),
                                                                TextureCoordinates = new Vector2D(),
                                                                VertexIndex = 0
                                                            }
                                                    }
                                        }
                                };

            GraphicalObject clone = source.Clone() as GraphicalObject;

            Assert.AreEqual(initialValue, clone.InitialModellingMatrix[0, 0]);
            Assert.AreEqual(source.MeshVertices.Count, clone.MeshVertices.Count);
            Assert.AreEqual(source.MeshVertices[0][0], clone.MeshVertices[0][0]);
            Assert.AreEqual(source.Planes.Count, clone.Planes.Count);
            Assert.AreEqual(source.Planes[0].Normal[0], clone.Planes[0].Normal[0]);
            Assert.AreEqual(source.Planes[0].PlaneVertices[0].Color, clone.Planes[0].PlaneVertices[0].Color);
            Assert.AreEqual(source.Planes[0].PlaneVertices[0].VertexIndex, clone.Planes[0].PlaneVertices[0].VertexIndex);
        }
    }
}
