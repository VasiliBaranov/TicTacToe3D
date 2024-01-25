using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo.Interfaces;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents information about the game
    /// </summary>
    [Serializable]
    public class GameInformation: ICloneable
    {
        private GameMasterInformation _gameMaster;
        private List<PlayerInformation> _players;
        private List<PlayerInformation> _winners;
        private GameStatus _gameStatus;
        private GameType _gameType;
        private IGameHistory _gameHistory;
        private GameRules _gameRules;
        private IGameField _gameField;

        public GameInformation()
        {
            _gameMaster = new GameMasterInformation();
            _players = new List<PlayerInformation>();
            _winners = new List<PlayerInformation>();
            _gameStatus = GameStatus.Unfinished;
            _gameType = GameType.SingleComputer;
        }

        /// <summary>
        /// Gets or sets game master info
        /// </summary>
        public GameMasterInformation GameMaster
        {
            get { return _gameMaster; }
            set { _gameMaster = value; }
        }

        /// <summary>
        /// Gets or sets player info list
        /// </summary>
        public List<PlayerInformation> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        /// <summary>
        /// Gets or sets winning players list
        /// </summary>
        public List<PlayerInformation> Winners
        {
            get { return _winners; }
            set { _winners = value; }
        }

        /// <summary>
        /// Gets or sets game status
        /// </summary>
        public GameStatus GameStatus
        {
            get { return _gameStatus; }
            set { _gameStatus = value; }
        }

        /// <summary>
        /// Gets or sets game type
        /// </summary>
        public GameType GameType
        {
            get { return _gameType; }
            set { _gameType = value; }
        }

        /// <summary>
        /// Gets or sets game history
        /// </summary>
        public IGameHistory GameHistory
        {
            get { return _gameHistory; }
            set { _gameHistory = value; }
        }

        /// <summary>
        /// Gets or sets game rules
        /// </summary>
        public GameRules GameRules
        {
            get { return _gameRules; }
            set { _gameRules = value; }
        }

        /// <summary>
        /// Gets or sets game field
        /// </summary>
        public IGameField GameField
        {
            get { return _gameField; }
            set { _gameField = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            GameInformation gameInfoCopy = new GameInformation();

            gameInfoCopy._gameField = (IGameField)_gameField.Clone();
            gameInfoCopy._gameHistory = (IGameHistory)_gameHistory.Clone();
            gameInfoCopy._gameMaster = (GameMasterInformation)_gameMaster.Clone();
            gameInfoCopy._gameRules = (GameRules)_gameRules.Clone();
            gameInfoCopy._gameStatus = _gameStatus;
            gameInfoCopy._gameType = _gameType;

            foreach (PlayerInformation playerInfo in _players)
            {
                PlayerInformation playerInfoCopy=(PlayerInformation)playerInfo.Clone();
                gameInfoCopy._players.Add(playerInfoCopy);
            }

            foreach (PlayerInformation winner in _winners)
            {
                PlayerInformation winnerCopy = (PlayerInformation)winner.Clone();
                gameInfoCopy._winners.Add(winnerCopy);
            }

            return gameInfoCopy;
        }

        #endregion
    }
}
