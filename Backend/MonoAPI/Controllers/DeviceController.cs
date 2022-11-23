using Application.Interfaces;
using Domain;
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

        return Ok(new List<Device>());
    }
    
    [HttpGet("{serialNumber}")]
    public IActionResult GetDeviceBySerialNumber(string serialNumber)
    {

        return Ok(_service.GetDevice(serialNumber));
    }

    [HttpGet]
    [Route("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }
}