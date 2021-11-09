using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Editor
{
    public class DirectoryWrapper : IDirectory
    {
        public string Combine(string dirName, string fileName)
        {
            return Path.Combine(dirName, fileName);
        }

        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public string[] GetFiles(string directory, string fileExtension)
        {
            return Directory.GetFiles(directory, fileExtension);
        }
    }
}
