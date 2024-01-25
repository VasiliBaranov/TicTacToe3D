using System;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WorldObjectsTest
    {


        #region Public Methods

        [Test]
        public void TestRotation()
        {
            Angle rotationAngle = new Angle(Math.PI/2);

            GraphicalObject graphicalObject = new GraphicalObject();

            graphicalObject.RotateRoundItsCenterRoundAxis(rotationAngle, Axis.X);

            Angle actualAngle = graphicalObject.RotationAngle;

            bool anglesAreEqual = rotationAngle.AlmostEqual(actualAngle);

            Assert.AreEqual(true, anglesAreEqual);

            Direction actualDirection = graphicalObject.RotationDirection;

            //direction along x
            Direction expectedDirection = new Direction(new Vector3D(1, 0, 0));

            Assert.AreEqual(true, actualDirection.AlmostEqual(expectedDirection));
        }

        [Test]
        public void TestRotationAndMove()
        {
            Angle rotationAngle = new Angle(Math.PI/2);
            Vector3D displacementVector = new Vector3D(0, 4, 2);

            GraphicalObject graphicalObject = new GraphicalObject();

            graphicalObject.RotateRoundItsCenterRoundAxis(rotationAngle, Axis.X);
            graphicalObject.Move(displacementVector);

            Angle actualAngle = graphicalObject.RotationAngle;
            Vector3D actualPosition = graphicalObject.Position;
            Direction actualDirection = graphicalObject.RotationDirection;

            //direction along x
            Direction expectedDirection = new Direction(new Vector3D(1, 0, 0));

            Assert.AreEqual(true, rotationAngle.AlmostEqual(actualAngle));
            Assert.AreEqual(true, actualDirection.AlmostEqual(expectedDirection));
            Assert.AreEqual(true, actualPosition.AlmostEqual(displacementVector));
        }

        [Test]
        public void TestMoveAndRotationRoundObjectCenter()
        {
            Angle rotationAngle = new Angle(Math.PI / 2);
            Vector3D displacementVector = new Vector3D(0, 2, 0);

            GraphicalObject graphicalObject = new GraphicalObject();

            graphicalObject.Move(displacementVector);
            graphicalObject.RotateRoundItsCenterRoundAxis(rotationAngle, Axis.X);

            Angle actualAngle = graphicalObject.RotationAngle;
            Direction actualDirection = graphicalObject.RotationDirection;
            Vector3D actualPosition = graphicalObject.Position;

            //direction along x
            Direction expectedDirection = new Direction(new Vector3D(1, 0, 0));

            Assert.AreEqual(true, rotationAngle.AlmostEqual(actualAngle));
            Assert.AreEqual(true, actualDirection.AlmostEqual(expectedDirection));
            Assert.AreEqual(true, actualPosition.AlmostEqual(displacementVector));
        }

        [Test]
        public void TestMoveAndRotationRoundCoordinatesCenter()
        {
            Angle rotationAngle = new Angle(Math.PI / 2);
            Vector3D displacementVector = new Vector3D(0, 2, 0);

            GraphicalObject graphicalObject = new GraphicalObject();

            graphicalObject.Move(displacementVector);
            graphicalObject.RotateRoundOriginOfCoordinatesRoundAxis(rotationAngle, Axis.X);

            Angle expectedAngle = new Angle(Math.PI / 2);
            Direction expectedDirection = new Direction(new Vector3D(1, 0, 0)); //direction along x
            Vector3D expectedPosition = new Vector3D(0, 0, 2); //NOT (0, 2, 0)

            Angle actualAngle = graphicalObject.RotationAngle;
            Direction actualDirection = graphicalObject.RotationDirection;
            Vector3D actualPosition = graphicalObject.Position;

            Assert.AreEqual(true, actualAngle.AlmostEqual(expectedAngle));
            Assert.AreEqual(true, actualDirection.AlmostEqual(expectedDirection));
            Assert.AreEqual(true, actualPosition.AlmostEqual(expectedPosition));
        }

        [Test]
        public void TestCameraView()
        {
            Angle rotationAngle = new Angle(Math.PI / 2);
            Vector3D displacementVector = new Vector3D(0, 2, 0);

            CameraParameters parameters = new CameraParameters(0, 0, 0, 0);
            Camera camera = new Camera(parameters);

            camera.Move(displacementVector);
            camera.RotateRoundOriginOfCoordinatesRoundAxis(rotationAngle, Axis.Y);

            Matrix result = camera.ModellingMatrix * camera.ViewMatrix;

            Assert.AreEqual(true, Matrix.GetIdentity(4).AlmostEqual(result));
        }

        /*[Test]
        public void TestCameraProjection()
        {
            double distanceToTheNearPlane = 10;
            double testPointSize = 5;
            CameraParameters parameters = new CameraParameters(distanceToTheNearPlane, 30, 100, 100);
            Camera camera = new Camera(parameters);

            //so that the near plane will be at the origin of coordinates
            //camera.Move(new Vector3D(0, 0, distanceToTheNearPlane));

            AffinePoint testPoint = new AffinePoint(testPointSize, 0, -distanceToTheNearPlane);

            AffinePoint projectedPoint = camera.ProjectionMatrix * camera.ViewMatrix * testPoint;

            Assert.AreEqual(true, AlmostEqual(projectedPoint[0], testPointSize));
        }

        [Test]
        public void TestCameraProjection2()
        {
            double distanceToTheNearPlane = 10;
            double multiplier = 2;
            double testPointSize = 5;
            CameraParameters parameters = new CameraParameters(distanceToTheNearPlane, 30, 100, 100);
            Camera camera = new Camera(parameters);

            //so that the near plane will be at the origin of coordinates
            //camera.Move(new Vector3D(0, 0, distanceToTheNearPlane));

            AffinePoint testPoint = new AffinePoint(testPointSize, 0, -multiplier * distanceToTheNearPlane);

            AffinePoint projectedPoint = camera.ProjectionMatrix * camera.ViewMatrix * testPoint;

            Assert.AreEqual(true, AlmostEqual(projectedPoint[0], testPointSize / multiplier));
        }*/

        [Test]
        public void TestCanonicalViewVolumeProjection()
        {
            const double testPointSize = 5;
            CameraParameters parameters = new CameraParameters(10, 30, 100, 100);
            Camera camera = new Camera(parameters);

            AffinePoint nearPoint = new AffinePoint(testPointSize, parameters.NearPlaneSizeAlongY / 2, -parameters.DistanceToTheNearPlane);
            AffinePoint farPoint = new AffinePoint(0, 0, -parameters.DistanceToTheFarPlane);

            AffinePoint projectedNearPoint = camera.ProjectionMatrix * camera.ViewMatrix * nearPoint;
            AffinePoint projectedFarPoint = camera.ProjectionMatrix * camera.ViewMatrix * farPoint;

            Assert.AreEqual(true, projectedNearPoint[1].AlmostEqual(1));
            Assert.AreEqual(true, projectedNearPoint[2].AlmostEqual(-1));

            Assert.AreEqual(true, projectedFarPoint[2].AlmostEqual(1));
        }

        [Test]
        public void TestCanonicalViewVolumeProjection2()
        {
            const double testPointSize = 5;
            CameraParameters parameters = new CameraParameters(10, 30, 100, 100);
            Camera camera = new Camera(parameters);

            AffinePoint nearPoint = new AffinePoint(testPointSize,
                                                    parameters.NearPlaneSizeAlongY / 2,
                                                    -parameters.DistanceToTheNearPlane);

            AffinePoint projectedNearPoint = camera.ViewMatrix * nearPoint;

            projectedNearPoint = projectedNearPoint.MultiplyBy(camera.ProjectionMatrix, false);

            for (int i = 0; i < 3; i++)
            {
                projectedNearPoint[i] = projectedNearPoint[i] / projectedNearPoint[3];
            }

            Assert.AreEqual(true, projectedNearPoint[1].AlmostEqual(1));
            Assert.AreEqual(true, projectedNearPoint[2].AlmostEqual(-1));

        }

        #endregion

        #region Private Methods

        #endregion
    }
}
