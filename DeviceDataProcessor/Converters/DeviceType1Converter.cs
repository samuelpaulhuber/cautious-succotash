using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Converters
{
    public class DeviceType1Converter : IDataConverter<DeviceType1Data>
    {
        public List<DeviceCommonData> ToDeviceCommonData(DeviceType1Data data)
        {
            var allDeviceCommonData = new List<DeviceCommonData>();
            var trackers = data.Trackers;

            if (trackers == null || trackers.Count == 0)
            {
                throw new Exception("No trackers found for the device type 1 data");
            }

            foreach (Tracker tracker in trackers)
            {
                DeviceCommonData deviceCommonData = new DeviceCommonData
                {
                    CompanyId = data.PartnerId,
                    CompanyName = data.PartnerName
                };

                deviceCommonData.DeviceId = tracker.Id;
                deviceCommonData.DeviceName = tracker.Model;

                var allSensorData = tracker.Sensors.SelectMany(s => s.Crumbs).ToList();

                if (allSensorData is null || allSensorData.Count == 0)
                {
                    throw new Exception("No sensor data found for the device type 1 data");
                }

                deviceCommonData.FirstReadingDtm = allSensorData.Min(c => c.CreatedDtm);
                deviceCommonData.LastReadingDtm = allSensorData.Max(c => c.CreatedDtm);

                var tempatures = tracker.Sensors.Where(s => s.Name == "Temperature").SelectMany(s => s.Crumbs).ToList();
                if (tempatures is not null && tempatures.Count > 0)
                {
                    deviceCommonData.TemperatureCount = tempatures.Count;
                    deviceCommonData.AverageTemperature = tempatures.Average(c => c.Value);
                }

                var humidities = tracker.Sensors.Where(s => s.Name == "Humidty").SelectMany(s => s.Crumbs).ToList();
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
