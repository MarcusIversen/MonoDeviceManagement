using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class DeviceRepository : IDeviceRepository
{
    private DatabaseContext _context;

    public DeviceRepository(DatabaseContext context)
    {
        _context = context;
    }

    public Device AddDevice(Device device)
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

    public Device UpdateDevice(int deviceId, Device device)
    {
        throw new NotImplementedException();
    }

    public Device DeleteDevice(int deviceId)
    {
        throw new NotImplementedException();
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
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}