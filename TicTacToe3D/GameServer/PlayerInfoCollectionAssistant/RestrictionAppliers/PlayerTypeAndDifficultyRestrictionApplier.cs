using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer
{
    public class PlayerTypeAndDifficultyRestrictionApplier:IRestrictionApplier
    {
        private List<PlayerType> _previousPlayerTypes;

        private List<IPLayerInfoCollector> _playerInfoCollectors;
        private List<PlayerType> _availablePlayerTypes;

        private List<PlayerDifficulty> _availableComputerPlayerDifficulties;
        private List<PlayerDifficulty> _availableHumanPlayerDifficulties;

        public PlayerTypeAndDifficultyRestrictionApplier(List<IPLayerInfoCollector> playerInfoCollectors)
        {
            _playerInfoCollectors = playerInfoCollectors;

            _availableComputerPlayerDifficulties = new List<PlayerDifficulty>();
            _availableHumanPlayerDifficulties = new List<PlayerDifficulty>();
            _availablePlayerTypes = new List<PlayerType>();
            _previousPlayerTypes = new List<PlayerType>();

            _availableComputerPlayerDifficulties.Add(PlayerDifficulty.Easy);
            _availableComputerPlayerDifficulties.Add(PlayerDifficulty.Normal);
            _availableComputerPlayerDifficulties.Add(PlayerDifficulty.Hard);

            _availableHumanPlayerDifficulties.Add(PlayerDifficulty.NotDefined);

            _availablePlayerTypes.Add(PlayerType.Computer);
            _availablePlayerTypes.Add(PlayerType.Human);
            _availablePlayerTypes.Add(PlayerType.NeuralNetwork);
        }

        #region IRestrictionApplier Members

        public List<IPLayerInfoCollector> PlayerInfoCollectors
        {
            get 
            {
                return _playerInfoCollectors;
            }
        }

        public PlayerInfoPropertyName PropertyToApplyRestrictionsTo
        {
            get 
            {
                return PlayerInfoPropertyName.PlayerType;
            }
        }

        public void SetDefaultAvailableListsAndPropertyValue()
        {
            PlayerInformation playerInfo;
            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.AvailablePlayerDifficulties = _availableComputerPlayerDifficulties;
                playerInfoCollector.AvailablePlayerTypes = _availablePlayerTypes;

                playerInfo = playerInfoCollector.PlayerInformation;
                playerInfo.PlayerType = PlayerType.Computer;
                playerInfoCollector.PlayerInformation = playerInfo;

                _previousPlayerTypes.Add(playerInfoCollector.PlayerInformation.PlayerType);
            }

            _playerInfoCollectors[0].AvailablePlayerDifficulties = _availableHumanPlayerDifficulties;
            playerInfo = _playerInfoCollectors[0].PlayerInformation;
            playerInfo.PlayerType = PlayerType.Human;
            playerInfo.Difficulty = PlayerDifficulty.NotDefined;
            _playerInfoCollectors[0].PlayerInformation = playerInfo;
            _previousPlayerTypes[0] = playerInfo.PlayerType;
        }

        public void ApplyRestrictionsToPropertiesAfterModification(IPLayerInfoCollector changedPlayerInfoCollector)
        {
            PlayerInformation playerInfo = changedPlayerInfoCollector.PlayerInformation;
            int changedCollectorIndex = _playerInfoCollectors.IndexOf(changedPlayerInfoCollector);
            
            if (playerInfo.PlayerType.Equals(_previousPlayerTypes[changedCollectorIndex]))
            {
                return;
            }

            if (playerInfo.PlayerType == PlayerType.Human)
            {
                changedPlayerInfoCollector.AvailablePlayerDifficulties = _availableHumanPlayerDifficulties;
                playerInfo.Difficulty = PlayerDifficulty.NotDefined;
                changedPlayerInfoCollector.PlayerInformation = playerInfo;
            }
            else
            {
                changedPlayerInfoCollector.AvailablePlayerDifficulties = _availableComputerPlayerDifficulties;
                playerInfo.Difficulty = PlayerDifficulty.Easy;
                changedPlayerInfoCollector.PlayerInformation = playerInfo;
            }
            _previousPlayerTypes[changedCollectorIndex] = changedPlayerInfoCollector.PlayerInformation.PlayerType;
        }

        #endregion
    }
}
