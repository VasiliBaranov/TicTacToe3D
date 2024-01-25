using System;
using System.Windows.Forms;
using TicTacToe3D.GameStages.BaseWorkflowStage;
using TicTacToe3D.GameStages.GameLoadingStage;
using TicTacToe3D.GameStages.GameLoadingStage.Event;
using TicTacToe3D.GameStages.GameStage;
using TicTacToe3D.GameStages.MainGameMenuStage;
using TicTacToe3D.GeneralWorkflow.Events;

namespace TicTacToe3D.GameStages.GameLoadingStage
{
    public class GameLoadingDirector : BaseWorkflowStep
    {
        private GameLoader _gameLoader;
        private Form _parentWinForm;

        #region IWorkflowStep Members

        public override void Show(Form parentWinForm)
        {
            _parentWinForm = parentWinForm;

            _gameLoader = new GameLoader {Enabled = false};
            _parentWinForm.Controls.Add(_gameLoader);

            CenterForm(_parentWinForm);

            SubscribeToGameLoaderEvents();
        }

        public override void StartOperation()
        {
            _gameLoader.Enabled = true;
        }

        public override void Hide()
        {
            _parentWinForm.Controls.Remove(_gameLoader);

            UnsubscribeFromGameLoaderEvents();
        }

        #endregion

        private void SubscribeToGameLoaderEvents()
        {
            _gameLoader.Exitting += HandleExittingEvent;
            _gameLoader.GameContinuing += HandleGameContinuingEvent;
            _gameLoader.GoingBack += HandleGoingBackEvent;
        }

        private void UnsubscribeFromGameLoaderEvents()
        {
            _gameLoader.Exitting -= HandleExittingEvent;
            _gameLoader.GameContinuing -= HandleGameContinuingEvent;
            _gameLoader.GoingBack -= HandleGoingBackEvent;

        }

        private void HandleExittingEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(null);
            OnOperationCompleted(eventArgs);
        }

        private void HandleGameContinuingEvent(object sender, GameContinuingEventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(GameStageFactory.CreateGameStage(e.GameInfo));
            OnOperationCompleted(eventArgs);
        }

        private void HandleGoingBackEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(MainGameMenuStageFactory.CreateMainGameMenuStage());
            OnOperationCompleted(eventArgs);
        }
    }
}