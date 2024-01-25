using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe3D.GameServer
{
    public interface IRestrictionApplier
    {
        List<IPlayer> Players
        {
            get;
        }

        PlayerInfoPropertyName PropertyToApplyRestrictionsTo
        {
            get;
        }

        void SetDefaultAvailableListsAndPropertyValue();

        void ApplyRestrictionsToPropertiesAfterModification(IPLayerInfoCollector changedPlayerInfoCollector);

    }
}
