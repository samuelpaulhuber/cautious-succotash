using DeviceDataProcessor.Converters;

namespace DeviceDataProcessor.Tests
{
    [TestClass]
    public class DeviceType2ConverterTests
    {
        [TestMethod]
        public void ToDeviceCommonData_Success()
        {
            // Arrange
            var data = new DeviceType2Data
            {
                CompanyId = 1,
                Company = "Foo1",
                Devices = new List<Device>
                {
                    new Device
                    {
                        DeviceID = 1,
                        Name = "ABC-100",
                        ReportedSensorData = new List<SensorData>
                        {
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:35:00"), Value = 23.1 },
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:40:00"), Value = 23.2 },
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:45:00"), Value = 23.3 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:35:00"), Value = 81.5 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:40:00"), Value = 82.5 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:45:00"), Value = 83.5 }
                        }
                    },
                    new Device
                    {
                        DeviceID = 2,
                        Name = "ABC-200",
                        ReportedSensorData = new List<SensorData>
                        {
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:35:00"), Value = 23.1 },
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:40:00"), Value = 23.2 },
                            new SensorData { SensorType = "TEMP", ReportedDate = DateTime.Parse("2020-08-17T10:45:00"), Value = 23.3 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:35:00"), Value = 81.5 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:40:00"), Value = 82.5 },
                            new SensorData { SensorType = "HUM", ReportedDate = DateTime.Parse("2020-08-17T10:45:00"), Value = 83.5 }
                        }
                    }
                }
            };

            var converter = new DeviceType2Converter();

            // Act
            var result = converter.ToDeviceCommonData(data);

            // Assert
            Assert.AreEqual(2, result.Count);

            // Device 1
            var device1Data = result[0];
            Assert.AreEqual(data.CompanyId, device1Data.CompanyId);
            Assert.AreEqual(data.Company, device1Data.CompanyName);
            Assert.AreEqual(1, device1Data.DeviceId);
            Assert.AreEqual("ABC-100", device1Data.DeviceName);
            Assert.AreEqual(DateTime.Parse("2020-08-17T10:35:00"), device1Data.FirstReadingDtm);
            Assert.AreEqual(DateTime.Parse("2020-08-17T10:45:00"), device1Data.LastReadingDtm);
            Assert.AreEqual(3, device1Data.TemperatureCount);
            Assert.AreEqual(23.2, device1Data.AverageTemperature);
            Assert.AreEqual(3, device1Data.HumidityCount);
            Assert.AreEqual(82.5, device1Data.AverageHumdity);

            // Device 2
            var device2Data = result[1];
            Assert.AreEqual(data.CompanyId, device2Data.CompanyId);
            Assert.AreEqual(data.Company, device2Data.CompanyName);
            Assert.AreEqual(2, device2Data.DeviceId);
            Assert.AreEqual("ABC-200", device2Data.DeviceName);
            Assert.AreEqual(DateTime.Parse("2020-08-17T10:35:00"), device2Data.FirstReadingDtm);
            Assert.AreEqual(DateTime.Parse("2020-08-17T10:45:00"), device2Data.LastReadingDtm);
            Assert.AreEqual(3, device2Data.TemperatureCount);
            Assert.AreEqual(23.2, device2Data.AverageTemperature);
            Assert.AreEqual(3, device2Data.HumidityCount);
            Assert.AreEqual(82.5, device2Data.AverageHumdity);
        }
    }
}
