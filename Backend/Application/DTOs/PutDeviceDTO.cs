

namespace Application.DTOs;

public class PutDeviceDTO
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    
    public int? UserId { get; set; }
    public string Status { get; set; }
    public string? DateOfIssue { get; set; }
    public string? DateOfTurnIn { get; set; }

}