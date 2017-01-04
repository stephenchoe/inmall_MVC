using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;


namespace Web.Core
{
    public interface ISendGridEmailService
    {
        string Sender { get; set; }
        string SenderName { get; set; }
        //void Send(string subject, string destination, string body, string sender = "", string senderName = "");
        Task SendAsync(string subject, string destination, string body, string sender = "", string senderName = "");
    }
    public class SendGridEmailService : ISendGridEmailService
    {
        private readonly string sendGridApiKey;
        private string SendGridApiKey { get { return sendGridApiKey; } }


        public string Sender { get; set; }
        public string SenderName { get; set; }

        public SendGridEmailService(string key)
        {
            this.sendGridApiKey = key;
        }

        public async Task SendAsync(string subject, string destination, string body, string sender = "", string senderName = "")
        {
            if (sender != "") Sender = sender;
            if (senderName != "") SenderName = senderName;
            await ConfigSendGridAsync(subject, destination, body);
        }

        //public void Send(string subject, string destination, string body, string sender = "", string senderName = "")
        //{
        //    SendAsync(subject, destination, body, sender, senderName).Wait();
  

        //}
        private async Task ConfigSendGridAsync(string subject, string destination, string body)
        {
            dynamic sg = new SendGridAPIClient(SendGridApiKey);

            Email from = new Email(Sender);
            Email to = new Email(destination);
            Content content = new Content("text/html", body);
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());

        }
    }
}
