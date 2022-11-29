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

    [Authorize ("AdminPolicy")]
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_service.GetUsers());
    } 
    
    [Authorize ("AdminPolicy")]
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(_service.GetUser(id));
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, PutUserDTO dto)
    {
        try
        {
            return Ok(_service.UpdateUser(id, dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No user was found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [Authorize ("AdminPolicy")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        return Ok(_service.DeleteUser(id));
    }

    [Authorize ("AdminPolicy")]
    [HttpPost]
    [Route("sendEmail")]
    //Cannot send email when connected to the school internet 
    public void SendEmail(EmailDTO email)
    {
        _service.SendEmail(email.Email, email.Body, email.Subject);
    }
}