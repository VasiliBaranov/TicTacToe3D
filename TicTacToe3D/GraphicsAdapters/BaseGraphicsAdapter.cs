using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.Properties;

namespace TicTacToe3D.GraphicsAdapters
{
    /// <summary>
    /// Represents a base graphics adapter.
    /// </summary>
    public abstract class BaseGraphicsAdapter : IGraphicsAdapter
    {
        #region Fields

        private readonly double _cellSize;
        private readonly IGameField _gameField;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGraphicsAdapter"/> class.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        protected BaseGraphicsAdapter(IGameField gameField)
        {
            _gameField = gameField;
            _cellSize = Settings.Default.DefaultCellSize;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the size of the cell.
        /// </summary>
        /// <value>The size of the cell.</value>
        protected double CellSize
        {
            get
            {
                return _cellSize;
            }
        }

        /// <summary>
        /// Gets the game field.
        /// </summary>
        /// <value>The game field.</value>
        protected IGameField GameField
        {
            get
            {
                return _gameField;
            }
        }

        #endregion

        #region Private Methods

        protected static CameraParameters GetCameraParameters(Graphics graphics)
        {
            Settings settings = Settings.Default;
            RectangleF clipBounds = graphics.VisibleClipBounds;
            CameraParameters cameraParameters = new CameraParameters
            {
                DistanceToTheFarPlane = settings.DistanceToTheFarPlaneOfCamera,
                DistanceToTheNearPlane = settings.DistanceToTheNearPlaneOfCamera,
                NearPlaneSizeAlongX = clipBounds.Width,
                NearPlaneSizeAlongY = clipBounds.Height
            };

            return cameraParameters;
        }

        protected static Vector3D GetCameraDispacementVector()
        {
            Settings settings = Settings.Default;
            Vector3D cameraDisplacementVector = new Vector3D();
            cameraDisplacementVector[0] = settings.DefaultCameraPositionAlongX;
            cameraDisplacementVector[1] = settings.DefaultCameraPositionAlongY;
            cameraDisplacementVector[2] = settings.DefaultCameraPositionAlongZ;

            return cameraDisplacementVector;
        }

        /// <summary>
        /// Gets the displacement vector from the starting cell to the destination cell.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        /// <param name="destinationCell">The destination cell.</param>
        /// <returns></returns>
        protected Vector3D GetDisplacementVector(Cell startingCell, Cell destinationCell)
        {
            Vector3D startingCellCenterVector = GetCellCenterVector(startingCell);
            Vector3D destinationCellCenterVector = GetCellCenterVector(destinationCell);
            Vector3D displacementVector = destinationCellCenterVector - startingCellCenterVector;
            return displacementVector;
        }

        /// <summary>
        /// Gets the cell center vector.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns></returns>
        protected Vector3D GetCellCenterVector(Cell cell)
        {
            Vector3D cellCenterVector = new Vector3D();

            cellCenterVector[0] = cell.I * _cellSize + _cellSize / 2;
            cellCenterVector[1] = cell.J * _cellSize + _cellSize / 2;
            cellCenterVector[2] = cell.K * _cellSize + _cellSize / 2;

            Vector3D gameSpaceCenter = new Vector3D();
            GameFieldParameters gamefieldParameters = _gameField.GameFieldParameters;

            gameSpaceCenter[0] = (gamefieldParameters.MinCoordinateAlongX + gamefieldParameters.SizeAlongX) * _cellSize / 2;
            gameSpaceCenter[1] = (gamefieldParameters.MinCoordinateAlongY + gamefieldParameters.SizeAlongY) * _cellSize / 2;
            gameSpaceCenter[2] = (gamefieldParameters.MinCoordinateAlongZ + gamefieldParameters.SizeAlongZ) * _cellSize / 2;

            cellCenterVector = cellCenterVector - gameSpaceCenter;

            return cellCenterVector;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears the viewport.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Removes all game objects.
        /// </summary>
        public abstract void RemoveAllGameObjects();

        /// <summary>
        /// Adds the game object.
        /// </summary>
        /// <param name="side">The side.</param>
        /// <param name="cell">The cell.</param>
        public abstract void AddGameObject(Side side, Cell cell);

        /// <summary>
        /// Removes the game object.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public abstract void RemoveGameObject(Cell cell);

        /// <summary>
        /// Moves the game object.
        /// </summary>
        /// <param name="startingCell">The starting cell.</param>
        /// <param name="destinationCell">The destination cell.</param>
        public abstract void MoveGameObject(Cell startingCell, Cell destinationCell);

        /// <summary>
        /// Rotates the camera round center of coordinates round axis.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <param name="rotationAxis">The rotation axis.</param>
        public abstract void RotateCameraRoundCenterOfCoordinatesRoundAxis(Angle rotationAngle, Axis rotationAxis);

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="displacementVector">The displacement vector.</param>
        public abstract void MoveCamera(Vector3D displacementVector);

        /// <summary>
        /// Gets the camera position.
        /// </summary>
        /// <returns></returns>
        public abstract Vector GetCameraPosition();

        /// <summary>
        /// Gets the direction of the given axis after camera rotation, as if this axis were rotated with the camera
        /// (assumed that the camera was first rotated, and then moved to the current position).
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        public abstract Direction GetDirectionOfCameraAxisAfterRotation(Axis axis);

        #endregion
    }
}