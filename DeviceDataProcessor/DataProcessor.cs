using DeviceDataProcessor.Converters;
using DeviceDataProcessor.Models;
using System.Text.Json;

public static class DataProcessor
{
    private const string DeviceDataFoo1FilePath = "Data/DeviceDataFoo1.json";
    private const string DeviceDataFoo2FilePath = "Data/DeviceDataFoo2.json";
    public static async Task CreateCommonDataFile()
    {
        var commonData = new List<DeviceCommonData>();
        commonData.AddRange(await ProcessDeviceType1Data());
        commonData.AddRange(await ProcessDeviceType2Data());

        var commonDataJson = JsonSerializer.Serialize(commonData);
        await File.WriteAllTextAsync("Data/CommonData.json", commonDataJson);
    }

    public static async Task<List<DeviceCommonData>> ProcessDeviceType1Data()
    {
        var deviceType1Data = await DeserializeDeviceType1Data();
        var deviceType1CommonData = ConvertType1DataToCommonData(deviceType1Data);

        return deviceType1CommonData;
    }
        
    private static async Task<DeviceType1Data> DeserializeDeviceType1Data()
    {
        DeviceType1Data? deviceType1Data = await JsonFileReader.ReadAsync<DeviceType1Data>(DeviceDataFoo1FilePath);
        if (deviceType1Data == null)
        {
            throw new InvalidOperationException("DeviceType1Data is null");
        }
        return deviceType1Data;
    }

    private static List<DeviceCommonData> ConvertType1DataToCommonData(DeviceType1Data data)
    {
        DeviceType1Converter DeviceType1Converter = new DeviceType1Converter();
        return DeviceType1Converter.ToDeviceCommonData(data);
    }

    public static async Task<List<DeviceCommonData>> ProcessDeviceType2Data()
    {
        var deviceType2Data = await DeserializeDeviceType2Data();
        var deviceType2CommonData = ConvertType2DataToCommonData(deviceType2Data);

        return deviceType2CommonData;
    }

    private static async Task<DeviceType2Data> DeserializeDeviceType2Data()
    {
        DeviceType2Data? deviceType2Data = await JsonFileReader.ReadAsync<DeviceType2Data>(DeviceDataFoo2FilePath);
        if (deviceType2Data == null)
        {
            throw new InvalidOperationException("DeviceType2Data is null");
        }
        return deviceType2Data;
    }

    private static List<DeviceCommonData> ConvertType2DataToCommonData(DeviceType2Data data)
    {
        DeviceType2Converter DeviceType2Converter = new DeviceType2Converter();
        return DeviceType2Converter.ToDeviceCommonData(data);
    }
}