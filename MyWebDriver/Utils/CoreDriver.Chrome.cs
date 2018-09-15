using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebDriver.Utils
{
    public partial class CoreDriver
    {
        public string CopyDefaultProfileChrome(string profilePath, string pathDefaultProfileDirectory)
        {
            return CreateIfMissingChromeProfile(profilePath, pathDefaultProfileDirectory);
        }







        private void CopyProfileFilesChrome(string profilePath, string defaultProfileChromePath)
        {
            // default profile location
            DirectoryInfo di = new DirectoryInfo(defaultProfileChromePath);

            // copy files
            List<string> file_lib = new List<string>() { "Cookies", "Login", "Preferences", "Secur" };
            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            if (files.Count() > 0)
            {
                foreach (var file in files)
                {
                    if (PassFileOrFolder(file.Name, file_lib))
                    {
                        file.CopyTo(profilePath + @"\" + file.Name, true);
                    }

                }
            }
        }




        private void CopyProfileFoldersChrome(string profilePath, string profileDefaultChromePath)
        {
            // default profile location
            DirectoryInfo di = new DirectoryInfo(profileDefaultChromePath);

            // copy folders
            List<string> folder_lib = new List<string>() { "databases", "Extension", " Storage", "Web Applications", "File System", "IndexedDB" };
            DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
            if (directories.Count() > 0)
            {
                foreach (var folder in directories)
                {
                    if (PassFileOrFolder(folder.Name, folder_lib))
                    {
                        DirectoryCopy(folder.FullName, profilePath + @"\" + folder.Name, true);
                    }

                }

            }
        }

        private string CreateIfMissingChromeProfile(string profilePath, string profileDefaultChromePath)
        {


            if (Directory.Exists(profilePath))
                return Path.GetFullPath(profilePath);
            var info = Directory.CreateDirectory(profilePath);

            // copy existing chrome profile. Keep cache, extensions, etc.
            // but stay away from opened tabs
            CopyProfileFilesChrome(info.FullName, profileDefaultChromePath);
            CopyProfileFoldersChrome(info.FullName, profileDefaultChromePath);

            return Path.GetFullPath(profilePath);
        }
    }
}
