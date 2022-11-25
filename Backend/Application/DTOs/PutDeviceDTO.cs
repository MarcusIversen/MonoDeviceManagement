using Domain;
using Domain.Enums;

namespace Application.DTOs;

public class PutDeviceDTO
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    
    public int? UserId { get; set; }
    public Status Status { get; set; }
    public DateOnly? DateOfIssue { get; set; }
    public DateOnly? DateOfTurnIn { get; set; }

}