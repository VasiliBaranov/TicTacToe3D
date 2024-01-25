using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Defines methods for applying the specified pipeline step to the visible world according to the actual world.
    /// </summary>
    public interface IGraphicsPipelineStep
    {
        /// <summary>
        /// Applies the specified pipeline step to the visible world according to the actual world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="visibleWorld">The visible world.</param>
        /// <returns></returns>
        IVisibleWorld Apply(IWorld world, IVisibleWorld visibleWorld);
    }
}
