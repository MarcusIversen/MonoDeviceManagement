using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    public User AddUser(User user)
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

    public User UpdateUser(int userId, User user)
    {
        throw new NotImplementedException();
    }

    public User DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }
}