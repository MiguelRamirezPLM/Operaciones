using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers.Manual
{
    public class ManualController : Controller
    {
        public GetText _Gettext = new Models.GetText();
        DEACI_20150917Entities db = new DEACI_20150917Entities();
        PLMUsers plm = new PLMUsers();
        public ActionResult Completo()
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var _UserNickName = (from _us in plm.Users
                                     where _us.UserId == p.userId
                                     select _us).ToList();
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            return View();
        }
        public ActionResult ManualPDF()
        {
            return View();
        }
        public ActionResult Manual_DEACI()
        {
            return new Rotativa.ActionAsPdf("ManualPDF");
        }
        public FileResult PDF()
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Manual/Manual_DEACI (Ventas).pdf"));
            return File(filepath, "Manual_DEACI (Ventas).pdf");
        }
        public ActionResult Correo(string De, string Mensaje)
        {
            var cuser = De + "@plmlatina.com";
            var _plmusers = (from _u in plm.Users
                             where _u.Email == cuser
                             select _u).ToList();
            if (_plmusers.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            _Gettext.From = De;
            _Gettext.Message = Mensaje;
            WriteText();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void WriteText()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(WriteTextMessage));
            t.Start(1);
        }
        public void WriteTextMessage(object r)
        {
            string De = _Gettext.From + "@plmlatina.com";
            string Mensaje = _Gettext.Message;
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            mmsg.To.Add("carlos.carrillo@plmlatina.com");
            mmsg.Subject = "Contacto" ;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            //mmsg.CC.Add("beatriz.vazquez@plmlatina.com");
            var _MessageBody = Mensaje;
            mmsg.Body = _MessageBody;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress(De);
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential(De, "Sitio DEACI");
            cliente.Host = "plmlatina.com";
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