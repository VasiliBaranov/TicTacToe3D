using System;
using System.Drawing;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Determines a colour of each vertex in all graphical objects.
    /// </summary>
    /// <remarks>
    /// TODO: Implement
    /// </remarks>
    public class VertexColouringStep : IGraphicsPipelineStep
    {

        #region Fields

        #endregion

        #region Constructors

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

            return visibleWorld;
        }

        #endregion

        #region Private Methods


        #endregion
    }
}
