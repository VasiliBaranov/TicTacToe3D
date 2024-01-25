using System.Collections.Generic;
using GraphicsFramework.BaseObjects;
using GraphicsFramework.GraphicsPipeline;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ObjectClippingTest
    {
        [Test]
        public void TestCompleteClipping()
        {
            double width = 0.5;
            double height = 2;
            double cvvSize = 1;

            Rectangle testRectangle = new Rectangle(height, width);

            IWorld world = new World{Camera = new Camera(new CameraParameters(30, 100, 20, 20))};
            world.AddGraphicalObject(testRectangle);

            IGraphicsPipeline pipeline = new GraphicsPipeline
                                             {
                                                 PipelineSteps = new List<IGraphicsPipelineStep>
                                                                     {
                                                                         new ClippingStep()
                                                                     }
                                             };

            IVisibleWorld visibleWorld = new VisibleWorld(world);

            pipeline.Apply(visibleWorld, world);

            List<AffinePoint> expectedMesh = new List<AffinePoint>
                                                   {
                                                       new AffinePoint(width, cvvSize, 0),
                                                       new AffinePoint(-width, cvvSize, 0),
                                                       new AffinePoint(-width, -cvvSize, 0),
                                                       new AffinePoint(width, -cvvSize, 0)
                                                   };

            bool actualMeshValid = CheckPoints(visibleWorld.GraphicalObjects[0], expectedMesh);

            Assert.AreEqual(true, actualMeshValid);
        }

        [Test]
        public void TestPartialClipping()
        {
            double width = 0.5;
            double height = 2;
            double cvvSize = 1;

            List<AffinePoint> initialMesh = new List<AffinePoint>
                                                   {
                                                       new AffinePoint(width, height, 0),
                                                       new AffinePoint(-width, height, 0),
                                                       new AffinePoint(-width, 0, 0),
                                                       new AffinePoint(width, 0, 0)
                                                   };

            PlaneObject testRectangle = new PlaneObject(initialMesh);

            IWorld world = new World { Camera = new Camera(new CameraParameters(30, 100, 20, 20)) };
            world.AddGraphicalObject(testRectangle);

            IGraphicsPipeline pipeline = new GraphicsPipeline
                                             {
                                                 PipelineSteps = new List<IGraphicsPipelineStep>
                                                                     {
                                                                         new ClippingStep()
                                                                     }
                                             };

            IVisibleWorld visibleWorld = new VisibleWorld(world);

            pipeline.Apply(visibleWorld, world);

            List<AffinePoint> expectedMesh = new List<AffinePoint>
                                                   {
                                                       new AffinePoint(width, cvvSize, 0),
                                                       new AffinePoint(-width, cvvSize, 0),
                                                       new AffinePoint(-width, 0, 0),
                                                       new AffinePoint(width, 0, 0)
                                                   };

            bool actualMeshValid = CheckPoints(visibleWorld.GraphicalObjects[0], expectedMesh);

            Assert.AreEqual(true, actualMeshValid);
        }

        private static bool CheckPoints(IGraphicalObject graphicalObject, ICollection<AffinePoint> expectedMesh)
        {
            foreach (AffinePoint expectedPoint in expectedMesh)
            {
                bool expectedPointIsPersentInObject =
                    graphicalObject.MeshVertices.
                    Exists(existingPoint => existingPoint.AlmostEqual(expectedPoint));

                if(!expectedPointIsPersentInObject)
                {
                    return false;
                }
            }

            return graphicalObject.MeshVertices.Count == expectedMesh.Count;
        }
    }
}
