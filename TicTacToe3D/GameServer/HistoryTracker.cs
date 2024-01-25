using System;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameInfo.Interfaces;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer
{
    /// <summary>
    /// Represents a history tracker: game observer that tracks history
    /// </summary>
    public class HistoryTracker: IGameObserver
    {
        private IGameHistory _history;

        #region IGameObserver Members

        /// <summary>
        /// Occurs when game observer confirms the turn
        /// </summary>
        public event EventHandler TurnConfirmed;

        #endregion

        #region IParticipant Members

        /// <summary>
        /// Initiates game participant preparation for the game
        /// </summary>
        /// <param name="gameInfo">Game info to be used while preparation</param>
        public void PrepareForGame(GameInformation gameInfo)
        {
            _history = gameInfo.GameHistory;

            PreparedForGame(this, new EventArgs());
        }

        /// <summary>
        /// Updates game info, known to the participant (this can be made only before game starts)
        /// </summary>
        /// <param name="gameInfo">New game info</param>
        public void UpdateGameInformation(GameInformation gameInfo)
        {
            _history = gameInfo.GameHistory;
        }

        /// <summary>
        /// Starts game 
        /// </summary>
        public void StartGame()
        {
            _history.GameStart = DateTime.Now;
        }

        /// <summary>
        /// Makes game participant modify its game field after the turn 
        /// (is called even if the turn has been made by this very participant)
        /// </summary>
        /// <param name="cell">Cell to modify</param>
        /// <param name="side">Side of the participant</param>
        public void ModifyCell(Cell cell, Side side)
        {
            _history.AddTurn(new Turn(cell, side));

            TurnConfirmed(this, new EventArgs());
        }

        /// <summary>
        /// Makes game participant handle game termination
        /// </summary>
        /// <param name="gameInformation">Game information updated by game master</param>
        public void HandleGameTermination(GameInformation gameInformation)
        {
            _history.GameEnd = DateTime.Now;

            GameTerminationHandled(this, new EventArgs());
        }

        /// <summary>
        /// Occurs when participant finishes its preparation for the game
        /// </summary>
        public event EventHandler PreparedForGame;

        /// <summary>
        /// Occurs when participant successfully handles game termination
        /// </summary>
        public event EventHandler GameTerminationHandled;

        public event EventHandler GameTerminating;

        public event EventHandler LeavingGame;

        #endregion
    }
}
