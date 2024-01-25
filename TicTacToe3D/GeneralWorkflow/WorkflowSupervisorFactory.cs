using System.Windows.Forms;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GeneralWorkflow
{
    public static class WorkflowSupervisorFactory
    {
        public static IWorkflowSupervisor CreateWorkflowSupervisor(Form parentWinFormForWorkflowSteps, IWorkflowStep startingWorkflowSep)
        {
            return new WorkflowSupervisor(parentWinFormForWorkflowSteps, startingWorkflowSep);
        }
    }
}
