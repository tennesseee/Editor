using System;
using System.IO;
using System.Linq;

namespace Editor
{
    public class API
    {
        public void CopyFileToStorage(string filePath)
        {
            try
            {
                if (filePath == null && File.Exists(filePath))
                {
                    throw new Exception("File path is not correct or file already exists");
                }
                else
                {
                    string directory = Directory.GetCurrentDirectory();
                    string newFile = Path.Combine(directory, "CreatedFile.txt");

                    File.Copy(filePath, newFile);
                }
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

            if (splitText == null || splitText.Length == 0)
            {
                throw new Exception("Can't operate with the file");
            }
            else
            {
                return (string[])splitText.Where(x => x.Contains(searchText));
            }
        }
    }
}
