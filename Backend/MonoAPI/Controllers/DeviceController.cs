using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
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
    
    [HttpGet("{id}")]
    public IActionResult GetDeviceById(int id)
    {
        return Ok(_service.GetDevice(id));
    }

    [Authorize ("AdminPolicy")]
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
    
    [Authorize ("AdminPolicy")]
    [HttpPut("{id}")]
    public IActionResult UpdateDevice(int id, PutDeviceDTO dto)
    {
        try
        {
            return Ok(_service.UpdateDevice(id, dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No device found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize ("AdminPolicy")]
    [HttpDelete("{id}")]
    public IActionResult DeleteDevice(int id)
    {
        return Ok(_service.DeleteDevice(id));
    }

    [Authorize ("AdminPolicy")]
    [HttpGet]
    [Route("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }
}