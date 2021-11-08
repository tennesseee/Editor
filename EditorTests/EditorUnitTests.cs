using Editor;
using NUnit.Framework;

namespace EditorTests
{
    public class Tests
    {
        [Test]
        public void CopyFileToStorageTest_ShouldThrowException(string filePath)
        {
            //Arrange 
            API api = new API();

            //Act
            api.CopyFileToStorage(filePath);

            //Assert
            Assert.Throws()
        }
    }
}