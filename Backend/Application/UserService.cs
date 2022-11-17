using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application;

public class UserService : IUserService
{

    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
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