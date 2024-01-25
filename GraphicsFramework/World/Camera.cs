using System;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a camera in the world. By default its lens is directed opposite to the OZ axis, 
    /// and the lense (not the near plane) is placed at the origin of coordinates.
    /// </summary>
    public class Camera : WorldObject, ICamera
    {
        #region Fields

        private CameraParameters cameraParameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="cameraParameters">The camera parameters.</param>
        public Camera(CameraParameters cameraParameters)
        {
            this.cameraParameters = cameraParameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        {
            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the camera parameters.
        /// </summary>
        /// <value>The camera parameters.</value>
        public CameraParameters CameraParameters
        {
            get { return cameraParameters; }
            set { cameraParameters = value; }
        }

        /// <summary>
        /// Gets the view matrix of the camera, which puts the world origin of coordinates into the camera lens.
        /// </summary>
        /// <value>The view matrix.</value>
        /// <remarks>
        /// View matrix is the inverse of the camera modelling matrix.
        /// That is because if you move camera, than rotate it, it may be seen as if you rotate the world (oppositely),
        /// and move it to the opposite direction.
        /// </remarks>
        public Matrix ViewMatrix
        {
            get
            {
                return InverseModellingMatrix(ModellingMatrix);
            }
        }

        /// <summary>
        /// Gets the projection matrix to transform the volume,
        /// visble by camera, into the canonical camera volume (a unitary one).
        /// </summary>
        /// <remarks>
        /// Don't forget to apply perspective division to return to real proportions.
        /// </remarks>
        /// <value>The projection matrix.</value>
        public Matrix ProjectionMatrix
        {
            get
            {
                return GetProjectionMatrix();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public override object Clone()
        {
            Camera clone = new Camera();
            CopyPropertiesFromThis(clone);
            return clone;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Inverses the modelling matrix.
        /// </summary>
        /// <param name="modellingMatrix">The transformation matrix.</param>
        /// <returns></returns>
        /// <remarks>
        /// modellingMatrix = D * R, where D = displacement matrix, R = rotation matrix.
        ///                      a  b  c          1  0  c       a  b  0
        /// if modellingMatrix = d  e  f than D = 0  1  f,  R = d  e  0
        ///                      0  0  1          0  0  1       0  0  1
        /// inversedMatrix = (1/R) * (1/D);
        /// 1/D = D(-displacement vector) (i.e. move to another direction)
        /// 1/R = R transposed (as rotations are orthonormal operators)
        ///        1  0  -c         a  d  0
        ///  1/D = 0  1  -f,  1/R = b  e  0
        ///        0  0  1          0  0  1
        /// Also notethat, as inversed diplacement matrix is almost unity, the structure of the inversed matrix will be simple:
        /// the rotation part will be the same as in the 1/R; 
        /// but the displacement part (the last column will change in comparison with 1/D).
        /// </remarks>
        private Matrix InverseModellingMatrix(Matrix modellingMatrix)
        {
            Matrix inversedRotationMatrix = modellingMatrix.Transpose();

            //the last row contains the initial displacement vector. Need to remove it, but leave the last "1"
            for(int i = 0; i < 3; i++)
            {
                inversedRotationMatrix[3, i] = 0;
            }

            //One variant
            Vector4D inversedDisplacement = new Vector4D();
            for (int i = 0; i < 4; i++)
            {
                inversedDisplacement[i] = -modellingMatrix[i, 3];
            }

            Vector resultingDisplacement = inversedRotationMatrix * inversedDisplacement;

            for (int i = 0; i < 3; i++)
            {
                inversedRotationMatrix[i, 3] = resultingDisplacement[i];
            }

            return inversedRotationMatrix;

            //Another variant
            /*Vector3D inversedDisplacement = new Vector3D();
            for (int i = 0; i < 3; i++)
            {
                inversedDisplacement[i] = -modellingMatrix[i, 3];
            }

            Matrix inversedDisplacementMatrix = GetTransformationMatrixForMove(inversedDisplacement);

            Matrix inversedMatrix = inversedRotationMatrix * inversedDisplacementMatrix;

            return inversedMatrix;*/

        }

        /// <summary>
        /// Copies the properties from the current instance to the target object.
        /// </summary>
        /// <param name="target">The target.</param>
        protected override void CopyPropertiesFromThis(WorldObject target)
        {
            Camera targetCamera = target as Camera;
            if(targetCamera == null)
            {
                throw new ArgumentException("Target is not camera.");
            }

            base.CopyPropertiesFromThis(target);

            targetCamera.cameraParameters = cameraParameters.Clone() as CameraParameters;
        }

        /// <summary> 
        /// Assigns the default value for the proection matrix.
        /// </summary> 
        private Matrix GetProjectionMatrix()
        {
            //See Hill, p.456
            double far = cameraParameters.DistanceToTheFarPlane;
            double near = cameraParameters.DistanceToTheNearPlane;

            double right = cameraParameters.NearPlaneSizeAlongX / 2;
            double left = -right;

            double top = cameraParameters.NearPlaneSizeAlongY / 2;
            double bottom = -top;

            Matrix projectionMatrix = new Matrix(4, 4);

            projectionMatrix[0, 0] = 2 * near / (right - left);
            projectionMatrix[0, 2] = (right + left) / (right - left);
            projectionMatrix[1, 1] = 2 * near / (top - bottom);
            projectionMatrix[1, 2] = (top + bottom) / (top - bottom);
            projectionMatrix[2, 2] = -(near + far) / (far - near);
            projectionMatrix[2, 3] = -2 * near * far / (far - near);
            projectionMatrix[3, 2] = -1;

            return projectionMatrix;
        }

        #endregion
    }
}
