using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class DeviceService : IDeviceService
{
    private IDeviceRepository _repository;
    private IMapper _mapper;
    private IValidator<PostDeviceDTO> _postDeviceValidator;
    private IValidator<PutDeviceDTO> _putDeviceValidator;

    public DeviceService(IDeviceRepository repository, IMapper mapper, IValidator<PostDeviceDTO> postDeviceValidator, IValidator<PutDeviceDTO> putDeviceValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _postDeviceValidator = postDeviceValidator;
        _putDeviceValidator = putDeviceValidator;
    }

    public Device AddDevice(PostDeviceDTO device)
    {
        ThrowsIfPostDeviceIsInvalid(device);
        var validate = _postDeviceValidator.Validate(device);
        if (!validate.IsValid) throw new ArgumentException(validate.ToString());
        return _repository.AddDevice(_mapper.Map<Device>(device));
    }

    public List<Device> GetDevices()
    {
        return _repository.GetDevices().ToList();
    }

    public Device GetDevice(int deviceId)
    {
        if (deviceId == null || deviceId < 1)
        {
            throw new ArgumentException("DeviceId cannot be less than 1 or null");
        }
        return _repository.GetDevice(deviceId);
    }
    
    public Device GetDevice(string serialNumber)
    {
        if (string.IsNullOrEmpty(serialNumber))
        {
            throw new ArgumentException("SerialNumber cannot be empty or null");
        }
        return _repository.GetDevice(serialNumber);
    }

    public Device UpdateDevice(int deviceId, PutDeviceDTO device)
    {
        if (deviceId != device.Id)
        {
            throw new ArgumentException("Id in the body and route are different");
        }
        
        ThrowsIfPutDeviceIsInvalid(device);

        var validate = _putDeviceValidator.Validate(device);
        if (!validate.IsValid)
        {
            throw new ArgumentException(validate.ToString());
        }

        return _repository.UpdateDevice(deviceId, _mapper.Map<Device>(device));
    }

    public Device DeleteDevice(int deviceId)
    {
        if (deviceId == null || deviceId < 1)
        {
            throw new ArgumentException("Device id cannot be null or less than 1");
        }
        return _repository.DeleteDevice(deviceId);
    }

    public List<Device> AssignedDevices(int userId)
    {
        if (userId == null || userId < 1)
        {
            throw new ArgumentException("User id cannot be null or less than 1");
        }
        return _repository.GetDevices().Where(d => d.UserId == userId).ToList();
    }


    public List<Device> GetNotAssignedDevices() {
        return _repository.GetDevices().Where(d => d.UserId == null).ToList();
    }
    
    public List<Device> GetDevicesWithRequestValue(string value) {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty");
            return _repository.GetDevices().Where(d => d.RequestEnum == value).ToList();
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }

    // Used to throw errors
    private void ThrowsIfPostDeviceIsInvalid(PostDeviceDTO device)
    {
        if (string.IsNullOrEmpty(device.DeviceName))
        {
            throw new ArgumentException("Device name cannot be empty or null");
        }

        if (string.IsNullOrEmpty(device.SerialNumber))
        {
            throw new ArgumentException("Device serialNumber cannot be empty or null");
        }
        if (device.Status is not ("I brug" or "På lager" or "Defekt"))
        {
            throw new ArgumentException("Incorrect device status");
        }
    }
    
    //Used to throw errors
    private void ThrowsIfPutDeviceIsInvalid(PutDeviceDTO device)
    {
        if (string.IsNullOrEmpty(device.DeviceName))
        {
            throw new ArgumentException("Device name cannot be empty or null");
        }

        if (string.IsNullOrEmpty(device.SerialNumber))
        {
            throw new ArgumentException("Device serialNumber cannot be empty or null");
        }
        if (device.Id == null || device.Id < 1)
        {
            throw new ArgumentException("Device id cannot be null or less than 1");   
        }

        if (device.Status is not ("I brug" or "På lager" or "Defekt"))
        {
            throw new ArgumentException("Incorrect device status");
        }
    }
}