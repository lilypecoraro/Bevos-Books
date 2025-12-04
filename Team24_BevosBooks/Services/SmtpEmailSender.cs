using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Team24_BevosBooks.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string smtpServer = _config["EmailSettings:SmtpServer"];
            int smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
            string senderName = _config["EmailSettings:SenderName"];
            string senderEmail = _config["EmailSettings:SenderEmail"];
            string username = _config["EmailSettings:Username"];
            string password = _config["EmailSettings:Password"];

            var message = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(email);

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(username, password);

                await client.SendMailAsync(message);
            }
        }
    }
}