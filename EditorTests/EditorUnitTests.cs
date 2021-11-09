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
        private readonly string _newFile = Path.Combine()

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
            _fileMock.VerifyAll();
        }

        [Test]
        public void CopyFileToStorageTestWithNull_ShouldThrowException()
        {
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(null))
                .Returns(true);

            Assert.That(() => _api.CopyFileToStorage(null, _fileName), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void CopyFileToStorageTestWithEmptyString_ShouldThrowException()
        {
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(""))
                .Returns(true);

            Assert.That(() => _api.CopyFileToStorage("", _fileName), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void CopyFileToStorageTest_ShouldCopyFileToGivenDestination()
        {
            _fileMock
                .Setup(f => f.IsNullOrWhiteSpace(_filePath))
                .Returns(false);

            _fileMock
                .Setup(f => f.CopyFile(_filePath, _fileName, false));

            _directoryMock
                .Setup(f => f.GetCurrentDirectory());
            
            _directoryMock
                .Setup(f => f.Combine(_directory, _fileName));
                

            _api.CopyFileToStorage(_filePath, _fileName);
        }
    }
}