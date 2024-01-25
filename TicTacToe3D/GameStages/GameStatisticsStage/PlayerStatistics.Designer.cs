namespace TicTacToe3D.GameStages.GameStatisticsStage
{
    partial class PlayerStatistics
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playerVictoryLabel = new System.Windows.Forms.Label();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.playerSideLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // playerVictoryLabel
            // 
            this.playerVictoryLabel.AutoSize = true;
            this.playerVictoryLabel.Location = new System.Drawing.Point(141, 7);
            this.playerVictoryLabel.Name = "playerVictoryLabel";
            this.playerVictoryLabel.Size = new System.Drawing.Size(28, 13);
            this.playerVictoryLabel.TabIndex = 1;
            this.playerVictoryLabel.Text = "wins";
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Location = new System.Drawing.Point(4, 4);
            this.playerNameTextBox.Multiline = true;
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.ReadOnly = true;
            this.playerNameTextBox.Size = new System.Drawing.Size(97, 20);
            this.playerNameTextBox.TabIndex = 2;
            this.playerNameTextBox.Text = "Player";
            // 
            // playerSideLabel
            // 
            this.playerSideLabel.AutoSize = true;
            this.playerSideLabel.Location = new System.Drawing.Point(107, 7);
            this.playerSideLabel.Name = "playerSideLabel";
            this.playerSideLabel.Size = new System.Drawing.Size(13, 13);
            this.playerSideLabel.TabIndex = 3;
            this.playerSideLabel.Text = "X";
            // 
            // PlayerStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerSideLabel);
            this.Controls.Add(this.playerNameTextBox);
            this.Controls.Add(this.playerVictoryLabel);
            this.Name = "PlayerStatistics";
            this.Size = new System.Drawing.Size(205, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerVictoryLabel;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.Label playerSideLabel;
    }
}