using System;
using TicTacToe3D.GameStages.GameLoadingStage.Event;

namespace TicTacToe3D.GameStages.GameLoadingStage.Interfaces
{
    internal interface IGameLoader
    {
        event EventHandler<GameContinuingEventArgs> GameContinuing;

        event EventHandler Exitting;

        event EventHandler GoingBack;
    }
}