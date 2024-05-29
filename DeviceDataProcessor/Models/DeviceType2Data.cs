using System.Text.Json.Serialization;

public class DeviceType2Data
{
    public int CompanyId { get; set; }
    public string Company { get; set; }
    public List<Device> Devices { get; set; }
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