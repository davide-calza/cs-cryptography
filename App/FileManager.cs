using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace App
{
    internal class FileManager
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
            ofd.InitialDirectory = ConfigurationManager.AppSettings.Get("last_open_path");            

            if (ofd.ShowDialog() != DialogResult.OK) return null;
            ConfigurationManager.AppSettings.Set("last_open_path", Path.GetDirectoryName(ofd.FileName));
            return ofd.FileNames.ToList();
        }

        /// <summary>
        /// Select the folder in which you want to operate
        /// </summary>
        /// <param name="fbd">Folder Browser Dialog</param>
        /// <returns>Returns the chosen folder path</returns>
        public static string SaveFile(FolderBrowserDialog fbd)
        {
            fbd.Description = "Select output directory";
            fbd.SelectedPath = ConfigurationManager.AppSettings.Get("last_save_path");
            
            if (fbd.ShowDialog() != DialogResult.OK) return null;
            ConfigurationManager.AppSettings.Set("last_save_path", Path.GetDirectoryName(fbd.SelectedPath));
            return fbd.SelectedPath;
        }
    }
}
