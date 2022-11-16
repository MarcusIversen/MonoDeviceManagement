using Domain;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {
    }
    
    [HttpGet]
    public IActionResult GetDevices()
    {

        return Ok(new List<User>());
    } 
}