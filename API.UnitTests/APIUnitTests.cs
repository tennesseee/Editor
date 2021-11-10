using Editor;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace EditorTests
{
    public class APIUnitTests
    {
        private Mock<IFile> _fileMock;
        private API _api;
        private Mock<IDirectory> _directoryMock;
        private readonly string _fileName = "name";
        private readonly string _filePath = "/path";
        private readonly string _directory = "/directory";        
        private readonly string _searchText = "e";
        private readonly string _replaceText = "b";
        private readonly string _newFile = "sad";
        private readonly string _textFromFile = "some text\r\n mock";
        private readonly string[] _filesArray = new[] { "asd", "sad" };

        [SetUp]
        public void Setup()
        {
            _fileMock = new Mock<IFile>(MockBehavior.Strict);
            _directoryMock = new Mock<IDirectory>(MockBehavior.Strict);
            _api = new API(_fileMock.Object, _directoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _directoryMock.VerifyAll();
            _fileMock.VerifyAll();
        }

        [TestCase(null)]
        [TestCase("")]
        public void CopyFileToStorageTestForException_ShouldThrowException(string filePath)
        {
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(filePath))
                .Returns(true);

            Assert.That(() => _api.CopyFileToStorage(filePath, _fileName), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void CopyFileToStorageTest_ShouldCopyFileToGivenDestination()
        {             
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(_filePath))
                .Returns(false);

            _directoryMock
                .Setup(f => f.GetCurrentDirectory())
                .Returns(_directory);

            _directoryMock
                .Setup(f => f.Combine(_directory, _fileName))
                .Returns(_newFile);

            _fileMock
                .Setup(f => f.CopyFile(_filePath, _newFile, false));

            _api.CopyFileToStorage(_filePath, _fileName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void FindAndReplaceTestForException_ShouldThrowException(string testCase)
        {
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(testCase))
                .Returns(true);

            Assert.That(() => _api.FindAndReplace(testCase, _searchText, _replaceText), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void FindAndReplaceTest_ShouldReplaceTextInFile()
        {
            int expectedResult = 2;
            
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(_fileName))
                .Returns(false);

            _fileMock
                .Setup(f => f.ReadAllText(_fileName))
                .Returns(_textFromFile);

            string replacedText = _textFromFile.Replace(_searchText, _replaceText);

            _fileMock
                .Setup(f => f.WriteAllText(_fileName, replacedText));

            int result = _api.FindAndReplace(_fileName, _searchText, _replaceText);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetFilesNamesInStorage_ShouldReturnFilesFromStorage()
        { 
            _directoryMock
                .Setup(f => f.GetCurrentDirectory())
                .Returns(_directory);

            _directoryMock
                .Setup(f => f.GetFiles(_directory, "*.txt"))
                .Returns(_filesArray);

            string[] resultOfAMethod = _api.GetFileNamesInStorage();

            Assert.That(_filesArray, Is.EqualTo(resultOfAMethod));
        }

        [Test]
        public void SearchParagraphsTest_ShouldReturnArrayOfParagraphs()
        {
            string[] expectedResult = new[] { "some text" };
            string[] splitedText = _textFromFile.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);

            _fileMock
                .Setup(f => f.ReadAllText(_fileName))
                .Returns(_textFromFile);

            string[] result = _api.SearchParagraphs(_fileName, _searchText);

            Assert.That(expectedResult, Is.EqualTo(result));
        }
    }
}