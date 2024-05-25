using DeviceDataProcessor.Utility;
using System.Text.Json;

public static class JsonFileReader
{
    public static async Task<T?> ReadAsync<T>(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        if(new FileInfo(filePath).Length == 0)
            throw new InvalidOperationException($"File contains no data: {filePath}");

        // Add DateTimeConverter to handle "08-17-2020 10:35:00" format
        var options = new JsonSerializerOptions
        {
            Converters = { new DateTimeConverter() }
        };

        using FileStream stream = File.OpenRead(filePath);

        return await JsonSerializer.DeserializeAsync<T>(stream, options);
    }
}

