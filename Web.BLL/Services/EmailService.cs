using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Web.Models.Enums;

namespace Web.BLL.Services
{
    public class EmailService : IMessageService
    {

        public MessageStates MessageStates { get; private set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string SenderMail { get; set; }
        public string FilePath { get; set; }
        public string Password { get; set; }
        public string Smtp { get; set; }
        public int SmtpPort { get; set; }

        public EmailService()
        {
            this.SenderMail="ab.service3922@gmail.com";
            this.Password="abservice3922.";
            this.Smtp = "smtp.gmail.com";
            this.SmtpPort = 587;
        }

        public EmailService(string senderMail, string Password,string Smtp,int SmtpPort)
        {
            this.SenderMail = senderMail;
            this.Password = Password;
            this.Smtp = Smtp;
            this.SmtpPort = SmtpPort;
        }


        public void Send(IdentityMessage message, params string[] contacts)
        {
            Task.Run(async () =>
            {
                await this.SendAsync(message, contacts);
            });
        }

        public async Task SendAsync(IdentityMessage message, params string[] contacts)
        {
            try
            {
                var mail = new MailMessage()
                {
                    From = new MailAddress(SenderMail)
                };

                if (!string.IsNullOrEmpty(FilePath))
                {
                    mail.Attachments.Add(new Attachment(FilePath));
                }

                foreach (var c in contacts)
                {
                    mail.To.Add(c);
                }

                if( Cc != null && Cc.Length > 0)
                {

                    foreach (var cc in Cc)
                    {
                        mail.CC.Add(new MailAddress(cc));
                    }
                    
                }
                if(Bcc != null && Bcc.Length > 0)
                {
                    foreach (var bcc in Bcc)
                    {
                        mail.Bcc.Add(new MailAddress(bcc));
                    }
                }

                mail.Subject = message.Subject;
                mail.Body = message.Body;

                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.HeadersEncoding = Encoding.UTF8;


                var smtpClient = new SmtpClient(this.Smtp, this.SmtpPort)
                {
                    Credentials = new NetworkCredential(this.SenderMail, this.Password),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(mail);
                MessageStates = MessageStates.Delivered;

            }
            catch (Exception ex)
            {
                MessageStates = MessageStates.NotDelivered;
            }
        }
    }
}
