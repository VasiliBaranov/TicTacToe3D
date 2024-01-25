using System;

namespace GraphicsFramework.LinearAlgebra
{
    /// <summary>
    /// Represents the direction of any vector by the angles of directional cosines.
    /// </summary>
    public class Direction
    {
        #region Fields

        private Angle angleX;
        private Angle angleY;
        private Angle angleZ;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the directional angle for the X axis.
        /// </summary>
        public Angle AngleX
        {
            get { return angleX; }
            set { angleX = value; }
        }

        /// <summary>
        /// Gets or sets the directional angle for the Y axis.
        /// </summary>
        public Angle AngleY
        {
            get { return angleY; }
            set { angleY = value; }
        }

        /// <summary>
        /// Gets or sets the directional angle for the Z axis.
        /// </summary>
        public Angle AngleZ
        {
            get { return angleZ; }
            set { angleZ = value; }
        }

        /// <summary>
        /// Gets the unit vector along the current direction.
        /// </summary>
        /// <value>The unit vector along direction.</value>
        public Vector3D UnitVectorAlongDirection
        {
            get
            {
                Vector3D unitVector = new Vector3D();
                unitVector[0] = Math.Cos(angleX.Value);
                unitVector[1] = Math.Cos(angleY.Value);
                unitVector[2] = Math.Cos(angleZ.Value);

                return unitVector;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Direction"/> class.
        /// </summary>
        /// <param name="angleX">The angle X.</param>
        /// <param name="angleY">The angle Y.</param>
        /// <param name="angleZ">The angle Z.</param>
        public Direction(Angle angleX, Angle angleY, Angle angleZ)
        {
            this.angleX = angleX;
            this.angleY = angleY;
            this.angleZ = angleZ;
        }

        public Direction()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Direction"/> class, so that it directs along the passed vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public Direction(Vector3D vector)
        {
            double length = vector.Length;

            angleX = new Angle(Math.Acos(vector[0]/length));
            angleY = new Angle(Math.Acos(vector[1]/length));
            angleZ = new Angle(Math.Acos(vector[2]/length));
        }

        /// <summary>
        /// Creates a new direction along the given axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        public Direction(Axis axis)
        {
            angleX = new Angle(Math.PI / 2);
            angleY = new Angle(Math.PI / 2);
            angleZ = new Angle(Math.PI / 2);

            SetAngleForAxis(axis, new Angle(0));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the directional angle of the current direction for the given axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        public Angle GetAngleForAxis(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    return angleX;
                case Axis.Y:
                    return angleY;
                case Axis.Z:
                    return angleZ;
                default:
                    return angleX;
            }
        }

        public void SetAngleForAxis(Axis axis, Angle angle)
        {
            switch (axis)
            {
                case Axis.X:
                    angleX = angle;
                    break;
                case Axis.Y:
                    angleY = angle;
                    break;
                case Axis.Z:
                    angleZ = angle;
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
