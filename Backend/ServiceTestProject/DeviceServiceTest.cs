using Application;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Moq;

namespace ServiceTestProject;

public class DeviceServiceTest
{
    // Member data

    #region GetAllDevicesTest

    public static IEnumerable<Object[]> GetAllDevices_TestCase()
    {
        Device device1 = new Device { Id = 1, Amount = 1, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 2, Amount = 1, DeviceName = "Seed device2", SerialNumber = "1123"};
        Device device3 = new Device { Id = 3, Amount = 1, DeviceName = "Seed device3", SerialNumber = "54543"};

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
    
    
    [Fact]
    public void CreateDeviceServiceTest()
    {
        // Arrange 
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        
        
        // Act 
        IDeviceService service = new DeviceService(mockRepository.Object);

        // Assert
        Assert.NotNull(service);
        Assert.True(service is DeviceService);

    }
    
    [Theory]
    [MemberData(nameof(GetAllDevices_TestCase))]
    public void GetDevices(Device[] data, List<Device> expectedResult)
    {
        // Arrange
        var fakeRepo = data;
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        IDeviceService service = new DeviceService(mockRepository.Object);
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
    public void GetValidDeviceTest(int deviceId, int expectedValueId)
    {
        // Arrange
        // Devices
        Device device1 = new Device { Id = 1, Amount = 1, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 2, Amount = 1, DeviceName = "Seed device2", SerialNumber = "1123"};
        
        var fakeRepo = new List<Device>();
        fakeRepo.Add(device1);
        fakeRepo.Add(device2);
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        IDeviceService service = new DeviceService(mockRepository.Object);
        mockRepository.Setup(r => r.GetDevice(deviceId)).Returns(fakeRepo.Find(d => d.Id == deviceId));
        
        // Act 
        var actual = service.GetDevice(deviceId);

        // Assert 
        Assert.Equal(expectedValueId, actual.Id);
        mockRepository.Verify(r => r.GetDevice(deviceId), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "DeviceId cannot be less than 1 or null")]     //Invalid deviceId 0
    [InlineData(-1, "DeviceId cannot be less than 1 or null")]    //Invalid deviceId -1
    [InlineData(null, "DeviceId cannot be less than 1 or null")]  //Invalid deviceId null
    public void GetInvalidDeviceTest(int deviceId, string expectedMessage )
    {
        // Arrange
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        IDeviceService service = new DeviceService(mockRepository.Object);

        // Act 
        Action action = () => service.GetDevice(deviceId);  

        // Assert
        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("DeviceId cannot be less than 1 or null", ex.Message);
        
        mockRepository.Verify(r => r.GetDevice(deviceId), Times.Never);

    }
    
    [Theory]
    [InlineData(1, "device1", 1, "123")]
    public void CreateValidDeviceTest(int deviceId, string deviceName, int amount, string serialNumber)
    {
        // Arrange
        List<Device> devices = new List<Device>();
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        IDeviceService service = new DeviceService(mockRepository.Object);
        mockRepository.Setup(r => r.AddDevice(It.IsAny<Device>())).Callback<Device>(d => devices.Add(d));
        Device device = new Device(){DeviceName = deviceName, Amount = amount, SerialNumber = serialNumber};
        devices.Add(device);

        // Act 
        //service.AddDevice(device);
        
        // Assert
        Assert.True(devices.Count == 1);
        Assert.Equal(device, devices[0]);
        mockRepository.Verify(r => r.AddDevice(device), Times.Once);
    }
    
    [Theory]
    [InlineData(0, "Laptop", "12345678", 1)]        //Invalid device with id 0 
    [InlineData(-1, "Computer", "12345678", 1)]     //Invalid device with id -1
    [InlineData(null, "Computer", "12345678", 1)]   //Invalid device with null as id
    [InlineData(1, "", "12345678", 1)]              //Invalid device with empty deviceName
    [InlineData(1, null, "12345678", 1)]            //Invalid device with null as deviceName
    [InlineData(2, "Monitor", "", 1)]               //Invalid device with empty deviceSerialNumber
    [InlineData(2, "Monitor", null, 1)]             //Invalid device with empty deviceSerialNumber
    [InlineData(3, "iPad", "12345678", 0)]          //Invalid device with amount 0
    [InlineData(3, "iPad", "12345678", -1)]         //Invalid device with amount -1
    [InlineData(3, "iPad", "12345678", null)]       //Invalid device with null as amount
    public void CreateInvalidDeviceTest(int deviceId, string deviceName, string deviceSerialNumber, int amount)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Theory]
    [InlineData(1, "iPad", "12345678", 1)]       //Existing device
    [InlineData(2, "Monitor", "87654321", 1)]    //Existing device 
    public void CreateExistingDeviceTest(int deviceId, string deviceName, string deviceSerialNumber, int number)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Fact]
    public void UpdateValidDeviceTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Theory]
    [InlineData(0, "Laptop", "12345678", 1)]      //Invalid device with id 0 
    [InlineData(-1, "Computer", "12345678", 1)]   //Invalid device with id -1
    [InlineData(null, "Computer", "12345678", 1)] //Invalid device with null as id
    [InlineData(1, "", "12345678", 1)]            //Invalid device with empty deviceName
    [InlineData(1, null, "12345678", 1)]          //Invalid device with null as deviceName
    [InlineData(2, "Monitor", "", 1)]             //Invalid device with empty deviceSerialNumber
    [InlineData(2, "Monitor", null, 1)]           //Invalid device with null as deviceSerialNumber
    [InlineData(3, "iPad", "12345678", 0)]        //Invalid device with amount 0 
    [InlineData(3, "iPad", "12345678", 0)]        //Invalid device with amount -1
    [InlineData(3, "iPad", "12345678", null)]     //Invalid device with null as amount
    public void InvalidDeviceUpdateTest(int deviceId, string deviceName, string deviceSerialNumber, int amount)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }

    [Theory]
    [InlineData(1, 1)]
    public void DeleteValidDeviceTest(int deviceId, int listSize)
    {
        // Arrange
        List<Device> devices = new List<Device>();
        
        Device deviceToDelete = new Device { Id = deviceId, Amount = 1, DeviceName = "Seed device1", SerialNumber = "1234553"};
        Device device2 = new Device { Id = 2, Amount = 1, DeviceName = "Seed device2", SerialNumber = "1123"};
        
        Mock<IDeviceRepository> mockRepository = new Mock<IDeviceRepository>();
        IDeviceService service = new DeviceService(mockRepository.Object);
        mockRepository.Setup(r => r.GetDevices()).Returns(devices);
        //mockRepository.Setup(r => r.DeleteDevice(deviceId)).Callback<int>(d => devices.Remove(d == deviceId)).Returns(deviceToDelete);
        //mockRepository.Setup(r => r.DeleteDevice(It.IsAny<int>())).Callback<int>(devices.Remove(deviceToDelete == )).Returns(deviceToDelete);
        
        devices.Add(deviceToDelete);
        devices.Add(device2);
        
        // Act 
        var actual = service.DeleteDevice(deviceId);

        // Assert
        Assert.Equal(listSize, devices.Count);
        mockRepository.Verify(r=> r.DeleteDevice(deviceId), Times.Once);
    }
    
    [Theory]
    [InlineData(-1)]   //Invalid deviceId -1
    [InlineData(0)]    //Invalid deviceId 0
    [InlineData(null)] //Invalid deviceId null
    [InlineData(255)]  //Nonexisting deviceId
    public void DeleteInvalidDeviceTest(int deviceId)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Fact]
    public void AddValidUserOnDeviceTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Theory]
    [InlineData(-1, 1)]    //Invalid deviceId -1
    [InlineData(0, 2)]     //Invalid deviceId 0 
    [InlineData(null, 2)]  //Invalid deviceId null 
    [InlineData(1, -1)]    //Invalid userId -1 
    [InlineData(2, 0)]     //Invalid userId 0 
    [InlineData(2, null)]  //Invalid userId null
    public void AddInvalidUserOnDeviceTest(int deviceId, int userId)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Fact]
    public void DeleteValidUserOnDeviceTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Theory]
    [InlineData(1, -1)]     //Invalid userId -1
    [InlineData(2, 0)]      //Invalid userId 0 
    [InlineData(2, 555)]    //Nonexisting userId
    [InlineData(2, null)]   //Invalid userId null
    [InlineData(-1, 1)]     //Invalid deviceId -1
    [InlineData(0, 1)]      //Invalid deviceId 0 
    [InlineData(222, 1)]    //Nonexisting deviceId
    [InlineData(null, 1)]   //Invalid userId null
    public void DeleteInvalidUserOnDeviceTest(int deviceId, int userId)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Fact]
    public void UpdateValidUserOnDeviceTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    [Theory]
    [InlineData(-1, 1)]     //Invalid deviceId -1
    [InlineData(0, 2)]      //Invalid deviceId 0 
    [InlineData(null, 2)]   //Invalid deviceId null
    [InlineData(1, -1)]     //Invalid userId -1
    [InlineData(2, 0)]      //Invalid userId 0
    [InlineData(2, null)]   //Invalid userId null
    public void UpdateInvalidUserOnDeviceTest(int deviceId, int userId)
    {
        // Arrange
        
        // Act 
        
        // Assert
    }


    [Fact]
    public void GetValidAssignedDevicesOnUserTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }

    [Fact]
    public void GetInvalidAssignedDevicesOnUserTest()
    {
        // Arrange
        
        // Act 
        
        // Assert
    }
    
    
}