namespace TicTacToe3D.GameStages.MainGameMenuStage
{
    partial class MainGameMenu
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
            this.newGameButton = new System.Windows.Forms.Button();
            this.loadGameButton = new System.Windows.Forms.Button();
            this.viewReplayButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(29, 71);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(128, 32);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "New Game";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.HandleNewGameButtonClick);
            // 
            // loadGameButton
            // 
            this.loadGameButton.Location = new System.Drawing.Point(29, 109);
            this.loadGameButton.Name = "loadGameButton";
            this.loadGameButton.Size = new System.Drawing.Size(128, 32);
            this.loadGameButton.TabIndex = 1;
            this.loadGameButton.Text = "Load Game";
            this.loadGameButton.UseVisualStyleBackColor = true;
            this.loadGameButton.Click += new System.EventHandler(this.HandleLoadGameButtonClick);
            // 
            // viewReplayButton
            // 
            this.viewReplayButton.Enabled = false;
            this.viewReplayButton.Location = new System.Drawing.Point(29, 147);
            this.viewReplayButton.Name = "viewReplayButton";
            this.viewReplayButton.Size = new System.Drawing.Size(128, 32);
            this.viewReplayButton.TabIndex = 2;
            this.viewReplayButton.Text = "View Replay";
            this.viewReplayButton.UseVisualStyleBackColor = true;
            this.viewReplayButton.Click += new System.EventHandler(this.HandleViewReplayButtonClick);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(30, 189);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(128, 32);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.HandleExitButtonClick);
            // 
            // MainGameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.viewReplayButton);
            this.Controls.Add(this.loadGameButton);
            this.Controls.Add(this.newGameButton);
            this.Name = "MainGameMenu";
            this.Size = new System.Drawing.Size(189, 311);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Button loadGameButton;
        private System.Windows.Forms.Button viewReplayButton;
        private System.Windows.Forms.Button exitButton;
    }
}