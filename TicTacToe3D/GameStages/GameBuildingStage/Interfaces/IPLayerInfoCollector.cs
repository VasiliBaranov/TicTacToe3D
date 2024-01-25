using System;
using System.Collections.Generic;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.GameBuildingStage.Events;
using TicTacToe3D.GameStages.GameBuildingStage.Interfaces;

namespace TicTacToe3D.GameStages.GameBuildingStage.Interfaces
{
    public interface IPlayerInfoCollector:IInfoCollector
    {
        List<Side> AvailableSides
        {
            get;
            set;
        }

        List<PlayerDifficulty> AvailablePlayerDifficulties
        {
            get;
            set;
        }

        List<PlayerType> AvailablePlayerTypes
        {
            get;
            set;
        }

        PlayerInformation PlayerInformation
        {
            get;
            set;
        }

        event EventHandler<PlayerInfoChangedEventArgs> PlayerInformationChangedByUser;
    }
}