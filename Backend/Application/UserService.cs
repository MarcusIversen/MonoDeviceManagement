using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
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

    public User AddUser(PostUserDTO user)
    {
        ThrowsIfPostUserIsInvalid(user);
        if (_repository.GetUsers().FirstOrDefault(u=> u.Email == user.Email) != null)
        {
            throw new ArgumentException("Email already exist");
        }
        var validate = _postUserValidator.Validate(user);
        if (!validate.IsValid) throw new ValidationException(validate.Errors.ToList());
        return _repository.AddUser(_mapper.Map<User>(user));
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
        if (userId != user.Id) throw new ArgumentException("Id in the body and route are different");
        var validate = _putUserValidator.Validate(user);
        if (!validate.IsValid) throw new ValidationException(validate.Errors.ToList());

        return _repository.UpdateUser(userId, _mapper.Map<User>(user));
    }

    public User DeleteUser(int userId)
    {
        if (userId == null || userId < 1) throw new ArgumentException("User id cannot be null or less than 1");
        return _repository.DeleteUser(userId);
    }
    
    //Used to throw errors
    private void ThrowsIfPostUserIsInvalid(PostUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be empty or null");
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be empty or null");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be empty or null");
        if (string.IsNullOrEmpty(user.WorkNumber)) throw new ArgumentException("Work number cannot be empty or null");
        if (string.IsNullOrEmpty(user.Role)) throw new ArgumentException("Role cannot be empty or null");
        if (string.IsNullOrEmpty(user.Password)) throw new ArgumentException("Password cannot be empty or null");
    }
    //Used to throw errors
    private void ThrowsIfPutUserIsInvalid(PutUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("Email cannot be empty or null");
        if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("First name cannot be empty or null");
        if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Last name cannot be empty or null");
        if (string.IsNullOrEmpty(user.WorkNumber)) throw new ArgumentException("Work number cannot be empty or null");
        if (user.Id == null || user.Id < 1) throw new ArgumentException("Device id cannot be null or less than 1");
    }
}