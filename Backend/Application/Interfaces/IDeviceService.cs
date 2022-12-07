using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IDeviceService
{
    #region CRUD
    
    Device AddDevice(PostDeviceDTO device);
    
    List<Device> GetDevices();
    
    Device GetDevice(int deviceId);
    
    Device GetDevice(string serialNumber);
    
    Device UpdateDevice(int deviceId, PutDeviceDTO device);

    Device DeleteDevice(int deviceId);

    #endregion
    
    List<Device> AssignedDevices(int userId);

    List<Device> GetNotAssignedDevices();
    
    List<Device> GetDevicesWithRequestValue(string value);
    
    void RebuildDB();
}