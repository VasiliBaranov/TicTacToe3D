using System;
using System.Collections.Generic;
using System.Collections;
using TicTacToe3D.GameInfo.Interfaces;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game history, i.e. a collection of turns
    /// </summary>
    [Serializable]
    public class GameHistory: IGameHistory
    {
        private DateTime _start;
        private DateTime _end;

        private readonly List<Turn> _gameTurns = new List<Turn>();

        private const string _argumentNullExceptionText = "Argument is null";
        private const string _argumentExceptionText = "the object is of unsupported type";
            

        /// <summary>
        /// List of game turns. Used just by the enumerator
        /// </summary>
        internal List<Turn> GameTurns
        {
            get
            {
                return _gameTurns;
            }
        }


        #region IGameHistory Members

        /// <summary>
        /// Gets or sets game start time and date
        /// </summary>
        public DateTime GameStart
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        /// <summary>
        /// Gets or sets game end time and date
        /// </summary>
        public DateTime GameEnd
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        /// <summary>
        /// Adds a turn to the game history
        /// </summary>
        /// <param name="turn">A turn to be added</param>
        /// <exception cref="ArgumentNullException">Argument is null</exception>
        public void AddTurn(Turn turn)
        {
            if (turn == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }
            _gameTurns.Add(turn);
        }

        /// <summary>
        /// Appends an existing history to the current history
        /// </summary>
        /// <param name="gameHistory">Existing game history</param>
        /// <exception cref="ArgumentException">Argument is of unsupported type</exception>
        /// <exception cref="ArgumentNullException">Argument is null</exception>
        public void AppendHistory(IGameHistory gameHistory)
        {
            if (gameHistory == null)
            {
                throw new ArgumentNullException(_argumentNullExceptionText);
            }
            GameHistory history = gameHistory as GameHistory;

            if (history == null)
            {
                throw new ArgumentException(_argumentExceptionText);
            }

            _gameTurns.AddRange(history._gameTurns);
        }

        #endregion

        #region IEnumerable<Turn> Members

        public IEnumerator<Turn> GetEnumerator()
        {
            return new GameHistoryEnumerator(this);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new GameHistoryEnumerator(this);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            GameHistory gameHistoryCopy = new GameHistory();
            gameHistoryCopy._end = _end;
            gameHistoryCopy._start = _start;

            foreach (Turn turn in _gameTurns)
            {
                Turn turnCopy = (Turn)turn.Clone();
                gameHistoryCopy._gameTurns.Add(turnCopy);
            }

            return gameHistoryCopy;
        }

        #endregion
    }
}
