using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe3D.GameServer
{
    public static class PlayerInfoCollectionAssistantFactory
    {
        public static IPlayerInfoCollectionAssistant CreatePlayerInfoCollectionAssistant
            (List<IPLayerInfoCollector> playerInfoCollectors)
        {
            PlayerInfoCollectionAssistant assistant=new PlayerInfoCollectionAssistant(playerInfoCollectors);

            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.PlayerType, 
                new PlayerTypeAndDifficultyRestrictionApplier(playerInfoCollectors));
            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.Side, new SideRestrictionApplier(playerInfoCollectors));
            assistant.RegisterRestrictionApplier(PlayerInfoPropertyName.Name, new NameRestrictionApplier(playerInfoCollectors));

            return assistant;
        }
    }
}
