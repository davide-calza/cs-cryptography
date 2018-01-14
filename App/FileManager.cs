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
        /// <summary>
        /// Open an open file dialog and select files
        /// </summary>
        /// <param name="ofd">Open File Dialog</param>
        /// <param name="title">Title of the dialog</param>
        /// <param name="multiSelection">Choose if multiple selection is enable or not</param>
        /// <returns>List of one or more file paths</returns>
        public static IEnumerable<string> OpenFile(OpenFileDialog ofd, string title, bool multiSelection)
        {
            ofd.Multiselect = multiSelection;
            ofd.Title = title;
            ofd.InitialDirectory = "./";
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileNames.ToList() : null;
        }

        /// <summary>
        /// Select the folder in which you want to operate
        /// </summary>
        /// <param name="fbd">Folder Browser Dialog</param>
        /// <returns>Returns the chosen folder path</returns>
        public static string SaveFile(FolderBrowserDialog fbd)
        {
            fbd.Description = "Select output directory";
            return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : null;
        }
    }
}
