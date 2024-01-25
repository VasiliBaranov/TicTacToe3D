using System;
using System.Collections.Generic;
using System.Linq;
using GraphicsFramework.Edges;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents a clipper using the Liang-Barsky algorithm.
    /// </summary>
    public class LiangBarskyEdgeClipper : IEdgeClipper
    {
        #region Fields

        private const int cvvPlanesCount = 6;

        private bool edgeCrossesCvv;

        private Edge clippedEdge;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets the clipped edge.
        /// </summary>
        /// <value>The clipped edge.</value>
        public Edge ClippedEdge
        {
            get
            {
                return clippedEdge;
            }
        }

        /// <summary>
        /// Indicates, if at least some part of the current edge is inside the canonical view volume.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// True, if at least some part of the edge is inside the canonical view volume;
        /// false, if the edge is completely outside the cvv.
        /// </returns>
        public bool EdgeCrossesCvv
        {
            get
            {
                return edgeCrossesCvv;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clips the edge, i.e. updates the start and the end points so that they are inside the canonical view volume.
        /// </summary>
        /// <param name="edge">The edge. Start and end points will be left unchanged.
        /// To get thee clipped edge, use the ClippedEdge property.</param>
        public void ClipEdge(Edge edge)
        {
            List<double> startPointBoundaryConditions = GetBoundaryCoordinates(edge.Start);
            List<double> endPointBoundaryConditions = GetBoundaryCoordinates(edge.End);

            //The edge is completely outside the cvv, because both points are in the outer half-space for the same plane,
            //so the edge can not cross the cvv.
            if (PointsAreInTheOuterHalfSpace(startPointBoundaryConditions, endPointBoundaryConditions))
            {
                edgeCrossesCvv = false;
                CloneEdge(edge);
                return;
            }

            bool startPointIsOutsideCvv = PointIsOutsideCvv(startPointBoundaryConditions);
            bool endPointIsOutsideCvv = PointIsOutsideCvv(endPointBoundaryConditions);
            //the edge is completely inside the cvv.
            if (!startPointIsOutsideCvv && !endPointIsOutsideCvv)
            {
                edgeCrossesCvv = true;
                CloneEdge(edge);
                return;
            }

            double cvvEnterTime;
            double cvvExitTime;
            edgeCrossesCvv = AssignEnterAndExitTimes(startPointBoundaryConditions, endPointBoundaryConditions,
                                                     out cvvEnterTime, out cvvExitTime);
            if (!edgeCrossesCvv)
            {
                CloneEdge(edge);
                return;
            }

            CloneEdge(edge);
            //update the edge points if necessary
            UpdateEdgePoints(clippedEdge, cvvEnterTime, cvvExitTime);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Assigns the enter and exit times.
        /// </summary>
        /// <param name="startPointBoundaryConditions">The start point boundary conditions.</param>
        /// <param name="endPointBoundaryConditions">The end point boundary conditions.</param>
        /// <param name="cvvEnterTime">The CVV enter time.</param>
        /// <param name="cvvExitTime">The CVV exit time.</param>
        /// <returns>
        /// True, if at least some part of the edge is inside the canonical view volume;
        /// false, if the edge is completely outside the cvv.
        /// </returns>
        private bool AssignEnterAndExitTimes(IList<double> startPointBoundaryConditions, IList<double> endPointBoundaryConditions,
                                             out double cvvEnterTime,
                                             out double cvvExitTime)
        {
            cvvEnterTime = 0;
            cvvExitTime = 1;

            for (int i = 0; i < cvvPlanesCount; i++)
            {
                bool startPointIsOutsideCvvByCurrentPlane = startPointBoundaryConditions[i] < 0;
                bool endPointIsOutsideCvvByCurrentPlane = endPointBoundaryConditions[i] < 0;

                double cvvHitTime;
                if (endPointIsOutsideCvvByCurrentPlane)
                {
                    cvvHitTime = startPointBoundaryConditions[i] /
                                 (startPointBoundaryConditions[i] - endPointBoundaryConditions[i]);
                    cvvExitTime = Math.Min(cvvExitTime, cvvHitTime);
                }
                else if (startPointIsOutsideCvvByCurrentPlane)
                {
                    cvvHitTime = startPointBoundaryConditions[i] /
                                 (startPointBoundaryConditions[i] - endPointBoundaryConditions[i]);
                    cvvEnterTime = Math.Max(cvvEnterTime, cvvHitTime);
                }

                if (cvvEnterTime > cvvExitTime)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Updates the edge starting and ending points by the cvv enter and exit times.
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="cvvEnterTime">The CVV enter time.</param>
        /// <param name="cvvExitTime">The CVV exit time.</param>
        /// <remarks>
        /// Time = 0 represents the initial edge start, time = 1 represents the initial edge end.
        /// Actually, time is just a fraction of the current position to the  initial edge length;
        /// but it is a common term in clipping algorithms.
        /// </remarks>
        private void UpdateEdgePoints(Edge edge, double cvvEnterTime, double cvvExitTime)
        {
            AffinePoint start = edge.Start;
            AffinePoint end = edge.End;

            AffinePoint temp = new AffinePoint();
            if (cvvEnterTime > 0) //cvvEnterTime changed
            {
                for (int i = 0; i < start.Dimensionality; i++)
                {
                    temp[i] = start[i] + cvvEnterTime * (end[i] - start[i]);
                }
            }
            if (cvvExitTime < 1) //cvvExitTime changed
            {
                for (int i = 0; i < start.Dimensionality; i++)
                {
                    end[i] = start[i] + cvvExitTime * (end[i] - start[i]);
                }
            }
            if (cvvEnterTime > 0) //cvvEnterTime changed
            {
                edge.Start = temp;
            }
        }

        /// <summary>
        /// Determines, whether the point is outside the canonical view volume.
        /// </summary>
        /// <param name="boundaryConditions">The boundary conditions.</param>
        /// <returns>
        /// True, if at least one of the boundary conditions is negative;
        /// if all of the boundary conditions are positive, returns false.</returns>
        private bool PointIsOutsideCvv(IEnumerable<double> boundaryConditions)
        {
            foreach (double boundaryCondition in boundaryConditions)
            {
                if (PointIsInTheOuterHalfSpace(boundaryCondition))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether the points are in the outer half space for at least one plane
        /// (i.e. are outside the cvv by the same plane).
        /// </summary>
        /// <param name="startPointBoundaryConditions">The start point boundary conditions.</param>
        /// <param name="endPointBoundaryConditions">The end point boundary conditions.</param>
        /// <returns></returns>
        /// <example>
        /// Let points have the y coordinates 1.2 and 1.3 erspectively. 
        /// So they are in the outer (for the cvv) half-space of the y = 1 cvv plane.
        /// </example>
        private bool PointsAreInTheOuterHalfSpace(
            IList<double> startPointBoundaryConditions, IList<double> endPointBoundaryConditions)
        {
            for (int i = 0; i < cvvPlanesCount; i++)
            {
                if (PointIsInTheOuterHalfSpace(startPointBoundaryConditions[i]) &&
                    PointIsInTheOuterHalfSpace(endPointBoundaryConditions[i]))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the boundary coordinates list; 
        /// each element shows, whether the point is outside cvv by one plane. 
        /// If the value for the given plane is less than 0, then the point is in the outer half-space.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        private List<double> GetBoundaryCoordinates(AffinePoint point)
        {
            List<double> boundaryConditions = Enumerable.Repeat(0.0, cvvPlanesCount).ToList();

            //x = -1
            boundaryConditions[0] = point.W + point.X;

            //x = 1
            boundaryConditions[1] = point.W - point.X;

            //y = -1
            boundaryConditions[2] = point.W + point.Y;

            //y = 1
            boundaryConditions[3] = point.W - point.Y;

            //z = -1
            boundaryConditions[4] = point.W + point.Z;

            //z = 1
            boundaryConditions[5] = point.W - point.Z;

            return boundaryConditions;
        }

        /// <summary>
        /// Checks whether the point is in the outer half space for the given plane
        /// (i.e. are outside the cvv by the same plane) by the plane bounday condition.
        /// </summary>
        /// <param name="planeBoundaryCondition">The plane boundary condition.</param>
        /// <returns></returns>
        private bool PointIsInTheOuterHalfSpace(double planeBoundaryCondition)
        {
            return planeBoundaryCondition < 0;
        }

        /// <summary>
        /// Clones the edge.
        /// </summary>
        /// <param name="edge">The edge.</param>
        private void CloneEdge(Edge edge)
        {
            clippedEdge = edge.Clone() as Edge;
        }

        #endregion

    }
}