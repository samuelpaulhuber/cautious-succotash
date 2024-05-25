using DeviceDataProcessor.Models;

public class DeviceType1Data
{
    public int PartnerId { get; set; }
    public string PartnerName { get; set; }
    public List<Tracker> Trackers { get; set; }

    public DeviceCommonData ToDeviceCommonData()
    {
        DeviceCommonData deviceCommonData = new DeviceCommonData
        {
            CompanyId = PartnerId,
            CompanyName = PartnerName
        };

        foreach (Tracker tracker in Trackers)
        {
            foreach (Sensor sensor in tracker.Sensors)
            {
                foreach (Crumb crumb in sensor.Crumbs)
                {
                    if (deviceCommonData.FirstReadingDtm == null || crumb.CreatedDtm < deviceCommonData.FirstReadingDtm)
                    {
                        deviceCommonData.FirstReadingDtm = crumb.CreatedDtm;
                    }

                    if (deviceCommonData.LastReadingDtm == null || crumb.CreatedDtm > deviceCommonData.LastReadingDtm)
                    {
                        deviceCommonData.LastReadingDtm = crumb.CreatedDtm;
                    }

                    if (sensor.Name == "Temperature")
                    {
                        deviceCommonData.TemperatureCount++;
                        deviceCommonData.AverageTemperature += crumb.Value;
                    }
                    else if (sensor.Name == "Humidity")
                    {
                        deviceCommonData.HumidityCount++;
                        deviceCommonData.AverageHumdity += crumb.Value;
                    }
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