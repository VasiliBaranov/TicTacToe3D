using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.Interfaces
{
    public interface IPlayerInfoListCollector: IInfoCollector
    {
        List<PlayerInformation> PlayerInfoList
        {
            get;
        }

        event EventHandler PlayerInfoListChanged;
    }
}