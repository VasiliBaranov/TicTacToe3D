using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.Interfaces
{
    /// <summary>
    /// Game Master step
    /// </summary>
    public interface IGameMasterStep
    {
        #region Methods
        /// <summary>
        /// Starts game master step operation
        /// </summary>
        void StartOperation();

        /// <summary>
        /// Makes game master step subscribe to participants events
        /// </summary>
        void SubscribeToParticipantsEvents();

        /// <summary>
        /// Makes game master step unsubscribe from participants events
        /// </summary>
        void UnsubscribeFromParticipantsEvents();

        /// <summary>
        /// Makes game master step handle adding a player
        /// </summary>
        /// <param name="player">Player to be added</param>
        void AddPlayer(IPlayer player);

        /// <summary>
        /// Makes game master step handle removing a player
        /// </summary>
        /// <param name="player">Player to be removed</param>
        void RemovePlayer(IPlayer player);

        /// <summary>
        /// Makes game master step handle adding a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be added</param>
        void AddGameObserver(IGameObserver gameObserver);

        /// <summary>
        /// Makes game master step handle removing a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be removed</param>
        void RemoveGameObserver(IGameObserver gameObserver);

        /// <summary>
        /// Makes game master step handle changing player info list host
        /// </summary>
        /// <param name="playerInfos">Player infos to be shifted</param>
        /// <param name="newPlayer">New player host for these player infos</param>
        void ChangePlayerObject(List<PlayerInformation> playerInfos, IPlayer newPlayer);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when game master step finishes operation
        /// </summary>
        event EventHandler<GMStepOperationCompletedEventArgs> GMStepOperationCompleted;

        #endregion
    }
}