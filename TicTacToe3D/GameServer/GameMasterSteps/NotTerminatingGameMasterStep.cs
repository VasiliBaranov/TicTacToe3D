using System;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.GameMasterSteps;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    /// <summary>
    /// Represents a game master step, which is incapable of handling game termination
    /// </summary>
    public abstract class NotTerminatingGameMasterStep: GameMasterStep
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of not terminating game master step
        /// </summary>
        /// <param name="extendedGameInfo">Extended game info to be used</param>
        protected NotTerminatingGameMasterStep(ExtendedGameInfo extendedGameInfo)
            : base(extendedGameInfo)
        {

        }
        

        #endregion

        #region IGameMasterStep Members

        #endregion

        #region IParticipant event handling methods

        /// <summary>
        /// Handles game terminating event from all participants
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event args</param>
        private void HandleGameTerminatingEvent(object sender, EventArgs e)
        {
            GMStepOperationCompletedEventArgs eventArgs = 
                new GMStepOperationCompletedEventArgs(new TerminationStep(ExtendedGameInfo));
            OnGMStepOperationCompleted(eventArgs);
        }

        #endregion

        #region IPlayer event handling methods

        ///// <summary>
        ///// Handles game terminating event from all participants
        ///// </summary>
        ///// <param name="sender">Sender of the event</param>
        ///// <param name="e">Event args</param>
        //private void HandlePlayerLeavingGameEvent(object sender, EventArgs e)
        //{
        //    GMStepOperationCompletedEventArgs eventArgs =
        //        new GMStepOperationCompletedEventArgs(new TerminationStep(ExtendedGameInfo));
        //    OnGMStepOperationCompleted(eventArgs);
        //}

        #endregion

        #region IGameObserver event handling methods

        ///// <summary>
        ///// Handles game terminating event from all participants
        ///// </summary>
        ///// <param name="sender">Sender of the event</param>
        ///// <param name="e">Event args</param>
        //private void HandleObserverLeavingGameEvent(object sender, EventArgs e)
        //{
        //    if (HumanParticipantsExist())
        //    {
        //        GMStepOperationCompletedEventArgs eventArgs =
        //            new GMStepOperationCompletedEventArgs(new TerminationStep(ExtendedGameInfo));
        //        OnGMStepOperationCompleted(eventArgs);
        //    }
        //}

        #endregion

        /// <summary>
        /// Makes game master step handle adding a player
        /// </summary>
        /// <param name="player">Player to be added</param>
        protected override void SubscribeToPlayerEvents(IPlayer player)
        {
            player.GameTerminating += HandleGameTerminatingEvent;
        }

        /// <summary>
        /// Makes game master step handle removing a player
        /// </summary>
        /// <param name="player">Player to be removed</param>
        protected override void UnsubscribeFromPlayerEvents(IPlayer player)
        {
            player.GameTerminating -= HandleGameTerminatingEvent;
        }

        /// <summary>
        /// Makes game master step handle adding a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be added</param>
        protected override void SubscribeToGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.GameTerminating += HandleGameTerminatingEvent;
        }

        /// <summary>
        /// Makes game master step handle removing a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be removed</param>
        protected override void UnsubscribeFromGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.GameTerminating -= HandleGameTerminatingEvent;
        }
    }
}