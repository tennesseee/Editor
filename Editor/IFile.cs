using System;
using System.Collections.Generic;
using System.Text;

namespace Editor
{
    public interface IFile
    {
        void CopyFile(string sourceFileName, string destFileName);

        void ReadTextFromFile(string fileName);

        bool CheckFileForExistence(string fileName);
    }
}
