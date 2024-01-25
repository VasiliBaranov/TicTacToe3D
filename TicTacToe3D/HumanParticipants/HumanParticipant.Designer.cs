namespace TicTacToe3D.HumanParticipants
{
    partial class HumanParticipant
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
            this.components = new System.ComponentModel.Container();
            this.stopButton = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.rotateLeftRoundXAxisButton = new System.Windows.Forms.Button();
            this.rotateLeftRoundYAxisButton = new System.Windows.Forms.Button();
            this.rotateRightRoundXAxisButton = new System.Windows.Forms.Button();
            this.rotateRightRoundYAxisButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.rotationGroupBox = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.rotateLeftRoundZAxisButton = new System.Windows.Forms.Button();
            this.rotateRightRoundZAxisButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.zoomGroupBox = new System.Windows.Forms.GroupBox();
            this.saveUnfinishedGameDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.rotationGroupBox.SuspendLayout();
            this.zoomGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(24, 510);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(115, 30);
            this.stopButton.TabIndex = 23;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.HandleStopButtonClick);
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.Color.White;
            this.displayPanel.Location = new System.Drawing.Point(167, 22);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(539, 518);
            this.displayPanel.TabIndex = 13;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.HandleDisplayPanelPaint);
            // 
            // rotateLeftRoundXAxisButton
            // 
            this.rotateLeftRoundXAxisButton.Location = new System.Drawing.Point(66, 62);
            this.rotateLeftRoundXAxisButton.Name = "rotateLeftRoundXAxisButton";
            this.rotateLeftRoundXAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateLeftRoundXAxisButton.TabIndex = 24;
            this.rotateLeftRoundXAxisButton.Text = "<<";
            this.rotateLeftRoundXAxisButton.UseVisualStyleBackColor = true;
            this.rotateLeftRoundXAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateLeftRoundXAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // rotateLeftRoundYAxisButton
            // 
            this.rotateLeftRoundYAxisButton.Location = new System.Drawing.Point(196, 62);
            this.rotateLeftRoundYAxisButton.Name = "rotateLeftRoundYAxisButton";
            this.rotateLeftRoundYAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateLeftRoundYAxisButton.TabIndex = 25;
            this.rotateLeftRoundYAxisButton.Text = "<<";
            this.rotateLeftRoundYAxisButton.UseVisualStyleBackColor = true;
            this.rotateLeftRoundYAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateLeftRoundYAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // rotateRightRoundXAxisButton
            // 
            this.rotateRightRoundXAxisButton.Location = new System.Drawing.Point(6, 62);
            this.rotateRightRoundXAxisButton.Name = "rotateRightRoundXAxisButton";
            this.rotateRightRoundXAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateRightRoundXAxisButton.TabIndex = 26;
            this.rotateRightRoundXAxisButton.Text = ">>";
            this.rotateRightRoundXAxisButton.UseVisualStyleBackColor = true;
            this.rotateRightRoundXAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateRightRoundXAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // rotateRightRoundYAxisButton
            // 
            this.rotateRightRoundYAxisButton.Location = new System.Drawing.Point(136, 62);
            this.rotateRightRoundYAxisButton.Name = "rotateRightRoundYAxisButton";
            this.rotateRightRoundYAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateRightRoundYAxisButton.TabIndex = 27;
            this.rotateRightRoundYAxisButton.Text = ">>";
            this.rotateRightRoundYAxisButton.UseVisualStyleBackColor = true;
            this.rotateRightRoundYAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateRightRoundYAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // rotationGroupBox
            // 
            this.rotationGroupBox.Controls.Add(this.textBox4);
            this.rotationGroupBox.Controls.Add(this.textBox3);
            this.rotationGroupBox.Controls.Add(this.textBox2);
            this.rotationGroupBox.Controls.Add(this.rotateLeftRoundZAxisButton);
            this.rotationGroupBox.Controls.Add(this.rotateRightRoundZAxisButton);
            this.rotationGroupBox.Controls.Add(this.rotateRightRoundXAxisButton);
            this.rotationGroupBox.Controls.Add(this.rotateLeftRoundYAxisButton);
            this.rotationGroupBox.Controls.Add(this.rotateRightRoundYAxisButton);
            this.rotationGroupBox.Controls.Add(this.rotateLeftRoundXAxisButton);
            this.rotationGroupBox.Location = new System.Drawing.Point(167, 560);
            this.rotationGroupBox.Name = "rotationGroupBox";
            this.rotationGroupBox.Size = new System.Drawing.Size(398, 129);
            this.rotationGroupBox.TabIndex = 28;
            this.rotationGroupBox.TabStop = false;
            this.rotationGroupBox.Text = "Rotation Buttons. Press to rotate the game field";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(268, 19);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(119, 37);
            this.textBox4.TabIndex = 33;
            this.textBox4.Text = "Rotate round Z axis";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(136, 19);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(114, 37);
            this.textBox3.TabIndex = 32;
            this.textBox3.Text = "Rotate round Y axis";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 19);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(114, 37);
            this.textBox2.TabIndex = 31;
            this.textBox2.Text = "Rotate round X axis";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rotateLeftRoundZAxisButton
            // 
            this.rotateLeftRoundZAxisButton.Location = new System.Drawing.Point(333, 62);
            this.rotateLeftRoundZAxisButton.Name = "rotateLeftRoundZAxisButton";
            this.rotateLeftRoundZAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateLeftRoundZAxisButton.TabIndex = 29;
            this.rotateLeftRoundZAxisButton.Text = "<<";
            this.rotateLeftRoundZAxisButton.UseVisualStyleBackColor = true;
            this.rotateLeftRoundZAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateLeftRoundZAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // rotateRightRoundZAxisButton
            // 
            this.rotateRightRoundZAxisButton.Location = new System.Drawing.Point(268, 62);
            this.rotateRightRoundZAxisButton.Name = "rotateRightRoundZAxisButton";
            this.rotateRightRoundZAxisButton.Size = new System.Drawing.Size(54, 54);
            this.rotateRightRoundZAxisButton.TabIndex = 28;
            this.rotateRightRoundZAxisButton.Text = ">>";
            this.rotateRightRoundZAxisButton.UseVisualStyleBackColor = true;
            this.rotateRightRoundZAxisButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseDown);
            this.rotateRightRoundZAxisButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleRotateButtonMouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(119, 37);
            this.textBox1.TabIndex = 36;
            this.textBox1.Text = "Zoom camera";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.Location = new System.Drawing.Point(71, 62);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(54, 54);
            this.zoomOutButton.TabIndex = 35;
            this.zoomOutButton.Text = "-";
            this.zoomOutButton.UseVisualStyleBackColor = true;
            this.zoomOutButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleZoomButtonMouseDown);
            this.zoomOutButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleZoomButtonMouseUp);
            // 
            // zoomInButton
            // 
            this.zoomInButton.Location = new System.Drawing.Point(6, 62);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(54, 54);
            this.zoomInButton.TabIndex = 34;
            this.zoomInButton.Text = "+";
            this.zoomInButton.UseVisualStyleBackColor = true;
            this.zoomInButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleZoomButtonMouseDown);
            this.zoomInButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleZoomButtonMouseUp);
            // 
            // zoomGroupBox
            // 
            this.zoomGroupBox.Controls.Add(this.textBox1);
            this.zoomGroupBox.Controls.Add(this.zoomOutButton);
            this.zoomGroupBox.Controls.Add(this.zoomInButton);
            this.zoomGroupBox.Location = new System.Drawing.Point(571, 560);
            this.zoomGroupBox.Name = "zoomGroupBox";
            this.zoomGroupBox.Size = new System.Drawing.Size(135, 129);
            this.zoomGroupBox.TabIndex = 34;
            this.zoomGroupBox.TabStop = false;
            this.zoomGroupBox.Text = "Zoom Buttons";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(24, 474);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(115, 30);
            this.saveButton.TabIndex = 35;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.HandleSaveButtonClick);
            // 
            // HumanParticipant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.zoomGroupBox);
            this.Controls.Add(this.rotationGroupBox);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.displayPanel);
            this.Name = "HumanParticipant";
            this.Size = new System.Drawing.Size(732, 715);
            this.rotationGroupBox.ResumeLayout(false);
            this.rotationGroupBox.PerformLayout();
            this.zoomGroupBox.ResumeLayout(false);
            this.zoomGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Button rotateLeftRoundXAxisButton;
        private System.Windows.Forms.Button rotateLeftRoundYAxisButton;
        private System.Windows.Forms.Button rotateRightRoundXAxisButton;
        private System.Windows.Forms.Button rotateRightRoundYAxisButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox rotationGroupBox;
        private System.Windows.Forms.Button rotateLeftRoundZAxisButton;
        private System.Windows.Forms.Button rotateRightRoundZAxisButton;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.GroupBox zoomGroupBox;
        private System.Windows.Forms.SaveFileDialog saveUnfinishedGameDialog;
        private System.Windows.Forms.Button saveButton;
    }
}
