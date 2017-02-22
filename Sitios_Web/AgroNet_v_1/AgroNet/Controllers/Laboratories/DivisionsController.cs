using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;

namespace AgroNet.Controllers.Laboratories
{
    public class DivisionsController : Controller
    {
        //
        // GET: /Laboratories/
        DEAQ db = new DEAQ();
        CreateDiv CreateDiv = new CreateDiv();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string Id)
        {
            CountriesSs D = (CountriesSs)Session["CountriesSs"];

            if (Id != null)
            {
                int LabId = int.Parse(Id);
                var Labs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.LaboratoryId == LabId                            
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                if (Labs.LongCount() > 0)
                {
                    ViewData["CountProds"] = 0;
                }
                else
                {
                    ViewData["CountProds"] = null;
                }
                CountriesSs CountriesSs = new CountriesSs(LabId);
                Session["CountriesSs"] = CountriesSs;
                return View(Labs);
            }
            else if (D != null)
            {
                int LabId = D.LabId;
                var Labs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.LaboratoryId == LabId
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                if (Labs.LongCount() > 0)
                {
                    ViewData["CountProds"] = 0;
                }
                else
                {
                    ViewData["CountProds"] = null;
                }
                return View(Labs);
            }
            else
            {
                var Labs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.LaboratoryId == 0
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                return View(Labs);
            }
        }

        public ActionResult editdivs(string DivisionId, string DivisionName, string ShortName, string LabId,string CountryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int Division = int.Parse(DivisionId);
            string DivisionN = DivisionName.Trim();
            string ShortN = ShortName.Trim();
            int LaboratoryId = int.Parse(LabId);
            int Country = int.Parse(CountryId);

            var Div = from Divs in db.Divisions
                      where Divs.DivisionId == Division
                      select Divs;
            foreach (Divisions D in Div)
            {
                D.DivisionName = DivisionName.Trim();
                D.ShortName = ShortName.Trim();
            }
            db.SaveChanges();
            ActivityLog.editdivision(DivisionN, ShortN, Country, LaboratoryId, ApplicationId, UsersId, Division);

            return View();
        }

        public ActionResult searchdivisions(string DivisionName)
        {
            int count = 0;
            CountriesSs c = (CountriesSs)Session["CountriesSs"];
            if (DivisionName != null)
            {
                if (DivisionName == string.Empty)
                {
                    return RedirectToAction("Index", "Divisions");
                }
                else
                {
                    int LabId = c.LabId;
                    var Divs = (from Lab in db.Divisions
                                join Count in db.Countries
                                on Lab.CountryId equals Count.CountryId
                                where Lab.LaboratoryId == LabId
                                select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                    if (!string.IsNullOrEmpty(DivisionName))
                    {
                        Divs = Divs.Where(x => x.Divisions.DivisionName.StartsWith(DivisionName) || x.Divisions.ShortName.StartsWith(DivisionName)).OrderBy(q => q.Divisions.DivisionName);
                        foreach (joinDivisionsCountries D in Divs)
                        {
                            count = count + 1;
                        }
                        ViewData["Count"] = count;
                    }
                    SearchDiv SearchDiv = new SearchDiv(DivisionName);
                    Session["SearchDiv"] = SearchDiv;
                    return View("Index", Divs);
                }
            }
            else
            {
                SearchDiv s = (SearchDiv)Session["SearchDiv"];
                int LabId = c.LabId;
                var Divs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.LaboratoryId == LabId
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                if (!string.IsNullOrEmpty(s.DivisionName))
                {
                    Divs = Divs.Where(x => x.Divisions.DivisionName.StartsWith(DivisionName) || x.Divisions.ShortName.StartsWith(DivisionName)).OrderBy(q => q.Divisions.DivisionName);
                    foreach (joinDivisionsCountries D in Divs)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                }
                return View("Index", Divs);
            }
        }

        public ActionResult creatediv(string DivisionN, string ShortName, string Country)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            CountriesSs D = (CountriesSs)Session["CountriesSs"];
            int LabId = D.LabId;

            bool Div = CreateDiv.CreateDivs(DivisionN, ShortName, Country, LabId, ApplicationId, UsersId);

            return Json(Div, JsonRequestBehavior.AllowGet);
        }

    }
}
