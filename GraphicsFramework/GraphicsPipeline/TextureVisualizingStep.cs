using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.Core;
using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using System.Linq;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Fills the visible picture according to textures and plane positions.
    /// Preconditions:
    /// 1. Should be applied after the projection step.
    /// 2. All the coordinates (both projected world coordinates and texture ones) 
    /// should be transformed to the bitmap standart before using this step
    /// (i.e. the origin of the coordinates should be in the top left corner of the viewport, 
    /// and that the Y axis should go downwards).
    /// 3. All planes should be a convex.
    /// </summary>
    public class TextureVisualizingStep : IGraphicsPipelineStep
    {

        #region Fields

        private readonly EdgeBuilder edgeBuilder = new EdgeBuilder();

        private List<PlaneEdge> currentPlaneEdges;

        private IVisibleWorld currentVisibleWorld;

        private GraphicalObject currentObject;

        private Plane currentPlane;

        private double[,] zIndexes;

        private static readonly Color clearColor = Color.White;

        private Color defaultDrawingColor = Color.Black;

        private VisualizationParametersBuilder visualizationParametersBuilder = new VisualizationParametersBuilder();

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default color of the drawing.
        /// </summary>
        /// <value>The default color of the drawing.</value>
        public Color DefaultDrawingColor
        {
            get { return defaultDrawingColor; }
            set { defaultDrawingColor = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the specified pipeline step to the visible world according to the actual world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="visibleWorld">The visible world.</param>
        /// <returns></returns>
        /// <remarks>
        /// For this step the visible world should be an exact copy of the actual world
        /// </remarks>
        public IVisibleWorld Apply(IWorld world, IVisibleWorld visibleWorld)
        {
            currentVisibleWorld = visibleWorld;

            ResetZIndexes();

            ClearPicture(currentVisibleWorld.Picture);

            DrawObjects();

            return visibleWorld;
        }

        #endregion

        #region Private Methods

        private void ResetZIndexes()
        {
            zIndexes = new double[currentVisibleWorld.Picture.Width, currentVisibleWorld.Picture.Height];
            int width = zIndexes.GetLength(0);
            int height = zIndexes.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    zIndexes[i, j] = 1;
                }
            }
        }

        /// <summary>
        /// Clears the picture.
        /// </summary>
        /// <param name="picture">The picture.</param>
        private void ClearPicture(Bitmap picture)
        {
            using (Graphics graphics = Graphics.FromImage(picture))
            {
                graphics.Clear(clearColor);
            }
        }

        private void DrawObjects()
        {
            foreach (GraphicalObject graphicalObject in currentVisibleWorld.GraphicalObjects)
            {
                currentObject = graphicalObject;

                //i.e. the objects is completely outside the visible volume
                if (currentObject.MeshVertices.IsNullOrEmpty())
                {
                    continue;
                }

                foreach (Plane plane in graphicalObject.Planes)
                {
                    currentPlane = plane;

                    DrawPlane();
                }
            }
        }

        /// <summary>
        /// Draws the plane.
        /// </summary>
        private void DrawPlane()
        {
            //i.e. the given plane is completely outside the visible volume
            //The minimum number of edges to draw a polygon is 3.
            if (currentPlane.PlaneVertices == null || currentPlane.PlaneVertices.Count < 3)
            {
                return;
            }

            //fill plane edges
            edgeBuilder.MeshVertices = currentObject.MeshVertices;
            currentPlaneEdges = edgeBuilder.GetPlaneEdges(currentPlane);

            List<double> recalculationYCoordinates = GetRecalculationYCoordinates();

            for (int i = 0; i < recalculationYCoordinates.Count - 1; i++)
            {
                DrawVerticalRegion(recalculationYCoordinates[i], recalculationYCoordinates[i + 1]);
            }
        }

        /// <summary>
        /// Gets the recalculation Y coordinates 
        /// (i.e. y coordinates, 
        /// when either the left edge or the right edge of the current plane change, so
        /// for which texture parameters and parameters' increments should be recalculated).
        /// </summary>
        /// <returns></returns>
        private List<double> GetRecalculationYCoordinates()
        {
            List<double> recalculationPoints =
                currentPlane.MeshVertices.
                    Select(affinePoint => affinePoint.Y).
                    Distinct().
                    ToList();

            recalculationPoints.Sort();

            return recalculationPoints;
        }

        /// <summary>
        /// Draws the vertical region of the plane, whose boundaries do not contain edge changes.
        /// </summary>
        /// <param name="bottomYCoordinate">The bottom Y coordinate.</param>
        /// <param name="topYCoordinate">The top Y coordinate.</param>
        private void DrawVerticalRegion(double bottomYCoordinate, double topYCoordinate)
        {
            //We use the middle coordinate to calculate the crossed edges, 
            //as we may not cross them for any of the boundary points due to finite calculations precision.

            //Also, consider a rectangular region. If we searched for the crossed edges by the bottom coordinate,
            //we could select a horizontal edge as one of the crossed ones, which is inappropriate
            double middleCoordinate = (bottomYCoordinate + topYCoordinate) / 2;

            List<PlaneEdge> crossedEdges = GetCrossedEdges(middleCoordinate);

            PlaneEdge leftEdge;
            PlaneEdge rightEdge;

            AssignRightAndLeftEdges(crossedEdges, out rightEdge, out leftEdge);

            DrawVerticalRegion(leftEdge, rightEdge, bottomYCoordinate, topYCoordinate);
        }

        /// <summary>
        /// Draws the vertical region of the plane, whose boundaries do not contain edge changes.
        /// </summary>
        /// <param name="leftEdge">The left edge.</param>
        /// <param name="rightEdge">The right edge.</param>
        /// <param name="bottomYCoordinate">The bottom Y coordinate.</param>
        /// <param name="topYCoordinate">The top Y coordinate.</param>
        private void DrawVerticalRegion(PlaneEdge leftEdge, PlaneEdge rightEdge, double bottomYCoordinate, double topYCoordinate)
        {
            visualizationParametersBuilder.TargetObject = currentObject;

            //Find left edge start and end parameters, parameters increment
            TextureVisualizationParameters leftParameters =
                visualizationParametersBuilder.GetStartingVisualizationParameters(leftEdge, bottomYCoordinate);

            TextureVisualizationParameters leftParametersIncrement =
                visualizationParametersBuilder.GetVisualizationParametersIncrement(leftEdge);

            //Find right edge starting parameters, parameters increment
            TextureVisualizationParameters rightParameters =
                visualizationParametersBuilder.GetStartingVisualizationParameters(rightEdge, bottomYCoordinate);

            TextureVisualizationParameters rightParametersIncrement =
                visualizationParametersBuilder.GetVisualizationParametersIncrement(rightEdge);

            int numberOfRowsToDraw = (int)topYCoordinate - (int)bottomYCoordinate + 1;

            for (int i = 0; i < numberOfRowsToDraw; i++)
            {
                DrawHorizontalRow(leftParameters, rightParameters);

                leftParameters.Add(leftParametersIncrement);
                rightParameters.Add(rightParametersIncrement);
            }
        }

        /// <summary>
        /// Assigns the right and left edges.
        /// </summary>
        /// <param name="crossedPlaneEdges">The crossed plane edges.</param>
        /// <param name="rightEdge">The right edge.</param>
        /// <param name="leftEdge">The left edge.</param>
        /// <remarks>
        /// out modifier is redundant, as PlaneEdge is a reference object, 
        /// but it prevents warnings and indicates which values will be assigned.
        /// </remarks>
        private void AssignRightAndLeftEdges(IList<PlaneEdge> crossedPlaneEdges, out PlaneEdge rightEdge, out PlaneEdge leftEdge)
        {
            //as far as we deal with convex polygons, 
            //any point of the right edge should be to the right of any point of the left edge
            //so we can compare any pair of the points

            PlaneEdge firstEdge = crossedPlaneEdges[0];
            PlaneEdge secondEdge = crossedPlaneEdges[1];

            double firstEdgeXCoordinate = currentObject.MeshVertices[firstEdge.Start.VertexIndex].X;
            double secondEdgeXCoordinate = currentObject.MeshVertices[secondEdge.Start.VertexIndex].X;

            if (firstEdgeXCoordinate <= secondEdgeXCoordinate)
            {
                leftEdge = firstEdge;
                rightEdge = secondEdge;
            }
            else
            {
                rightEdge = firstEdge;
                leftEdge = secondEdge;
            }
        }

        /// <summary>
        /// Gets the edges, crossed by the current vertical line.
        /// As far as all planes in the world are convex, any line can cross no more than 2 edges
        /// (in some exceptional cases 3, when the line coincides with the horizontal edge and crosses 2 neighbouring edges,
        /// but such cases are not supported and should be avoided).
        /// </summary>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <returns></returns>
        private List<PlaneEdge> GetCrossedEdges(double yCoordinate)
        {
            List<PlaneEdge> crossedEdges = new List<PlaneEdge>();
            foreach (PlaneEdge edge in currentPlaneEdges)
            {
                AffinePoint startPoint = currentObject.MeshVertices[edge.Start.VertexIndex];
                AffinePoint endPoint = currentObject.MeshVertices[edge.End.VertexIndex];

                bool edgeCrossesHorizontalLine = (startPoint.Y <= yCoordinate && endPoint.Y >= yCoordinate) ||
                                                 (startPoint.Y >= yCoordinate && endPoint.Y <= yCoordinate);

                if (edgeCrossesHorizontalLine)
                {
                    crossedEdges.Add(edge);
                }

                //as far as the plane is a convex figure, the horizontal line may not cross more than 2 edges
                if (crossedEdges.Count == 2)
                {
                    break;
                }
            }

            return crossedEdges;
        }

        /// <summary>
        /// Draws the horizontal row.
        /// </summary>
        /// <param name="leftParameters">The left parameters.</param>
        /// <param name="rightParameters">The right parameters.</param>
        private void DrawHorizontalRow(TextureVisualizationParameters leftParameters, TextureVisualizationParameters rightParameters)
        {
            double length = rightParameters.PreciseX - leftParameters.PreciseX;

            TextureVisualizationParameters horizontalIncrement = (rightParameters - leftParameters) / length;

            int numberOfPixels = rightParameters.X - leftParameters.X + 1;

            TextureVisualizationParameters currentPixelParameters = (TextureVisualizationParameters)leftParameters.Clone();

            for(int i=0;i<numberOfPixels;i++)
            {
                DrawPixel(currentPixelParameters);

                currentPixelParameters.Add(horizontalIncrement);
            }
        }

        /// <summary>
        /// Draws the pixel.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        private void DrawPixel(TextureVisualizationParameters pixelParameters)
        {
            //This may happen due to round-off and interpolation errors.
            if(!CoordinatesAreCorrect(pixelParameters))
            {
                return;
            }

            //Do not draw pixels, that are under the existing one.
            if (!PixelIsVisible(pixelParameters))
            {
                return;
            }

            Color color = GetPixelColor(pixelParameters);

            bool pixelIsTransparent = color.A == 0;
            if (pixelIsTransparent)
            {
                //do not draw a transparent pixel, and do not change z-indexes
                return;
            }

            //Here the bitmap coordinates standart is needed (see the class comments).
            currentVisibleWorld.Picture.SetPixel(pixelParameters.X, pixelParameters.Y, color);

            zIndexes[pixelParameters.X, pixelParameters.Y] = pixelParameters.ZIndex;
        }

        /// <summary>
        /// Gets the color of the pixel.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        /// <returns></returns>
        private Color GetPixelColor(TextureVisualizationParameters pixelParameters)
        {
            if (currentPlane.Texture == null || currentPlane.Texture.Bitmap == null)
            {
                return defaultDrawingColor;
            }

            return currentPlane.Texture.Bitmap.GetPixel(pixelParameters.TextureCoordinateX,
                                                        pixelParameters.TextureCoordinateY);
        }

        /// <summary>
        /// Checks whether the pixel is visible.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        /// <returns></returns>
        private bool PixelIsVisible(TextureVisualizationParameters pixelParameters)
        {
            bool pixelIsOverExistingPixel = pixelParameters.ZIndex < zIndexes[pixelParameters.X, pixelParameters.Y];

            return pixelIsOverExistingPixel;
        }

        /// <summary>
        /// Checks whether the coordinateses the are correct.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        /// <returns></returns>
        private bool CoordinatesAreCorrect(TextureVisualizationParameters pixelParameters)
        {
            return ViewportCoordinatesAreCorrect(pixelParameters) && TextureCoordinatesAreCorrect(pixelParameters);
        }

        /// <summary>
        /// Checks whether the projected viewport coordinates are correct.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        /// <returns></returns>
        private bool ViewportCoordinatesAreCorrect(TextureVisualizationParameters pixelParameters)
        {
            return pixelParameters.X >= 0 &&
                   pixelParameters.X < zIndexes.GetLength(0) &&
                   pixelParameters.Y >= 0 &&
                   pixelParameters.Y < zIndexes.GetLength(1);
        }

        /// <summary>
        /// Checks whether the texture coordinates are correct.
        /// </summary>
        /// <param name="pixelParameters">The pixel parameters.</param>
        /// <returns></returns>
        private bool TextureCoordinatesAreCorrect(TextureVisualizationParameters pixelParameters)
        {
            if (currentPlane.Texture == null || currentPlane.Texture.Bitmap == null)
            {
                return true;
            }

            return pixelParameters.TextureCoordinateX >= 0 &&
                   pixelParameters.TextureCoordinateX < currentPlane.Texture.Bitmap.Width &&
                   pixelParameters.TextureCoordinateY >= 0 &&
                   pixelParameters.TextureCoordinateY < currentPlane.Texture.Bitmap.Height;
        }

        #endregion
    }
}
