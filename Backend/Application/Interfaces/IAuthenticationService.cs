using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public string Register(RegisterDTO dto);
    public string Login(LoginDTO dto);
}