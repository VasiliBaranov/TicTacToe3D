using System;
using System.Drawing;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Represents a class for filling the displayed graphics with the visible world picture.
    /// 
    /// Preconditions:
    /// 1. the visible world picture should be ready.
    /// </summary>
    public class GraphicsFillingStep : IGraphicsPipelineStep
    {

        #region Fields

        private readonly Graphics graphics;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsFillingStep"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        public GraphicsFillingStep(Graphics graphics)
        {
            this.graphics = graphics;
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
            if (visibleWorld == null)
            {
                throw new ArgumentNullException("visibleWorld");
            }

            graphics.Clear(Color.White);
            graphics.DrawImage(visibleWorld.Picture, 0, 0);

            return visibleWorld;
        }

        #endregion

        #region Private Methods


        #endregion
    }
}
