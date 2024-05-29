using DeviceDataProcessor.Converters;

namespace DeviceDataProcessor.Tests
{
    [TestClass]
    public class DeviceType1ConverterTests
    {
        [TestMethod]
        public void ToDeviceCommonData_Success()
        {
            // Arrange
            var data = new DeviceType1Data
            {
                PartnerId = 1,
                PartnerName = "Foo1",
                Trackers = new List<Tracker>
                {
                    new Tracker
                    {
                        Id = 1,
                        Model = "ABC-100",
                        Sensors = new List<Sensor>
                        {
                            new Sensor
                            {
                                Name = "Temperature",
                                Crumbs = new List<Crumb>
                                {
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:35:00"), Value = 23.1 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:40:00"), Value = 23.2 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:45:00"), Value = 23.3 }
                                }
                            },
                            new Sensor
                            {
                                Name = "Humidty",
                                Crumbs = new List<Crumb>
                                {
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:35:00"), Value = 81.5 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:40:00"), Value = 82.5 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:45:00"), Value = 83.5 }
                                }
                            }
                        }
                    },
                    new Tracker
                    {
                        Id = 2,
                        Model = "ABC-200",
                        Sensors = new List<Sensor>
                        {
                            new Sensor
                            {
                                Name = "Temperature",
                                Crumbs = new List<Crumb>
                                {
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:35:00"), Value = 23.1 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:40:00"), Value = 23.2 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:45:00"), Value = 23.3 }
                                }
                            },
                            new Sensor
                            {
                                Name = "Humidty",
                                Crumbs = new List<Crumb>
                                {
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:35:00"), Value = 81.5 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:40:00"), Value = 82.5 },
                                    new Crumb { CreatedDtm = DateTime.Parse("2020-08-17T10:45:00"), Value = 83.5 }
                                }
                            }
                        }
                    }
                }
            };

            var converter = new DeviceType1Converter();

            // Act
            var result = converter.ToDeviceCommonData(data);

            // Assert
            Assert.AreEqual(2, result.Count);

            // Device 1
            var device1Data = result[0];
            Assert.AreEqual(data.PartnerId, device1Data.CompanyId);
            Assert.AreEqual(data.PartnerName, device1Data.CompanyName);
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
            Assert.AreEqual(data.PartnerId, device2Data.CompanyId);
            Assert.AreEqual(data.PartnerName, device2Data.CompanyName);
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
