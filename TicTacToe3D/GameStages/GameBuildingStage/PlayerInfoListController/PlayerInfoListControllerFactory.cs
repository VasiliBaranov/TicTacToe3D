using System.Collections.Generic;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.Interfaces;
using TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController.RestrictionAppliers;

namespace TicTacToe3D.GameStages.GameBuildingStage.PlayerInfoListController
{
    public static class PlayerInfoListControllerFactory
    {
        public static IPlayerInfoListController CreatePlayerInfoCollectionAssistant
            (List<IPlayerInfoCollector> playerInfoCollectors)
        {
            PlayerInfoCollectionController assistant=new PlayerInfoCollectionController(playerInfoCollectors);

            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.PlayerType, 
                                                 new PlayerTypeAndDifficultyRestrictionApplier(playerInfoCollectors));
            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.Side, new SideRestrictionApplier(playerInfoCollectors));
            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.Name, new NameRestrictionApplier(playerInfoCollectors));

            return assistant;
        }
    }
}