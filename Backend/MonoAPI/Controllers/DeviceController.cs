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
        try
        {
            return Ok(_service.GetDevice(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No device found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize("AdminPolicy")]
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
    public IActionResult UpdateDevice(int id, PutDeviceDTO dto)
    {
        try
        {
            return Ok(_service.UpdateDevice(id, dto));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No device found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize("AdminPolicy")]
    [HttpDelete("{id}")]
    public IActionResult DeleteDevice(int id)
    {
        try
        {
            return Ok(_service.DeleteDevice(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No device found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet("/AssignDev/{id}")]
    public IActionResult GetAssignedDevices(int id)
    {
        return Ok(_service.AssignedDevices(id));
    }

    [HttpGet("/RequestValue/{value}")]
    public IActionResult GetRequestValueDevices(string value)
    {
        return Ok(_service.GetDevicesWithRequestValue(value));
    }

    [HttpGet("/Malfunctioned")]
    public IActionResult GetDevicesWithStatusMalfunction()
    {
        return Ok(_service.GetDevicesWithStatusMalfunction());
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("rebuildDB/{password}")]
    public IActionResult RebuildDB(string password)
    {
        if (password == "dsfghfdsafghjfdsadewrtyuikljmnbgfdrtyujkjhmnbvcfdcsefrgthnbvcxdsaefrghbvcxdsadefrgbvcxsaadf")
        {
            _service.RebuildDB();
            return Ok();
        }

        return StatusCode(422, "Invalid kodeord");

    }
}