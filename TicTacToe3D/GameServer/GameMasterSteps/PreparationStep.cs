using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    public class PreparationStep : NotTerminatingGameMasterStep
    {
        #region Fields

        private List<IGameObserver> _gameObserversPreparedForGame;
        private List<IPlayer> _playersPreparedForGame;

        #endregion

        #region Constructors

        public PreparationStep(ExtendedGameInfo extendedgameInfo)
            : base(extendedgameInfo)
        {

        }

        #endregion

        #region IGameMasterStep Members

        public override void StartOperation()
        {
            _gameObserversPreparedForGame = new List<IGameObserver>();
            _playersPreparedForGame = new List<IPlayer>();

            foreach (IPlayer player in Players)
            {
                player.PrepareForGame(GameInformation);
            }

            foreach (IGameObserver gameObserver in GameObservers)
            {
                gameObserver.PrepareForGame(GameInformation);
            }
        }

        //next four methods should be carefully checked

        public override void AddPlayer(IPlayer player)
        {
            CheckRestrictionsWhenAddingPlayer(player);

            Players.Add(player);

            foreach (PlayerInformation playerInfo in player.AvailablePlayerInfos)
            {
                ExtendedGameInfo.PlayersSequence.Add(playerInfo);
            }

            SubscribeToPlayerEvents(player);

            //to ensure further stability
            GameInformation gameInfoCopy = (GameInformation)GameInformation.Clone();

            player.PrepareForGame(gameInfoCopy);
        }

        public override void RemovePlayer(IPlayer player)
        {
            CheckRestrictionsWhenRemovingPlayer(player);

            UnsubscribeFromPlayerEvents(player);

            if (_playersPreparedForGame.Contains(player))
            {
                _playersPreparedForGame.Remove(player);
            }

            Players.Remove(player);
        }

        public override void AddGameObserver(IGameObserver gameObserver)
        {
            CheckRestrictionsWhenAddingGameObserver(gameObserver);

            GameObservers.Add(gameObserver);
            SubscribeToGameObserverEvents(gameObserver);

            //to ensure further stability
            GameInformation gameInfoCopy = (GameInformation)GameInformation.Clone();

            gameObserver.PrepareForGame(gameInfoCopy);
        }

        public override void RemoveGameObserver(IGameObserver gameObserver)
        {
            CheckRestrictionsWhenRemovingGameObserver(gameObserver);

            UnsubscribeFromGameObserverEvents(gameObserver);

            if (_gameObserversPreparedForGame.Contains(gameObserver))
            {
                _gameObserversPreparedForGame.Remove(gameObserver);
            }

            GameObservers.Remove(gameObserver);
        }

        #endregion

        private bool CanMakeTheFirstTurn()
        {
            int numberOfPlayerInfosPrepared = 0;

            foreach (IPlayer player in _playersPreparedForGame)
            {
                numberOfPlayerInfosPrepared += player.AvailablePlayerInfos.Count;
            }

            return
                (_gameObserversPreparedForGame.Count == GameObservers.Count
                 && numberOfPlayerInfosPrepared == 2
                 && _playersPreparedForGame.Count == Players.Count);
        }

        private void MakeTheFirstTurn()
        {
            foreach (IPlayer player in Players)
            {
                player.StartGame();
            }

            foreach (IGameObserver gameObserver in GameObservers)
            {
                gameObserver.StartGame();
            }

            TurnStep turnStep = new TurnStep(ExtendedGameInfo);
            GMStepOperationCompletedEventArgs e = new GMStepOperationCompletedEventArgs(turnStep);
            OnGMStepOperationCompleted(e);
        }

        #region IGameObserver event handling methods

        #region IParticipant event handling methods

        private void HandleObserverPreparedForGameEvent(object sender, EventArgs empty)
        {
            IGameObserver gameObserver = sender as IGameObserver;
            if (gameObserver == null)
            {
                return;
            }
            if (_gameObserversPreparedForGame.Contains(gameObserver))
            {
                return;
            }

            _gameObserversPreparedForGame.Add(gameObserver);

            if (CanMakeTheFirstTurn())
            {
                MakeTheFirstTurn();
            }
        }

        #endregion

        #endregion

        #region IPlayer event handling methods

        private void HandlePlayerInfoChangedEvent(object sender, EventArgs e)
        {
            //rewrite
            IPlayer changedPlayer = sender as IPlayer;
            int indexOfChangedPlayer = Players.IndexOf(changedPlayer);
            GameInformation.Players[indexOfChangedPlayer] = changedPlayer.CurrentPlayerInfo;
            //and here we should check it
            foreach (IPlayer player in Players)
            {
                player.UpdateGameInformation(GameInformation);
            }
            foreach (IGameObserver gameObserver in GameObservers)
            {
                gameObserver.UpdateGameInformation(GameInformation);
            }
        }

        #region IParticipant event handling methods

        private void HandlePlayerPreparedForGameEvent(object sender, EventArgs empty)
        {
            IPlayer player = sender as IPlayer;
            if (player == null)
            {
                return;
            }

            if (_playersPreparedForGame.Contains(player))
            {
                return;
            }

            _playersPreparedForGame.Add(player);

            if (CanMakeTheFirstTurn())
            {
                MakeTheFirstTurn();
            }
        }

        #endregion

        #endregion

        protected override void SubscribeToPlayerEvents(IPlayer player)
        {
            player.PreparedForGame += HandlePlayerPreparedForGameEvent;
            player.AvailablePlayerInfosChanged += HandlePlayerInfoChangedEvent;
            base.SubscribeToPlayerEvents(player);
        }

        protected override void UnsubscribeFromPlayerEvents(IPlayer player)
        {
            player.PreparedForGame -= HandlePlayerPreparedForGameEvent;
            player.AvailablePlayerInfosChanged -= HandlePlayerInfoChangedEvent;
            base.UnsubscribeFromPlayerEvents(player);
        }

        protected override void SubscribeToGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.PreparedForGame += HandleObserverPreparedForGameEvent;
            base.SubscribeToGameObserverEvents(gameObserver);
        }

        protected override void UnsubscribeFromGameObserverEvents(IGameObserver gameObserver)
        {
            gameObserver.PreparedForGame -= HandleObserverPreparedForGameEvent;
            base.UnsubscribeFromGameObserverEvents(gameObserver);
        }
    }
}