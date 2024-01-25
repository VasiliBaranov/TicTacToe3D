using System;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Defines methods and properties for the world object.
    /// </summary>
    public interface IWorldObject : ICloneable
    {
        #region Properties

        /// <summary>
        /// Gets the direction of the object rotation 
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The direction.</value>
        Direction RotationDirection { get; }

        /// <summary>
        /// Gets the rotation angle
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The rotation angle.</value>
        Angle RotationAngle { get; }

        /// <summary>
        /// Gets the position of the object
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <value>The position.</value>
        Vector3D Position { get; }

        /// <summary>
        /// Gets the modelling matrix, i.e. the matrix that represents rotations and moves of the object.
        /// </summary>
        /// <value>The modelling matrix.</value>
        Matrix ModellingMatrix { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Rotates the object round the origin of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateRoundOriginOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Rotates the obejct round its center round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateRoundItsCenterRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Rotates the object round some point round some vector.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationDirection">The rotation direction.</param>
        /// <remarks> See Хилл, p.296, (5.33) </remarks>
        void RotateRoundSomePointRoundSomeDirection(Angle rotationAngle, Vector3D centerOfRotation, Direction rotationDirection);

        /// <summary>
        /// Rotats the object round some direction.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationDirection">The rotation direction.</param>
        void RotatRoundSomeDirection(Angle rotationAngle, Direction rotationDirection);

        /// <summary>
        /// Moves the object by specified displacement vector.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        void Move(Vector3D displacementVector);

        /// <summary>
        /// Gets the direction of the given axis after object rotation, as if this axis were rotated with the object
        /// (assumed that the object was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        Direction GetDirectionOfAxisAfterRotation(Axis axis);

        #endregion
    }
}