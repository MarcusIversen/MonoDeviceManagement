using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }
    

    [HttpGet]
    public IActionResult GetDevices()
    {

        return Ok(new List<User>());
    } 
}