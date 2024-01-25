using System;
using System.Collections.Generic;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// A special enumerator to enumerate through all the turns in the game history 
    /// </summary>
    public /*class*/struct GameHistoryEnumerator:IEnumerator<Turn>
    {
        private IEnumerator<Turn> _turnListEnumerator;
        private bool _disposed;
        private const string _objectDisposedExceptionText = "The object has already been disposed";

        public GameHistoryEnumerator(GameHistory gameHistory)
        {
            if (gameHistory.GameTurns != null)
            {
                _turnListEnumerator = gameHistory.GameTurns.GetEnumerator();
            }
            else
            {
                throw new InvalidOperationException();
            }

            _disposed = false;
        }

        #region IEnumerator<Turn> Members

        public Turn Current
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(_objectDisposedExceptionText);
                }
                return _turnListEnumerator != null ? _turnListEnumerator.Current : null;
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
                return _turnListEnumerator != null ? _turnListEnumerator.Current : null;
            }
        }

        public bool MoveNext()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }
            return _turnListEnumerator != null && _turnListEnumerator.MoveNext();
        }

        public void Reset()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_objectDisposedExceptionText);
            }
            if (_turnListEnumerator != null)
            {
                _turnListEnumerator.Reset();
            }
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
                _turnListEnumerator.Dispose();
            }
            _disposed = true;

        }

        /*~GameHistoryEnumerator()
        {
            Dispose(false);
        }*/
    }
}
