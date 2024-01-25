using System.Collections.Generic;
using System.Reflection;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers
{
    public abstract class RestrictionApplierRemovingDuplicatedProperties<T>:IRestrictionApplier
    {
        private List<IPlayerInfoCollector> _playerInfoCollectors;
        private List<T> _previousPropertyValues;
        private readonly PropertyInfo _property;
        private readonly PlayerInfoPropertyName _propertyName;

        protected RestrictionApplierRemovingDuplicatedProperties(List<IPlayerInfoCollector> playerInfoCollectors, PlayerInfoPropertyName propertyName)
        {
            _playerInfoCollectors = playerInfoCollectors;
            _propertyName = propertyName;
            _property = typeof(PlayerInformation).GetProperty(propertyName.ToString());
            _previousPropertyValues = new List<T>();
        }

        #region IRestrictionApplier Members

        public List<IPlayerInfoCollector> PlayerInfoCollectors
        {
            get
            {
                return _playerInfoCollectors;
            }

            protected set
            {
                _playerInfoCollectors = value;
            }
        }

        public PlayerInfoPropertyName PropertyToApplyRestrictionsTo
        {
            get
            {
                return _propertyName;
            }
        }

        abstract public void SetDefaultAvailableListsAndPropertyValue();

        public void ApplyRestrictionsToPropertiesAfterModification(IPlayerInfoCollector changedPlayerInfoCollector)
        {
            AvoidDuplicatePropertyValues(changedPlayerInfoCollector);
        }

        #endregion

        protected List<T> PreviousPropertyValues
        {
            get
            {
                return _previousPropertyValues;
            }

            set
            {
                _previousPropertyValues = value;
            }
        }

        private List<IPlayerInfoCollector> GetPlayerInfoCollectorsWithEqualProperties(IPlayerInfoCollector playerInfoCollectorToCompareTo)
        {
            List<IPlayerInfoCollector> playerInfoCollectorsWithTheSamePropertyValue = new List<IPlayerInfoCollector>();
            object propertyToCompareTo = _property.GetValue(playerInfoCollectorToCompareTo.PlayerInformation, null);
            foreach (IPlayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                if (playerInfoCollector == playerInfoCollectorToCompareTo)
                {
                    continue;
                }
                object currentProperty = _property.GetValue(playerInfoCollector.PlayerInformation, null);

                if (propertyToCompareTo.Equals(currentProperty))
                {
                    playerInfoCollectorsWithTheSamePropertyValue.Add(playerInfoCollector);
                }
            }
            return playerInfoCollectorsWithTheSamePropertyValue;
        }

        private void AvoidDuplicatePropertyValues(IPlayerInfoCollector changedPlayerInfoCollector)
        {
            int changedCollectorIndex = _playerInfoCollectors.IndexOf(changedPlayerInfoCollector);

            object currentPropertyValueOfTheChangedCollector = _property.GetValue(changedPlayerInfoCollector.PlayerInformation, null);
            object previousPropertyValueOfTheChangedCollector = _previousPropertyValues[changedCollectorIndex];

            if (!currentPropertyValueOfTheChangedCollector.Equals(previousPropertyValueOfTheChangedCollector))
            {
                List<IPlayerInfoCollector> playerInfoCollectorsWithTheSamePropertyValue =
                    GetPlayerInfoCollectorsWithEqualProperties(changedPlayerInfoCollector);

                if (playerInfoCollectorsWithTheSamePropertyValue.Count == 0)
                {
                    return;
                }
                IPlayerInfoCollector playerInfoCollectorToBeCorrected = playerInfoCollectorsWithTheSamePropertyValue[0];
                SetPropertyValue(previousPropertyValueOfTheChangedCollector, playerInfoCollectorToBeCorrected);

                UpdatePlayerInfoLists(playerInfoCollectorToBeCorrected);
                UpdatePlayerInfoLists(changedPlayerInfoCollector);
            }
        }



        private void SetPropertyValue(object propertyValue,
                                      IPlayerInfoCollector playerInfoCollectorToBeChanged)
        {
            PlayerInformation playerInfoToBeChanged = playerInfoCollectorToBeChanged.PlayerInformation;
            _property.SetValue(playerInfoToBeChanged, propertyValue, null);
            playerInfoCollectorToBeChanged.PlayerInformation = playerInfoToBeChanged;
        }

        private void UpdatePlayerInfoLists(IPlayerInfoCollector changedPlayerInfoCollector)
        {
            int changedCollectorIndex = _playerInfoCollectors.IndexOf(changedPlayerInfoCollector);
            object newPropertyValue = _property.GetValue(changedPlayerInfoCollector.PlayerInformation, null);
            //_property.SetValue(_previousPropertyValues[changedCollectorIndex], newPropertyValue, null);
            _previousPropertyValues[changedCollectorIndex] = (T)newPropertyValue;
        }
    }
}