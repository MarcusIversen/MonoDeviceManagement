using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Moq;

namespace ServiceTestProject;

public class UserServiceTest
{
    
    #region GetAllUsersTest

    public static IEnumerable<Object[]> GetAllUsers_TestCase()
    {
        User user1 = new User { Id = 1, Email = "Test@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678", Role = "Admin", Hash = "Hash", Salt = "Salt"};
        User user2 = new User { Id = 2, Email = "Marcus@mail.com", FirstName = "Marcus", LastName = "Iversen", WorkNumber = "87654321", Role = "Admin", Hash = "Hash", Salt = "Salt"};
        User user3 = new User { Id = 3, Email = "Andy@mail.com", FirstName = "Andy", LastName = "Nguyen", WorkNumber = "11223344", Role = "User", Hash = "Hash", Salt = "Salt"};

        yield return new Object[]
        {
            new User[]
            {
            },
            new List<User>()
        };

        yield return new object[]
        {

            new User[]
            {
                user1
            },
            new List<User>() { user1 }
        };

        yield return new object[]
        {
            new User[]
            {
                user1,
                user2, 
                user3
            },
            new List<User>() { user1, user2, user3 }
        };
    }

    #endregion
    
    
    [Fact]
    public void CreateUserServiceTest()
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        //Act
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);

        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }
    
    [Theory]
    [MemberData(nameof(GetAllUsers_TestCase))]
    public void GetUsers(User[] data, List<User> expectedResult)
    {
        //Arrange
        var fakeRepo = data;
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);
        mockRepository.Setup(u => u.GetUsers()).Returns(fakeRepo);
        
        //Act
        var actual = service.GetUsers();

        //Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetUsers(), Times.Once);
    }
    
    [Theory]
    [InlineData(1, 1)]      //Valid user
    [InlineData(2, 2)]      //Valid user
    public void GetValidUserTest(int userId, int expectedValueId)
    {
        // Arrange
        User user1 = new User { Id = userId, Email = "andy@mail.com", FirstName = "Andy", LastName = "Nguyen", WorkNumber = "12345678", Hash = "Hash", Salt = "Salt"};
        User user2 = new User { Id = 3, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "87654321", Hash = "Hash", Salt = "Salt"};

        var fakeRepo = new List<User>();
        fakeRepo.Add(user1);
        fakeRepo.Add(user2);
        
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);
        mockRepository.Setup(r => r.GetUser(userId)).Returns(fakeRepo.Find(u => u.Id == userId));
        
        // Act 
        var actual = service.GetUser(userId);

        // Assert 
        Assert.Equal(expectedValueId, actual.Id);
        mockRepository.Verify(r => r.GetUser(userId), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "UserId cannot be less than 1 or null")]     //Invalid UserID 0
    [InlineData(-1, "UserId cannot be less than 1 or null")]    //Invalid UserID -1
    [InlineData(null, "UserId cannot be less than 1 or null")]  //Invalid UserID null
    public void GetInvalidUserTest(int userId, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);

        // Act 
        Action action = () => service.GetUser(userId);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("UserId cannot be less than 1 or null", ex.Message);
        
        mockRepository.Verify(r => r.GetUser(userId), Times.Never);
    }
    
    [Fact]
    public void CreateValidUserTest()
    {
        //Arrange
        
        //Act
        
        //Assert

    }
    
    [Fact]
    public void CreateInvalidUserTest()
    {
        //Arrange
        
        //Act
        
        //Assert

    }
    
    [Fact]
    public void CreateExistingUserTest()
    {
        //Arrange
        
        //Act
        
        //Assert

    }
    
    [Theory]
    [InlineData(1 , "Andy")]
    public void UpdateValidUserTest(int id, string firstName)
    {
        // Arrange
        User user = new User{Id = 1, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", Salt = "123123", Hash = "123123", Role = "Admin", WorkNumber = "12345678"};

        PutUserDTO dto = new PutUserDTO {Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Password = user.Salt+user.Hash, Role = user.Role, WorkNumber = user.WorkNumber};

        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserDTO, User>();
        }).CreateMapper();
        
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);

        mockRepository.Setup(r => r.UpdateUser(id, It.IsAny<User>())).Returns(user);

        // Act 
        dto.FirstName = firstName;
        User updateUser = service.UpdateUser(id, dto);

        // Assert
        Assert.Equal(user, updateUser);
        Assert.Equal(user.Id, updateUser.Id);
        Assert.Equal(user.Email, updateUser.Email);
        Assert.Equal(user.FirstName, updateUser.FirstName);
        Assert.Equal(user.LastName, updateUser.LastName);
        Assert.Equal(user.Role, updateUser.Role);
        Assert.Equal(user.WorkNumber, updateUser.WorkNumber);
        Assert.Equal(user.Salt, updateUser.Salt);
        Assert.Equal(user.Hash, updateUser.Hash);
        mockRepository.Verify(r => r.UpdateUser(id, It.IsAny<User>()), Times.Once);
    }
    
    [Fact]
    public void InvalidUserUpdateTest()
    {
        //Arrange
        
        //Act
        
        //Assert

    }
    
    [Theory]
    [InlineData(1, 1)] // Delete user with id 1 and expectedListSize 
    public void DeleteValidUserTest(int userId, int exceptedListSize)
    {
        // Arrange
        List<User> users = new List<User>();
        User userToDelete = new User { Id = 1, Email = "Test@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678", Role = "Admin", Hash = "Hash", Salt = "Salt"};
        User user = new User { Id = 2, Email = "Tester@mail.com", FirstName = "Andy", LastName = "Nguyen", WorkNumber = "87654321", Role = "Admin", Hash = "Hash", Salt = "Salt"};

        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);
        mockRepository.Setup(r => r.GetUsers()).Returns(users);
        mockRepository.Setup(r => r.DeleteUser(userId)).Returns(() =>
        {
            users.Remove(userToDelete);
            return userToDelete;
        });
        
        users.Add(userToDelete);
        users.Add(user);
        
        // Act 
        var actual = service.DeleteUser(userId);

        // Assert
        Assert.Equal(exceptedListSize, users.Count);
        Assert.Equal(userToDelete, actual);
        Assert.DoesNotContain(userToDelete, users);
        mockRepository.Verify(r=> r.DeleteUser(userId), Times.Once);
    }
    
    [Theory]
    [InlineData(-1, "User id cannot be null or less than 1")]   //Invalid userId -1
    [InlineData(0, "User id cannot be null or less than 1")]    //Invalid userId 0
    [InlineData(null, "User id cannot be null or less than 1")] //Invalid userId null
    public void DeleteInvalidUserTest(int userId, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        
        var postUserValidator = new PostUserValidator();
        var putUserValidator = new PutUserValidator();
        IUserService service = new UserService(mockRepository.Object, mapper, postUserValidator, putUserValidator);

        // Act 
        var action = () => service.DeleteUser(userId);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=>r.DeleteUser(userId),Times.Never);
    }
    
}