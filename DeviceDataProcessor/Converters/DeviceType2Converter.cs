using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Converters
{
    public class DeviceType2Converter : IDataConverter<DeviceType2Data>
    {
        public List<DeviceCommonData> ToDeviceCommonData(DeviceType2Data data)
        {
            var allDeviceCommonData = new List<DeviceCommonData>();
            
            if(data == null)
            {
                throw new Exception("No data found for the device type 2 data");
            }

            if (data.Devices == null || data.Devices.Count == 0)
            {
                throw new Exception("No devices found for the device type 2 data");
            }

            foreach(var device in data.Devices)
            {
                DeviceCommonData deviceCommonData = new DeviceCommonData
                {
                    CompanyId = data.CompanyId,
                    CompanyName = data.Company
                };

                deviceCommonData.DeviceId = device.DeviceID;
                deviceCommonData.DeviceName = device.Name;

                if(device.ReportedSensorData == null || device.ReportedSensorData.Count == 0)
                {
                    throw new Exception("No sensor data found for the device type 2 data");
                }

                deviceCommonData.FirstReadingDtm = device.ReportedSensorData.Min(c => c.ReportedDate);
                deviceCommonData.LastReadingDtm = device.ReportedSensorData.Max(c => c.ReportedDate);

                var tempatures = device.ReportedSensorData.Where(s => s.SensorType == "TEMP").ToList();
                if(tempatures is not null && tempatures.Count > 0)
                {
                    deviceCommonData.TemperatureCount = tempatures.Count;
                    deviceCommonData.AverageTemperature = tempatures.Average(c => c.Value);
                }

                var humidities = device.ReportedSensorData.Where(s => s.SensorType == "HUM").ToList();
                if (humidities is not null && humidities.Count > 0)
                {
                    deviceCommonData.HumidityCount = humidities.Count;
                    deviceCommonData.AverageHumdity = humidities.Average(c => c.Value);
                }

                allDeviceCommonData.Add(deviceCommonData);
            }

            return allDeviceCommonData;
        }
    }
}
