using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediVet.Models;
using System.IO;

namespace MediVet.Controllers.Produccion
{
    public class ProductionController : Controller
    {
        PEV db = new PEV();
        EditionProductShots EditionProductShots = new EditionProductShots();
        EditionProductShotsImageSizes EditionProductShotsImageSizes = new EditionProductShotsImageSizes();
        ClassReplace ClassReplace = new ClassReplace();
        ActivityLog ActivityLog = new ActivityLog();

        //
        // GET: /Production/
        public ActionResult Index(string CountryId, string DivisionId, string EditionId, string BookId)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];

            if (CountryId != null)
            {
                int CId = int.Parse(CountryId);
                int DId = int.Parse(DivisionId);
                int EId = int.Parse(EditionId);
                int BId = int.Parse(BookId);

                SessionProduction SessionProduction = new SessionProduction(CId, DId, EId, BId);
                Session["SessionProduction"] = SessionProduction;

                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == DId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return View(Ind);
            }
            if (index != null)
            {
                int CId = index.CId;
                int DId = index.DId;
                int EId = index.EId;
                int BId = index.BId;

                SessionProduction SessionProduction = new SessionProduction(CId, DId, EId, BId);
                Session["SessionProduction"] = SessionProduction;

                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == EId && x.CountryId == CId && x.DivisionId == DId && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();

                return View(Ind);
            }
            else
            {
                var Ind = db.plm_vwProductsByEdition.Where(x => x.EditionId == 0 && x.CountryId == 0 && x.DivisionId == 0 && x.TypeInEdition == "P").OrderBy(x => x.ProductName).ToList();
                return View(Ind);
            }
        }

        public ActionResult _editregister(string Product, string Country, string DivisionId, string RegisterSanitary, string PharmaForm, string Category, string ProductName, string Description)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(Product);
            int CountryId = int.Parse(Country);
            int Division = int.Parse(DivisionId);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);


            var prods = db.Products.Where(x => x.ProductId == ProductId).ToList();
            if (prods.LongCount() > 0)
            {
                foreach (Products _prods in prods)
                {
                    if (_prods.ProductName.ToUpper().Trim() != ProductName.ToUpper().Trim())
                    {
                        _prods.ProductName = ProductName.Trim();

                        db.SaveChanges();

                        ActivityLog._editproductSM(ProductId, Description, ApplicationId, UsersId, ProductName);
                    }
                    if (_prods.Description != null)
                    {
                        if (_prods.Description.ToUpper().Trim() != Description.ToUpper().Trim())
                        {
                            _prods.Description = Description.Trim();

                            db.SaveChanges();

                            ActivityLog._editproductSM(ProductId, Description, ApplicationId, UsersId, ProductName);
                        }
                    }
                    else
                    {
                        if (_prods.Description != Description)
                        {
                            _prods.Description = Description.Trim();

                            db.SaveChanges();

                            ActivityLog._editproductSM(ProductId, Description, ApplicationId, UsersId, ProductName);
                        }
                    }
                }
            }

            var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == Division && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (pc.LongCount() > 0)
            {
                foreach (ProductCategories _pc in pc)
                {
                    if (_pc.RegisterSanitary != null)
                    {
                        if (!string.IsNullOrEmpty(RegisterSanitary))
                        {
                            if (_pc.RegisterSanitary.ToUpper().Trim() != RegisterSanitary.ToUpper().Trim())
                            {
                                var reg = db.ProductCategories.Where(x => x.RegisterSanitary.ToUpper().Trim() == RegisterSanitary.ToUpper().Trim()).ToList();
                                if (reg.LongCount() > 0)
                                {
                                    return Json(false, JsonRequestBehavior.AllowGet);
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
                            _pc.RegisterSanitary = null;

                            db.SaveChanges();

                            int OperationId = 2;
                            ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, null, OperationId);

                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(RegisterSanitary))
                        {
                            var reg = db.ProductCategories.Where(x => x.RegisterSanitary.ToUpper().Trim() == RegisterSanitary.ToUpper().Trim()).ToList();
                            if (reg.LongCount() == 0)
                            {
                                _pc.RegisterSanitary = RegisterSanitary.Trim();

                                db.SaveChanges();

                                int OperationId = 2;
                                ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, RegisterSanitary.Trim(), OperationId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(false, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            var PF = _pc.RegisterSanitary;

                            //_pc.RegisterSanitary = null;

                            //db.SaveChanges();

                            //int OperationId = 2;
                            //ActivityLog._addregisterSanitaryProductCategories(CategoryId, Division, PharmaFormId, ProductId, ApplicationId, UsersId, null, OperationId);

                            return Json(true, JsonRequestBehavior.AllowGet);

                            //return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult updatepages(string page, string Product, string Country, string Division, string PharmaForm, string Category, string Edition)
        {
            if (Product != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int CountryId = int.Parse(Country);
                int DivisionId = int.Parse(Division);
                int EditionId = int.Parse(Edition);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);
                int ProductId = int.Parse(Product);

                int OperationId = 2;

                var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId && x.Page != page.ToUpper().Trim()).ToList();

                if (pp.LongCount() > 0)
                {
                    foreach (ParticipantProducts _pp in pp)
                    {
                        if (_pp.Page == null)
                        {
                            if (!string.IsNullOrEmpty(page))
                            {
                                _pp.Page = page.Trim();

                                db.SaveChanges();

                                ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, page.Trim(), EditionId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                _pp.Page = null;

                                db.SaveChanges();

                                ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, null, EditionId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (_pp.Page.ToUpper().Trim() == page.ToUpper().Trim())
                            {
                                return Json(false, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(page))
                                {
                                    _pp.Page = page.Trim();

                                    db.SaveChanges();

                                    ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, page.Trim(), EditionId);

                                    return Json(true, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _pp.Page = null;

                                    db.SaveChanges();

                                    ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, null, EditionId);

                                    return Json(true, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchproduct(string ProductName)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];

            int CId = index.CId;
            int ClId = index.DId;
            int EId = index.EId;
            int BId = index.BId;

            if (!string.IsNullOrEmpty(ProductName))
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

                return RedirectToAction("Index", "Production");
            }
        }


        /*      IMÁGENES      */

        public ActionResult productshots(int? ProductId, int? DivisionId, int? EditionId, int? CategoryId, int? PharmaFormId)
        {
            if (ProductId == null)
            {
                return RedirectToAction("Logout", "Login");
            }
            var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

            foreach (Products _p in p)
            {
                ViewData["ProductNameImg"] = _p.ProductName;
                ViewData["ProductIdImg"] = _p.ProductId;
            }

            var d = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            foreach (Divisions _d in d)
            {
                ViewData["DivisionNameImg"] = _d.DivisionName;
                ViewData["DivisionIdImg"] = _d.DivisionId;
            }

            var e = db.Editions.Where(x => x.EditionId == EditionId).ToList();

            foreach (Editions _e in e)
            {
                ViewData["EditionIdImg"] = _e.EditionId;
                ViewData["NumberEditionImg"] = _e.NumberEdition;
            }

            var c = db.Categories.Where(x => x.CategoryId == CategoryId).ToList();

            foreach (Categories _c in c)
            {
                ViewData["CategoryIdImg"] = _c.CategoryId;
                ViewData["CategoryNameImg"] = _c.CategoryName;
            }

            var pf = db.PharmaceuticalForms.Where(x => x.PharmaFormId == PharmaFormId).ToList();

            foreach (PharmaceuticalForms _pf in pf)
            {
                ViewData["PharmaFormIdImg"] = _pf.PharmaFormId;
                ViewData["PharmaFormImg"] = _pf.PharmaForm;
            }

            var img = (from epsis in db.EditionProductShotsImageSizes
                       join eps in db.EditionProductShots
                           on epsis.EditionProductShotId equals eps.EditionProductShotId
                       join ims in db.ImageSizes
                           on epsis.ImageSizeId equals ims.ImageSizeId
                       where eps.CategoryId == CategoryId
                       && eps.DivisionId == DivisionId
                       && eps.EditionId == EditionId
                       && eps.PharmaFormId == PharmaFormId
                       && eps.ProductId == ProductId
                       select new JoinProductShots { EditionProductShots = eps, EditionProductShotsImageSizes = epsis, ImageSizes = ims }).ToList();

            return View(img);
        }

        public ActionResult newimages(HttpPostedFileBase file, string size, string Product, string DivisionIdImg, string PharmaFormIdImg, string CategoryIdImg, string EditionIdImg)
        {
            if ((Product != null) && (Product != string.Empty))
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int ProductId = int.Parse(Product);
                int DivisionId = int.Parse(DivisionIdImg);
                int PharmaFormId = int.Parse(PharmaFormIdImg);
                int CategoryId = int.Parse(CategoryIdImg);
                int EditionId = int.Parse(EditionIdImg);
                byte ImageSizeId = Convert.ToByte(size);

                string ImageName = file.FileName.ToLower();
                string extention = Path.GetExtension(ImageName);

                ImageName = ImageName.Replace(extention, "");

                ImageName = ClassReplace.replacestring(ImageName);

                ImageName = ImageName + extention;

                var eps = db.EditionProductShots.Where(x => x.CategoryId == CategoryId &&
                                                       x.PharmaFormId == PharmaFormId &&
                                                       x.DivisionId == DivisionId &&
                                                       x.EditionId == EditionId &&
                                                       x.ProductId == ProductId).ToList();
                if (eps.LongCount() == 0)
                {
                    EditionProductShots.Active = true;
                    EditionProductShots.BaseUrl = "img\\";
                    EditionProductShots.CategoryId = CategoryId;
                    EditionProductShots.DivisionId = DivisionId;
                    EditionProductShots.EditionId = EditionId;
                    EditionProductShots.PharmaFormId = PharmaFormId;
                    EditionProductShots.ProductId = ProductId;
                    EditionProductShots.ProductShot = ImageName;
                    EditionProductShots.PSTypeId = 1;

                    db.EditionProductShots.Add(EditionProductShots);
                    db.SaveChanges();


                    var eps2 = db.EditionProductShots.Where(x => x.CategoryId == CategoryId &&
                                                       x.PharmaFormId == PharmaFormId &&
                                                       x.DivisionId == DivisionId &&
                                                       x.EditionId == EditionId &&
                                                       x.ProductId == ProductId).ToList();
                    foreach (EditionProductShots _eps2 in eps2)
                    {
                        ActivityLog._editionproductshots(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, 1, ImageName, EditionId, _eps2.EditionProductShotId);
                    }
                }
                else
                {
                    foreach (EditionProductShots _eps in eps)
                    {
                        _eps.ProductShot = ImageName;

                        db.SaveChanges();

                        ActivityLog._editimagename(_eps.EditionProductShotId, ImageName, ApplicationId, UsersId, ProductId, DivisionId, EditionId, PharmaFormId, CategoryId);
                    }
                }

                var eps1 = db.EditionProductShots.Where(x => x.CategoryId == CategoryId &&
                                                       x.PharmaFormId == PharmaFormId &&
                                                       x.DivisionId == DivisionId &&
                                                       x.EditionId == EditionId &&
                                                       x.ProductId == ProductId).ToList();
                foreach (EditionProductShots _eps1 in eps1)
                {
                    var epsim = db.EditionProductShotsImageSizes.Where(x => x.EditionProductShotId == _eps1.EditionProductShotId && x.ImageSizeId == ImageSizeId).ToList();

                    if (epsim.LongCount() == 0)
                    {
                        EditionProductShotsImageSizes.EditionProductShotId = _eps1.EditionProductShotId;
                        EditionProductShotsImageSizes.ImageSizeId = ImageSizeId;

                        db.EditionProductShotsImageSizes.Add(EditionProductShotsImageSizes);
                        db.SaveChanges();

                        ActivityLog._editionproductshotsimagesizes(_eps1.EditionProductShotId, ImageSizeId, ApplicationId, UsersId);
                    }
                }

                var ims = db.ImageSizes.Where(x => x.ImageSizeId == ImageSizeId).ToList();

                foreach (ImageSizes _ims in ims)
                {
                    var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), _ims.ImageSize);

                    var path = Path.Combine(root, ImageName);

                    if (System.IO.Directory.Exists(root))
                    {
                        file.SaveAs(path);
                    }
                    else
                    {
                        DirectoryInfo Dir = Directory.CreateDirectory(root);

                        file.SaveAs(path);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showimagesdetails(int? ProductId, int? DivisionId, int? EditionId, int? CategoryId, int? PharmaFormId)
        {
            string ProductShot = "";
            var img = (from epsis in db.EditionProductShotsImageSizes
                       join eps in db.EditionProductShots
                           on epsis.EditionProductShotId equals eps.EditionProductShotId
                       join ims in db.ImageSizes
                           on epsis.ImageSizeId equals ims.ImageSizeId
                       where eps.CategoryId == CategoryId
                       && eps.DivisionId == DivisionId
                       && eps.EditionId == EditionId
                       && eps.PharmaFormId == PharmaFormId
                       && eps.ProductId == ProductId
                       select ims).ToList();

            if (img.LongCount() > 0)
            {
                var eps = db.EditionProductShots.Where(x => x.CategoryId == CategoryId &&
                                                      x.PharmaFormId == PharmaFormId &&
                                                      x.DivisionId == DivisionId &&
                                                      x.EditionId == EditionId &&
                                                      x.ProductId == ProductId).ToList();

                foreach (EditionProductShots _eps in eps)
                {
                    ProductShot = _eps.ProductShot;
                }

                foreach (ImageSizes _ims in img)
                {
                    var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), _ims.ImageSize);

                    var path = Path.Combine(root, ProductShot);

                    if (System.IO.File.Exists(path))
                    {
                        return File(path, "image/png");
                    }
                }
            }
            else
            {
                string ProductShots = "not_available.png";
                var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                return File(rooterr, "image/png");
            }
            string ProductShotss = "not_available.png";
            var rooterror = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShotss);
            return File(rooterror, "image/png");
        }

        public ActionResult showimages(string image, int EditionProductShotId, byte size)
        {
            int ImageSizeId = Convert.ToInt32(size);

            var img = (from epsis in db.EditionProductShotsImageSizes
                       join eps in db.EditionProductShots
                           on epsis.EditionProductShotId equals eps.EditionProductShotId
                       join ims in db.ImageSizes
                           on epsis.ImageSizeId equals ims.ImageSizeId
                       where epsis.EditionProductShotId == EditionProductShotId
                       && epsis.ImageSizeId == ImageSizeId
                       select ims).ToList();
            if (img.LongCount() > 0)
            {
                foreach (ImageSizes _ims in img)
                {
                    var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), _ims.ImageSize);

                    var path = Path.Combine(root, image);

                    if (System.IO.File.Exists(path))
                    {
                        return File(path, "image/png");
                    }
                    else
                    {
                        string ProductShots = "not_available.png";
                        var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                        return File(rooterr, "image/png");
                    }
                }
            }
            else
            {
                string ProductShots = "not_available.png";
                var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                return File(rooterr, "image/png");
            }
            return View();
        }


        /*      PRODUCTOS POR EDICIÓN       */

        public ActionResult ProductsByEdition(string CountryId,string EditionId, string BookId)
        {
            SessionProductsByEdition index = (SessionProductsByEdition)Session["SessionProductsByEdition"];

            if (CountryId != null)
            {
                int CId = int.Parse(CountryId);
                int EId = int.Parse(EditionId);
                int BId = int.Parse(BookId);

                SessionProductsByEdition SessionProductsByEdition = new SessionProductsByEdition(CId, EId, BId);
                Session["SessionProductsByEdition"] = SessionProductsByEdition;

                var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + EId + ", @countryid=" + CId + "").ToList();

                return View(Ind);
            }
            if (index != null)
            {
                int CId = index.CId;
                int EId = index.EId;
                int BId = index.BId;

                SessionProductsByEdition SessionProductsByEdition = new SessionProductsByEdition(CId, EId, BId);
                Session["SessionProductsByEdition"] = SessionProductsByEdition;

                var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + EId + ", @countryid=" + CId + "").ToList();

                return View(Ind);
            }
            else
            {
                var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + 0 + ", @countryid=" + 0 + "").ToList();

                return View(Ind);
            }
        }

        public ActionResult searchproductpbe(string ProductName)
        {
            SessionProductsByEdition index = (SessionProductsByEdition)Session["SessionProductsByEdition"];

            int CId = index.CId;
            int EId = index.EId;
            int BId = index.BId;

            if (!string.IsNullOrEmpty(ProductName))
            {
                int leng = ProductName.Length;

                if (leng <= 3)
                {
                    var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + EId + ", @countryid=" + CId + "").Where(x => x.ProductName.ToUpper().Trim().StartsWith(ProductName.ToUpper().Trim())).ToList();

                    return View("ProductsByEdition", Ind);
                }
                if (leng > 3)
                {
                    var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + EId + ", @countryid=" + CId + "").Where(x => x.ProductName.ToUpper().Trim().Contains(ProductName.ToUpper().Trim())).ToList();

                    return View("ProductsByEdition", Ind);
                }
                else
                {
                    var Ind = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetProductsByEdition @editionid=" + EId + ", @countryid=" + CId + "").ToList();

                    return View("ProductsByEdition", Ind);
                }
            }
            else
            {
                return RedirectToAction("ProductsByEdition", "Production");
            }
        }

        public ActionResult updatepagespbe(string page, string Product, string Country, string Division, string PharmaForm, string Category, string Edition)
        {
            if (Product != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int CountryId = int.Parse(Country);
                int DivisionId = int.Parse(Division);
                int EditionId = int.Parse(Edition);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);
                int ProductId = int.Parse(Product);

                int OperationId = 2;

                var pp = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId && x.Page != page.ToUpper().Trim()).ToList();

                if (pp.LongCount() > 0)
                {
                    foreach (ParticipantProducts _pp in pp)
                    {
                        if (_pp.Page == null)
                        {
                            if (!string.IsNullOrEmpty(page))
                            {
                                _pp.Page = page.Trim();

                                db.SaveChanges();

                                ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, page.Trim(), EditionId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                _pp.Page = null;

                                db.SaveChanges();

                                ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, null, EditionId);

                                return Json(true, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (_pp.Page.ToUpper().Trim() == page.ToUpper().Trim())
                            {
                                return Json(false, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(page))
                                {
                                    _pp.Page = page.Trim();

                                    db.SaveChanges();

                                    ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, page.Trim(), EditionId);

                                    return Json(true, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    _pp.Page = null;

                                    db.SaveChanges();

                                    ActivityLog._updatepages(CategoryId, DivisionId, PharmaFormId, ProductId, ApplicationId, UsersId, OperationId, null, EditionId);

                                    return Json(true, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}