using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IUserService
{
    #region CRUD
 
    List<User> GetUsers();
    
    User GetUser(int userId);
    
    User GetUserByEmail(string email);
    
    User UpdateUser(int userId, PutUserDTO user);
    
    User DeleteUser(int userId);
    
    List<User> GetRoleTypeUser();
    
    #endregion

    #region Email

    public void SendEmail(string toMail, string subject, string body);

    #endregion
}