using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using System.Linq;

namespace GraphicsFramework.BaseObjects
{
    public class PlaneObject : GraphicalObject
    {
        #region Fields

        private readonly Color color = Color.Black;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public PlaneObject(List<AffinePoint> vertices)
        {
            MeshVertices = vertices;
            AssignPlane();
        }

        #endregion

        #region Private Methods

        /// <summary> 
        /// Initializes and assigns the default Mesh
        /// </summary> 
        private void AssignPlane()
        {
            List<PlaneVertex> planeVertices = MeshVertices.
                Select((meshVertex, index) => new PlaneVertex { Color = color, VertexIndex = index }).
                ToList();

            Plane plane = new Plane
                              {
                                  Normal = new AffinePoint(0, 1, 0),
                                  ParentObject = this,
                                  PlaneVertices = planeVertices
                              };

            Planes = new List<Plane> { plane };
        }

        #endregion
    }
}