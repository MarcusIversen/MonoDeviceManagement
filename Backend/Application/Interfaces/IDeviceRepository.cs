using Domain;

namespace Application.Interfaces;

public interface IDeviceRepository
{
    #region CRUD
    
    Device AddDevice(Device device);
    
    IEnumerable<Device> GetDevices();
    
    Device GetDevice(int deviceId);
    
    Device GetDevice(string serialNumber);
    
    Device UpdateDevice(int deviceId, Device device);
    
    Device DeleteDevice(int deviceId);

    #endregion
    
    void RebuildDB();
}