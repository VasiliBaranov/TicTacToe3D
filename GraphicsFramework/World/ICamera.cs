using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a camera in the world. By default its lens is directed opposite to the OZ axis, 
    /// and the lense (not the near plane) is placed at the origin of coordinates.
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// Gets or sets the camera parameters.
        /// </summary>
        /// <value>The camera parameters.</value>
        CameraParameters CameraParameters { get; set; }

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        /// <remarks>
        /// View matrix is the inverse of the camera modelling matrix.
        /// That is because if you move camera, than rotate it, it may be seen as if you rotate the world (oppositely),
        /// and move it to the opposite direction.
        /// </remarks>
        Matrix ViewMatrix { get; }

        /// <summary>
        /// Gets the projection matrix to transform the volume,
        /// visble by camera, into the canonical camera volume (a unitary one).
        /// </summary>
        /// <remarks>
        /// Don't forget to apply perspective division to return to real proportions.
        /// </remarks>
        /// <value>The projection matrix.</value>
        Matrix ProjectionMatrix { get; }
    }
}