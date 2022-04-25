using System.Net;
using System.Net.Mail;

namespace CodeRabbits.KaoList.Web.Services;
public class EmailSender : IEmailSender
{

    private readonly MailAddress _account;
    private readonly string _password;

    protected ILogger<EmailSender> Logger { get; init; }

    public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
    {
        var emailSections = configuration.GetSection("Email:Coderabbits");
        _account = new MailAddress(emailSections["Account"]);
        _password = emailSections["Password"];
        Logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = _account,
            Subject = subject,
            Body = htmlMessage,
            BodyEncoding = System.Text.Encoding.UTF8,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        using var smtpClient = GetSmtpClient(_account, _password);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (SmtpException e)
        {
            Logger.LogWarning(e.Message);
            Logger.LogWarning(e.StackTrace);
        }
    }

    private SmtpClient GetSmtpClient(MailAddress address, string password) => new()
    {
        Host = address.Host,
        Credentials = new NetworkCredential(
                address.Address,
                password
            ),
        Timeout = 30,
        EnableSsl = true,
        Port = 587
    };
}

