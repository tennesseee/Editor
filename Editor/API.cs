using System;
using System.IO;
using System.Linq;

namespace Editor
{
    public class API
    {
        private readonly IFile _fileWrapper;

        public API(IFile fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public void CopyFileToStorage(string filePath)
        {
            try
            {
                if (filePath != null && _fileWrapper.CheckFileForExistence(filePath))
                {
                    throw new Exception();
                }

                string directory = Directory.GetCurrentDirectory();
                string newFile = Path.Combine(directory, "CreatedFile.txt");

                _fileWrapper.CopyFile(filePath, newFile);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string[] GetFileNamesInStorage()
        {
            try
            {
                string directory = Directory.GetCurrentDirectory();
                string[] files = Directory.GetFiles(directory, "*.txt");

                if (files == null || files.Length == 0)
                {
                    throw new Exception("There is no valid files in the storage/directory");
                }
                else
                {
                    return files;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int FindAndReplace(string fileName, string searchText, string replaceText)
        {
            string textInFile = File.ReadAllText(fileName);
            textInFile = textInFile.Replace(searchText, replaceText);
            File.WriteAllText(fileName, textInFile);

            return textInFile.Replace(searchText, replaceText).Count();
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            string textInFile = File.ReadAllText(fileName);
            string[] splitText = textInFile.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

            if (splitText == null)
            {
                throw new ArgumentNullException();
            }

            if (splitText.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return splitText.Where(x => x.Contains(searchText)).ToArray();

        }
    }
}
