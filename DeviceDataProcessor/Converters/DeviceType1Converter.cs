using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Converters
{
    public class DeviceType1Converter : IDataConverter<DeviceType1Data>
    {
        /// <summary>
        /// Converts device type 1 data to common data
        /// </summary>
        /// <param name="data">DeviceType1Data</param>
        /// <returns>List<DeviceCommonData></returns>
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
                var deviceCommonData = InitializeCommonDataModel(data, tracker);
                var allSensorData = GetDeviceSensorData(tracker);

                deviceCommonData.FirstReadingDtm = allSensorData.Min(c => c.CreatedDtm);
                deviceCommonData.LastReadingDtm = allSensorData.Max(c => c.CreatedDtm);

                var tempatureData = GetDeviceTempatureData(tracker);
                SetCommonDataTempatureValues(deviceCommonData, tempatureData);

                var humidityData = GetDeviceHumidityData(tracker);
                SetCommonDataHumidityValues(deviceCommonData, humidityData);

                allDeviceCommonData.Add(deviceCommonData);
            }

            return allDeviceCommonData;
        }

        private DeviceCommonData InitializeCommonDataModel(DeviceType1Data data, Tracker tracker)
        {
            DeviceCommonData deviceCommonData = new DeviceCommonData
            {
                CompanyId = data.PartnerId,
                CompanyName = data.PartnerName,
                DeviceId = tracker.Id,
                DeviceName = tracker.Model
            };

            return deviceCommonData;
        }

        private List<Crumb> GetDeviceSensorData(Tracker tracker)
        {
            var allSensorData = tracker.Sensors.SelectMany(s => s.Crumbs).ToList();

            if (allSensorData is null || allSensorData.Count == 0)
            {
                throw new Exception("No sensor data found for the device type 1 data");
            }

            return allSensorData;
        }
                
        private List<Crumb>? GetDeviceTempatureData(Tracker tracker)
        {
            var tempatures = tracker.Sensors.Where(s => s.Name == "Temperature").SelectMany(s => s.Crumbs).ToList();

            return tempatures;
        }

        private void SetCommonDataTempatureValues(DeviceCommonData deviceCommonData, List<Crumb>? tempatureData)
        {
            if (tempatureData is not null && tempatureData.Count > 0)
            {
                deviceCommonData.TemperatureCount = tempatureData.Count;
                deviceCommonData.AverageTemperature = Math.Round(tempatureData.Average(c => c.Value), 2);
            }
        }

        private List<Crumb>? GetDeviceHumidityData(Tracker tracker)
        {
            var humidities = tracker.Sensors.Where(s => s.Name == "Humidty").SelectMany(s => s.Crumbs).ToList();

            return humidities;
        }

        private void SetCommonDataHumidityValues(DeviceCommonData deviceCommonData, List<Crumb>? humidityData)
        {
            if (humidityData is not null && humidityData.Count > 0)
            {
                deviceCommonData.HumidityCount = humidityData.Count;
                deviceCommonData.AverageHumdity = Math.Round(humidityData.Average(c => c.Value), 2);
            }
        }
    }
}
