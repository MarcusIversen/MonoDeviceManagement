using Application;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Options;
using Moq;

namespace ServiceTestProject;

public class AuthServiceTest
{
    /**
    [Fact]
    public void CreateAuthServiceTest()
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var postUserValidator = new PostUserValidator();
        var token = new Token();

            //Act
        IAuthenticationService service = new AuthenticationService(mockRepository.Object, postUserValidator);

        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }
    
    [Theory]
    [InlineData(1)]
    public void CreateValidUserTest(int userId)
    {
        //Arrange
        List<User> users = new List<User>();
        
        User user1 = new User {Id = userId, Email = "andy@gmail.com", Role = Role.Admin, FirstName = "andy", LastName = "lam", Hash = "sfesdeefe3", Salt = "sdadassdas", WorkNumber = "334212312"};
        
        PostUserDTO DTO = new PostUserDTO {Email = user1.Email, Role = Role.Admin, FirstName = user1.FirstName, LastName = user1.Salt, Password = user1.Salt + user1.Hash, WorkNumber = user1.WorkNumber};
        
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);
        mockRepository.Setup(r => r.CreateUser(It.IsAny<User>())).Returns(() =>
        {
            users.Add(user1);
            return user1;
        });
        
        //Act
        var createdUser = service.CreateUser(DTO);

        //Assert
        Assert.True(users.Count == 1);
        Assert.Equal(user1.Email, createdUser.Email);
        Assert.Equal(user1.FirstName, createdUser.FirstName);
        Assert.Equal(user1.LastName, createdUser.LastName);
        Assert.Equal(user1.Id, createdUser.Id);
        mockRepository.Verify(r=>r.CreateUser(It.IsAny<User>()), Times.Once);

    }
    
    [Theory]
    [InlineData(1, null, "lam", "andy@gmail.com", "12345678", "32432432", "334324324", Role.Admin, "First name cannot be null or empty")]
    [InlineData(1, "", "lam", "andy@gmail.com", "12345678", "32432432", "334324324", Role.Admin, "First name cannot be null or empty")]
    [InlineData(1, "andy", null, "andy@gmail.com", "12345678", "32432432", "334324324", Role.Admin, "Last name cannot be null or empty")]
    [InlineData(1, "andy", "", "andy@gmail.com", "12345678", "32432432", "334324324", Role.Admin, "Last name cannot be null or empty")]
    [InlineData(1, "andy", "lam", null, "12345678", "32432432", "334324324", Role.Admin, "Email cannot be null, empty and must be a valid email")]
    [InlineData(1, "andy", "lam", "", "12345678", "32432432", "334324324", Role.Admin, "Email cannot be null, empty and must be a valid email")]
    [InlineData(1, "andy", "lam", "andy@gmail.com", null, "32432432", "334324324", Role.Admin, "Work number cannot be null, empty and must have a minimum length greater than 7")]
    [InlineData(1, "andy", "lam", "andy@gmail.com", "", "32432432", "334324324", Role.Admin, "Work number cannot be null, empty and must have a minimum length greater than 7")]
    [InlineData(1, "andy", "lam", "andy@gmail.com", "21312", "32432432", "334324324", Role.Admin, "Work number cannot be null, empty and must have a minimum length greater than 7")]
    [InlineData(1, "andy", "lam", "andy@gmail.com", "32432432", "32432432", "334324324", null, "Role cannot be null")]
    public void CreateInvalidUserTest(int userId, string firstName, string lastName, string email, string workNumber, string salt, string hash, Role role, string expectedMessage)
    {
        //Arrange
        User user1 = new User {Id = userId, Email = email, FirstName = firstName, LastName = lastName, Salt = salt, Hash = hash, WorkNumber = workNumber, Role = role};
        PostUserDTO dto = new PostUserDTO {Email = user1.Email, FirstName = user1.FirstName, LastName = user1.LastName, Password = user1.Salt + user1.Hash, WorkNumber = user1.WorkNumber, Role = user1.Role};
        
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);
        
        //Act
        var action = () => service.CreateUser(dto);
        var ex = Assert.Throws<ArgumentException>(() => service.CreateUser(dto));
        
        //Assert
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=>r.CreateUser(It.IsAny<User>()),Times.Never);
    }
    **/
}