using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;


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
    
    [HttpGet("{id}")]
    public IActionResult GetDeviceBySId(int deviceId)
    {
        return Ok(_service.GetDevice(deviceId));
    }

    [HttpPost]
    public IActionResult CreateDevice(PostDeviceDTO dto)
    {
        try
        {
            var device = _service.AddDevice(dto);
            return Created("Device/" + device.Id, device);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
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
    [Route("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }
}