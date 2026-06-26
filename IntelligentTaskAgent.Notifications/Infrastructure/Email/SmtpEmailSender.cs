using IntelligentTaskAgent.Notifications.Domain.Entities;
using IntelligentTaskAgent.Notifications.Domain.Enums;
using IntelligentTaskAgent.Notifications.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace IntelligentTaskAgent.Notifications.Infrastructure.Email
{
    public class SmtpEmailSender : INotificationSender
    {
        private readonly EmailOptions options;

        public NotificationChannelType ChannelType => NotificationChannelType.Email;

        public SmtpEmailSender(IOptions<EmailOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendAsync(
            string destination,
            string message,
            string? subject = null)
        {
            using var client = new SmtpClient(options.Host, options.Port)
            {
                EnableSsl = options.EnableSsl,
                Credentials = new NetworkCredential(
                    options.Username,
                    options.Password)
            };

            var emailBody = ConstructEmailBody(message);
            var emailSubject = subject ?? "Task Reminder ⏰"; 

            var mail = new MailMessage(
                options.From,
                destination,
                subject: emailSubject,
                body:emailBody)
                       {
                        IsBodyHtml = true
                       };

            await client.SendMailAsync(mail);
        }

private string ConstructEmailBody(string message)
    {
        // Encode the message to prevent HTML breaking characters
        var safeMessage = HttpUtility.HtmlEncode(message);

        return $"""
       <html>
       <body style="font-family: Arial, sans-serif; line-height: 1.6; color: #333;">
           <div style="max-width: 600px; margin: auto; border: 1px solid #eee; padding: 20px;">
               <h2 style="color: #2c3e50; margin-top: 0;">Task Reminder</h2>
               <p>Hello,</p>
               <p>You have a new reminder:</p>
               <div style="background: #f9f9f9; border-left: 5px solid #ccc; padding: 15px; margin: 15px 0; font-style: italic; min-height: 20px;">
                   {safeMessage}
               </div>
               <p style="margin-bottom: 0;">Regards,<br>
               <strong>Task Agent</strong></p>
           </div>
       </body>
       </html>
       """;
    }
}
}
