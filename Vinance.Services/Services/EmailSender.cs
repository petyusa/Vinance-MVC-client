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

        public async Task SendEmail(VinanceUser user)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("noreply@vinance.com", "Vinance"),
                Subject = "Sending with SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, even with C#",
                HtmlContent = "<strong>and easy to do anywhere, even with C#</strong>"
            };
            msg.AddTo(new EmailAddress(user.Email, $"{user.FirstName} {user.LastName}"));
            var response = await _sendGridClient.SendEmailAsync(msg);
            if ((int) response.StatusCode <= 200 || (int) response.StatusCode > 299)
            {
                throw new Exception("There was an error sending the email.");
            }
        }
    }
}