using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController
{
    internal class PlayerInfoCollectionController : IPlayerInfoListController
    {
        private readonly List<IPlayerInfoCollector> _playerInfoCollectors=new List<IPlayerInfoCollector>();
        private readonly Dictionary<PlayerInfoPropertyName, IRestrictionApplier> _restrictionAplliersRegistry =
            new Dictionary<PlayerInfoPropertyName, IRestrictionApplier>();


        public PlayerInfoCollectionController(List<IPlayerInfoCollector> playerInfoCollectors)
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

        #region IPlayerInfoCollectorAssistant Members

        public void ApplyRestrictionsToCollectorsValuesAndLists(IPlayerInfoCollector changedPlayerInfoCollector, 
                                                                PlayerInfoPropertyName changedPropertyName)
        {
            if (_restrictionAplliersRegistry.ContainsKey(changedPropertyName))
            {
                IRestrictionApplier correspondingRestrictionApplier = _restrictionAplliersRegistry[changedPropertyName];
                correspondingRestrictionApplier.ApplyRestrictionsToPropertiesAfterModification(changedPlayerInfoCollector);
            }
        }

        public void SetDefaultCollectorsAvailableListsAndProperties()
        {
            foreach (IPlayerInfoCollector playerInfoCollector in _playerInfoCollectors)
            {
                playerInfoCollector.PlayerInformation = new PlayerInformation();
            }

            foreach (IRestrictionApplier restrictionApplier in _restrictionAplliersRegistry.Values)
            {
                restrictionApplier.SetDefaultAvailableListsAndPropertyValue();
            }
        }

        #endregion
    }
}