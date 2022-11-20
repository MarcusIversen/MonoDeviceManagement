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
    private IValidator<PutUserValidator> _putUserValidator;

    public UserService(IUserRepository repository, IMapper mapper, IValidator<PostUserDTO> postUserValidator, IValidator<PutUserValidator> putUserValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _postUserValidator = postUserValidator;
        _putUserValidator = putUserValidator;
    }

    public User AddUser(PostUserDTO user)
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public User GetUser(int userId)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(int userId, PutUserDTO user)
    {
        throw new NotImplementedException();
    }

    public User DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }
}