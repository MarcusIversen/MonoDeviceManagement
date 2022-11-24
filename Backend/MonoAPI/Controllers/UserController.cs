using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
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
    public IActionResult GetUser(int deviceId)
    {
        return Ok(_service.GetUser(deviceId));
    }
    
    [HttpPost]
    public IActionResult CreateUser(PostUserDTO dto)
    {
        try
        {
            var user = _service.CreateUser(dto);
            return Created("User/" + user.Id, user);
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
}