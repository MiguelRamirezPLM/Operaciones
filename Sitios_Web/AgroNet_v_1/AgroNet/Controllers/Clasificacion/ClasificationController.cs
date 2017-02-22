using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;
using System.Windows.Forms;

namespace AgroNet.Controllers.Clasificacion
{
    public class ClasificationController : Controller
    {
        private DEAQ db = new DEAQ();
        public DEAQ DEAQ = new DEAQ();
        public PProductSubstances PProductSubstances = new PProductSubstances();
        public ProductCrops ProductCrops = new ProductCrops();
        public PProductSeeds PProductSeeds = new PProductSeeds();
        public PProductAgrochemicalUses PProductAgrochemicalUses = new PProductAgrochemicalUses();
        ActivityLog ActivityLog = new ActivityLog();


        public ActionResult Index(string CountryId, string DivisionId, string EditionId, string ProductName, string ProductId, string BookId, string TypeInEdition)
        {
            SearchClass search = (SearchClass)Session["SearchClass"];
            TypeInEditionn ST = (TypeInEditionn)Session["TypeInEdition"];
            if (search != null)
            {
                if (ProductName != null)
                {
                    if (ProductName == string.Empty)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        if (ST == null)
                        {
                            int count = 0;
                            int Coun = int.Parse(search.CId);
                            int Div1 = int.Parse(search.DId);
                            int Edition = int.Parse(search.EId);

                            var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                       where plm_vw.DivisionId == Div1
                                       && plm_vw.EditionId == Edition
                                       && plm_vw.CountryId == Coun
                                       && plm_vw.TypeInEdition == "P"
                                       select plm_vw);

                            if (!string.IsNullOrEmpty(ProductName))
                            {
                                Ind = Ind.Where(s => s.ProductName.StartsWith(ProductName)).OrderBy(x => x.ProductName);
                                foreach (plm_vwProductsByEdition J in Ind)
                                {
                                    count = count + 1;
                                }
                                ViewData["Count"] = count;
                            }
                            return View(Ind);
                        }
                        else
                        {
                            if (TypeInEdition == null)
                            {
                                int count = 0;
                                string type = ST.TypeInEdition;
                                if (type == "P")
                                {
                                    int Edit = int.Parse(search.EId);
                                    int Div1 = int.Parse(search.DId);
                                    int Coun = int.Parse(search.CId);
                                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                               where plm_vw.DivisionId == Div1
                                               && plm_vw.EditionId == Edit
                                               && plm_vw.CountryId == Coun
                                               && plm_vw.TypeInEdition == "P"
                                               select plm_vw);

                                    if (!string.IsNullOrEmpty(ProductName))
                                    {
                                        Ind = Ind.Where(s => s.ProductName.StartsWith(ProductName)).OrderBy(x => x.ProductName);
                                        foreach (plm_vwProductsByEdition J in Ind)
                                        {
                                            count = count + 1;
                                        }
                                        ViewData["Count"] = count;
                                    }
                                    return View(Ind);
                                }
                                else if (type == "N")
                                {
                                    int Edit = int.Parse(search.EId);
                                    int Div1 = int.Parse(search.DId);
                                    int Coun = int.Parse(search.CId);
                                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                               where plm_vw.DivisionId == Div1
                                               && plm_vw.EditionId == Edit
                                               && plm_vw.CountryId == Coun
                                               && plm_vw.TypeInEdition == "P"
                                               && plm_vw.NewProduct == "N"
                                               select plm_vw);

                                    if (!string.IsNullOrEmpty(ProductName))
                                    {
                                        Ind = Ind.Where(s => s.ProductName.StartsWith(ProductName)).OrderBy(x => x.ProductName);
                                        foreach (plm_vwProductsByEdition J in Ind)
                                        {
                                            count = count + 1;
                                        }
                                        ViewData["Count"] = count;
                                    }
                                    return View("Index", Ind);
                                }
                                else if (type == "C/C")
                                {
                                    int Edit = int.Parse(search.EId);
                                    int Div1 = int.Parse(search.DId);
                                    int Coun = int.Parse(search.CId);
                                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                               where plm_vw.DivisionId == Div1
                                               && plm_vw.EditionId == Edit
                                               && plm_vw.CountryId == Coun
                                               && plm_vw.TypeInEdition == "P"
                                               && plm_vw.ContentType == "C/C"
                                               select plm_vw);

                                    if (!string.IsNullOrEmpty(ProductName))
                                    {
                                        Ind = Ind.Where(s => s.ProductName.StartsWith(ProductName)).OrderBy(x => x.ProductName);
                                        foreach (plm_vwProductsByEdition J in Ind)
                                        {
                                            count = count + 1;
                                        }
                                        ViewData["Count"] = count;
                                    }
                                    return View("Index", Ind);
                                }
                            }
                        }
                    }
                }
            }

            if (TypeInEdition != null)
            {
                int count = 0;
                if (TypeInEdition == "P")
                {
                    int Edit = int.Parse(search.EId);
                    int Div1 = int.Parse(search.DId);
                    int Coun = int.Parse(search.CId);
                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                               where plm_vw.DivisionId == Div1
                               && plm_vw.EditionId == Edit
                               && plm_vw.CountryId == Coun
                               && plm_vw.TypeInEdition == "P"
                               orderby plm_vw.ProductName ascending
                               select plm_vw).ToList();
                    foreach (plm_vwProductsByEdition plm in Ind)
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
                    TypeInEditionn TInEdition = new TypeInEditionn(TypeInEdition);
                    Session["TypeInEdition"] = TInEdition;

                    return View("Index", Ind);
                }
                else if (TypeInEdition == "N")
                {
                    int Edit = int.Parse(search.EId);
                    int Div1 = int.Parse(search.DId);
                    int Coun = int.Parse(search.CId);
                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                               where plm_vw.DivisionId == Div1
                               && plm_vw.EditionId == Edit
                               && plm_vw.CountryId == Coun
                               && plm_vw.TypeInEdition == "P"
                               && plm_vw.NewProduct == "N"
                               orderby plm_vw.ProductName ascending
                               select plm_vw).ToList();
                    foreach (plm_vwProductsByEdition plm in Ind)
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
                    TypeInEditionn TInEdition = new TypeInEditionn(TypeInEdition);
                    Session["TypeInEdition"] = TInEdition;

                    return View("Index", Ind);
                }
                else if (TypeInEdition == "C/C")
                {
                    int Edit = int.Parse(search.EId);
                    int Div1 = int.Parse(search.DId);
                    int Coun = int.Parse(search.CId);
                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                               where plm_vw.DivisionId == Div1
                               && plm_vw.EditionId == Edit
                               && plm_vw.CountryId == Coun
                               && plm_vw.TypeInEdition == "P"
                               && plm_vw.ContentType == "C/C"
                               orderby plm_vw.ProductName ascending
                               select plm_vw).ToList();
                    foreach (plm_vwProductsByEdition plm in Ind)
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
                    TypeInEditionn TInEdition = new TypeInEditionn(TypeInEdition);
                    Session["TypeInEdition"] = TInEdition;

                    return View("Index", Ind);
                }
            }

            if (DivisionId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int Division = int.Parse(DivisionId);
                int Edition = int.Parse(EditionId);
                string CId = CountryId;
                string DId = DivisionId;
                string EId = EditionId;
                string PId = ProductId;
                string BId = BookId;

                List<JoinClasification> LCLASS = new List<JoinClasification>();
                List<Crops> LCROPS = new List<Crops>();
                JoinClasification jclass = new JoinClasification();
                Crops Crops = new Models.Crops();

                var Ind = (from plm_vw in db.plm_vwProductsByEdition
                           where plm_vw.DivisionId == Division
                           && plm_vw.EditionId == Edition
                           && plm_vw.CountryId == country
                           && plm_vw.TypeInEdition == "P"
                           orderby plm_vw.ProductName ascending
                           select plm_vw).ToList();
                foreach (plm_vwProductsByEdition plm in Ind)
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
                SearchClass SearchClass = new SearchClass(CId, DId, EId, PId, BId);
                Session["SearchClass"] = SearchClass;
                return View(Ind);
            }

            else if (search != null)
            {
                if (ST != null)
                {
                    if (TypeInEdition == null)
                    {
                        int count = 0;
                        string type = ST.TypeInEdition;
                        if (type == "P")
                        {
                            int Edit = int.Parse(search.EId);
                            int Div1 = int.Parse(search.DId);
                            int Coun = int.Parse(search.CId);
                            var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                       where plm_vw.DivisionId == Div1
                                       && plm_vw.EditionId == Edit
                                       && plm_vw.CountryId == Coun
                                       && plm_vw.TypeInEdition == "P"
                                       orderby plm_vw.ProductName ascending
                                       select plm_vw).ToList();
                            foreach (plm_vwProductsByEdition plm in Ind)
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
                            return View("Index", Ind);
                        }
                        else if (type == "N")
                        {
                            int Edit = int.Parse(search.EId);
                            int Div1 = int.Parse(search.DId);
                            int Coun = int.Parse(search.CId);
                            var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                       where plm_vw.DivisionId == Div1
                                       && plm_vw.EditionId == Edit
                                       && plm_vw.CountryId == Coun
                                       && plm_vw.TypeInEdition == "P"
                                       && plm_vw.NewProduct == "N"
                                       orderby plm_vw.ProductName ascending
                                       select plm_vw).ToList();
                            foreach (plm_vwProductsByEdition plm in Ind)
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
                            return View("Index", Ind);
                        }
                        else if (type == "C/C")
                        {
                            int Edit = int.Parse(search.EId);
                            int Div1 = int.Parse(search.DId);
                            int Coun = int.Parse(search.CId);
                            var Ind = (from plm_vw in db.plm_vwProductsByEdition
                                       where plm_vw.DivisionId == Div1
                                       && plm_vw.EditionId == Edit
                                       && plm_vw.CountryId == Coun
                                       && plm_vw.TypeInEdition == "P"
                                       && plm_vw.ContentType == "C/C"
                                       orderby plm_vw.ProductName ascending
                                       select plm_vw).ToList();
                            foreach (plm_vwProductsByEdition plm in Ind)
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
                            return View("Index", Ind);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    int Edit = int.Parse(search.EId);
                    int Div1 = int.Parse(search.DId);
                    int Coun = int.Parse(search.CId);

                    var Ind = (from plm_vw in db.plm_vwProductsByEdition
                               where plm_vw.DivisionId == Div1
                               && plm_vw.EditionId == Edit
                               && plm_vw.CountryId == Coun
                               && plm_vw.TypeInEdition == "P"
                               orderby plm_vw.ProductName ascending
                               select plm_vw).ToList();
                    foreach (plm_vwProductsByEdition plm in Ind)
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
                    return View(Ind);
                }
            }

            if (DivisionId == null)
            {
                var Ind = from plm_vw in db.plm_vwProductsByEdition
                          where plm_vw.DivisionId == 0
                          && plm_vw.EditionId == 0
                          && plm_vw.CountryId == 0
                          && plm_vw.TypeInEdition == "P"
                          orderby plm_vw.ProductName ascending
                          select plm_vw;
                ViewData["CountProds"] = null;
                return View(Ind);
            }
            return View();
        }


        [HttpPost]
        public JsonResult Divisions(int country)
        {
            Divisions Divs = new Divisions();
            List<Divisions> LDivs = new List<Models.Divisions>();
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            List<int> Lcountry = p.country;

            var Divisions = (from Division in db.Divisions
                             where Division.CountryId == country
                             && Division.ParentId == null
                             orderby Division.DivisionName ascending
                             select Division);
            foreach (Divisions w in Divisions)
            {
                Divs = new Divisions();
                Divs.DivisionName = w.DivisionName;
                Divs.DivisionId = w.DivisionId;
                LDivs.Add(Divs);
            }
            return Json(LDivs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edition(int country, string BookId)
        {
            Editions Editions = new Models.Editions();
            List<Editions> LEditions = new List<Models.Editions>();
            int Book = int.Parse(BookId);
            var Edit = from Editionss in db.Editions
                       where Editionss.CountryId == country
                       && Editionss.BookId == Book
                       orderby Editionss.NumberEdition ascending
                       select Editionss;
            foreach (Editions Ed in Edit)
            {
                Editions = new Editions();
                Editions.NumberEdition = Ed.NumberEdition;
                Editions.EditionId = Ed.EditionId;
                LEditions.Add(Editions);
            }
            return Json(LEditions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult books(int country)
        {
            Books Book = new Books();
            List<Books> LBooks = new List<Books>();

            var _book = from Bookss in db.Books
                        select Bookss;
            foreach (Books B in _book)
            {
                Book = new Books();
                Book.BookId = B.BookId;
                Book.BookName = B.BookName;
                Book.Active = B.Active;
                Book.ShortName = B.ShortName;
                LBooks.Add(Book);
            }
            return Json(LBooks, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Products(int country, int editionid, int divisionid)
        {
            List<Products> LPRODS = new List<Models.Products>();
            Products Prod = new Models.Products();
            var Divs = from Divisions in db.Divisions
                       where Divisions.DivisionId == divisionid
                       select Divisions;
            foreach (Divisions D in Divs)
            {
                if (D.DivisionId.Equals(divisionid))
                {
                    int? LaboratoryId = D.LaboratoryId;
                    var Prodcuts = from Prods in db.Products
                                   where Prods.CountryId == country
                                   && Prods.LaboratoryId == LaboratoryId
                                   orderby Prods.ProductName ascending
                                   select Prods;
                    foreach (Products P in Prodcuts)
                    {
                        Prod = new Products();
                        Prod.ProductId = P.ProductId;
                        Prod.ProductName = P.ProductName;
                        LPRODS.Add(Prod);
                    }
                }
            }

            return Json(LPRODS, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Clasification(int? ProductId, int? PharmaFormId, int? CategoryId)
        {
            ViewData["Subs"] = false;
            if (ProductId == null)
            {
                ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
                int? PId = CP1.PId;
                var aA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + PId + "").ToList();
                //ViewData["Subs"] = true;
                return View(aA);
            }
            else
            {
                int? PId = ProductId;
                int? PFId = PharmaFormId;
                int? CatId = CategoryId;
                ClasificationProds ClasificationProds = new ClasificationProds(PId, PFId, CatId);
                Session["ClasificationProds"] = ClasificationProds;
                var aA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + PId + "").ToList();
                //ViewData["Subs"] = false;
                return View(aA);
            }
        }

        public ActionResult InsertActiveSubstances(string ActiveSubstanceId, string ProductId, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = 0;
            int UsersId = 0;
            ApplicationId = p.ApplicationId;
            UsersId = p.userId;

            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;

            int ProdId = Convert.ToInt32(PProductId);
            int ActiveSubstance = int.Parse(ActiveSubstanceId);

            PProductSubstances.ActiveSubstanceId = ActiveSubstance;
            PProductSubstances.ProductId = ProdId;

            db.PProductSubstances.Add(PProductSubstances);
            db.SaveChanges();


            ActivityLog.ProductSubstances(ApplicationId, ActiveSubstance, ProdId, UsersId);


            return RedirectToAction("Clasification", "Clasification");
        }

        public ActionResult DeleteActiveSubstances(string ActiveSubstanceId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int ActiveSubstance = int.Parse(ActiveSubstanceId);

            var Act = from AS in db.PProductSubstances
                      where AS.ActiveSubstanceId == ActiveSubstance
                      && AS.ProductId == ProdId
                      select AS;

            foreach (PProductSubstances PPS in Act)
            {
                var Delete = DEAQ.PProductSubstances.FirstOrDefault(x => x.ProductId == PPS.ProductId && x.ActiveSubstanceId == PPS.ActiveSubstanceId);
                DEAQ.PProductSubstances.Remove(Delete);
                DEAQ.SaveChanges();
                ActivityLog.DeleteProductSubstances(ApplicationId, ActiveSubstance, ProdId, UsersId);
            }
            return RedirectToAction("Clasification", "Clasification");
        }

        public ActionResult searchsubstance(string ActiveSubstanceName)
        {
            int count = 0;
            if (ActiveSubstanceName != null)
            {
                if (ActiveSubstanceName == string.Empty)
                {
                    return RedirectToAction("Clasification");
                }
                else
                {
                    ClasificationProds CPPC = (ClasificationProds)Session["ClasificationProds"];

                    int? ProdIdC = CPPC.PId;
                    var PSubs = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPPSubstancesSearch @ProductId=" + ProdIdC + ", @param='" + ActiveSubstanceName + "'").ToList();
                    foreach (ActiveSubstances A in PSubs)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                    SearchSubstance SearchSubs = new SearchSubstance(ActiveSubstanceName);
                    Session["SearchSubs"] = SearchSubs;
                    return View("Clasification", PSubs);
                }
            }
            else if (ActiveSubstanceName == null)
            {
                SearchSubstance Sr = (SearchSubstance)Session["SearchSubs"];
                var Ind = from ActSubs in db.ActiveSubstances
                          select ActSubs;

                if (!string.IsNullOrEmpty(Sr.ActiveSubstance))
                {
                    Ind = Ind.Where(s => s.ActiveSubstanceName.StartsWith(Sr.ActiveSubstance));
                    foreach (ActiveSubstances J in Ind)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                }
                return View("Clasification", Ind);
            }
            return View();
        }

        public ActionResult substances(int? ProductId, int? PharmaFormId, int? CategoryId, string ActiveSubstanceName)
        {
            Search search = (Search)Session["Search"];

            if (ProductId != null)
            {
                int? PId = ProductId;
                int? PFId = PharmaFormId;
                int? CatId = CategoryId;
                ClasificationProds ClasificationProds = new ClasificationProds(PId, PFId, CatId);
                Session["ClasificationProds"] = ClasificationProds;
                var aA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + PId + "").ToList();
                //ViewData["Subs"] = false;
                ViewData["Count"] = null;
                return View(aA);
            }

            else
            {
                ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
                int? PId = CP1.PId;
                var aA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + PId + "").ToList();
                ViewData["Subs"] = true;
                ViewData["Count"] = null;
                return View(aA);
            }
        }

        public ActionResult productcrops()
        {

            ClasificationProds CPPC = (ClasificationProds)Session["ClasificationProds"];

            int? ProdIdC = CPPC.PId;
            var PCrops = db.Database.SqlQuery<Crops>("plm_spGetPProductCrops @ProductId=" + ProdIdC + "").ToList();
            ViewData["Count"] = null;
            return View(PCrops);

        }

        public ActionResult InsertProductCrops(string CropId, string ProductId, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = 0;
            int UsersId = 0;
            ApplicationId = p.ApplicationId;
            UsersId = p.userId;

            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;

            int ProdId = Convert.ToInt32(PProductId);
            int Crop = int.Parse(CropId);

            ProductCrops.CropId = Crop;
            ProductCrops.ProductId = ProdId;

            db.ProductCrops.Add(ProductCrops);
            db.SaveChanges();
            ActivityLog.InsertProductCrops(ApplicationId, Crop, ProdId, UsersId);
            return RedirectToAction("productcrops", "Clasification");
        }

        public ActionResult DeleteProductCrops(string CropId, string ProductId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int Crop = int.Parse(CropId);

            var Act = from AS in db.ProductCrops
                      where AS.CropId == Crop
                      && AS.ProductId == ProdId
                      select AS;

            foreach (ProductCrops PPS in Act)
            {
                var Delete = DEAQ.ProductCrops.FirstOrDefault(x => x.ProductId == PPS.ProductId && x.CropId == PPS.CropId);
                DEAQ.ProductCrops.Remove(Delete);
                DEAQ.SaveChanges();
                ActivityLog.DeleteProductCrops(ApplicationId, Crop, ProdId, UsersId);
            }
            return RedirectToAction("productcrops", "Clasification");
        }

        public ActionResult productseeds()
        {
            ClasificationProds CPPC = (ClasificationProds)Session["ClasificationProds"];
            int? ProdIdC = CPPC.PId;
            var PSeeds = db.Database.SqlQuery<Seeds>("plm_spGetPProductSeeds  @ProductId=" + ProdIdC + "").ToList();
            ViewData["Count"] = null;
            return View(PSeeds);
        }

        public ActionResult InsertProductSeeds(string SeedId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int Seed = int.Parse(SeedId);

            PProductSeeds.ProductId = ProdId;
            PProductSeeds.SeedId = Seed;

            db.PProductSeeds.Add(PProductSeeds);
            db.SaveChanges();
            ActivityLog.InsertProductSeeds(ApplicationId, Seed, ProdId, UsersId);
            return RedirectToAction("productseeds", "Clasification");
        }

        public ActionResult DeleteProductSeeds(string SeedId, string ProductId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int Seed = int.Parse(SeedId);

            var Act = from AS in db.PProductSeeds
                      where AS.SeedId == Seed
                      && AS.ProductId == ProdId
                      select AS;

            foreach (PProductSeeds PPS in Act)
            {
                var Delete = DEAQ.PProductSeeds.FirstOrDefault(x => x.ProductId == PPS.ProductId && x.SeedId == PPS.SeedId);
                DEAQ.PProductSeeds.Remove(Delete);
                DEAQ.SaveChanges();
                ActivityLog.InsertProductSeeds(ApplicationId, Seed, ProdId, UsersId);
            }
            return RedirectToAction("productseeds", "Clasification");
        }

        public ActionResult searchcrop(string CropName)
        {
            int count = 0;
            if (CropName != null)
            {
                if (CropName == string.Empty)
                {
                    return RedirectToAction("productcrops");
                }
                else
                {
                    ClasificationProds CPPC = (ClasificationProds)Session["ClasificationProds"];

                    int? ProdIdC = CPPC.PId;
                    var PCrops = db.Database.SqlQuery<Crops>("plm_spGetPPCropsSearch @ProductId=" + ProdIdC + ", @param='" + CropName + "'").ToList();
                    foreach (Crops C in PCrops)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;

                    SearchCrop searchCrop = new SearchCrop(CropName);
                    Session["searchCrop"] = searchCrop;
                    return View("productcrops", PCrops);
                }
            }
            else if (CropName == null)
            {
                SearchCrop Sr = (SearchCrop)Session["searchCrop"];
                var Ind = from Crop in db.Crops
                          select Crop;

                if (!string.IsNullOrEmpty(Sr.CropName))
                {
                    Ind = Ind.Where(s => s.CropName.StartsWith(Sr.CropName));
                    foreach (Crops J in Ind)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                }
                return View("productcrops", Ind);
            }
            return View();
        }

        public ActionResult searchseeds(string SeedName)
        {
            Search search = (Search)Session["Search"];
            if (search != null)
            {
                if (SeedName != null)
                {
                    if (SeedName == string.Empty)
                    {
                        return RedirectToAction("productseeds");
                    }
                    else
                    {
                        int count = 0;
                        int Coun = int.Parse(search.CId);
                        int Div1 = int.Parse(search.DId);
                        int Edition = int.Parse(search.EId);

                        var Ind = from plm_vw in db.Seeds
                                  select plm_vw;

                        if (!string.IsNullOrEmpty(SeedName))
                        {
                            Ind = Ind.Where(s => s.SeedName.StartsWith(SeedName));
                            foreach (Seeds J in Ind)
                            {
                                count = count + 1;
                            }
                            ViewData["Count"] = count;
                        }
                        SearchSeeds SearchSeed = new SearchSeeds(SeedName);
                        Session["SearchSeeds"] = SearchSeed;
                        return View("productseeds", Ind);
                    }
                }
                else
                {
                    SearchSeeds Sr = (SearchSeeds)Session["SearchSeeds"];
                    int count = 0;
                    var Ind = from plm_vw in db.Seeds
                              select plm_vw;

                    if (!string.IsNullOrEmpty(Sr.SeedName))
                    {
                        Ind = Ind.Where(s => s.SeedName.StartsWith(Sr.SeedName));
                        foreach (Seeds J in Ind)
                        {
                            count = count + 1;
                        }
                        ViewData["Count"] = count;
                    }
                    return View("productseeds", Ind);
                }
            }
            return View();
        }

        public ActionResult agrochemicaluses(string AgrochemicalUseName)
        {
            List<AgrochemicalUses> all = new List<AgrochemicalUses>();

            SessionSearchUse ind = (SessionSearchUse)Session["SessionSearchUse"];

            if (!string.IsNullOrEmpty(AgrochemicalUseName))
            {
                all = db.Database.SqlQuery<AgrochemicalUses>("plm_spGetAgrochemicalUserSearch @param='" + AgrochemicalUseName + "'").ToList();
                ViewData["Count"] = null;

                SessionSearchUse SessionSearchUse = new SessionSearchUse(AgrochemicalUseName);
                Session["SessionSearchUse"] = SessionSearchUse;

                return View(all);
            }
            //if (ind != null)
            //{
            //    all = db.Database.SqlQuery<AgrochemicalUses>("plm_spGetAgrochemicalUserSearch @param='" + ind.AgrochemicalUseName + "'").ToList();
            //    ViewData["Count"] = null;

            //    //SessionSearchUse SessionSearchUse = new SessionSearchUse(ind.AgrochemicalUseName);
            //    //Session["SessionSearchUse"] = SessionSearchUse;

            //    return View(all);
            //}
            else
            {
                all = db.AgrochemicalUses.OrderBy(a => a.ParentId).OrderBy(a => a.AgrochemicalUseName).ToList();
                ViewData["Count"] = null;
                Session["SessionSearchUse"] = null;
                return View(all);
            }
        }

        public ActionResult insertagrochemicaluses(int? Id)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int Use = Convert.ToInt32(Id);

            var PAU = from ProdAU in db.PProductAgrochemicalUsesSet
                      where ProdAU.ProductId == ProdId
                      && ProdAU.AgrochemicalUseId == Use
                      select ProdAU;
            if (PAU.LongCount() > 0)
            {
                MessageBox.Show("El uso ya esta asignado", "Aviso");
                return RedirectToAction("agrochemicaluses", "Clasification");
            }
            else
            {
                PProductAgrochemicalUses.ProductId = ProdId;
                PProductAgrochemicalUses.AgrochemicalUseId = Use;

                db.PProductAgrochemicalUsesSet.Add(PProductAgrochemicalUses);
                db.SaveChanges();

                Id = null;
                ActivityLog.InsertProductAgrochemicalUses(ApplicationId, Use, ProdId, UsersId);
                return RedirectToAction("agrochemicaluses", "Clasification");
            }
        }

        public ActionResult deleteagrochemicaluses(string AgrochemicalUseId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            ClasificationProds CP1 = (ClasificationProds)Session["ClasificationProds"];
            int? PProductId = CP1.PId;
            int ProdId = Convert.ToInt32(PProductId);
            int Use = int.Parse(AgrochemicalUseId);

            var Act = from AS in db.PProductAgrochemicalUsesSet
                      where AS.AgrochemicalUseId == Use
                      && AS.ProductId == ProdId
                      select AS;

            foreach (PProductAgrochemicalUses PPS in Act)
            {
                var Delete = DEAQ.PProductAgrochemicalUsesSet.FirstOrDefault(x => x.ProductId == PPS.ProductId && x.AgrochemicalUseId == PPS.AgrochemicalUseId);
                DEAQ.PProductAgrochemicalUsesSet.Remove(Delete);
                DEAQ.SaveChanges();
                ActivityLog.DeleteProductAgrochemicalUses(ApplicationId, Use, ProdId, UsersId);
            }
            return RedirectToAction("agrochemicaluses", "Clasification");
        }

        public ActionResult searchs(string SeedName)
        {
            int count = 0;
            if (SeedName != null)
            {
                if (SeedName == string.Empty)
                {
                    return RedirectToAction("productseeds");
                }
                else
                {
                    ClasificationProds CPPC = (ClasificationProds)Session["ClasificationProds"];

                    int? ProdIdC = CPPC.PId;
                    var PSeeds = db.Database.SqlQuery<Seeds>("plm_spGetPPSeedsSearch @ProductId=" + ProdIdC + ", @param='" + SeedName + "'").ToList();
                    foreach (Seeds S in PSeeds)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                    SearchSeeds searchseed = new SearchSeeds(SeedName);
                    Session["searchseed"] = searchseed;
                    return View("productseeds", PSeeds);
                }
            }
            else if (SeedName == null)
            {
                SearchSeeds Sr = (SearchSeeds)Session["searchseed"];
                var Ind = from seed in db.Seeds
                          select seed;

                if (!string.IsNullOrEmpty(Sr.SeedName))
                {
                    Ind = Ind.Where(s => s.SeedName.StartsWith(Sr.SeedName));
                    foreach (Seeds J in Ind)
                    {
                        count = count + 1;
                    }
                    ViewData["Count"] = count;
                }
                return View("productseeds", Ind);
            }
            return View();
        }

        public ActionResult filter(string type)
        {
            SearchClass search = (SearchClass)Session["SearchClass"];
            if (type != null)
            {

                int typeid = int.Parse(type);
                if (typeid == 2)
                {
                    int Edit = int.Parse(search.EId);
                    int Div1 = int.Parse(search.DId);
                    int Coun = int.Parse(search.CId);
                    var Ind = from plm_vw in db.plm_vwProductsByEdition
                              where plm_vw.DivisionId == Div1
                              && plm_vw.EditionId == Edit
                              && plm_vw.CountryId == Coun
                              && plm_vw.TypeInEdition == "P"
                              && plm_vw.NewProduct == "N"
                              select plm_vw;
                    return View("Index", Ind);
                }
                else if (typeid == 3)
                {
                    return View();
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult participantproductsclasif(string Values)
        {
            sessionParticipantProductsClasification sessionParticipantProductsClasification = new sessionParticipantProductsClasification(Values);
            Session["sessionParticipantProductsClasification"] = sessionParticipantProductsClasification;

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
