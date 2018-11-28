using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Vinance.Contracts.Interfaces;
using Vinance.Contracts.Models.Identity;

namespace Vinance.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _sendGridClient;

        public EmailSender(SendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmail(string email, string message)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@vinance.com", "Vinance"),
                Subject = "Email confirmation",
                HtmlContent = $@"Dear user,
please click <a href='https://localhost:44366/confirm-email?email={email}&token={message}'>here</a> to confirm your email address."
            };
            msg.AddTo(new EmailAddress(email));
            var response = await _sendGridClient.SendEmailAsync(msg);
            if ((int) response.StatusCode <= 200 || (int) response.StatusCode > 299)
            {
                throw new Exception("There was an error sending the email.");
            }
        }
    }
}