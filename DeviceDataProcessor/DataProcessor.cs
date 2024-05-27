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

        var deviceType1Data = await DeserializeDeviceType1Data();
        var deviceType1CommonData = ConvertType1DataToCommonData(deviceType1Data);

        commonData.AddRange(deviceType1CommonData);
        //var deviceType2CommonData = await GetDeviceType2ConvertedCommonData();

        //var commonData = new List<DeviceCommonData> { deviceType1CommonData };//, deviceType2CommonData };
        var commonDataJson = JsonSerializer.Serialize(commonData);

        await File.WriteAllTextAsync("Data/CommonData.json", commonDataJson);
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
      
    //private static async Task<DeviceType1Data> DeserializeDeviceType2Data()
    //{
    //    DeviceType1Data? deviceType1Data = await JsonFileReader.ReadAsync<DeviceType1Data>(DeviceDataFoo1FilePath);
    //    if (deviceType1Data == null)
    //    {
    //        throw new InvalidOperationException("DeviceType1Data is null");
    //    }
    //    return deviceType1Data;
    //}

    //private static List<DeviceCommonData> ConvertType2DataToCommonData(DeviceType2Data data)
    //{
    //    DeviceType2Converter DeviceType2Converter = new DeviceType2Converter();
    //    return DeviceType1Converter.ToDeviceCommonData(data);
    //}
}