using System;
using System.Collections.Generic;
using System.Text;

namespace Editor
{
    public interface IFile
    {
        void CopyFile(string sourceFileName, string destFileName);

        string ReadAllText(string fileName);

        bool Exists(string fileName);

        void WriteAllText(string fileName, string text);
    }
}
