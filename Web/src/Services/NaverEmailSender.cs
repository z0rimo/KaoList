// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CodeRabbits.KaoList.Web.Services
{
    public class NaverEmailSender : IEmailSender
    {
        private readonly string? _smtpUsername;
        private readonly string? _smtpPassword;

        public NaverEmailSender(IConfiguration configuration)
        {
            _smtpUsername = configuration[AuthenticationKey.NaverClientId];
            _smtpPassword = configuration[AuthenticationKey.NaverClientClientSecret];
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Attempting to send email to {email} with subject {subject}");

            var SmtpServer = new SmtpClient("smtp.naver.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(email);
            Console.WriteLine($"Sending email to {email}");
            SmtpServer.Send(mail);
            Console.WriteLine($"Successfully sent email to {email}");

            return Task.CompletedTask;
        }
    }
}
