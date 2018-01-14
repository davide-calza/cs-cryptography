namespace App
{
    partial class GetKey
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
            this.btnEnter = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtInput = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.AutoSize = true;
            this.btnEnter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnter.Depth = 0;
            this.btnEnter.Icon = null;
            this.btnEnter.Location = new System.Drawing.Point(76, 154);
            this.btnEnter.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Primary = true;
            this.btnEnter.Size = new System.Drawing.Size(63, 36);
            this.btnEnter.TabIndex = 0;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtInput
            // 
            this.txtInput.Depth = 0;
            this.txtInput.Hint = "Enter key here...";
            this.txtInput.Location = new System.Drawing.Point(12, 110);
            this.txtInput.MaxLength = 32767;
            this.txtInput.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtInput.Name = "txtInput";
            this.txtInput.PasswordChar = '\0';
            this.txtInput.SelectedText = "";
            this.txtInput.SelectionLength = 0;
            this.txtInput.SelectionStart = 0;
            this.txtInput.Size = new System.Drawing.Size(268, 23);
            this.txtInput.TabIndex = 1;
            this.txtInput.TabStop = false;
            this.txtInput.UseSystemPasswordChar = false;
            // 
            // GetKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 220);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnEnter);
            this.Name = "GetKey";
            this.Text = "Insert secret key";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRaisedButton btnEnter;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtInput;
    }
}