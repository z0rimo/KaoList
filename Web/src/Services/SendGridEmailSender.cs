// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CodeRabbits.KaoList.Web.Services;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridClient _client;
    private readonly EmailAddress _from;
    private readonly string? _sendGridAPIKey;

    public SendGridEmailSender(IConfiguration configuration)
    {
        _sendGridAPIKey = configuration[AuthenticationKey.SendGridApiKey];
        _client = new SendGridClient(_sendGridAPIKey);
        _from = new EmailAddress("noreply@kaolist.com", "Kaolist");
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"Attempting to send email to {email} with subject {subject}");

        var to = new EmailAddress(email);
        var plainTextContent = htmlMessage;
        var htmlContent = $"<strong>{htmlMessage}</strong>";
        var msg = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);

        try
        {
            var response = await _client.SendEmailAsync(msg);
            Console.WriteLine($"Email to {email} sent with status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}
