using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Editor
{
    public class FileWrapper : IFile
    {
        public void CopyFile(string sourceFileName, string destFileName, bool check)
        {
            check = false;
            File.Copy(sourceFileName, destFileName, check);
        }

        public string ReadAllText(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void WriteAllText(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }

        public bool IsNullOrWhiteSpace(string filePath)
        {
            return String.IsNullOrWhiteSpace(filePath);
        }
    }
}
