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
            this.listView = new MaterialSkin.Controls.MaterialListView();
            this.btn_encrypt = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btn_decrypt = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btn_md5 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.Color.White;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Depth = 0;
            this.listView.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.listView.ForeColor = System.Drawing.Color.Black;
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(12, 206);
            this.listView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listView.MouseState = MaterialSkin.MouseState.OUT;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.RightToLeftLayout = true;
            this.listView.Size = new System.Drawing.Size(556, 269);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_encrypt.Depth = 0;
            this.btn_encrypt.Icon = null;
            this.btn_encrypt.Location = new System.Drawing.Point(12, 79);
            this.btn_encrypt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Primary = true;
            this.btn_encrypt.Size = new System.Drawing.Size(191, 36);
            this.btn_encrypt.TabIndex = 1;
            this.btn_encrypt.Text = "Encrypt";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // btn_decrypt
            // 
            this.btn_decrypt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_decrypt.Depth = 0;
            this.btn_decrypt.Icon = null;
            this.btn_decrypt.Location = new System.Drawing.Point(12, 121);
            this.btn_decrypt.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_decrypt.Name = "btn_decrypt";
            this.btn_decrypt.Primary = true;
            this.btn_decrypt.Size = new System.Drawing.Size(191, 36);
            this.btn_decrypt.TabIndex = 2;
            this.btn_decrypt.Text = "Decrypt";
            this.btn_decrypt.UseVisualStyleBackColor = true;
            this.btn_decrypt.Click += new System.EventHandler(this.btn_decrypt_Click);
            // 
            // btn_md5
            // 
            this.btn_md5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_md5.Depth = 0;
            this.btn_md5.Icon = null;
            this.btn_md5.Location = new System.Drawing.Point(13, 164);
            this.btn_md5.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_md5.Name = "btn_md5";
            this.btn_md5.Primary = true;
            this.btn_md5.Size = new System.Drawing.Size(190, 36);
            this.btn_md5.TabIndex = 3;
            this.btn_md5.Text = "verify MD5";
            this.btn_md5.UseVisualStyleBackColor = true;
            this.btn_md5.Click += new System.EventHandler(this.btn_md5_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 487);
            this.Controls.Add(this.btn_md5);
            this.Controls.Add(this.btn_decrypt);
            this.Controls.Add(this.btn_encrypt);
            this.Controls.Add(this.listView);
            this.Name = "App";
            this.Text = "Files Encrypter/Decrypter";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView listView;
        private MaterialSkin.Controls.MaterialRaisedButton btn_encrypt;
        private MaterialSkin.Controls.MaterialRaisedButton btn_decrypt;
        private MaterialSkin.Controls.MaterialRaisedButton btn_md5;
    }
}