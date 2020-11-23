using SendGrid;
using SendGrid.Helpers.Mail;
using Support_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_API.Tools
{
    public static class Email
    {
        public static async Task<Response> SingleEmail(SingleEmailPost email, string ApiKey)
        {
            var client = new SendGridClient(ApiKey);
            var from = new EmailAddress(email.From_Email, email.From_Name);
            var to = new EmailAddress(email.To_Email, email.To_Name);
            var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Content_Plain, email.Content_Html);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
