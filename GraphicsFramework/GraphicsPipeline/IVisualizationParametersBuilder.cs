using GraphicsFramework.Edges;

namespace GraphicsFramework.GraphicsPipeline
{
    public interface IVisualizationParametersBuilder
    {
        /// <summary>
        /// Gets the starting visualization parameters for the edge and the starting y coordinate.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <returns></returns>
        TextureVisualizationParameters GetStartingVisualizationParameters(PlaneEdge edge, double yCoordinate);

        /// <summary>
        /// Gets the visualization parameters increment for the edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <returns></returns>
        TextureVisualizationParameters GetVisualizationParametersIncrement(PlaneEdge edge);
    }
}
