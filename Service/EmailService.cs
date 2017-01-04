using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;


namespace Service
{
    public class SendGridEmailService : IIdentityMessageService
    {
        private readonly string sendGridApiKey;
        private string SendGridApiKey { get { return sendGridApiKey;} }


        public string Sender { get; set; }
        public string SenderName { get; set; }


        public SendGridEmailService(string key)
        {
            this.sendGridApiKey = key;
        }
        


        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }
                
        private async Task ConfigSendGridAsync(IdentityMessage message)
        {
            dynamic sg = new SendGridAPIClient(SendGridApiKey);
            Email from = new Email(this.Sender);            
            string subject = message.Subject;
            Email to = new Email(message.Destination);
            Content content = new Content("text/plain", message.Body);
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }

       
    }
}
