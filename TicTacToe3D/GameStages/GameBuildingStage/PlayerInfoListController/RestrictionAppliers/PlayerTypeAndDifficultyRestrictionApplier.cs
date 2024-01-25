using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers
{
    public class PlayerTypeAndDifficultyRestrictionApplier:IRestrictionApplier
    {
        private readonly List<PlayerType> _previousPlayerTypes;

        private readonly List<IPlayerInfoCollector> _playerInfoCollectors;
        private readonly List<PlayerType> _availablePlayerTypes;

        private readonly List<PlayerDifficulty> _availableComputerPlayerDifficulties;
        private readonly List<PlayerDifficulty> _availableHumanPlayerDifficulties;

        public PlayerTypeAndDifficultyRestrictionApplier(List<IPlayerInfoCollector> playerInfoCollectors)
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

        public List<IPlayerInfoCollector> PlayerInfoCollectors
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
            foreach (IPlayerInfoCollector playerInfoCollector in _playerInfoCollectors)
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

        public void ApplyRestrictionsToPropertiesAfterModification(IPlayerInfoCollector changedPlayerInfoCollector)
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