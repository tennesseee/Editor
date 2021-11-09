using System;
using System.IO;
using System.Linq;

namespace Editor
{
    public class API
    {
        private readonly IFile _fileWrapper;
        private readonly IDirectory _directoryWrapper;

        public API(IFile fileWrapper, IDirectory directoryWrapper)
        {
            _fileWrapper = fileWrapper;
            _directoryWrapper = directoryWrapper;
        }

        public void CopyFileToStorage(string filePath, string fileName)
        {
            try
            {
                if (_fileWrapper.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentNullException();
                }

                string directory = _directoryWrapper.GetCurrentDirectory();
                string newFile = _directoryWrapper.Combine(directory, fileName);

                _fileWrapper.CopyFile(filePath, newFile, false);
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
