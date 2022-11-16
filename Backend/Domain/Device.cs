namespace Domain;

public class Device
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    public int Amount { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }
}