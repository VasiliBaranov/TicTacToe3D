using System;
using System.Collections.Generic;
using System.Drawing;
using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;
using GraphicsFramework.World;

namespace GraphicsFramework.GraphicsPipeline
{
    /// <summary>
    /// Clips the vertices that are outside the canonical visible volume; may add new vertices.
    /// 
    /// Preconditions: 
    /// 1. Should be applied when the camera volume is transformed to the canonical visible volume (after the projection step).
    /// 2. Should be applied before the perspective division step (so that the last coordinates have not been normalized yet).
    /// 2. All planes should be a convex. (TODO: Not Sure, think about.)
    /// 3. Plane vertices should be arranged (in any direction) by the angle 
    /// (i.e. if you select a vertex A, then a vertex B, then a normal to the plane,
    /// the angle to the vertex C will be CAB, if you look from the end of the normal).
    /// 
    /// Postconditions: preconditions 2 and 3 hold.
    /// </summary>
    /// <remarks>
    /// see Hill, p. 456. This is the Liang-Barsky algorithm (with Blinn improvements).
    /// </remarks>
    public class ClippingStep : IGraphicsPipelineStep
    {

        #region Fields

        private readonly IEdgeClipper edgeClipper = new LiangBarskyEdgeClipper();

        private readonly EdgeBuilder edgeBuilder = new EdgeBuilder();

        private GraphicalObject currentObject;

        private Plane currentPlane;

        private const double epsilon = 1e-3;

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
            foreach (GraphicalObject graphicalObject in visibleWorld.GraphicalObjects)
            {
                edgeBuilder.MeshVertices = graphicalObject.MeshVertices;
                currentObject = graphicalObject;

                //reset the vertices
                currentObject.MeshVertices = new List<AffinePoint>();

                foreach (Plane plane in currentObject.Planes)
                {
                    currentPlane = plane;
                    ClipCurrentPlane();
                }
            }

            return visibleWorld;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clips the plane.
        /// </summary>
        private void ClipCurrentPlane()
        {
            List<PlaneEdge> planeEdges = edgeBuilder.GetPlaneEdges(currentPlane);

            //reset the plane vertices
            currentPlane.PlaneVertices = new List<PlaneVertex>();

            foreach (PlaneEdge planeEdge in planeEdges)
            {
                Edge edge = edgeBuilder.ConvertEdge(planeEdge);

                edgeClipper.ClipEdge(edge);
                Edge clippedEdge = edgeClipper.ClippedEdge;
                if (edgeClipper.EdgeCrossesCvv)
                {
                    AddEdge(planeEdge, edge, clippedEdge);
                }
            }
        }

        /// <summary>
        /// Adds the edge to the plane.
        /// </summary>
        private void AddEdge(PlaneEdge initialPlaneEdge, Edge initialEdge, Edge clippedEdge)
        {
            //if the edge start is different from the plane last point
            if (ShouldAddStartPointToPlaneVertices(clippedEdge.Start))
            {
                AddVertex(initialPlaneEdge, initialEdge, clippedEdge.Start);
            }

            //if the edge end is different from the plane first point
            if (ShouldAddEndPointToPlaneVertices(clippedEdge.End))
            {
                AddVertex(initialPlaneEdge, initialEdge, clippedEdge.End);
            }
        }

        /// <summary>
        /// Determines if we should add the start point to the plane vertices,
        /// i.e. if the edge start is different from the plane vertices last point.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <returns></returns>
        private bool ShouldAddStartPointToPlaneVertices(AffinePoint start)
        {
            if (currentPlane.PlaneVertices.Count == 0)
            {
                return true;
            }

            //get last plane point
            PlaneVertex lastPlaneVertex = currentPlane.PlaneVertices[currentPlane.PlaneVertices.Count - 1];
            AffinePoint lastPlanePoint = currentObject.MeshVertices[lastPlaneVertex.VertexIndex];

            //if the edge start is different from the plane last point
            return !lastPlanePoint.AlmostEqual(start);
        }

        /// <summary>
        /// Determines if we should add the end point to the plane vertices,
        /// i.e. if the edge end is different from the plane vertices first point.
        /// </summary>
        /// <param name="end">The start.</param>
        private bool ShouldAddEndPointToPlaneVertices(AffinePoint end)
        {
            if (currentPlane.PlaneVertices.Count == 0)
            {
                return true;
            }

            GraphicalObject graphicalObject = currentPlane.ParentObject;

            //get the first plane point
            PlaneVertex firstPlaneVertex = currentPlane.PlaneVertices[0];
            AffinePoint firstPlanePoint = graphicalObject.MeshVertices[firstPlaneVertex.VertexIndex];

            //if the edge end is different from the plane first point
            return !firstPlanePoint.AlmostEqual(end);
        }

        /// <summary>
        /// Adds the vertex to the plane.
        /// </summary>
        /// <param name="initialPlaneEdge">The initial plane edge.</param>
        /// <param name="vertex">The vertex.</param>
        private void AddVertex(PlaneEdge initialPlaneEdge, Edge initialEdge, AffinePoint vertex)
        {
            int existingVertexIndex =
                currentObject.MeshVertices.FindIndex(meshVertex => meshVertex.AlmostEqual(vertex));

            if (existingVertexIndex == -1)
            {
                //Add vertex to the object
                currentObject.MeshVertices.Add(vertex);
            }

            //Add a plane vertex
            int vertexIndex = existingVertexIndex == -1
                                  ? currentObject.MeshVertices.Count - 1
                                  : existingVertexIndex;
            PlaneVertex planeVertex = CreatePlaneVertex(initialPlaneEdge, initialEdge, vertex, vertexIndex);
            currentPlane.PlaneVertices.Add(planeVertex);
        }

        private PlaneVertex CreatePlaneVertex(PlaneEdge initialPlaneEdge, Edge initialEdge, AffinePoint vertex, int vertexIndex)
        {
            double vertexPositionOnEdge = GetVertexPositionOnEdge(initialEdge, vertex);

            PlaneVertex planeVertex =
                new PlaneVertex
                    {
                        VertexIndex = vertexIndex,
                        TextureCoordinates = InterpolateTextureCoordinates(initialPlaneEdge, vertexPositionOnEdge),
                        Color = InterpolateColor(initialPlaneEdge, vertexPositionOnEdge)
                    };

            return planeVertex;
        }

        private Vector2D InterpolateTextureCoordinates(PlaneEdge initialPlaneEdge, double vertexPositionOnEdge)
        {
            Vector2D startCoordinates = initialPlaneEdge.Start.TextureCoordinates;
            Vector2D endCoordinates = initialPlaneEdge.End.TextureCoordinates;

            if (startCoordinates == null || endCoordinates == null)
            {
                return null;
            }

            return startCoordinates + vertexPositionOnEdge * (endCoordinates - startCoordinates);
        }

        private Color InterpolateColor(PlaneEdge initialPlaneEdge, double vertexPositionOnEdge)
        {
            Color startColor = initialPlaneEdge.Start.Color;
            Color endColor = initialPlaneEdge.End.Color;

            int red = (int)LinearAlgebraExtensions.Interpolate(startColor.R, endColor.R, vertexPositionOnEdge);
            int green = (int)LinearAlgebraExtensions.Interpolate(startColor.G, endColor.G, vertexPositionOnEdge);
            int blue = (int)LinearAlgebraExtensions.Interpolate(startColor.B, endColor.B, vertexPositionOnEdge);
            int alpha = (int)LinearAlgebraExtensions.Interpolate(startColor.A, endColor.A, vertexPositionOnEdge);

            return Color.FromArgb(alpha, red, green, blue);
        }

        private double GetVertexPositionOnEdge(Edge initialEdge, AffinePoint vertex)
        {
            AffinePoint startAffinePoint = initialEdge.Start;
            AffinePoint endAffinePoint = initialEdge.End;

            //Find the first axis, for which the initial edge is not parallel
            int i = -1;
            double length;
            do
            {
                i++;
                length = endAffinePoint[i] - startAffinePoint[i];
            } while (Math.Abs(length) <= epsilon);

            //Compute the relative position of the vertex on this edge by this coordinate
            double vertexPositionOnEdge = (vertex[i] - startAffinePoint[i]) / length;

            return vertexPositionOnEdge;
        }

        #endregion
    }
}
