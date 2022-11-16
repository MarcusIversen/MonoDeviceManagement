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
        throw new NotImplementedException();
    }

    public Device GetDevice(int deviceId)
    {
        throw new NotImplementedException();
    }

    public Device UpdateDevice(int deviceId, PutDeviceDTO device)
    {
        throw new NotImplementedException();
    }

    public Device DeleteDevice(int deviceId)
    {
        throw new NotImplementedException();
    }

    public void AddUserToDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserFromDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public Device UpdateUserOnDevice(int userId, int deviceId)
    {
        throw new NotImplementedException();
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }
}