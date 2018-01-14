using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    internal static class FileManager
    {
        public static IEnumerable<string> OpenFile(OpenFileDialog ofd, string title, bool multiSelection)
        {
            ofd.Multiselect = multiSelection;
            ofd.Title = title;
            ofd.InitialDirectory = "./";
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileNames.ToList() : null;
        }

        public static string SaveFile(FolderBrowserDialog ffd)
        {
            ffd.Description = "Select output directory";
            return ffd.ShowDialog() == DialogResult.OK ? ffd.SelectedPath : null;
        }
    }
}
