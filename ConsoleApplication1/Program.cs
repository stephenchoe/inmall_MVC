using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net.Mail;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloEmail().Wait();
        }

        private static async Task HelloEmail()
        {
            String apiKey = "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";
            dynamic sg = new SendGrid.SendGridAPIClient(apiKey, "https://api.sendgrid.com");

            Email from = new Email("traders.com.tw@gmail.com");
            String subject = "測試信件";
            //Email to = new Email("lalala3366@gmail.com");
            Email to = new Email("traders.com.tw@gmail.com");
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);
            //Email email = new Email("test2@example.com");
            //mail.Personalization[0].AddTo(email);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());

            Console.WriteLine(mail.Get());
            Console.ReadLine();

        }

    }
}
