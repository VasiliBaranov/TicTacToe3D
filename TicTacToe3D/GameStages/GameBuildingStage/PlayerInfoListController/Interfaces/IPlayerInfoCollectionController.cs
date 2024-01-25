using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces
{
    public interface IPlayerInfoListController
    {
        void ApplyRestrictionsToCollectorsValuesAndLists(IPlayerInfoCollector changedPlayerInfoCollector, 
                                                         PlayerInfoPropertyName changedPropertyName);

        void SetDefaultCollectorsAvailableListsAndProperties();
    }
}