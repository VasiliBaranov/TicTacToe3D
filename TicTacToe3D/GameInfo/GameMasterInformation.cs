using System;
using System.Net;

namespace TicTacToe3D.GameInfo
{
    /// <summary>
    /// Represents game master info
    /// </summary>
    [Serializable]
    public class GameMasterInformation : ICloneable
    {
        private string _name;
        private IPAddress _ipAddress;

        /// <summary>
        /// Gets or sets game master name
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
        /// Gets or sets game master ip address
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

        public GameMasterInformation(string name, IPAddress ipAddress)
        {
            _name = name;
            _ipAddress = ipAddress;
        }

        public GameMasterInformation()
        {
            _name = string.Empty;
            _ipAddress = null;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new GameMasterInformation(_name, _ipAddress);
        }

        #endregion
    }
}
