using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediVet.Models;

namespace MediVet.Controllers.Veterinario
{
    public class VeterinaryController : Controller
    {
        PEV db = new PEV();
        ProductEquipment ProductEquipment = new ProductEquipment();
        ProductSpecies ProductSpecies = new ProductSpecies();
        ProductSubstances ProductSubstances = new ProductSubstances();
        ProductTherapeuticUses ProductTherapeuticUses = new ProductTherapeuticUses();
        ActivityLog ActivityLog = new ActivityLog();

        //
        // GET: /Veterinary/
        public ActionResult Index(string CountryId, string DivisionId, string EditionId, string BookId)
        {
            sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];
            if (CountryId != null)
            {
                int CId = int.Parse(CountryId);
                int ClId = int.Parse(DivisionId);
                int EId = int.Parse(EditionId);
                int BId = int.Parse(BookId);

                sessionveterinary sessionveterinary = new sessionveterinary(CId, ClId, EId, BId);
                Session["sessionveterinary"] = sessionveterinary;

                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return View(Ind);
            }
            if (index != null)
            {
                int CId = index.CId;
                int ClId = index.DId;
                int EId = index.EId;
                int BId = index.BId;

                sessionveterinary sessionveterinary = new sessionveterinary(CId, ClId, EId, BId);
                Session["sessionveterinary"] = sessionveterinary;

                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return View(Ind);
            }
            else
            {
                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == 0 && x.CountryId == 0 && x.DivisionId == 0 && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return View(Ind);
            }
        }

        public ActionResult searchproduct(string ProductName)
        {
            sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

            int CId = index.CId;
            int ClId = index.DId;
            int EId = index.EId;
            int BId = index.BId;

            if (ProductName != null)
            {
                //sessionveterinary sessionveterinary = new sessionveterinary(CId, ClId, EId, BId);
                //Session["sessionveterinary"] = sessionveterinary;

                int leng = ProductName.Length;

                if (leng <= 3)
                {

                    var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P" && x.ProductName.StartsWith(ProductName)).OrderBy(x => x.ProductName).ToList();

                    return View("Index", Ind);
                }
                if (leng > 3)
                {
                    var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P" && x.ProductName.Contains(ProductName)).OrderBy(x => x.ProductName).ToList();

                    return View("Index", Ind);
                }
                else
                {
                    var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                    return View("Index", Ind);
                }
            }
            else
            {
                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == ClId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return RedirectToAction("Index", "Veterinary");
            }
        }


        /*      EQUIPMENT       */
        public ActionResult clasificationProductEquipment(int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            if (ProductId != 0)
            {
                sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

                if (index != null)
                {
                    int CId = index.CId;
                    int DId = index.DId;
                    int EId = index.EId;
                    int BId = index.BId;

                    var c = db.Countries.Where(x => x.CountryId == CId).ToList();

                    foreach (Countries _c in c)
                    {
                        ViewData["CountryNameV"] = _c.CountryName;
                    }

                    var d = db.Divisions.Where(x => x.DivisionId == DId).ToList();

                    foreach (Divisions _d in d)
                    {
                        ViewData["DivisionNameV"] = _d.DivisionName;
                    }

                    var e = db.Editions.Where(x => x.EditionId == EId).ToList();

                    foreach (Editions _e in e)
                    {
                        ViewData["NumberEditionV"] = _e.NumberEdition;
                    }

                    var b = db.Books.Where(x => x.BookId == BId).ToList();

                    foreach (Books _b in b)
                    {
                        ViewData["BookNameV"] = _b.BookName;
                    }

                    var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

                    foreach (Products _p in p)
                    {
                        ViewData["ProductNameV"] = _p.ProductName;
                        ViewData["ProductIdV"] = _p.ProductId;
                    }

                    var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

                    foreach (PharmaceuticalForms _pf in pf)
                    {
                        ViewData["PharmaFormV"] = _pf.PharmaForm;
                        ViewData["PharmaFormIdV"] = _pf.PharmaFormId;
                    }

                    var cat = db.Categories.Where(x => x.CategoryId == CategoryId);

                    foreach (Categories _cat in cat)
                    {
                        ViewData["CategoryNameV"] = _cat.CategoryName;
                        ViewData["CategoryIdV"] = _cat.CategoryId;
                    }

                    var pe = db.ProductEquipment.Where(x => x.ProductId == ProductId).ToList();

                    if (pe.LongCount() > 0)
                    {
                        Equipment E = new Equipment();
                        List<Equipment> LE = new List<Equipment>();

                        foreach (ProductEquipment _pe in pe)
                        {
                            var eq = db.Equipment.Where(x => x.EquipmentId == _pe.EquipmentId);

                            foreach (Equipment _eq in eq)
                            {
                                E = new Equipment();

                                E.Active = _eq.Active;
                                E.EquipmentId = _eq.EquipmentId;
                                E.EquipmentName = _eq.EquipmentName;

                                LE.Add(E);
                            }
                        }

                        LE = LE.OrderBy(x => x.EquipmentName).ToList();

                        return View(LE);
                    }
                    else
                    {
                        var eq = db.Equipment.Where(x => x.EquipmentId == 0).ToList();

                        return View(eq);
                    }

                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult insertProductEquipment(int EquipmentId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var pe = db.ProductEquipment.Where(x => x.EquipmentId == EquipmentId && x.ProductId == ProductId).ToList();

            if (pe.LongCount() == 0)
            {
                ProductEquipment.EquipmentId = Convert.ToByte(EquipmentId);
                ProductEquipment.ProductId = ProductId;

                db.ProductEquipment.Add(ProductEquipment);
                db.SaveChanges();

                int OperationId = 1;
                ActivityLog._productequipment(EquipmentId, ProductId, ApplicationId, UsersId, OperationId);
            }

            return RedirectToAction("clasificationProductEquipment", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult deleteProductEquipment(int EquipmentId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var pe = db.ProductEquipment.SingleOrDefault(x => x.EquipmentId == EquipmentId && x.ProductId == ProductId);

            if (pe != null)
            {
                db.ProductEquipment.Remove(pe);
                db.SaveChanges();

                int OperationId = 4;
                ActivityLog._productequipment(EquipmentId, ProductId, ApplicationId, UsersId, OperationId);
            }

            return RedirectToAction("clasificationProductEquipment", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult searchequipmentasoc(string EquipmentName)
        {
            searchEquipment searchEquipment = new searchEquipment(EquipmentName);
            Session["searchEquipment"] = searchEquipment;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchequipment(string EquipmentName)
        {
            sessionEquipmentAsoc sessionEquipmentAsoc = new sessionEquipmentAsoc(EquipmentName);
            Session["sessionEquipmentAsoc"] = sessionEquipmentAsoc;

            return Json(true, JsonRequestBehavior.AllowGet);
        }



        /*      SPECIES         */
        public ActionResult clasificationProductSpecies(int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            if (ProductId != 0)
            {
                sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

                if (index != null)
                {
                    int CId = index.CId;
                    int DId = index.DId;
                    int EId = index.EId;
                    int BId = index.BId;

                    var c = db.Countries.Where(x => x.CountryId == CId).ToList();

                    foreach (Countries _c in c)
                    {
                        ViewData["CountryNameV"] = _c.CountryName;
                    }

                    var d = db.Divisions.Where(x => x.DivisionId == DId).ToList();

                    foreach (Divisions _d in d)
                    {
                        ViewData["DivisionNameV"] = _d.DivisionName;
                    }

                    var e = db.Editions.Where(x => x.EditionId == EId).ToList();

                    foreach (Editions _e in e)
                    {
                        ViewData["NumberEditionV"] = _e.NumberEdition;
                    }

                    var b = db.Books.Where(x => x.BookId == BId).ToList();

                    foreach (Books _b in b)
                    {
                        ViewData["BookNameV"] = _b.BookName;
                    }

                    var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

                    foreach (Products _p in p)
                    {
                        ViewData["ProductNameV"] = _p.ProductName;
                        ViewData["ProductIdV"] = _p.ProductId;
                    }

                    var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

                    foreach (PharmaceuticalForms _pf in pf)
                    {
                        ViewData["PharmaFormV"] = _pf.PharmaForm;
                        ViewData["PharmaFormIdV"] = _pf.PharmaFormId;
                    }

                    var cat = db.Categories.Where(x => x.CategoryId == CategoryId);

                    foreach (Categories _cat in cat)
                    {
                        ViewData["CategoryNameV"] = _cat.CategoryName;
                        ViewData["CategoryIdV"] = _cat.CategoryId;
                    }

                    var pe = db.ProductSpecies.Where(x => x.ProductId == ProductId).ToList();

                    if (pe.LongCount() > 0)
                    {
                        Species S = new Species();
                        List<Species> LS = new List<Species>();

                        foreach (ProductSpecies _pe in pe)
                        {
                            var eq = db.Species.Where(x => x.SpecieId == _pe.SpecieId);

                            foreach (Species _eq in eq)
                            {
                                S = new Species();

                                S.Active = _eq.Active;
                                S.SpecieId = _eq.SpecieId;
                                S.SpecieName = _eq.SpecieName;

                                LS.Add(S);
                            }
                        }

                        LS = LS.OrderBy(x => x.SpecieName).ToList();

                        return View(LS);
                    }
                    else
                    {
                        var eq = db.Species.Where(x => x.SpecieId == 0).ToList();

                        return View(eq);
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult insertproductspecies(int SpecieId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ps = db.ProductSpecies.Where(x => x.SpecieId == SpecieId && x.ProductId == ProductId).ToList();

            if (ps.LongCount() == 0)
            {
                ProductSpecies.ProductId = ProductId;
                ProductSpecies.SpecieId = SpecieId;

                db.ProductSpecies.Add(ProductSpecies);
                db.SaveChanges();

                int OperationId = 1;
                ActivityLog._productspecies(SpecieId, ProductId, ApplicationId, UsersId, OperationId);
            }

            return RedirectToAction("clasificationProductSpecies", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult deleteproductspecies(int SpecieId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ps = db.ProductSpecies.SingleOrDefault(x => x.SpecieId == SpecieId && x.ProductId == ProductId);

            if (ps != null)
            {
                db.ProductSpecies.Remove(ps);
                db.SaveChanges();

                int OperationId = 4;
                ActivityLog._productspecies(SpecieId, ProductId, ApplicationId, UsersId, OperationId);
            }
            return RedirectToAction("clasificationProductSpecies", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult searchspecies(string SpecieName)
        {
            SessionsearchSpecies SessionsearchSpecies = new SessionsearchSpecies(SpecieName);
            Session["SessionsearchSpecies"] = SessionsearchSpecies;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchspeciesasociate(string SpecieName)
        {
            SessionSpeciesasociate SessionSpeciesasociate = new SessionSpeciesasociate(SpecieName);
            Session["SessionSpeciesasociate"] = SessionSpeciesasociate;

            return Json(true, JsonRequestBehavior.AllowGet);
        }



        /*      ACTIVESUBSTANCES         */
        public ActionResult clasificationProductSubstances(int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            if (ProductId != 0)
            {
                sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

                if (index != null)
                {
                    int CId = index.CId;
                    int DId = index.DId;
                    int EId = index.EId;
                    int BId = index.BId;

                    var c = db.Countries.Where(x => x.CountryId == CId).ToList();

                    foreach (Countries _c in c)
                    {
                        ViewData["CountryNameV"] = _c.CountryName;
                    }

                    var d = db.Divisions.Where(x => x.DivisionId == DId).ToList();

                    foreach (Divisions _d in d)
                    {
                        ViewData["DivisionNameV"] = _d.DivisionName;
                    }

                    var e = db.Editions.Where(x => x.EditionId == EId).ToList();

                    foreach (Editions _e in e)
                    {
                        ViewData["NumberEditionV"] = _e.NumberEdition;
                    }

                    var b = db.Books.Where(x => x.BookId == BId).ToList();

                    foreach (Books _b in b)
                    {
                        ViewData["BookNameV"] = _b.BookName;
                    }

                    var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

                    foreach (Products _p in p)
                    {
                        ViewData["ProductNameV"] = _p.ProductName;
                        ViewData["ProductIdV"] = _p.ProductId;
                    }

                    var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

                    foreach (PharmaceuticalForms _pf in pf)
                    {
                        ViewData["PharmaFormV"] = _pf.PharmaForm;
                        ViewData["PharmaFormIdV"] = _pf.PharmaFormId;
                    }

                    var cat = db.Categories.Where(x => x.CategoryId == CategoryId);

                    foreach (Categories _cat in cat)
                    {
                        ViewData["CategoryNameV"] = _cat.CategoryName;
                        ViewData["CategoryIdV"] = _cat.CategoryId;
                    }

                    var pe = db.ProductSubstances.Where(x => x.ProductId == ProductId).ToList();

                    if (pe.LongCount() > 0)
                    {
                        ActiveSubstances A = new ActiveSubstances();
                        List<ActiveSubstances> LA = new List<ActiveSubstances>();

                        foreach (ProductSubstances _pe in pe)
                        {
                            var eq = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == _pe.ActiveSubstanceId);

                            foreach (ActiveSubstances _as in eq)
                            {
                                A = new ActiveSubstances();

                                A.Active = _as.Active;
                                A.ActiveSubstanceId = _as.ActiveSubstanceId;
                                A.ActiveSubstanceName = _as.ActiveSubstanceName;

                                LA.Add(A);
                            }
                        }

                        LA = LA.OrderBy(x => x.ActiveSubstanceName).ToList();

                        return View(LA);
                    }
                    else
                    {
                        var eq = db.ActiveSubstances.Where(x => x.ActiveSubstanceId == 0);

                        return View(eq);
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult insertproductsubstances(int ActiveSubstanceId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ps = db.ProductSubstances.Where(x => x.ActiveSubstanceId == ActiveSubstanceId && x.ProductId == ProductId).ToList();

            if (ps.LongCount() == 0)
            {
                ProductSubstances.ActiveSubstanceId = ActiveSubstanceId;
                ProductSubstances.ProductId = ProductId;

                db.ProductSubstances.Add(ProductSubstances);
                db.SaveChanges();

                int OperationId = 1;
                ActivityLog._productsubstances(ActiveSubstanceId, ProductId, ApplicationId, UsersId, OperationId);
            }
            return RedirectToAction("clasificationProductSubstances", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult deleteproductsubstances(int ActiveSubstanceId, int ProductId, int PharmaFormId, int CategoryId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ps = db.ProductSubstances.SingleOrDefault(x => x.ActiveSubstanceId == ActiveSubstanceId && x.ProductId == ProductId);

            if (ps != null)
            {
                db.ProductSubstances.Remove(ps);
                db.SaveChanges();

                int OperationId = 4;
                ActivityLog._productsubstances(ActiveSubstanceId, ProductId, ApplicationId, UsersId, OperationId);
            }
            return RedirectToAction("clasificationProductSubstances", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult searchsubstances(string ActiveSubstanceName)
        {
            SessionSubstances SessionSubstances = new SessionSubstances(ActiveSubstanceName);
            Session["SessionSubstances"] = SessionSubstances;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchsubstancesasoc(string ActiveSubstanceName)
        {
            SessionSubstancessearch SessionSubstancessearch = new SessionSubstancessearch(ActiveSubstanceName);
            Session["SessionSubstancessearch"] = SessionSubstancessearch;

            return Json(true, JsonRequestBehavior.AllowGet);
        }



        /*      THERAPEUTIC USES      */
        public ActionResult clasificationTherapeuticUses(int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            if (ProductId != 0)
            {
                sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

                if (index != null)
                {
                    int CId = index.CId;
                    int DId = index.DId;
                    int EId = index.EId;
                    int BId = index.BId;

                    var c = db.Countries.Where(x => x.CountryId == CId).ToList();

                    foreach (Countries _c in c)
                    {
                        ViewData["CountryNameV"] = _c.CountryName;
                    }

                    var d = db.Divisions.Where(x => x.DivisionId == DId).ToList();

                    foreach (Divisions _d in d)
                    {
                        ViewData["DivisionNameV"] = _d.DivisionName;
                    }

                    var e = db.Editions.Where(x => x.EditionId == EId).ToList();

                    foreach (Editions _e in e)
                    {
                        ViewData["NumberEditionV"] = _e.NumberEdition;
                    }

                    var b = db.Books.Where(x => x.BookId == BId).ToList();

                    foreach (Books _b in b)
                    {
                        ViewData["BookNameV"] = _b.BookName;
                    }

                    var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

                    foreach (Products _p in p)
                    {
                        ViewData["ProductNameV"] = _p.ProductName;
                        ViewData["ProductIdV"] = _p.ProductId;
                    }

                    var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

                    foreach (PharmaceuticalForms _pf in pf)
                    {
                        ViewData["PharmaFormV"] = _pf.PharmaForm;
                        ViewData["PharmaFormIdV"] = _pf.PharmaFormId;
                    }

                    var cat = db.Categories.Where(x => x.CategoryId == CategoryId);

                    foreach (Categories _cat in cat)
                    {
                        ViewData["CategoryNameV"] = _cat.CategoryName;
                        ViewData["CategoryIdV"] = _cat.CategoryId;
                    }


                    var eq = db.Database.SqlQuery<TherapeuticUsesStructure>("plm_spGetTherapeuticUsesByProduct @ProductId=" + ProductId + "").OrderBy(x => x.TherapeuticName).ToList();


                    return View(eq);
                }
                else
                {
                    return RedirectToAction("Logout", "Login");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult getTherapeuticUses(string Letter, string Product, string PharmaForm, string Category)
        {
            if (!string.IsNullOrEmpty(Letter))
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int TherapeuticId = int.Parse(Letter);
                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);

                var tp = db.ProductTherapeuticUses.Where(x => x.TherapeuticId == TherapeuticId && x.ProductId == ProductId).ToList();

                if (tp.LongCount() == 0)
                {
                    ProductTherapeuticUses.ProductId = ProductId;
                    ProductTherapeuticUses.TherapeuticId = TherapeuticId;

                    db.ProductTherapeuticUses.Add(ProductTherapeuticUses);
                    db.SaveChanges();

                    int OperationId = 1;
                    ActivityLog._producttherapeuticuses(TherapeuticId, ProductId, ApplicationId, UsersId, OperationId);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("exist", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteTherapeuticUses(int TherapeuticId, int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            if (ProductId != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                var pt = db.ProductTherapeuticUses.SingleOrDefault(x => x.TherapeuticId == TherapeuticId && x.ProductId == ProductId);

                if (pt != null)
                {
                    db.ProductTherapeuticUses.Remove(pt);
                    db.SaveChanges();

                    int OperationId = 4;
                    ActivityLog._producttherapeuticuses(TherapeuticId, Convert.ToInt32(ProductId), ApplicationId, UsersId, OperationId);
                }
            }
            return RedirectToAction("clasificationTherapeuticUses", new { ProductId, PharmaFormId, CategoryId });
        }

        public ActionResult getletter(string Letters)
        {
            if (!string.IsNullOrEmpty(Letters))
            {
                SessionTherapeuticUsesTree SessionTherapeuticUsesTree = new SessionTherapeuticUsesTree(Letters);
                Session["SessionTherapeuticUsesTree"] = SessionTherapeuticUsesTree;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cleanletter(string Letters)
        {
            Session["SessionTherapeuticUsesTree"] = null;
            Session["SessionsearchTherapeuticUses"] = null;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchuses(string TherapeuticName)
        {
            TherapeuticName = TherapeuticName.ToUpper().Substring(0, 1).Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ü", "U");

            SessionTherapeuticUses SessionTherapeuticUses = new SessionTherapeuticUses(TherapeuticName);
            Session["SessionTherapeuticUses"] = SessionTherapeuticUses;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchtherapeuticuses(string TherapeuticName, string Product, string PharmaForm, string Category)
        {
            sessionveterinary index = (sessionveterinary)Session["sessionveterinary"];

            if (index != null)
            {
                int CId = index.CId;
                int DId = index.DId;
                int EId = index.EId;
                int BId = index.BId;
                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);


                var c = db.Countries.Where(x => x.CountryId == CId).ToList();

                foreach (Countries _c in c)
                {
                    ViewData["CountryNameV"] = _c.CountryName;
                }

                var d = db.Divisions.Where(x => x.DivisionId == DId).ToList();

                foreach (Divisions _d in d)
                {
                    ViewData["DivisionNameV"] = _d.DivisionName;
                }

                var e = db.Editions.Where(x => x.EditionId == EId).ToList();

                foreach (Editions _e in e)
                {
                    ViewData["NumberEditionV"] = _e.NumberEdition;
                }

                var b = db.Books.Where(x => x.BookId == BId).ToList();

                foreach (Books _b in b)
                {
                    ViewData["BookNameV"] = _b.BookName;
                }

                var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

                foreach (Products _p in p)
                {
                    ViewData["ProductNameV"] = _p.ProductName;
                    ViewData["ProductIdV"] = _p.ProductId;
                }

                var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

                foreach (PharmaceuticalForms _pf in pf)
                {
                    ViewData["PharmaFormV"] = _pf.PharmaForm;
                    ViewData["PharmaFormIdV"] = _pf.PharmaFormId;
                }

                var cat = db.Categories.Where(x => x.CategoryId == CategoryId);

                foreach (Categories _cat in cat)
                {
                    ViewData["CategoryNameV"] = _cat.CategoryName;
                    ViewData["CategoryIdV"] = _cat.CategoryId;
                }
            }

            if (!string.IsNullOrEmpty(TherapeuticName))
            {
                SessionsearchTherapeuticUses SessionsearchTherapeuticUses = new SessionsearchTherapeuticUses(TherapeuticName);
                Session["SessionsearchTherapeuticUses"] = SessionsearchTherapeuticUses;
            }
            else
            {
                Session["SessionsearchTherapeuticUses"] = null;
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}