using System;
using System.Windows.Forms;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameStages.BaseWorkflowStage;
using TicTacToe3D.GameStages.GameStage;
using TicTacToe3D.GeneralWorkflow.Events;
using TicTacToe3D.GeneralWorkflow.Interfaces;


namespace TicTacToe3D.GameStages.GameBuildingStage
{
    public class GameBuildingDirector: BaseWorkflowStep
    {
        private GameInformation _gameInfo;
        private GameInformationCollector _gameInfoCollector;
        private Form _parentWinForm;

        #region IWorkflowStep Members

        public override void Show(Form parentWinForm)
        {
            _parentWinForm = parentWinForm;
            _gameInfoCollector = new GameInformationCollector();
            _parentWinForm.Controls.Add(_gameInfoCollector);

            CenterForm(_parentWinForm);

            SubscribeToGameInfoCollectorEvents();
        }

        public override void StartOperation()
        {
            _gameInfoCollector.StartInfoCollection(); 
        }

        public override void Hide()
        {
            _parentWinForm.Controls.Remove(_gameInfoCollector);

            UnsubscribeFromGameInfoCollectorEvents();
        }

        #endregion

        private void SubscribeToGameInfoCollectorEvents()
        {
            _gameInfoCollector.GameInfoCollectionFinished += HandleGameInfoCollectionFinishedEvent;
        }

        private void UnsubscribeFromGameInfoCollectorEvents()
        {
            _gameInfoCollector.GameInfoCollectionFinished -= HandleGameInfoCollectionFinishedEvent;
        }

        private void HandleGameInfoCollectionFinishedEvent(object sender, EventArgs e)
        {
            _gameInfo = _gameInfoCollector.GameInformation;
            IWorkflowStep game = GameStageFactory.CreateGameStage(_gameInfo);
            OperationCompletedEventArgs operationCompletedEventArgs = new OperationCompletedEventArgs(game);
            OnOperationCompleted(operationCompletedEventArgs);
        }
    }
}