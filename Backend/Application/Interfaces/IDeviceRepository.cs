using Domain;

namespace Application.Interfaces;

public interface IDeviceRepository
{
    #region CRUD

    // Create
    /// <summary>
    /// Adds a device to database
    /// </summary>
    /// <param name="device"></param>
    /// <returns>Added device</returns>
    Device AddDevice(Device device);

    // Read
    /// <summary>
    /// Gets all devices from database
    /// </summary>
    /// <returns>List of devices</returns>
    IEnumerable<Device> GetDevices();
    
    /// <summary>
    /// Gets the device with the given deviceId
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns>Device with the given serialNumber</returns>
    Device GetDevice(int deviceId);
    
    /// <summary>
    /// Gets the device with the given serialNumber
    /// </summary>
    /// <param name="serialNumber"></param>
    /// <returns>Device with the given serialNumber</returns>
    Device GetDevice(string serialNumber);

    // Update 
    /// <summary>
    /// Updates a device with the given deviceId
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="device"></param>
    /// <returns>The updated device</returns>
    Device UpdateDevice(int deviceId, Device device);

    // Delete 
    /// <summary>
    /// Deletes the device with the given deviceId
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns>Deleted device</returns>
    Device DeleteDevice(int deviceId);

    #endregion

    IEnumerable<Device> GetAssignedDevice(int userId);

    //Rebuild Database
    /// <summary>
    /// Rebuillds the database
    /// </summary>
    void RebuildDB();
}