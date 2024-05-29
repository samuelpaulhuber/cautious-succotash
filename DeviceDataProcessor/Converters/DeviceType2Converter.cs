using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Converters
{
    public class DeviceType2Converter : IDataConverter<DeviceType2Data>
    {
        public List<DeviceCommonData> ToDeviceCommonData(DeviceType2Data data)
        {
            var allDeviceCommonData = new List<DeviceCommonData>();
            
            ValidateDataStructure(data);

            foreach(var device in data.Devices)
            {
                ValidateSensorData(device.ReportedSensorData);
                var deviceCommonData = InitializeCommonDataModel(data, device);

                deviceCommonData.FirstReadingDtm = device.ReportedSensorData.Min(c => c.ReportedDate);
                deviceCommonData.LastReadingDtm = device.ReportedSensorData.Max(c => c.ReportedDate);

                var tempatures = GetDeviceTempatureData(device);
                SetCommonDataTempatureValues(deviceCommonData, tempatures);

                var humidities = GetDeviceHumidityData(device);
                SetCommonDataHumidityValues(deviceCommonData, humidities);

                allDeviceCommonData.Add(deviceCommonData);
            }

            return allDeviceCommonData;
        }

        private void ValidateDataStructure(DeviceType2Data data)
        {
            if (data == null)
            {
                throw new Exception("No data found for the device type 2 data");
            }

            if (data.Devices == null || data.Devices.Count == 0)
            {
                throw new Exception("No devices found for the device type 2 data");
            }
        }

        private void ValidateSensorData(List<SensorData> sensorData)
        {
            if (sensorData == null || sensorData.Count == 0)
            {
                throw new Exception("No sensor data found for the device type 2 data");
            }
        }
        
        private DeviceCommonData InitializeCommonDataModel(DeviceType2Data data, Device device)
        {
            DeviceCommonData deviceCommonData = new DeviceCommonData
            {
                CompanyId = data.CompanyId,
                CompanyName = data.Company,
                DeviceId = device.DeviceID,
                DeviceName = device.Name
            };

            return deviceCommonData;
        }

        private List<SensorData>? GetDeviceTempatureData(Device device)
        {
            var tempatures = device.ReportedSensorData.Where(s => s.SensorType == "TEMP").ToList();

            return tempatures;
        }

        private void SetCommonDataTempatureValues(DeviceCommonData deviceCommonData, List<SensorData>? tempatureData)
        {
            if (tempatureData is not null && tempatureData.Count > 0)
            {
                deviceCommonData.TemperatureCount = tempatureData.Count;
                deviceCommonData.AverageTemperature = Math.Round(tempatureData.Average(c => c.Value), 2);
            }
        }

        private List<SensorData>? GetDeviceHumidityData(Device device)
        {
            var humidities = device.ReportedSensorData.Where(s => s.SensorType == "HUM").ToList();

            return humidities;
        }

        private void SetCommonDataHumidityValues(DeviceCommonData deviceCommonData, List<SensorData>? humidityData)
        {
            if (humidityData is not null && humidityData.Count > 0)
            {
                deviceCommonData.HumidityCount = humidityData.Count;
                deviceCommonData.AverageHumdity = Math.Round(humidityData.Average(c => c.Value), 2);
            }
        }
    }
}
