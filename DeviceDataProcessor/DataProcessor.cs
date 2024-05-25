using DeviceDataProcessor.Models;
using System.Text.Json;

public static class DataProcessor
{
    private const string DeviceDataFoo1FilePath = "Data/DeviceDataFoo1.json";
    private const string DeviceDataFoo2FilePath = "Data/DeviceDataFoo2.json";

    public static async Task CreateCommonDataFile()
    {
        var deviceType1CommonData = await GetDeviceType1ConvertedCommonData();
        var deviceType2CommonData = await GetDeviceType2ConvertedCommonData();

        var commonData = new List<DeviceCommonData> { deviceType1CommonData, deviceType2CommonData };
        var commonDataJson = JsonSerializer.Serialize(commonData);

        await File.WriteAllTextAsync("Data/CommonData.json", commonDataJson);
    }

    private static async Task<DeviceCommonData> GetDeviceType1ConvertedCommonData()
    {
        DeviceType1Data? deviceType1Data = await JsonFileReader.ReadAsync<DeviceType1Data>(DeviceDataFoo1FilePath);
        if(deviceType1Data == null)
        {
            throw new InvalidOperationException("DeviceType1Data is null");
        }
        return deviceType1Data.ToDeviceCommonData();
    }

    private static async Task<DeviceCommonData> GetDeviceType2ConvertedCommonData()
    {
        DeviceType2Data? deviceType2Data = await JsonFileReader.ReadAsync<DeviceType2Data>(DeviceDataFoo2FilePath);
        if(deviceType2Data == null)
        {
            throw new InvalidOperationException("DeviceType2Data is null");
        }
        return deviceType2Data.ToDeviceCommonData();
    }
}