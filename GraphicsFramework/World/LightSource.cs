namespace GraphicsFramework.World
{
    /// <summary>
    /// Represents a light source in the physical world.
    /// </summary>
    public class LightSource : WorldObject
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties

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
            LightSource clone = new LightSource();
            CopyPropertiesFromThis(clone);
            return clone;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}