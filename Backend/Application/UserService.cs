using System.Net;
using System.Net.Mail;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private IMapper _mapper;
    private IValidator<PutUserDTO> _putUserValidator;
    private readonly AppSettings _appSettings;

    public UserService(
        IUserRepository repository,
        IMapper mapper,
        IValidator<PutUserDTO> putUserValidator,
        IOptions<AppSettings> appSettings)
    {
        _repository = repository ?? throw new ArgumentException("repository cannot be null");
        _mapper = mapper ?? throw new ArgumentException("mapper cannot be null");
        _putUserValidator = putUserValidator ?? throw new ArgumentException("putUserValidator cannot be null");
        _appSettings = appSettings.Value ?? throw new ArgumentException("appSettings cannot be null");
    }

    public UserService(IUserRepository repository, IMapper mapper, IValidator<PutUserDTO> putUserValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _putUserValidator = putUserValidator;
    }

    public List<User> GetUsers()
    {
        return _repository.GetUsers().ToList();
    }

    public User GetUser(int userId)
    {
        if (userId == null || userId < 1)
        {
            throw new ArgumentException("UserId cannot be less than 1 or null");
        }

        return _repository.GetUser(userId);
    }

    public User GetUserByEmail(string email)
    {
        if (email == null || email == "")
        {
            throw new ArgumentException("Email cannot be null or empty");
        }

        return _repository.GetUserByEmail(email);
    }

    public User UpdateUser(int userId, PutUserDTO user)
    {
        ThrowsIfPutUserIsInvalid(user);
        var validate = _putUserValidator.Validate(user);
        if (!validate.IsValid)
        {
            throw new ArgumentException(validate.ToString());
        }

        if (userId != user.Id)
        {
            throw new ArgumentException("Id in the body and route are different");
        }

        return _repository.UpdateUser(userId, _mapper.Map<User>(user));
    }

    public User DeleteUser(int userId)
    {
        if (userId == null || userId < 1)
        {
            throw new ArgumentException("User id cannot be null or less than 1");
        }

        return _repository.DeleteUser(userId);
    }

    public List<User> GetRoleTypeUser()
    {
        return _repository.GetUsers().Where(u => u.Role == "User").ToList();
    }

    public void SendEmail(string toMail, string subject, string body)
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(_appSettings.Email);
            mail.To.Add(new MailAddress(toMail));
            mail.Subject = subject.Replace("\n", "<br>").Replace("\r", "<br>");
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = body.Replace("\n", "<br>").Replace("\r", "<br>");
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential(_appSettings.Email, _appSettings.Password);
                client.EnableSsl = true;
                client.Send(mail);
                Console.WriteLine(mail);
            }
        }
    }

    //Used to throw errors
    private void ThrowsIfPutUserIsInvalid(PutUserDTO user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentException("Email cannot be null, empty and must be a valid email");
        }

        if (string.IsNullOrEmpty(user.FirstName))
        {
            throw new ArgumentException("First name cannot be null or empty");
        }

        if (string.IsNullOrEmpty(user.LastName))
        {
            throw new ArgumentException("Last name cannot be null or empty");
        }

        if (string.IsNullOrEmpty(user.WorkNumber) || user.WorkNumber.Length < 8)
        {
            throw new ArgumentException(
                "Work number cannot be null, empty and must have a minimum length greater than 7");
        }

        if (user.Id < 1)
        {
            throw new ArgumentException("Id cannot be null or less than 1");
        }
    }
}