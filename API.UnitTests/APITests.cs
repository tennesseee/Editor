using Editor;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace EditorTests
{
    public class Tests
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
        private readonly string _textFromFile = "some text";

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
            int resultOfMethod = 2;
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(_fileName))
                .Returns(false);

            _fileMock
                .Setup(f => f.ReadAllText(_fileName))
                .Returns(_textFromFile);

            _fileMock
                .Setup(f => f.WriteAllText(_fileName, _textFromFile));

            Assert.AreEqual(resultOfMethod, _api.FindAndReplace(_fileName, _searchText, _replaceText));
        }

        [Test]
        public void GetFilesNamesInStorage_ShouldReturnFilesFromStorage()
        {
            string[] filesArray = new[] { "asd", "sad" };

            _directoryMock
                .Setup(f => f.GetCurrentDirectory())
                .Returns(_directory);

            _directoryMock
                .Setup(f => f.GetFiles(_directory, "*.txt"))
                .Returns(filesArray);

            Assert.That(filesArray, Is.EqualTo(_api.GetFileNamesInStorage()));
        }
    }
}