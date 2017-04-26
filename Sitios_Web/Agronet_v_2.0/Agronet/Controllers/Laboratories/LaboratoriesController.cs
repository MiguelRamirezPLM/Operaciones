using Agronet.Models.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agronet.Models;

namespace Agronet.Controllers.Laboratories
{
    public class LaboratoriesController : Controller
    {
        DEAQ db = new DEAQ();

        public ActionResult Index(string CountryId, string DivisionId)
        {
            sessionCountryId ind = (sessionCountryId)Session["sessionCountryId"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Division = int.Parse(DivisionId);

                sessionCountryId sessionCountryId = new sessionCountryId(Country, Division);
                Session["sessionCountryId"] = sessionCountryId;

                List<DivisionInformation> LD = db.Database.SqlQuery<DivisionInformation>("plm_spGetDivisionInformationByDivision @DivisionId=" + Division + "").ToList();

                return View(LD);
            }

            if (ind != null)
            {
                int Country = ind.CountryId;
                int Division = ind.DivisionId;

                sessionCountryId sessionCountryId = new sessionCountryId(Country, Division);
                Session["sessionCountryId"] = sessionCountryId;

                List<DivisionInformation> LD = db.Database.SqlQuery<DivisionInformation>("plm_spGetDivisionInformationByDivision @DivisionId=" + Division + "").ToList();

                return View(LD);
            }
            else
            {
                List<DivisionInformation> LD = db.Database.SqlQuery<DivisionInformation>("plm_spGetDivisionInformationByDivision @DivisionId=" + 0 + "").ToList();

                return View(LD);
            }
        }

        public JsonResult EditDivisions(string Division, string DivisionName, string ShortName)
        {

            int DivisionId = int.Parse(Division);

            var d = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            if (d.LongCount() > 0)
            {
                foreach (Divisions _d in d)
                {
                    _d.DivisionName = DivisionName.Trim();
                    _d.ShortName = ShortName.Trim();

                    db.SaveChanges();
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditAddresses(string Address,
                                        string Suburb,
                                        string Location,
                                        string ZipCode,
                                        string Telephone,
                                        string Lada,
                                        string Fax,
                                        string Web,
                                        string City,
                                        string State,
                                        string Email,
                                        string DivisionInformation)
        {

            int DivisionInformationId = int.Parse(DivisionInformation);

            var DI = db.DivisionInformation.Where(x => x.DivisionInformationId == DivisionInformationId).ToList();

            if (DI.LongCount() > 0)
            {
                foreach (DivisionInformation _di in DI)
                {
                    if (!string.IsNullOrEmpty(Address))
                    {
                        _di.Address = Address.Trim();
                    }
                    else
                    {
                        _di.Address = null;
                    }

                    if (!string.IsNullOrEmpty(Suburb))
                    {
                        _di.Suburb = Suburb.Trim();
                    }
                    else
                    {
                        _di.Suburb = null;
                    }

                    if (!string.IsNullOrEmpty(Location))
                    {
                        _di.Location = Location.Trim();
                    }
                    else
                    {
                        _di.Location = null;
                    }

                    if (!string.IsNullOrEmpty(ZipCode))
                    {
                        _di.ZipCode = ZipCode.Trim();
                    }
                    else
                    {
                        _di.ZipCode = null;
                    }

                    if (!string.IsNullOrEmpty(Telephone))
                    {
                        _di.Telephone = Telephone.Trim();
                    }
                    else
                    {
                        _di.Telephone = null;
                    }

                    if (!string.IsNullOrEmpty(Lada))
                    {
                        _di.Lada = Lada.Trim();
                    }
                    else
                    {
                        _di.Email = null;
                    }

                    if (!string.IsNullOrEmpty(Fax))
                    {
                        _di.Fax = Fax.Trim();
                    }
                    else
                    {
                        _di.Fax = null;
                    }

                    if (!string.IsNullOrEmpty(Web))
                    {
                        _di.Web = Web.Trim();
                    }
                    else
                    {
                        _di.Web = null;
                    }

                    if (!string.IsNullOrEmpty(City))
                    {
                        _di.City = City.Trim();
                    }
                    else
                    {
                        _di.City = null;
                    }

                    if (!string.IsNullOrEmpty(State))
                    {
                        _di.State = State.Trim();
                    }
                    else
                    {
                        _di.State = null;
                    }

                    if (!string.IsNullOrEmpty(Email))
                    {
                        _di.Email = Email.Trim();
                    }
                    else
                    {
                        _di.Email = null;
                    }
                }

                db.SaveChanges();

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddAddress(string Address,
                                        string Suburb,
                                        string Location,
                                        string ZipCode,
                                        string Telephone,
                                        string Lada,
                                        string Fax,
                                        string Web,
                                        string City,
                                        string State,
                                        string Email,
                                        string Division)
        {

            DivisionInformation DI = new DivisionInformation();

            int DivisionId = int.Parse(Division);

            DI.DivisionId = DivisionId;

            if (!string.IsNullOrEmpty(Address))
            {
                DI.Address = Address.Trim();
            }
            else
            {
                DI.Address = null;
            }

            if (!string.IsNullOrEmpty(Suburb))
            {
                DI.Suburb = Suburb.Trim();
            }
            else
            {
                DI.Suburb = null;
            }

            if (!string.IsNullOrEmpty(Location))
            {
                DI.Location = Location.Trim();
            }
            else
            {
                DI.Location = null;
            }

            if (!string.IsNullOrEmpty(ZipCode))
            {
                DI.ZipCode = ZipCode.Trim();
            }
            else
            {
                DI.ZipCode = null;
            }

            if (!string.IsNullOrEmpty(Telephone))
            {
                DI.Telephone = Telephone.Trim();
            }
            else
            {
                DI.Telephone = null;
            }

            if (!string.IsNullOrEmpty(Lada))
            {
                DI.Lada = Lada.Trim();
            }
            else
            {
                DI.Email = null;
            }

            if (!string.IsNullOrEmpty(Fax))
            {
                DI.Fax = Fax.Trim();
            }
            else
            {
                DI.Fax = null;
            }

            if (!string.IsNullOrEmpty(Web))
            {
                DI.Web = Web.Trim();
            }
            else
            {
                DI.Web = null;
            }

            if (!string.IsNullOrEmpty(City))
            {
                DI.City = City.Trim();
            }
            else
            {
                DI.City = null;
            }

            if (!string.IsNullOrEmpty(State))
            {
                DI.State = State.Trim();
            }
            else
            {
                DI.State = null;
            }

            if (!string.IsNullOrEmpty(Email))
            {
                DI.Email = Email.Trim();
            }
            else
            {
                DI.Email = null;
            }

            db.DivisionInformation.Add(DI);
            db.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAddresses(string Address, string Division)
        {
            int AddressId = int.Parse(Address);
            int DivisionId = int.Parse(Division);

            List<DivisionInformation> LS = db.DivisionInformation.Where(x => x.DivisionInformationId == AddressId && x.DivisionId == DivisionId).ToList();

            if (LS.LongCount() > 0)
            {
                var Delete = db.DivisionInformation.SingleOrDefault(x => x.DivisionInformationId == AddressId && x.DivisionId == DivisionId);
                db.DivisionInformation.Remove(Delete);
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DivisionImages(int? DivisionId)
        {
            return View();
        }

        public JsonResult SaveDivisionImages(HttpPostedFileBase file, string Size, string Division, string Country)
        {
            int SizeId = int.Parse(Size);
            int DivisionId = int.Parse(Division);
            int CountryId = int.Parse(Country);

            List<Divisions> LD = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            string FileName = file.FileName;
            string Extention = System.IO.Path.GetExtension(FileName);

            string ImageName = LD[0].DivisionName.Trim().ToLower();

            ImageName = ReplaceProductShot(ImageName.Trim());

            ImageName = ImageName.Trim() + Extention.Trim();

            List<DivisionImages> LDI = db.DivisionImages.Where(x => x.DivisionId == DivisionId).ToList();

            int DivisionImageId = 0;

            if (LDI.LongCount() > 0)
            {
                foreach(DivisionImages item in LDI)
                {
                    item.ImageName = ImageName.Trim();

                    db.SaveChanges();

                    DivisionImageId = item.DivisionImageId;
                }
            }



            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public static string ReplaceProductShot(string img)
        {
            img = img.Replace(" ", "_");
            img = img.Replace(",", "_");
            img = img.Replace(", ", "_");
            img = img.Replace(",_", "_");
            img = img.Replace(".", "_");
            img = img.Replace("__", "_");
            img = img.Replace(". ", "_");
            img = img.Replace("/", "_");
            img = img.Replace("Á", "A");
            img = img.Replace("á", "a");
            img = img.Replace("É", "E");
            img = img.Replace("é", "e");
            img = img.Replace("Í", "I");
            img = img.Replace("í", "i");
            img = img.Replace("Ó", "O");
            img = img.Replace("ó", "o");
            img = img.Replace("ú", "u");
            img = img.Replace("ü", "u");
            img = img.Replace("Ü", "U");
            img = img.Replace("Ú", "U");
            img = img.Replace("- ", "-");
            img = img.Replace(" - ", "-");


            return img;
        }
    }
}