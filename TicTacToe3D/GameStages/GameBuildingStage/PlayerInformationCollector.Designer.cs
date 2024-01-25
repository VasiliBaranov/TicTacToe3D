namespace TicTacToe3D.GameStages.GameBuildingStage
{
    partial class PlayerInformationCollector
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
            this.playerDifficultyComboBox = new System.Windows.Forms.ComboBox();
            this.playerNameTextBox = new System.Windows.Forms.TextBox();
            this.playerTypeComboBox = new System.Windows.Forms.ComboBox();
            this.playerSideComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // playerDifficultyComboBox
            // 
            this.playerDifficultyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerDifficultyComboBox.FormattingEnabled = true;
            this.playerDifficultyComboBox.Location = new System.Drawing.Point(332, 3);
            this.playerDifficultyComboBox.Name = "playerDifficultyComboBox";
            this.playerDifficultyComboBox.Size = new System.Drawing.Size(79, 21);
            this.playerDifficultyComboBox.TabIndex = 15;
            // 
            // playerNameTextBox
            // 
            this.playerNameTextBox.Location = new System.Drawing.Point(198, 3);
            this.playerNameTextBox.Name = "playerNameTextBox";
            this.playerNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.playerNameTextBox.TabIndex = 14;
            // 
            // playerTypeComboBox
            // 
            this.playerTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerTypeComboBox.FormattingEnabled = true;
            this.playerTypeComboBox.Location = new System.Drawing.Point(89, 3);
            this.playerTypeComboBox.Name = "playerTypeComboBox";
            this.playerTypeComboBox.Size = new System.Drawing.Size(57, 21);
            this.playerTypeComboBox.TabIndex = 13;
            // 
            // playerSideComboBox
            // 
            this.playerSideComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.playerSideComboBox.FormattingEnabled = true;
            this.playerSideComboBox.Location = new System.Drawing.Point(3, 3);
            this.playerSideComboBox.Name = "playerSideComboBox";
            this.playerSideComboBox.Size = new System.Drawing.Size(57, 21);
            this.playerSideComboBox.TabIndex = 12;
            // 
            // PlayerInformationCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerDifficultyComboBox);
            this.Controls.Add(this.playerNameTextBox);
            this.Controls.Add(this.playerTypeComboBox);
            this.Controls.Add(this.playerSideComboBox);
            this.Enabled = false;
            this.Name = "PlayerInformationCollector";
            this.Size = new System.Drawing.Size(417, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox playerDifficultyComboBox;
        private System.Windows.Forms.TextBox playerNameTextBox;
        private System.Windows.Forms.ComboBox playerTypeComboBox;
        private System.Windows.Forms.ComboBox playerSideComboBox;
    }
}