using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.SimpleGraphicsFramework
{
    /// <summary>
    /// Defines methods for a graphical engine.
    /// </summary>
    public interface IGraphicalEngine
    {
        #region General Methods

        /// <summary>
        /// Clears the viewport.
        /// </summary>
        void Clear();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        void Draw();

        #endregion

        #region Graphical Objects

        /// <summary>
        /// Adds the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        void AddGraphicalObject(IGraphicalObject graphicalObject);

        /// <summary>
        /// Removes the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        void RemoveGraphicalObject(IGraphicalObject graphicalObject);

        /// <summary>
        /// Rotates the graphical object round origin of coordinates round axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateGraphicalObjectRoundOriginOfCoordinatesRoundAxis(IGraphicalObject graphicalObject,
                                                                    Angle rotationAngle,
                                                                    Axis rotationAxis);

        /// <summary>
        /// Rotates the graphical object round its center round axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateGraphicalObjectRoundItsCenterRoundAxis(IGraphicalObject graphicalObject,
                                                          Angle rotationAngle,
                                                          Axis rotationAxis);

        /// <summary>
        /// Rotates the graphical object round some point round some vector.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationAxisDirectionVector">The rotation axis direction vector.</param>
        void RotateGraphicalObjectRoundSomePointRoundSomeVector(IGraphicalObject graphicalObject,
                                                                Angle rotationAngle,
                                                                Vector3D centerOfRotation,
                                                                Vector3D rotationAxisDirectionVector);

        /// <summary>
        /// Moves the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="displacementVector">The displacement vector.</param>
        void MoveGraphicalObject(IGraphicalObject graphicalObject, Vector3D displacementVector);

        /// <summary>
        /// Gets the graphical object position.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <returns></returns>
        Vector GetGraphicalObjectPosition(IGraphicalObject graphicalObject);

        /// <summary>
        /// Gets the graphical object direction angle along axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="directionAngleAxis">The direction angle axis.</param>
        /// <returns></returns>
        Angle GetGraphicalObjectDirectionAngleAlongAxis(IGraphicalObject graphicalObject, Axis directionAngleAxis);

        #endregion

        #region Camera

        /// <summary>
        /// Gets or sets the camera parameters.
        /// </summary>
        /// <value>The camera parameters.</value>
        CameraParameters CameraParameters
        {
            get; set;
        }

        /// <summary>
        /// Rotates the camera round origin of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateCameraRoundOriginOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Rotates the camera round its center round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        void RotateCameraRoundItsCenterRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Rotates the camera round some point round some vector.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationAxisDirectionVector">The rotation axis direction vector.</param>
        void RotateCameraRoundSomePointRoundSomeVector(Angle rotationAngle,
                                                       Vector3D centerOfRotation,
                                                       Vector3D rotationAxisDirectionVector);

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        void MoveCamera(Vector3D displacementVector);

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        Vector3D GetCameraPosition();

        #endregion

    }
}