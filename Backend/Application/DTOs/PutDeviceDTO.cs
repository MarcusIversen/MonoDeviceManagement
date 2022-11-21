using Domain;

namespace Application.DTOs;

public class PutDeviceDTO
{
    public int Id { get; set; }
    public string DeviceName { get; set; }
    public string SerialNumber { get; set; }
    public int Amount { get; set; }
    public int? UserId { get; set; }
}