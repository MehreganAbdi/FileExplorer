using FileExplorer.DTOs;
using FileExplorer.IService;
using System.Net.Mail;
using System.Net;

namespace FileExplorer.Services
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendFileByEmail(EmailDTO email, string path)
        {
            var senderEmail = new MailAddress("mehreganabdiwebmail@gmail.com");
            var receiverEmail = new MailAddress(email.Reciever, "Receiver");
            var password = "kxbo ipin pkyn vgfo";
            var sub = email.Subject;
            var body = email.message ;
            Attachment attachment;

            attachment = new Attachment(path);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            var message = new MailMessage()
            {
                Body = body,
                Subject = sub
            };
                message.Attachments.Add(attachment);
            {
                try
                {

                    smtp.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }


        }

    }
}
