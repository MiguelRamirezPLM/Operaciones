using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;
using System.IO;
using System.Text;

namespace GuiaNet.Controllers.Producción
{
    public class ReferencesReportController : Controller
    {
        Guia db = new Guia();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionreferenceheterogeneouscategories ind = (sessionreferenceheterogeneouscategories)Session["sessionreferenceheterogeneouscategories"];
            if (CountryId != null)
            {
                int _clientid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);

                sessionreferenceheterogeneouscategories sessionreferenceheterogeneouscategories = new Models.sessionreferenceheterogeneouscategories(_countryid, _clientid, _editionid, _book);
                Session["sessionreferenceheterogeneouscategories"] = sessionreferenceheterogeneouscategories;


                var cc = db.Database.SqlQuery<plm_vwHeterogeneousCategories>("plm_spGetEditionHeterogeneousCategoryByClients  @editionId =" + _editionid + ",  @clientId='" + _clientid + "', @type=" + 2 + "").OrderBy(x => x.Description).ToList();

                return View(cc);
            }
            if (ind != null)
            {
                int _clientid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                sessionreferenceheterogeneouscategories sessionreferenceheterogeneouscategories = new Models.sessionreferenceheterogeneouscategories(_countryid, _clientid, _editionid, _book);
                Session["sessionreferenceheterogeneouscategories"] = sessionreferenceheterogeneouscategories;


                var cc = db.Database.SqlQuery<plm_vwHeterogeneousCategories>("plm_spGetEditionHeterogeneousCategoryByClients  @editionId =" + _editionid + ",  @clientId='" + _clientid + "', @type=" + 2 + "").OrderBy(x => x.Description).ToList();

                return View(cc);
            }
            else
            {
                var cc = db.Database.SqlQuery<plm_vwHeterogeneousCategories>("plm_spGetEditionHeterogeneousCategoryByClients  @editionId =" + 0 + ",  @clientId='" + 0 + "'").OrderBy(x => x.Description).ToList();

                return View(cc);
            }
        }

        public ActionResult clientspecialities(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];
            if (CountryId != null)
            {
                int _clientid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);
                sessionreferencespecialities sessionreferencespecialities = new sessionreferencespecialities(_countryid, _clientid, _editionid, _book);
                Session["sessionreferencespecialities"] = sessionreferencespecialities;

                var plm = (from cb in db.EditionClientSpecialities
                           join c in db.Clients
                               on cb.ClientId equals c.ClientId
                           join b in db.Specialities
                               on cb.SpecialityId equals b.SpecialityId
                           join e in db.Editions
                           on cb.EditionId equals e.EditionId
                           where c.ClientId == _clientid
                           && e.EditionId == _editionid
                           orderby b.Description ascending
                           select new SpecialitiesAdvers { AdversDescription = cb.AdversDescription, Description = b.Description, Quantity = cb.Quantity, SpecialityId = b.SpecialityId }).ToList();

                List<Clients> LS = new List<Clients>();
                Clients Clients = new Models.Clients();


                var br = (from _br in db.Clients
                          join ec in db.EditionClients
                          on _br.ClientId equals ec.ClientId
                          where _br.ClientIdParent == _clientid
                          && _br.Active == true
                          orderby _br.CompanyName ascending
                          select _br).Distinct().ToList();
                if (br.LongCount() > 0)
                {
                    foreach (Clients _c in br)
                    {
                        Clients = new Clients();

                        Clients.ClientId = _c.ClientId;
                        Clients.ClientIdParent = _c.ClientIdParent;
                        Clients.CompanyName = _c.CompanyName;
                        Clients.ShortName = _c.ShortName;

                        LS.Add(Clients);
                    }

                    LS = LS.OrderBy(x => x.CompanyName).ToList();

                    sessionlistbranchreference sessionlistbranchreference = new Models.sessionlistbranchreference(LS);
                    Session["sessionlistbranchreference"] = sessionlistbranchreference;

                    return View(plm);
                }
                else
                {
                    Session["sessionlistbranchreference"] = null;
                }
                return View(plm);
            }
            if (ind != null)
            {
                int _clientid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                var plm = (from cb in db.EditionClientSpecialities
                           join c in db.Clients
                               on cb.ClientId equals c.ClientId
                           join b in db.Specialities
                               on cb.SpecialityId equals b.SpecialityId
                           join e in db.Editions
                           on cb.EditionId equals e.EditionId
                           where c.ClientId == _clientid
                           && e.EditionId == _editionid
                           orderby b.Description ascending
                           select new SpecialitiesAdvers { AdversDescription = cb.AdversDescription, Description = b.Description, Quantity = cb.Quantity, SpecialityId = b.SpecialityId }).ToList();

                List<Clients> LS = new List<Clients>();
                Clients Clients = new Models.Clients();


                var br = (from _br in db.Clients
                          join ec in db.EditionClients
                          on _br.ClientId equals ec.ClientId
                          where _br.ClientIdParent == _clientid
                          && _br.Active == true
                          orderby _br.CompanyName ascending
                          select _br).Distinct().ToList();
                if (br.LongCount() > 0)
                {
                    foreach (Clients _c in br)
                    {
                        Clients = new Clients();

                        Clients.ClientId = _c.ClientId;
                        Clients.ClientIdParent = _c.ClientIdParent;
                        Clients.CompanyName = _c.CompanyName;
                        Clients.ShortName = _c.ShortName;

                        LS.Add(Clients);
                    }

                    LS = LS.OrderBy(x => x.CompanyName).ToList();

                    sessionlistbranchreference sessionlistbranchreference = new Models.sessionlistbranchreference(LS);
                    Session["sessionlistbranchreference"] = sessionlistbranchreference;

                    return View(plm);
                }
                return View(plm);
            }
            else
            {
                var plm = (from cb in db.EditionClientSpecialities
                           join c in db.Clients
                               on cb.ClientId equals c.ClientId
                           join b in db.Specialities
                               on cb.SpecialityId equals b.SpecialityId
                           join e in db.Editions
                           on cb.EditionId equals e.EditionId
                           where c.ClientId == 0
                           && e.EditionId == 0
                           orderby b.Description ascending
                           select new SpecialitiesAdvers { AdversDescription = cb.AdversDescription, Description = b.Description, Quantity = cb.Quantity, SpecialityId = b.SpecialityId }).ToList();

                return View(plm);
            }

        }

        public ActionResult getspecialitiesbybranch(string client, string edition)
        {
            Session["sessionspbybranch"] = null;

            int ClientId = int.Parse(client);
            int EditionId = int.Parse(edition);

            var LS = (from edt in db.EditionClientSpecialities
                      join sp in db.Specialities
                          on edt.SpecialityId equals sp.SpecialityId
                      where edt.ClientId == ClientId
                      && edt.EditionId == EditionId
                      orderby sp.Description ascending
                      select new SpecialitiesAdvers { AdversDescription = edt.AdversDescription, Description = sp.Description, Quantity = edt.Quantity, SpecialityId = sp.SpecialityId }).ToList();

            sessionspbybranch sessionspbybranch = new sessionspbybranch(ClientId, EditionId, LS);
            Session["sessionspbybranch"] = sessionspbybranch;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult brandimages(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionbrandimages ind = (sessionbrandimages)Session["sessionbrandimages"];
            if (CountryId != null)
            {

                int _clientid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);

                var cbrands = (from cb in db.ClientBrands
                               join b in db.Brands
                               on cb.BrandId equals b.BrandId
                               join cbt in db.ClientBrandTypes
                               on cb.ClientBrandTypeId equals cbt.ClientBrandTypeId into bcd
                               from cbt in bcd.DefaultIfEmpty()
                               where cb.EditionId == _editionid
                               && cb.ClientId == _clientid
                               orderby b.BrandName ascending
                               select new JoinClientBrandsImage { ClientBrands = cb, Brands = b, ClientBrandTypes = cbt }).ToList();

                foreach (JoinClientBrandsImage _cbrands in cbrands)
                {
                    if (_cbrands.ClientBrandTypes == null)
                    {
                        _cbrands.ClientBrandTypes = new ClientBrandTypes();
                    }
                }

                sessionbrandimages sessionbrandimages = new sessionbrandimages(_countryid, _clientid, _editionid, _book);
                Session["sessionbrandimages"] = sessionbrandimages;

                return View(cbrands);
            }
            if (ind != null)
            {
                int _clientid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                var cbrands = (from cb in db.ClientBrands
                               join b in db.Brands
                               on cb.BrandId equals b.BrandId
                               join cbt in db.ClientBrandTypes
                               on cb.ClientBrandTypeId equals cbt.ClientBrandTypeId into bcd
                               from cbt in bcd.DefaultIfEmpty()
                               where cb.EditionId == _editionid
                               && cb.ClientId == _clientid
                               orderby b.BrandName ascending
                               select new JoinClientBrandsImage { ClientBrands = cb, Brands = b, ClientBrandTypes = cbt }).ToList();

                foreach (JoinClientBrandsImage _cbrands in cbrands)
                {
                    if (_cbrands.ClientBrandTypes == null)
                    {
                        _cbrands.ClientBrandTypes = new ClientBrandTypes();
                    }
                }

                sessionbrandimages sessionbrandimages = new sessionbrandimages(_countryid, _clientid, _editionid, _book);
                Session["sessionbrandimages"] = sessionbrandimages;

                return View(cbrands);
            }
            else
            {
                var cbrands = (from cb in db.ClientBrands
                               join b in db.Brands
                               on cb.BrandId equals b.BrandId
                               join cbt in db.ClientBrandTypes
                               on cb.ClientBrandTypeId equals cbt.ClientBrandTypeId
                               where cb.EditionId == 0
                               && cb.ClientId == 0
                               orderby b.BrandName ascending
                               select new JoinClientBrandsImage { ClientBrands = cb, Brands = b, ClientBrandTypes = cbt }).ToList();
                return View(cbrands);
            }
        }

        public ActionResult showimagesdetails(int BrandId, int ClientId, int EditionId)
        {
            var ims = (from cb in db.ClientBrands
                       join eps in db.Brands
                           on cb.BrandId equals eps.BrandId
                       join bis in db.BrandImageSizes
                           on cb.BrandId equals bis.BrandId
                       join imss in db.ImagesSizes
                       on bis.ImageSizeId equals imss.ImageSizeId
                       where cb.ClientId == ClientId
                       && cb.EditionId == EditionId
                       select imss).ToList();

            if (ims.LongCount() > 0)
            {
                string ProductShot = "";
                string Size = "";
                var images = (from img in db.BrandImageSizes
                              where img.BrandId == BrandId
                              select img).ToList();
                if (images.LongCount() > 0)
                {
                    foreach (BrandImageSizes _images in images)
                    {
                        ProductShot = _images.Logo;

                        foreach (ImagesSizes _imagesS in ims)
                        {
                            Size = _imagesS.ImageSize;

                            var root = Server.MapPath("~/App_Data/BrandsImages");
                            root = Path.Combine(root, Size);
                            var path = Path.Combine(root, ProductShot);
                            if (System.IO.File.Exists(path))
                            {
                                return File(path, "image/png");
                            }
                            //else
                            //{
                            //    string ProductShots = "not_available.png";
                            //    var rooterr = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                            //    return File(rooterr, "image/png");
                            //}
                        }
                    }
                }
                else
                {
                    string ProductShots = "not_available.png";
                    var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShots);
                    return File(root, "image/png");
                }
                return View();
            }
            else
            {
                string ProductShot = "not_available.png";
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), ProductShot);
                return File(root, "image/png");
            }
        }

        public ActionResult images(int? BrandId, int? ClientId, int? EditionId)
        {
            joinclientproductshots joinclientproductshots = new Models.joinclientproductshots();
            List<joinclientproductshots> ljoin = new List<Models.joinclientproductshots>();
            sessionbrandimages ind = (sessionbrandimages)Session["sessionbrandimages"];
            if (ind != null)
            {
                int _clienid = ind.ClId;
                int _editionid = ind.EId;

                var brands = (from p in db.Brands
                              where p.BrandId == BrandId
                              select p).ToList();
                if (brands.LongCount() > 0)
                {
                    foreach (Brands _prods in brands)
                    {
                        ViewData["BrandId"] = _prods.BrandId;
                        ViewData["BrandName"] = _prods.BrandName.ToUpper();
                    }
                }
                var ims = (from cb in db.ClientBrands
                           join eps in db.Brands
                               on cb.BrandId equals eps.BrandId
                           join bis in db.BrandImageSizes
                               on cb.BrandId equals bis.BrandId
                           join imss in db.ImagesSizes
                           on bis.ImageSizeId equals imss.ImageSizeId
                           where cb.ClientId == ClientId
                           && cb.EditionId == EditionId
                           && cb.BrandId == BrandId
                           orderby imss.ImageSize ascending
                           select new JoinBrandImagesSizes { Brands = eps, BrandImageSizes = bis, ClientBrands = cb, ImagesSizes = imss }).ToList();

                return View("images", ims);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult newimages(HttpPostedFileBase file, string size, string Brand)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            sessionbrandimages ind = (sessionbrandimages)Session["sessionbrandimages"];
            BrandImageSizes BrandImageSizes = new BrandImageSizes();
            if (ind != null)
            {
                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int BrandId = int.Parse(Brand);
                int SizeId = int.Parse(size);

                var brands = (from b in db.Brands
                              where b.BrandId == BrandId
                              //&& b.Logo != file.FileName
                              select b).ToList();

                if (brands.LongCount() > 0)
                {
                    foreach (Brands _brands in brands)
                    {
                        _brands.Logo = file.FileName;

                        db.SaveChanges();

                        ActivityLog._updatebrandslogo(BrandId, file.FileName, ApplicationId, UsersId);
                    }
                }

                var bis = (from _bis in db.BrandImageSizes
                           where _bis.BrandId == BrandId
                           && _bis.ImageSizeId == SizeId
                           select _bis).ToList();
                if (bis.LongCount() == 0)
                {
                    BrandImageSizes.BrandId = BrandId;
                    BrandImageSizes.ImageSizeId = Convert.ToByte(SizeId);
                    BrandImageSizes.Logo = file.FileName;

                    db.BrandImageSizes.Add(BrandImageSizes);
                    db.SaveChanges();

                    ActivityLog._insertBrandImageSizes(BrandId, SizeId, file.FileName, ApplicationId, UsersId);
                }
                else
                {
                    foreach (BrandImageSizes _bis in bis)
                    {
                        _bis.Logo = file.FileName;

                        db.SaveChanges();
                    }
                }

                var root = Server.MapPath("~/App_Data/BrandsImages");

                var ims = db.ImagesSizes.Where(x => x.ImageSizeId == SizeId).ToList();

                foreach (ImagesSizes _imd in ims)
                {
                    root = Path.Combine(root, _imd.ImageSize);

                    if (System.IO.Directory.Exists(root))
                    {
                        root = Path.Combine(root, file.FileName);

                        file.SaveAs(root);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        System.IO.DirectoryInfo dir = Directory.CreateDirectory(root);

                        root = Path.Combine(root, file.FileName);

                        file.SaveAs(root);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View();
        }

        public ActionResult showimages(string image, int? BrandId, int? size)
        {
            var ims = (from cb in db.ClientBrands
                       join eps in db.Brands
                           on cb.BrandId equals eps.BrandId
                       join bis in db.BrandImageSizes
                           on cb.BrandId equals bis.BrandId
                       join imss in db.ImagesSizes
                       on bis.ImageSizeId equals imss.ImageSizeId
                       where imss.ImageSizeId == size
                       && cb.BrandId == BrandId
                       select imss).ToList();


            foreach (ImagesSizes _images in ims)
            {
                var root = Server.MapPath("~/App_Data/BrandsImages");
                root = Path.Combine(root, _images.ImageSize);
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
            return View();
        }

        public ActionResult deletebrandimages(int BrandId, int size)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            sessionbrandimages ind = (sessionbrandimages)Session["sessionbrandimages"];
            if (ind != null)
            {
                var bis = db.BrandImageSizes.SingleOrDefault(x => x.BrandId == BrandId && x.ImageSizeId == size);

                if (bis != null)
                {
                    db.BrandImageSizes.Remove(bis);
                    db.SaveChanges();

                    ActivityLog._deleteBrandImageSizes(BrandId, size, ApplicationId, UsersId);
                }

                return RedirectToAction("images", new { BrandId = BrandId, ClientId = ind.ClId, EditionId = ind.EId });
            }
            else
            {
                return View();
            }
        }

        public ActionResult editbrand(string BrandNames, string BrandIds, string Client, string Edition, string ExpireDescription)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int BrandId = int.Parse(BrandIds);

            byte ClientTypeId = 0;

            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == "ANUNCIANTE"
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                ClientTypeId = _ct.ClientTypeId;
            }

            if (BrandIds != null)
            {
                var brand = db.Brands.Where(x => x.BrandId == BrandId).ToList();

                foreach (Brands _brand in brand)
                {
                    if (_brand.BrandName.Trim() != BrandNames.Trim())
                    {
                        _brand.BrandName = BrandNames.Trim();

                        db.SaveChanges();

                        ActivityLog._editBrandName(BrandId, BrandNames.Trim(), ApplicationId, UsersId);
                    }
                }
            }

            var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId).ToList();

            if (cb.LongCount() > 0)
            {
                foreach (ClientBrands _cb in cb)
                {
                    if (!string.IsNullOrEmpty(ExpireDescription))
                    {
                        _cb.ExpireDescription = ExpireDescription.Trim();

                        db.SaveChanges();

                        ActivityLog._updateClientBrands(_cb.BrandId, _cb.ClientId, _cb.ClientBrandTypeId, _cb.EditionId, _cb.ClientTypeId, ExpireDescription, ApplicationId, UsersId);
                    }
                    else
                    {
                        _cb.ExpireDescription = null;

                        db.SaveChanges();

                        ActivityLog._updateClientBrands(_cb.BrandId, _cb.ClientId, _cb.ClientBrandTypeId, _cb.EditionId, _cb.ClientTypeId, null, ApplicationId, UsersId);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult editclients(string CompanyName, string ClientId, string ShortName)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ClientId != null)
            {
                int Client = int.Parse(ClientId);

                var cl = db.Clients.Where(x => x.ClientId == Client).ToList();

                if (cl.LongCount() > 0)
                {
                    foreach (Clients _cl in cl)
                    {
                        if (_cl.CompanyName.Trim() != CompanyName.Trim())
                        {
                            _cl.CompanyName = CompanyName.Trim();
                        }
                        if (_cl.ShortName.Trim() != ShortName.Trim())
                        {
                            _cl.ShortName = ShortName.Trim();
                        }
                        db.SaveChanges();

                        ActivityLog._editclients(Client, CompanyName.Trim(), ShortName.Trim(), ApplicationId, UsersId);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getresults(string Values)
        {

            if (Values != "0")
            {
                sessionresultsofbrands sessionresultsofbrands = new sessionresultsofbrands(Values);
                Session["sessionresultsofbrands"] = sessionresultsofbrands;
            }
            else
            {
                Session["sessionresultsofbrands"] = null;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Advertisements(int? Id)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];
            Advertisements Advertisements = new Advertisements();
            List<Advertisements> LA = new List<Advertisements>();


            if (ind == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            int ClientId = ind.ClId;
            int EditionId = ind.EId;
            int SpecialityId = Convert.ToInt32(Id);


            SessionAdvers SessionAdvers = new Models.SessionAdvers(SpecialityId, ClientId, EditionId);
            Session["SessionAdvers"] = SessionAdvers;

            var esca = db.EditionSpecialityClientAdvers.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ClientTypeId == ClientTypeId && x.SpecialityId == SpecialityId).ToList();

            if (esca.LongCount() > 0)
            {
                foreach (EditionSpecialityClientAdvers _esca in esca)
                {
                    var a = db.Advertisements.Where(x => x.AdvertId == _esca.AdvertId).ToList();

                    foreach (Advertisements _a in a)
                    {
                        Advertisements = new Advertisements();

                        Advertisements.Active = _a.Active;
                        Advertisements.AdvertId = _a.AdvertId;
                        Advertisements.AdvertName = _a.AdvertName;
                        Advertisements.AdvertTypeId = _a.AdvertTypeId;
                        Advertisements.BaseUrl = _a.BaseUrl;
                        Advertisements.Order = _a.Order;

                        LA.Add(Advertisements);
                    }
                }
            }

            LA = LA.OrderBy(x => x.AdvertName).ToList();

            return View(LA);
        }

        public ActionResult getimageadvers(int? Id, int? Speciality)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];

            if (ind == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            int ClientId = ind.ClId;
            int EditionId = ind.EId;
            int SpecialityId = Convert.ToInt32(Speciality);
            int AdvertId = Convert.ToInt32(Id);

            int CountryId = ind.CId;

            var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            var path = Server.MapPath("~/App_Data/Advertisements/" + c[0].ID + "");


            var esca = db.EditionSpecialityClientAdvers.Where(x => x.ClientId == ClientId &&
                                                                   x.EditionId == EditionId &&
                                                                   x.ClientTypeId == ClientTypeId &&
                                                                   x.SpecialityId == SpecialityId).ToList();

            if (esca.LongCount() > 0)
            {
                var a = db.Advertisements.Where(x => x.AdvertId == AdvertId).ToList();

                if (System.IO.Directory.Exists(path))
                {
                    var root = Path.Combine(path, a[0].AdvertName);

                    return File(root, "image/png");
                }
                else
                {
                    var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                    return File(root, "image/png");
                }
            }
            else
            {
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                return File(root, "image/png");
            }
        }

        public JsonResult addnewadv(HttpPostedFileBase file, string Speciality, string Client, string Edition)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];

            EditionClientSpecialities EditionClientSpecialities = new Models.EditionClientSpecialities();
            Advertisements Advertisements = new Models.Advertisements();

            int SpecialityId = int.Parse(Speciality);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            string FileName = file.FileName;

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            var ecs = db.EditionClientSpecialities.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.SpecialityId == SpecialityId).ToList();

            if (ecs.LongCount() > 0)
            {
                operation(FileName, ClientId, EditionId, SpecialityId, ClientTypeId);

                int CountryId = ind.CId;

                var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                var path = Server.MapPath("~/App_Data/Advertisements/" + c[0].ID + "");

                var root = Path.Combine(path, FileName);

                if (System.IO.Directory.Exists(path))
                {
                    file.SaveAs(root);
                }
                else
                {
                    System.IO.DirectoryInfo dir = Directory.CreateDirectory(path);

                    file.SaveAs(root);
                }
            }
            else
            {
                EditionClientSpecialities = new EditionClientSpecialities();

                EditionClientSpecialities.ClientId = ClientId;
                EditionClientSpecialities.EditionId = EditionId;
                EditionClientSpecialities.ClientTypeId = ClientTypeId;
                EditionClientSpecialities.SpecialityId = SpecialityId;

                db.EditionClientSpecialities.Add(EditionClientSpecialities);
                db.SaveChanges();

                //ActivityLog._insertEditionClientSpecialities(ClientId, EditionId, SpecialityId, ClientTypeId, ApplicationId, UsersId);

                operation(FileName, ClientId, EditionId, SpecialityId, ClientTypeId);

                int CountryId = ind.CId;

                var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                var path = Server.MapPath("~/App_Data/Advertisements/" + c[0].ID + "");

                var root = Path.Combine(path, FileName);

                if (System.IO.Directory.Exists(path))
                {
                    file.SaveAs(root);
                }
                else
                {
                    System.IO.DirectoryInfo dir = Directory.CreateDirectory(path);

                    file.SaveAs(root);
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void operation(String FileName, int ClientId, int EditionId, int SpecialityId, byte ClientTypeId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            EditionSpecialityClientAdvers EditionSpecialityClientAdvers = new Models.EditionSpecialityClientAdvers();
            Advertisements Advertisements = new Models.Advertisements();

            var adt = db.AdvertTypes.Where(x => x.Description.ToUpper().Trim() == "Internet-Impreso".ToUpper().Trim()).ToList();

            byte AdvertTypeId = adt[0].AdvertTypeId;

            var adv = db.Advertisements.Where(x => x.AdvertName == FileName && x.AdvertTypeId == AdvertTypeId).ToList();

            if (adv.LongCount() == 0)
            {
                Advertisements = new Advertisements();

                Advertisements.Active = true;
                Advertisements.AdvertName = FileName;
                Advertisements.AdvertTypeId = AdvertTypeId;
                Advertisements.BaseUrl = "";
                Advertisements.Order = 1;

                db.Advertisements.Add(Advertisements);
                db.SaveChanges();

                var advinss = db.Advertisements.Where(x => x.AdvertName == FileName && x.AdvertTypeId == AdvertTypeId).ToList();

                int AdvertIds = advinss[0].AdvertId;

                //ActivityLog._insertadvertisements(AdvertIds, FileName, AdvertTypeId, ApplicationId, UsersId);
            }

            var advins = db.Advertisements.Where(x => x.AdvertName == FileName && x.AdvertTypeId == AdvertTypeId).ToList();

            if (advins.LongCount() > 0)
            {
                int AdvertId = advins[0].AdvertId;

                var esca = db.EditionSpecialityClientAdvers.Where(x => x.AdvertId == AdvertId &&
                                                                       x.ClientId == ClientId &&
                                                                       x.EditionId == EditionId &&
                                                                       x.ClientTypeId == ClientTypeId &&
                                                                       x.SpecialityId == SpecialityId).ToList();

                if (esca.LongCount() == 0)
                {
                    EditionSpecialityClientAdvers = new EditionSpecialityClientAdvers();

                    EditionSpecialityClientAdvers.Active = null;
                    EditionSpecialityClientAdvers.AdvertId = AdvertId;
                    EditionSpecialityClientAdvers.ClientId = ClientId;
                    EditionSpecialityClientAdvers.ClientTypeId = ClientTypeId;
                    EditionSpecialityClientAdvers.EditionId = EditionId;
                    EditionSpecialityClientAdvers.SpecialityId = SpecialityId;

                    db.EditionSpecialityClientAdvers.Add(EditionSpecialityClientAdvers);
                    db.SaveChanges();

                    // ActivityLog._insertEditionSpecialityClientAdvers(ClientId, EditionId, SpecialityId, ClientTypeId, AdvertId, ApplicationId, UsersId);
                }
            }
        }

        public JsonResult deleteadv(string Advert, string Speciality, string Client, string Edition)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int AdvertId = int.Parse(Advert);
            int SpecialityId = int.Parse(Speciality);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            var esca = db.EditionSpecialityClientAdvers.Where(x => x.AdvertId == AdvertId &&
                                                                   x.ClientId == ClientId &&
                                                                   x.EditionId == EditionId &&
                                                                   x.SpecialityId == SpecialityId &&
                                                                   x.ClientTypeId == ClientTypeId).ToList();

            if (esca.LongCount() > 0)
            {
                var delete = db.EditionSpecialityClientAdvers.SingleOrDefault(x => x.AdvertId == AdvertId &&
                                                                                   x.ClientId == ClientId &&
                                                                                   x.EditionId == EditionId &&
                                                                                   x.SpecialityId == SpecialityId &&
                                                                                   x.ClientTypeId == ClientTypeId);

                db.EditionSpecialityClientAdvers.Remove(delete);
                db.SaveChanges();

                //ActivityLog._deleteEditionSpecialityClientAdvers(ClientId, EditionId, SpecialityId, ClientTypeId, AdvertId, ApplicationId, UsersId);
            }

            var adv = db.Advertisements.Where(x => x.AdvertId == AdvertId).ToList();

            if (adv.LongCount() > 0)
            {
                var delete = db.Advertisements.SingleOrDefault(x => x.AdvertId == AdvertId);
                db.Advertisements.Remove(delete);
                db.SaveChanges();

                ActivityLog._deleteadvertisements(adv[0].AdvertId, adv[0].AdvertName, adv[0].AdvertTypeId, ApplicationId, UsersId);

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showimageadvers(int? Speciality)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];

            if (ind == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            int ClientId = ind.ClId;
            int EditionId = ind.EId;
            int SpecialityId = Convert.ToInt32(Speciality);

            int CountryId = ind.CId;

            var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            var path = Server.MapPath("~/App_Data/Advertisements/" + c[0].ID + "");


            var esca = db.EditionSpecialityClientAdvers.Where(x => x.ClientId == ClientId &&
                                                                   x.EditionId == EditionId &&
                                                                   x.ClientTypeId == ClientTypeId &&
                                                                   x.SpecialityId == SpecialityId).ToList();

            if (esca.LongCount() > 0)
            {
                foreach (EditionSpecialityClientAdvers _esca in esca)
                {
                    var a = db.Advertisements.Where(x => x.AdvertId == _esca.AdvertId).ToList();

                    if (System.IO.Directory.Exists(path))
                    {
                        var root = Path.Combine(path, a[0].AdvertName);

                        return File(root, "image/png");
                    }
                    else
                    {
                        var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                        return File(root, "image/png");
                    }
                }
                var roots = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                return File(roots, "image/png");
            }
            else
            {
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                return File(root, "image/png");
            }
        }

        public ActionResult showimageadverstr(int? Speciality)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];

            if (ind == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE".ToUpper().Trim()).ToList();

            byte ClientTypeId = ct[0].ClientTypeId;

            int ClientId = ind.ClId;
            int EditionId = ind.EId;
            int SpecialityId = Convert.ToInt32(Speciality);

            int CountryId = ind.CId;

            var c = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            var path = Server.MapPath("~/App_Data/Advertisements/" + c[0].ID + "");


            var esca = db.EditionSpecialityClientAdvers.Where(x => x.ClientId == ClientId &&
                                                                   x.EditionId == EditionId &&
                                                                   x.ClientTypeId == ClientTypeId &&
                                                                   x.SpecialityId == SpecialityId).ToList();

            if (esca.LongCount() > 0)
            {
                foreach (EditionSpecialityClientAdvers _esca in esca)
                {
                    var a = db.Advertisements.Where(x => x.AdvertId == _esca.AdvertId).ToList();

                    if (System.IO.Directory.Exists(path))
                    {
                        var root = Path.Combine(path, a[0].AdvertName);

                        // return File(root, "image/png");
                        return Json(root, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                        return File(root, "image/png");
                        //return Json(root, JsonRequestBehavior.AllowGet);
                    }
                }
                var roots = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                //return File(roots, "image/png");
                return Json(roots, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var root = Path.Combine(Server.MapPath("~/App_Data/uploads"), "not_available.png");

                //return File(root, "image/png");
                return Json(root, JsonRequestBehavior.AllowGet);
            }
        }
    }
}