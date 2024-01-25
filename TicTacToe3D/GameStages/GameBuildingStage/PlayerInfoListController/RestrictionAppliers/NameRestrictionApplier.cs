using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers
{
    public class NameRestrictionApplier : RestrictionApplierRemovingDuplicatedProperties<string>
    {
        public NameRestrictionApplier(List<IPlayerInfoCollector> playerInfoCollectors)
            : base(playerInfoCollectors, PlayerInfoPropertyName.Name)
        {

        }

        #region IRestrictionApplier Members

        override public void SetDefaultAvailableListsAndPropertyValue()
        {
            int i = 1;

            foreach (IPlayerInfoCollector playerInfoCollector in PlayerInfoCollectors)
            {
                PlayerInformation playerInfo = playerInfoCollector.PlayerInformation;
                playerInfo.Name = "Player " + i;
                playerInfoCollector.PlayerInformation = playerInfo;
                PreviousPropertyValues.Add(playerInfoCollector.PlayerInformation.Name);
                i++;
            }
        }

        #endregion
    }
}