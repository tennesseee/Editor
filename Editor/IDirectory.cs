using System;
using System.Collections.Generic;
using System.Text;

namespace Editor
{
    public interface IDirectory
    {
        string GetCurrentDirectory();

        string[] GetFiles(string directory, string fileExtension);

        string Combine(string dirName, string fileName);
    }
}
