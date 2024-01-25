using System;

namespace GraphicsFramework.Core
{
    /// <summary>
    /// Represents an event arguments class which can store a specific item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemEventArgs<T> : EventArgs
    {
        #region Fields

        private T item;

        #endregion

        #region Constructors

        public ItemEventArgs(T item)
        {
            this.item = item;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the item stored.
        /// </summary>
        /// <value>The item.</value>
        public T Item
        {
            get { return item; }
            set { item = value; }
        }

        #endregion
    }
}
