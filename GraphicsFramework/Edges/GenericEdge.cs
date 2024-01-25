using System;

namespace GraphicsFramework.Edges
{
    /// <summary>
    /// Represents a generic edge (i.e. something that has a start and the end).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericEdge<T> : ICloneable where T : class, ICloneable
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public T Start
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public T End
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public abstract object Clone();

        #endregion

        #region Private Methods

        /// <summary>
        /// Copies the properties from the this instance to the target one.
        /// </summary>
        /// <param name="target">The target.</param>
        protected virtual void CopyPropertiesFromThis(GenericEdge<T> target)
        {
            target.Start = Start.Clone() as T;
            target.End = End.Clone() as T;
        }

        #endregion
    }
}