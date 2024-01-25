using System;
using System.Collections.Generic;
using GraphicsFramework.Core;
using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a physical object of the world, which may be seen through the camera.
    /// </summary>
    public class GraphicalObject : WorldObject, IGraphicalObject
    {
        #region Fields

        private Matrix initialModellingMatrix = Matrix.GetIdentity(4);

        private List<Plane> planes = new List<Plane>();
        
        private List<AffinePoint> meshVertices = new List<AffinePoint>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the initial modelling matrix, i.e. the matrix, that applied before the rotations and moves.
        /// You may specify here object volume changes, deformations, etc, as far as rotations and moves.
        /// By default it is a identity matrix.
        /// </summary>
        /// <value>The initial modelling matrix.</value>
        public Matrix InitialModellingMatrix
        {
            get { return initialModellingMatrix; }
            set { initialModellingMatrix = value; }
        }

        /// <summary>
        /// Gets or sets the planes, which comprise the object
        /// </summary>
        /// <value>The planes.</value>
        public List<Plane> Planes
        {
            get { return planes; }
            set { planes = value; }
        }

        /// <summary>
        /// Gets or sets the mesh vertices of the object.
        /// </summary>
        /// <value>The mesh vertices.</value>
        public List<AffinePoint> MeshVertices
        {
            get { return meshVertices; }
            set { meshVertices = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalObject"/> class.
        /// </summary>
        /// <param name="initialModellingMatrix">The initial modelling matrix.</param>
        public GraphicalObject(Matrix initialModellingMatrix)
        {
            this.initialModellingMatrix = initialModellingMatrix;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalObject"/> class.
        /// </summary>
        public GraphicalObject()
        {

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
        public override object Clone()
        {
            GraphicalObject clone = new GraphicalObject();
            CopyPropertiesFromThis(clone);
            return clone;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Copies the properties from the current instance to the target object.
        /// </summary>
        /// <param name="target">The target.</param>
        protected override void CopyPropertiesFromThis(WorldObject target)
        {
            GraphicalObject targetGraphicalObject = target as GraphicalObject;

            if(targetGraphicalObject == null)
            {
                throw new ArgumentException("target is not a graphical object.");
            }

            base.CopyPropertiesFromThis(target);
            targetGraphicalObject.initialModellingMatrix = initialModellingMatrix.Clone() as Matrix;
            targetGraphicalObject.planes = planes.DeepClone();
            targetGraphicalObject.meshVertices = meshVertices.DeepClone();

            foreach (Plane plane in targetGraphicalObject.planes)
            {
                plane.ParentObject = targetGraphicalObject;
            }
        }

        #endregion


    }
}
