﻿using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    #region CRUD
    // Create
    /// <summary>
    /// Adds an user to database
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Added user</returns>
    User AddUser(User user);

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
    User UpdateUser(int userId, User user);

    // Delete 
    /// <summary>
    /// Deletes the user with the given userId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Deleted user</returns>
    User DeleteUser(int userId);
    #endregion
}