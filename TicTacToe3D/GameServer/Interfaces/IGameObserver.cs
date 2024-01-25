using System;

namespace TicTacToe3D.GameServer.Interfaces
{
    /// <summary>
    /// Represents a game observer
    /// </summary>
    public interface IGameObserver: IParticipant
    {
        #region Events

        /// <summary>
        /// Occurs when game observer confirms the turn
        /// </summary>
        event EventHandler TurnConfirmed;

        #endregion

    }
}