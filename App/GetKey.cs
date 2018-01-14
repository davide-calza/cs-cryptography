using System;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace App
{
    public partial class GetKey : MaterialForm
    {
        public string Key { get; set; }

        public GetKey()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            
            //btn
            btnEnter.AutoSize = false;
            btnEnter.Location = new Point(20, 150);
            btnEnter.Width = Width-40;
            btnEnter.Height = Height - 170;

            //txtbox
            txtInput.Location = new Point(20, 110);
            txtInput.Width = Width - 40;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInput.Text)) return;
            Key = txtInput.Text;
            Close();
        }
    }
}
