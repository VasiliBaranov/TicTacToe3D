using System;
using TicTacToe3D.GameInfo;
using TicTacToe3D.GameServer.Interfaces;

namespace TicTacToe3D.HumanParticipants
{
    public partial class HumanGameObserver : HumanParticipant, IGameObserver
    {
        public HumanGameObserver()
        {
            InitializeComponent();
        }

        public override void ModifyCell(Cell cell, Side side)
        {
            base.ModifyCell(cell, side);

            confirmButton.Enabled = true;
        }

        protected override void DisableAllGameFlowControlButtons()
        {
            confirmButton.Enabled = false;
        }

        #region IGameObserver Members

        public event EventHandler TurnConfirmed;

        #endregion

        private void HandleConfirmButtonClick(object sender, EventArgs e)
        {
            TurnConfirmed(this, new EventArgs());
        }
    }
}
