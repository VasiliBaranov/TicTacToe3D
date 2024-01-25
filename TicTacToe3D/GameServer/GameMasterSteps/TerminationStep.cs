using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;

using TicTacToe3D.AI;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.GameMasterSteps;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    public class TerminationStep : GameMasterStep
    {
        #region Fields

        private List<IGameObserver> _gameObserversThatHandledGameTermination;
        private List<IPlayer> _playersThatHandledGameTermination;

        #endregion

        #region Constructors

        public TerminationStep(ExtendedGameInfo extendedgameInfo)
            : base(extendedgameInfo)
        {

        }


        #endregion

        #region IGameMasterStep Members

        public override void StartOperation()
        {
            _gameObserversThatHandledGameTermination = new List<IGameObserver>();
            _playersThatHandledGameTermination = new List<IPlayer>();

            foreach (IPlayer player in Players)
            {
                player.HandleGameTermination(GameInformation);
            }
            foreach (IGameObserver gameObserver in GameObservers)
            {
                gameObserver.HandleGameTermination(GameInformation);
            }
        }

        #endregion

        #region IGameObserver event handling methods

        #region IParticipant event handling methods

        private void HandleObserverHandledGameTerminationEvent(object sender, EventArgs empty)
        {
            IGameObserver gameObserver = sender as IGameObserver;
            if (gameObserver == null)
            {
                return;
            }

            if (_gameObserversThatHandledGameTermination.Contains(gameObserver))
            {
                return;
            }

            _gameObserversThatHandledGameTermination.Add(gameObserver);

            if (CanEndGame())
            {
                EndGame();
            }
        }

        #endregion

        #endregion

        #region IPlayer event handling methods

        #region IParticipant event handling methods

        private void HandlePlayerHandledGameTerminationEvent(object sender, EventArgs empty)
        {
            IPlayer player = sender as IPlayer;
            if (player == null)
            {
                return;
            }

            if (_playersThatHandledGameTermination.Contains(player))
            {
                return;
            }

            _playersThatHandledGameTermination.Add(player);

            if (CanEndGame())
            {
                EndGame();
            }
        }

        #endregion

        #endregion

        protected override void SubscribeToPlayerEvents(IPlayer player)
        {
            player.GameTerminationHandled += HandlePlayerHandledGameTerminationEvent;
        }

        protected override void UnsubscribeFromPlayerEvents(IPlayer player)
        {
            player.GameTerminationHandled -= HandlePlayerHandledGameTerminationEvent;
        }

        protected override void SubscribeToGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.GameTerminationHandled += HandleObserverHandledGameTerminationEvent;
        }

        protected override void UnsubscribeFromGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.GameTerminationHandled -= HandleObserverHandledGameTerminationEvent;
        }

        private bool CanEndGame()
        {
            return (_gameObserversThatHandledGameTermination.Count == GameObservers.Count &&
                    _playersThatHandledGameTermination.Count == Players.Count);
        }

        private void EndGame()
        {
            UpdateGameInformation();

            GMStepOperationCompletedEventArgs eventArgs = new GMStepOperationCompletedEventArgs(null);
            OnGMStepOperationCompleted(eventArgs);
        }

        private void UpdateGameInformation()
        {
            GameInformation gameInfo = ExtendedGameInfo.GameInfo;

            //update winners
            List<PlayerInformation> winners = new List<PlayerInformation>();

            List<Cell> winningCells = new List<Cell>();

            foreach (PlayerInformation playerInfo in gameInfo.Players)
            {
                if (GameAssistant.SideWins(gameInfo.GameField, gameInfo.GameRules, playerInfo.Side, winningCells))
                {
                    winners.Add(playerInfo);
                }
            }

            gameInfo.Winners = winners;

            if (gameInfo.Winners.Count != 0)
            {
                gameInfo.GameStatus = GameStatus.Finished;
            }
            else if (!GameAssistant.HasEmptyCells(gameInfo.GameField))
            {
                gameInfo.GameStatus = GameStatus.NoPlaceAvailable;
            }
            else
            {
                gameInfo.GameStatus = GameStatus.Unfinished;
            }
        }
    }
}