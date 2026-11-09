using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Mini_Cinema.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
        "thooomasmaged@gmail.com",
        "your password" // i have problem in MyEmail Account Cannot Support the app password Service
    )
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: "thooomasmaged@gmail.com",
                    to: email,
                    subject: subject,
                    body: htmlMessage
                )
                {
                    IsBodyHtml = true
                }
            );
        }
    }

}
