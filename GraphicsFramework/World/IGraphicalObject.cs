using System.Collections.Generic;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a physical object of the world, which may be seen through the camera.
    /// </summary>
    public interface IGraphicalObject : IWorldObject
    {
        /// <summary>
        /// Gets or sets the initial modelling matrix, i.e. the matrix, that applied before the rotations and moves. 
        /// You may specify here object volume changes, deformations, etc, as far as rotations and moves.
        /// By default it is a identity matrix.
        /// </summary>
        /// <value>The initial modelling matrix.</value>
        Matrix InitialModellingMatrix { get; set; }


        /// <summary>
        /// Gets or sets the planes, which comprise the object
        /// </summary>
        /// <value>The planes.</value>
        List<Plane> Planes { get; set; }


        /// <summary>
        /// Gets or sets the mesh vertices of the object.
        /// </summary>
        /// <value>The mesh vertices.</value>
        List<AffinePoint> MeshVertices { get; set; }
    }
}