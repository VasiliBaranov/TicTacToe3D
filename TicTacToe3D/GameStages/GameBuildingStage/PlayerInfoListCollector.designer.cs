namespace TicTacToe3D.GameStages.GameBuildingStage
{
    partial class PlayerInfoListCollector
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
            this.playerDifficultyLabel = new System.Windows.Forms.Label();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.playerSideLabel = new System.Windows.Forms.Label();
            this.playerTypeLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.playerInformationCollector1 = new PlayerInformationCollector();
            this.playerInformationCollector2 = new PlayerInformationCollector();
            this.SuspendLayout();
            // 
            // playerDifficultyLabel
            // 
            this.playerDifficultyLabel.AutoSize = true;
            this.playerDifficultyLabel.Location = new System.Drawing.Point(332, 22);
            this.playerDifficultyLabel.Name = "playerDifficultyLabel";
            this.playerDifficultyLabel.Size = new System.Drawing.Size(82, 13);
            this.playerDifficultyLabel.TabIndex = 8;
            this.playerDifficultyLabel.Text = "Player Difficulty";
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Location = new System.Drawing.Point(198, 22);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(67, 13);
            this.playerNameLabel.TabIndex = 7;
            this.playerNameLabel.Text = "Player Name";
            // 
            // playerSideLabel
            // 
            this.playerSideLabel.AutoSize = true;
            this.playerSideLabel.Location = new System.Drawing.Point(3, 22);
            this.playerSideLabel.Name = "playerSideLabel";
            this.playerSideLabel.Size = new System.Drawing.Size(60, 13);
            this.playerSideLabel.TabIndex = 6;
            this.playerSideLabel.Text = "Player Side";
            // 
            // playerTypeLabel
            // 
            this.playerTypeLabel.AutoSize = true;
            this.playerTypeLabel.Location = new System.Drawing.Point(89, 22);
            this.playerTypeLabel.Name = "playerTypeLabel";
            this.playerTypeLabel.Size = new System.Drawing.Size(64, 13);
            this.playerTypeLabel.TabIndex = 5;
            this.playerTypeLabel.Text = "Player Type";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Location = new System.Drawing.Point(6, 0);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.descriptionLabel.TabIndex = 9;
            this.descriptionLabel.Text = "Description";
            // 
            // playerInformationCollector1
            // 
            this.playerInformationCollector1.AvailablePlayerDifficulties = null;
            this.playerInformationCollector1.AvailablePlayerTypes = null;
            this.playerInformationCollector1.AvailableSides = null;
            this.playerInformationCollector1.Enabled = false;
            this.playerInformationCollector1.Location = new System.Drawing.Point(9, 39);
            this.playerInformationCollector1.Name = "playerInformationCollector1";
            this.playerInformationCollector1.Size = new System.Drawing.Size(417, 28);
            this.playerInformationCollector1.TabIndex = 10;
            // 
            // playerInformationCollector2
            // 
            this.playerInformationCollector2.AvailablePlayerDifficulties = null;
            this.playerInformationCollector2.AvailablePlayerTypes = null;
            this.playerInformationCollector2.AvailableSides = null;
            this.playerInformationCollector2.Enabled = false;
            this.playerInformationCollector2.Location = new System.Drawing.Point(9, 74);
            this.playerInformationCollector2.Name = "playerInformationCollector2";
            this.playerInformationCollector2.Size = new System.Drawing.Size(417, 28);
            this.playerInformationCollector2.TabIndex = 11;
            // 
            // PlayerInfoCollectorsDirector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerInformationCollector2);
            this.Controls.Add(this.playerInformationCollector1);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.playerDifficultyLabel);
            this.Controls.Add(this.playerNameLabel);
            this.Controls.Add(this.playerSideLabel);
            this.Controls.Add(this.playerTypeLabel);
            this.Enabled = false;
            this.Name = "PlayerInfoCollectorsDirector";
            this.Size = new System.Drawing.Size(432, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playerDifficultyLabel;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.Label playerSideLabel;
        private System.Windows.Forms.Label playerTypeLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private PlayerInformationCollector playerInformationCollector1;
        private PlayerInformationCollector playerInformationCollector2;

    }
}