using System;
using System.Windows.Forms;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.BaseWorkflowStage;
using TicTacToe3D.GameStages.GameStatisticsStage;
using TicTacToe3D.GameStages.MainGameMenuStage;
using TicTacToe3D.GeneralWorkflow.Events;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.GameStatisticsStage
{
    public class GameStatisticsStageDirector : BaseWorkflowStep
    {
        private readonly GameInformation _gameInfo;
        private GameStatistics _gameStatistics;
        private Form _parentWinForm;

        public GameStatisticsStageDirector(GameInformation gameInfo)
        {
            _gameInfo = gameInfo;
        }

        #region IWorkflowStep Members

        public override void Show(Form parentWinForm)
        {
            _parentWinForm = parentWinForm;
            _gameStatistics = new GameStatistics();
            _parentWinForm.Controls.Add(_gameStatistics);

            CenterForm(_parentWinForm);

            SubscribeToGameStatisticsEvents();
        }

        public override void StartOperation()
        {
            _gameStatistics.ShowStatisticsOnGame(_gameInfo);
        }

        public override void Hide()
        {
            _parentWinForm.Controls.Remove(_gameStatistics);

            UnsubscribeFromGameStatisticsEvents();
        }

        #endregion

        private void SubscribeToGameStatisticsEvents()
        {
            _gameStatistics.StatisticsConfirmed += HandleStatisticsConfirmedEvent;
            _gameStatistics.Exitting += HandleExittingEvent;
        }

        private void UnsubscribeFromGameStatisticsEvents()
        {
            _gameStatistics.StatisticsConfirmed -= HandleStatisticsConfirmedEvent;
            _gameStatistics.Exitting -= HandleExittingEvent;
        }

        private void HandleExittingEvent(object sender, EventArgs e)
        {
            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(null);
            OnOperationCompleted(eventArgs);
        }

        private void HandleStatisticsConfirmedEvent(object sender, EventArgs e)
        {
            IWorkflowStep mainMenuStage = MainGameMenuStageFactory.CreateMainGameMenuStage();

            OperationCompletedEventArgs eventArgs = new OperationCompletedEventArgs(mainMenuStage);
            OnOperationCompleted(eventArgs);
        }
    }
}