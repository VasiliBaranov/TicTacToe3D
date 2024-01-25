namespace TicTacToe3D.GameStages.GameBuildingStage
{
    partial class GameInformationCollector
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
            this.playButton = new System.Windows.Forms.Button();
            this.gameFieldSize = new System.Windows.Forms.Label();
            this.elementsInLineToWinLabel = new System.Windows.Forms.Label();
            this.playerInfoListCollector = new PlayerInfoListCollector();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(464, 398);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play!";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // gameFieldParameters
            // 
            this.gameFieldSize.AutoSize = true;
            this.gameFieldSize.Location = new System.Drawing.Point(3, 0);
            this.gameFieldSize.Name = "gameFieldParameters";
            this.gameFieldSize.Size = new System.Drawing.Size(125, 13);
            this.gameFieldSize.TabIndex = 6;
            this.gameFieldSize.Text = "Game Field Size = 3x3x3";
            // 
            // elementsInLineToWinLabel
            // 
            this.elementsInLineToWinLabel.AutoSize = true;
            this.elementsInLineToWinLabel.Location = new System.Drawing.Point(3, 31);
            this.elementsInLineToWinLabel.Name = "elementsInLineToWinLabel";
            this.elementsInLineToWinLabel.Size = new System.Drawing.Size(132, 13);
            this.elementsInLineToWinLabel.TabIndex = 7;
            this.elementsInLineToWinLabel.Text = "Elements in line to win = 3";
            // 
            // playerInfoCollectorsDirector
            // 
            this.playerInfoListCollector.Location = new System.Drawing.Point(6, 48);
            this.playerInfoListCollector.Name = "playerInfoListCollector";
            this.playerInfoListCollector.Size = new System.Drawing.Size(450, 150);
            this.playerInfoListCollector.TabIndex = 8;
            // 
            // GameInformationCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerInfoListCollector);
            this.Controls.Add(this.elementsInLineToWinLabel);
            this.Controls.Add(this.gameFieldSize);
            this.Controls.Add(this.playButton);
            this.Enabled = false;
            this.Name = "GameInformationCollector";
            this.Size = new System.Drawing.Size(542, 424);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label gameFieldSize;
        private System.Windows.Forms.Label elementsInLineToWinLabel;
        private PlayerInfoListCollector playerInfoListCollector;
    }
}