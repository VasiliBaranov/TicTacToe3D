using System;
using System.Windows.Forms;
using TicTacToe3D.GameStages.MainGameMenuStage;
using TicTacToe3D.GeneralWorkflow;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            IWorkflowSupervisor workflowSupervisor =
                WorkflowSupervisorFactory.CreateWorkflowSupervisor(this, MainGameMenuStageFactory.CreateMainGameMenuStage());
            workflowSupervisor.StartWorkflowProcessing();
            workflowSupervisor.WorkflowProcessingCompleted += HandleWorkflowProcessingCompletedEvent;
        }

        private static void HandleWorkflowProcessingCompletedEvent(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}