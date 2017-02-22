using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;
using System.Windows.Forms;
using System.Data;


namespace AgroNet.Controllers
{
    public class VentasController : Controller
    {
        private DEAQ db = new DEAQ();
        public DEAQ DEAQ = new DEAQ();
        DEAQ dbs = new DEAQ();
        joinProductEditions joinProductEditions = new joinProductEditions();
        CreateProducts NewProducts = new CreateProducts();
        MentionatedProds MentionatedProds = new MentionatedProds();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        ActivityLogs ActivityLogs = new ActivityLogs();
        ActivityLog ActivityLog = new ActivityLog();

        MentionatedProducts MentionatedProductsIns = new MentionatedProducts();
        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();

        public PharmaForms P = new PharmaForms();
        public List<string> LJSON = new List<string>();
        public JSON J = new JSON();
        public Products Products = new Products();
        public Divisions DivisionsP = new Divisions();
        //
        // GET: /Ventas/

        public ActionResult Index(string CountryId, string DivisionId, string EditionId, string ProductName, string ProductId, string BookId)
        {
            Search search = (Search)Session["Search"];
            if (search != null)
            {
                if (ProductName != null)
                {
                    int count = 0;
                    int Coun = int.Parse(search.CId);
                    int Div1 = int.Parse(search.DId);
                    var Ind = (from PC in db.ProductCategories
                               join P in db.Products
                               on PC.ProductId equals P.ProductId
                               where P.CountryId == Coun
                               join D in db.Divisions
                               on PC.DivisionId equals D.DivisionId
                               where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))
                               join PF in db.PharmaForms
                               on PC.PharmaFormId equals PF.PharmaFormId
                               join C in db.Categories
                               on PC.CategoryId equals C.CategoryId
                               where D.DivisionId == Div1
                               && P.CountryId == Coun
                               orderby P.ProductName ascending
                               select new joinProductEditions { Products = P, Categories = C, Divisions = D, PharmaForms = PF, ProductCategories = PC/*, ProductPharmaForms = ppf , ParticipantProducts=PP*/ });

                    if (!string.IsNullOrEmpty(ProductName))
                    {
                        Ind = Ind.Where(s => s.Products.ProductName.StartsWith(ProductName));
                        foreach (joinProductEditions J in Ind)
                        {
                            count = count + 1;
                        }
                        ViewData["Count"] = count;
                    }
                    return View(Ind);
                }
            }
            if (DivisionId != null)
            {

                int count = 0;
                int country = int.Parse(CountryId);
                int Division = int.Parse(DivisionId);
                int book = int.Parse(BookId);
                int edition = int.Parse(EditionId);
                string CId = CountryId;
                string DId = DivisionId;
                string EId = EditionId;
                string PId = ProductId;
                string BId = BookId;
                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))
                           join PF in db.PharmaForms
                           on PC.PharmaFormId equals PF.PharmaFormId
                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where D.DivisionId == Division
                           && P.CountryId == country
                           orderby P.ProductName ascending
                           select new joinProductEditions { Products = P, Categories = C, Divisions = D, PharmaForms = PF, ProductCategories = PC/*, ProductPharmaForms = ppf , ParticipantProducts=PP*/ });

                if (!string.IsNullOrEmpty(DivisionId))
                {
                    var division = from Div in db.Divisions
                                   where Div.DivisionId == Division
                                   select Div;
                    foreach (Divisions D in division)
                    {
                        if (D.DivisionId.Equals(Division))
                        {
                            var Divisionname = D.DivisionName;
                            Ind = Ind.Where(s => s.Divisions.DivisionName.ToUpper().Contains(Divisionname));
                            foreach (joinProductEditions J in Ind)
                            {
                                count = count + 1;
                            }
                            ViewData["Count"] = null;
                        }
                    }
                }
                Search Search = new Search(CId, DId, EId, PId, BId);
                Session["Search"] = Search;
                return View(Ind);
            }

            else if (search != null)
            {
                int Coun = int.Parse(search.CId);
                int Div1 = int.Parse(search.DId);
                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                           join PF in db.PharmaForms
                           on PC.PharmaFormId equals PF.PharmaFormId
                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where D.DivisionId == Div1
                           && P.CountryId == Coun
                           orderby P.ProductName ascending

                           select new joinProductEditions { Products = P, Categories = C, Divisions = D, PharmaForms = PF, ProductCategories = PC/*, ProductPharmaForms = ppf , ParticipantProducts=PP*/ });

                if (!string.IsNullOrEmpty(Div1.ToString()))
                {
                    var division = from Div in db.Divisions
                                   where Div.DivisionId == Div1
                                   select Div;
                    foreach (Divisions D in division)
                    {
                        if (D.DivisionId.Equals(Div1))
                        {
                            var Divisionname = D.DivisionName;
                            Ind = Ind.Where(s => s.Divisions.DivisionName.ToUpper().Contains(Divisionname));
                        }
                    }
                }
                return View(Ind);
            }
            if (DivisionId == null)
            {

                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           join PF in db.PharmaForms
                           on PC.PharmaFormId equals PF.PharmaFormId
                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where PC.DivisionId == 0
                           && P.CountryId == 0
                           orderby P.ProductName ascending
                           select new joinProductEditions { Products = P, Categories = C, Divisions = D, PharmaForms = PF, ProductCategories = PC/*, ProductPharmaForms = ppf , ParticipantProducts=PP*/ });
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
                             && Division.Active == true
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

        [HttpPost]
        public ActionResult EditProducts(Products Products, string PName, string ProdDescription, string Register, string ProductId, string AppId, string UserId, string CountryId, string DivisionId)
        {
            try
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                if (ModelState.IsValid)
                {
                    var Division = int.Parse(DivisionId);
                    var country = int.Parse(CountryId);
                    int Product = int.Parse(ProductId);

                    var Divs = db.Divisions.Where(x => x.DivisionId == Division).ToList();

                    if (Divs.LongCount() > 0)
                    {
                        foreach (Divisions _divs in Divs)
                        {
                            var prod = db.Products.Where(x => x.LaboratoryId == _divs.LaboratoryId && x.ProductId == Product).ToList();
                            if (prod.LongCount() > 0)
                            {
                                foreach (Products lb in prod)
                                {
                                    lb.ProductName = PName.Trim();
                                    lb.Description = ProdDescription.Trim();
                                    lb.Register = Register.Trim();

                                    db.SaveChanges();

                                    ActivityLog.EditProducts(ApplicationId, UsersId, PName, ProdDescription, Register, ProductId, _divs.LaboratoryId, country);
                                }
                            }

                        }
                    }
                }
                return View(Products);
            }
            catch
            {
                return View(Products);
            }
        }

        public ActionResult InsertNewProducts(string ProductId, string DivisionId, string Category, string PharmaForm, string EditionId, Newproducts NProducts)
        {
            var PharmaF = from PForm in db.PharmaForms
                          where PForm.PharmaForm == PharmaForm
                          select PForm;
            foreach (PharmaForms PF in PharmaF)
            {
                if (PF.PharmaForm.Equals(PharmaForm))
                {
                    int PharmaFormId = PF.PharmaFormId;

                    var Catg = from Categ in db.Categories
                               where Categ.CategoryName == Category
                               select Categ;

                    foreach (Categories Categories in Catg)
                    {
                        if (Categories.CategoryName.Equals(Category))
                        {
                            int CategoryId = Categories.CategoryId;
                            NProducts.ProductId = int.Parse(ProductId);
                            NProducts.PharmaFormId = PharmaFormId;
                            NProducts.DivisionId = int.Parse(DivisionId);
                            NProducts.CategoryId = CategoryId;
                            NProducts.EditionId = int.Parse(EditionId);
                        }
                    }
                }
            }
            db.Newproducts.Add(NProducts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*      PRODUCTOS MENCIONADOS*/

        public ActionResult MentionatedProducts(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = int.Parse(AppId);
            int UsersId = int.Parse(UserId);
            ApplicationId = p.ApplicationId;
            UsersId = p.userId;

            int Product = int.Parse(ProductId);
            int Division = int.Parse(DivisionId);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int Edition = int.Parse(EditionId);

            var pc = db.ProductContents.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (pc.LongCount() > 0)
            {
                foreach (ProductContents _pc in pc)
                {
                    var delete = db.ProductContents.SingleOrDefault(x => x.CategoryId == _pc.CategoryId && x.DivisionId == _pc.DivisionId && x.EditionId == _pc.EditionId && x.PharmaFormId == _pc.PharmaFormId && x.ProductId == _pc.ProductId && x.AttributeId == _pc.AttributeId);
                    db.ProductContents.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._deleteproductcontents(_pc.ProductId, _pc.PharmaFormId, _pc.EditionId, _pc.DivisionId, _pc.CategoryId, _pc.AttributeId, _pc.HTMLContent, _pc.PlainContent, _pc.Content, ApplicationId, UsersId);
                }
            }

            var pp = db.ParticipantProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product);
            if (pp != null)
            {
                db.ParticipantProducts.Remove(pp);
                db.SaveChanges();

                ActivityLog.DeleteParticipanProducts(Product, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }

            var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (edp.LongCount() == 0)
            {
                EditionDivisionProducts.CategoryId = CategoryId;
                EditionDivisionProducts.DivisionId = Division;
                EditionDivisionProducts.EditionId = Edition;
                EditionDivisionProducts.PharmaFormId = PharmaFormId;
                EditionDivisionProducts.ProductId = Product;

                db.EditionDivisionProducts.Add(EditionDivisionProducts);
                db.SaveChanges();

                ActivityLog.EditionDivisionProducts(Product, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }

            var mp = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (mp.LongCount() == 0)
            {
                MentionatedProductsIns.CategoryId = CategoryId;
                MentionatedProductsIns.DivisionId = Division;
                MentionatedProductsIns.EditionId = Edition;
                MentionatedProductsIns.PharmaFormId = PharmaFormId;
                MentionatedProductsIns.ProductId = Product;

                db.MentionatedProducts.Add(MentionatedProductsIns);
                db.SaveChanges();

                ActivityLog.MentionatedProducts(Product, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMentionatedProducts(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int Division = int.Parse(DivisionId);
            int Edition = int.Parse(EditionId);
            int ProdId = int.Parse(ProductId);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);

            var mp = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId).ToList();

            if (mp.LongCount() > 0)
            {
                foreach (MentionatedProducts _mp in mp)
                {
                    var delete = db.MentionatedProducts.SingleOrDefault(x => x.CategoryId == _mp.CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId);
                    if (delete != null)
                    {
                        db.MentionatedProducts.Remove(delete);
                        db.SaveChanges();
                    }
                    ActivityLog.DeleteMentionatedProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                }
            }

            var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId).ToList();
            foreach (EditionDivisionProducts e in edp)
            {
                var delete = DEAQ.EditionDivisionProducts.SingleOrDefault(x => x.ProductId == e.ProductId && x.EditionId == e.EditionId && x.PharmaFormId == e.PharmaFormId && x.CategoryId == e.CategoryId && x.DivisionId == e.DivisionId);
                if (delete != null)
                {
                    DEAQ.EditionDivisionProducts.Remove(delete);
                    DEAQ.SaveChanges();

                    ActivityLog.DeleteDivisionProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                }

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /*      PRODUCTOS PARTICIPANTES      */

        public ActionResult InsertParticipantProducts(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProdId = int.Parse(ProductId);
            int Edition = int.Parse(EditionId);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int Division = int.Parse(DivisionId);

            var mp = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId).ToList();
            if (mp.LongCount() > 0)
            {
                foreach (MentionatedProducts _mp in mp)
                {
                    var delete = db.MentionatedProducts.SingleOrDefault(x => x.CategoryId == _mp.CategoryId && x.DivisionId == _mp.DivisionId && x.EditionId == _mp.EditionId && x.PharmaFormId == _mp.PharmaFormId && x.ProductId == _mp.ProductId);
                    if (delete != null)
                    {
                        db.MentionatedProducts.Remove(delete);
                        db.SaveChanges();

                        ActivityLog.DeleteMentionatedProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                    }
                }
            }

            var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId).ToList();
            if (edp.LongCount() == 0)
            {
                EditionDivisionProducts.EditionId = Edition;
                EditionDivisionProducts.ProductId = ProdId;
                EditionDivisionProducts.PharmaFormId = PharmaFormId;
                EditionDivisionProducts.DivisionId = Division;
                EditionDivisionProducts.CategoryId = CategoryId;

                DEAQ.EditionDivisionProducts.Add(EditionDivisionProducts);
                DEAQ.SaveChanges();

                ActivityLog.EditionDivisionProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }

            var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == ProdId).ToList();
            if (pp.LongCount() == 0)
            {
                ParticipantProducts.CategoryId = CategoryId;
                ParticipantProducts.DivisionId = Division;
                ParticipantProducts.EditionId = Edition;
                ParticipantProducts.PharmaFormId = PharmaFormId;
                ParticipantProducts.ProductId = ProdId;

                db.ParticipantProducts.Add(ParticipantProducts);
                db.SaveChanges();

                ActivityLog.ParticipanProducts(ProdId, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteParticipantProducts(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = int.Parse(AppId);
            int UsersId = int.Parse(UserId);
            ApplicationId = p.ApplicationId;
            UsersId = p.userId;

            int Division = int.Parse(DivisionId);
            int Edition = int.Parse(EditionId);
            int Product = int.Parse(ProductId);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);

            var pc = db.ProductContents.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (pc.LongCount() > 0)
            {
                foreach (ProductContents _pc in pc)
                {
                    var delete = db.ProductContents.SingleOrDefault(x => x.CategoryId == _pc.CategoryId && x.DivisionId == _pc.DivisionId && x.EditionId == _pc.EditionId && x.PharmaFormId == _pc.PharmaFormId && x.ProductId == _pc.ProductId && x.AttributeId == _pc.AttributeId);
                    db.ProductContents.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._deleteproductcontents(_pc.ProductId, _pc.PharmaFormId, _pc.EditionId, _pc.DivisionId, _pc.CategoryId, _pc.AttributeId, _pc.HTMLContent, _pc.PlainContent, _pc.Content, ApplicationId, UsersId);
                }
            }


            var pp = db.ParticipantProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product);
            if (pp != null)
            {
                db.ParticipantProducts.Remove(pp);
                db.SaveChanges();

                ActivityLog.DeleteParticipanProducts(Product, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
            }


            var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (edp.LongCount() > 0)
            {
                foreach (EditionDivisionProducts e in edp)
                {
                    var Del = DEAQ.EditionDivisionProducts.SingleOrDefault(x => x.ProductId == e.ProductId && x.EditionId == e.EditionId && x.PharmaFormId == e.PharmaFormId && x.CategoryId == e.CategoryId && x.DivisionId == e.DivisionId);
                    DEAQ.EditionDivisionProducts.Remove(Del);
                    DEAQ.SaveChanges();

                    ActivityLog.DeleteDivisionProducts(Product, PharmaFormId, Division, CategoryId, ApplicationId, UsersId, Edition);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /*          CREAR PRODUCTO          */

        [HttpPost]
        public ActionResult createNewProduct(string Product, string description, string division, string editionid, string pharmaform, string category, string Mentionated, string Participant, string RegisterSanitary, string Country)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            bool d = NewProducts.CreateProduct(Product, description, division, editionid, pharmaform,
            category, Mentionated, Participant, RegisterSanitary, ApplicationId, UsersId, Country);

            return Json(d, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GridView(string CountryId, string DivisionId)
        {

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int Division = int.Parse(DivisionId);
                var Ind = (from p in db.Products
                           join d in db.Divisions
                           on p.LaboratoryId equals d.LaboratoryId
                           where p.CountryId == d.CountryId
                           join ppf in db.ProductPharmaForms
                           on p.ProductId equals ppf.ProductId
                           join pf in db.PharmaForms
                           on ppf.PharmaFormId equals pf.PharmaFormId
                           join pc in db.ProductCategories
                           on p.ProductId equals pc.ProductId
                           where ((d.DivisionId == pc.DivisionId) || d.ParentId == pc.DivisionId)
                           && ppf.PharmaFormId == pc.PharmaFormId
                           join c in db.Categories
                           on pc.CategoryId equals c.CategoryId
                           where p.CountryId == country
                             && d.DivisionId == 302
                           orderby p.ProductName ascending
                           select new joinProductEditions { Products = p, Categories = c, Divisions = d, PharmaForms = pf, ProductCategories = pc, ProductPharmaForms = ppf });

                if (!string.IsNullOrEmpty(DivisionId))
                {
                    var division = from Div in db.Divisions
                                   where Div.DivisionId == 302
                                   select Div;
                    foreach (Divisions D in division)
                    {
                        if (D.DivisionId.Equals(302))
                        {
                            var Divisionname = D.DivisionName;
                            Ind = Ind.Where(s => s.Divisions.DivisionName.ToUpper().Contains(Divisionname));
                        }
                    }
                }
                return PartialView(Ind);
            }
            else
            {
                var Ind = (from p in db.Products
                           join d in db.Divisions
                           on p.LaboratoryId equals d.LaboratoryId
                           where p.CountryId == d.CountryId
                           join ppf in db.ProductPharmaForms
                           on p.ProductId equals ppf.ProductId
                           join pf in db.PharmaForms
                           on ppf.PharmaFormId equals pf.PharmaFormId
                           join pc in db.ProductCategories
                           on ppf.ProductId equals pc.ProductId
                           where ((d.DivisionId == pc.DivisionId) || d.ParentId == pc.DivisionId)
                           && ppf.PharmaFormId == pc.PharmaFormId
                           join c in db.Categories
                           on pc.CategoryId equals c.CategoryId
                           where p.CountryId == 0
                           && d.DivisionId == 0
                           orderby p.ProductName ascending
                           select new joinProductEditions { Products = p, Categories = c, Divisions = d, PharmaForms = pf, ProductCategories = pc, ProductPharmaForms = ppf });
                return View(Ind);
            }
        }


        /*      CAMBIOS EN LOS PRODUCTOS        */

        [HttpPost]
        public ActionResult productchanges(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int PharmaFormId = int.Parse(PharmaForm);
            int Division = int.Parse(DivisionId);
            int Edition = int.Parse(EditionId);
            int Product = int.Parse(ProductId);
            int CategoryId = int.Parse(Category);
            int ContentType = 0;

            var np = db.Newproducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (np.LongCount() == 0)
            {
                var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();

                if (pp.LongCount() > 0)
                {
                    foreach (ParticipantProducts P in pp)
                    {
                        P.ContentTypeId = 2;
                        ContentType = 2;

                        db.SaveChanges();
                    }
                    ActivityLog.EditParticipantProducts(ApplicationId, Product, Division, Edition, PharmaFormId, CategoryId, ContentType, UsersId);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteChangeProds(string ProductId, string DivisionId, string Category, string EditionId, string PharmaForm, string AppId, string UserId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = int.Parse(AppId);
            int UsersId = int.Parse(UserId);
            ApplicationId = p.ApplicationId;
            UsersId = p.userId;

            int PharmaFormId = int.Parse(PharmaForm);
            int Division = int.Parse(DivisionId);
            int Edition = int.Parse(EditionId);
            int Product = int.Parse(ProductId);
            int CategoryId = int.Parse(Category);


            var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.EditionId == Edition && x.PharmaFormId == PharmaFormId && x.ProductId == Product).ToList();
            if (pp.LongCount() > 0)
            {
                 foreach (ParticipantProducts PProds in pp)
                 {
                     PProds.ContentTypeId = null;

                     db.SaveChanges();
                 }
                 ActivityLog.EditParticipantProductsDeleteChanges(ApplicationId, Product, Division, Edition, PharmaFormId, CategoryId, 0, UsersId);
            }
            return View();
        }


        /*      REFERENCIA SIDEF EN LOS PRODUCTOS        */

        public ActionResult insertreferencesidef(string productid, string divisionid, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProdId = int.Parse(productid);
            int DivisionId = int.Parse(divisionid);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ProductId == ProdId
                      && _pp.DivisionId == DivisionId
                      && _pp.EditionId == EditionId
                      && _pp.PharmaFormId == PharmaFormId
                      && _pp.CategoryId == CategoryId
                      select _pp).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    _pp.Active = true;

                    db.SaveChanges();

                    ActivityLog._insertreferencesidef(ProdId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult deletereferencesidef(string productid, string divisionid, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProdId = int.Parse(productid);
            int DivisionId = int.Parse(divisionid);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ProductId == ProdId
                      && _pp.DivisionId == DivisionId
                      && _pp.EditionId == EditionId
                      && _pp.PharmaFormId == PharmaFormId
                      && _pp.CategoryId == CategoryId
                      select _pp).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    _pp.Active = false;

                    db.SaveChanges();

                    ActivityLog._deletereferencesidef(ProdId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                }
            }
            return View();
        }

        /*      BUSQUEDAS       */

        [HttpPost]
        public JsonResult search(string country, string term)
        {
            int CountryId = int.Parse(country);

            var json = (from PC in db.ProductCategories
                        join Prods in db.Products
                        on PC.ProductId equals Prods.ProductId
                        join Divs in db.Divisions
                        on PC.DivisionId equals Divs.DivisionId
                        join PF in db.PharmaForms
                        on PC.PharmaFormId equals PF.PharmaFormId
                        join EDPRODS in db.EditionDivisionProducts
                        on PC.PharmaFormId equals EDPRODS.PharmaFormId
                        where PC.ProductId == EDPRODS.ProductId
                        && PC.CategoryId == EDPRODS.CategoryId
                        && PC.DivisionId == EDPRODS.DivisionId
                        where Prods.CountryId == CountryId
                        && Prods.ProductName.StartsWith(term)
                        && Divs.CountryId == CountryId
                        select new JSON { PharmaForms = PF, ProductCategories = PC, Products = Prods, Divisions = Divs }).Distinct().ToList();
            foreach (JSON JS in json)
            {
                string _result = ("" + JS.Products.ProductName + "," + JS.PharmaForms.PharmaForm + "," + JS.Divisions.DivisionName + "");
                LJSON.Add(_result);

            }
            return Json(LJSON, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchprod(string ProductName)
        {
            Search search = (Search)Session["Search"];

            if (search != null)
            {
                int Coun = int.Parse(search.CId);
                int Div1 = int.Parse(search.DId);
                var Ind = (from p in db.Products
                           join d in db.Divisions
                           on p.LaboratoryId equals d.LaboratoryId
                           where p.CountryId == d.CountryId
                           join ppf in db.ProductPharmaForms
                           on p.ProductId equals ppf.ProductId
                           join pf in db.PharmaForms
                           on ppf.PharmaFormId equals pf.PharmaFormId
                           join pc in db.ProductCategories
                           on p.ProductId equals pc.ProductId
                           where ((d.DivisionId == pc.DivisionId) && (d.ParentId == null))
                           && ppf.PharmaFormId == pc.PharmaFormId
                           join c in db.Categories
                           on pc.CategoryId equals c.CategoryId
                           where p.CountryId == Coun
                           && d.DivisionId == Div1
                           orderby p.ProductName ascending
                           select new joinProductEditions { Products = p, Categories = c, Divisions = d, PharmaForms = pf, ProductCategories = pc, ProductPharmaForms = ppf /*, ParticipantProducts=PP*/ });
                if (!string.IsNullOrEmpty(ProductName))
                {
                    Ind = Ind.Where(s => s.Products.ProductName.StartsWith(ProductName));
                }

                return View(Ind);
            }
            return View();
        }
    }
}
