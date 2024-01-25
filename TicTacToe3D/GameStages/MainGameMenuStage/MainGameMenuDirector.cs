using System;
using System.Windows.Forms;
using TicTacToe3D.GameStages.BaseWorkflowStage;
using TicTacToe3D.GameStages.GameBuildingStage;
using TicTacToe3D.GameStages.GameLoadingStage;
using TicTacToe3D.GameStages.MainGameMenuStage;
using TicTacToe3D.GeneralWorkflow.Events;

namespace TicTacToe3D.GameStages.MainGameMenuStage
{
    public class MainGameMenuDirector : BaseWorkflowStep
    {
        private MainGameMenu _mainMenu;
        private Form _parentWinForm;

        #region IWorkflowStep Members

        public override void Show(Form parentWinForm)
        {
            _parentWinForm = parentWinForm;

            _mainMenu = new MainGameMenu {Enabled = false};
            _parentWinForm.Controls.Add(_mainMenu);

            CenterForm(_parentWinForm);

            SubscribeToMainMenuEvents();
        }

        public override void StartOperation()
        {
            _mainMenu.Enabled = true;
        }

        public override void Hide()
        {
            _parentWinForm.Controls.Remove(_mainMenu);

            UnsubscribeFromMainMenuEvents();
        }

        #endregion

        private void SubscribeToMainMenuEvents()
        {
            _mainMenu.NewGameStarting += HandleNewGameStartingEvent;
            _mainMenu.GameLoading += HandleGameLoadingEvent;
            _mainMenu.GameReplayViewing += HandleGameReplayViewingEvent;
            _mainMenu.Exitting += HandleExittingEvent;
        }

        private void UnsubscribeFromMainMenuEvents()
        {
            _mainMenu.NewGameStarting -= HandleNewGameStartingEvent;
            _mainMenu.GameLoading -= HandleGameLoadingEvent;
            _mainMenu.GameReplayViewing -= HandleGameReplayViewingEvent;
            _mainMenu.Exitting -= HandleExittingEvent;

        }

        private void HandleExittingEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(null);
            OnOperationCompleted(eventArgs);
        }

        private static void HandleGameReplayViewingEvent(object sender, EventArgs e)
        {

        }

        private void HandleGameLoadingEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(GameLoadingStagFactory.CreateGameLoadingStage());
            OnOperationCompleted(eventArgs);
        }

        private void HandleNewGameStartingEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(GameBuildingStageFactory.CreateGameBuildingStage());
            OnOperationCompleted(eventArgs);
        }
    }
}