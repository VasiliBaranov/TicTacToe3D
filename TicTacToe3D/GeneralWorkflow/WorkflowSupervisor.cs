using System;
using System.Windows.Forms;
using TicTacToe3D.GeneralWorkflow.Events;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GeneralWorkflow
{
    public class WorkflowSupervisor : IWorkflowSupervisor
    {
        private IWorkflowStep _currentWorkflowStep;
        private readonly Form _parentWinFormForWorkflowSteps;
        private bool _wasWorkflowStarted;

        public WorkflowSupervisor(Form parentWinFormForWorkflowSteps, IWorkflowStep startingWorkflowStep)
        {
            _parentWinFormForWorkflowSteps = parentWinFormForWorkflowSteps;
            _currentWorkflowStep = startingWorkflowStep;
            _wasWorkflowStarted = false;
        }

        #region IWorkflowSupervisor Members

        public void StartWorkflowProcessing()
        {
            if (!_wasWorkflowStarted)
            {
                ProcessCurrentWorkflowStep();
                _wasWorkflowStarted = true;
            }
        }

        public event EventHandler WorkflowProcessingCompleted;

        #endregion

        private void HandleOperationCompletedEvent(object sender, OperationCompletedEventArgs e)
        {
            _currentWorkflowStep.Hide();
            _currentWorkflowStep.OperationCompleted -= HandleOperationCompletedEvent;
            _currentWorkflowStep = e.NextScreenUser;

            if (_currentWorkflowStep == null && WorkflowProcessingCompleted != null)
            {
                WorkflowProcessingCompleted(this, new EventArgs());

                return;
            }
            if (_currentWorkflowStep == null && WorkflowProcessingCompleted == null)
            {
                return;
            }

            ProcessCurrentWorkflowStep();
        }

        private void ProcessCurrentWorkflowStep()
        {
            _currentWorkflowStep.OperationCompleted += HandleOperationCompletedEvent;
            _currentWorkflowStep.Show(_parentWinFormForWorkflowSteps);
            _currentWorkflowStep.StartOperation();
        }
    }
}
