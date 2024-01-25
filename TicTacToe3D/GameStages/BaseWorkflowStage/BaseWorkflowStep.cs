using System;
using System.Windows.Forms;
using System.Drawing;
using TicTacToe3D.GeneralWorkflow.Events;
using TicTacToe3D.GeneralWorkflow.Interfaces;

namespace TicTacToe3D.GameStages.BaseWorkflowStage
{
    public abstract class BaseWorkflowStep : IWorkflowStep
    {

        #region IWorkflowStep Members

        public abstract void Show(Form parentWinForm);

        public abstract void StartOperation();

        public abstract void Hide();

        public event EventHandler<OperationCompletedEventArgs> OperationCompleted;

        #endregion

        protected void OnOperationCompleted(OperationCompletedEventArgs e)
        {
            OperationCompleted(this, e);
        }

        protected static void CenterForm(Form form)
        {
            Screen screen = Screen.FromControl(form);

            Rectangle formRectangle = form.Bounds;

            formRectangle.X = (screen.Bounds.X + screen.Bounds.Width - form.Width) / 2;
            formRectangle.Y = (screen.Bounds.Y + screen.Bounds.Height - form.Height) / 2;

            form.Bounds = formRectangle;
        }
    }
}