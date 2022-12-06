﻿using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authentication;
    
    public AuthController(IAuthenticationService authentication)
    {
        _authentication = authentication;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult Login(LoginDTO dto)
    {
        try
        {
            return Ok(_authentication.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("register")]
    public ActionResult Register(PostUserDTO dto)
    {
        try
        {
            return Ok(_authentication.Register(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("update-password/{id}")]
    public ActionResult UpdatePassword([FromRoute]int id, [FromBody]PutPasswordDTO dto)
    {
        try
        {
            Console.WriteLine(id);
            Console.WriteLine(dto);
            return Ok(_authentication.UpdatePassword(id, dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}