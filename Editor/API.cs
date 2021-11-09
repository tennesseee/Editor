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
                if (_fileWrapper.Exists(null))
                {
                    throw new ArgumentNullException();
                }

                if (_fileWrapper.Exists(filePath))
                {
                    throw new ArgumentOutOfRangeException();
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

                if (files == null)
                {
                    throw new ArgumentNullException();
                }
                
                return files;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int FindAndReplace(string fileName, string searchText, string replaceText)
        {
            string textInFile = _fileWrapper.ReadAllText(fileName);
            textInFile = textInFile.Replace(searchText, replaceText);
            _fileWrapper.WriteAllText(fileName, textInFile);

            return textInFile.Replace(searchText, replaceText).Count();
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            string textInFile = _fileWrapper.ReadAllText(fileName);
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
