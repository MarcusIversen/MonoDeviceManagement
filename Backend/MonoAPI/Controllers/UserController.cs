using Application.DTOs;
using Application.Interfaces;
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
        return Ok(_service.GetUsers());
    } 
    
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(_service.GetUser(id));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int userId, PutUserDTO dto)
    {
        try
        {
            return Ok(_service.UpdateUser(userId, dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No user was found at id: " + userId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int userId)
    {
        return Ok(_service.DeleteUser(userId));
    }

    //[Authorize ("AdminPolicy")]
    [AllowAnonymous]
    [HttpPost]
    [Route("sendEmail")]
    public void SendEmail(EmailDTO email)
    {
        _service.SendEmail(email.Email, email.Body, email.Subject);
    }
}