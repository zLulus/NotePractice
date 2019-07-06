namespace MyWindowsService.Client
{
    partial class ServiceManageForm
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
            this.InstallButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.UninstallButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(23, 22);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "安装服务";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(124, 22);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "启动服务";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(223, 22);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "停止服务";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // UninstallButton
            // 
            this.UninstallButton.Location = new System.Drawing.Point(318, 22);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(75, 23);
            this.UninstallButton.TabIndex = 3;
            this.UninstallButton.Text = "卸载服务";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 67);
            this.Controls.Add(this.UninstallButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.InstallButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button UninstallButton;
    }
}

