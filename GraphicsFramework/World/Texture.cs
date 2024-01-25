using System;
using System.Drawing;

namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a texture for the graphical object planes.
    /// </summary>
    public class Texture : ICloneable
    {
        #region Fields

        private Bitmap bitmap;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the texture bitmap.
        /// </summary>
        /// <value>The bitmap.</value>
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
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
            Texture clone = new Texture();
            if (bitmap != null)
            {
                clone.bitmap = bitmap.Clone() as Bitmap;
            }
            return clone;
        }

        #endregion
    }
}
