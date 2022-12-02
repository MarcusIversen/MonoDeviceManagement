using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private DatabaseContext _context;
    private DeviceRepository _deviceRepository;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public User CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUser(int userId)
    {
        _deviceRepository = new DeviceRepository(_context);
        var user = _context.Users.FirstOrDefault(u => u.Id == userId);
        user.Devices = _deviceRepository.GetAssignedDevice(user.Id).ToList();
        return user;
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new KeyNotFoundException("There was no user with email " + email);
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
            userToUpdate.ProfilePicture = user.ProfilePicture;
            userToUpdate.PrivateMail = user.PrivateMail;
            userToUpdate.PrivateNumber= user.PrivateNumber;
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