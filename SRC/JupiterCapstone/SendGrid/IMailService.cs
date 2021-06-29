using JupiterCapstone.SendGrid;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Microsoft.Extensions.Configuration;

namespace JupiterCapstone.SendGrid
{
    public interface IMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        
    }

    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ekeneanolue@outlook.com", "ADUABA TEAM JUPITER");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }

}