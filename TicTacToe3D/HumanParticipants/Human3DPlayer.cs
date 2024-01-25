using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe3D
{
    public partial class Human3DPlayer : UserControl, IPlayer
    {
        private GraphicsIn3D _artist;

        private Side _side;

        /// <summary>
        /// The index of the last appeared figure
        /// </summary>
        private int[] _lastShown = new int[3];


        public Human3DPlayer()
        {
            InitializeComponent();

            Graphics temp_graph_panel = Graphics.FromHwnd(this.displayPanel.Handle);

            _artist = new GraphicsIn3D(temp_graph_panel);
            _artist.GraphPanel.PageUnit = GraphicsUnit.Pixel;
            _artist.GraphPanel.TranslateTransform(259, 259);
            PrepareView();

            xTrackBar.Minimum = 0; xTrackBar.Maximum = 2;
            yTrackBar.Minimum = 0; yTrackBar.Maximum = 2;
            zTrackBar.Minimum = 0; zTrackBar.Maximum = 2;

            this.KeyDown += new KeyEventHandler(Control_KeyDown);
        }

        #region IPlayer Members

        public Side Side
        {
            get
            {
                return _side;
            }
            set
            {
                _side = value;
            }
        }

        public void PrepareForGame(GameRules gameRules)
        {
            _artist.DeleteAll();
            PrepareView();

            PreparedForGame(this, new EventArgs());
        }

        public void HandleGameTermination(GameStatus gameStatus, List<IPlayer> winners)
        {
            beginThinkingButton.Enabled = false;
            confirmButton.Enabled = false;
            turnGroupBox.Enabled = false;
            xTrackBar.Enabled = false;
            yTrackBar.Enabled = false;
            zTrackBar.Enabled = false;
        }

        public void MakeTurn()
        {
            beginThinkingButton.Enabled = true;
        }

        public void ModifyCell(Cell cell, CellState state)
        {
            _artist.AddObject(state, cell[0], cell[1], cell[2]);
        }

        public event EventHandler PreparedForGame;

        public event EventHandler GameTerminated;

        public event EventHandler<TurnMadeEventArgs> TurnMade;

        #endregion

        private void beginThinkingButton_Click(object sender, EventArgs e)
        {
            turnGroupBox.Enabled = true;
            xTrackBar.Enabled = true;
            yTrackBar.Enabled = true;
            zTrackBar.Enabled = true;
            confirmButton.Enabled = true;
            beginThinkingButton.Enabled = false;

            int i, j, k;
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    for (k = 0; k < size; k++)
                    {
                        if (Brains.disposal3[i, j, k] == n)
                        {
                            xTrackBar.Value = i;
                            yTrackBar.Value = j;
                            zTrackBar.Value = k;

                            _artist.AddObject(_side, i, j, k);
                            _lastShown[0] = i;
                            _lastShown[1] = j;
                            _lastShown[2] = k;
                            return;
                        }
                    }
                }
            }
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            turnGroupBox.Enabled = false;
            xTrackBar.Enabled = false;
            yTrackBar.Enabled = false; 
            zTrackBar.Enabled = false;
            confirmButton.Enabled = false; 
            beginThinkingButton.Enabled = true;

            Cell recentlyModifiedCell=new Cell(_lastShown);
            TurnMadeEventArgs e = new TurnMadeEventArgs(recentlyModifiedCell);
            TurnMade(this, e);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            GameTerminated(this, new EventArgs());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void CoordinateTrackBar_Scroll(object sender, EventArgs e)
        {
            _artist.DeleteObject(_lastShown[0], _lastShown[1], _lastShown[2]);

            if (Brains.disposal3[xTrackBar.Value, yTrackBar.Value, zTrackBar.Value] != n) return;
            _lastShown[0] = xTrackBar.Value;
            _lastShown[1] = yTrackBar.Value;
            _lastShown[2] = zTrackBar.Value;
            _artist.AddObject(_side, _lastShown[0], _lastShown[1], _lastShown[2]);
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            _artist.DrawPanel();
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                _artist.PovorotOtnX((double)-Math.PI / 20);
            }
            else if (e.KeyCode == Keys.S)
            {
                _artist.PovorotOtnX((double)Math.PI / 20);
            }
            else if (e.KeyCode == Keys.A)
            {
                _artist.PovorotOtnZ((double)-Math.PI / 20);
            }
            else if (e.KeyCode == Keys.D)
            {
                _artist.PovorotOtnZ((double)Math.PI / 20);
            }
            else if (e.KeyCode == Keys.Z)
            {
                _artist.PovorotOtnY((double)-Math.PI / 20);
            }
            else if (e.KeyCode == Keys.V)
            {
                _artist.PovorotOtnY((double)Math.PI / 20);
            }
        }

        /// <summary>
        /// Prepares the default view for the game lattice
        /// </summary>
        private void PrepareView()
        {
            _artist.PovorotOtnX((double)Math.PI / 4);

            _artist.PovorotOtnY((double)Math.PI / 5);

            _artist.PovorotOtnZ((double)Math.PI / 2);
        }
    }
}
