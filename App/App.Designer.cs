namespace App
{
    partial class App
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
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verifyMD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateTxtMD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.ffd = new System.Windows.Forms.FolderBrowserDialog();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLogs
            // 
            this.txtLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.txtLogs.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLogs.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtLogs.ForeColor = System.Drawing.Color.White;
            this.txtLogs.HideSelection = false;
            this.txtLogs.Location = new System.Drawing.Point(0, 64);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(546, 241);
            this.txtLogs.TabIndex = 4;
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptFilesToolStripMenuItem,
            this.decryptFilesToolStripMenuItem,
            this.verifyMD5ToolStripMenuItem,
            this.calculateTxtMD5ToolStripMenuItem});
            this.actionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // encryptFilesToolStripMenuItem
            // 
            this.encryptFilesToolStripMenuItem.Name = "encryptFilesToolStripMenuItem";
            this.encryptFilesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.encryptFilesToolStripMenuItem.Text = "Encrypt files";
            this.encryptFilesToolStripMenuItem.Click += new System.EventHandler(this.encryptFilesToolStripMenuItem_Click);
            // 
            // decryptFilesToolStripMenuItem
            // 
            this.decryptFilesToolStripMenuItem.Name = "decryptFilesToolStripMenuItem";
            this.decryptFilesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.decryptFilesToolStripMenuItem.Text = "Decrypt files";
            this.decryptFilesToolStripMenuItem.Click += new System.EventHandler(this.decryptFilesToolStripMenuItem_Click);
            // 
            // verifyMD5ToolStripMenuItem
            // 
            this.verifyMD5ToolStripMenuItem.Name = "verifyMD5ToolStripMenuItem";
            this.verifyMD5ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.verifyMD5ToolStripMenuItem.Text = "Verify acm MD5";
            this.verifyMD5ToolStripMenuItem.Click += new System.EventHandler(this.verifyMD5ToolStripMenuItem_Click);
            // 
            // calculateTxtMD5ToolStripMenuItem
            // 
            this.calculateTxtMD5ToolStripMenuItem.Name = "calculateTxtMD5ToolStripMenuItem";
            this.calculateTxtMD5ToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.calculateTxtMD5ToolStripMenuItem.Text = "Calculate txt MD5";
            this.calculateTxtMD5ToolStripMenuItem.Click += new System.EventHandler(this.calculateTxtMD5ToolStripMenuItem_Click);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menu.Dock = System.Windows.Forms.DockStyle.None;
            this.menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionsToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(150, 24);
            this.menu.TabIndex = 5;
            // 
            // ofd
            // 
            this.ofd.FileName = "ofd";
            this.ofd.Multiselect = true;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 487);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.menu);
            this.Location = new System.Drawing.Point(0, 64);
            this.MainMenuStrip = this.menu;
            this.Name = "App";
            this.Text = "Files Encrypter/Decrypter";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verifyMD5ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.FolderBrowserDialog ffd;
        private System.Windows.Forms.ToolStripMenuItem calculateTxtMD5ToolStripMenuItem;
    }
}