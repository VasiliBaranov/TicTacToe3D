using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer
{
    public class NameRestrictionApplier : RestrictionApplierRemovingDuplicatedProperties<string>
    {
        public NameRestrictionApplier(List<IPLayerInfoCollector> playerInfoCollectors)
            : base(playerInfoCollectors, PlayerInfoPropertyName.Name)
        {

        }

        #region IRestrictionApplier Members

        override public void SetDefaultAvailableListsAndPropertyValue()
        {
            int i = 1;

            foreach (IPLayerInfoCollector playerInfoCollector in PlayerInfoCollectors)
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
