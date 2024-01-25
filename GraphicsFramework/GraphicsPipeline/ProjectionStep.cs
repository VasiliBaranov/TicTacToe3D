using System;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using GraphicsFramework.Core;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Applies a projection matrix for each vertex. Doesn't divide affine point coordinates by the last element.
    /// Preconditions:
    /// 1. Is Usually applied after the modelling-view step.
    /// </summary>
    /// <remarks>
    /// See Hill, p. 456
    /// </remarks>
    public class ProjectionStep : IGraphicsPipelineStep
    {
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

            if (visibleWorld.Camera == null)
            {
                throw new ArgumentNullException("visibleWorld", "World camera is null.");
            }

            Matrix projectionMatrix = visibleWorld.Camera.ProjectionMatrix;

            if (projectionMatrix == null)
            {
                throw new ArgumentNullException("visibleWorld", "World camera projection matrix is null.");
            }

            if(visibleWorld.GraphicalObjects.IsNullOrEmpty())
            {
                return visibleWorld;
            }

            foreach (GraphicalObject graphicalObject in visibleWorld.GraphicalObjects)
            {
                UpdateGraphicalObject(projectionMatrix, graphicalObject);
            }
            return visibleWorld;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the graphical object.
        /// </summary>
        /// <param name="projectionMatrix">The projection matrix.</param>
        /// <param name="graphicalObject">The graphical object.</param>
        private static void UpdateGraphicalObject(Matrix projectionMatrix, IGraphicalObject graphicalObject)
        {
            for (int i = 0; i < graphicalObject.MeshVertices.Count; i++)
            {
                //graphicalObject.MeshVertices[i] = projectionMatrix * graphicalObject.MeshVertices[i];

                //do not perform perspective division. It will be performed separately in the perspective division step.
                graphicalObject.MeshVertices[i] = graphicalObject.MeshVertices[i].MultiplyBy(projectionMatrix, false);
            }
        }

        #endregion
    }
}
