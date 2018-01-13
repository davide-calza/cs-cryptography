using System.Drawing;

namespace FilesEnDecrypter
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
            this.btn_encrypt = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btn_decrypt = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btn_md5 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblLogs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.AutoSize = true;
            this.btn_encrypt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_encrypt.Depth = 0;
            this.btn_encrypt.Icon = null;
            this.btn_encrypt.Location = new System.Drawing.Point(22, 79);
            this.btn_encrypt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Primary = true;
            this.btn_encrypt.Size = new System.Drawing.Size(81, 36);
            this.btn_encrypt.TabIndex = 1;
            this.btn_encrypt.Text = "Encrypt";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // btn_decrypt
            // 
            this.btn_decrypt.AutoSize = true;
            this.btn_decrypt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_decrypt.Depth = 0;
            this.btn_decrypt.Icon = null;
            this.btn_decrypt.Location = new System.Drawing.Point(22, 121);
            this.btn_decrypt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_decrypt.Name = "btn_decrypt";
            this.btn_decrypt.Primary = true;
            this.btn_decrypt.Size = new System.Drawing.Size(81, 36);
            this.btn_decrypt.TabIndex = 2;
            this.btn_decrypt.Text = "Decrypt";
            this.btn_decrypt.UseVisualStyleBackColor = true;
            this.btn_decrypt.Click += new System.EventHandler(this.btn_decrypt_Click);
            // 
            // btn_md5
            // 
            this.btn_md5.AutoSize = true;
            this.btn_md5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_md5.Depth = 0;
            this.btn_md5.Icon = null;
            this.btn_md5.Location = new System.Drawing.Point(22, 163);
            this.btn_md5.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_md5.Name = "btn_md5";
            this.btn_md5.Primary = true;
            this.btn_md5.Size = new System.Drawing.Size(98, 36);
            this.btn_md5.TabIndex = 3;
            this.btn_md5.Text = "verify MD5";
            this.btn_md5.UseVisualStyleBackColor = true;
            this.btn_md5.Click += new System.EventHandler(this.btn_md5_Click);
            // 
            // lblLogs
            // 
            this.lblLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.lblLogs.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblLogs.ForeColor = System.Drawing.Color.White;
            this.lblLogs.Location = new System.Drawing.Point(19, 220);
            this.lblLogs.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(542, 258);
            this.lblLogs.TabIndex = 4;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 487);
            this.Controls.Add(this.lblLogs);
            this.Controls.Add(this.btn_md5);
            this.Controls.Add(this.btn_decrypt);
            this.Controls.Add(this.btn_encrypt);
            this.Name = "App";
            this.Text = "Files Encrypter/Decrypter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialRaisedButton btn_encrypt;
        private MaterialSkin.Controls.MaterialRaisedButton btn_decrypt;
        private MaterialSkin.Controls.MaterialRaisedButton btn_md5;
        private System.Windows.Forms.Label lblLogs;
    }
}