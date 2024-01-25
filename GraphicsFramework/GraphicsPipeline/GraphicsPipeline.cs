using System;
using System.Collections.Generic;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Represents a class for applying all the pipeline steps to the visible according to the actual world.
    /// </summary>
    public class GraphicsPipeline : IGraphicsPipeline
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the pipeline steps.
        /// </summary>
        /// <value>The pipeline steps.</value>
        public List<IGraphicsPipelineStep> PipelineSteps
        {
            get; set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies all the specified pipeline steps to the visible world according to the actual world.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="visibleWorld">The visible world.</param>
        /// <returns></returns>
        public void Apply(IVisibleWorld visibleWorld, IWorld world)
        {
            if(PipelineSteps == null)
            {
                throw new InvalidOperationException("There are no pipeline steps in the pipeline.");
            }

            foreach (IGraphicsPipelineStep pipelineStep in PipelineSteps)
            {
                visibleWorld = pipelineStep.Apply(world, visibleWorld);
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}