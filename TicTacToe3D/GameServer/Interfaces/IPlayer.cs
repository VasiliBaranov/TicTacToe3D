using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.Interfaces
{
    /// <summary>
    /// Repersents a player, which can use several player informations
    /// </summary>
    public interface IPlayer: IParticipant
    {
        #region Properties

        /// <summary>
        /// Gets or sets list of player infos, which are represented by this player
        /// </summary>
        //This is done, as one player may play for different sides, or one player object may represent several player infos
        //(i.e., one GUI may be used by 2 human players, one vs the other)
        IList<PlayerInformation> AvailablePlayerInfos
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets current player information, which is supported by the player object. 
        /// This item should be from AlailablePlayerInfos list
        /// </summary>
        PlayerInformation CurrentPlayerInfo
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make turn
        /// </summary>
        void MakeTurn();

        /// <summary>
        /// Makes the player rollback last turn
        /// </summary>
        /// <param name="message">Message from the game master with the reasons for rolling back</param>
        void RollbackLastTurn(string message);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the player has made the turn
        /// </summary>
        event EventHandler<TurnMadeEventArgs> TurnMade;

        /// <summary>
        /// Occurs
        /// </summary>
        event EventHandler AvailablePlayerInfosChanged;

        #endregion
    }
}