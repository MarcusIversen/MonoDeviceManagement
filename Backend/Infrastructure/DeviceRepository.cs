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
        _context.Devices.Add(device);
        _context.SaveChanges();
        return device;    
    }

    public IEnumerable<Device> GetDevices()
    {
        return _context.Devices.ToList();
    }

    public Device GetDevice(int deviceId)
    {
        return _context.Devices.FirstOrDefault(d => d.Id == deviceId);
    }
    public Device GetDevice(string serialNumber)
    {
        return _context.Devices.FirstOrDefault(d=>d.SerialNumber == serialNumber) ?? throw new KeyNotFoundException("There is no device with serial number" + serialNumber);
    }
    

    public Device UpdateDevice(int deviceId, Device device)
    {
        var dev = _context.Devices.FirstOrDefault(d => d.Id == deviceId);
        if (dev.Id == deviceId)
        {
            dev.DeviceName = device.DeviceName;
            dev.SerialNumber = device.SerialNumber;
            dev.User = device.User;
            dev.UserId = device.UserId;
            dev.Status = device.Status;
            _context.Update(dev);
            _context.SaveChanges();
        }

        return dev;
    }

    public Device DeleteDevice(int deviceId)
    {
        var device = _context.Devices.FirstOrDefault(d => d.Id == deviceId);
        _context.Devices.Remove(device);
        _context.SaveChanges();
        return device;
    }

    public IEnumerable<Device> GetAssignedDevice(int userId)
    {
        return _context.Devices.Where(d => d.UserId == userId).ToList();
    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}