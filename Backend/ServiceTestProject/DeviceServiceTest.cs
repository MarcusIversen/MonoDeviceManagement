using Application;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain;
using Moq;

namespace ServiceTestProject;

public class DeviceServiceTest
{
    //MemberData

    #region GetAllDevicesMemberData

    public static IEnumerable<Object[]> GetAllDevices_TestCase()
    {
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123"};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543"};

        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };

        yield return new object[]
        {

            new Device[]
            {
                device1
            },
            new List<Device>() { device1 }
        };

        yield return new object[]
        {
            new Device[]
            {
                device1,
                device2, 
                device3
            },
            new List<Device>() { device1, device2, device3 }
        };
    }

    #endregion
    
    #region ListOfDevicesWithUserMemberData

    public static IEnumerable<Object[]> ListOfDevicesWithUser_TestCase()
    {
        User user = new User
        {
            Id = 1, Email = "Kristian@mail.com", FirstName = "Kristian", LastName = "Hansen", Salt = "123123",
            Hash = "123123", Role = "Admin", WorkNumber = "12345678"
        };
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", UserId = user.Id};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", UserId = user.Id};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543", UserId = user.Id};
        Device device4 = new Device { Id = 4, DeviceName = "Seed device4", SerialNumber = "3f"};
        Device device5 = new Device { Id = 5, DeviceName = "Seed device5", SerialNumber = "1efdf4"};

        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };
        
        yield return new object[]
        {
            new Device[]
            {
                device1, device4, device5
            },
            new List<Device>() { device1 }
        };
        
        yield return new object[]
        {
            new Device[]
            {
                device1,
                device2, 
                device3,
                device4,
                device5
            },
            new List<Device>() { device1, device2, device3 }
        };
    }

    #endregion

    #region GetDeviceWithRequestValueMemberData
    
    public static IEnumerable<Object[]> GetAllIkkeSendt_TestCase()
    {
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543", Status = "Defekt", RequestValue = "Sendt"};
        Device device4 = new Device { Id = 4, DeviceName = "Seed device4", SerialNumber = "1122g", Status = "Defekt", RequestValue = "Sendt"};
        Device device5 = new Device { Id = 5, DeviceName = "Seed device5", SerialNumber = "332434", Status = "Defekt", RequestValue = "Accepteret"};
        Device device6 = new Device { Id = 6, DeviceName = "Seed device6", SerialNumber = "df235", Status = "Defekt", RequestValue = "Accepteret"};

        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };

        yield return new object[]
        {

            new Device[]
            {
                device1, device3, device4, device5, device6
            },
            new List<Device>() { device1 }
        };

        yield return new object[]
        {
            new Device[]
            {
                device1,
                device2, 
                device3,
                device4,
                device5,
                device6
            },
            new List<Device>() { device1, device2 }
        };
    }
    
    public static IEnumerable<Object[]> GetAllSendt_TestCase()
    {
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543", Status = "Defekt", RequestValue = "Sendt"};
        Device device4 = new Device { Id = 4, DeviceName = "Seed device4", SerialNumber = "1122g", Status = "Defekt", RequestValue = "Sendt"};
        Device device5 = new Device { Id = 5, DeviceName = "Seed device5", SerialNumber = "332434", Status = "Defekt", RequestValue = "Accepteret"};
        Device device6 = new Device { Id = 6, DeviceName = "Seed device6", SerialNumber = "df235", Status = "Defekt", RequestValue = "Accepteret"};

        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };

        yield return new object[]
        {

            new Device[]
            {
                device1, 
                device2, 
                device4,
                device5, 
                device6
            },
            new List<Device>() { device4 }
        };

        yield return new object[]
        {
            new Device[]
            {
                device1,
                device2, 
                device3,
                device4, 
                device5,
                device6
            },
            new List<Device>() { device3, device4 }
        };
    }
    
    public static IEnumerable<Object[]> GetAllAccepteret_TestCase()
    {
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", Status = "Defekt", RequestValue = "IkkeSendt"};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543", Status = "Defekt", RequestValue = "Sendt"};
        Device device4 = new Device { Id = 4, DeviceName = "Seed device4", SerialNumber = "1122g", Status = "Defekt", RequestValue = "Sendt"};
        Device device5 = new Device { Id = 5, DeviceName = "Seed device5", SerialNumber = "332434", Status = "Defekt", RequestValue = "Accepteret"};
        Device device6 = new Device { Id = 6, DeviceName = "Seed device6", SerialNumber = "df235", Status = "Defekt", RequestValue = "Accepteret"};

        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };

        yield return new object[]
        {

            new Device[]
            {
                device1, 
                device2, 
                device3,
                device4, 
                device5
            },
            new List<Device>() { device5 }
        };

        yield return new object[]
        {
            new Device[]
            {
                device1,
                device2, 
                device3,
                device4, 
                device5,
                device6
            },
            new List<Device>() { device5, device6 }
        };
    }

    #endregion

    #region GetMalfunctionedMemberData

    public static IEnumerable<Object[]> ListOfDevicesWithStatusMalfunction_TestCase()
    {
        Device device1 = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "Defekt", RequestValue = "Accepteret"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", Status = "I brug", RequestValue = "Accepteret"};
        Device device3 = new Device { Id = 3, DeviceName = "Seed device3", SerialNumber = "54543", Status = "På lager", RequestValue = "Accepteret"};
        Device device4 = new Device { Id = 4, DeviceName = "Seed device4", SerialNumber = "1122g", Status = "Defekt", RequestValue = "Accepteret"};
        
        yield return new Object[]
        {
            new Device[]
            {
            },
            new List<Device>()
        };

        yield return new object[]
        {

            new Device[]
            {
                device1, device2, device3
            },
            new List<Device>() { device1 }
        };

        yield return new object[]
        {
            new Device[]
            {
                device1, device2, device3, device4
            },
            new List<Device>() { device1, device4 }
        };
    }

    #endregion
    
    //Tests

    #region CreateDeviceService

    [Fact]
    public void CreateDeviceServiceTest()
    {
        // Arrange 
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();

        // Act 
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Assert
        Assert.NotNull(service);
        Assert.True(service is DeviceService);
    }

    [Theory]
    [InlineData("repository cannot be null")]
    public void CreateDeviceServiceWithMockRepoNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        // Act 
        var action = ()=> new DeviceService(null, mapper, postDeviceValidator, putDeviceValidator); ;
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    } 
    
    [Theory]
    [InlineData("mapper cannot be null")]
    public void CreateDeviceServiceWithMapperNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        // Act 
        var action = ()=> new DeviceService(mockRepository.Object, null, postDeviceValidator, putDeviceValidator); ;
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    } 
    
    [Theory]
    [InlineData("postDeviceValidator cannot be null")]
    public void CreateDeviceServiceWithPutValidatorNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        // Act 
        var action = ()=> new DeviceService(mockRepository.Object, mapper, null, putDeviceValidator); ;
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    } 
    
    [Theory]
    [InlineData("putDeviceValidator cannot be null")]
    public void CreateDeviceServiceWithPostValidatorNullArgumentExceptionTest(string expectedMessage)
    {
        //Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        // Act 
        var action = ()=> new DeviceService(mockRepository.Object, mapper, postDeviceValidator, null); ;
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
    

    #endregion

    #region Read

    [Theory]
    [MemberData(nameof(GetAllDevices_TestCase))]
    public void GetDevices(Device[] data, List<Device> expectedResult)
    {
        // Arrange
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.GetDevices();

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }

    [Theory]
    [InlineData(1, 1)]      //Valid device
    [InlineData(2, 2)]      //Valid device
    public void GetValidDeviceByIdTest(int deviceId, int expectedValueId)
    {
        // Arrange
        Device device1 = new Device { Id = deviceId, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 3, DeviceName = "Seed device2", SerialNumber = "1123"};
        
        var fakeRepo = new List<Device>();
        fakeRepo.Add(device1);
        fakeRepo.Add(device2);
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevice(deviceId)).Returns(fakeRepo.Find(d => d.Id == deviceId));
        
        // Act 
        var actual = service.GetDevice(deviceId);

        // Assert 
        Assert.Equal(expectedValueId, actual.Id);
        Assert.Equal("Seed device1", actual.DeviceName);
        Assert.Equal("1234553", actual.SerialNumber);
        mockRepository.Verify(r => r.GetDevice(deviceId), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "DeviceId cannot be less than 1 or null")]     //Invalid deviceId 0
    [InlineData(-1, "DeviceId cannot be less than 1 or null")]    //Invalid deviceId -1
    [InlineData(null, "DeviceId cannot be less than 1 or null")]  //Invalid deviceId null
    public void GetInvalidDeviceByIdTest(int deviceId, string expectedMessage )
    {
        // Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Act 
        Action action = () => service.GetDevice(deviceId);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r => r.GetDevice(deviceId), Times.Never);
    }

    [Theory]
    [InlineData("1123", 2)]
    public void GetValidDeviceBySerialNumberTest(string serialNumber, int expectedValueId)
    {
        // Arrange
        Device device1 = new Device
        {
            Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "På lager",
            RequestValue = "IkkeSendt"
        };
        Device device2 = new Device
        {
            Id = 2, DeviceName = "Seed device2", SerialNumber = "1123", Status = "På lager", RequestValue = "IkkeSendt"
        };

        var fakeRepo = new List<Device>();
        fakeRepo.Add(device1);
        fakeRepo.Add(device2);

        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config => { config.CreateMap<PostDeviceDTO, Device>(); }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();

        IDeviceService service =
            new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevice(serialNumber))
            .Returns(fakeRepo.Find(d => d.SerialNumber == serialNumber));

        // Act 
        var actual = service.GetDevice(serialNumber);

        // Assert 
        Assert.Equal(expectedValueId, actual.Id);
        Assert.Equal("Seed device2", actual.DeviceName);
        Assert.Equal(serialNumber, actual.SerialNumber);
        mockRepository.Verify(r => r.GetDevice(serialNumber), Times.Once);
    }
    
    [Theory]
    [InlineData("", "SerialNumber cannot be empty or null")]    //Invalid empty serialNumber
    [InlineData(null, "SerialNumber cannot be empty or null")]  //Invalid null serialNumber
    public void GetInvalidDeviceBySerialNumberTest(string serialNumber, string expectedMessage )
    {
        // Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Act 
        Action action = () => service.GetDevice(serialNumber);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r => r.GetDevice(serialNumber), Times.Never);
    }

    #endregion

    #region Create

    [Theory]
    [InlineData(1, "device1", "123", "På lager", "IkkeSendt")]
    public void CreateValidDeviceTest(int deviceId, string deviceName, string serialNumber, string status, string requestValue)
    {
        // Arrange
        List<Device> devices = new List<Device>();
        Device device = new Device{Id = deviceId, DeviceName = deviceName, SerialNumber = serialNumber, Status = status, RequestValue = requestValue};
        PostDeviceDTO dto = new PostDeviceDTO { DeviceName = deviceName, SerialNumber = serialNumber, Status = device.Status, RequestValue = requestValue};
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.AddDevice(It.IsAny<Device>())).Returns(() =>
        {
            devices.Add(device);
            return device;
        });
        
        // Act 
        var createdDevice = service.AddDevice(dto);
        
        // Assert
        Assert.True(devices.Count == 1);
        Assert.Equal(device.Id, createdDevice.Id);
        Assert.Equal(device.DeviceName, createdDevice.DeviceName);
        Assert.Equal(device.SerialNumber, createdDevice.SerialNumber);
        Assert.Equal(device.RequestValue, createdDevice.RequestValue);
        mockRepository.Verify(r => r.AddDevice(It.IsAny<Device>()), Times.Once);
    }
    
    [Theory]
    [InlineData(1, "", "12345678", "På lager", "IkkeSendt", "Device name cannot be empty or null")]                  //Invalid device with empty deviceName
    [InlineData(1, null, "12345678", "På lager", "IkkeSendt", "Device name cannot be empty or null")]                //Invalid device with null as deviceName
    [InlineData(2, "Monitor", "", "På lager", "IkkeSendt", "Device serialNumber cannot be empty or null")]           //Invalid device with empty deviceSerialNumber
    [InlineData(2, "Monitor", null, "På lager", "IkkeSendt", "Device serialNumber cannot be empty or null")]         //Invalid device with empty deviceSerialNumber
    [InlineData(2, "Monitor", "12345678", null, "IkkeSendt", "Incorrect device status")]                             //Invalid device with null status
    [InlineData(2, "Monitor", "12345678", "", "IkkeSendt", "Incorrect device status")]                               //Invalid device with empty status
    [InlineData(2, "Monitor", "12345678","Pdvd", "IkkeSendt", "Incorrect device status")]                            //Invalid device with incorrect value status
    [InlineData(2, "Monitor", "12345678", "På lager", null, "Incorrect device requestValue")]                        //Invalid device with null requestValue
    [InlineData(2, "Monitor", "12345678", "På lager", "", "Incorrect device requestValue")]                          //Invalid device with empty requestValue
    [InlineData(2, "Monitor", "12345678","På lager", "svs", "Incorrect device requestValue")]                        //Invalid device with incorrect value requestValue
    public void CreateInvalidDeviceTest(int deviceId, string deviceName, string serialNumber, string status, string requestValue, string expectedMessage)
    {
        // Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        Device device = new Device{Id = deviceId, DeviceName = deviceName, SerialNumber = serialNumber, Status = status, RequestValue = requestValue};
        PostDeviceDTO dto = new PostDeviceDTO { DeviceName = deviceName, SerialNumber = serialNumber, Status = status, RequestValue = requestValue};
        // Act 
        var action = () => service.AddDevice(dto);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r => r.AddDevice(device), Times.Never);
    }
    #endregion

    #region Update

    [Theory]
    [InlineData(1 , "Computer")]
    public void UpdateValidDeviceTest(int id, string deviceName)
    {
        // Arrange
        Device device = new Device { Id = id, DeviceName = "Seed device1", SerialNumber = "1234553", Status = "På lager", RequestValue = "IkkeSendt"};
        PutDeviceDTO dto = new PutDeviceDTO {Id = device.Id, DeviceName = device.DeviceName, SerialNumber = device.SerialNumber, Status = device.Status, RequestValue = device.RequestValue};

        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutDeviceDTO, Device>();
        }).CreateMapper();
        
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.UpdateDevice(id, It.IsAny<Device>())).Returns(device);

        // Act 
        dto.DeviceName = deviceName;
        Device updateDevice = service.UpdateDevice(id, dto);

        // Assert
        Assert.Equal(device, updateDevice);
        Assert.Equal(device.Id, updateDevice.Id);
        Assert.Equal(device.SerialNumber, updateDevice.SerialNumber);
        Assert.Equal(device.DeviceName, updateDevice.DeviceName);
        Assert.Equal(device.Status, updateDevice.Status);
        mockRepository.Verify(r => r.UpdateDevice(id, It.IsAny<Device>()), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "Laptop", "12345678","På lager", "IkkeSendt", "Device id cannot be null or less than 1")]         //Invalid device with id 0 
    [InlineData(-1, "Computer", "12345678","På lager", "IkkeSendt", "Device id cannot be null or less than 1")]      //Invalid device with id -1
    [InlineData(null, "Computer", "12345678","På lager", "IkkeSendt", "Device id cannot be null or less than 1")]    //Invalid device with null as id
    [InlineData(1, "", "12345678","På lager", "IkkeSendt", "Device name cannot be empty or null")]                   //Invalid device with empty deviceName
    [InlineData(1, null, "12345678","På lager", "IkkeSendt", "Device name cannot be empty or null")]                 //Invalid device with null as deviceName
    [InlineData(2, "Monitor", "","På lager", "IkkeSendt", "Device serialNumber cannot be empty or null")]            //Invalid device with empty deviceSerialNumber
    [InlineData(2, "Monitor", null,"På lager", "IkkeSendt", "Device serialNumber cannot be empty or null")]          //Invalid device with null deviceSerialNumber
    [InlineData(2, "Monitor", "12345678", "", "IkkeSendt", "Incorrect device status")]                               //Invalid device with empty status
    [InlineData(2, "Monitor", "12345678", null, "IkkeSendt", "Incorrect device status")]                             //Invalid device with null status
    [InlineData(2, "Monitor", "12345678", "I stykker", "IkkeSendt", "Incorrect device status")]                      //Invalid device with invalid status message 
    [InlineData(2, "Monitor", "12345678","På lager", null, "Incorrect device requestValue")]                         //Invalid device with null requestValue
    [InlineData(2, "Monitor", "12345678", "På lager", "", "Incorrect device requestValue")]                          //Invalid device with empty requestValue
    [InlineData(2, "Monitor", "12345678", "På lager", "dcs", "Incorrect device requestValue")]                       //Invalid device with invalid value requestValue
    public void InvalidDeviceUpdateTest(int deviceId, string deviceName, string deviceSerialNumber, string status, string requestValue, string expectedMessage)
    {
        // Arrange
        Device device = new Device{Id = deviceId, DeviceName = deviceName, SerialNumber = deviceSerialNumber, Status = status, RequestValue = requestValue};
        PutDeviceDTO dto = new PutDeviceDTO {Id = deviceId, DeviceName = deviceName, SerialNumber = deviceSerialNumber, Status = status, RequestValue = requestValue};
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        
        // Act 
        var action = () => service.UpdateDevice(deviceId, dto);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=> r.UpdateDevice(deviceId, device),Times.Never);
    }

    [Theory]
    [InlineData(2, "Id in the body and route are different")] //Invalid id not the same
    public void InvalidIdInputExceptionTest(int deviceId, string expectedMessage)
    {
        // Arrange
        Device device = new Device{Id = 1, DeviceName = "deviceName", SerialNumber = "deviceSerialNumber", Status = "På lager", RequestValue = "IkkeSendt"};
        PutDeviceDTO dto = new PutDeviceDTO {Id = 1, DeviceName = "deviceName", SerialNumber = "deviceSerialNumber", Status = "På lager", RequestValue = "IkkeSendt"};
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PutDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        
        // Act 
        var action = () => service.UpdateDevice(deviceId, dto);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=> r.UpdateDevice(deviceId, device),Times.Never);
    }

    #endregion 

    #region Delete

    [Theory]
    [InlineData(1, 1)] // Delete device with id 1 and expectedListSize 
    public void DeleteValidDeviceTest(int deviceId, int exceptedListSize)
    {
        // Arrange
        List<Device> devices = new List<Device>();
        Device deviceToDelete = new Device { Id = 1, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 2, DeviceName = "Seed device2", SerialNumber = "1123"};
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(devices);
        mockRepository.Setup(r => r.DeleteDevice(deviceId)).Returns(() =>
        {
            devices.Remove(deviceToDelete);
            return deviceToDelete;
        });
        
        devices.Add(deviceToDelete);
        devices.Add(device2);
        
        // Act 
        var actual = service.DeleteDevice(deviceId);

        // Assert
        Assert.Equal(exceptedListSize, devices.Count);
        Assert.Equal(deviceToDelete, actual);
        Assert.DoesNotContain(deviceToDelete, devices);
        mockRepository.Verify(r=> r.DeleteDevice(deviceId), Times.Once);
    }
    
    [Theory]
    [InlineData(-1, "Device id cannot be null or less than 1")]   //Invalid deviceId -1
    [InlineData(0, "Device id cannot be null or less than 1")]    //Invalid deviceId 0
    [InlineData(null, "Device id cannot be null or less than 1")] //Invalid deviceId null
    public void DeleteInvalidDeviceTest(int deviceId, string expectedMessage)
    {
        // Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Act 
        var action = () => service.DeleteDevice(deviceId);

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r=>r.DeleteDevice(deviceId),Times.Never);
    }

    #endregion

    #region GetAssignedDevices

    [Theory]
    [MemberData(nameof(ListOfDevicesWithUser_TestCase))]
    public void GetValidAssignedDevicesOnUserTest(Device[] data, List<Device> expectedResult)
    {
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.AssignedDevices(1);

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }

    [Theory]
    [InlineData(null, "User id cannot be null or less than 1")] //User ID is null
    [InlineData(0, "User id cannot be null or less than 1")]    //User ID is 0
    [InlineData(-1, "User id cannot be null or less than 1")]   //User ID is -1
    public void GetInvalidAssignedDevicesOnUserTest(int userId, string expectedMessage)
    {
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Act 
        var action = ()=> service.AssignedDevices(userId);
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r => r.GetDevices(), Times.Never);
    }
    #endregion
    
    #region GetRequestValueTests

    [Theory]
    [MemberData(nameof(GetAllIkkeSendt_TestCase))]
    public void GetValidIkkeSendtRequestValue(Device[] data, List<Device> expectedResult)
    {
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.GetDevicesWithRequestValue("IkkeSendt");

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }
    
    [Theory]
    [MemberData(nameof(GetAllSendt_TestCase))]
    public void GetValidSendtRequestValue(Device[] data, List<Device> expectedResult)
    {
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.GetDevicesWithRequestValue("Sendt");

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }
    
    [Theory]
    [MemberData(nameof(GetAllAccepteret_TestCase))]
    public void GetValidAccepteretRequestValue(Device[] data, List<Device> expectedResult)
    {
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.GetDevicesWithRequestValue("Accepteret");

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }
    
    [Theory]
    [InlineData(null, "Value cannot be null or empty")] //Request value is null
    [InlineData("", "Value cannot be null or empty")]   //Request value is empty
    public void GetInvalidRequestValue(string value, string expectedMessage)
    {
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);

        // Act 
        var action = ()=> service.GetDevicesWithRequestValue(value);
        var ex = Assert.Throws<ArgumentException>(action);

        // Assert
        Assert.Equal(expectedMessage, ex.Message);
        mockRepository.Verify(r => r.GetDevices(), Times.Never);
    }
    
    #endregion

    #region GetMalfunctionedDevicesTest

    [Theory]
    [MemberData(nameof(ListOfDevicesWithStatusMalfunction_TestCase))]
    public void GetDevicesWithStatusMalfunctionTest(Device[] data, List<Device> expectedResult)
    {
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        var mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<PostDeviceDTO, Device>();
        }).CreateMapper();
        var postDeviceValidator = new PostDeviceValidator();
        var putDeviceValidator = new PutDeviceValidator();
        
        IDeviceService service = new DeviceService(mockRepository.Object, mapper, postDeviceValidator, putDeviceValidator);
        mockRepository.Setup(r => r.GetDevices()).Returns(fakeRepo);

        // Act 
        var actual = service.GetDevicesWithStatusMalfunction();

        // Assert
        Assert.Equal(expectedResult, actual);
        Assert.True(Enumerable.SequenceEqual(expectedResult, actual));
        mockRepository.Verify(r => r.GetDevices(), Times.Once);
    }

    #endregion
    
}