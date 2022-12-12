namespace Application.Helpers;

public class AppSettings
{
    //You just manually type your own secret, email and password into the appsettings.json file. 
    //The secret is used for storing passwords in the database.
    //The email and password is used for sending emails from the application.
    public string Secret { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}