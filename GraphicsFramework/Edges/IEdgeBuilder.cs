using System.Collections.Generic;
using GraphicsFramework.Edges;
using GraphicsFramework.World;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents an interface for building edges from a plane.
    /// </summary>
    public interface IEdgeBuilder
    {
        /// <summary>
        /// Gets the edges from the plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        List<Edge> GetEdges(Plane plane);

        /// <summary>
        /// Gets the plane edges from the plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns></returns>
        List<PlaneEdge> GetPlaneEdges(Plane plane);

        /// <summary>
        /// Converts the plane edge to the affine points edge.
        /// </summary>
        /// <param name="planeEdge">The plane edge.</param>
        /// <returns></returns>
        Edge ConvertEdge(PlaneEdge planeEdge);
    }
}