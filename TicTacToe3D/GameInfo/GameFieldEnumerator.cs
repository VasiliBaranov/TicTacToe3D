using System;
using System.Collections.Generic;
using System.Collections;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// A special enumerator to enumerate through all the game field cells
    /// </summary>
    public struct /*class*/ GameFieldEnumerator : IEnumerator<Cell>
    {
        private bool _disposed;
        private IEnumerator<Cell> _cellsEnumerator;
        private const string _objectDisposedExceptionText = "The object has already been disposed";

        public GameFieldEnumerator(GameField gameField)
        {
            _cellsEnumerator = gameField.Cells.GetEnumerator();
            _disposed = false;
        }

        #region IEnumerator<Cell> Members

        public Cell Current
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(_objectDisposedExceptionText);
                }
                return _cellsEnumerator.Current;
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(_objectDisposedExceptionText);
                }
                return _cellsEnumerator.Current;
            }
        }

        public bool MoveNext()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }
            return _cellsEnumerator.MoveNext();
        }

        public void Reset()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }
            _cellsEnumerator.Reset();
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
                _cellsEnumerator.Dispose();
            }
            _disposed = true;

        }

        /*~GameFieldEnumerator()
        {
            Dispose(false);
        }*/
    }
}
