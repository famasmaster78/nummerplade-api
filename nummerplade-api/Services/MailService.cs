using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace nummerplade_api.Classes
{
    public class MailService
    {
        // Configuration
        private IConfiguration configuration;

        public MailService()
        {

            // Load configuration from appsettings.json
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        }

        public void sendMail(string subject, string body, string toAddress, string toName)
        {

            // Create new message
            var message = new MimeMessage();

            // Update message
            message.To.Add(new MailboxAddress(toName, toAddress));
            message.From.Add(new MailboxAddress(configuration["MailService:Name"], configuration["MailService:Email"]));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            // Initialize new SMTPClient for each request.
            using (var client = new SmtpClient())
            {
                // Connect to SMTP Server
                client.Connect(configuration["MailService:SMTP:Host"], Convert.ToInt16(configuration["MailService:SMTP:Port"]), MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with SMTP server
                client.Authenticate(configuration["MailService:Username"], configuration["MailService:Password"]);

                // Send message
                client.Send(message);

                // Disconnect
                client.Disconnect(true);
            }
        }

    }
}

