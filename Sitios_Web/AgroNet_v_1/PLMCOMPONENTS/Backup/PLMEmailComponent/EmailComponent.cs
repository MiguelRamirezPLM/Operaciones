using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;



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
