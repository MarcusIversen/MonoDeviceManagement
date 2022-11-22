using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MonoAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
        
    }

    public ActionResult Login(LoginAndRegisterDTO dto)
    {
        return null;
    }
    
    public ActionResult Register(LoginAndRegisterDTO dto)
    {
        return null;
    }
}