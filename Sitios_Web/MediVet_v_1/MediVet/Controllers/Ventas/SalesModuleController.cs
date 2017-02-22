using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediVet.Models;

namespace MediVet.Controllers.Ventas
{
    public class SalesModuleController : Controller
    {
        PEV db = new PEV();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        ProductCategories ProductCategories = new ProductCategories();
        MentionatedProducts MentionatedProducts = new MentionatedProducts();
        Products Products = new Products();
        ProductPharmaForms ProductPharmaForms = new ProductPharmaForms();
        DivisionCategories DivisionCategories = new DivisionCategories();
        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();
        NewProducts NewProducts = new NewProducts();
        DivisionInformation DivisionInformation = new DivisionInformation();
        DivisionProducts DivisionProducts = new DivisionProducts();

        ActivityLog ActivityLog = new ActivityLog();
        //
        // GET: /SalesModule/
        public ActionResult Index(string CountryId, string DivisionId, string EditionId, string BookId)
        {
            _indexsession index = (_indexsession)Session["_indexsession"];
            if (CountryId != null)
            {
                int CId = int.Parse(CountryId);
                int ClId = int.Parse(DivisionId);
                int EId = int.Parse(EditionId);
                int BId = int.Parse(BookId);

                _indexsession _indexsession = new _indexsession(CId, ClId, EId, BId);
                Session["_indexsession"] = _indexsession;

                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           where P.CountryId == CId

                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                           join PF in db.PharmaceuticalForms
                           on PC.PharmaFormId equals PF.PharmaFormId

                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where D.DivisionId == ClId
                           && P.CountryId == CId
                           && P.Active == true
                           orderby P.ProductName ascending
                           select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                return View(Ind);

            }
            if (index != null)
            {
                int CId = index.CId;
                int ClId = index.DId;
                int EId = index.EId;
                int BId = index.BId;

                _indexsession _indexsession = new _indexsession(CId, ClId, EId, BId);
                Session["_indexsession"] = _indexsession;

                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           where P.CountryId == CId

                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                           join PF in db.PharmaceuticalForms
                           on PC.PharmaFormId equals PF.PharmaFormId

                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where D.DivisionId == ClId
                           && P.CountryId == CId
                           && P.Active == true
                           orderby P.ProductName ascending
                           select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                return View(Ind);
            }
            else
            {
                var Ind = (from PC in db.ProductCategories
                           join P in db.Products
                           on PC.ProductId equals P.ProductId
                           where P.CountryId == 0

                           join D in db.Divisions
                           on PC.DivisionId equals D.DivisionId
                           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                           join PF in db.PharmaceuticalForms
                           on PC.PharmaFormId equals PF.PharmaFormId

                           join C in db.Categories
                           on PC.CategoryId equals C.CategoryId
                           where D.DivisionId == 0
                           && P.CountryId == 0
                           && P.Active == true
                           orderby P.ProductName ascending
                           select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                return View(Ind);
            }
        }

        public JsonResult books(int country)
        {
            Books book = new Books();
            List<Books> lbook = new List<Books>();

            var _book = (from b in db.Books
                         select b).OrderBy(x => x.BookName).ToList();
            foreach (Books _b in _book)
            {
                book = new Books();
                book.Active = _b.Active;
                book.BookId = _b.BookId;
                book.BookName = _b.BookName;
                lbook.Add(book);
            }
            return Json(lbook, JsonRequestBehavior.AllowGet);
        }

        public JsonResult editions(string country, string bookid)
        {
            Editions edition = new Editions();
            List<Editions> leditions = new List<Editions>();

            int countryid = int.Parse(country);
            int book = int.Parse(bookid);

            var _edition = (from e in db.Editions
                            where e.CountryId == countryid
                            && e.BookId == book
                            select e).OrderBy(x => x.NumberEdition).ToList();

            foreach (Editions _e in _edition)
            {
                edition = new Editions();
                edition.Active = _e.Active;
                edition.BarCode = _e.BarCode;
                edition.BookId = _e.BookId;
                edition.CountryId = _e.CountryId;
                edition.EditionId = _e.EditionId;
                edition.ISBN = _e.ISBN;
                edition.NumberEdition = _e.NumberEdition;
                edition.ParentId = _e.ParentId;
                leditions.Add(edition);
            }
            return Json(leditions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclients(string country)
        {
            Divisions Divisions = new Divisions();
            List<Divisions> lDivs = new List<Divisions>();

            int countryid = int.Parse(country);

            var _labs = (from l in db.Laboratories
                         join d in db.Divisions
                             on l.LaboratoryId equals d.LaboratoryId
                         where d.CountryId == countryid
                         && d.Active == true
                         orderby d.DivisionName ascending
                         select d).OrderBy(x => x.DivisionName).ToList();

            foreach (Divisions _c in _labs)
            {
                Divisions = new Divisions();
                Divisions.Active = _c.Active;
                Divisions.CountryId = _c.CountryId;
                Divisions.DivisionName = _c.DivisionName;
                Divisions.ShortName = _c.ShortName;
                Divisions.LaboratoryId = _c.LaboratoryId;
                Divisions.DivisionId = _c.DivisionId;
                Divisions.ParentId = _c.ParentId;
                lDivs.Add(Divisions);
            }
            return Json(lDivs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProducts(string ProductName, string Product, string Description, string Country, string DivisionId, string RegisterSanitary, string PharmaForm, string Category)
        {
            if (Product != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);
                int Division = int.Parse(DivisionId);

                var prod = db.Products.Where(x => x.ProductId == ProductId).ToList();

                if (prod.LongCount() > 0)
                {
                    foreach (Products _prod in prod)
                    {
                        _prod.ProductName = ProductName.Trim();

                        if (string.IsNullOrEmpty(Description.Trim()))
                        {
                            _prod.Description = null;

                            Description = null;

                            ActivityLog._editproductSM(ProductId, Description, ApplicationId, UsersId, ProductName);
                        }
                        else
                        {
                            _prod.Description = Description.Trim();

                            ActivityLog._editproductSM(ProductId, Description, ApplicationId, UsersId, ProductName);
                        }

                        db.SaveChanges();
                    }

                    if (!string.IsNullOrEmpty(RegisterSanitary.Trim()))
                    {
                        var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                        foreach (ProductCategories _pc in pc)
                        {
                            if (_pc.RegisterSanitary != null)
                            {
                                if (_pc.RegisterSanitary.ToUpper().Trim() != RegisterSanitary.ToUpper().Trim())
                                {
                                    var reg = db.ProductCategories.Where(x => x.RegisterSanitary.ToUpper().Trim() == RegisterSanitary.ToUpper().Trim()).ToList();
                                    if (reg.LongCount() > 0)
                                    {
                                        return Json("existregister", JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        _pc.RegisterSanitary = RegisterSanitary.Trim();

                                        db.SaveChanges();

                                        int OperationId = 2;
                                        ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, RegisterSanitary.Trim(), OperationId);

                                        return Json(true, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            else
                            {
                                _pc.RegisterSanitary = RegisterSanitary.Trim();

                                db.SaveChanges();

                                int OperationId = 2;
                                ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, RegisterSanitary.Trim(), OperationId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    else
                    {
                        var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                        if (pc.LongCount() > 0)
                        {
                            foreach (ProductCategories _pc in pc)
                            {
                                if (string.IsNullOrEmpty(RegisterSanitary))
                                {
                                    _pc.RegisterSanitary = null;

                                    db.SaveChanges();

                                    int OperationId = 2;
                                    ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, null, OperationId);
                                }
                                else
                                {
                                    _pc.RegisterSanitary = RegisterSanitary.Trim();

                                    db.SaveChanges();

                                    int OperationId = 2;
                                    ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, RegisterSanitary.Trim(), OperationId);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertparticipantproducts(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
            if (pc.LongCount() > 0)
            {
                var mp = db.MentionatedProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

                if (mp != null)
                {
                    db.MentionatedProducts.Remove(mp);
                    db.SaveChanges();

                    ActivityLog._deletementionatedproducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                }

                var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);

                if (edp.LongCount() == 0)
                {
                    EditionDivisionProducts.CategoryId = CategoryId;
                    EditionDivisionProducts.DivisionId = DivisionId;
                    EditionDivisionProducts.EditionId = EditionId;
                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
                    EditionDivisionProducts.ProductId = ProductId;

                    db.EditionDivisionProducts.Add(EditionDivisionProducts);
                    db.SaveChanges();

                    ActivityLog._insertEditionDivisionProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                    var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

                    if (pp.LongCount() == 0)
                    {
                        ParticipantProducts.CategoryId = CategoryId;
                        ParticipantProducts.DivisionId = DivisionId;
                        ParticipantProducts.EditionId = EditionId;
                        ParticipantProducts.ProductId = ProductId;
                        ParticipantProducts.PharmaFormId = PharmaFormId;

                        db.ParticipantProducts.Add(ParticipantProducts);
                        db.SaveChanges();

                        ActivityLog._insertParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                    }

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

                    if (pp.LongCount() == 0)
                    {
                        ParticipantProducts.CategoryId = CategoryId;
                        ParticipantProducts.DivisionId = DivisionId;
                        ParticipantProducts.EditionId = EditionId;
                        ParticipantProducts.ProductId = ProductId;
                        ParticipantProducts.PharmaFormId = PharmaFormId;

                        db.ParticipantProducts.Add(ParticipantProducts);
                        db.SaveChanges();

                        ActivityLog._insertParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteparticipantproducts(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);


            var pc = db.ProductContents.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (pc.LongCount() > 0)
            {
                return Json("existcontent", JsonRequestBehavior.AllowGet);
            }

            var pp = db.ParticipantProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

            if (pp != null)
            {
                db.ParticipantProducts.Remove(pp);
                db.SaveChanges();

                ActivityLog._deleteParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                var edp = db.EditionDivisionProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

                if (edp != null)
                {
                    db.EditionDivisionProducts.Remove(edp);
                    db.SaveChanges();

                    ActivityLog._deleteEditionDivisionProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertmentionatedproducts(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pc = db.ProductContents.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId && x.EditionId == EditionId && x.DivisionId == DivisionId && x.CategoryId == CategoryId).ToList();

            if (pc.LongCount() > 0)
            {
                return Json("existcontent", JsonRequestBehavior.AllowGet);
            }

            var pp = db.ParticipantProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

            if (pp != null)
            {
                db.ParticipantProducts.Remove(pp);
                db.SaveChanges();

                ActivityLog._deleteParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
            }

            var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);

            if (edp.LongCount() == 0)
            {
                EditionDivisionProducts.CategoryId = CategoryId;
                EditionDivisionProducts.DivisionId = DivisionId;
                EditionDivisionProducts.EditionId = EditionId;
                EditionDivisionProducts.PharmaFormId = PharmaFormId;
                EditionDivisionProducts.ProductId = ProductId;

                db.EditionDivisionProducts.Add(EditionDivisionProducts);
                db.SaveChanges();

                ActivityLog._insertEditionDivisionProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);

                var mp = db.MentionatedProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

                if (mp.LongCount() == 0)
                {
                    MentionatedProducts.CategoryId = CategoryId;
                    MentionatedProducts.DivisionId = DivisionId;
                    MentionatedProducts.EditionId = EditionId;
                    MentionatedProducts.PharmaFormId = PharmaFormId;
                    MentionatedProducts.ProductId = ProductId;

                    db.MentionatedProducts.Add(MentionatedProducts);
                    db.SaveChanges();

                    ActivityLog._insertMentionatedProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var mp = db.MentionatedProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

                if (mp.LongCount() == 0)
                {
                    MentionatedProducts.CategoryId = CategoryId;
                    MentionatedProducts.DivisionId = DivisionId;
                    MentionatedProducts.EditionId = EditionId;
                    MentionatedProducts.PharmaFormId = PharmaFormId;
                    MentionatedProducts.ProductId = ProductId;

                    db.MentionatedProducts.Add(MentionatedProducts);
                    db.SaveChanges();

                    ActivityLog._insertMentionatedProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletementionatedproducts(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var mp = db.MentionatedProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

            if (mp != null)
            {
                db.MentionatedProducts.Remove(mp);
                db.SaveChanges();

                ActivityLog._deletementionatedproducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
            }

            var edp = db.EditionDivisionProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

            if (edp != null)
            {
                db.EditionDivisionProducts.Remove(edp);
                db.SaveChanges();

                ActivityLog._deleteEditionDivisionProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertproduct(string Product, string description, string division, string editionid, string pharmaform, string category, bool Mentionated, bool Participant, string RegisterSanitary)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            int ProductId = 0;

            if (!string.IsNullOrEmpty(RegisterSanitary.Trim()))
            {
                var reg = db.ProductCategories.Where(x => x.RegisterSanitary.ToUpper().Trim() == RegisterSanitary.ToUpper().Trim()).ToList();
                if (reg.LongCount() > 0)
                {
                    return Json("existregister", JsonRequestBehavior.AllowGet);
                }
            }

            var lab = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            if (lab.LongCount() > 0)
            {
                foreach (Divisions _lab in lab)
                {
                    if (string.IsNullOrEmpty(description.Trim()))
                    {
                        var prods = db.Products.Where(x => x.LaboratoryId == _lab.LaboratoryId && x.CountryId == _lab.CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == null).ToList();

                        if (prods.LongCount() == 0)
                        {
                            Products.Active = true;
                            Products.CountryId = _lab.CountryId;
                            if (string.IsNullOrEmpty(description.Trim()))
                            {
                                Products.Description = null;
                            }
                            else
                            {
                                Products.Description = description.Trim();
                            }
                            Products.LaboratoryId = _lab.LaboratoryId;
                            Products.ProductName = Product.Trim();
                            Products.IdAccess = 0;

                            db.Products.Add(Products);
                            db.SaveChanges();

                            ActivityLog._insertProduct(ProductId, description, _lab.CountryId, Product, _lab.LaboratoryId, ApplicationId, UsersId);
                        }

                    }
                    else
                    {
                        var prods = db.Products.Where(x => x.LaboratoryId == _lab.LaboratoryId && x.CountryId == _lab.CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == description.ToUpper().Trim()).ToList();

                        if (prods.LongCount() == 0)
                        {
                            Products.Active = true;
                            Products.CountryId = _lab.CountryId;
                            if (string.IsNullOrEmpty(description.Trim()))
                            {
                                Products.Description = null;
                            }
                            else
                            {
                                Products.Description = description.Trim();
                            }
                            Products.LaboratoryId = _lab.LaboratoryId;
                            Products.ProductName = Product.Trim();
                            Products.IdAccess = 0;

                            db.Products.Add(Products);
                            db.SaveChanges();

                            ActivityLog._insertProduct(ProductId, description, _lab.CountryId, Product, _lab.LaboratoryId, ApplicationId, UsersId);
                        }
                    }

                    if (string.IsNullOrEmpty(description.Trim()))
                    {
                        var prod = db.Products.Where(x => x.LaboratoryId == _lab.LaboratoryId && x.CountryId == _lab.CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == null).ToList();

                        if (prod.LongCount() > 0)
                        {
                            foreach (Products _prod in prod)
                            {
                                ProductId = _prod.ProductId;
                            }
                        }
                    }
                    else
                    {
                        var prod = db.Products.Where(x => x.LaboratoryId == _lab.LaboratoryId && x.CountryId == _lab.CountryId && x.ProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.Description.ToUpper().Trim() == description.ToUpper().Trim()).ToList();

                        if (prod.LongCount() > 0)
                        {
                            foreach (Products _prod in prod)
                            {
                                ProductId = _prod.ProductId;
                            }
                        }
                    }

                    var pff = db.ProductPharmaForms.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId).ToList();

                    if (pff.LongCount() == 0)
                    {
                        ProductPharmaForms.Active = true;
                        ProductPharmaForms.PharmaFormId = PharmaFormId;
                        ProductPharmaForms.ProductId = ProductId;

                        db.ProductPharmaForms.Add(ProductPharmaForms);
                        db.SaveChanges();

                        ActivityLog._insertProductPharmaForms(PharmaFormId, ProductId, ApplicationId, UsersId);
                    }

                    var dc = db.DivisionCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId).ToList();

                    if (dc.LongCount() == 0)
                    {
                        DivisionCategories.CategoryId = CategoryId;
                        DivisionCategories.DivisionId = DivisionId;

                        db.DivisionCategories.Add(DivisionCategories);
                        db.SaveChanges();

                        ActivityLog._insertdivisioncategories(CategoryId, DivisionId, ApplicationId, UsersId);
                    }

                    var dp = db.DivisionProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId).ToList();

                    if (dp.LongCount() == 0)
                    {
                        DivisionProducts = new DivisionProducts();

                        DivisionProducts.DivisionId = DivisionId;
                        DivisionProducts.ProductId = ProductId;

                        db.DivisionProducts.Add(DivisionProducts);
                        db.SaveChanges();

                        ActivityLog._addDivisionProducts(DivisionId, ProductId, ApplicationId, UsersId, 1);
                    }

                    var pc = db.ProductCategories.Where(x => x.ProductId == ProductId && x.PharmaFormId == PharmaFormId && x.DivisionId == DivisionId && x.CategoryId == CategoryId).ToList();

                    if (pc.LongCount() == 0)
                    {
                        ProductCategories.CategoryId = CategoryId;
                        ProductCategories.DivisionId = DivisionId;
                        ProductCategories.PharmaFormId = PharmaFormId;
                        ProductCategories.ProductId = ProductId;
                        ProductCategories.RegisterSanitary = RegisterSanitary.Trim();

                        db.ProductCategories.Add(ProductCategories);
                        db.SaveChanges();

                        ActivityLog._insertProductCategories(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, RegisterSanitary.Trim());
                    }

                    var edp = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    if (edp.LongCount() == 0)
                    {
                        EditionDivisionProducts.CategoryId = CategoryId;
                        EditionDivisionProducts.DivisionId = DivisionId;
                        EditionDivisionProducts.EditionId = EditionId;
                        EditionDivisionProducts.PharmaFormId = PharmaFormId;
                        EditionDivisionProducts.ProductId = ProductId;

                        db.EditionDivisionProducts.Add(EditionDivisionProducts);
                        db.SaveChanges();

                        ActivityLog._insertEditionDivisionProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                    }

                    var np = db.NewProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    if (np.LongCount() == 0)
                    {
                        NewProducts.CategoryId = CategoryId;
                        NewProducts.DivisionId = DivisionId;
                        NewProducts.EditionId = EditionId;
                        NewProducts.PharmaFormId = PharmaFormId;
                        NewProducts.ProductId = ProductId;

                        db.NewProducts.Add(NewProducts);
                        db.SaveChanges();

                        ActivityLog._insertnewproducts(CategoryId, DivisionId, EditionId, PharmaFormId, ProductId, ApplicationId, UsersId);
                    }

                    if ((pff.LongCount() > 0) && (dc.LongCount() > 0) && (pc.LongCount() > 0) && (edp.LongCount() > 0))
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        if (Mentionated == true)
                        {
                            var pp = db.ParticipantProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

                            if (pp != null)
                            {
                                db.ParticipantProducts.Remove(pp);
                                db.SaveChanges();

                                ActivityLog._deleteParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                            }

                            MentionatedProducts.CategoryId = CategoryId;
                            MentionatedProducts.DivisionId = DivisionId;
                            MentionatedProducts.EditionId = EditionId;
                            MentionatedProducts.PharmaFormId = PharmaFormId;
                            MentionatedProducts.ProductId = ProductId;

                            db.MentionatedProducts.Add(MentionatedProducts);
                            db.SaveChanges();

                            ActivityLog._insertMentionatedProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                        }

                        if (Participant == true)
                        {
                            var mp = db.MentionatedProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId);

                            if (mp != null)
                            {
                                db.MentionatedProducts.Remove(mp);
                                db.SaveChanges();

                                ActivityLog._deletementionatedproducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                            }

                            ParticipantProducts.CategoryId = CategoryId;
                            ParticipantProducts.DivisionId = DivisionId;
                            ParticipantProducts.EditionId = EditionId;
                            ParticipantProducts.PharmaFormId = PharmaFormId;
                            ParticipantProducts.ProductId = ProductId;

                            db.ParticipantProducts.Add(ParticipantProducts);
                            db.SaveChanges();

                            ActivityLog._insertParticipantProducts(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, ApplicationId, UsersId);
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertreferencesidef(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    if (_pp.Active != true)
                    {
                        _pp.Active = true;

                        db.SaveChanges();

                        ActivityLog._referencesidef(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, true, ApplicationId, UsersId);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deletereferencesidef(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    if (_pp.Active != false)
                    {
                        _pp.Active = false;

                        db.SaveChanges();

                        ActivityLog._referencesidef(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, false, ApplicationId, UsersId);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertproductchanges(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var np = db.NewProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (np.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var mp = db.MentionatedProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (mp.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId).ToList();

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    var ct = db.ContentTypes.Where(x => x.ContentType == "Con Cambio").ToList();

                    if (ct.LongCount() > 0)
                    {
                        foreach (ContentTypes _ct in ct)
                        {
                            _pp.ContentTypeId = _ct.ContentTypeId;

                            db.SaveChanges();

                            ActivityLog._productchanges(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, _ct.ContentTypeId, ApplicationId, UsersId);
                        }
                    }
                    else
                    {
                        return Json("_errorct", JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteproductchanges(string productid, string division, string editionid, string pharmaform, string category)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(productid);
            int DivisionId = int.Parse(division);
            int EditionId = int.Parse(editionid);
            int PharmaFormId = int.Parse(pharmaform);
            int CategoryId = int.Parse(category);

            var ct = db.ContentTypes.Where(x => x.ContentType == "Con Cambio").ToList();

            if (ct.LongCount() > 0)
            {
                foreach (ContentTypes _ct in ct)
                {
                    var pp = db.ParticipantProducts.Where(x => x.ProductId == ProductId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.CategoryId == CategoryId && x.ContentTypeId == _ct.ContentTypeId).ToList();

                    if (pp.LongCount() > 0)
                    {
                        foreach (ParticipantProducts _pp in pp)
                        {
                            _pp.ContentTypeId = null;

                            db.SaveChanges();

                            ActivityLog._deleteproductchanges(ProductId, DivisionId, EditionId, PharmaFormId, CategoryId, _ct.ContentTypeId, ApplicationId, UsersId);
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchproduct(string ProductName)
        {
            _indexsession index = (_indexsession)Session["_indexsession"];

            int CId = index.CId;
            int ClId = index.DId;
            int EId = index.EId;
            int BId = index.BId;

            if (!string.IsNullOrEmpty(ProductName))
            {
                _indexsession _indexsession = new _indexsession(CId, ClId, EId, BId);
                Session["_indexsession"] = _indexsession;

                int leng = ProductName.Length;

                if (leng <= 3)
                {
                    var Ind = (from PC in db.ProductCategories
                               join P in db.Products
                               on PC.ProductId equals P.ProductId
                               where P.CountryId == CId

                               join D in db.Divisions
                               on PC.DivisionId equals D.DivisionId
                               where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                               join PF in db.PharmaceuticalForms
                               on PC.PharmaFormId equals PF.PharmaFormId

                               join C in db.Categories
                               on PC.CategoryId equals C.CategoryId
                               where D.DivisionId == ClId
                               && P.CountryId == CId
                               && P.ProductName.StartsWith(ProductName)
                               && P.Active == true
                               orderby P.ProductName ascending
                               select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                    return View("Index", Ind);
                }
                if (leng > 3)
                {
                    var Ind = (from PC in db.ProductCategories
                               join P in db.Products
                               on PC.ProductId equals P.ProductId
                               where P.CountryId == CId

                               join D in db.Divisions
                               on PC.DivisionId equals D.DivisionId
                               where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                               join PF in db.PharmaceuticalForms
                               on PC.PharmaFormId equals PF.PharmaFormId

                               join C in db.Categories
                               on PC.CategoryId equals C.CategoryId
                               where D.DivisionId == ClId
                               && P.CountryId == CId
                               && P.ProductName.Contains(ProductName)
                               && P.Active == true
                               orderby P.ProductName ascending
                               select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                    return View("Index", Ind);
                }
                else
                {
                    var Ind = (from PC in db.ProductCategories
                               join P in db.Products
                               on PC.ProductId equals P.ProductId
                               where P.CountryId == CId

                               join D in db.Divisions
                               on PC.DivisionId equals D.DivisionId
                               where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                               join PF in db.PharmaceuticalForms
                               on PC.PharmaFormId equals PF.PharmaFormId

                               join C in db.Categories
                               on PC.CategoryId equals C.CategoryId
                               where D.DivisionId == ClId
                               && P.CountryId == CId
                               && P.Active == true
                               orderby P.ProductName ascending
                               select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                    return View("Index", Ind);
                }
            }
            else
            {
                //var Ind = (from PC in db.ProductCategories
                //           join P in db.Products
                //           on PC.ProductId equals P.ProductId
                //           where P.CountryId == CId

                //           join D in db.Divisions
                //           on PC.DivisionId equals D.DivisionId
                //           where ((D.DivisionId == PC.DivisionId) && (D.ParentId == null))

                //           join PF in db.PharmaceuticalForms
                //           on PC.PharmaFormId equals PF.PharmaFormId

                //           join C in db.Categories
                //           on PC.CategoryId equals C.CategoryId
                //           where D.DivisionId == ClId
                //           && P.CountryId == CId
                //           && P.Active == true
                //           orderby P.ProductName ascending
                //           select new JoinIndexSalesModule { Categories = C, PharmaceuticalForms = PF, Products = P, ProductCategories = PC }).ToList();

                //return View(Ind);
                return RedirectToAction("Index", "SalesModule");
            }
        }

        public ActionResult deletedivisioninformation(int DivisionInformationId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var di = db.DivisionInformation.SingleOrDefault(x => x.DivisionInfId == DivisionInformationId);

            if (di != null)
            {
                db.DivisionInformation.Remove(di);
                db.SaveChanges();

                ActivityLog._deletedivisioninformation(DivisionInformationId, ApplicationId, UsersId);
            }

            return RedirectToAction("Divisions", "SalesModule");
        }

        /* PREDICTIVE*/
        public JsonResult searchpredictive(string term, string Division, string Country)
        {
            if (Division != null)
            {
                int DivisionId = int.Parse(Division);
                int CountryId = int.Parse(Country);
                int LaboratoryId = 0;
                var divs = db.Divisions.Where(x => x.DivisionId == DivisionId && x.CountryId == CountryId).ToList();

                foreach (Divisions _divs in divs)
                {
                    LaboratoryId = _divs.LaboratoryId;
                }

                var prods = db.Products.Where(x => x.LaboratoryId == LaboratoryId).OrderBy(x => x.ProductName).ToList();

                List<String> ls = new List<String>();

                for (var i = 0; i <= 2; i++)
                {
                    ls.Add("Productos");
                }

                return Json(ls, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /*---------------   DIVISIONS   ---------------*/
        public ActionResult Divisions(string CountryId, string DivisionId, string EditionId, string BookId)
        {
            _indexDivisions ind = (_indexDivisions)Session["_indexDivisions"];
            if (CountryId != null)
            {
                int CId = int.Parse(CountryId);
                int DId = int.Parse(DivisionId);
                int EId = int.Parse(EditionId);
                int BId = int.Parse(BookId);

                _indexDivisions _indexDivisions = new _indexDivisions(CId, DId, EId, BId);
                Session["_indexDivisions"] = _indexDivisions;

                var div = db.DivisionInformation.Where(x => x.DivisionId == DId && x.Active == true).ToList();

                return View(div);
            }
            if (ind != null)
            {
                int CId = ind.CId;
                int DId = ind.DId;
                int EId = ind.EId;
                int BId = ind.BId;

                var div = db.DivisionInformation.Where(x => x.DivisionId == DId && x.Active == true).ToList();

                return View(div);
            }
            else
            {
                var div = db.DivisionInformation.Where(x => x.DivisionId == 0).ToList();

                return View(div);
            }
        }

        public JsonResult editdivisioninformation(string DivisionInfId, string Address, string Suburb, string ZipCode, string City, string State, string Telephone, string Fax, string Web, string Email, string Country, string Division, string Lada)
        {
            if (DivisionInfId != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int DivisionInformationId = int.Parse(DivisionInfId);
                int DivisionId = int.Parse(Division);
                int CountryId = int.Parse(Country);
                int Count = 0;
                var di = db.DivisionInformation.Where(x => x.DivisionInfId == DivisionInformationId && x.DivisionId == DivisionId);

                if (di.LongCount() > 0)
                {
                    foreach (DivisionInformation _di in di)
                    {
                        if (_di.Address != Address.Trim())
                        {
                            if (string.IsNullOrEmpty(Address.Trim()))
                            {
                                _di.Address = null;
                            }
                            else
                            {
                                _di.Address = Address.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.Suburb != Suburb.Trim())
                        {
                            if (string.IsNullOrEmpty(Suburb.Trim()))
                            {
                                _di.Suburb = null;
                            }
                            else
                            {
                                _di.Suburb = Suburb.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.ZipCode != ZipCode.Trim())
                        {
                            if (string.IsNullOrEmpty(ZipCode.Trim()))
                            {
                                _di.ZipCode = null;
                            }
                            else
                            {
                                _di.ZipCode = ZipCode.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.City != City.Trim())
                        {
                            if (string.IsNullOrEmpty(City.Trim()))
                            {
                                _di.City = null;
                            }
                            else
                            {
                                _di.City = City.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.State != State.Trim())
                        {
                            if (string.IsNullOrEmpty(State.Trim()))
                            {
                                _di.State = null;
                            }
                            else
                            {
                                _di.State = State.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.Telephone != Telephone.Trim())
                        {
                            if (string.IsNullOrEmpty(Telephone.Trim()))
                            {
                                _di.Telephone = null;
                            }
                            else
                            {
                                _di.Telephone = Telephone.Trim();
                            }


                            Count += 1;
                        }
                        if (_di.Fax != Fax.Trim())
                        {
                            if (string.IsNullOrEmpty(Fax.Trim()))
                            {
                                _di.Fax = null;
                            }
                            else
                            {
                                _di.Fax = Fax.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.Web != Web.Trim())
                        {
                            if (string.IsNullOrEmpty(Web.Trim()))
                            {
                                _di.Web = null;
                            }
                            else
                            {
                                _di.Web = Web.Trim();
                            }

                            Count += 1;
                        }
                        if (_di.Email != Email.Trim())
                        {
                            if (string.IsNullOrEmpty(Email.Trim()))
                            {
                                _di.Email = null;
                            }
                            else
                            {
                                _di.Email = Email.Trim();
                            }

                            Count += 1;
                        }

                        if (_di.Lada != Lada.Trim())
                        {
                            if (string.IsNullOrEmpty(Lada.Trim()))
                            {
                                _di.Lada = null;
                            }
                            else
                            {
                                _di.Lada = Lada.Trim();
                            }

                            Count += 1;
                        }

                        if (Count > 0)
                        {
                            db.SaveChanges();

                            ActivityLog._editdivisioninformation(DivisionInformationId, DivisionId, Address, Suburb, ZipCode, City, State, Telephone, Fax, Web, Email, Lada, ApplicationId, UsersId);
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult createaddress(string division, string editionid, string Street, string Suburb, string CP, string City, string State, string Lada, string Telephone, string Email, string Web, string Fax)
        {
            if (division != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                var di = db.DivisionInformation.Max(x => x.DivisionInfId);

                int DivisionId = int.Parse(division);
                int EditionId = int.Parse(editionid);

                DivisionInformation.Active = true;
                DivisionInformation.Address = Street.Trim();
                DivisionInformation.City = City.Trim();
                DivisionInformation.DivisionId = DivisionId;
                DivisionInformation.Email = Email.Trim();
                DivisionInformation.Fax = Fax.Trim();
                DivisionInformation.Lada = Lada.Trim();
                DivisionInformation.State = State.Trim();
                DivisionInformation.Suburb = Suburb.Trim();
                DivisionInformation.Telephone = Telephone.Trim();
                DivisionInformation.ZipCode = CP.Trim();

                db.DivisionInformation.Add(DivisionInformation);
                db.SaveChanges();

                di = di + 1;

                ActivityLog._insertdivisioninformation(di, DivisionId, Street, Suburb, CP, City, State, Telephone, Fax, Web, Email, Lada, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}