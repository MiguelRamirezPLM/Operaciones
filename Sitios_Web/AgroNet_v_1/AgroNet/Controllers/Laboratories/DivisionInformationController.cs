using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;

namespace AgroNet.Controllers.Laboratories
{
    public class DivisionInformationController : Controller
    {
        //
        // GET: /DivisionInformation/
        DEAQ db = new DEAQ();
        CreateDivisionInformation CDI = new CreateDivisionInformation();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string DivIdd, bool flag)
        {
            DivisionsInf dinf = (DivisionsInf)Session["DivisionsInf"];
            if (DivIdd != null)
            {
                int Id = int.Parse(DivIdd);
                var DivIn = from Dinf in db.DivisionInformation
                            where Dinf.DivisionId == Id
                            select Dinf;

                DivisionsInf DivisionsInf = new DivisionsInf(Id,flag);
                Session["DivisionsInf"] = DivisionsInf;
                return View(DivIn);
            }
            else
            {
                int DivId = dinf.Id;
                var DivIn = from Dinf in db.DivisionInformation
                            where Dinf.DivisionId == DivId
                            select Dinf;
                return View(DivIn);
            }

        }

        public ActionResult editinfo(string Address, string Suburb, string Location, string ZipCode, string Telephone, string Lada, string Fax,
            string Web, string City, string State, string Email, string DivisionId, string DivisionInformation)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int Division = int.Parse(DivisionId);
            int DivisionInformationId = int.Parse(DivisionInformation);
            var DI = from DivInf in db.DivisionInformation
                     where DivInf.DivisionId == Division
                     && DivInf.DivisionInformationId == DivisionInformationId
                     select DivInf;
            foreach (DivisionInformation DInf in DI)
            {
                if (Address != string.Empty)
                {
                    DInf.Address = Address.Trim();
                }
                else
                {
                    DInf.Address = null;
                }
                if (Suburb != string.Empty)
                {
                    DInf.Suburb = Suburb.Trim();
                }
                else
                {
                    DInf.Suburb = null;
                }
                if (Location != string.Empty)
                {
                    DInf.Location = Location.Trim();
                }
                else
                {
                    DInf.Location = null;
                }
                if (ZipCode != string.Empty)
                {
                    DInf.ZipCode = ZipCode.Trim();
                }
                else
                {
                    DInf.ZipCode = null;
                }
                if (Telephone != string.Empty)
                {
                    DInf.Telephone = Telephone.Trim();
                }
                else
                {
                    DInf.Telephone = null;
                }
                if (Lada != string.Empty)
                {
                    DInf.Lada = Lada.Trim();
                }
                else
                {
                    DInf.Lada = null;
                }
                if (Fax != string.Empty)
                {
                    DInf.Fax = Fax.Trim();
                }
                else
                {
                    DInf.Fax = null;
                }
                if (Web != string.Empty)
                {
                    DInf.Web = Web.Trim();
                }
                else
                {
                    DInf.Web = null;
                }
                if (City != string.Empty)
                {
                    DInf.City = City.Trim();
                }
                else
                {
                    DInf.City = null;
                }
                if (State != string.Empty)
                {
                    DInf.State = State.Trim();
                }
                else
                {
                    DInf.State = null;
                }
                if (Email != string.Empty)
                {
                    DInf.Email = Email.Trim();
                }
                else
                {
                    DInf.Email = null;
                }
            }
            db.SaveChanges();
            //ActivityLog.editivisioninformation(Address.Trim(), Suburb.Trim(), Location.Trim(), ZipCode.Trim(), Telephone.Trim(),
            //    Lada.Trim(), Fax.Trim(), Web.Trim(), City.Trim(), State.Trim(), Email.Trim(), Division, DivisionInformationId, ApplicationId, UsersId);

            return RedirectToAction("Index", "DivisionInformation");
        }

        public ActionResult insertinfordiv(string Address, string Suburb, string Location, string ZipCode, string Telephone, string Lada, string Fax,
            string Web, string City, string State, string Email, string DivisionId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            bool div = CDI.DivisionInformationC(Address, Suburb, Location, ZipCode, Telephone, Lada, Fax,
            Web, City, State, Email, DivisionId,ApplicationId,UsersId);

            return Json(div, JsonRequestBehavior.AllowGet);
        }
    }
}
