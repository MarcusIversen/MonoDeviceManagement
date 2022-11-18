using Domain;

namespace ServiceTestProject;

public class DeviceServiceTest
{
    [Fact]
    public void CreateDeviceServiceTest()
    {
        
    }
    
    [Fact]
    public void GetDevices()
    {
        
    }
    
    [Theory]
    [InlineData(1)]     //Valid device
    public int GetValidDeviceTest(int deviceID)
    {
        return deviceID;
    }
    
    [Theory]
    [InlineData(0)]     //Invalid device
    [InlineData(-1)]    //Invalid device
    public int GetInvalidDeviceTest(int deviceID)
    {
        return deviceID;
    }

    [Fact]
    public Device CreateValidDeviceTest()
    {
        return null;
    }
    
    [Theory]
    [InlineData(0, "Laptop", "12345678", 1)]        //Invalid device with ID 0 
    [InlineData(-1, "Computer", "12345678", 1)]     //Invalid device with ID -1
    [InlineData(1, "", "12345678", 1)]              //Invalid device with empty deviceName
    [InlineData(2, "Monitor", "", 1)]               //Invalid device with empty deviceSerialNumber
    [InlineData(3, "iPad", "12345678", 0)]          //Invalid device with empty amount
    public int CreateInvalidDeviceTest(int deviceID, string deviceName, string deviceSerialNumber, int amount)
    {
        return deviceID;
    }
    
    [Theory]
    [InlineData(1, "iPad", "12345678", 1)]       //Existing device
    [InlineData(2, "Monitor", "87654321", 1)]    //Existing device 
    public int CreateExistingDeviceTest(int deviceID, string deviceName, string deviceSerialNumber, int number)
    {
        return deviceID;
    }
    
    [Fact]
    public int UpdateValidDeviceTest()
    {
        return UpdateValidDeviceTest();
    }
    
    [Theory]
    [InlineData(0, "Laptop", "12345678", 1)]    //Invalid device with ID 0 
    [InlineData(-1, "Computer", "12345678", 1)] //Invalid device with ID -1
    [InlineData(1, "", "12345678", 1)]          //Invalid device with empty deviceName
    [InlineData(2, "Monitor", "", 1)]           //Invalid device with empty deviceSerialNumber
    [InlineData(3, "iPad", "12345678", 0)]      //Invalid device with empty amount
    public int InvalidDeviceUpdateTest(int deviceID, string deviceName, string deviceSerialNumber, int amount)
    {
        return deviceID;
    }

    [Fact]
    public void DeleteValidDeviceTest()
    {
        
    }
    
    [Fact]
    public void DeleteInvalidDeviceTest()
    {
        
    }
    
    [Fact]
    public void AddValidUserOnDeviceTest()
    {
        
    }
    
    [Theory]
    [InlineData(-1, 1)] //Invalid deviceID
    [InlineData(0, 2)]  //Invalid deviceID
    [InlineData(1, -1)] //Invalid userID
    [InlineData(2, 0)]  //Invalid userID
    public void AddInvalidUserOnDeviceTest(int deviceID, int userID)
    {
        
    }
    
    [Fact]
    public void DeleteValidUserOnDeviceTest()
    {
        
    }
    
    [Theory]
    [InlineData(1, -1)]     //Invalid userID
    [InlineData(2, 0)]      //Invalid userID
    [InlineData(2, 555)]    //Nonexisting userID
    public void DeleteInvalidUserOnDeviceTest(int deviceID, int userID)
    {
        
    }
    
    [Fact]
    public void UpdateValidUserOnDeviceTest()
    {
        
    }
    
    [Theory]
    [InlineData(-1, 1)] //Invalid deviceID
    [InlineData(0, 2)]  //Invalid deviceID
    [InlineData(1, -1)] //Invalid userID
    [InlineData(2, 0)]  //Invalid userID
    public void UpdateInvalidUserOnDeviceTest(int deviceID, int userID)
    {
        
    }


    [Fact]
    public void GetValidAssignedDevicesOnUserTest()
    {
        
    }

    [Fact]
    public void GetInvalidAssignedDevicesOnUserTest()
    {
        
    }
    
    
}