using System;
using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    public class VisualizationParametersBuilder : IVisualizationParametersBuilder
    {
        public GraphicalObject TargetObject
        {
            get; set;
        }

        /// <summary>
        /// Gets the starting visualization parameters for the edge and the starting y coordinate.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <returns></returns>
        public TextureVisualizationParameters GetStartingVisualizationParameters(PlaneEdge edge, double yCoordinate)
        {
            TextureVisualizationParameters startParameters = GetVisualizationParameters(edge.Start);
            TextureVisualizationParameters endParameters = GetVisualizationParameters(edge.End);

            double height = endParameters.PreciseY - startParameters.PreciseY;
            TextureVisualizationParameters interpolatedParameters;

            //If the edge is situated so that it takes less than 1 pixel in height 
            //there is impossible to interpolate its parameters by Y
            if (Math.Abs(height) > 1)
            {
                double fraction = (yCoordinate - startParameters.PreciseY) / height;
                interpolatedParameters = startParameters + (endParameters - startParameters) * fraction;
            }
            else
            {
                //or throw exception?
                interpolatedParameters = startParameters;
            }

            return interpolatedParameters;
        }

        /// <summary>
        /// Gets the visualization parameters increment for the edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <returns></returns>
        public TextureVisualizationParameters GetVisualizationParametersIncrement(PlaneEdge edge)
        {
            TextureVisualizationParameters startParameters = GetVisualizationParameters(edge.Start);
            TextureVisualizationParameters endParameters = GetVisualizationParameters(edge.End);

            double height = endParameters.PreciseY - startParameters.PreciseY;

            TextureVisualizationParameters parametersIncrement = (endParameters - startParameters) / height;

            return parametersIncrement;
        }

        /// <summary>
        /// Gets the visualization parameters for the vertex.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <returns></returns>
        private TextureVisualizationParameters GetVisualizationParameters(PlaneVertex vertex)
        {
            TextureVisualizationParameters visualizationParameters = new TextureVisualizationParameters();

            AffinePoint affinePoint = TargetObject.MeshVertices[vertex.VertexIndex];

            visualizationParameters.PreciseX = affinePoint.X;
            visualizationParameters.PreciseY = affinePoint.Y;
            visualizationParameters.ZIndex = affinePoint.Z;

            if (vertex.TextureCoordinates != null)
            {
                visualizationParameters.HorizontalTextureCoordinateNominator =
                    vertex.TextureCoordinates[0] * vertex.PerspectiveMultiplier;
                visualizationParameters.VerticalTextureCoordinateNominator =
                    vertex.TextureCoordinates[1] * vertex.PerspectiveMultiplier;
            }

            visualizationParameters.HorizontalTextureCoordinateDenominator = vertex.PerspectiveMultiplier;
            visualizationParameters.VerticalTextureCoordinateDenominator = vertex.PerspectiveMultiplier;

            return visualizationParameters;
        }
    }
}
