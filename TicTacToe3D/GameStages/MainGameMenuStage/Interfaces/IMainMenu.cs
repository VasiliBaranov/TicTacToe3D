using System;

namespace TicTacToe3D.GameStages.MainGameMenuStage.Interfaces
{
    internal interface IMainGameMenu
    {
        event EventHandler NewGameStarting;

        event EventHandler GameLoading;

        event EventHandler GameReplayViewing;

        event EventHandler Exitting;
    }
}