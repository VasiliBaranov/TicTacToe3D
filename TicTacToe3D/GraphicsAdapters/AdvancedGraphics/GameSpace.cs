using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using System.Linq;

namespace TicTacToe3D.GraphicsAdapters.AdvancedGraphics
{
    /// <summary>
    /// Represents a game space.
    /// </summary>
    /// <remarks>
    /// TODO: REFACTOR!!!
    /// </remarks>
    public class GameSpace : GraphicalObject
    {
        private readonly double _cellSize;
        private readonly Pen _drawingPen;


        public GameSpace(double cellSize, Color drawingColor)
        {
            _cellSize = cellSize;
            _drawingPen = new Pen(drawingColor);

            AssignMesh();
        }

        protected void AssignMesh()
        {
            AddHorizontalPlanes();
            AddVerticalPlanes();
        }

        private void AddHorizontalPlanes()
        {
            double halfSize = 1.5 * _cellSize;
            double planesDisplacement = 0.5 * _cellSize;

            List<AffinePoint> horizontalPlaneVertices = new List<AffinePoint>
                                                            {
                                                                new AffinePoint(halfSize, halfSize, 0),
                                                                new AffinePoint(halfSize, -halfSize, 0),
                                                                new AffinePoint(-halfSize, -halfSize, 0),
                                                                new AffinePoint(-halfSize, halfSize, 0),
                                                            };
            Texture horizontalTexture = GetHorizontalTexture();
            List<Vector2D> horizontalTextureCoordinates =
                GetTextureCoordinates(horizontalPlaneVertices, horizontalTexture, 0, 1);

            AddPlane(horizontalPlaneVertices, -planesDisplacement, Axis.Z, horizontalTexture, horizontalTextureCoordinates);
            AddPlane(horizontalPlaneVertices, planesDisplacement, Axis.Z, horizontalTexture, horizontalTextureCoordinates);
        }

        private void AddVerticalPlanes()
        {
            double halfSize = 1.5 * _cellSize;
            double planesDisplacement = 0.5 * _cellSize;

            List<AffinePoint> verticalPlaneVertices = new List<AffinePoint>
                                                          {
                                                              new AffinePoint(halfSize, 0, halfSize),
                                                              new AffinePoint(halfSize, 0, -halfSize),
                                                              new AffinePoint(-halfSize, 0, -halfSize),
                                                              new AffinePoint(-halfSize, 0, halfSize),
                                                          };

            Texture verticalTexture = GetVerticalTexture();
            List<Vector2D> verticalTextureCoordinates =
                GetTextureCoordinates(verticalPlaneVertices, verticalTexture, 0, 2);
            AddPlane(verticalPlaneVertices, -planesDisplacement, Axis.Y, verticalTexture, verticalTextureCoordinates);
            AddPlane(verticalPlaneVertices, planesDisplacement, Axis.Y, verticalTexture, verticalTextureCoordinates);
        }

        /// <summary>
        /// Gets the horizontal texture.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Horizontal texture will consist of 4 lines: 
        ///    1   2
        ///    |   |   
        /// -----------3
        ///    |   |   
        /// -----------4
        ///    |   |   
        /// </remarks>
        private Texture GetHorizontalTexture()
        {
            List<KeyValuePair<PointF, PointF>> lines = GetHorizontalLines();
            lines.AddRange(GetVerticalLines());

            return GetTexture(lines);
        }

        /// <summary>
        /// Gets the vertical texture.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Texture will consist of 2 vertical lines: 
        ///    1   2
        ///    |   |   
        /// - -   -   -
        ///    |   |   
        /// -   -   -  
        ///    |   |   
        /// </remarks>
        private Texture GetVerticalTexture()
        {
            List<KeyValuePair<PointF, PointF>> lines = GetVerticalLines();

            return GetTexture(lines);
        }

        private Texture GetTexture(IEnumerable<KeyValuePair<PointF, PointF>> lines)
        {
            Bitmap bitmap = new Bitmap((int)(3 * _cellSize), (int)(3 * _cellSize));
            Graphics graphics = Graphics.FromImage(bitmap);

            foreach (KeyValuePair<PointF, PointF> line in lines)
            {
                graphics.DrawLine(_drawingPen, line.Key, line.Value);
            }

            return new Texture { Bitmap = bitmap };
        }

        /// <summary>
        /// Gets vertical lines coordinates for textures.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Texture will consist of 2 vertical lines: 
        ///    1   2
        ///    |   |   
        /// - -   -   -
        ///    |   |   
        /// -   -   -  
        ///    |   |   
        /// </remarks>
        private List<KeyValuePair<PointF, PointF>> GetVerticalLines()
        {
            float cellSize = (float)_cellSize;

            List<KeyValuePair<PointF, PointF>> lines =
                new List<KeyValuePair<PointF, PointF>>
                    {
                        new KeyValuePair<PointF, PointF>(new PointF(cellSize, 0),
                                                         new PointF(cellSize, 3 * cellSize)),
                        new KeyValuePair<PointF, PointF>(new PointF(2 * cellSize, 0),
                                                         new PointF(2 * cellSize, 3 * cellSize))
                    };
            return lines;
        }

        /// <summary>
        /// Gets the horizontal lines for a texture.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Texture will consist of 2 lines: 
        ///    |   |   
        /// -----------1
        ///            
        /// -----------2
        ///    |   |   
        /// </remarks>
        private List<KeyValuePair<PointF, PointF>> GetHorizontalLines()
        {
            float cellSize = (float) _cellSize;

            List<KeyValuePair<PointF, PointF>> lines =
                new List<KeyValuePair<PointF, PointF>>
                    {
                        new KeyValuePair<PointF, PointF>(new PointF(0, cellSize),
                                                         new PointF(3 * cellSize, cellSize)),
                        new KeyValuePair<PointF, PointF>(new PointF(0, 2 * cellSize),
                                                         new PointF(3 * cellSize, 2 * cellSize))
                    };
            return lines;
        }

        private void AddPlane(IEnumerable<AffinePoint> templateVertices,
            double displacementDistance,
            Axis displacementAxis,
            Texture texture,
            IEnumerable<Vector2D> textureCoordinates)
        {
            int verticesCount = MeshVertices.Count;
            List<AffinePoint> meshVertices = MovePlaneAlongAxis(templateVertices, displacementDistance, displacementAxis);
            MeshVertices.AddRange(meshVertices);

            List<PlaneVertex> planeVertices = GetPlaneVertices(textureCoordinates, verticesCount);

            Plane plane = new Plane
                              {
                                  ParentObject = this,
                                  Normal = GetNormal(displacementDistance, displacementAxis),
                                  PlaneVertices = planeVertices,
                                  Texture = texture.Clone() as Texture
                              };
            Planes.Add(plane);
        }

        private AffinePoint GetNormal(double displacementDistance, Axis displacementAxis)
        {
            Direction axisDirection = new Direction(displacementAxis);
            Vector3D axisUnitVector = axisDirection.UnitVectorAlongDirection;
            if (displacementDistance < 0)
            {
                axisUnitVector = -axisUnitVector;
            }

            return new AffinePoint(axisUnitVector, true);
        }

        /// <summary>
        /// Gets the plane vertices.
        /// </summary>
        /// <param name="textureCoordinates">The texture coordinates.</param>
        /// <param name="startingIndex">Inclusive starting index for plane vertices.</param>
        /// <returns></returns>
        private List<PlaneVertex> GetPlaneVertices(IEnumerable<Vector2D> textureCoordinates, int startingIndex)
        {
            return textureCoordinates.Select(
                (textureCoordinate, index) =>
                new PlaneVertex
                    {
                        TextureCoordinates = textureCoordinate.Clone() as Vector2D,
                        VertexIndex = startingIndex + index
                    }).ToList();
        }

        private List<AffinePoint> MovePlaneAlongAxis(IEnumerable<AffinePoint> planeVertices, double distance, Axis axis)
        {
            Direction directionAlongAxis = new Direction(axis);
            Vector3D vectorAlongAxis = distance * directionAlongAxis.UnitVectorAlongDirection;

            AffinePoint displacement = new AffinePoint(vectorAlongAxis, true);

            return MovePlane(planeVertices, displacement);
        }

        private List<AffinePoint> MovePlane(IEnumerable<AffinePoint> planeVertices, AffinePoint displacement)
        {
            return planeVertices.Select(affinePoint => affinePoint + displacement).ToList();
        }

        private List<Vector2D> GetTextureCoordinates(IEnumerable<AffinePoint> vertexCoordinates,
            Texture texture, int xTextureCoordinateIndex, int yTextureCoordinateIndex)
        {
            double halfWidth = texture.Bitmap.Width / 2.0;
            double halfHeight = texture.Bitmap.Height / 2.0;
            return vertexCoordinates.
                Select(vertex => new Vector2D(vertex[xTextureCoordinateIndex] + halfWidth,
                                              vertex[yTextureCoordinateIndex] + halfHeight)).
                ToList();
        }
    }
}