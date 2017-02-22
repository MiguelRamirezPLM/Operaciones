using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;


namespace PLMEmailComponent
{
    public class EmailComponent
    {
        #region Constructors

        public EmailComponent() { }

        #endregion

        #region Public Methods

        public void sendHTMLMail(string mailTo, string name, string subject, string body, bool isHtml, params string[] mailCC)
        {
            MailAddress to = new MailAddress(mailTo, name);

            MailMessage message = new MailMessage();
            message.To.Add(to);
            for (int x = 0; x < mailCC.Length; x++)
            {
                message.CC.Add(mailCC[x]);
            }
            message.Subject = subject;
            message.IsBodyHtml = isHtml;
            message.Body = body;
           
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
            message.Dispose();

        }


        public bool sendHTMLMail(string mailTo, string name, string subject, string body, bool isHtml,string sender,string password, params string[] mailCC)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(mailTo);
            msg.From = new MailAddress(sender,name, System.Text.Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(sender, password,"plmlatina.com");
            client.Port = 25;
            client.Host = "plmlatina.com";
            client.EnableSsl = false; 
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                new Exception(ex.ToString());
                return false;
            }
 
        }

        public void sendMailAttach(string mailTo, string name, string subject, string body, bool isHtml, string pathFile)
        {
            MailAddress to = new MailAddress(mailTo, name);

            MailMessage message = new MailMessage();
            message.To.Add(to);
            message.Subject = subject;
            message.IsBodyHtml = isHtml;
            message.Body = body;

            Attachment attach = new Attachment(pathFile);
            message.Attachments.Add(attach);
            
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);
            message.Dispose();

        }

        #endregion





    }
}
