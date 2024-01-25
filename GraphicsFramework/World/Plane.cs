using System;
using System.Collections.Generic;
using GraphicsFramework.Core;
using GraphicsFramework.LinearAlgebra;
using System.Linq;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a plane in the graphical object.
    /// Requirements:
    /// 1. Plane should be a convex one; 
    /// 2. Plane vertices should be arranged (in any direction) by the angle 
    /// (i.e. if you select a vertex A, then a vertex B, then a normal to the plane,
    /// the angle to the vertex C will be CAB, if you look from the end of the normal).
    /// </summary>
    public class Plane : ICloneable
    {
        #region Constructors

        #endregion

        #region Fields

        private Texture texture;

        private AffinePoint normal;

        private GraphicalObject parentObject;

        private List<PlaneVertex> planeVertices = new List<PlaneVertex>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the texture for the plane. May be null.
        /// </summary>
        /// <value>The texture.</value>
        public Texture Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// Gets or sets the parent object, which contains the plane.
        /// </summary>
        /// <value>The parent object.</value>
        public GraphicalObject ParentObject
        {
            get { return parentObject; }
            set { parentObject = value; }
        }

        /// <summary>
        /// Gets or sets the plane vertices.
        /// </summary>
        /// <value>The plane vertices.</value>
        public List<PlaneVertex> PlaneVertices
        {
            get { return planeVertices; }
            set { planeVertices = value; }
        }

        /// <summary>
        /// Gets the mesh vertices of the parent object, corresponding to the plane vertices.
        /// </summary>
        /// <value>The mesh vertices.</value>
        public List<AffinePoint> MeshVertices
        {
            get
            {
                if(parentObject==null)
                {
                    return new List<AffinePoint>();
                }

                return planeVertices.Select(planeVertex => parentObject.MeshVertices[planeVertex.VertexIndex]).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        /// <value>The normal.</value>
        public AffinePoint Normal
        {
            get { return normal; }
            set { normal = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            Plane clone = new Plane();
            if (texture != null)
            {
                clone.texture = texture.Clone() as Texture;
            }
            if (planeVertices != null)
            {
                clone.planeVertices = planeVertices.DeepClone();
            }
            if (normal != null)
            {
                clone.normal = normal.Clone() as AffinePoint;
            }
            clone.parentObject = parentObject;
            return clone;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}