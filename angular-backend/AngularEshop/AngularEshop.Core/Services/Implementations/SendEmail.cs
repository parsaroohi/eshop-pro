using System.Net.Mail;
using AngularEshop.Core.Services.Interfaces;

namespace AngularEshop.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "test@test.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("barnamenevis.ir");

            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 25;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "password");

            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }
    }
}

