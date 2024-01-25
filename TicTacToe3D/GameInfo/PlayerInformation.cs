using System;
using System.Net;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents player information
    /// </summary>
    [Serializable]
    public class PlayerInformation : ICloneable
    {
        private string _name;
        private IPAddress _ipAddress;
        private PlayerType _playerType;
        private PlayerDifficulty _difficulty;
        private Side _side;

        /// <summary>
        /// Gets or sets player name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets player ip address
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets player type
        /// </summary>
        public PlayerType PlayerType
        {
            get
            {
                return _playerType;
            }
            set
            {
                _playerType = value;
            }
        }

        /// <summary>
        /// Gets or sets player difficulty
        /// </summary>
        public PlayerDifficulty Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
            }
        }

        /// <summary>
        /// Gets or sets player side
        /// </summary>
        public Side Side
        {
            get
            {
                return _side;
            }
            set
            {
                _side = value;
            }
        }

        public PlayerInformation(string name, IPAddress ipAddress, PlayerType playerType, PlayerDifficulty difficulty, Side side)
        {
            _name = name;
            _ipAddress = ipAddress;
            _playerType = playerType;
            _difficulty = difficulty;
            _side = side;
        }

        public PlayerInformation()
        {
            _name = string.Empty;
            _ipAddress = IPAddress.None;
            _playerType = PlayerType.Computer;
            _difficulty = PlayerDifficulty.Easy;
            _side = Side.O;
        }

        public override bool Equals(object obj)
        {
            PlayerInformation playerInfo = obj as PlayerInformation;
            if (playerInfo == null)
            {
                return false;
            }
            return _side.Equals(playerInfo._side) &&
                   _playerType.Equals(playerInfo._playerType) &&
                   _name.Equals(playerInfo._name) &&
                   _ipAddress.Equals(playerInfo._ipAddress) &&
                   _difficulty.Equals(playerInfo._difficulty);
        }

        public override int GetHashCode()
        {
            return (int)_side ^ (int)_playerType ^ (int)_difficulty;
        }


        #region ICloneable Members

        public object Clone()
        {
            return new PlayerInformation(_name, _ipAddress, _playerType, _difficulty, _side);
        }

        #endregion
    }
}

