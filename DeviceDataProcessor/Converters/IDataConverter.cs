using DeviceDataProcessor.Models;

namespace DeviceDataProcessor.Converters
{
    public interface IDataConverter<T>
    {
        public List<DeviceCommonData> ToDeviceCommonData(T data);
    }
}
