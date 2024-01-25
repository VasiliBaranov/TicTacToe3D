using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;
using GraphicsFramework;
using TicTacToe3D.Properties;

namespace TicTacToe3D.GraphicsAdapters.AdvancedGraphics
{
    /// <summary>
    /// Represents a graphics adapter on the basis of the graphics framework.
    /// </summary>
    public class AdvancedGraphicsAdapter : BaseGraphicsAdapter
    {
        #region Fields

        private readonly IGraphicalEngine _engine;
        private readonly IGraphicalObject _gameSpace;
        private readonly IWorld _world;
        private readonly Dictionary<Cell, IGraphicalObject> _gameObjectsCells;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedGraphicsAdapter"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="gameField">The game field.</param>
        public AdvancedGraphicsAdapter(Graphics graphics, IGameField gameField) : base(gameField)
        {
            Camera camera = CreateCamera(graphics);
            _world = new World {Camera = camera};

            _engine = new GraphicalEngine(graphics, _world);

            graphics.PageUnit = GraphicsUnit.Pixel;

            _gameObjectsCells = new Dictionary<Cell, IGraphicalObject>();

            _gameSpace = GraphicalObjectsFactory.CreateGameSpace(CellSize, Settings.Default.DrawingColor);

            _world.AddGraphicalObject(_gameSpace);
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Clears the viewport
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
                _world.RemoveGraphicalObject(gameObject);
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

            IGraphicalObject gameObject = GraphicalObjectsFactory.CreateGameObject(CellSize, side);

            Vector3D cellCenter = GetCellCenterVector(cell);
            gameObject.Move(cellCenter);

            _world.AddGraphicalObject(gameObject);

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
            _world.RemoveGraphicalObject(gameObject);

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

            Vector3D startingCellCenterVector = GetCellCenterVector(startingCell);
            Vector3D destinationCellCenterVector = GetCellCenterVector(destinationCell);
            Vector3D displacementVector = destinationCellCenterVector - startingCellCenterVector;

            gameObject.Move(displacementVector);

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
            _world.Camera.RotateRoundOriginOfCoordinatesRoundAxis(rotationAngle, rotationAxis);
        }

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        public override void MoveCamera(Vector3D displacementVector)
        {
            _world.Camera.Move(displacementVector);
        }

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        public override Vector GetCameraPosition()
        {
            return _world.Camera.Position;
        }

        /// <summary>
        /// Gets the direction of the given axis after camera rotation, as if this axis were rotated with the camera
        /// (assumed that the camera was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        public override Direction GetDirectionOfCameraAxisAfterRotation(Axis axis)
        {
            return _world.Camera.GetDirectionOfAxisAfterRotation(axis);
        }

        #endregion

        #region Private Methods

        private static Camera CreateCamera(Graphics graphics)
        {
            Camera camera = new Camera(GetCameraParameters(graphics));
            camera.Move(GetCameraDispacementVector());

            return camera;
        }

        #endregion
    }
}