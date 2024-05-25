using DeviceDataProcessor.Models;
using System.Text.Json.Serialization;

public class DeviceType2Data
{
    public int CompanyId { get; set; }
    public string Company { get; set; }
    public List<Device> Devices { get; set; }

    public DeviceCommonData ToDeviceCommonData()
    {
        DeviceCommonData deviceCommonData = new DeviceCommonData
        {
            CompanyId = CompanyId,
            CompanyName = Company
        };

        foreach (Device device in Devices)
        {
            foreach (SensorData sensorData in device.ReportedSensorData)
            {
                if (deviceCommonData.FirstReadingDtm == null || sensorData.ReportedDate < deviceCommonData.FirstReadingDtm)
                {
                    deviceCommonData.FirstReadingDtm = sensorData.ReportedDate;
                }

                if (deviceCommonData.LastReadingDtm == null || sensorData.ReportedDate > deviceCommonData.LastReadingDtm)
                {
                    deviceCommonData.LastReadingDtm = sensorData.ReportedDate;
                }

                if (sensorData.SensorType == "Temperature")
                {
                    deviceCommonData.TemperatureCount++;
                    deviceCommonData.AverageTemperature += sensorData.Value;
                }
                else if (sensorData.SensorType == "Humidity")
                {
                    deviceCommonData.HumidityCount++;
                    deviceCommonData.AverageHumdity += sensorData.Value;
                }
            }
        }

        if (deviceCommonData.TemperatureCount > 0)
        {
            deviceCommonData.AverageTemperature /= deviceCommonData.TemperatureCount;
        }

        if (deviceCommonData.HumidityCount > 0)
        {
            deviceCommonData.AverageHumdity /= deviceCommonData.HumidityCount;
        }

        return deviceCommonData;
    }
}

public class Device
{
    public int DeviceID { get; set; }
    public string Name { get; set; }
    public DateTime StartDateTime { get; set; }
    [JsonPropertyName("SensorData")]
    public List<SensorData> ReportedSensorData { get; set; }
}

public class SensorData
{
    public string SensorType { get; set; }
    [JsonPropertyName("DateTime")]
    public DateTime ReportedDate { get; set; }
    public double Value { get; set; }
}