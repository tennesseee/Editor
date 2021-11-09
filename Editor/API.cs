using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
                string directory = _directoryWrapper.GetCurrentDirectory();
                string[] files = _directoryWrapper.GetFiles(directory, "*.txt");

                if (files == null || files.Length == 0)
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
            try
            {
                if (_fileWrapper.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentNullException();
                }

                string textInFile = _fileWrapper.ReadAllText(fileName);
                int counter = textInFile.Split(searchText).Count() - 1;
                string textAfterReplacement = textInFile.Replace(searchText, replaceText);

                _fileWrapper.WriteAllText(fileName, textAfterReplacement);


                return counter;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string[] SearchParagraphs(string fileName, string searchText)
        {
            string textInFile = _fileWrapper.ReadAllText(fileName);
            string[] splitText = textInFile.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

            if (splitText == null || splitText.Length == 0)
            {
                throw new ArgumentNullException();
            }

            string[] v = splitText.Where(x => x.Contains(searchText)).ToArray();

            return v;
        }
    }
}
