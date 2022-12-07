using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    #region CRUD
 
    
    User CreateUser(User user);
    
    IEnumerable<User> GetUsers();
    
    User GetUser(int userId);
    
    User GetUserByEmail(string email);
    
    User UpdateUser(int userId, User user);
    
    User UpdateUserPassword(int userId, User user);
    
    User DeleteUser(int userId);
    #endregion
    
}