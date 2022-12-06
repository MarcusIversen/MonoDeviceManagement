namespace Domain;

public class Device
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    public string Status { get; set; }
    public User? User { get; set; } 
    public int? UserId { get; set; }
    public DateOnly? DateOfIssue { get; set; }
    public DateOnly? DateOfTurnIn { get; set; }
    public string RequestEnum { get; set; }
}