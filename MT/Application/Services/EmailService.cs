using System.Net.Mail;
using System.Net;

namespace Application.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendEmailsAsync(List<string> to, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        public async Task SendEmailsAsync(List<string> to, string subject, string body)
        {
            try
            {
                var smtpSettings = new SmtpSettings
                {
                    Username = "staging.app.noreply@gmail.com",
                    Password = "iznszfqpultnvweu",
                    Server = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                };

                if (smtpSettings == null)
                    throw new Exception("SMTP settings are not configured.");

                var client = new SmtpClient(smtpSettings.Server, smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                    EnableSsl = smtpSettings.EnableSsl
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpSettings.Username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                to.ForEach(_ => mailMessage.To.Add(_));

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await SendEmailsAsync(new List<string> { to }, subject, body);
        }
    }

    public class SmtpSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
