using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Editor
{
    class FileWrapper : IFile
    {
        public void CopyFile(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
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
    }
}
