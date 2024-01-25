using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer
{
    public abstract class RestrictionApplierRemovingDuplicatedProperties<T>:IRestrictionApplier
    {
        private List<IPLayerInfoCollector> _playerInfoCollectors;
        private List<T> _previousPropertyValues;
        private PropertyInfo _property;
        private PlayerInfoPropertyName _propertyName;

        public RestrictionApplierRemovingDuplicatedProperties(List<IPLayerInfoCollector> playerInfoCollectors, PlayerInfoPropertyName propertyName)
        {
            _playerInfoCollectors = playerInfoCollectors;
            _propertyName = propertyName;
            _property = typeof(PlayerInformation).GetProperty(propertyName.ToString());
            _previousPropertyValues = new List<T>();
        }

        #region IRestrictionApplier Members

        public List<IPLayerInfoCollector> PlayerInfoCollectors
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

        public void ApplyRestrictionsToPropertiesAfterModification(IPLayerInfoCollector changedPlayerInfoCollector)
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

        private List<IPLayerInfoCollector> GetPlayerInfoCollectorsWithEqualProperties(IPLayerInfoCollector playerInfoCollectorToCompareTo)
        {
            List<IPLayerInfoCollector> playerInfoCollectorsWithTheSamePropertyValue = new List<IPLayerInfoCollector>();
            object propertyToCompareTo = _property.GetValue(playerInfoCollectorToCompareTo.PlayerInformation, null);
            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
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

        private void AvoidDuplicatePropertyValues(IPLayerInfoCollector changedPlayerInfoCollector)
        {
            int changedCollectorIndex = _playerInfoCollectors.IndexOf(changedPlayerInfoCollector);

            object currentPropertyValueOfTheChangedCollector = _property.GetValue(changedPlayerInfoCollector.PlayerInformation, null);
            object previousPropertyValueOfTheChangedCollector = _previousPropertyValues[changedCollectorIndex];

            if (!currentPropertyValueOfTheChangedCollector.Equals(previousPropertyValueOfTheChangedCollector))
            {
                List<IPLayerInfoCollector> playerInfoCollectorsWithTheSamePropertyValue =
                    GetPlayerInfoCollectorsWithEqualProperties(changedPlayerInfoCollector);

                if (playerInfoCollectorsWithTheSamePropertyValue.Count == 0)
                {
                    return;
                }
                IPLayerInfoCollector playerInfoCollectorToBeCorrected = playerInfoCollectorsWithTheSamePropertyValue[0];
                SetPropertyValue(previousPropertyValueOfTheChangedCollector, playerInfoCollectorToBeCorrected);

                UpdatePlayerInfoLists(playerInfoCollectorToBeCorrected);
                UpdatePlayerInfoLists(changedPlayerInfoCollector);
            }
        }



        private void SetPropertyValue(object propertyValue,
            IPLayerInfoCollector playerInfoCollectorToBeChanged)
        {
            PlayerInformation playerInfoToBeChanged = playerInfoCollectorToBeChanged.PlayerInformation;
            _property.SetValue(playerInfoToBeChanged, propertyValue, null);
            playerInfoCollectorToBeChanged.PlayerInformation = playerInfoToBeChanged;
        }

        private void UpdatePlayerInfoLists(IPLayerInfoCollector changedPlayerInfoCollector)
        {
            int changedCollectorIndex = _playerInfoCollectors.IndexOf(changedPlayerInfoCollector);
            object newPropertyValue = _property.GetValue(changedPlayerInfoCollector.PlayerInformation, null);
            //_property.SetValue(_previousPropertyValues[changedCollectorIndex], newPropertyValue, null);
            _previousPropertyValues[changedCollectorIndex] = (T)newPropertyValue;
        }
    }
}
