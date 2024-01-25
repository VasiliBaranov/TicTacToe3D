using System;
using System.Windows.Forms;
using TicTacToe3D.GeneralWorkflow.Events;

namespace TicTacToe3D.GeneralWorkflow.Interfaces
{
    public interface IWorkflowStep
    {
        void Show(Form parentWinForm);
        void StartOperation();
        void Hide();

        event EventHandler<OperationCompletedEventArgs> OperationCompleted;
    }
}