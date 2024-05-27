
using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Tests
{
    [TestClass]
    public class DataProcessorTests
    {
        [TestMethod]
        public async Task ProcessDeviceType1Data_Success()
        {
            // Arrange
            var data = new List<DeviceCommonData>
            {
                new DeviceCommonData { 
                    CompanyId = 1,
                    CompanyName = "Foo1",
                    DeviceId = 1,
                    DeviceName = "ABC-100",
                    FirstReadingDtm = DateTime.Parse("2020-08-17T10:35:00"),
                    LastReadingDtm = DateTime.Parse("2020-08-17T10:45:00"),
                    TemperatureCount = 3,
                    AverageTemperature = 23.149999999999995,
                    HumidityCount = 3,
                    AverageHumdity = 81.5
                },
                new DeviceCommonData
                {
                    CompanyId = 1,
                    CompanyName = "Foo1",
                    DeviceId = 2,
                    DeviceName = "ABC-200",
                    FirstReadingDtm = DateTime.Parse("2020-08-18T10:35:00"),
                    LastReadingDtm = DateTime.Parse("2020-08-18T10:45:00"),
                    TemperatureCount = 3,
                    AverageTemperature = 24.149999999999995,
                    HumidityCount = 3,
                    AverageHumdity = 82.5
                }
            };

            // Act
            var result = await DataProcessor.ProcessDeviceType1Data();

            // Assert
            Assert.AreEqual(2, result.Count());

            // Device 1
            var device1Data = data[0];
            Assert.AreEqual(device1Data.CompanyId, device1Data.CompanyId);
            Assert.AreEqual(device1Data.CompanyName, device1Data.CompanyName);
            Assert.AreEqual(device1Data.DeviceId, device1Data.DeviceId);
            Assert.AreEqual(device1Data.DeviceName, device1Data.DeviceName);
            Assert.AreEqual(device1Data.FirstReadingDtm, device1Data.FirstReadingDtm);
            Assert.AreEqual(device1Data.LastReadingDtm, device1Data.LastReadingDtm);
            Assert.AreEqual(device1Data.TemperatureCount, device1Data.TemperatureCount);
            Assert.AreEqual(device1Data.AverageTemperature, device1Data.AverageTemperature);
            Assert.AreEqual(device1Data.HumidityCount, device1Data.HumidityCount);
            Assert.AreEqual(device1Data.AverageHumdity, device1Data.AverageHumdity);

            // Device 2
            var device2Data = data[1];
            Assert.AreEqual(device2Data.CompanyId, device2Data.CompanyId);
            Assert.AreEqual(device2Data.CompanyName, device2Data.CompanyName);
            Assert.AreEqual(device2Data.DeviceId, device2Data.DeviceId);
            Assert.AreEqual(device2Data.DeviceName, device2Data.DeviceName);
            Assert.AreEqual(device2Data.FirstReadingDtm, device2Data.FirstReadingDtm);
            Assert.AreEqual(device2Data.LastReadingDtm, device2Data.LastReadingDtm);
            Assert.AreEqual(device2Data.TemperatureCount, device2Data.TemperatureCount);
            Assert.AreEqual(device2Data.AverageTemperature, device2Data.AverageTemperature);
            Assert.AreEqual(device2Data.HumidityCount, device2Data.HumidityCount);
            Assert.AreEqual(device2Data.AverageHumdity, device2Data.AverageHumdity);
        }

        [TestMethod]
        public async Task ProcessDeviceType2Data_Success()
        {
            // Arrange
            var data = new List<DeviceCommonData>
            {
                new DeviceCommonData {
                    CompanyId = 2,
                    CompanyName = "Foo2",
                    DeviceId = 1,
                    DeviceName = "XYZ-100",
                    FirstReadingDtm = DateTime.Parse("2020-08-18T10:35:00"),
                    LastReadingDtm = DateTime.Parse("2020-08-18T10:45:00"),
                    TemperatureCount = 3,
                    AverageTemperature = 33.15,
                    HumidityCount = 3,
                    AverageHumdity = 91.5
                },
                new DeviceCommonData
                {
                    CompanyId = 2,
                    CompanyName = "Foo2",
                    DeviceId = 2,
                    DeviceName = "XYZ-200",
                    FirstReadingDtm = DateTime.Parse("2020-08-19T10:35:00"),
                    LastReadingDtm = DateTime.Parse("2020-08-19T10:45:00"),
                    TemperatureCount = 3,
                    AverageTemperature = 43.15,
                    HumidityCount = 3,
                    AverageHumdity = 92.5
                }
            };

            // Act
            var result = await DataProcessor.ProcessDeviceType1Data();

            // Assert
            Assert.AreEqual(2, result.Count());

            // Device 1
            var device1Data = data[0];
            Assert.AreEqual(device1Data.CompanyId, device1Data.CompanyId);
            Assert.AreEqual(device1Data.CompanyName, device1Data.CompanyName);
            Assert.AreEqual(device1Data.DeviceId, device1Data.DeviceId);
            Assert.AreEqual(device1Data.DeviceName, device1Data.DeviceName);
            Assert.AreEqual(device1Data.FirstReadingDtm, device1Data.FirstReadingDtm);
            Assert.AreEqual(device1Data.LastReadingDtm, device1Data.LastReadingDtm);
            Assert.AreEqual(device1Data.TemperatureCount, device1Data.TemperatureCount);
            Assert.AreEqual(device1Data.AverageTemperature, device1Data.AverageTemperature);
            Assert.AreEqual(device1Data.HumidityCount, device1Data.HumidityCount);
            Assert.AreEqual(device1Data.AverageHumdity, device1Data.AverageHumdity);

            // Device 2
            var device2Data = data[1];
            Assert.AreEqual(device2Data.CompanyId, device2Data.CompanyId);
            Assert.AreEqual(device2Data.CompanyName, device2Data.CompanyName);
            Assert.AreEqual(device2Data.DeviceId, device2Data.DeviceId);
            Assert.AreEqual(device2Data.DeviceName, device2Data.DeviceName);
            Assert.AreEqual(device2Data.FirstReadingDtm, device2Data.FirstReadingDtm);
            Assert.AreEqual(device2Data.LastReadingDtm, device2Data.LastReadingDtm);
            Assert.AreEqual(device2Data.TemperatureCount, device2Data.TemperatureCount);
            Assert.AreEqual(device2Data.AverageTemperature, device2Data.AverageTemperature);
            Assert.AreEqual(device2Data.HumidityCount, device2Data.HumidityCount);
            Assert.AreEqual(device2Data.AverageHumdity, device2Data.AverageHumdity);
        }
    }
}


