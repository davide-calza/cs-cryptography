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

            //txtLogs
            txtLogs.Location = new Point(0, 220);
            txtLogs.BorderStyle = BorderStyle.None;
            txtLogs.Multiline = true;
            txtLogs.ScrollBars = ScrollBars.Vertical;
            txtLogs.Width = Width;
            txtLogs.Height = Height - 220;

            //buttons
            btn_decrypt.AutoSize = false;
            btn_encrypt.AutoSize = false;
            btn_md5.AutoSize = false;
            btn_encrypt.Width = Width/2;
            btn_decrypt.Width = Width/2;
            btn_md5.Width = Width/2;
        }

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            var list = new List<string> {"test1.txt", "test2.txt", "test3.txt"};
            
            foreach (var l in ClassACM.EncryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            var list = new List<string> {"./output/test1.acm", "./output/test2.acm", "./output/test3.acm"};
            
            foreach (var l in ClassACM.DecryptFileList(list, "./output", "stufa"))
                WriteLogs(l);
        }

        private void btn_md5_Click(object sender, EventArgs e)
        {
            WriteLogs(ClassACM.Md5FileString("test.txt"));
        }

        private void WriteLogs(string text)
        {
            var time = DateTime.Now;
            txtLogs.AppendText(DateTime.Now.ToString("HH:mm:ss") +" ~ " + text + '\n');
        }
    }
}