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

    public IEnumerable<Device> GetDevices()
    {
        return _context.Devices.ToList();
    }

    public Device GetDevice(int deviceId)
    {
        return _context.Devices.FirstOrDefault(d => d.Id == deviceId);
    }

    public Device UpdateDevice(int deviceId, Device device)
    {
        throw new NotImplementedException();
    }

    public Device DeleteDevice(int deviceId)
    {
        var device = _context.Devices.FirstOrDefault(d => d.Id == deviceId);
        _context.Devices.Remove(device);
        _context.SaveChanges();
        return device;
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

    public IEnumerable<Device> AssignedDevices(int userId)
    {
        throw new NotImplementedException();
    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}