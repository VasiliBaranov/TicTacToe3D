using System.Collections.Generic;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces
{
    public interface IRestrictionApplier
    {
        List<IPlayerInfoCollector> PlayerInfoCollectors
        {
            get;
        }

        PlayerInfoPropertyName PropertyToApplyRestrictionsTo
        {
            get;
        }

        void SetDefaultAvailableListsAndPropertyValue();

        void ApplyRestrictionsToPropertiesAfterModification(IPlayerInfoCollector changedPlayerInfoCollector);

    }
}