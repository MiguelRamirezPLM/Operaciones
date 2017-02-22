using AgroNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroNet.Controllers.Produccion
{
    public class ProductionController : Controller
    {
        //
        // GET: /Production/
        DEAQ db = new DEAQ();
        DEAQ DEAQ = new DEAQ();
        ParticipantProducts PProds = new ParticipantProducts();
        ActivityLog ActivityLog = new ActivityLog();
        public ActionResult Index(string CountryId, string EditionId, string BookId, string DivisionId)
        {
            SearchPages S = (SearchPages)Session["SearchPages"];
            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int Edition = int.Parse(EditionId);
                int Division = int.Parse(DivisionId);
                string CountId = CountryId;
                string EditId = EditionId;
                string BbookId = BookId;
                string DDivisionId = DivisionId;

                var ParticipantP = (from plm in db.plm_vwProductsByEdition
                                    where plm.EditionId == Edition
                                   && plm.CountryId == country
                                   && plm.TypeInEdition == "P"
                                   && plm.DivisionId == Division
                                    orderby plm.ProductName ascending
                                    select plm).ToList();
                if (ParticipantP.LongCount() > 0)
                {
                    ViewData["CountProds"] = 0;
                }
                else
                {
                    ViewData["CountProds"] = null;
                }
                SearchPages SearchPages = new SearchPages(CountId, EditId, BbookId, DDivisionId);
                Session["SearchPages"] = SearchPages;
                return View(ParticipantP);
            }

            else if (S != null)
            {
                int Division = int.Parse(S.DDivisionId);
                int Edition = int.Parse(S.EditId);
                int Country = int.Parse(S.CountId);
                int Book = int.Parse(S.BbookId);
                var ParticipantP = (from plm in db.plm_vwProductsByEdition
                                    where plm.EditionId == Edition
                                   && plm.CountryId == Country
                                   && plm.TypeInEdition == "P"
                                   && plm.DivisionId == Division
                                    orderby plm.ProductName ascending
                                    select plm).ToList();
                if (ParticipantP.LongCount() > 0)
                {
                    ViewData["CountProds"] = 0;
                }
                else
                {
                    ViewData["CountProds"] = null;
                }
                return View(ParticipantP);
            }
            else if (CountryId == null)
            {
                var ParticipantP = (from plm in db.plm_vwProductsByEdition
                                    where plm.EditionId == 0
                                   && plm.Page == null
                                   && plm.CountryId == 0
                                   && plm.DivisionId == 0
                                    select plm).ToList();
                return View(ParticipantP);
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

        public ActionResult Pages(string PharmaForm, string ProductId, string Category, string Page, string DivisionId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;
            SearchPages S = (SearchPages)Session["SearchPages"];
            int EditionId = int.Parse(S.EditId);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProdId = int.Parse(ProductId);
            int CategoryId = int.Parse(Category);
            int PageNum = int.Parse(Page);
            int Division = int.Parse(DivisionId);
            var ParticipantP = (from PP in db.ParticipantProducts
                                where PP.ProductId == ProdId
                                && PP.PharmaFormId == PharmaFormId
                                && PP.CategoryId == CategoryId
                                && PP.EditionId == EditionId
                                && PP.DivisionId == Division
                                && PP.Page == Page
                                select PP).ToList();
            if (ParticipantP.LongCount() > 0)
            {

            }
            else
            {
                var ParticipantPP = (from PP in db.ParticipantProducts
                                     where PP.ProductId == ProdId
                                     && PP.PharmaFormId == PharmaFormId
                                     && PP.CategoryId == CategoryId
                                     && PP.EditionId == EditionId
                                     && PP.DivisionId == Division
                                     select PP).ToList();
                foreach (ParticipantProducts PP in ParticipantPP)
                {
                    PP.Page = Page;
                }
                db.SaveChanges();
                ActivityLog.Updatepages(EditionId, PharmaFormId, ProdId, CategoryId, ApplicationId, UsersId, Page, Division);
            }
            return View();
        }

        public ActionResult UpdatePages(string PharmaForm, string ProductId, string Category, string Page, string DivisionId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;
            if (Page != "")
            {
                SearchPages Se = (SearchPages)Session["SearchPages"];
                int EditionId = int.Parse(Se.EditId);
                int PharmaFormId = int.Parse(PharmaForm);
                int ProdId = int.Parse(ProductId);
                int CategoryId = int.Parse(Category);
                int Division = int.Parse(DivisionId);
                var ParticipantP = (from PP in db.ParticipantProducts
                                    where PP.ProductId == ProdId
                                    && PP.PharmaFormId == PharmaFormId
                                    && PP.CategoryId == CategoryId
                                    && PP.EditionId == EditionId
                                    && PP.DivisionId == Division
                                    && PP.Page == Page
                                    select PP).ToList();
                if (ParticipantP.LongCount() > 0)
                {

                }
                return View();
            }
            else if (Page == "")
            {
                SearchPages Se = (SearchPages)Session["SearchPages"];
                int EditionId = int.Parse(Se.EditId);
                int PharmaFormId = int.Parse(PharmaForm);
                int ProdId = int.Parse(ProductId);
                int CategoryId = int.Parse(Category);
                int Division = int.Parse(DivisionId);
                var ParticipantP = (from PP in db.ParticipantProducts
                                    where PP.ProductId == ProdId
                                    && PP.PharmaFormId == PharmaFormId
                                    && PP.CategoryId == CategoryId
                                    && PP.EditionId == EditionId
                                    && PP.DivisionId == Division
                                    select PP).ToList();
                foreach (ParticipantProducts PP in ParticipantP)
                {
                    PP.Page = null;
                }
                db.SaveChanges();
                ActivityLog.Updatepagesn(EditionId, PharmaFormId, ProdId, CategoryId, ApplicationId, UsersId, Page, Division);
            }
            return View();
        }

        public ActionResult EditRegister(string Register, string ProductId)
        {

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;
            SearchPages Se = (SearchPages)Session["SearchPages"];
            int EditionId = int.Parse(Se.EditId);
            var Division = int.Parse(Se.CountId);

            string Registry = Register.Trim();

            int Product = int.Parse(ProductId);
            var Prod = (from Prods in db.Products
                        where Prods.ProductId == Product
                        select Prods);

            if (Prod.LongCount() > 0)
            {
                foreach (Products lb in Prod)
                {
                    lb.Register = Registry;
                }
            }
            db.SaveChanges();
            ActivityLog.UpdateRegistry(ApplicationId, Product, UsersId, Registry);
            return View();
        }

        public ActionResult searchpages(string ProductName)
        {
            SearchPages S = (SearchPages)Session["SearchPages"];
            int country = int.Parse(S.CountId);
            int Edition = int.Parse(S.EditId);
            int Division = int.Parse(S.DDivisionId);
            if (ProductName != null)
            {
                if (ProductName == string.Empty)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    int count = 0;
                    var Prods = (from plm in db.plm_vwProductsByEdition
                                 where plm.EditionId == Edition
                                && plm.CountryId == country
                                && plm.TypeInEdition == "P"
                                && plm.DivisionId == Division
                                 orderby plm.ProductName ascending
                                 select plm);

                    if (!string.IsNullOrEmpty(ProductName))
                    {
                        Prods = Prods.Where(m => m.ProductName.StartsWith(ProductName)).OrderBy(o => o.ProductName);
                        foreach (plm_vwProductsByEdition J in Prods)
                        {
                            count = count + 1;
                        }
                        ViewData["Count"] = count;
                    }
                    pagessearch pagessearch = new pagessearch(ProductName);
                    Session["pagessearch"] = pagessearch;
                    return View("Index", Prods);
                }
            }
            else if (ProductName == null)
            {
                pagessearch ps = (pagessearch)Session["pagessearch"];
                var Prods = (from plm in db.plm_vwProductsByEdition
                             where plm.EditionId == Edition
                            && plm.CountryId == country
                            && plm.TypeInEdition == "P"
                            && plm.DivisionId == Division
                             orderby plm.ProductName ascending
                             select plm);

                if (!string.IsNullOrEmpty(ps.ProductName))
                {
                    Prods = Prods.Where(m => m.ProductName.StartsWith(ps.ProductName)).OrderBy(o => o.ProductName);
                }
                return View("Index", Prods);
            }
            return View("Index");
        }

        public ActionResult showimagesdetails(int ProductId, int? division, int? edition, int? PharmaF, int? Category)
        {
            int count = 0;
            var eps = (from _eps in db.ProductImages
                       where _eps.CategoryId == Category
                       && _eps.DivisionId == division
                       && _eps.PharmaFormId == PharmaF
                       && _eps.ProductId == ProductId
                       select _eps).ToList();
            if (eps.LongCount() > 0)
            {
                String image = "";
                foreach (ProductImages _eps in eps)
                {
                    image = _eps.ProductShot;
                }

                var r = (from _eps in db.ProductImages
                         join pis in db.ProductImageSizes
                         on _eps.ProductImageId equals pis.ProductImageId
                         join ims in db.ImageSizes
                         on pis.ImageSizeId equals ims.ImageSizeId
                         where _eps.ProductId == ProductId
                         && _eps.CategoryId == Category
                         && _eps.DivisionId == division
                         && _eps.PharmaFormId == PharmaF
                         select ims).ToList();

                var length = r.LongCount();

                if (r.LongCount() > 0)
                {
                    foreach (ImageSizes _ims in r)
                    {
                        var root = Path.Combine(Server.MapPath("~/App_Data/uploads/ProductShots"), _ims.Size);
                        var path = Path.Combine(root, image);
                        if (System.IO.File.Exists(path))
                        {
                            return File(path, "image/png");
                        }
                        count = count + 1;

                        if (count == length)
                        {
                            string ProductShot = "not_available.png";
                            var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);

                            return File(rootnot, "image/png");
                        }
                    }
                }
            }
            else
            {
                string ProductShot = "not_available.png";
                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);

                return File(rootnot, "image/png");

            }
            return View();
        }

        public ActionResult images(int ProductId, int? division, int? edition, int? PharmaF, int? Category)
        {
            var prod = (from p in db.Products
                        where p.ProductId == ProductId
                        select p).ToList();
            foreach (Products _prod in prod)
            {
                ViewData["ProductNameI"] = _prod.ProductName;
                ViewData["ProductIdI"] = _prod.ProductId;
            }

            var div = (from d in db.Divisions
                       where d.DivisionId == division
                       select d).ToList();
            foreach (Divisions _div in div)
            {
                ViewData["DivisionNameI"] = _div.DivisionName;
                ViewData["DivisionIdI"] = _div.DivisionId;
            }

            var pf = (from _pf in db.PharmaForms
                      where _pf.PharmaFormId == PharmaF
                      select _pf).ToList();
            foreach (PharmaForms _pf in pf)
            {
                ViewData["PharmaFormI"] = _pf.PharmaForm;
                ViewData["PharmaFormIdI"] = _pf.PharmaFormId;
            }

            var cat = (from cats in db.Categories
                       where cats.CategoryId == Category
                       select cats).ToList();
            foreach (Categories _cat in cat)
            {
                ViewData["CategoryName"] = _cat.CategoryName;
                ViewData["CategoryId"] = _cat.CategoryId;
            }

            var edt = (from _edt in db.Editions
                       where _edt.EditionId == edition
                       select _edt).ToList();
            foreach (Editions _edt in edt)
            {
                ViewData["NumberEditionI"] = _edt.NumberEdition;
                ViewData["EditionIdI"] = _edt.EditionId;
            }


            var images = (from pi in db.ProductImages
                          join pis in db.ProductImageSizes
                          on pi.ProductImageId equals pis.ProductImageId
                          join ims in db.ImageSizes
                          on pis.ImageSizeId equals ims.ImageSizeId
                          where pi.ProductId == ProductId
                          && pi.CategoryId == Category
                          && pi.DivisionId == division
                          && pi.PharmaFormId == PharmaF
                          select new joinproductimagessizes { ProductImageSizes = pis, ImageSizes = ims, ProductImages = pi }).OrderBy(x => x.ImageSizes.Size).ToList();

            return View(images);
        }

        public ActionResult newimages(HttpPostedFileBase file, string size, string Product, string PharmaF, string Category, string Division, string Edition)
        {
            try
            {
                CountriesUsers pp = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = pp.ApplicationId;
                int UsersId = pp.userId;

                ProductImages ProductImages = new Models.ProductImages();

                int SizeId = int.Parse(size);
                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaF);
                int CategoryId = int.Parse(Category);
                int DivisionId = int.Parse(Division);
                int EditionId = int.Parse(Edition);

                String FileName = Path.GetFileName(file.FileName);
                String extention = Path.GetExtension(file.FileName);

                FileName = FileName.Replace(extention, "");

                var prods = (from p in db.Products
                             where p.ProductId == ProductId
                             select p).ToList();
                foreach (Products _prods in prods)
                {
                    FileName = _prods.ProductName.Trim().ToUpper();
                }

                FileName = ReplacesImageNames.replaces(FileName);

                FileName = FileName + extention;

                var img = (from _ims in db.ImageSizes
                           where _ims.ImageSizeId == SizeId
                           select _ims).ToList();
                foreach (ImageSizes _img in img)
                {
                    var root = Path.Combine(Server.MapPath("~/App_Data/uploads/ProductShots"), _img.Size);
                    var path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Path3"], _img.Size);
                    if (System.IO.Directory.Exists(root))
                    {
                        //path = path + "\\" + FileName;
                        root = root + "\\" + FileName;
                        //file.SaveAs(path);
                        file.SaveAs(root);
                    }
                    else
                    {
                        DirectoryInfo Dir = Directory.CreateDirectory(root);
                        root = Path.Combine(root, FileName);
                        file.SaveAs(root);

                        if (System.IO.Directory.Exists(root))
                        {
                            root = root + "\\" + FileName;
                            file.SaveAs(root);
                        }
                        //else
                        //{
                        //    DirectoryInfo Dirs = Directory.CreateDirectory(root);
                        //    root = Path.Combine(root, FileName);
                        //    file.SaveAs(root);
                        //}
                    }
                }

                var edp = (from _edp in db.ProductCategories
                           where _edp.CategoryId == CategoryId
                           && _edp.DivisionId == DivisionId
                           && _edp.PharmaFormId == PharmaFormId
                           && _edp.ProductId == ProductId
                           select _edp).ToList();
                if (edp.LongCount() > 0)
                {
                    var eps = (from _eps in db.ProductImages
                               where _eps.CategoryId == CategoryId
                               && _eps.DivisionId == DivisionId
                               && _eps.PharmaFormId == PharmaFormId
                               && _eps.ProductId == ProductId
                               && _eps.ProductShot == FileName
                               select _eps).ToList();

                    if (eps.LongCount() == 0)
                    {
                        ProductImages.Active = true;
                        ProductImages.CategoryId = CategoryId;
                        ProductImages.DivisionId = DivisionId;
                        ProductImages.PharmaFormId = PharmaFormId;
                        ProductImages.ProductId = ProductId;
                        ProductImages.ProductShot = FileName;
                        ProductImages.BaseURL = null;

                        db.ProductImages.Add(ProductImages);
                        db.SaveChanges();



                        var epss = (from _eps in db.ProductImages
                                    where _eps.CategoryId == CategoryId
                                    && _eps.DivisionId == DivisionId
                                    && _eps.PharmaFormId == PharmaFormId
                                    && _eps.ProductId == ProductId
                                    && _eps.ProductShot == FileName
                                    select _eps).ToList();
                        int ProductImageSizeId = 0;
                        foreach (ProductImages _epss in epss)
                        {
                            ProductImageSizeId = _epss.ProductImageId;

                            var pis = (from _pis in db.ProductImageSizes
                                       where _pis.ImageSizeId == SizeId
                                       && _pis.ProductImageId == _epss.ProductImageId
                                       select _pis).ToList();
                            if (pis.LongCount() == 0)
                            {
                                ProductImageSizes ProductImageSizes = new ProductImageSizes();

                                ProductImageSizes.ImageSizeId = Convert.ToByte(SizeId);
                                ProductImageSizes.ProductImageId = _epss.ProductImageId;

                                db.ProductImageSizes.Add(ProductImageSizes);
                                db.SaveChanges();

                                ActivityLog._insertProductImageSizes(ProductImageSizeId, SizeId, ApplicationId, UsersId);
                            }

                            ActivityLog._insertproductimages(ProductId, PharmaFormId, DivisionId, CategoryId, ProductImageSizeId, FileName, ApplicationId, UsersId);
                        }
                    }
                    else
                    {
                        var epss = (from _eps in db.ProductImages
                                    where _eps.CategoryId == CategoryId
                                    && _eps.DivisionId == DivisionId
                                    && _eps.PharmaFormId == PharmaFormId
                                    && _eps.ProductId == ProductId
                                    && _eps.ProductShot == FileName
                                    select _eps).ToList();
                        int ProductImageSizeId = 0;
                        foreach (ProductImages _epss in epss)
                        {
                            ProductImageSizeId = _epss.ProductImageId;

                            var pis = (from _pis in db.ProductImageSizes
                                       where _pis.ImageSizeId == SizeId
                                       && _pis.ProductImageId == _epss.ProductImageId
                                       select _pis).ToList();
                            if (pis.LongCount() == 0)
                            {
                                ProductImageSizes ProductImageSizes = new ProductImageSizes();

                                ProductImageSizes.ImageSizeId = Convert.ToByte(SizeId);
                                ProductImageSizes.ProductImageId = _epss.ProductImageId;

                                db.ProductImageSizes.Add(ProductImageSizes);
                                db.SaveChanges();

                                ActivityLog._insertProductImageSizes(ProductImageSizeId, SizeId, ApplicationId, UsersId);
                            }
                        }
                        ProductImages.ProductShot = FileName;
                        db.SaveChanges();

                        ActivityLog._updateproductimages(ProductId, PharmaFormId, DivisionId, CategoryId, ProductImageSizeId, FileName, ApplicationId, UsersId);
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                String msg = e.Message;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult showimages(string image, int? ProductImageId, int size)
        {
            var images = (from pi in db.ProductImages
                          join pis in db.ProductImageSizes
                          on pi.ProductImageId equals pis.ProductImageId
                          join ims in db.ImageSizes
                          on pis.ImageSizeId equals ims.ImageSizeId
                          where pis.ProductImageId == ProductImageId
                          && pis.ImageSizeId == size
                          select ims).ToList();

            foreach (ImageSizes _images in images)
            {
                //var root = System.Configuration.ConfigurationManager.AppSettings["Path3"];
                //root = root + _images.Size;
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads/ProductShots"), _images.Size);
                var path = Path.Combine(root, image);
                if (System.IO.File.Exists(path))
                {
                    return File(path, "image/png");
                }
                else
                {
                    string ProductShot = "not_available.png";
                    var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                    return File(rootnot, "image/png");
                }
            }
            return View();
        }
    }
}