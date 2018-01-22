using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using CryptoLib;

namespace App
{
    public partial class App : MaterialForm
    {
        public App()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            //menu
            menu.ForeColor = Color.White;
            menu.Dock = DockStyle.None;
            menu.Location = new Point(0, 0);
            menu.Renderer = new MyRenderer();

            //txtLogs
            txtLogs.Location = new Point(0, 64);
            txtLogs.BorderStyle = BorderStyle.None;
            txtLogs.Multiline = true;
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.Width = Width;
            txtLogs.Height = Height - 64;
        }

        private class MyRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                var rc = new Rectangle(Point.Empty, e.Item.Size);
                var c = e.Item.Selected ? Color.FromArgb(55,71,79) : Color.FromArgb(38, 50, 56);
                using (var brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
                e.Item.ForeColor = Color.White;
            }
        }

        private void WriteLogs(string text)
        {
            txtLogs.AppendText(DateTime.Now.ToString("HH:mm:ss") + " ~ " + text + "\r\n");
        }

        private void encryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = FileManager.OpenFile(ofd, "Select files to encrypt", true);
            if (list == null) return;

            var outDir = FileManager.SaveFile(ffd);
            if (outDir == null) return;
            
            var key = GetKey();
            if (string.IsNullOrEmpty(key)) return;
            
            foreach (var l in ClassAcm.EncryptFileList(list, outDir, key))
                WriteLogs(l + "\r\n");
        }

        private void decryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = FileManager.OpenFile(ofd, "Select files to decrypt", true);
            if (list == null) return;

            var outDir = FileManager.SaveFile(ffd);
            if (outDir == null) return;

            var key = GetKey();
            if (string.IsNullOrEmpty(key)) return;

            foreach (var l in ClassAcm.DecryptFileList(list, outDir, key))
                WriteLogs(l + "\r\n");
        }

        private void verifyMD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = FileManager.OpenFile(ofd, "Select .acm file to calculate the MD5 of", true);
            if (list == null) return;
            
            var key = GetKey();
            if (string.IsNullOrEmpty(key)) return;
            
            foreach (var l in ClassAcm.VerifyAcmMd5List(list, key))
                WriteLogs(l + "\r\n");
        }

        private void calculateTxtMD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = FileManager.OpenFile(ofd, "Select .txt file to calculate the MD5 of", true);
            if (list == null) return;

            foreach (var l in ClassAcm.CalculateTxtMd5List(list))
                WriteLogs(l + "\r\n");
        }

        private static string GetKey()
        {
            var keyForm = new GetKey();
            keyForm.ShowDialog();
            if (string.IsNullOrEmpty(keyForm.Key)) return null;
            var key = keyForm.Key;
            keyForm.Key = null;
            return key;
        }
    }    
}
