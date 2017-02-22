using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PillBooks.Models;

namespace PillBooks.Controllers
{
    public class SubstancesController : Controller
    {
        MedinetPB db = new MedinetPB();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getinnsubstances(String Description)
        {
            var inn = db.Database.SqlQuery<INNActiveSubstances>("plm_spGetINNSubstancesBySearch @param='" + Description + "'").ToList();

            return Json(inn, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getplmsubstances(String Description)
        {
            var plm = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPLMSubstancesBySearch @param='" + Description + "'").ToList();

            return Json(plm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getplminnsubstances(string INNActiveSubstance)
        {
            ActiveSubstances ActiveSubstances = new Models.ActiveSubstances();
            List<ActiveSubstances> LS = new List<Models.ActiveSubstances>();

            int INNActiveSubstanceId = int.Parse(INNActiveSubstance);

            var plm = (from plms in db.PLMINNSubstances
                       join ac in db.ActiveSubstances
                           on plms.ActiveSubstanceId equals ac.ActiveSubstanceId
                       where plms.INNActiveSubstanceId == INNActiveSubstanceId
                       && ac.Active == true
                       select ac).OrderBy(x => x.Description).ToList();

            foreach (ActiveSubstances _plm in plm)
            {
                ActiveSubstances = new ActiveSubstances();

                ActiveSubstances.Active = _plm.Active;
                ActiveSubstances.ActiveSubstanceId = _plm.ActiveSubstanceId;
                ActiveSubstances.Description = _plm.Description;
                ActiveSubstances.EnglishDescription = _plm.EnglishDescription;
                ActiveSubstances.Enunciative = _plm.Enunciative;
                ActiveSubstances.SubstanceKey = _plm.SubstanceKey;

                LS.Add(ActiveSubstances);
            }

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addplmtoinnsubstances(string ActiveSubstances, string INNActiveSubstance)
        {
            PLMINNSubstances PLMINNSubstances = new Models.PLMINNSubstances();
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];

            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;
            int ActiveSubstancesId = int.Parse(ActiveSubstances);
            int INNActiveSubstanceId = int.Parse(INNActiveSubstance);

            var plm = db.PLMINNSubstances.Where(x => x.INNActiveSubstanceId == INNActiveSubstanceId && x.ActiveSubstanceId == ActiveSubstancesId).ToList();

            if (plm.LongCount() == 0)
            {

                PLMINNSubstances = new PLMINNSubstances();

                PLMINNSubstances.ActiveSubstanceId = ActiveSubstancesId;
                PLMINNSubstances.INNActiveSubstanceId = INNActiveSubstanceId;

                db.PLMINNSubstances.Add(PLMINNSubstances);
                db.SaveChanges();

                ActivityLog.insertplminnsubstances(ActiveSubstancesId, INNActiveSubstanceId, 1, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteplminnsubstances(string ActiveSubstances, string INNActiveSubstance)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];

            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;
            int ActiveSubstancesId = int.Parse(ActiveSubstances);
            int INNActiveSubstanceId = int.Parse(INNActiveSubstance);

            var plm = db.PLMINNSubstances.SingleOrDefault(x => x.ActiveSubstanceId == ActiveSubstancesId && x.INNActiveSubstanceId == INNActiveSubstanceId);

            if (plm != null)
            {
                db.PLMINNSubstances.Remove(plm);
                db.SaveChanges();

                ActivityLog.insertplminnsubstances(ActiveSubstancesId, INNActiveSubstanceId, 4, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}