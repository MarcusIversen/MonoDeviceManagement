using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase
{
    public DeviceController()
    {
    }

    [HttpGet]
    public IActionResult GetDevices()
    {

        return Ok(new List<Device>());
    } 
}