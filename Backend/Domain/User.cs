namespace Domain;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Salt { get; set; }
    public string Hash { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string WorkNumber { get; set; }
    public string? PrivateNumber { get; set; }
    public string WorkMail { get; set; }
    public string? PrivateMail { get; set; }
    public List<Device> Devices { get; set; }
}