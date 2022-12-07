using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Domain;
using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validators;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _repository;

    private IValidator<PostUserDTO> _postUserValidator;
    private IValidator<PutPasswordDTO> _putPasswordValidator;

    public AuthenticationService(
        IUserRepository repository,
        IOptions<AppSettings> appSettings,
        IValidator<PostUserDTO> postUserValidator,
        IValidator<PutPasswordDTO> putPasswordValidator)
    {
        _appSettings = appSettings.Value;
        _repository = repository;
        _postUserValidator = postUserValidator;
        _putPasswordValidator = putPasswordValidator;
    }

    public string Register(PostUserDTO dto)
    {
        try
        {
            _repository.GetUserByEmail(dto.Email);
        }
        catch (KeyNotFoundException)
        {
            ThrowsIfPostUserIsInvalid(dto);

            var validate = _postUserValidator.Validate(dto);
            if (!validate.IsValid)
            {
                throw new ArgumentException(validate.ToString());
            }
            
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
            _repository.CreateUser(user);
            return GenerateToken(user);
        }

        throw new Exception("Email  " + dto.Email + "is already taken");
    }
    
    public string UpdatePassword(int userId, PutPasswordDTO dto)
    {
        try
        {
            ThrowsIfPutPasswordIsInvalid(dto);

            var validate = _putPasswordValidator.Validate(dto);
            if (!validate.IsValid)
            {
                throw new ArgumentException(validate.ToString());
            }

            var user = _repository.GetUser(userId);
            user.Hash = BCrypt.Net.BCrypt.HashPassword(dto.Password + user.Salt);

            _repository.UpdateUserPassword(userId, user);
            return GenerateToken(user);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {   
                new Claim("email", user.Email),
                new Claim("role", user.Role),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            }),
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


    private void ThrowsIfPutPasswordIsInvalid(PutPasswordDTO user)
    {
        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ArgumentException("Password cannot be null, empty and must be greater than 8 characters");
        }

        if (user.Id.Equals(null) || user.Id.Equals(""))
        {
            throw new ArgumentException("Id cannot be null or empty");
        }
    }
    
    private void ThrowsIfPostUserIsInvalid(PostUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentException("Email cannot be null, empty and must be a valid email");
        }

        if (string.IsNullOrEmpty(user.FirstName))
        {
            throw new ArgumentException("First name cannot be null or empty");
        }

        if (string.IsNullOrEmpty(user.LastName))
        {
            throw new ArgumentException("Last name cannot be null or empty");
        }

        if (string.IsNullOrEmpty(user.WorkNumber) || user.WorkNumber.Length < 8)
        {
            throw new ArgumentException("Work number cannot be null, empty and must have a minimum length greater than 7");
        }

        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
        {
            throw new ArgumentException("Password cannot be null, empty and must have a minimum length greater than 7");
        }

        if (user.Role is not ("Admin" or "User"))
        {
            throw new ArgumentException("Role cannot be null and must be Admin or User");
        }
    }
}