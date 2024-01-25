using GraphicsFramework.LinearAlgebra;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents an edge of the affine points
    /// </summary>
    public class Edge : GenericEdge<AffinePoint>
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            Edge clone = new Edge();
            CopyPropertiesFromThis(clone);

            return clone;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}