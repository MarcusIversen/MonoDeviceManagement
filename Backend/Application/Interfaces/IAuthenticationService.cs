using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthenticationService
{
    public string Register(PostUserDTO dto);
    public string Login(LoginDTO dto);
}