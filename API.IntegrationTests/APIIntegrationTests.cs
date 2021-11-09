using NUnit.Framework;
using Editor;
using System.IO;

namespace API.IntegrationTests
{
    public class APIIntegrationTests
    {
        private readonly IFile _fileWrapper = new FileWrapper();
        private readonly IDirectory _directoryWrapper = new DirectoryWrapper();
        private readonly string _fileName = "result.txt";
        private readonly string _filePath = @"C:\Users\Anton\Desktop\testFile.txt";

        [Test]
        public void CopyFileToStorageIntegrationTest_ShouldCopyGiveFileToPath()
        {
            Editor.API api = new Editor.API(_fileWrapper, _directoryWrapper);
            string pathForNewFile = _directoryWrapper.Combine(_directoryWrapper.GetCurrentDirectory(), _fileName);

            api.CopyFileToStorage(_filePath, pathForNewFile);

            Assert.That(_fileWrapper.Exists(pathForNewFile));
        }

        [Test]
        public void SearchParagraphsIntegrationTest_ShouldReturnArrayOfFiles()
        {
            string[] result = new[] { "Andora", "Estonia" };
            Editor.API api = new Editor.API(_fileWrapper, _directoryWrapper);

            Assert.That(result, Is.EqualTo(api.SearchParagraphs(_filePath, "a")));
        }

        [Test]
        public void FindAndReplace_ShouldReturnCountOfReplaces()
        {
            int result = 1;
            Editor.API api = new Editor.API(_fileWrapper, _directoryWrapper);

            Assert.That(result, Is.EqualTo(api.FindAndReplace(_filePath, "Mexico", "Guatemala")));
        }
    }
}