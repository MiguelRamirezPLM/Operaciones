using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;

namespace AgroNet.Controllers.Laboratories
{
    public class LaboratoriesController : Controller
    {
        //
        // GET: /Laboratories/
        DEAQ db = new DEAQ();
        CreateLab CreateLab = new CreateLab();
        ActivityLog ActivityLog = new ActivityLog();
        
        public ActionResult Index(string CountryId)
        {
            var Labs = from Lab in db.Laboratories
                       orderby Lab.LaboratoryName ascending
                       select Lab;
            return View(Labs);
        }

        public ActionResult searchlabs(string LaboratoryName)
        {
            int count = 0;
            if (LaboratoryName != null)
            {
                if (LaboratoryName == string.Empty)
                {
                    return RedirectToAction("Index", "Laboratories");
                }
                else
                {
                    var Labs = (from Lab in db.Laboratories
                                select Lab).Distinct();

                    if (!string.IsNullOrEmpty(LaboratoryName))
                    {
                        Labs = Labs.Where(x => x.LaboratoryName.StartsWith(LaboratoryName));
                        foreach (AgroNet.Models.Laboratories L in Labs)
                        {
                            count = count + 1;
                        }
                        ViewData["Count"] = count;
                    }
                    SearchLab SearchLab = new SearchLab(LaboratoryName);
                    Session["SearchLab"] = SearchLab;
                    return View("Index", Labs);
                }
            }
            else
            {
                SearchLab s = (SearchLab)Session["SearchLab"];
                var Labs = (from Lab in db.Laboratories
                            select Lab).Distinct();
                if (!string.IsNullOrEmpty(s.LaboratoryName))
                {
                    Labs = Labs.Where(x => x.LaboratoryName.StartsWith(s.LaboratoryName));
                    foreach (AgroNet.Models.Laboratories L in Labs)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                }
                return View("Index", Labs);
            }
        }

        public ActionResult createlab(string LabName)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            bool Lab = CreateLab.CreateLabs(LabName, ApplicationId, UsersId);

            return Json(Lab, JsonRequestBehavior.AllowGet);
        }

        public ActionResult editlab(string LaboratoryN, string LabId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int LaboratoryId = int.Parse(LabId);
            var EditLab = from Lab in db.Laboratories
                          where Lab.LaboratoryId == LaboratoryId
                          select Lab;
            foreach(AgroNet.Models.Laboratories l in EditLab)
            {
                l.LaboratoryName = LaboratoryN;
            }
            db.SaveChanges();
            ActivityLog.updatelab(LaboratoryN, ApplicationId, UsersId, LaboratoryId);
            return View();
        }

    }
}
