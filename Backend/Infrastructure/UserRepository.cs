using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public User AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUser(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
    }

    public User UpdateUser(int userId, User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (userToUpdate.Id == userId)
        {
            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.WorkNumber = user.WorkNumber;
            userToUpdate.PrivateMail = user.PrivateMail;
            userToUpdate.PrivateNumber= user.PrivateNumber;
            userToUpdate.Hash = user.Hash; //TODO Skal det her være her?
            userToUpdate.Salt = user.Salt; //TODO Skal det her være her?
            userToUpdate.Id = user.Id;
            _context.Update(userToUpdate);
            _context.SaveChanges();
        }

        return userToUpdate;
    }

    public User DeleteUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        _context.Users.Remove(user);
        _context.SaveChanges();
        return user;
    }
}