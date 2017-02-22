using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;

namespace AgroNet.Controllers.Laboratories
{
    public class DivsController : Controller
    {
        //
        // GET: /Divs/
        DEAQ db = new DEAQ();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId)
        {
            DivisionsbyCountry DivC = (DivisionsbyCountry)Session["DivisionsbyCountry"];
            if (CountryId != null)
            {
                int count = 0;
                int Country = int.Parse(CountryId);
                var Divs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.CountryId == Country
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                foreach (joinDivisionsCountries jdc in Divs)
                {
                    count = count + 1;
                }
                if (count == 0)
                {
                    ViewData["CountProds"] = null;
                }
                else
                {
                    ViewData["CountProds"] = count;
                }
                DivisionsbyCountry DivisionsbyCountry = new DivisionsbyCountry(Country);
                Session["DivisionsbyCountry"] = DivisionsbyCountry;
                return View(Divs);
            }
            else if (DivC != null)
            {
                int count = 0;
                int Country = DivC.Country;
                var Divs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.CountryId == Country
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                foreach (joinDivisionsCountries jdc in Divs)
                {
                    count = count + 1;
                }
                if (count == 0)
                {
                    ViewData["CountProds"] = null;
                }
                else
                {
                    ViewData["CountProds"] = count;
                }
                return View(Divs);
            }
            else
            {
                var Divs = (from Lab in db.Divisions
                            join Count in db.Countries
                            on Lab.CountryId equals Count.CountryId
                            where Lab.CountryId == 0
                            select new joinDivisionsCountries { Countries = Count, Divisions = Lab }).OrderBy(x => x.Divisions.DivisionName);
                ViewData["CountProds"] = 0;
                return View(Divs);
            }
        }

        [HttpPost]
        public ActionResult searchdivs(string DivisionName)
        {
            DivisionsbyCountry DivC = (DivisionsbyCountry)Session["DivisionsbyCountry"];
            if (DivC != null)
            {
                if (DivisionName != null)
                {
                    if (DivisionName == string.Empty)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        int count = 0;
                        int Country = DivC.Country;
                        var Divs = (from Lab in db.Divisions
                                    join Count in db.Countries
                                    on Lab.CountryId equals Count.CountryId
                                    where Lab.CountryId == Country
                                    select new joinDivisionsCountries { Countries = Count, Divisions = Lab });
                        if (!string.IsNullOrEmpty(DivisionName))
                        {
                            Divs = Divs.Where(s => s.Divisions.DivisionName.StartsWith(DivisionName));
                            foreach (joinDivisionsCountries J in Divs)
                            {
                                count = count + 1;
                            }
                            ViewData["Count"] = count;
                        }
                        return View("Index", Divs);
                    }
                }

            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
            return View();
        }

        public ActionResult editdivs(string DivisionId, string DivisionName, string ShortName, string LabId, string CountryId)
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
    }
}
