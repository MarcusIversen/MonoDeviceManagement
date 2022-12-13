namespace Application.DTOs;

public class PutDeviceDTO
{
    public int Id { get; set; }
  
    public string DeviceName { get; set; }
  
    public string SerialNumber { get; set; }
  
    public string Status { get; set; }
  
    public int? UserId { get; set; }
  
    public DateOnly? DateOfIssue { get; set; }
    public DateOnly? DateOfTurnIn { get; set; }
  
    public string RequestValue { get; set; }
    public int? RequesterId { get; set; }
  
    public string? ErrorSubject { get; set; }
    public string? ErrorDescription { get; set; }
  
    
  
}