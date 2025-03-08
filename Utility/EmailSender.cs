using System;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace OnlineShop.Utility;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Execute(email, subject, htmlMessage);
    }

    public async Task Execute(string email, string subject, string body)
    {
        MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"))
        {
           // Version = ApiVersion.V3_1,
        };
        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
           .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "pilot@mailjet.com"},
                  {"Name", "Mailjet Pilot"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", email},
                   {"Name", "passenger 1"}
                   }
                  }},
                 {"Subject", subject},
                 {"TextPart", "Dear passenger 1, welcome to Mailjet! May the delivery force be with you!"},
                 {"HTMLPart", body}
                 }
               });
        MailjetResponse response = await client.PostAsync(request);
    }


}
