using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Vinance.Contracts.Interfaces;

namespace Vinance.Services.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _sendGridClient;
        private readonly string _url;

        public EmailSender(SendGridClient sendGridClient, IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _url = configuration["Vinance-MVC-url"];
        }

        public async Task SendEmail(string userName, string email, string token)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("noreply@vinance.com", "Vinance"));
            msg.AddTo(new EmailAddress(email, userName));
            msg.SetTemplateId("d-40183e562ed34108bd3b08059a08a570");
            var data = new MailData(userName, $"{_url}/confirm-email?email={email}&token={token}");
            msg.SetTemplateData(data);

            var response = await _sendGridClient.SendEmailAsync(msg);

            if ((int)response.StatusCode <= 200 || (int)response.StatusCode > 299)
            {
                throw new Exception("There was an error sending the email.");
            }
        }

        private class MailData
        {
            public MailData(string userName, string link)
            {
                UserName = userName;
                Link = link;
            }

            [JsonProperty("username")]
            public string UserName { get; set; }

            [JsonProperty("link")]
            public string Link { get; set; }
        }
    }
}