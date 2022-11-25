using Domain;
using Domain.Enums;

namespace Application.DTOs;

public class PostDeviceDTO
{
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    public int Amount { get; set; }
    public User? User { get; set; }
    public int? UserId { get; set; }
    public Status Status { get; set; }
    public DateOnly? DateOfIssue { get; set; }
    public DateOnly? DateOfTurnIn { get; set; }
}