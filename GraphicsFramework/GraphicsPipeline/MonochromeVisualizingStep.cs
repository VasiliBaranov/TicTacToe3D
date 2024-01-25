using System.Drawing;
using GraphicsFramework.World;
using System.Linq;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Fills the visible picture according to textures and plane positions with the given color.
    /// Preconditions: 
    /// 1. Should be applied after the projection step.
    /// 2. Vertex coordinates should be in the traditional convention 
    /// (i.e. the origin of the coordinates is in the viewport center, the y axis goes upwards).
    /// </summary>
    public class MonochromeVisualizingStep : IGraphicsPipelineStep
    {

        #region Fields

        private readonly Brush brush = Brushes.Black;

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        public MonochromeVisualizingStep()
        {

        }

        public MonochromeVisualizingStep(Brush brush)
        {
            this.brush = brush;
        }

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
            Graphics graphics = Graphics.FromImage(visibleWorld.Picture);

            float pictureHeight = visibleWorld.Picture.Height;
            float pictureWidth = visibleWorld.Picture.Width;

            foreach (GraphicalObject graphicalObject in visibleWorld.GraphicalObjects)
            {
                foreach (Plane plane in graphicalObject.Planes)
                {
                    DrawPlane(plane, graphics, pictureHeight, pictureWidth);
                }
            }

            graphics.Save();

            return visibleWorld;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Draws the plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="pictureHeight">Height of the picture.</param>
        /// <param name="pictureWidth">Width of the picture.</param>
        private void DrawPlane(Plane plane, Graphics graphics, float pictureHeight, float pictureWidth)
        {
            //The origin of coordinates for the projected objects is in 
            //the center of the camera near plane, but graphics origin of coordinates is in the top left corner of the plane.
            //And that the Y axis for the graphics goes downwards, while the Y graphics for the camera goes upwards

            float centerHeight = pictureHeight / 2;
            float centerWidth = pictureWidth / 2;

            //on this step each 2 first coordinates of a mesh vertex contain are palne coordinates
            PointF[] points = plane.
                MeshVertices.
                Select(vertex => new PointF(centerWidth + (float) vertex[0], centerHeight - (float) vertex[1])).
                ToArray();

            if(points.Length > 0)
            {
                graphics.FillPolygon(brush, points);
            }
        }

        #endregion
    }
}
