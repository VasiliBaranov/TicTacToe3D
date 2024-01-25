namespace TicTacToe3D
{
    partial class Human3DPlayer
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
            this.stopButton = new System.Windows.Forms.Button();
            this.beginThinkingButton = new System.Windows.Forms.Button();
            this.textBox_Difficulty = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.turnGroupBox = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.zTrackBar = new System.Windows.Forms.TrackBar();
            this.yTrackBar = new System.Windows.Forms.TrackBar();
            this.xTrackBar = new System.Windows.Forms.TrackBar();
            this.exitButton = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.turnGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(577, 533);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(115, 30);
            this.stopButton.TabIndex = 23;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // beginThinkingButton
            // 
            this.beginThinkingButton.Location = new System.Drawing.Point(577, 287);
            this.beginThinkingButton.Name = "beginThinkingButton";
            this.beginThinkingButton.Size = new System.Drawing.Size(115, 30);
            this.beginThinkingButton.TabIndex = 22;
            this.beginThinkingButton.Text = "Begin thinking";
            this.beginThinkingButton.UseVisualStyleBackColor = true;
            this.beginThinkingButton.Click += new System.EventHandler(this.beginThinkingButton_Click);
            // 
            // textBox_Difficulty
            // 
            this.textBox_Difficulty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Difficulty.Location = new System.Drawing.Point(609, 63);
            this.textBox_Difficulty.Name = "textBox_Difficulty";
            this.textBox_Difficulty.ReadOnly = true;
            this.textBox_Difficulty.Size = new System.Drawing.Size(22, 13);
            this.textBox_Difficulty.TabIndex = 21;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(734, 287);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(115, 30);
            this.confirmButton.TabIndex = 19;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // turnGroupBox
            // 
            this.turnGroupBox.Controls.Add(this.textBox1);
            this.turnGroupBox.Controls.Add(this.zTrackBar);
            this.turnGroupBox.Controls.Add(this.yTrackBar);
            this.turnGroupBox.Controls.Add(this.xTrackBar);
            this.turnGroupBox.Location = new System.Drawing.Point(542, 159);
            this.turnGroupBox.Name = "turnGroupBox";
            this.turnGroupBox.Size = new System.Drawing.Size(306, 109);
            this.turnGroupBox.TabIndex = 18;
            this.turnGroupBox.TabStop = false;
            this.turnGroupBox.Text = "Choose the place you want to use";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(274, 13);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "     x                                      y                              z";
            // 
            // zTrackBar
            // 
            this.zTrackBar.Location = new System.Drawing.Point(215, 44);
            this.zTrackBar.Name = "zTrackBar";
            this.zTrackBar.Size = new System.Drawing.Size(72, 45);
            this.zTrackBar.TabIndex = 2;
            this.zTrackBar.Scroll += new System.EventHandler(this.CoordinateTrackBar_Scroll);
            // 
            // yTrackBar
            // 
            this.yTrackBar.Location = new System.Drawing.Point(118, 44);
            this.yTrackBar.Name = "yTrackBar";
            this.yTrackBar.Size = new System.Drawing.Size(72, 45);
            this.yTrackBar.TabIndex = 1;
            this.yTrackBar.Scroll += new System.EventHandler(this.CoordinateTrackBar_Scroll);
            // 
            // xTrackBar
            // 
            this.xTrackBar.Location = new System.Drawing.Point(18, 44);
            this.xTrackBar.Name = "xTrackBar";
            this.xTrackBar.Size = new System.Drawing.Size(72, 45);
            this.xTrackBar.TabIndex = 0;
            this.xTrackBar.Scroll += new System.EventHandler(this.CoordinateTrackBar_Scroll);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(734, 532);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(115, 30);
            this.exitButton.TabIndex = 14;
            this.exitButton.Text = "&Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.Color.White;
            this.displayPanel.Location = new System.Drawing.Point(17, 44);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(518, 518);
            this.displayPanel.TabIndex = 13;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(560, 63);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(56, 13);
            this.textBox2.TabIndex = 16;
            this.textBox2.Text = "Difficulty:";
            // 
            // HumanPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.beginThinkingButton);
            this.Controls.Add(this.textBox_Difficulty);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.turnGroupBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.displayPanel);
            this.Name = "Human3DPlayer";
            this.Size = new System.Drawing.Size(864, 606);
            this.turnGroupBox.ResumeLayout(false);
            this.turnGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button beginThinkingButton;
        private System.Windows.Forms.TextBox textBox_Difficulty;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.GroupBox turnGroupBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar zTrackBar;
        private System.Windows.Forms.TrackBar yTrackBar;
        private System.Windows.Forms.TrackBar xTrackBar;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.TextBox textBox2;
    }
}
