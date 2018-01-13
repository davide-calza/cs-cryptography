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

            // Configure color schema
            //materialSkinManager.ColorScheme = new ColorScheme(
            //    Primary.Blue400, Primary.Blue500,
            //    Primary.Blue500, Accent.LightBlue200,
            //    TextShade.WHITE
            //);
        }

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            WriteLogs(ClassACM.EncryptFile("test.txt", "./output", "stufa"));
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            WriteLogs(ClassACM.DecryptFile("./output/test.acm", "./output", "stufa"));
        }

        private void btn_md5_Click(object sender, EventArgs e)
        {
            WriteLogs(ClassACM.Md5FileString("test.txt"));
        }

        private void WriteLogs(string text)
        {
            var time = DateTime.Now;
            lblLogs.Text += DateTime.Now.ToString("HH:mm:ss") +" ~ " + text + "\n";
        }
    }
}