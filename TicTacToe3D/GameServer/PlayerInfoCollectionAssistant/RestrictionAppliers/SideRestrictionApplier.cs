using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameServer
{
    public class SideRestrictionApplier : RestrictionApplierRemovingDuplicatedProperties<Side>
    {
        private List<Side> _availableSides;

        public SideRestrictionApplier(List<IPLayerInfoCollector> playerInfoCollectors)
            : base(playerInfoCollectors, PlayerInfoPropertyName.Side)
        {
            _availableSides = new List<Side>();
            _availableSides.Add(Side.X);
            _availableSides.Add(Side.O);
        }

        #region IRestrictionApplier Members

        override public void SetDefaultAvailableListsAndPropertyValue()
        {
            IEnumerator<Side> sideEnumerator = _availableSides.GetEnumerator();
            sideEnumerator.Reset();

            foreach (IPLayerInfoCollector playerInfoCollector in PlayerInfoCollectors)
            {
                playerInfoCollector.AvailableSides = _availableSides;

                PlayerInformation playerInfo = playerInfoCollector.PlayerInformation;
                playerInfo.Side = sideEnumerator.Current;
                playerInfoCollector.PlayerInformation = playerInfo;

                PreviousPropertyValues.Add(sideEnumerator.Current);
                sideEnumerator.MoveNext();
            }
        }

        #endregion
    }
}
