using System;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents the camera parameters.
    /// </summary>
    public class CameraParameters : ICloneable
    {
        #region Fields

        private double distanceToTheNearPlane;
        private double distanceToTheFarPlane;
        private double nearPlaneSizeAlongX;
        private double nearPlaneSizeAlongY;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraParameters"/> class.
        /// </summary>
        /// <param name="distanceToTheNearPlane">The distance to the near plane.</param>
        /// <param name="distanceToTheFarPlane">The distance to the far plane.</param>
        /// <param name="nearPlaneSizeAlongX">The near plane size along X.</param>
        /// <param name="nearPlaneSizeAlongY">The near plane size along Y.</param>
        public CameraParameters(double distanceToTheNearPlane, double distanceToTheFarPlane, double nearPlaneSizeAlongX, double nearPlaneSizeAlongY)
        {
            this.distanceToTheNearPlane = distanceToTheNearPlane;
            this.distanceToTheFarPlane = distanceToTheFarPlane;
            this.nearPlaneSizeAlongX = nearPlaneSizeAlongX;
            this.nearPlaneSizeAlongY = nearPlaneSizeAlongY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraParameters"/> class.
        /// </summary>
        public CameraParameters()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the distance to the near plane.
        /// </summary>
        /// <value>The distance to the near plane.</value>
        public double DistanceToTheNearPlane
        {
            get { return distanceToTheNearPlane; }
            set { distanceToTheNearPlane = value; }
        }

        /// <summary>
        /// Gets or sets the distance to the far plane.
        /// </summary>
        /// <value>The distance to the far plane.</value>
        public double DistanceToTheFarPlane
        {
            get { return distanceToTheFarPlane; }
            set { distanceToTheFarPlane = value; }
        }

        /// <summary>
        /// Gets or sets the near plane size along X.
        /// </summary>
        /// <value>The near plane size along X.</value>
        public double NearPlaneSizeAlongX
        {
            get { return nearPlaneSizeAlongX; }
            set { nearPlaneSizeAlongX = value; }
        }

        /// <summary>
        /// Gets or sets the near plane size along Y.
        /// </summary>
        /// <value>The near plane size along Y.</value>
        public double NearPlaneSizeAlongY
        {
            get { return nearPlaneSizeAlongY; }
            set { nearPlaneSizeAlongY = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            CameraParameters clone =
                new CameraParameters(distanceToTheNearPlane,
                distanceToTheFarPlane,
                nearPlaneSizeAlongX,
                nearPlaneSizeAlongY);

            return clone;
        }

        #endregion
    }
}
