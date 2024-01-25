namespace GraphicsFrameworkTest
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.RotateCameraToTheRightButton = new System.Windows.Forms.Button();
            this.RotateCameraToTheLeft = new System.Windows.Forms.Button();
            this.ZoomInButton = new System.Windows.Forms.Button();
            this.ZoomOutButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(13, 13);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(472, 271);
            this.MainPanel.TabIndex = 0;
            // 
            // RotateCameraToTheRightButton
            // 
            this.RotateCameraToTheRightButton.Location = new System.Drawing.Point(406, 290);
            this.RotateCameraToTheRightButton.Name = "RotateCameraToTheRightButton";
            this.RotateCameraToTheRightButton.Size = new System.Drawing.Size(79, 65);
            this.RotateCameraToTheRightButton.TabIndex = 1;
            this.RotateCameraToTheRightButton.Text = ">>";
            this.RotateCameraToTheRightButton.UseVisualStyleBackColor = true;
            this.RotateCameraToTheRightButton.Click += new System.EventHandler(this.RotateCameraToTheRightButton_Click);
            // 
            // RotateCameraToTheLeft
            // 
            this.RotateCameraToTheLeft.Location = new System.Drawing.Point(12, 290);
            this.RotateCameraToTheLeft.Name = "RotateCameraToTheLeft";
            this.RotateCameraToTheLeft.Size = new System.Drawing.Size(79, 65);
            this.RotateCameraToTheLeft.TabIndex = 2;
            this.RotateCameraToTheLeft.Text = "<<";
            this.RotateCameraToTheLeft.UseVisualStyleBackColor = true;
            this.RotateCameraToTheLeft.Click += new System.EventHandler(this.RotateCameraToTheLeft_Click);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.Location = new System.Drawing.Point(285, 290);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(79, 65);
            this.ZoomInButton.TabIndex = 3;
            this.ZoomInButton.Text = "+";
            this.ZoomInButton.UseVisualStyleBackColor = true;
            this.ZoomInButton.Click += new System.EventHandler(this.HandleZoomInClick);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.Location = new System.Drawing.Point(135, 290);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(79, 65);
            this.ZoomOutButton.TabIndex = 4;
            this.ZoomOutButton.Text = "-";
            this.ZoomOutButton.UseVisualStyleBackColor = true;
            this.ZoomOutButton.Click += new System.EventHandler(this.HandleZoomOutClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 397);
            this.Controls.Add(this.ZoomOutButton);
            this.Controls.Add(this.ZoomInButton);
            this.Controls.Add(this.RotateCameraToTheLeft);
            this.Controls.Add(this.RotateCameraToTheRightButton);
            this.Controls.Add(this.MainPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button RotateCameraToTheRightButton;
        private System.Windows.Forms.Button RotateCameraToTheLeft;
        private System.Windows.Forms.Button ZoomInButton;
        private System.Windows.Forms.Button ZoomOutButton;
    }
}

