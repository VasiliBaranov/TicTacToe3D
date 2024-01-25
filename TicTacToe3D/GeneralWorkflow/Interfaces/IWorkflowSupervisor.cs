using System;

namespace TicTacToe3D.GeneralWorkflow.Interfaces
{
    public interface IWorkflowSupervisor
    {
        void StartWorkflowProcessing();

        event EventHandler WorkflowProcessingCompleted;
    }
}