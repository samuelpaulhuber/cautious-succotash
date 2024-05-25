namespace DeviceDataProcessor.Tests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public async Task ReadAsync_DeviceData1_Success()
        {
            // Arrange
            string filePath = "TestData/DeviceDataFoo1.json";

            // Act
            DeviceType1Data result = await JsonFileReader.ReadAsync<DeviceType1Data>(filePath);

            // Assert
            Assert.IsNotNull(result);
        }

        // test for object values and structure after deserialization for type 1

        [TestMethod]
        public async Task ReadAsync_DeviceData2_Success()
        {
            // Arrange
            string filePath = "TestData/DeviceDataFoo2.json";

            // Act
            DeviceType2Data result = await JsonFileReader.ReadAsync<DeviceType2Data>(filePath);

            // Assert
            Assert.IsNotNull(result);
        }

        // test for object values and structure after deserialization for type 1

        [TestMethod]
        public async Task ReadAsync_FileNotFound_Exception()
        {
            // Arrange
            string filePath = "TestData/fileDoesNotExist.json";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(async () =>
            {
                await JsonFileReader.ReadAsync<object>(filePath);
            });
        }

        [TestMethod]
        public async Task ReadAsync_EmptyFile_Exception()
        {
            // Arrange
            string filePath = "TestData/empty.json";

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                await JsonFileReader.ReadAsync<object>(filePath);
            });
        }
    }
}