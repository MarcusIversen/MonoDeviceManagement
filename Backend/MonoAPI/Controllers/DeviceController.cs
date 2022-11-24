using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private IDeviceService _service;

    public DeviceController(IDeviceService service)
    {
        _service = service;
    }

    
    [HttpGet]
    public IActionResult GetDevices()
    {

        return Ok(_service.GetDevices());
    }
    
    [HttpGet("{serialNumber}")]
    public IActionResult GetDeviceBySerialNumber(string serialNumber)
    {
        return Ok(_service.GetDevice(serialNumber));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDevice(int deviceId, PutDeviceDTO dto)
    {
        try
        {
            return Ok(_service.UpdateDevice(deviceId, dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No device found at id: " + deviceId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteDevice(int deviceId)
    {
        return Ok(_service.DeleteDevice(deviceId));
    }

    [HttpGet]
    public IActionResult GetAssignedDevices(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }
}