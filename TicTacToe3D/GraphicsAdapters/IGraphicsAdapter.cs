using GraphicsFramework.LinearAlgebra;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GraphicsAdapters
{
    /// <summary>
    /// Defines methods for graphics adapter.
    /// </summary>
    /// <remarks>
    /// Adapter/facade/gateway patterns.
    /// </remarks>
    public interface IGraphicsAdapter
    {
        /// <summary>
        /// Clears the viewport.
        /// </summary>
        void Clear();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        void Draw();

        /// <summary>
        /// Removes all game objects.
        /// </summary>
        void RemoveAllGameObjects();

        /// <summary>
        /// Adds the game object.
        /// </summary>
        /// <param name="side">The side.</param>
        /// <param name="cell">The cell.</param>
        void AddGameObject(Side side, Cell cell);

        /// <summary>
        /// Removes the game object.
        /// </summary>
        /// <param name="cell">The cell.</param>
        void RemoveGameObject(Cell cell);

        /// <summary>
        /// Moves the game object.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        /// <param name="destinationCell">The destination cell.</param>
        void MoveGameObject(Cell startingCell, Cell destinationCell);

        /// <summary>
        /// Rotates the camera round center of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateCameraRoundCenterOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        void MoveCamera(Vector3D displacementVector);

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        Vector GetCameraPosition();

        /// <summary>
        /// Gets the direction of the given axis after camera rotation, as if this axis were rotated with the camera
        /// (assumed that the camera was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        Direction GetDirectionOfCameraAxisAfterRotation(Axis axis);
    }
}