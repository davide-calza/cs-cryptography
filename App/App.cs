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

        internal class MyRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                Color c = e.Item.Selected ? Color.FromArgb(55,71,79) : Color.FromArgb(38, 50, 56);
                using (SolidBrush brush = new SolidBrush(c))
                    e.Graphics.FillRectangle(brush, rc);
                e.Item.ForeColor = Color.White;
            }
        }

        private void WriteLogs(string text)
        {
            var time = DateTime.Now;
            txtLogs.AppendText(DateTime.Now.ToString("HH:mm:ss") + " ~ " + text + '\n');
        }

        private void encryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<string> { "test1.txt", "test2.txt", "test3.txt" };

            foreach (var l in ClassACM.EncryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void decryptFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = new List<string> { "./output/test1.acm", "./output/test2.acm", "./output/test3.acm" };

            foreach (var l in ClassACM.DecryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void verifyMD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteLogs(ClassACM.Md5FileString("test.txt"));
        }
    }    
}
