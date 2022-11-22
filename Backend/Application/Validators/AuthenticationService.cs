using System.Net;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Validators;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _repository;
    
    public AuthenticationService(IUserRepository repository)
    {
        _repository = repository;
    }

    public string Register(LoginAndRegisterDTO dto)
    {
        throw new NotImplementedException();
    }

    public string Login(LoginAndRegisterDTO dto)
    {
        throw new NotImplementedException();
    }
}