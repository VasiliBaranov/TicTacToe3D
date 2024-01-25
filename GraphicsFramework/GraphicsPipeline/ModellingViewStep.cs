using System;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using GraphicsFramework.Core;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Applies a modelling-view matrix to each graphical object.
    /// </summary>
    public class ModellingViewStep : IGraphicsPipelineStep
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

            Matrix viewMatrix = visibleWorld.Camera.ViewMatrix;

            if (viewMatrix == null)
            {
                throw new ArgumentNullException("visibleWorld", "World camera ViewMatrix is null.");
            }

            if(visibleWorld.GraphicalObjects.IsNullOrEmpty())
            {
                return visibleWorld;
            }

            foreach (GraphicalObject graphicalObject in visibleWorld.GraphicalObjects)
            {
                UpdateGraphicalObject(viewMatrix, graphicalObject);
            }
            return visibleWorld;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the graphical object according to the view matrix and its own modelling matrix.
        /// </summary>
        /// <param name="viewMatrix">The view matrix.</param>
        /// <param name="graphicalObject">The graphical object.</param>
        private void UpdateGraphicalObject(Matrix viewMatrix, GraphicalObject graphicalObject)
        {
            Matrix fullMatrix = GetFullMatrix(viewMatrix, graphicalObject);

            for (int i = 0; i < graphicalObject.MeshVertices.Count; i++)
            {
                graphicalObject.MeshVertices[i] = fullMatrix * graphicalObject.MeshVertices[i];
            }

            foreach (Plane plane in graphicalObject.Planes)
            {
                plane.Normal = fullMatrix * plane.Normal;
            }
        }

        /// <summary>
        /// Gets the full transformation matrix for the graphical object.
        /// </summary>
        /// <param name="viewMatrix">The view matrix.</param>
        /// <param name="graphicalObject">The graphical object.</param>
        /// <returns></returns>
        private Matrix GetFullMatrix(Matrix viewMatrix, GraphicalObject graphicalObject)
        {
            if (graphicalObject.ModellingMatrix == null)
            {
                throw new ArgumentNullException("graphicalObject", "Graphical object modelling matrix is null.");
            }

            if (graphicalObject.InitialModellingMatrix == null)
            {
                throw new ArgumentNullException("graphicalObject", "Graphical object initial modelling matrix is null.");
            }

            Matrix fullMatrix = viewMatrix*
                                graphicalObject.ModellingMatrix*
                                graphicalObject.InitialModellingMatrix;

            return fullMatrix;
        }

        #endregion
    }
}
