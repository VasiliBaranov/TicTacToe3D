using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer.Interfaces
{
    /// <summary>
    /// Represents a participant in the game
    /// </summary>
    public interface IParticipant
    {
        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Initiates game participant preparation for the game
        /// </summary>
        /// <param name="gameInfo">Game info to be used while preparation</param>
        void PrepareForGame(GameInformation gameInfo);

        /// <summary>
        /// Updates game info, known to the participant (this can be made only before game starts)
        /// </summary>
        /// <param name="gameInfo">New game info</param>
        void UpdateGameInformation(GameInformation gameInfo);

        /// <summary>
        /// Starts game 
        /// </summary>
        void StartGame();

        /// <summary>
        /// Makes game participant modify its game field after the turn 
        /// (is called even if the turn has been made by this very participant)
        /// </summary>
        /// <param name="cell">Cell to modify</param>
        /// <param name="side">Side of the participant</param>
        void ModifyCell(Cell cell, Side side);

        /// <summary>
        /// Makes game participant handle game termination
        /// </summary>
        /// <param name="gameInformation">Game information updated by game master</param>
        void HandleGameTermination(GameInformation gameInformation);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when participant finishes its preparation for the game
        /// </summary>
        event EventHandler PreparedForGame;

        /// <summary>
        /// Occurs when participant successfully handles game termination
        /// </summary>
        event EventHandler GameTerminationHandled;

        //We implement the next two different events for the following reasons:
        //logically (according to our abstractions) players can either terminate or leave the game.
        //Despite the fact that when a player leaves the game it should be terminated (as 2 players are necessary), 
        //we implement two separate events, as further extension may be performed and more than two players may participate.
        //We implement these events here (not in the IPlayer interface), as game observers should also have an opportunity to
        //leave the game (than it should proceed, except the case when there no more human players--but that's up to a game master)
        //and to terminate the game (especially when it's a human game observer).
        //Probably it would be good for a game master to check if the game terminating participant is a human player 
        //and if it is the game creator.

        /// <summary>
        /// Occurs when participant initiates game terminating
        /// </summary>
        event EventHandler GameTerminating;

        /// <summary>
        /// Occurs when participant leaves the game
        /// </summary>
        event EventHandler LeavingGame;

        #endregion
    }
}