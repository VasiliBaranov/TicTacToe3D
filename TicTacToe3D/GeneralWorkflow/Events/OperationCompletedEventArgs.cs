using System;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GeneralWorkflow.Events
{
    public class OperationCompletedEventArgs:EventArgs
    {
        private readonly IWorkflowStep _nextScreenUser;

        public IWorkflowStep NextScreenUser
        {
            get
            {
                return _nextScreenUser;
            }
        }

        public OperationCompletedEventArgs(IWorkflowStep nextScreenUser)
        {
            _nextScreenUser = nextScreenUser;
        }
    }
}