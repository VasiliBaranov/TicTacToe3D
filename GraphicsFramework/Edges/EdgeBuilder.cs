using System.Collections.Generic;
using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;
using System.Linq;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents a class for building edges from a plane.
    /// Reqeirepemts:
    /// 1. Plane should be a convex one; 
    /// 2. Plane vertices should be arranged (in any direction) by the angle 
    /// (i.e. if you select a vertex A, then a vertex B, then a normal to the plane,
    /// the angle to the vertex C will be CAB, if you look from the end of the normal).
    /// </summary>
    public class EdgeBuilder : IEdgeBuilder
    {
        #region Fields

        private List<AffinePoint> meshVertices;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the mesh vertices.
        /// </summary>
        /// <value>The mesh vertices.</value>
        /// <remarks>
        /// We use a separate property for mesh vertices, instead of using plane.Parent.MeshVertices, as
        /// parent mesh vertices may be changing dynamically.
        /// </remarks>
        public List<AffinePoint> MeshVertices
        {
            get { return meshVertices; }
            set { meshVertices = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the edges from the plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        public List<Edge> GetEdges(Plane plane)
        {
            List<PlaneEdge> planeEdges = GetPlaneEdges(plane);

            List<Edge> edges = planeEdges.Select<PlaneEdge, Edge>(ConvertEdge).ToList();

            return edges;
        }

        /// <summary>
        /// Gets the plane edges from the plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        public List<PlaneEdge> GetPlaneEdges(Plane plane)
        {
            List<PlaneEdge> edges = new List<PlaneEdge>(plane.PlaneVertices.Count - 1);

            for (int i = 0; i < plane.PlaneVertices.Count - 1; i++)
            {
                PlaneVertex planeVertexStart = plane.PlaneVertices[i];
                PlaneVertex planeVertexEnd = plane.PlaneVertices[i + 1];
                edges.Add(new PlaneEdge{Start = planeVertexStart, End = planeVertexEnd});
            }

            //add the last edge
            PlaneVertex lastVertexStart = plane.PlaneVertices[plane.PlaneVertices.Count - 1];
            PlaneVertex lastVertexEnd = plane.PlaneVertices[0];
            edges.Add(new PlaneEdge { Start = lastVertexStart, End = lastVertexEnd });

            return edges;
        }

        /// <summary>
        /// Converts the plane edge to the affine points edge.
        /// </summary>
        /// <param name="planeEdge">The plane edge.</param>
        /// <returns></returns>
        public Edge ConvertEdge(PlaneEdge planeEdge)
        {
            return GetEdge(planeEdge.Start, planeEdge.End);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the edge.
        /// </summary>
        /// <param name="planeVertexStart">The plane vertex start.</param>
        /// <param name="planeVertexEnd">The plane vertex end.</param>
        /// <returns></returns>
        private Edge GetEdge(PlaneVertex planeVertexStart, PlaneVertex planeVertexEnd)
        {
            AffinePoint start = meshVertices[planeVertexStart.VertexIndex];
            AffinePoint end = meshVertices[planeVertexEnd.VertexIndex];

            return new Edge { Start = start, End = end };
        }

        #endregion
    }
}