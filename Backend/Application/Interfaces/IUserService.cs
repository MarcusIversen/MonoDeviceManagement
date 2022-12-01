using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IUserService
{
    #region CRUD
    // Read
    /// <summary>
    /// Gets all users from database
    /// </summary>
    /// <returns>List of users</returns>
    List<User> GetUsers();
    
    /// <summary>
    /// Gets the user with the given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>User with the given userId</returns>
    User GetUser(int userId);

    // Update 
    /// <summary>
    /// Updates an user with the given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="user"></param>
    /// <returns>The updated user</returns>
    User UpdateUser(int userId, PutUserDTO user);

    // Delete 
    /// <summary>
    /// Deletes the user with the given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Deleted user</returns>
    User DeleteUser(int userId);
    #endregion

    /// <summary>
    /// Get a list of users with user as a type 
    /// </summary>
    /// <returns>List of Users with type user</returns>
    List<User> GetRoleTypeUser();

    #region Email

    public void SendEmail(string toMail, string subject, string body);

    #endregion
}