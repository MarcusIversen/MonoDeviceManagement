using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;

namespace Application;

public class UserService : IUserService
{

    private IUserRepository _repository;
    private IMapper _mapper;
    private IValidator<PostUserDTO> _postUserValidator;
    private IValidator<PutUserDTO> _putUserValidator;

    public UserService(IUserRepository repository, IMapper mapper, IValidator<PostUserDTO> postUserValidator, IValidator<PutUserDTO> putUserValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _postUserValidator = postUserValidator;
        _putUserValidator = putUserValidator;
    }

    public User CreateUser(PostUserDTO user)
    {
        ThrowsIfPostUserIsInvalid(user);
        if (_repository.GetUsers().FirstOrDefault(u=> u.Email == user.Email) != null)
        {
            throw new ArgumentException("Email already exist");
        }
        var validate = _postUserValidator.Validate(user);
        if (!validate.IsValid)
        {
            throw new ArgumentException(validate.ToString());
        }
        return _repository.CreateUser(_mapper.Map<User>(user));
    }

    public List<User> GetUsers()
    {
        return _repository.GetUsers().ToList();
    }

    public User GetUser(int userId)
    {
        if (userId == null || userId < 1) throw new ArgumentException("UserId cannot be less than 1 or null");
        return _repository.GetUser(userId);
    }

    public User UpdateUser(int userId, PutUserDTO user)
    {
        ThrowsIfPutUserIsInvalid(user);
        var validate = _putUserValidator.Validate(user);
        if (!validate.IsValid) throw new ArgumentException(validate.ToString());
        if (userId != user.Id) throw new ArgumentException("Id in the body and route are different");
        User updatedUser = _repository.GetUserByEmail(user.Email);
        updatedUser.Hash = BCrypt.Net.BCrypt.HashPassword(user.Password + updatedUser.Salt);
        
        return _repository.UpdateUser(userId, updatedUser);
    }

    public User DeleteUser(int userId)
    {
        if (userId == null || userId < 1) throw new ArgumentException("User id cannot be null or less than 1");
        return _repository.DeleteUser(userId);
    }
    
    //Used to throw errors
    private void ThrowsIfPostUserIsInvalid(PostUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, empty and must be a valid email");
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be null or empty");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be null or empty");
        if (string.IsNullOrEmpty(user.WorkNumber) || user.WorkNumber.Length < 8 ) throw new ArgumentException("Work number cannot be null, empty and must have a minimum length greater than 7");
        if (string.IsNullOrEmpty(user.Role)) throw new ArgumentException("Role cannot be null or empty");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8) throw new ArgumentException("Password cannot be null, empty and must have a minimum length greater than 7");
    }
    private void ThrowsIfPutUserIsInvalid(PutUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be null, empty and must be a valid email");
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be null or empty");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be null or empty");
        if (string.IsNullOrEmpty(user.WorkNumber) || user.WorkNumber.Length < 8 ) throw new ArgumentException("Work number cannot be null, empty and must have a minimum length greater than 7");
        if (string.IsNullOrEmpty(user.Role)) throw new ArgumentException("Role cannot be null or empty");
        if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8) throw new ArgumentException("Password cannot be null, empty and must have a minimum length greater than 7");
        if (user.Id == null || user.Id < 1) throw new ArgumentException("Id cannot be null or less than 1");
    }
}