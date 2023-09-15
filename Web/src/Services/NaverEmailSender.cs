// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CodeRabbits.KaoList.Web.Services
{
    public class NaverEmailSender : IEmailSender
    {
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public NaverEmailSender(
            string smtpUsername,
            string smtpPassword
            )
        {
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.naver.com");
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(_smtpUsername),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(email);
            SmtpServer.Send(mail);

            return Task.CompletedTask;
        }
    }
}
