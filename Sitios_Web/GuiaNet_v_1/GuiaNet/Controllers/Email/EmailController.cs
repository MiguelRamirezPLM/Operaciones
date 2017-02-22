using GuiaNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuiaNet.Controllers.Email
{
    public class EmailController : Controller
    {
        PLMUsers db = new PLMUsers();
        //
        // GET: /Email/
        public JsonResult Index(string Message)
        {
            CountriesUsers pp = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = pp.ApplicationId;
            int UsersId = pp.userId;

            var usr = db.Users.Where(x => x.UserId == UsersId).ToList();

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add("miguel.ramirez@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Requerimientos GuiaNet";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            //Cuerpo del Mensaje
            mmsg.Body = Message;

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje

            //mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("miguel.ramirez@plmlatina.com");


            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("miguel.ramirez@plmlatina.com", "micontraseña");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            
        }
	}
}