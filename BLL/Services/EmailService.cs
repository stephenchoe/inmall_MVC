using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Web.Core;

namespace BLL
{
    public interface IEmailService : IIdentityMessageService
    {
        void SetSender(string sender);
        void SetSenderName(string senderName);
        // void Send(string subject, string destination, string body, string sender = "", string senderName = "");
        Task SendAsync(string subject, string destination, string body, string sender = "", string senderName = "");

    }
    public class AppEmailService : IEmailService
    {
        private readonly ISendGridEmailService sendGridEmailService;
        public AppEmailService(ISendGridEmailService sendGridEmailService)
        {
            this.sendGridEmailService = sendGridEmailService;
        }

        //IIdentityMessageService Member
        public async Task SendAsync(IdentityMessage message)
        {
            await sendGridEmailService.SendAsync(message.Subject, message.Destination, message.Body);
        }

        //IEmailService Member
        public void SetSender(string sender)
        {
            sendGridEmailService.Sender = sender;
        }

        public void SetSenderName(string senderName)
        {
            sendGridEmailService.SenderName = senderName;
        }
        public async Task SendAsync(string subject, string destination, string body, string sender = "", string senderName = "")
        {
            await sendGridEmailService.SendAsync(subject, destination, body, sender, senderName);
        }
        //public void Send(string subject, string destination, string body, string sender = "", string senderName = "")
        //{
        //    SendAsync(subject, destination, body, sender, senderName).Wait();
        //}
    }
}
