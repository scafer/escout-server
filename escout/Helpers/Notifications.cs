using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace escout.Helpers
{
    public static class Notifications
    {
        public static async Task SendEmail(string emailTo, string subject, string content)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var sendKey = Environment.GetEnvironmentVariable("SENDGRID_EMAIL");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sendKey, "eScout App");
            var to = new EmailAddress(emailTo, emailTo);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg);
        }
    }
}
