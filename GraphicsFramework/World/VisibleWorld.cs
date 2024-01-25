using System.Drawing;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a visible world, i.e. an objects that will be passed through the whole graphics pipeline
    /// and which is capable of storing all the information needed for each graphics pipeline step.
    /// </summary>
    /// <remarks>
    /// Actually, visible world may not be even connected to the actual world, 
    /// and be constructed from completely different objects (or actual world subclasses),
    /// but as far actual world classes are almost sufficient for the graphics pipeline functioning, i'm using them.
    /// The only place i had to introduce visible world required fields is the PlaneVertex.PerspectiveMultiplier property.
    /// </remarks>
    public class VisibleWorld : World, IVisibleWorld
    {
        #region Fields

        private readonly Bitmap picture;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibleWorld"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public VisibleWorld(int width, int height)
        {
            picture = new Bitmap(width, height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibleWorld"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public VisibleWorld(IWorld world)
            : base(world.Camera, world.GraphicalObjects, world.LightSources)
        {
            int width = (int) world.Camera.CameraParameters.NearPlaneSizeAlongX;
            int height = (int) world.Camera.CameraParameters.NearPlaneSizeAlongY;
            picture = new Bitmap(width, height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the displayed picture.
        /// </summary>
        /// <value>The picture.</value>
        public Bitmap Picture
        {
            get { return picture; }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}