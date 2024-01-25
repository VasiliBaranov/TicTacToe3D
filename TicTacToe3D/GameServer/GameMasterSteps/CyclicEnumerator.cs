using System;
using System.Collections.Generic;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    /// <summary>
    /// Supports a cyclic iteration over a generic collection.
    /// Represents a special enumerator, which is based on some other (usually linear) enumerator, 
    /// but when the last one reaches the end of the collection, the cyclic enumerator automatically resets it.
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    public class CyclicEnumerator<T>:IEnumerator<T>
    {
        private bool _disposed;
        private readonly IEnumerator<T> _linearEnumerator;
        private const string _objectDisposedExceptionText = "The object has already been disposed";

        /// <summary>
        /// Initializes a new instance of the cyclic enumerator, based upon some other (linear) enumerator
        /// </summary>
        /// <param name="linearEnumerator">Some other (linear) enumerator to be used</param>
        /// <exception cref="ArgumentNullException">When linear enumerator is null</exception>
        public CyclicEnumerator(IEnumerator<T> linearEnumerator)
        {
            if (linearEnumerator == null)
            {
                throw new ArgumentNullException();
            }
            _linearEnumerator = linearEnumerator;
        }

        #region IEnumerator<T> Members

        public T Current
        {
            get 
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(_objectDisposedExceptionText);
                }
                return _linearEnumerator.Current;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get 
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(_objectDisposedExceptionText);
                }
                return _linearEnumerator.Current; 
            }
        }

        public bool MoveNext()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }

            if (!_linearEnumerator.MoveNext())
            {
                _linearEnumerator.Reset();
                _linearEnumerator.MoveNext();
            }

            return true;
        }

        public void Reset()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }

            _linearEnumerator.Reset();
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether your code has initiated the object's disposal (not the CLR's garbage collector)</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _linearEnumerator.Dispose();
            }
            _disposed = true;

        }

        ~CyclicEnumerator()
        {
            Dispose(false);
        }
    }
}