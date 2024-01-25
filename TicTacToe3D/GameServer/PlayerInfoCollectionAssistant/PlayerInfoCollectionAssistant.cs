using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer
{
    internal class PlayerInfoCollectionAssistant : IPlayerInfoCollectionAssistant
    {
        private List<IPLayerInfoCollector> _playerInfoCollectors=new List<IPLayerInfoCollector>();
        private List<PlayerInformation> _playersInformation = new List<PlayerInformation>();
        private Dictionary<PlayerInfoPropertyName, IRestrictionApplier> _restrictionAplliersRegistry =
            new Dictionary<PlayerInfoPropertyName, IRestrictionApplier>();


        public PlayerInfoCollectionAssistant(List<IPLayerInfoCollector> playerInfoCollectors)
        {
            _playerInfoCollectors = playerInfoCollectors;
        }

        public void RegisterRestrictionApplier(PlayerInfoPropertyName propertyName, IRestrictionApplier correspondingRestrictionApplier)
        {
            if (_restrictionAplliersRegistry.ContainsKey(propertyName))
            {
                _restrictionAplliersRegistry[propertyName] = correspondingRestrictionApplier;
            }
            else
            {
                _restrictionAplliersRegistry.Add(propertyName, correspondingRestrictionApplier);
            }
        }

        public void UnregisterRestrictionApplier(PlayerInfoPropertyName propertyName)
        {
            if(_restrictionAplliersRegistry.ContainsKey(propertyName))
            {
                _restrictionAplliersRegistry.Remove(propertyName);
            }
        }

        private void SubscribeToCollectorsEvents()
        {
            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformationChanged += new EventHandler<PlayerInfoChangedEventArgs>(HandlePlayerInformationChangedEvent);
            }
        }

        private void UnsubscribeFromCollectorsEvents()
        {
            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformationChanged -= new EventHandler<PlayerInfoChangedEventArgs>(HandlePlayerInformationChangedEvent);
            }
        }

        private void SetDefaultCollectorsAvailableListsAndProperties()
        {
            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformation = new PlayerInformation();
            }

            foreach (IRestrictionApplier restrictionApplier in _restrictionAplliersRegistry.Values)
            {
                restrictionApplier.SetDefaultAvailableListsAndPropertyValue();
            }
        }

        private void FillInPlayersInformationList()
        {
            _playersInformation.Clear();

            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                _playersInformation.Add(playerInfoCollector.PlayerInformation);
            }
        }

        private void HandlePlayerInformationChangedEvent(object sender, PlayerInfoChangedEventArgs e)
        {
            IPLayerInfoCollector changedPlayerInfoCollector = sender as IPLayerInfoCollector;
            if (_restrictionAplliersRegistry.ContainsKey(e.ChangedPropertyName))
            {
                IRestrictionApplier correspondingRestrictionApplier = _restrictionAplliersRegistry[e.ChangedPropertyName];
                correspondingRestrictionApplier.ApplyRestrictionsToPropertiesAfterModification(changedPlayerInfoCollector);
            }

            FillInPlayersInformationList();

            PlayersInfoListChanged(this, new EventArgs());
        }

        #region IPlayerInfoCollectorAssistant Members

        public List<PlayerInformation> PlayersInformation
        {
            get
            {
                return _playersInformation;
            }
        }

        public event EventHandler PlayersInfoListChanged;

        public void StartInfoCollection()
        {
            SubscribeToCollectorsEvents();
            SetDefaultCollectorsAvailableListsAndProperties();
            FillInPlayersInformationList();
            PlayersInfoListChanged(this, new EventArgs());

            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.StartInfoCollection();
            }
        }

        public void StopInfoCollection()
        {
            UnsubscribeFromCollectorsEvents();

            foreach (IPLayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.StopInfoCollection();
            }
        }

        #endregion
    }
}
