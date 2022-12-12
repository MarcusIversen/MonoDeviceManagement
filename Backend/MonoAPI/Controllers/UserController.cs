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

    [Authorize("AdminPolicy")]
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_service.GetUsers());
    }

    [Authorize("AdminPolicy")]
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        try
        {
            return Ok(_service.GetUser(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No user was found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize("AdminPolicy")]
    [HttpGet]
    [Route("RoleType")]
    public IActionResult GetUsersByRole()
    {
        return Ok(_service.GetRoleTypeUser());
    }


    [AllowAnonymous]
    [HttpGet("email/{email}")]
    public IActionResult GetUserByEmail(string email)
    {
        try
        {
            return Ok(_service.GetUserByEmail(email));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No user was found at email: " + email);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }


    [AllowAnonymous]
    [HttpPut("update/{id}")]
    public IActionResult UpdateUser(int id, PutUserDTO dto)
    {
        try
        {
            return Ok(_service.UpdateUser(id, dto));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No user was found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize("AdminPolicy")]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            return Ok(_service.DeleteUser(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound("No user was found at id: " + id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize("AdminPolicy")]
    [HttpPost]
    [Route("sendEmail")]
    //This doesn't work when connected to the school internet
    //You must put your own email and password into the appsettings.json file for this to work.
    public void SendEmail(EmailDTO email)
    {
        _service.SendEmail(email.Email, email.Subject, email.Body);
    }
}