using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers
{
    public class SideRestrictionApplier : RestrictionApplierRemovingDuplicatedProperties<Side>
    {
        private readonly List<Side> _availableSides;

        public SideRestrictionApplier(List<IPlayerInfoCollector> playerInfoCollectors)
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

            foreach (IPlayerInfoCollector playerInfoCollector in PlayerInfoCollectors)
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