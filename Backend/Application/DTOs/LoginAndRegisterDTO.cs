namespace Application.DTOs;

public class RegisterDTO
{
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string WorkNumber { get; set; }
    public string? PrivateNumber { get; set; }
    public string? PrivateMail { get; set; }
    
}

public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}