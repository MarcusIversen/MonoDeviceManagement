using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _repository;

    public DeviceService(IDeviceRepository repository)
    {
        _repository = repository;
    }

    public Device AddDevice(PostDeviceDTO device)
    {
        throw new NotImplementedException();
    }

    public List<Device> GetDevices()
    {
        return _repository.GetDevices().ToList();
    }

    public Device GetDevice(int deviceId)
    {
        if (deviceId == null || deviceId < 1) throw new ArgumentException("DeviceId cannot be less than 1 or null");
        return _repository.GetDevice(deviceId);
    }

    public Device UpdateDevice(int deviceId, PutDeviceDTO device)
    {
        throw new NotImplementedException();
    }

    public Device DeleteDevice(int deviceId)
    {
        return _repository.DeleteDevice(deviceId);
    }

    public Device AddUserToDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public Device DeleteUserFromDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public Device UpdateUserOnDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public List<Device> AssignedDevices(int userId)
    {
        throw new NotImplementedException();
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }
    
    // Use to throws errors
    private void ThrowsIfInvalid(Device device)
    {
        if (device.Id == null || device.Id < 1) throw new ArgumentException("DeviceId cannot be less than 1 or null");
    }
}