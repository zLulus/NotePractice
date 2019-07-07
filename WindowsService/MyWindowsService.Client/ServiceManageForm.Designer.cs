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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.ServiceNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(21, 98);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(75, 23);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "安装服务";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(122, 98);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "启动服务";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(221, 98);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "停止服务";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // UninstallButton
            // 
            this.UninstallButton.Location = new System.Drawing.Point(316, 98);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(75, 23);
            this.UninstallButton.TabIndex = 3;
            this.UninstallButton.Text = "卸载服务";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "选择文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "服务名称";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(316, 16);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(75, 23);
            this.SelectFileButton.TabIndex = 6;
            this.SelectFileButton.Text = "选择文件";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Enabled = false;
            this.FilePathTextBox.Location = new System.Drawing.Point(97, 18);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(199, 21);
            this.FilePathTextBox.TabIndex = 7;
            // 
            // ServiceNameTextBox
            // 
            this.ServiceNameTextBox.Location = new System.Drawing.Point(97, 55);
            this.ServiceNameTextBox.Name = "ServiceNameTextBox";
            this.ServiceNameTextBox.Size = new System.Drawing.Size(199, 21);
            this.ServiceNameTextBox.TabIndex = 8;
            // 
            // ServiceManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 133);
            this.Controls.Add(this.ServiceNameTextBox);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UninstallButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.InstallButton);
            this.Name = "ServiceManageForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button UninstallButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TextBox ServiceNameTextBox;
    }
}

