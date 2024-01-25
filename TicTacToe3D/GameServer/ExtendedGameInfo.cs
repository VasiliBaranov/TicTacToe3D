using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.GameServer
{
    /// <summary>
    /// Represents extended game information (i.e. game info to be used by different game master steps).
    /// Encapsulates knowledge about the game info, players, game observers, history tracker, etc.
    /// </summary>
    public class ExtendedGameInfo//: GameInformation
    {
        private GameInformation _gameInfo;
        private List<IPlayer> _players;
        private List<PlayerInformation> _playersSequence;
        private List<IGameObserver> _gameObservers;
        private HistoryTracker _historyTracker;
        private IGameMaster _gameMaster;

        /// <summary>
        /// Gets or sets game info
        /// </summary>
        public GameInformation GameInfo
        {
            get
            {
                return _gameInfo;
            }
            set
            {
                _gameInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets players
        /// </summary>
        public List<IPlayer> Players
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
        /// Gets or sets sequence of players, which should make turns 
        /// (it should be composed of those player infos, that GameInfo.Players contains)
        /// </summary>
        public List<PlayerInformation> PlayersSequence
        {
            get 
            { 
                return _playersSequence; 
            }
            set 
            { 
                _playersSequence = value; 
            }
        }

        /// <summary>
        /// Gets or sets game observers
        /// </summary>
        public List<IGameObserver> GameObservers
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
        /// Gets or sets history tracker (it's a game observer that tracks history)
        /// </summary>
        public HistoryTracker HistoryTracker
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

        public IGameMaster GameMaster
        {
            get 
            { 
                return _gameMaster; 
            }
            set 
            { 
                _gameMaster = value; 
            }
        }

        /// <summary>
        /// Initializes a new instance of the ExtendedGameInfo using the specified game info
        /// </summary>
        /// <param name="gameInfo">Game info to create an extended game info</param>
        public ExtendedGameInfo(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;

            _players = new List<IPlayer>();
            _gameObservers = new List<IGameObserver>();
            _historyTracker = new HistoryTracker();
            _playersSequence = new List<PlayerInformation>();
        }
    }
}
