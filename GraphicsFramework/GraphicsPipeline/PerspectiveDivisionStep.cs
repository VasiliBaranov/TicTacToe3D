using System;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Divides spatial and texture coordinates of each vertex by the fourth affine coordinate, 
    /// so that coordinates are in range from -1 to 1 (all the vertices are in the canonical view volume (CVV)).
    /// Sets the perspective multiplier for vertices.
    /// Preconditions:
    /// 1. Should be applied after the projection step (and usually after the clipping step).
    /// Postconditions:
    /// 1. All the vertices are in the canonical view volume (CVV).
    /// </summary>
    public class PerspectiveDivisionStep : IGraphicsPipelineStep
    {
        #region Fields

        private const double epsilon = 1e-4;

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

            if (visibleWorld.Camera == null)
            {
                throw new ArgumentNullException("visibleWorld", "World camera is null.");
            }
            foreach (GraphicalObject graphicalObject in visibleWorld.GraphicalObjects)
            {
                UpdateGraphicalObject(graphicalObject);
            }

            return visibleWorld;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the graphical object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        private  void UpdateGraphicalObject(IGraphicalObject graphicalObject)
        {
            UpdatePlanes(graphicalObject);
            UpdateMeshVertices(graphicalObject);
        }

        /// <summary>
        /// Updates the planes of the object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        private void UpdatePlanes(IGraphicalObject graphicalObject)
        {
            foreach (Plane plane in graphicalObject.Planes)
            {
                foreach (PlaneVertex vertex in plane.PlaneVertices)
                {
                    double denominator = graphicalObject.MeshVertices[vertex.VertexIndex][3];
                    if (Math.Abs(denominator) < epsilon)
                    {
                        continue;
                    }
                    vertex.PerspectiveMultiplier = 1 / denominator;
                }
            }
        }

        /// <summary>
        /// Updates the mesh vertices of the object.
        /// </summary>
        /// <param name="graphicalObject">The graphical object.</param>
        private void UpdateMeshVertices(IGraphicalObject graphicalObject)
        {
            for (int i = 0; i < graphicalObject.MeshVertices.Count; i++)
            {
                double denominator = graphicalObject.MeshVertices[i][3];
                if (Math.Abs(denominator) < epsilon)
                {
                    continue;
                }

                //divide manually, to avoid creation of a separate mesh vertex object while using an operator
                for (int j = 0; j < graphicalObject.MeshVertices[i].Dimensionality; j++)
                {
                    graphicalObject.MeshVertices[i][j] = graphicalObject.MeshVertices[i][j] / denominator;
                }
            }
        }

        #endregion
    }
}
