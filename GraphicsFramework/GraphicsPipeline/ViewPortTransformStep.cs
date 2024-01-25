using System;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Transforms the vertices from the canonical view volume to fit to the original view port size (i.e. the camera near plane).
    /// Also, transforms the pseudo-depth to span from 0 to 1 (instead of -1 to 1).
    /// Preconditions:
    /// 1. Should be applied on the vertices in the canonical view volume (i.e. after the perspective division step).
    /// </summary>
    public class ViewPortTransformStep : IGraphicsPipelineStep
    {
        #region Fields

        private double pictureHeight;
        private double pictureWidth;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether coordinates will be transformed to the bitmap standart.
        /// </summary>
        /// <remarks>
        /// If true, the origin of the coordinates will be in the top left corner of the viewport, 
        /// and that the Y axis will go downwards.
        /// If false, the origin of the coordinates will be in the center of the camera near plane (viewport),
        /// and the Y axis for the will go upwards.
        /// </remarks>
        public bool TransformCoordinatesToTheBitmapStandart
        {
            get; set;
        }

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

            CameraParameters cameraParameters = visibleWorld.Camera.CameraParameters;
            pictureHeight = cameraParameters.NearPlaneSizeAlongY;
            pictureWidth = cameraParameters.NearPlaneSizeAlongX;

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
        private void UpdateGraphicalObject(IGraphicalObject graphicalObject)
        {
            for (int i = 0; i < graphicalObject.MeshVertices.Count; i++)
            {
                if (TransformCoordinatesToTheBitmapStandart)
                {
                    graphicalObject.MeshVertices[i][0] = (1 + graphicalObject.MeshVertices[i][0]) * pictureWidth / 2;
                    graphicalObject.MeshVertices[i][1] = (1 - graphicalObject.MeshVertices[i][1]) * pictureHeight / 2;
                }
                else
                {
                    graphicalObject.MeshVertices[i][0] = graphicalObject.MeshVertices[i][0] * pictureWidth / 2;
                    graphicalObject.MeshVertices[i][1] = graphicalObject.MeshVertices[i][1] * pictureHeight / 2;
                }

                graphicalObject.MeshVertices[i][2] = (graphicalObject.MeshVertices[i][2] + 1) / 2;
            }
        }

        #endregion
    }
}
