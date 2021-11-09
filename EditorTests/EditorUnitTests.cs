using Editor;
using Moq;
using NUnit.Framework;
using System;

namespace EditorTests
{
    public class Tests
    {
        private Mock<IFile> _fileMock;
        private API _api;

        [SetUp]
        public void Setup()
        {
            _fileMock = new Mock<IFile>(MockBehavior.Strict);
            _api = new API(_fileMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _fileMock.VerifyAll();
        }
        
        [Test]
        public void CopyFileToStorageTestWithInvalidPath_ShouldThrowException()
        {
            // Arrange
            const string filePath = "asssda";
            _fileMock
                .Setup(f => f.Exists(filePath))
                .Returns(true);

            // Assert
            Assert.That(() => _api.CopyFileToStorage(filePath), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CopyFileToStorageTestWithNull_ShouldThrowException()
        {
            // Arrange
            _fileMock
                .Setup(f => f.Exists(null))
                .Returns(true);

            // Assert
            Assert.That(() => _api.CopyFileToStorage(null), Throws.TypeOf<ArgumentNullException>());
        }




    }
}