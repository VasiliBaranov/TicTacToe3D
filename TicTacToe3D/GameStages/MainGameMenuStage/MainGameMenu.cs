using System;
using System.Windows.Forms;
using TicTacToe3D.GameStages.MainGameMenuStage.Interfaces;

namespace TicTacToe3D.GameStages.MainGameMenuStage
{
    public partial class MainGameMenu : UserControl, IMainGameMenu
    {
        public MainGameMenu()
        {
            InitializeComponent();
        }

        #region IMainGameMenu Members

        public event EventHandler NewGameStarting;

        public event EventHandler GameLoading;

        public event EventHandler GameReplayViewing;

        public event EventHandler Exitting;

        #endregion

        private void HandleNewGameButtonClick(object sender, EventArgs e)
        {
            if (NewGameStarting != null)
            {
                NewGameStarting(this, e);
            }
        }

        private void HandleLoadGameButtonClick(object sender, EventArgs e)
        {
            if (GameLoading != null)
            {
                GameLoading(this, e);
            }
        }

        private void HandleViewReplayButtonClick(object sender, EventArgs e)
        {
            if (GameReplayViewing != null)
            {
                GameReplayViewing(this, e);
            }
        }

        private void HandleExitButtonClick(object sender, EventArgs e)
        {
            if (Exitting != null)
            {
                Exitting(this, e);
            }
        }
    }
}