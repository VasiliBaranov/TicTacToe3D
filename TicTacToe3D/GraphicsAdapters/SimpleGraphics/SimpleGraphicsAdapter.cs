using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.Properties;
using TicTacToe3D.SimpleGraphicsFramework;
using IGraphicalObject = TicTacToe3D.SimpleGraphicsFramework.IGraphicalObject;

namespace TicTacToe3D.GraphicsAdapters.SimpleGraphics
{
    /// <summary>
    /// Represents a graphics adapter over the simple graphics engine.
    /// </summary>
    public class SimpleGraphicsAdapter : BaseGraphicsAdapter
    {
        private readonly GraphicalEngine _engine;
        private readonly IGraphicalObject _gameSpace;
        private readonly double _initialCameraDistance;
        private readonly Dictionary<Cell, IGraphicalObject> _gameObjectsCells;

        private readonly Color _drawingColor;

        public SimpleGraphicsAdapter(Graphics graphics, IGameField gameField)
            : base(gameField)
        {
            _drawingColor = Settings.Default.DrawingColor;

            CameraParameters cameraParameters = GetCameraParameters(graphics);

            graphics.PageUnit = GraphicsUnit.Pixel;
            graphics.TranslateTransform((float) cameraParameters.NearPlaneSizeAlongX / 2,
                                        (float) cameraParameters.NearPlaneSizeAlongY / 2);

            _engine = new GraphicalEngine(graphics);
            _engine.CameraParameters = cameraParameters;

            Vector3D cameraDisplacementVector = GetCameraDispacementVector();
            _initialCameraDistance = cameraDisplacementVector.Length;
            _engine.MoveCamera(cameraDisplacementVector);

            _gameObjectsCells = new Dictionary<Cell, IGraphicalObject>();

            _gameSpace = GraphicalObjectsFactory.CreateGameSpace(CellSize, _drawingColor);

            _engine.AddGraphicalObject(_gameSpace);
        }


        #region IGraphicsAdapter Members

        /// <summary>
        /// Clears the viewport.
        /// </summary>
        public override void Clear()
        {
            _engine.Clear();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            _engine.Draw();
        }

        /// <summary>
        /// Removes all game objects.
        /// </summary>
        public override void RemoveAllGameObjects()
        {
            foreach (IGraphicalObject gameObject in _gameObjectsCells.Values)
            {
                _engine.RemoveGraphicalObject(gameObject);
            }
        }

        /// <summary>
        /// Adds the game object.
        /// </summary>
        /// <param name="side">The side.</param>
        /// <param name="cell">The cell.</param>
        public override void AddGameObject(Side side, Cell cell)
        {
            if (_gameObjectsCells.ContainsKey(cell))
            {
                throw new InvalidOperationException("this cell is not empty");
            }

            IGraphicalObject gameObject = GraphicalObjectsFactory.CreateGameObject(CellSize, side, _drawingColor);
            Vector3D cellCenter = GetCellCenterVector(cell);

            _engine.AddGraphicalObject(gameObject);
            _engine.MoveGraphicalObject(gameObject, cellCenter);

            _gameObjectsCells.Add(cell, gameObject);
        }

        /// <summary>
        /// Removes the game object.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public override void RemoveGameObject(Cell cell)
        {
            if (!_gameObjectsCells.ContainsKey(cell))
            {
                throw new InvalidOperationException("this cell is empty");
            }

            IGraphicalObject gameObject = _gameObjectsCells[cell];
            _engine.RemoveGraphicalObject(gameObject);

            _gameObjectsCells.Remove(cell);
        }

        /// <summary>
        /// Moves the game object.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        /// <param name="destinationCell">The destination cell.</param>
        public override void MoveGameObject(Cell startingCell, Cell destinationCell)
        {
            if (!_gameObjectsCells.ContainsKey(startingCell))
            {
                throw new InvalidOperationException("this cell is empty");
            }

            IGraphicalObject gameObject = _gameObjectsCells[startingCell];
            _engine.MoveGraphicalObject(gameObject, GetDisplacementVector(startingCell, destinationCell));

            _gameObjectsCells.Remove(startingCell);
            _gameObjectsCells.Add(destinationCell, gameObject);
        }

        /// <summary>
        /// Rotates the camera round center of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public override void RotateCameraRoundCenterOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis)
        {
            _engine.RotateCameraRoundOriginOfCoordinatesRoundAxis(rotationAngle, rotationAxis);
        }

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        public override void MoveCamera(Vector3D displacementVector)
        {
            if (CanMoveCamera(displacementVector))
            {
                _engine.MoveCamera(displacementVector);
            }
        }

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        public override Vector GetCameraPosition()
        {
            return _engine.GetCameraPosition();
        }

        /// <summary>
        /// Gets the direction of the given axis after camera rotation, as if this axis were rotated with the camera
        /// (assumed that the camera was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        public override Direction GetDirectionOfCameraAxisAfterRotation(Axis axis)
        {
            throw new NotImplementedException();
        }

        #endregion

        private bool CanMoveCamera(Vector3D displacementVector)
        {
            Vector3D cameraPosition = _engine.GetCameraPosition();
            Vector3D newCameraPosition = cameraPosition + displacementVector;
            double newCameraDistance = newCameraPosition.Length;
            return newCameraDistance >= _initialCameraDistance;

            ////min length^2 = 3 * (a / 2)^2, min length = sqrt(3) a / 2; a is a game field side
            //Vector3D cameraPosition = _engine.GetCameraPosition();
            //Vector3D newCameraPosition = cameraPosition + displacementVector;

            //double positionLength = cameraPosition.Length;
            //double newPositionLength = newCameraPosition.Length;
            //if(newPositionLength > positionLength)
            //{
            //    return true;
            //}

            //double gameFieldSize = CellSize * 3;
            //bool cameraCrossesGameField = newPositionLength <
            //                              Math.Sqrt(3) / 2 * gameFieldSize;

            //return !cameraCrossesGameField;
        }
    }
}