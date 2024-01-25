using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.GraphicsPipeline;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using NUnit.Framework;
using Rectangle=GraphicsFramework.BaseObjects.Rectangle;

namespace Tests
{
    [TestFixture]
    public class TextureVisualizationTest
    {
        private IGraphicsPipeline pipeline;

        private readonly Color defaultDrawingColor = Color.FromArgb(0, 0, 0);

        private readonly Bitmap picture = new Bitmap(100, 200);

        [TestFixtureSetUp]
        public void FillGraphicsPipeLine()
        {
            Graphics graphics = Graphics.FromImage(picture);

            List<IGraphicsPipelineStep> pipelineSteps =
                new List<IGraphicsPipelineStep>
                    {
                        new ModellingViewStep(),
                        new VertexColouringStep(),
                        new ProjectionStep(),
                        new ClippingStep(),
                        new PerspectiveDivisionStep(),
                        new ViewPortTransformStep {TransformCoordinatesToTheBitmapStandart = true},
                        new TextureVisualizingStep
                            {
                                DefaultDrawingColor = defaultDrawingColor
                            },
                        new GraphicsFillingStep(graphics)
                    };

            pipeline = new GraphicsPipeline { PipelineSteps = pipelineSteps };
        }

        [Test]
        public void TestCompleteFilling()
        {
            double distanceToTheNearPlane = 30;

            Rectangle testRectangle = new Rectangle((double)picture.Height / 2, (double)picture.Width / 2);

            IWorld world = new World
                               {
                                   Camera = new Camera(new CameraParameters(distanceToTheNearPlane, 100, picture.Width, picture.Height))
                               };
            world.AddGraphicalObject(testRectangle);

            //as camera looks oppositely to the OZ
            testRectangle.Move(new Vector3D(0, 0, - distanceToTheNearPlane));

            IVisibleWorld visibleWorld = new VisibleWorld(world);

            pipeline.Apply(visibleWorld, world);

            bool pictureIsFilledWithDefaultColor = CheckPictureColor(picture, defaultDrawingColor);

            picture.Save("CompleteFilling.bmp");

            Assert.AreEqual(true, pictureIsFilledWithDefaultColor);
        }

        [Test]
        public void TestPerpendicularPlaneAbsence()
        {
            double distanceToTheNearPlane = 30;

            Rectangle testRectangle = new Rectangle((double)picture.Height / 2, (double)picture.Width / 2);

            IWorld world = new World
            {
                Camera = new Camera(new CameraParameters(distanceToTheNearPlane, 100, picture.Width, picture.Height))
            };
            world.AddGraphicalObject(testRectangle);

            //as camera looks oppositely to the OZ
            testRectangle.Move(new Vector3D(0, 0, -distanceToTheNearPlane));

            //So the rectangle is perpendicular to the camera near plane
            testRectangle.RotateRoundItsCenterRoundAxis(new Angle(Math.PI / 2),Axis.X );

            IVisibleWorld visibleWorld = new VisibleWorld(world);

            pipeline.Apply(visibleWorld, world);

            bool pictureIsEmpty = CheckPictureColor(picture, Color.FromArgb(255, 255, 255));

            picture.Save("PerpendicularPlaneAbsence.bmp");

            Assert.AreEqual(true, pictureIsEmpty);
        }

        private bool CheckPictureColor(Bitmap image, Color color)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color currentColor = image.GetPixel(i, j);
                    if (currentColor != color)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
