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

        public void ReadTextFromFile(string fileName)
        {
            File.ReadAllText(fileName);
        }

        public bool CheckFileForExistence(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
