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
    //MemberData
    
    #region GetAllUsersMemberData

    public static IEnumerable<Object[]> GetAllUsers_TestCase()
    {
        User user1 = new User { Id = 1, Email = "Test@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678", Role = "Admin"};
        User user2 = new User { Id = 2, Email = "Marcus@mail.com", FirstName = "Marcus", LastName = "Iversen", WorkNumber = "87654321", Role = "Admin"};
        User user3 = new User { Id = 3, Email = "Andy@mail.com", FirstName = "Andy", LastName = "Nguyen", WorkNumber = "11223344", Role = "User"};

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

    #region GetRoleTypeUserMemberData
    public static IEnumerable<Object[]> GetRoleTypeUser_TestCase()
    {
        User user1 = new User { Id = 1, Email = "Test@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678", Role = "User", Hash = "Hash", Salt = "Salt"};
        User user2 = new User { Id = 2, Email = "Marcus@mail.com", FirstName = "Marcus", LastName = "Iversen", WorkNumber = "87654321", Role = "User", Hash = "Hash", Salt = "Salt"};
        User user3 = new User { Id = 3, Email = "Andy@mail.com", FirstName = "Andy", LastName = "Nguyen", WorkNumber = "11223344", Role = "User", Hash = "Hash", Salt = "Salt"};
        User user4 = new User { Id = 3, Email = "John@mail.com", FirstName = "John", LastName = "Johnson", WorkNumber = "76765757", Role = "Admin", Hash = "Hash", Salt = "Salt"};

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
                user1, user4
            },
            new List<User>() { user1 }
        };

        yield return new object[]
        {
            new User[]
            {
                user1,
                user2, 
                user3,
                user4
            },
            new List<User>() { user1, user2, user3 }
        };
    }

    #endregion
    
    // Tests

    #region CreateUserService

    [Fact]
    public void CreateUserServiceTest()
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();

        //Act
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);

        //Assert
        Assert.NotNull(service);
        Assert.True(service is UserService);
    }
    
    [Theory]
    [InlineData("repository cannot be null")]
    public void CreateUserServiceWithMockRepoNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();

        //Act
        var action = ()=> new UserService(null, mapper, putUserValidator);
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    } 
    
    [Theory]
    [InlineData("mapper cannot be null")]
    public void CreateUserServiceWithMapperNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();

        //Act
        var action = ()=> new UserService(mockRepository.Object, null, putUserValidator);
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    } 
    
    [Theory]
    [InlineData("putUserValidator cannot be null")]
    public void CreateUserServiceWithPutValidatorNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();

        //Act
        var action = ()=> new UserService(mockRepository.Object, mapper, null);
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    #endregion
    
    #region Read

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
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
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
    public void GetValidUserByIdTest(int userId, int expectedValueId)
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
        
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
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
    public void GetInvalidUserByIdTest(int userId, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);

        // Act 
        Action action = () => service.GetUser(userId);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("UserId cannot be less than 1 or null", ex.Message);
        
        mockRepository.Verify(r => r.GetUser(userId), Times.Never);
    }
    
    [Theory]
    [InlineData("andy@mail.com", 1)]      //Valid user
    public void GetValidUserByEmailTest(string email, int expectedValueId)
    {
        // Arrange
        User user1 = new User { Id = 1, Email = email, FirstName = "Andy", LastName = "Nguyen", WorkNumber = "12345678", Hash = "Hash", Salt = "Salt"};
        User user2 = new User { Id = 3, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "87654321", Hash = "Hash", Salt = "Salt"};

        var fakeRepo = new List<User>();
        fakeRepo.Add(user1);
        fakeRepo.Add(user2);
        
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
        mockRepository.Setup(r => r.GetUserByEmail(email)).Returns(fakeRepo.Find(u => u.Email == email));
        
        // Act 
        var actual = service.GetUserByEmail(email);

        // Assert 
        Assert.Equal(expectedValueId, actual.Id);
        Assert.Equal(email, actual.Email);
        mockRepository.Verify(r => r.GetUserByEmail(email), Times.Once);
    }
    
    [Theory]
    [InlineData(null, "Email cannot be null or empty")]    //Invalid null email
    [InlineData("", "Email cannot be null or empty")]      //Invalid empty email
    public void GetInvalidUserByEmailTest(string email, string expectedMessage)
    {
        // Arrange
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostUserDTO, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);

        // Act 
        Action action = () => service.GetUserByEmail(email);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        
        mockRepository.Verify(r => r.GetUserByEmail(email), Times.Never);
    }

    #endregion

    #region Update
    
    [Theory]
    [InlineData(1 , "Andy")]
    public void UpdateValidUserTest(int id, string firstName)
    {
        // Arrange
        User user = new User{Id = 1, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678"};
        PutUserDTO dto = new PutUserDTO {Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName,  WorkNumber = user.WorkNumber};

        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserDTO, User>();
        }).CreateMapper();
        
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);

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
        Assert.Equal(user.WorkNumber, updateUser.WorkNumber);
        mockRepository.Verify(r => r.UpdateUser(id, It.IsAny<User>()), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "kristian@mail.dk", "Kristian", "Hansen", "12345678", "Id cannot be null or less than 1")]                                             //Invalid user with id 0 
    [InlineData(-1, "kristian@mail.dk", "Kristian", "Hansen", "12345678", "Id cannot be null or less than 1")]                                            //Invalid user with id -1 
    [InlineData(null, "kristian@mail.dk", "Kristian", "Hansen", "12345678", "Id cannot be null or less than 1")]                                          //Invalid user with id null
    [InlineData(1, null, "Kristian", "Hansen", "12345678", "Email cannot be null, empty and must be a valid email")]                                      //Invalid null email
    [InlineData(1, "", "Kristian", "Hansen", "12345678", "Email cannot be null, empty and must be a valid email")]                                        //Invalid empty email
    [InlineData(1, "kristian@mail.dk", null, "Hansen", "12345678", "First name cannot be null or empty")]                                                 //Invalid null first name
    [InlineData(1, "kristian@mail.dk", "", "Hansen", "12345678", "First name cannot be null or empty")]                                                   //Invalid empty first name
    [InlineData(1, "kristian@mail.dk", "Kristian", null, "12345678", "Last name cannot be null or empty")]                                                //Invalid null last name
    [InlineData(1, "kristian@mail.dk", "Kristian", "", "12345678", "Last name cannot be null or empty")]                                                  //Invalid empty last name
    [InlineData(1, "kristian@mail.dk", "Kristian", "Hansen", null, "Work number cannot be null, empty and must have a minimum length greater than 7")]    //Invalid null work number 
    [InlineData(1, "kristian@mail.dk", "Kristian", "Hansen", "", "Work number cannot be null, empty and must have a minimum length greater than 7")]      //Invalid empty work number 
    [InlineData(1, "kristian@mail.dk", "Kristian", "Hansen", "343", "Work number cannot be null, empty and must have a minimum length greater than 7")]   //Invalid work number minimum length less than 8 
    public void InvalidUserUpdateTest(int userId, string email, string firstName, string lastName, string workNumber, string expectedMessage)
    {
        // Arrange
        User user = new User{Id = userId, Email = email, FirstName = firstName, LastName = lastName, WorkNumber = workNumber};
        PutUserDTO dto = new PutUserDTO {Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, WorkNumber = user.WorkNumber};
        
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserDTO, User>();
        }).CreateMapper();
        
        var putUserValidator = new PutUserValidator();
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
        
        // Act 
        var action = () => service.UpdateUser(userId, dto);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=> r.UpdateUser(userId, user),Times.Never);
    }
    
    [Theory]
    [InlineData(2, "Id in the body and route are different")] //Invalid id not the same
    public void InvalidIdInputExceptionTest(int userId, string expectedMessage)
    {
        // Arrange
        User user = new User{Id = 1, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", WorkNumber = "12345678"};
        PutUserDTO dto = new PutUserDTO {Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName,  WorkNumber = user.WorkNumber};

        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserDTO, User>();
        }).CreateMapper();
        
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
        
        // Act 
        var action = () => service.UpdateUser(userId, dto);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=> r.UpdateUser(userId, user),Times.Never);
    }
    #endregion

    #region Delete

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
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
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
        
        var putUserValidator = new PutUserValidator();
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);

        // Act 
        var action = () => service.DeleteUser(userId);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=>r.DeleteUser(userId),Times.Never);
    }

    #endregion
    
    #region GetRoletypeUserOnlyTest

    [Theory]
    [MemberData(nameof(GetRoleTypeUser_TestCase))]
    public void GetValidUsersWithTypeUserTest(User[] data, List<User> expectedResult)
    {
        var fakeRepo = data;
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutUserValidator, User>();
        }).CreateMapper();
        var putUserValidator = new PutUserValidator();
        
        IUserService service = new UserService(mockRepository.Object, mapper, putUserValidator);
        mockRepository.Setup(r => r.GetUsers()).Returns(fakeRepo);

        // Act 
        var actual = service.GetRoleTypeUser();

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetUsers(), Times.Once);
    }
    #endregion
    
    

}