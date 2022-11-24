using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validators;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _repository;
    
    public AuthenticationService(IUserRepository repository,
        IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        _repository = repository;
    }

    public string Register(RegisterDTO dto)
    {
        try
        {
            _repository.GetUserByEmail(dto.Email);
        }
        catch (KeyNotFoundException e)
        {
            var salt = RandomNumberGenerator.GetBytes(32).ToString();
            var user = new User
            {
                Email = dto.Email,
                Salt = salt,
                Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
                Role = dto.Role,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfilePicture = dto.ProfilePicture,
                WorkNumber = dto.WorkNumber,
                PrivateNumber = dto.PrivateNumber,
                PrivateMail = dto.PrivateMail
            };
            _repository.AddUser(user);
            return GenerateToken(user);
        }

        throw new Exception("Email  " + dto.Email + "is already taken");
    }

    public string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email), new Claim("role", user.Role)}),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public string Login(LoginDTO dto)
    {
        var user = _repository.GetUserByEmail(dto.Email);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.Hash))
        {
            return GenerateToken(user);
        }

        throw new Exception("Invalid login");
    }
}