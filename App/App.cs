using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MaterialSkin;
using MaterialSkin.Controls;

namespace FilesEnDecrypter
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
            var time = DateTime.Now;
            txtLogs.AppendText(DateTime.Now.ToString("HH:mm:ss") + " ~ " + text + '\n');
        }

        private static IEnumerable<string> OpenFile(OpenFileDialog ofd, string title, bool multiSelection)
        {
            ofd.Multiselect = multiSelection;
            ofd.Title = title;
            ofd.InitialDirectory = "./";
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileNames.ToList() : null;
        }

        private void encryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = OpenFile(ofd, "Select files to encrypt", true);
            if (list == null) return;
            
            foreach (var l in ClassACM.EncryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void decryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = OpenFile(ofd, "Select files to decrypt", true);
            if (list == null) return;

            foreach (var l in ClassACM.DecryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void verifyMD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = OpenFile(ofd, "Select file to calculate the MD5 of", false).ElementAt(0);
            if (file == null) return;
            
            WriteLogs(ClassACM.Md5FileString(file));
        }
    }    
}
