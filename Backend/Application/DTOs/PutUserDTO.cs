using Domain.Enums;

namespace Application.DTOs;

public class PutUserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string WorkNumber { get; set; }
    public string? PrivateNumber { get; set; }
    public string? PrivateMail { get; set; }
}