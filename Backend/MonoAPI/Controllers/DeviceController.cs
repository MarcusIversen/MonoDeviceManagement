using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _service;

    public DeviceController(IDeviceService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetDevices()
    {

        return Ok(new List<Device>());
    }

    [HttpGet]
    [Route("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }
}