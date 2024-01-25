using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Events;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer.GameMasterSteps
{
    /// <summary>
    /// Represents a game master step, provides some basic functionality
    /// </summary>
    public abstract class GameMasterStep : IGameMasterStep
    {
        #region Fields

        private List<IPlayer> _players;
        private List<IGameObserver> _gameObservers;
        private GameInformation _gameInformation;
        private HistoryTracker _historyTracker;
        private ExtendedGameInfo _extendedGameInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an instance of a game master step, using extended game info
        /// </summary>
        /// <param name="extendedGameInfo">Game info to be used</param>
        protected GameMasterStep(ExtendedGameInfo extendedGameInfo)
        {
            _extendedGameInfo = extendedGameInfo;

            _gameInformation = _extendedGameInfo.GameInfo;
            _players = _extendedGameInfo.Players;
            _gameObservers = _extendedGameInfo.GameObservers;
            _historyTracker = extendedGameInfo.HistoryTracker;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets extended game info
        /// </summary>
        protected ExtendedGameInfo ExtendedGameInfo
        {
            get
            {
                return _extendedGameInfo;
            }
            set
            {
                _extendedGameInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets game info (on the basis of extended game info)
        /// </summary>
        protected GameInformation GameInformation
        {
            get
            {
                return _gameInformation;
            }
            set
            {
                _gameInformation = value;
            }
        }

        /// <summary>
        /// Gets or sets players (on the basis of extended game info)
        /// </summary>
        protected List<IPlayer> Players
        {
            get
            {
                return _players;
            }
            set
            {
                _players = value;
            }
        }

        /// <summary>
        /// Gets or sets game observers (on the basis of extended game info)
        /// </summary>
        protected List<IGameObserver> GameObservers
        {
            get
            {
                return _gameObservers;
            }
            set
            {
                _gameObservers = value;
            }
        }

        /// <summary>
        /// Gets or sets history tracker observer (on the basis of extended game info)
        /// </summary>
        protected HistoryTracker HistoryTracker
        {
            get
            {
                return _historyTracker;
            }
            set
            {
                _historyTracker = value;
            }
        }

        #endregion

        #region IGameMasterStep Members

        /// <summary>
        /// Starts game master step operation
        /// </summary>
        public abstract void StartOperation();

        /// <summary>
        /// Subscribes game master step to those participants events, which he is interested in
        /// </summary>
        public virtual void SubscribeToParticipantsEvents()
        {
            foreach (IPlayer player in _players)
            {
                SubscribeToPlayerEvents(player);
            }

            foreach (IGameObserver gameObserver in GameObservers)
            {
                SubscribeToGameObserverEvents(gameObserver);
            }
        }

        /// <summary>
        /// Unsubscribes game master step from participants events
        /// </summary>
        public virtual void UnsubscribeFromParticipantsEvents()
        {
            foreach (IPlayer player in _players)
            {
                UnsubscribeFromPlayerEvents(player);
            }

            foreach (IGameObserver gameObserver in GameObservers)
            {
                UnsubscribeFromGameObserverEvents(gameObserver);
            }
        }

        //next four methods are made empty as most steps do not support these actions

        /// <summary>
        /// Makes game master step handle adding a player
        /// </summary>
        /// <param name="player">Player to be added</param>
        public virtual void AddPlayer(IPlayer player)
        {

        }

        /// <summary>
        /// Makes game master step handle removing a player
        /// </summary>
        /// <param name="player">Player to be removed</param>
        public virtual void RemovePlayer(IPlayer player)
        {

        }

        /// <summary>
        /// Makes game master step handle adding a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be added</param>
        public virtual void AddGameObserver(IGameObserver gameObserver)
        {

        }

        /// <summary>
        /// Makes game master step handle removing a game observer
        /// </summary>
        /// <param name="gameObserver">Game observer to be removed</param>
        public virtual void RemoveGameObserver(IGameObserver gameObserver)
        {

        }

        /// <summary>
        /// Makes game master step handle changing player info list host
        /// </summary>
        /// <param name="playerInfos">Player infos to be shifted</param>
        /// <param name="newPlayer">New player host for these player infos</param>
        public virtual void ChangePlayerObject(List<PlayerInformation> playerInfos, IPlayer newPlayer)
        {
            throw new NotImplementedException("method is not implemented");

            CheckRestrictionsWhenChangingPlayerObject(newPlayer);

            if (!_players.Contains(newPlayer))
            {
                _players.Add(newPlayer);
            }

            foreach (PlayerInformation playerInfo in playerInfos)
            {
                newPlayer.AvailablePlayerInfos.Add(playerInfo);
            }

            RemovePlayerInfosFromPlayers(playerInfos);

            RemoveEmptyPlayers();

            SubscribeToPlayerEvents(newPlayer);

            //call all player events that should be called. And i don't know how to make it 
            //in an independent of the current step way.
        }

        public event EventHandler<GMStepOperationCompletedEventArgs> GMStepOperationCompleted;

        #endregion

        /// <summary>
        /// Occurs when game master step finishes operation
        /// </summary>
        protected virtual void OnGMStepOperationCompleted(GMStepOperationCompletedEventArgs e)
        {
            GMStepOperationCompleted(this, e);
        }

        /// <summary>
        /// Subscribes game master step to player events
        /// </summary>
        /// <param name="player">Player to be subscribed to</param>
        protected abstract void SubscribeToPlayerEvents(IPlayer player);

        /// <summary>
        /// Unsubscribes game master step from player events
        /// </summary>
        /// <param name="player">Player to be unsubscribed from</param>
        protected abstract void UnsubscribeFromPlayerEvents(IPlayer player);

        /// <summary>
        /// Subscribes game master step to game observer events
        /// </summary>
        /// <param name="gameObserver">Game observer to be subscribed to</param>
        protected abstract void SubscribeToGameObserverEvents(IGameObserver gameObserver);

        /// <summary>
        /// Unsubscribes game master step from game observer events
        /// </summary>
        /// <param name="gameObserver">Game observer to be unsubscribed from</param>
        protected abstract void UnsubscribeFromGameObserverEvents(IGameObserver gameObserver);

        /// <summary>
        /// Checks restrictions when player object is changed
        /// </summary>
        /// <param name="player">Player to be checked</param>
        protected virtual void CheckRestrictionsWhenChangingPlayerObject(IPlayer player)
        {


        }

        /// <summary>
        /// Checks restrictions when player is being added
        /// </summary>
        /// <param name="player">Player to be checked</param>
        protected virtual void CheckRestrictionsWhenAddingPlayer(IPlayer player)
        {
            if (Players.Contains(player))
            {
                throw new ArgumentException("player is already in the list of players");
            }
        }

        protected virtual void CheckRestrictionsWhenRemovingPlayer(IPlayer player)
        {
            if (!Players.Contains(player))
            {
                throw new ArgumentException("player isn't present in the list of players");
            }
        }

        protected virtual void CheckRestrictionsWhenAddingGameObserver(IGameObserver gameObserver)
        {
            if (GameObservers.Contains(gameObserver))
            {
                throw new ArgumentException("game observer is already in the list of game observers");
            }
        }

        protected virtual void CheckRestrictionsWhenRemovingGameObserver(IGameObserver gameObserver)
        {
            if (!GameObservers.Contains(gameObserver))
            {
                throw new ArgumentException("game observer isn't present in the list of game observers");
            }
        }

        /// <summary>
        /// Removes all player infos from players' AvailablePlayerInfos lists
        /// </summary>
        /// <param name="playerInfos">Player infos</param>
        private void RemovePlayerInfosFromPlayers(List<PlayerInformation> playerInfos)
        {


        }

        /// <summary>
        /// Removes players with empty available player infos lists from the extended game info
        /// </summary>
        private void RemoveEmptyPlayers()
        {


        }
    }
}