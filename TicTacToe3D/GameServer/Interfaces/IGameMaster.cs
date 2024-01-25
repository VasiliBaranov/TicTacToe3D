using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.Interfaces
{
    /// <summary>
    /// Represents game master
    /// </summary>
    public interface IGameMaster
    {
        #region Properties

        /// <summary>
        /// Gets information about the game
        /// </summary>
        GameInformation GameInfo
        {
            get;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Adds a player to the game
        /// </summary>
        /// <param name="player">Player to be added</param>
        void AddPlayer(IPlayer player);

        /// <summary>
        /// Removes a player from the game
        /// </summary>
        /// <param name="player">Player to be removed</param>
        void RemovePlayer(IPlayer player);

        /// <summary>
        /// Adds a game observer to the game
        /// </summary>
        /// <param name="gameObserver">Game observer to be added</param>
        void AddGameObserver(IGameObserver gameObserver);

        /// <summary>
        /// Removes a game observer from the game
        /// </summary>
        /// <param name="gameObserver">Game observer to be removed</param>
        void RemoveGameObserver(IGameObserver gameObserver);

        /// <summary>
        /// Removes several player infos from existing players and adds these player infos to the 
        /// available player infos list of the newPlayer
        /// </summary>
        /// <param name="playerInfos">Player infos to be shifted</param>
        /// <param name="newPlayer">New player host for these player infos</param>
        void ChangePlayerObject(List<PlayerInformation> playerInfos, IPlayer newPlayer);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the game has ended
        /// </summary>
        event EventHandler<GameEndedEventArgs> GameEnded;

        #endregion
    }
}