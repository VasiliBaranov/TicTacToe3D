using System;
using TicTacToe3D.GameInfo;

namespace TicTacToe3D.GameStages.GameBuildingStage.Interfaces
{
    interface IGameInformationCollector: IInfoCollector
    {
        GameInformation GameInformation
        {
            get;
        }

        event EventHandler GameInfoCollectionFinished;
    }
}