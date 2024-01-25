using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using TicTacToe3D.SimpleGraphicsFramework;

namespace TicTacToe3D.SimpleGraphicsFramework
{
    /// <summary>
    /// Represents a simple graphical engine.
    /// </summary>
    public class GraphicalEngine : IGraphicalEngine
    {
        #region Fields

        /// <summary>
        /// matrix descripting camera position
        /// </summary>
        private Matrix _viewMatrix; //V

        /// <summary>
        /// matrix descripting proection on a screen
        /// </summary>
        private Matrix _proectionMatrix;  //P

        /// <summary>
        /// graphics object which is a screen to draw on
        /// </summary>
        private readonly Graphics _graphics;

        private readonly Dictionary<IGraphicalObject, Matrix> _graphObjectsAndModellingMatrices;   //M

        private CameraParameters cameraParameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of a graphical engine. 
        /// At once camera is placed at the center of coordinates and directed oppositely to the z axis
        /// </summary>
        /// <param name="graphics"></param>
        public GraphicalEngine(Graphics graphics)
        {
            _graphics = graphics;
            _graphObjectsAndModellingMatrices = new Dictionary<IGraphicalObject, Matrix>();

            _viewMatrix = Matrix.GetIdentity(4);
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Clears the viewport.
        /// </summary>
        public void Clear()
        {
            _graphics.Clear(Color.White);
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            if (_proectionMatrix == null)
            {
                throw new InvalidOperationException("camera properties have not been set yet");
            }

            Clear();

            Matrix proectionViewMatrix = _proectionMatrix * _viewMatrix;

            foreach (KeyValuePair<IGraphicalObject, Matrix> pair in _graphObjectsAndModellingMatrices)
            {
                Matrix fullTransormationMatrix = proectionViewMatrix * pair.Value;

                pair.Key.Draw(fullTransormationMatrix, _graphics);
            }
        }

        public CameraParameters CameraParameters
        {
            get
            {
                return cameraParameters;
            }
            set
            {
                cameraParameters = value;
                AssignProectionMatrix();
            }
        }


        /// <summary>
        /// Adds the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        public void AddGraphicalObject(IGraphicalObject graphicalObject)
        {
            _graphObjectsAndModellingMatrices.Add(graphicalObject, Matrix.GetIdentity(4));
        }

        /// <summary>
        /// Removes the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        public void RemoveGraphicalObject(IGraphicalObject graphicalObject)
        {
            _graphObjectsAndModellingMatrices.Remove(graphicalObject);
        }

        /// <summary>
        /// Rotates the graphical object round origin of coordinates round axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public void RotateGraphicalObjectRoundOriginOfCoordinatesRoundAxis(IGraphicalObject graphicalObject,
            Angle rotationAngle,
            Axis rotationAxis)
        {
            if (!_graphObjectsAndModellingMatrices.ContainsKey(graphicalObject))
            {
                throw new InvalidOperationException("graphical object is not present");
            }

            Matrix graphObjectModellingMatrix = _graphObjectsAndModellingMatrices[graphicalObject];
            _graphObjectsAndModellingMatrices[graphicalObject] =
                GetTransformationMatrixForRotation(rotationAngle, rotationAxis) * graphObjectModellingMatrix;
        }

        /// <summary>
        /// Rotates the graphical object round its center round axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public void RotateGraphicalObjectRoundItsCenterRoundAxis(IGraphicalObject graphicalObject,
            Angle rotationAngle,
            Axis rotationAxis)
        {
            if (!_graphObjectsAndModellingMatrices.ContainsKey(graphicalObject))
            {
                throw new InvalidOperationException("graphical object is not present");
            }

            Matrix graphObjectModellingMatrix = _graphObjectsAndModellingMatrices[graphicalObject];
            _graphObjectsAndModellingMatrices[graphicalObject] =
                graphObjectModellingMatrix * GetTransformationMatrixForRotation(rotationAngle, rotationAxis);
        }

        /// <summary>
        /// Rotates the graphical object round some point round some vector.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationAxisDirectionVector">The rotation axis direction vector.</param>
        public void RotateGraphicalObjectRoundSomePointRoundSomeVector(IGraphicalObject graphicalObject,
            Angle rotationAngle,
            Vector3D centerOfRotation,
            Vector3D rotationAxisDirectionVector)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Moves the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="displacementVector">The displacement vector.</param>
        public void MoveGraphicalObject(IGraphicalObject graphicalObject, Vector3D displacementVector)
        {
            if (!_graphObjectsAndModellingMatrices.ContainsKey(graphicalObject))
            {
                throw new InvalidOperationException("graphical object is not present");
            }

            Matrix graphObjectModellingMatrix = _graphObjectsAndModellingMatrices[graphicalObject];
            _graphObjectsAndModellingMatrices[graphicalObject] =
                GetTransformationMatrixForMove(displacementVector) * graphObjectModellingMatrix;
        }

        /// <summary>
        /// Gets the graphical object position.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <returns></returns>
        public Vector GetGraphicalObjectPosition(IGraphicalObject graphicalObject)
        {
            Matrix modellingMatrix = _graphObjectsAndModellingMatrices[graphicalObject];

            Vector positionVector = new Vector(3);

            for (int i = 0; i < 3; i++)
            {
                positionVector[i] = modellingMatrix[i, 3];
            }

            return positionVector;
        }

        /// <summary>
        /// Gets the graphical object direction angle along axis.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <param name="directionAngleAxis">The direction angle axis.</param>
        /// <returns></returns>
        public Angle GetGraphicalObjectDirectionAngleAlongAxis(IGraphicalObject graphicalObject, Axis directionAngleAxis)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Rotates the camera round origin of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public void RotateCameraRoundOriginOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis)
        {
            //camera actions are inverse actions over all objects,
            //so we should calculate inverse matrices
            //Fortunately, spatial rotation matrices are orthonormal, 
            //so their inverses are equal to transposed matrices
            /*Matrix transformationMatrix = GetTransformationMatrixForRotation(rotationAngle, rotationAxis);
            transformationMatrix = transformationMatrix.Transpose();*/
            Matrix transformationMatrix = GetTransformationMatrixForRotation(-rotationAngle, rotationAxis);

            _viewMatrix = _viewMatrix * transformationMatrix;
        }

        /// <summary>
        /// Rotates the camera round its center round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public void RotateCameraRoundItsCenterRoundAxis(Angle rotationAngle, Axis rotationAxis)
        {
            Matrix transformationMatrix = GetTransformationMatrixForRotation(-rotationAngle, rotationAxis);

            _viewMatrix = transformationMatrix * _viewMatrix;
        }

        /// <summary>
        /// Rotates the camera round some point round some vector.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="centerOfRotation">The center of rotation.</param>
        /// <param name="rotationAxisDirectionVector">The rotation axis direction vector.</param>
        public void RotateCameraRoundSomePointRoundSomeVector(Angle rotationAngle, Vector3D centerOfRotation, Vector3D rotationAxisDirectionVector)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        public void MoveCamera(Vector3D displacementVector)
        {
            /*Matrix transformationMatrix = GetTransformationMatrixForMove(displacementVector);
            for (int i = 0; i < 3; i++)
            {
                transformationMatrix[i, 3] = -transformationMatrix[i, 3];
            }*/

            Matrix transformationMatrix = GetTransformationMatrixForMove(-displacementVector);

            _viewMatrix = transformationMatrix * _viewMatrix;
        }

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        public Vector3D GetCameraPosition()
        {
            Vector3D positionVector = new Vector3D();

            for (int i = 0; i < 3; i++)
            {
                positionVector[i] = -_viewMatrix[i, 3];
            }

            return positionVector;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the transformation matrix for rotation.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        /// <returns></returns>
        private static Matrix GetTransformationMatrixForRotation(Angle rotationAngle, Axis rotationAxis)
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
        private static Matrix GetTransformationMatrixForRotationRoundX(Angle rotationAngle)
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
        private static Matrix GetTransformationMatrixForRotationRoundY(Angle rotationAngle)
        {

            double c = Math.Cos(rotationAngle.Value);
            double s = Math.Sin(rotationAngle.Value);

            //update the transformation matrixto represent a turn round the Y axis

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
        private static Matrix GetTransformationMatrixForRotationRoundZ(Angle rotationAngle)
        {
            double c = Math.Cos(rotationAngle.Value);
            double s = Math.Sin(rotationAngle.Value);

            //update the transformation matrixto represent a turn round the Z axis

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
        private static Matrix GetTransformationMatrixForMove(Vector vector)
        {
            Matrix transformationMatrix = Matrix.GetIdentity(4);

            for (int i = 0; i < 3; i++)
            {
                transformationMatrix[i, 3] = vector[i];
            }

            return transformationMatrix;
        }

        /// <summary> 
        /// Assigns the default value for the proection matrix 
        /// </summary> 
        private void AssignProectionMatrix()
        {
            double distanceToTheFarPlane = cameraParameters.DistanceToTheFarPlane;
            double distanceToTheNearPlane = cameraParameters.DistanceToTheNearPlane;

            double a = -(distanceToTheFarPlane + distanceToTheNearPlane) / (distanceToTheFarPlane - distanceToTheNearPlane);
            double b = -2 * distanceToTheFarPlane * distanceToTheNearPlane / (distanceToTheFarPlane - distanceToTheNearPlane);

            _proectionMatrix = new Matrix(4, 4);

            _proectionMatrix[0, 0] = distanceToTheNearPlane;
            _proectionMatrix[1, 1] = distanceToTheNearPlane;
            _proectionMatrix[2, 2] = a;
            _proectionMatrix[2, 3] = b;
            _proectionMatrix[3, 2] = -1;
        }

        #endregion
    }
}