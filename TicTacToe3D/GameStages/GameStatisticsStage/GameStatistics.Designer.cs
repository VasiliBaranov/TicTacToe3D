namespace TicTacToe3D.GameStages.GameStatisticsStage
{
    partial class GameStatistics
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
            this.label1 = new System.Windows.Forms.Label();
            this.playerStatistics1 = new PlayerStatistics();
            this.playerStatistics2 = new PlayerStatistics();
            this.saveReplayButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.saveReplayDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Location = new System.Drawing.Point(67, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Statistics";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerStatistics1
            // 
            this.playerStatistics1.Location = new System.Drawing.Point(6, 47);
            this.playerStatistics1.Name = "playerStatistics1";
            this.playerStatistics1.Size = new System.Drawing.Size(184, 28);
            this.playerStatistics1.TabIndex = 1;
            // 
            // playerStatistics2
            // 
            this.playerStatistics2.Location = new System.Drawing.Point(6, 81);
            this.playerStatistics2.Name = "playerStatistics2";
            this.playerStatistics2.Size = new System.Drawing.Size(184, 28);
            this.playerStatistics2.TabIndex = 2;
            // 
            // saveReplayButton
            // 
            this.saveReplayButton.Location = new System.Drawing.Point(161, 156);
            this.saveReplayButton.Name = "saveReplayButton";
            this.saveReplayButton.Size = new System.Drawing.Size(97, 23);
            this.saveReplayButton.TabIndex = 3;
            this.saveReplayButton.Text = "Save replay?";
            this.saveReplayButton.UseVisualStyleBackColor = true;
            this.saveReplayButton.Click += new System.EventHandler(this.HandleSaveReplayButtonClickEvent);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(161, 185);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(97, 23);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Main Menu";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.HandleNextButtonClickEvent);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(161, 214);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(97, 23);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.HandleExitButtonClickEvent);
            // 
            // GameStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.saveReplayButton);
            this.Controls.Add(this.playerStatistics2);
            this.Controls.Add(this.playerStatistics1);
            this.Controls.Add(this.label1);
            this.Name = "GameStatistics";
            this.Size = new System.Drawing.Size(269, 240);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private PlayerStatistics playerStatistics1;
        private PlayerStatistics playerStatistics2;
        private System.Windows.Forms.Button saveReplayButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.SaveFileDialog saveReplayDialog;
    }
}