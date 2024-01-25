using System.Collections.Generic;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Defines methods and properties for applying all the pipeline steps to the visible according to the actual world.
    /// </summary>
    public interface IGraphicsPipeline
    {
        /// <summary>
        /// Gets or sets the pipeline steps.
        /// </summary>
        /// <value>The pipeline steps.</value>
        List<IGraphicsPipelineStep> PipelineSteps { get; set; }

        /// <summary>
        /// Applies all the specified pipeline steps to the visible world according to the actual world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="visibleWorld">The visible world.</param>
        /// <returns></returns>
        void Apply(IVisibleWorld visibleWorld, IWorld world);
    }
}
