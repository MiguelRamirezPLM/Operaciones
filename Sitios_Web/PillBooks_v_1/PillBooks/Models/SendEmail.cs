using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text;

namespace PillBooks.Models
{
    public class SendEmail
    {
        public void EmailClosePB(string PillBook, int UsersId)
        {
            PLMUsers plm = new PLMUsers();

            var usr = plm.Users.Where(x => x.UserId == UsersId).ToList();

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }


            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();


            /*-------------------------CORREO RECEPTOR-------------------------*/

            mmsg.To.Add("beatriz.vazquez@plmlatina.com");
            mmsg.To.Add("erick.morales@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            /*-------------------------ASUNTO-------------------------*/


            mmsg.Subject = "Cierre de PillBook";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;


            /*-------------------------CORREO A ENVÍAR COPIA-------------------------*/

           //mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            mmsg.CC.Add("miguel.ramirez@plmlatina.com");




            /*-------------------------CUERPO DEL CORREO-------------------------*/

            StringBuilder sb = new StringBuilder();

            sb.Append("Hola !!!");
            sb.Append("<br>");
            sb.Append("<br>");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("&bull; El Usuario: <b>" + usr[0].Name.ToUpper().Trim() + " " + usr[0].LastName.ToUpper().Trim() + " (" + usr[0].NickName + ")" + "</b> " + " ha cerrado el PillBook: <label style='color: #065977'><b><i> " + PillBook + "</i></b></label>");

            String Message = sb.ToString();

            mmsg.Body = Message;

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Enviar Correo como HTML


            /*-------------------------CORREO EMISOR-------------------------*/


            ////mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("PillBook_System@plmlatina.com");


            /*-------------------------OBJETO TIPO CLIENTE DE CORREO-------------------------*/

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


            /*-------------------------CREDENCIALES DEL CORREO EMISOR-------------------------*/

            cliente.Credentials =
                new System.Net.NetworkCredential("PillBook_System@plmlatina.com", "PillBook_System");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }

        public void EmailOpenPB(string PillBook, int UsersId)
        {
            PLMUsers plm = new PLMUsers();

            var usr = plm.Users.Where(x => x.UserId == UsersId).ToList();

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();


            /*-------------------------CORREO RECEPTOR-------------------------*/

            mmsg.To.Add("beatriz.vazquez@plmlatina.com");
            mmsg.To.Add("erick.morales@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            /*-------------------------ASUNTO-------------------------*/


            mmsg.Subject = "Apertura de PillBook";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;


            /*-------------------------CORREO A ENVÍAR COPIA-------------------------*/

            //mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            mmsg.CC.Add("miguel.ramirez@plmlatina.com");


            /*-------------------------CUERPO DEL CORREO-------------------------*/

            StringBuilder sb = new StringBuilder();

            sb.Append("Hola !!!");
            sb.Append("<br>");
            sb.Append("<br>");
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            sb.Append("&bull; El Usuario: <b>" + usr[0].Name.ToUpper().Trim() + " " + usr[0].LastName.ToUpper().Trim() + " (" + usr[0].NickName + ")" + "</b> " + " ha abierto el PillBook: <label style='color: #065977'><b><i> " + PillBook + "</i></b></label>");

            String Message = sb.ToString();

            mmsg.Body = Message;

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Enviar Correo como HTML


            /*-------------------------CORREO EMISOR-------------------------*/


            ////mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("PillBook_System@plmlatina.com");


            /*-------------------------OBJETO TIPO CLIENTE DE CORREO-------------------------*/

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


            /*-------------------------CREDENCIALES DEL CORREO EMISOR-------------------------*/

            cliente.Credentials =
                new System.Net.NetworkCredential("PillBook_System@plmlatina.com", "PillBook_System");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }

    }
}