using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.GraphicsPipeline;
using GraphicsFramework.World;

namespace GraphicsFramework
{
    /// <summary>
    /// Represents a graphical engine.
    /// </summary>
    public class GraphicalEngine : IGraphicalEngine
    {

        #region Fields

        private readonly IWorld world;

        private readonly Graphics graphics;

        private IGraphicsPipeline pipeline;

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of a graphical engine.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="world">The world.</param>
        public GraphicalEngine(Graphics graphics, IWorld world)
        {
            this.graphics = graphics;
            this.world = world;

            FillGraphicsPipeLine();
        }

        #endregion

        #region Public Methods

        public void Clear()
        {
            graphics.Clear(Color.White);
        }

        /// <summary>
        /// Draws the world, assigned to the instance.
        /// </summary>
        public void Draw()
        {
            IWorld clone = world.Clone() as IWorld;
            IVisibleWorld visibleWorld = new VisibleWorld(clone);

            pipeline.Apply(visibleWorld, world);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Fills the graphics pipe line.
        /// </summary>
        private void FillGraphicsPipeLine()
        {
            List<IGraphicsPipelineStep> pipelineSteps =
                new List<IGraphicsPipelineStep>
                    {
                        new ModellingViewStep(),
                        new VertexColouringStep(),
                        new ProjectionStep(),
                        new ClippingStep(),
                        new PerspectiveDivisionStep(),
                        //new ViewPortTransformStep(),
                        //new MonochromeVisualizingStep(),
                        new ViewPortTransformStep {TransformCoordinatesToTheBitmapStandart = true},
                        new TextureVisualizingStep(),
                        new GraphicsFillingStep(graphics)
                    };

            pipeline = new GraphicsPipeline.GraphicsPipeline {PipelineSteps = pipelineSteps};
        }

        #endregion
    }
}