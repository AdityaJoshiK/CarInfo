
using System.Net;
using System.Net.Mail;

namespace CarInfo.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "support@carinfo.com";
            var pw = "01F7D82D3FC932DC3A33EE898CBD70844AF2";

            var client = new SmtpClient("smtp.elasticemail.com", 2525)
            { 
                EnableSsl = true,
                Credentials = new NetworkCredential(mail,pw)
            };

            return client.SendMailAsync(
                new MailMessage(from:"carinfo.connect@gmail.com",
                    to:email,
                    subject,
                    body:message
                )
                {
                    IsBodyHtml=true
                }
                );
        }
    }
}
