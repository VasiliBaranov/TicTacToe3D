using GraphicsFramework.Edges;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Defines methods and properties for clipping edges by the canonical view volume (i.e. a cube from -1 to 1 by all axes).
    /// </summary>
    public interface IEdgeClipper
    {
        /// <summary>
        /// Clips the edge, i.e. updates the start and the end points so that they are inside the canonical view volume.
        /// </summary>
        /// <param name="edge">The edge. Start and end points will be left unchanged. 
        /// To get thee clipped edge, use the ClippedEdge property.</param>
        void ClipEdge(Edge edge);

        /// <summary>
        /// Gets the clipped edge.
        /// </summary>
        /// <value>The clipped edge.</value>
        Edge ClippedEdge { get; }

        /// <summary>
        /// Indicates, if at least some part of the current edge is inside the canonical view volume.
        /// </summary>
        /// <returns>
        /// True, if at least some part of the edge is inside the canonical view volume;
        /// false, if the edge is completely outside the cvv.
        /// </returns>
        bool EdgeCrossesCvv { get; }
    }
}