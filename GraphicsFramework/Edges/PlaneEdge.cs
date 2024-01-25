using GraphicsFramework.World;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents a edge of plane vertices.
    /// </summary>
    public class PlaneEdge : GenericEdge<PlaneVertex>
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
        public override object Clone()
        {
            PlaneEdge clone = new PlaneEdge();
            CopyPropertiesFromThis(clone);

            return clone;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}