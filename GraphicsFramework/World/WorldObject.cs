using System;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a basic world object.
    /// </summary>
    /// <remarks>
    /// See Hill, Computer graphics programming, ch. 5
    /// </remarks>
    public abstract class WorldObject : IWorldObject
    {
        #region Fields

        private Matrix currentModellingMatrix = Matrix.GetIdentity(4);

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets the direction of the object rotation
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The direction.</value>
        public Direction RotationDirection
        {
            get { return GetRotationDirection(); }
        }

        /// <summary>
        /// Gets the rotation angle
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The rotation angle.</value>
        public Angle RotationAngle
        {
            get { return GetRotationAngle(); }
        }

        /// <summary>
        /// Gets the position of the object
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The position.</value>
        public Vector3D Position
        {
            get { return GetPosition(); }
        }

        /// <summary>
        /// Gets the modelling matrix, i.e. the matrix that represents rotations and moves of the object.
        /// </summary>
        /// <value>The modelling matrix.</value>
        public Matrix ModellingMatrix
        {
            get { return currentModellingMatrix; }
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
        public abstract object Clone();

        public void RotateRoundOriginOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis)
        {
            currentModellingMatrix = GetTransformationMatrixForRotation(rotationAngle, rotationAxis) * currentModellingMatrix;
        }

        /// <summary>
        /// Rotates the obejct round its center round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public void RotateRoundItsCenterRoundAxis(Angle rotationAngle, Axis rotationAxis)
        {
            currentModellingMatrix = currentModellingMatrix * GetTransformationMatrixForRotation(rotationAngle, rotationAxis);
        }

        /// <summary>
        /// Rotates the object round some point round some vector.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationDirection">The rotation direction.</param>
        /// <remarks> See Хилл, p.296, (5.33) </remarks>
        public void RotateRoundSomePointRoundSomeDirection(Angle rotationAngle, Vector3D centerOfRotation, Direction rotationDirection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Rotats the object round some direction.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationDirection">The rotation direction.</param>
        public void RotatRoundSomeDirection(Angle rotationAngle, Direction rotationDirection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the object by specified displacement vector.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        public void Move(Vector3D displacementVector)
        {
            currentModellingMatrix = GetTransformationMatrixForMove(displacementVector) * currentModellingMatrix;
        }

        /// <summary>
        /// Gets the direction of the given axis after object rotation, as if this axis were rotated with the object
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        public Direction GetDirectionOfAxisAfterRotation(Axis axis)
        {
            Direction directionAlongAxis = new Direction(axis);
            Vector3D unitVectorAlongAxis = directionAlongAxis.UnitVectorAlongDirection;

            Matrix rotationMatrixPart = currentModellingMatrix.GetSubMatrix(3, 3);
            Vector3D rotatedUnitVector = rotationMatrixPart * unitVectorAlongAxis;

            return new Direction(rotatedUnitVector);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Copies the properties from the current instance to the target object.
        /// </summary>
        /// <param name="target">The target.</param>
        protected virtual void CopyPropertiesFromThis(WorldObject target)
        {
            target.currentModellingMatrix = currentModellingMatrix.Clone() as Matrix;
        }

        /// <summary>
        /// Gets the transformation matrix for rotation.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        /// <returns></returns>
        protected Matrix GetTransformationMatrixForRotation(Angle rotationAngle, Axis rotationAxis)
        {
            if (rotationAxis == Axis.X)
            {
                return GetTransformationMatrixForRotationRoundX(rotationAngle);
            }
            if (rotationAxis == Axis.Y)
            {
                return GetTransformationMatrixForRotationRoundY(rotationAngle);
            }
            return GetTransformationMatrixForRotationRoundZ(rotationAngle);
        }

        /// <summary>
        /// Gets the transformation matrix for rotation round X.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <returns></returns>
        protected Matrix GetTransformationMatrixForRotationRoundX(Angle rotationAngle)
        {

            double c = Math.Cos(rotationAngle.Value);
            double s = Math.Sin(rotationAngle.Value);

            //update the transformation matrix to represent a turn round the X axis

            Matrix transformationMatrix = Matrix.GetIdentity(4);

            transformationMatrix[1, 1] = c; transformationMatrix[1, 2] = -s;
            transformationMatrix[2, 1] = s; transformationMatrix[2, 2] = c;

            return transformationMatrix;
        }

        /// <summary>
        /// Gets the transformation matrix for rotation round Y.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <returns></returns>
        protected Matrix GetTransformationMatrixForRotationRoundY(Angle rotationAngle)
        {

            double c = Math.Cos(rotationAngle.Value);
            double s = Math.Sin(rotationAngle.Value);

            //update the transformation matrix to represent a turn round the Y axis

            Matrix transformationMatrix = Matrix.GetIdentity(4);

            transformationMatrix[0, 0] = c; transformationMatrix[0, 2] = s;
            transformationMatrix[2, 0] = -s; transformationMatrix[2, 2] = c;

            return transformationMatrix;
        }

        /// <summary>
        /// Gets the transformation matrix for rotation round Z.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <returns></returns>
        protected Matrix GetTransformationMatrixForRotationRoundZ(Angle rotationAngle)
        {
            double c = Math.Cos(rotationAngle.Value);
            double s = Math.Sin(rotationAngle.Value);

            //update the transformation matrix to represent a turn round the Z axis

            Matrix transformationMatrix = Matrix.GetIdentity(4);

            transformationMatrix[0, 0] = c; transformationMatrix[0, 1] = -s;
            transformationMatrix[1, 0] = s; transformationMatrix[1, 1] = c;

            return transformationMatrix;
        }

        /// <summary>
        /// Gets the transformation matrix for move.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        protected Matrix GetTransformationMatrixForMove(Vector3D vector)
        {
            Matrix transformationMatrix = Matrix.GetIdentity(4);

            for (int i = 0; i < 3; i++)
            {
                transformationMatrix[i, 3] = vector[i];
            }

            return transformationMatrix;
        }

        /// <summary>
        /// Gets the position of the object.
        /// </summary>
        /// <returns></returns>
        private Vector3D GetPosition()
        {
            Vector3D positionVector = new Vector3D();

            for (int i = 0; i < 3; i++)
            {
                positionVector[i] = currentModellingMatrix[i, 3];
            }

            return positionVector;
        }

        /// <summary>
        /// Gets the rotation direction.
        /// </summary>
        /// <returns></returns>
        private Direction GetRotationDirection()
        {
            Angle rotationAngle = GetRotationAngle();
            double s = Math.Sin(rotationAngle.Value);

            Vector3D directionVector = new Vector3D();
            directionVector[0] = (currentModellingMatrix[2, 1] - currentModellingMatrix[1, 2])/(2*s);
            directionVector[1] = (currentModellingMatrix[0, 2] - currentModellingMatrix[2, 0])/(2*s);
            directionVector[2] = (currentModellingMatrix[1, 0] - currentModellingMatrix[0, 1])/(2*s);
            return new Direction(directionVector);
        }

        /// <summary>
        /// Gets the rotation angle.
        /// </summary>
        /// <returns></returns>
        private Angle GetRotationAngle()
        {
            //There is a mistake in Hill. cos(beta) = (3D trace - 1) /2

            //As 3D trace = 4D trace - 1 we get
            double angleValue = Math.Acos(currentModellingMatrix.Trace/2 - 1);
            return new Angle(angleValue);
        }

        #endregion
    }
}
