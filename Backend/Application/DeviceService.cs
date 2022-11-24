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
        if (!validate.IsValid) throw new ValidationException(validate.Errors.ToList());
        return _repository.AddDevice(_mapper.Map<Device>(device));
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
    
    public Device GetDevice(string serialNumber)
    {
        if (string.IsNullOrEmpty(serialNumber)) throw new ArgumentException("SerialNumber cannot be empty or null");
        return _repository.GetDevice(serialNumber);
    }

    public Device UpdateDevice(int deviceId, PutDeviceDTO device)
    {
        ThrowsIfPutDeviceIsInvalid(device);
        if (deviceId != device.Id) throw new ArgumentException("Id in the body and route are different");
        var validate = _putDeviceValidator.Validate(device);
        if (!validate.IsValid) throw new ValidationException(validate.Errors.ToList());

        return _repository.UpdateDevice(deviceId, _mapper.Map<Device>(device));
    }

    public Device DeleteDevice(int deviceId)
    {
        if (deviceId == null || deviceId < 1) throw new ArgumentException("Device id cannot be null or less than 1");
            return _repository.DeleteDevice(deviceId);
    }

    public List<Device> AssignedDevices(int userId)
    {
        if (userId == null || userId < 1) throw new ArgumentException("User id cannot be null or less than 1");
        return _repository.GetDevices().Where(d => d.UserId == userId).ToList();
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }

    // Used to throw errors
    private void ThrowsIfPostDeviceIsInvalid(PostDeviceDTO device)
    {
        if (string.IsNullOrEmpty(device.DeviceName)) throw new ArgumentException("Device name cannot be empty or null");
        if (string.IsNullOrEmpty(device.SerialNumber)) throw new ArgumentException("Device serialNumber cannot be empty or null");
        if (device.Amount == null || device.Amount < 1) throw new ArgumentException("Device amount cannot be null or less than 0");
    }
    private void ThrowsIfPutDeviceIsInvalid(PutDeviceDTO device)
    {
        if (string.IsNullOrEmpty(device.DeviceName)) throw new ArgumentException("Device name cannot be empty or null");
        if (string.IsNullOrEmpty(device.SerialNumber)) throw new ArgumentException("Device serialNumber cannot be empty or null");
        if (device.Amount == null || device.Amount < 1) throw new ArgumentException("Device amount cannot be null or less than 0");
        if (device.Id == null || device.Id < 1) throw new ArgumentException("Device id cannot be null or less than 1");
    }
}