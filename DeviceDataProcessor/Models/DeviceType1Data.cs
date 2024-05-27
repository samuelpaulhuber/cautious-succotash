using DeviceDataProcessor.Models;

public class DeviceType1Data
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; }
    public List<Tracker> Trackers { get; set; }

    public List<DeviceCommonData> ToDeviceCommonData()
    {
        var allDeviceCommonData = new List<DeviceCommonData>();

        if (Trackers == null || Trackers.Count == 0)
        {
            throw new Exception("No trackers found for the device type 1 data");
        }

        foreach (Tracker tracker in Trackers)
        {
            DeviceCommonData deviceCommonData = new DeviceCommonData
            {
                CompanyId = PartnerId,
                CompanyName = PartnerName
            };

            deviceCommonData.DeviceId = tracker.Id;
            deviceCommonData.DeviceName = tracker.Model;

            var allSensorData = tracker.Sensors.SelectMany(s => s.Crumbs).ToList();

            if(allSensorData is null || allSensorData.Count == 0)
            {
                throw new Exception("No sensor data found for the device type 1 data");
            }

            deviceCommonData.FirstReadingDtm = allSensorData.Min(c => c.CreatedDtm);
            deviceCommonData.LastReadingDtm = allSensorData.Max(c => c.CreatedDtm);

            var tempatures = tracker.Sensors.Where(s => s.Name == "Temperature").SelectMany(s => s.Crumbs).ToList();
            if(tempatures is not null && tempatures.Count > 0)
            {
                deviceCommonData.TemperatureCount = tempatures.Count;
                deviceCommonData.AverageTemperature = tempatures.Average(c => c.Value);
            }

            var humidities = tracker.Sensors.Where(s => s.Name == "Humidty").SelectMany(s => s.Crumbs).ToList();
            if(humidities is not null && humidities.Count > 0)
            {
                deviceCommonData.HumidityCount = humidities.Count;
                deviceCommonData.AverageHumdity = humidities.Average(c => c.Value);
            }

            allDeviceCommonData.Add(deviceCommonData);
        }

        return allDeviceCommonData;
    }
}

public class Crumb
{
    public DateTime CreatedDtm { get; set; }
    public double Value { get; set; }
}

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Crumb> Crumbs { get; set; }
}

public class Tracker
{
    public int Id { get; set; }
    public string Model { get; set; }
    public DateTime ShipmentStartDtm { get; set; }
    public List<Sensor> Sensors { get; set; }
}