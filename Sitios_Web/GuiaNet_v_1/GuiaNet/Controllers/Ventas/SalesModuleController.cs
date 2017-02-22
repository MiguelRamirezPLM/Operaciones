using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;
using Newtonsoft.Json;

namespace GuiaNet.Controllers.Ventas
{
    public class SalesModuleController : Controller
    {
        //
        // GET: /SalesModule/
        public Guia db = new Guia();
        createproductwithcontent createproductwithcontent = new createproductwithcontent();
        ParticipantProducts ParticipantProducts = new ParticipantProducts();
        createcategories createcategories = new createcategories();
        insertclientcategories insertclientcategories = new insertclientcategories();
        ActivityLog ActivityLog = new ActivityLog();
        ClientHeterogeneousCategories ClientHeterogeneousCategories = new ClientHeterogeneousCategories();
        EditionClientHeterogeneousCategories EditionClientHeterogeneousCategories = new EditionClientHeterogeneousCategories();
        EditionClientProductShots EditionClientProductShots = new EditionClientProductShots();
        BarCodes BarCodes = new BarCodes();
        ClientProductBarCodes ClientProductBarCodes = new ClientProductBarCodes();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            if (CountryId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                byte typeid = 0;

                var pt = (from ptypes in db.ProductTypes
                          where ptypes.Description == "Productos"
                          select ptypes).ToList();
                foreach (ProductTypes _pt in pt)
                {
                    typeid = _pt.TypeId;
                }
                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + _clientid + ", @typeid=" + typeid + ", @EditionId=" + edition + "").ToList();

                foreach (plm_vwProductsByClientRegisterandBarcode p in plm)
                {
                    count = count + 1;
                }
                if (count > 0)
                {
                    ViewData["Count"] = count;
                }
                else
                {
                    ViewData["Count"] = null;
                }
                indexsalesmodule indexsalesmodule = new indexsalesmodule(country, _clientid, edition, book, null);
                Session["indexsalesmodule"] = indexsalesmodule;
                return View(plm);
            }
            if (index != null)
            {
                int country = index.CId;
                int _clientid = index.ClId;
                int edition = index.EId;

                byte typeid = 0;

                var pt = (from ptypes in db.ProductTypes
                          where ptypes.Description == "Productos"
                          select ptypes).ToList();
                foreach (ProductTypes _pt in pt)
                {
                    typeid = _pt.TypeId;
                }

                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + _clientid + ", @typeid=" + typeid + ", @EditionId=" + edition + "").ToList();
                ViewData["Count"] = 0;
                return View(plm);
            }
            else
            {
                byte typeid = 0;

                var pt = (from ptypes in db.ProductTypes
                          where ptypes.Description == "Productos"
                          select ptypes).ToList();
                foreach (ProductTypes _pt in pt)
                {
                    typeid = _pt.TypeId;
                }


                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + 0 + ", @typeid=" + typeid + ", @EditionId=" + 0 + "").ToList();
                ViewData["Count"] = 0;
                return View(plm);
            }
        }

        public JsonResult books(string country)
        {
            Books book = new Books();
            List<Books> lbook = new List<Books>();

            var _book = (from b in db.Books
                         select b).ToList();

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

            var _edition = from e in db.Editions
                           where e.CountryId == countryid
                           && e.BookId == book
                           select e;
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
            Clients clients = new Clients();
            List<Clients> lclients = new List<Clients>();

            int countryid = int.Parse(country);
            byte clienttypeid = 0;
            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == "ANUNCIANTE"
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                clienttypeid = _ct.ClientTypeId;
            }

            var _clients = (from c in db.Clients
                            join ec in db.EditionClients
                            on c.ClientId equals ec.ClientId
                            where ec.ClientTypeId == clienttypeid
                            && c.CountryId == countryid
                            orderby c.CompanyName ascending
                            select c).Distinct().OrderBy(x => x.CompanyName).ToList();

            foreach (Clients _c in _clients)
            {
                clients = new Clients();
                clients.Active = _c.Active;
                clients.ClientId = _c.ClientId;
                clients.ClientIdParent = _c.ClientIdParent;
                clients.ClientCode = _c.ClientCode;
                clients.CompanyName = _c.CompanyName;
                clients.Image = _c.Image;
                clients.ShortName = _c.ShortName;
                clients.Description = _c.Description;
                clients.AlphabetId = _c.AlphabetId;
                clients.ANUNCIANTEID = _c.ANUNCIANTEID;
                lclients.Add(clients);
            }
            return Json(lclients, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclientspecialities(string country)
        {
            Clients clients = new Clients();
            List<Clients> lclients = new List<Clients>();

            int countryid = int.Parse(country);
            byte clienttypeid = 0;
            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == "NORMAL"
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                clienttypeid = _ct.ClientTypeId;
            }

            var _clients = (from c in db.Clients
                            join ec in db.EditionClients
                            on c.ClientId equals ec.ClientId
                            where ec.ClientTypeId == clienttypeid
                            && c.CountryId == countryid
                            orderby c.CompanyName ascending
                            select c).Distinct().OrderBy(x => x.CompanyName).Take(1000).ToList();

            foreach (Clients _c in _clients)
            {
                clients = new Clients();
                clients.Active = _c.Active;
                clients.ClientId = _c.ClientId;
                clients.ClientIdParent = _c.ClientIdParent;
                clients.ClientCode = _c.ClientCode;
                clients.CompanyName = _c.CompanyName;
                clients.Image = _c.Image;
                clients.ShortName = _c.ShortName;
                clients.Description = _c.Description;
                clients.AlphabetId = _c.AlphabetId;
                clients.ANUNCIANTEID = _c.ANUNCIANTEID;
                lclients.Add(clients);
            }
            return Json(lclients, JsonRequestBehavior.AllowGet);
        }

        public ActionResult categories(string CountryId, string ClientId, string EditionId, string BookId, string Description)
        {
            indexclasification index = (indexclasification)Session["indexclasification"];
            seachcategory seachcat = (seachcategory)Session["seachcategory"];
            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);
                bool flag = false;
                string param = string.Empty;
                indexclasification indexclasification = new indexclasification(country, _clientid, edition, book, flag, param);
                Session["indexclasification"] = indexclasification;

                flaghetcat flaghetcat = new flaghetcat(flag, Description);
                Session["flaghetcat"] = flaghetcat;

                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }
            if (Description != null)
            {
                if (Description == string.Empty)
                {
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                    return View("categories", _response);
                }
                else
                {
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + Description + "'").OrderBy(x => x.ParentId).ToList();
                    seachcategory seachcategory = new seachcategory(Description);
                    //index.flag = false;
                    Session["seachcategory"] = seachcategory;
                    return View("categories", _response);
                }
            }

            if (Description == null)
            {
                if (index != null)
                {
                    bool flag = index.flag;
                    if (flag == true)
                    {
                        //index.flag = false;
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", _response);
                    }
                    else
                    {
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", _response);
                    }
                }

                if (seachcat != null)
                {
                    var desc = seachcat.Description;
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                    return View("categories", _response);
                }
                else
                {
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                    return View("categories", _response);
                }
            }
            else
            {
                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }

        }

        public ActionResult insertreecategories(string categoryname)
        {
            bool _response = createcategories.newcategories(categoryname);

            if (_response == true)
            {
                int categoryid = 0;
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;
                var cat = (from category in db.Categories
                           where category.Description == categoryname
                           select category).ToList();
                foreach (Categories c in cat)
                {
                    categoryid = c.CategoryId;
                }
                ActivityLog.createcategory(categoryname, ApplicationId, UsersId, categoryid);
            }
            return Json(_response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult editproducts(string PName, string ProductId, string regist, string Clientid, string BarCode, string BarCodeId)
        {

            CountriesUsers pp = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = pp.ApplicationId;
            int UsersId = pp.userId;

            int ClientId = int.Parse(Clientid);
            int BarCodeIdd = 0;

            byte alphabetid = 0;
            var products = PName.Substring(0, 1);

            var alph = (from leters in db.Alphabet
                        where leters.Letter == products
                        select leters).ToList();
            foreach (Alphabet al in alph)
            {
                alphabetid = al.AlphabetId;
            }

            byte typeid = 0;

            var pt = (from ptypes in db.ProductTypes
                      where ptypes.Description == "Productos"
                      select ptypes).ToList();
            foreach (ProductTypes _pt in pt)
            {
                typeid = _pt.TypeId;
            }

            var prs = db.Products.Where(x => x.ProductName.ToUpper().Trim() == PName.ToUpper().Trim()).ToList();


            int prodid = int.Parse(ProductId);
            var prods = from product in db.Products
                        where product.ProductId == prodid
                        && product.TypeId == typeid
                        select product;
            foreach (Products p in prods)
            {
                if (prs.LongCount() > 0)
                {
                    return Json("pexist", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    p.ProductName = PName.Trim();

                    db.SaveChanges();
                }
            }

            if ((regist != string.Empty) && (regist != null))
            {
                var cp = (from clientp in db.ClientProducts
                          where clientp.ProductId == prodid
                          && clientp.ClientId == ClientId
                          select clientp).ToList();
                foreach (ClientProducts _cp in cp)
                {
                    _cp.RegisterSanitary = regist;

                    db.SaveChanges();
                }
            }

            if ((BarCodeId != string.Empty) && (BarCodeId != null))
            {
                BarCodeIdd = int.Parse(BarCodeId);

                var bc = (from barcc in db.BarCodes
                          where barcc.BarCode == BarCode.Trim()
                          select barcc).ToList();
                if (bc.LongCount() == 0)
                {
                    var bcd = (from barcc in db.BarCodes
                               where barcc.BarCodeId == BarCodeIdd
                               select barcc).ToList();
                    foreach (BarCodes _bc in bcd)
                    {
                        _bc.BarCode = BarCode;

                        db.SaveChanges();

                        ActivityLog._editbarcode(BarCodeIdd, BarCode, ApplicationId, UsersId);
                    }
                }
                else
                {
                    var BarCd = BarCode.Trim();
                    return Json(BarCd, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var bc = (from barcc in db.BarCodes
                          where barcc.BarCode == BarCode.Trim()
                          select barcc).ToList();
                if (bc.LongCount() > 0)
                {
                    string BarCd = BarCode.Trim();
                    return Json(BarCd, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if ((regist != string.Empty) && (BarCode != string.Empty))
                    {
                        BarCodes.Active = true;
                        BarCodes.BarCode = BarCode.Trim();

                        db.BarCodes.Add(BarCodes);
                        db.SaveChanges();

                        int BarCdId = 0;
                        var bcc = (from cpb in db.BarCodes
                                   where cpb.BarCode == BarCode.Trim()
                                   select cpb).ToList();
                        foreach (BarCodes _bcc in bcc)
                        {
                            BarCdId = _bcc.BarCodeId;
                        }

                        ActivityLog._insertbarcode(BarCdId, BarCode, ApplicationId, UsersId);

                        var cpbc = (from cpbcc in db.ClientProductBarCodes
                                    where cpbcc.ClientId == ClientId
                                    && cpbcc.BarCodeId == BarCdId
                                    select cpbcc).ToList();
                        if (cpbc.LongCount() == 0)
                        {
                            ClientProductBarCodes.ClientId = ClientId;
                            ClientProductBarCodes.BarCodeId = BarCdId;
                            ClientProductBarCodes.ProductId = prodid;

                            db.ClientProductBarCodes.Add(ClientProductBarCodes);
                            db.SaveChanges();

                            ActivityLog._insertClientProductBarCodes(ClientId, BarCdId, prodid, ApplicationId, UsersId);
                        }
                    }
                    if ((regist == string.Empty) && (BarCode != string.Empty))
                    {
                        BarCodes.Active = true;
                        BarCodes.BarCode = BarCode.Trim();

                        db.BarCodes.Add(BarCodes);
                        db.SaveChanges();

                        int BarCdId = 0;
                        var bcc = (from cpb in db.BarCodes
                                   where cpb.BarCode == BarCode.Trim()
                                   select cpb).ToList();
                        foreach (BarCodes _bcc in bcc)
                        {
                            BarCdId = _bcc.BarCodeId;
                        }

                        ActivityLog._insertbarcode(BarCdId, BarCode, ApplicationId, UsersId);

                        var cpbc = (from cpbcc in db.ClientProductBarCodes
                                    where cpbcc.ClientId == ClientId
                                    && cpbcc.BarCodeId == BarCdId
                                    select cpbcc).ToList();
                        if (cpbc.LongCount() == 0)
                        {
                            ClientProductBarCodes.ClientId = ClientId;
                            ClientProductBarCodes.BarCodeId = BarCdId;
                            ClientProductBarCodes.ProductId = prodid;

                            db.ClientProductBarCodes.Add(ClientProductBarCodes);
                            db.SaveChanges();

                            ActivityLog._insertClientProductBarCodes(ClientId, BarCdId, prodid, ApplicationId, UsersId);
                        }
                    }
                }
            }

            ActivityLog.editproduct(PName, prodid, ApplicationId, UsersId, alphabetid, typeid);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult createnewproduct(string ProductN, string editionid, string clientid, string registers, string barcode)
        {
            int edition = int.Parse(editionid);
            int client = int.Parse(clientid);

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ProductN.Contains(","))
            {
                String Value = String.Empty;
                String cierre = ",";
                int j, i;
                String _string = ProductN;
                i = _string.IndexOf(_string, 0);
                if (i >= 0)
                {
                    j = _string.IndexOf(cierre, i);
                    Value = _string.Substring(i, j - i);
                    ProductN = Value;
                }
            }

            string BarCode = "";
            int BarCodeId = 0;
            var bcodes = (from barc in db.BarCodes
                          where barc.BarCode == barcode.Trim()
                          select barc).ToList();
            if (bcodes.LongCount() > 0)
            {
                foreach (BarCodes _bcodes in bcodes)
                {
                    BarCode = _bcodes.BarCode;
                }
                return Json(BarCode, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if ((barcode.Trim() == string.Empty) || (barcode.Trim() == null))
                {
                    bool response = createproductwithcontent.createproduct(ProductN, edition, client, ApplicationId, UsersId, registers, BarCodeId);

                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var bc = (from barc in db.BarCodes
                              where barc.BarCode == barcode.Trim()
                              select barc).ToList();
                    if (bc.LongCount() == 0)
                    {
                        BarCodes.Active = true;
                        BarCodes.BarCode = barcode.Trim();

                        db.BarCodes.Add(BarCodes);
                        db.SaveChanges();

                        var bcode = (from barc in db.BarCodes
                                     where barc.BarCode == barcode.Trim()
                                     select barc).ToList();
                        if (bcode.LongCount() > 0)
                        {
                            foreach (BarCodes _bcodes in bcode)
                            {
                                BarCodeId = _bcodes.BarCodeId;
                            }

                            ActivityLog._insertbarcode(BarCodeId, barcode, ApplicationId, UsersId);

                            bool response = createproductwithcontent.createproduct(ProductN, edition, client, ApplicationId, UsersId, registers, BarCodeId);

                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        foreach (BarCodes _bcodes in bcodes)
                        {
                            BarCodeId = _bcodes.BarCodeId;
                        }
                        bool response = createproductwithcontent.createproduct(ProductN, edition, client, ApplicationId, UsersId, registers, BarCodeId);

                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View();
        }

        public JsonResult insertparticipantproducts(string productid, string clientid, string editionid)
        {
            int product = int.Parse(productid);
            int client = int.Parse(clientid);
            int edition = int.Parse(editionid);
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var mp = db.MentionatedProducts.Where(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product).ToList();
            if (mp.LongCount() > 0)
            {
                var delete = db.MentionatedProducts.SingleOrDefault(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product);
                db.MentionatedProducts.Remove(delete);
                db.SaveChanges();

            }

            var pp = (from partprod in db.ParticipantProducts
                      where partprod.EditionId == edition
                      && partprod.ClientId == client
                      && partprod.ProductId == product
                      select partprod).ToList();
            if (pp.LongCount() == 0)
            {
                ParticipantProducts.ClientId = client;
                ParticipantProducts.EditionId = edition;
                ParticipantProducts.FileName = null;
                ParticipantProducts.HTMLContent = null;
                ParticipantProducts.ProductId = product;
                ParticipantProducts.XMLContent = null;

                db.ParticipantProducts.Add(ParticipantProducts);
                db.SaveChanges();

                ActivityLog._insertparticipantproducts(edition, client, product, ApplicationId, UsersId);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteparticipantproducts(string productid, string clientid, string editionid)
        {
            int product = int.Parse(productid);
            int client = int.Parse(clientid);
            int edition = int.Parse(editionid);

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            var ecps = (from edc in db.EditionClientProductShots
                        where edc.ClientId == client
                        && edc.EditionId == edition
                        && edc.ProductId == product
                        select edc).ToList();
            if (ecps.LongCount() > 0)
            {
                foreach (EditionClientProductShots _ecps in ecps)
                {
                    var es = (from ecp in db.EditionProductShotSizes
                              where ecp.EditionClientProductShotId == _ecps.EditionClientProductShotId
                              select ecp).ToList();
                    if (es.LongCount() > 0)
                    {
                        foreach (EditionProductShotSizes _es in es)
                        {
                            var delete = db.EditionProductShotSizes.SingleOrDefault(x => x.EditionClientProductShotId == _ecps.EditionClientProductShotId && x.ImageSizeId == _es.ImageSizeId);
                            db.EditionProductShotSizes.Remove(delete);
                            db.SaveChanges();
                        }
                    }
                    var deletee = db.EditionClientProductShots.SingleOrDefault(x => x.EditionClientProductShotId == _ecps.EditionClientProductShotId && x.ClientId == client && x.EditionId == edition && x.ProductId == product);
                    db.EditionClientProductShots.Remove(deletee);
                    db.SaveChanges();
                }
            }

            var pc = db.ProductContents.Where(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product).ToList();
            if (pc.LongCount() > 0)
            {
                return Json("_errorpc", JsonRequestBehavior.AllowGet);
            }

            //var pp = (from partprod in db.ParticipantProducts
            //          where partprod.EditionId == edition
            //          && partprod.ClientId == client
            //          && partprod.ProductId == product
            //          select partprod).ToList();
            //if (pp.LongCount() > 0)
            //{
            //    foreach (ParticipantProducts p in pp)
            //    {
            //        var delete = db.ParticipantProducts.SingleOrDefault(x => x.ProductId == product && x.ClientId == client && x.EditionId == edition);

            //        db.ParticipantProducts.Remove(delete);
            //        db.SaveChanges();

            //        ActivityLog._deleteparticipantproducts(edition, client, product, ApplicationId, UsersId);
            //    }
            //}

            var pp = db.ParticipantProducts.Where(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product).ToList();
            if (pp.LongCount() > 0)
            {
                var ppc = db.ParticipantProducts.Where(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product && x.HTMLContent != null).ToList();
                if (ppc.LongCount() > 0)
                {
                    return Json("_existContent", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var delete = db.ParticipantProducts.SingleOrDefault(x => x.ClientId == client && x.EditionId == edition && x.ProductId == product);
                    db.ParticipantProducts.Remove(delete);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertclientcategoriess(int Id)
        {
            seachcategory seachcat = (seachcategory)Session["seachcategory"];

            indexclasification index = (indexclasification)Session["indexclasification"];
            int _clientid = index.ClId;
            int editionid = index.EId;
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            bool _response = insertclientcategories.asociatecategorybyclient(Id, _clientid, editionid, ApplicationId, UsersId);

            if (_response == true)
            {
                ActivityLog._insertclientcategories(Id, _clientid, ApplicationId, UsersId);
            }
            else
            {
                var cats = (from cat in db.Categories
                            where cat.ParentId == Id
                            select cat).ToList();
                if (cats.LongCount() > 0)
                {
                    if (seachcat != null)
                    {
                        ViewData["parent"] = "La Categoria NO puede asociarse si tiene Subcategorías";
                        var desc = seachcat.Description;
                        var response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", response);
                    }
                    else
                    {
                        ViewData["parent"] = "La Categoria NO puede asociarse si tiene Subcategorías";
                        var response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", response);
                    }

                }
                else
                {
                    if (seachcat != null)
                    {
                        ViewData["exist"] = "La Categoria ya esta Asociada al Cliente";
                        var desc = seachcat.Description;
                        var response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", response);
                    }
                    else
                    {
                        ViewData["exist"] = "La Categoria ya esta Asociada al Cliente";
                        var response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("categories", response);
                    }
                }
            }
            return RedirectToAction("categories", "SalesModule");
        }

        public ActionResult deleteclientcategories(int categoryid, int? categoryidnodo)
        {
            indexclasification index = (indexclasification)Session["indexclasification"];
            int _clientid = index.ClId;
            int _editionid = index.EId;

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;
            if (categoryidnodo != null)
            {

                var ccat = (from ccc in db.EditionClientCategories
                            where ccc.EditionId == _editionid
                            && ccc.ClientId == _clientid
                            && ccc.CategoryId == categoryidnodo
                            select ccc).ToList();
                foreach (EditionClientCategories ecc in ccat)
                {
                    var delete = db.EditionClientCategories.SingleOrDefault(x => x.CategoryId == categoryidnodo && x.ClientId == _clientid && x.EditionId == _editionid);
                    db.EditionClientCategories.Remove(delete);
                    db.SaveChanges();

                    int categoryidn = Convert.ToInt32(categoryidnodo);
                    ActivityLog._deleteditionclientcategories(_clientid, categoryidn, _editionid, ApplicationId, UsersId);
                }

                return RedirectToAction("categories", "SalesModule");
            }
            else
            {
                var ccat = (from ccc in db.EditionClientCategories
                            where ccc.EditionId == _editionid
                            && ccc.ClientId == _clientid
                            && ccc.CategoryId == categoryid
                            select ccc).ToList();
                foreach (EditionClientCategories ecc in ccat)
                {
                    var delete = db.EditionClientCategories.SingleOrDefault(x => x.CategoryId == categoryid && x.ClientId == _clientid && x.EditionId == _editionid);
                    db.EditionClientCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._deleteditionclientcategories(_clientid, categoryid, _editionid, ApplicationId, UsersId);
                }

                return RedirectToAction("categories", "SalesModule");
            }
        }

        public JsonResult searchpredictive(string term, string country)
        {
            int CountryId = int.Parse(country);
            List<string> Products = new List<string>();
            List<plm_vwProductsByClient> plm = new List<plm_vwProductsByClient>();
            var pname = (from cp in db.ClientProducts
                         join p in db.Products
                         on cp.ProductId equals p.ProductId
                         join c in db.Clients
                         on cp.ClientId equals c.ClientId
                         where c.CountryId == CountryId
                         select new
                         {
                             ProductName = p.ProductName,
                             CompanyName = c.CompanyName
                         }).ToList();

            var prod = (from prodcs in pname
                        where prodcs.ProductName.ToUpper().StartsWith(term.ToUpper())
                        select prodcs).Distinct().OrderBy(x => x.ProductName).ToList();

            foreach (var p in prod)
            {
                string _response = ("" + p.ProductName + ", " + p.CompanyName + "");
                Products.Add(_response);
            }
            return Json(Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchasociatecategories(string Description)
        {
            if (Description != null)
            {
                if (Description == string.Empty)
                {
                    indexclasification index = (indexclasification)Session["indexclasification"];
                    index.flag = false;
                    index.param = Description;
                    return RedirectToAction("categories", "SalesModule");
                }
                else
                {
                    indexclasification index = (indexclasification)Session["indexclasification"];
                    index.flag = true;
                    index.param = Description;
                    int clientid = index.ClId;
                    var cats = (from cat in db.Categories
                                join cc in db.ClientCategories
                                on cat.CategoryId equals cc.CategoryId
                                where cat.Description.StartsWith(Description)
                                && cc.ClientId == clientid
                                select cat).ToList();
                    return RedirectToAction("categories", "SalesModule");
                }
            }
            else
            {
                return RedirectToAction("categories", "SalesModule");
            }
        }

        public ActionResult searchproduct(string ProductName)
        {
            if (ProductName != null)
            {
                if (ProductName == string.Empty)
                {
                    return RedirectToAction("Index", "SalesModule");
                }
                else
                {
                    int count = 0;
                    indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
                    int _clientid = index.ClId;
                    int typeid = 0;
                    var type = (from types in db.ProductTypes
                                where types.Description == "Productos"
                                select types).ToList();
                    foreach (ProductTypes t in type)
                    {
                        typeid = t.TypeId;
                    }

                    List<plm_vwProductsByClientRegisterandBarcode> lprods = new List<plm_vwProductsByClientRegisterandBarcode>();
                    plm_vwProductsByClientRegisterandBarcode plm_vwProductsByClientRegisterandBarcode = new Models.plm_vwProductsByClientRegisterandBarcode();
                    var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + _clientid + ", @typeid=" + typeid + "").ToList();

                    var result = (from pr in plm
                                  where pr.ProductName.ToUpper().StartsWith(ProductName.ToUpper())
                                  select pr).ToList();
                    foreach (plm_vwProductsByClientRegisterandBarcode _plm in result)
                    {
                        count = count + 1;
                    }
                    ViewData["Countresults"] = count;
                    return View("Index", result);
                }
            }
            return View();
        }

        public ActionResult clasificationhetcat(int Id)
        {
            indexclasification index = (indexclasification)Session["indexclasification"];
            int _clientid = index.ClId;
            int editionid = index.EId;

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            var cats = (from cat in db.HeterogeneousCategories
                        where cat.ParentId == Id
                        select cat).ToList();
            if (cats.LongCount() > 0)
            {
                ViewData["subcat"] = "La Categoría no puede asociarse si tiene Subcategorías";
                return View("categories");
            }
            else
            {
                var cc = (from clientc in db.ClientHeterogeneousCategories
                          where clientc.HeterogeneousCategoryId == Id
                          && clientc.ClientId == _clientid
                          && clientc.EditionId == editionid
                          select clientc).ToList();
                if (cc.LongCount() == 0)
                {
                    ClientHeterogeneousCategories.ClientId = _clientid;
                    ClientHeterogeneousCategories.HeterogeneousCategoryId = Id;
                    ClientHeterogeneousCategories.EditionId = editionid;

                    db.ClientHeterogeneousCategories.Add(ClientHeterogeneousCategories);
                    db.SaveChanges();

                    ActivityLog._insertclientheterogeneouscategories(Id, _clientid, ApplicationId, UsersId);
                }

                var ccat = (from ccc in db.EditionClientHeterogeneousCategories
                            where ccc.EditionId == editionid
                            && ccc.ClientId == _clientid
                            && ccc.HeterogeneousCategoryId == Id
                            select ccc).ToList();
                if (ccat.LongCount() == 0)
                {
                    EditionClientHeterogeneousCategories.HeterogeneousCategoryId = Id;
                    EditionClientHeterogeneousCategories.ClientId = _clientid;
                    EditionClientHeterogeneousCategories.EditionId = editionid;

                    db.EditionClientHeterogeneousCategories.Add(EditionClientHeterogeneousCategories);
                    db.SaveChanges();

                    ActivityLog._inserteditionclientheterogeneouscategories(_clientid, Id, editionid, ApplicationId, UsersId);

                    return RedirectToAction("categories", "SalesModule");
                }

                if ((ccat.LongCount() > 0) && (cc.LongCount() > 0))
                {
                    ViewData["errorcat"] = "La Categoría ya está Asociada al Cliente";
                    return View("categories");
                }
            }
            return View();
        }

        public ActionResult deleteeditionclientheterogeneouscategories(int? HeterogeneousCategoryId, int? HeterogeneousCategoryIdNodo)
        {
            indexclasification index = (indexclasification)Session["indexclasification"];
            int _clientid = index.ClId;
            int _editionid = index.EId;

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            if (HeterogeneousCategoryIdNodo != null)
            {
                var ech = (from echc in db.EditionClientHeterogeneousCategories
                           where echc.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo
                           && echc.ClientId == _clientid
                           && echc.EditionId == _editionid
                           select echc).ToList();
                if (ech.LongCount() > 0)
                {
                    foreach (EditionClientHeterogeneousCategories _ech in ech)
                    {
                        var delete = db.EditionClientHeterogeneousCategories.SingleOrDefault(x => x.ClientId == _clientid && x.EditionId == _editionid && x.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo);
                        db.EditionClientHeterogeneousCategories.Remove(delete);
                        db.SaveChanges();


                        int Id = Convert.ToInt32(HeterogeneousCategoryIdNodo);
                        ActivityLog._deleteditionclientheterogeneouscategories(_clientid, Id, _editionid, ApplicationId, UsersId);
                    }
                }
            }
            else
            {
                var ech = (from echc in db.EditionClientHeterogeneousCategories
                           where echc.HeterogeneousCategoryId == HeterogeneousCategoryId
                           && echc.ClientId == _clientid
                           && echc.EditionId == _editionid
                           select echc).ToList();
                if (ech.LongCount() > 0)
                {
                    foreach (EditionClientHeterogeneousCategories _ech in ech)
                    {
                        var delete = db.EditionClientHeterogeneousCategories.SingleOrDefault(x => x.ClientId == _clientid && x.EditionId == _editionid && x.HeterogeneousCategoryId == HeterogeneousCategoryId);
                        db.EditionClientHeterogeneousCategories.Remove(delete);
                        db.SaveChanges();

                        int Id = Convert.ToInt32(HeterogeneousCategoryId);
                        ActivityLog._deleteditionclientheterogeneouscategories(_clientid, Id, _editionid, ApplicationId, UsersId);
                    }
                }
            }
            return RedirectToAction("categories", "SalesModule");
        }

        public ActionResult getheterogeneouscategories(string Description)
        {
            seachheterogeneouscategories seachheterogeneouscategories = new seachheterogeneouscategories(Description);
            Session["seachheterogeneouscategories"] = seachheterogeneouscategories;
            return View("categories");
        }

        public ActionResult searchhetcatasociate(string Description)
        {
            if (Description != null)
            {

                flaghetcat flaghetcat = new flaghetcat(false, Description);
                Session["flaghetcat"] = flaghetcat;

                if (Description == string.Empty)
                {
                    flaghetcat index = (flaghetcat)Session["flaghetcat"];
                    index.flag = false;
                    index.param = Description;
                    return RedirectToAction("categories", "SalesModule");
                }
                else
                {
                    flaghetcat index = (flaghetcat)Session["flaghetcat"];
                    index.flag = true;
                    index.param = Description;

                    var cats = (from cat in db.Categories
                                join cc in db.ClientCategories
                                on cat.CategoryId equals cc.CategoryId
                                where cat.Description.StartsWith(Description)
                                //&& cc.ClientId == clientid
                                select cat).ToList();
                    return RedirectToAction("categories", "SalesModule");
                }
            }
            else
            {
                return RedirectToAction("categories", "SalesModule");
            }
        }

        public ActionResult insertreferencesidef(string ProductId, string clientid, string editionid)
        {
            int Prodid = int.Parse(ProductId);
            int ClientId = int.Parse(clientid);
            int Editionid = int.Parse(editionid);

            var pp = (from partp in db.ParticipantProducts
                      where partp.ClientId == ClientId
                      && partp.EditionId == Editionid
                      && partp.ProductId == Prodid
                      select partp).ToList();
            if (pp.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ecps = (from ecp in db.ParticipantProducts
                            where ecp.ProductId == Prodid
                            && ecp.EditionId == Editionid
                            && ecp.ClientId == ClientId
                            select ecp).ToList();
                if (ecps.LongCount() >= 0)
                {
                    try
                    {
                        foreach (ParticipantProducts _pp in ecps)
                        {
                            _pp.Active = true;

                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        var msg = e.Message;
                    }
                }
            }
            return View();
        }

        public ActionResult deletereferencesidef(string ProductId, string clientid, string editionid)
        {
            int Prodid = int.Parse(ProductId);
            int ClientId = int.Parse(clientid);
            int Editionid = int.Parse(editionid);

            var ecps = (from ecp in db.ParticipantProducts
                        where ecp.ProductId == Prodid
                        && ecp.EditionId == Editionid
                        && ecp.ClientId == ClientId
                        select ecp).ToList();
            if (ecps.LongCount() >= 0)
            {
                foreach (ParticipantProducts _pp in ecps)
                {
                    _pp.Active = false;

                    db.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult clientspecialities(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionclientspecialities ind = (sessionclientspecialities)Session["sessionclientspecialities"];
            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);
                sessionclientspecialities sessionclientspecialities = new sessionclientspecialities(Country, Client, Edition, Book);
                Session["sessionclientspecialities"] = sessionclientspecialities;

                var ece = db.Database.SqlQuery<ClientSpecialities>("plm_spGetSpecialitiesByClient @clientId=" + Client + ", @editionId=" + Edition + ", @countryId=" + Country + "").ToList();

                List<Clients> LS = new List<Clients>();
                Clients Clients = new Models.Clients();

                var br = (from _br in db.Clients
                          join ec in db.EditionClients
                          on _br.ClientId equals ec.ClientId
                          where _br.ClientIdParent == Client
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

                    sessionlistbranch sessionlistbranch = new Models.sessionlistbranch(LS);
                    Session["sessionlistbranch"] = sessionlistbranch;

                }
                else
                {
                    List<Clients> LSS = new List<Clients>();

                    sessionlistbranch sessionlistbranch = new Models.sessionlistbranch(LSS);
                    Session["sessionlistbranch"] = sessionlistbranch;
                }

                return View(ece);
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;
                sessionclientspecialities sessionclientspecialities = new sessionclientspecialities(Country, Client, Edition, Book);
                Session["sessionclientspecialities"] = sessionclientspecialities;

                var ece = db.Database.SqlQuery<ClientSpecialities>("plm_spGetSpecialitiesByClient @clientId=" + Client + ", @editionId=" + Edition + ", @countryId=" + Country + "").ToList();

                List<Clients> LS = new List<Clients>();
                Clients Clients = new Models.Clients();

                var br = (from _br in db.Clients
                          join ec in db.EditionClients
                          on _br.ClientId equals ec.ClientId
                          where _br.ClientIdParent == Client
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

                    sessionlistbranch sessionlistbranch = new Models.sessionlistbranch(LS);
                    Session["sessionlistbranch"] = sessionlistbranch;
                }
                else
                {
                    List<Clients> LSS = new List<Clients>();

                    sessionlistbranch sessionlistbranch = new Models.sessionlistbranch(LSS);
                    Session["sessionlistbranch"] = sessionlistbranch;
                }

                return View(ece);
            }

            else
            {
                var ece = db.Database.SqlQuery<ClientSpecialities>("plm_spGetSpecialitiesByClient @clientId=" + 0 + ", @editionId=" + 0 + "").ToList();

                List<Clients> LS = new List<Clients>();
                Clients Clients = new Models.Clients();

                var br = (from _br in db.Clients
                          join ec in db.EditionClients
                          on _br.ClientId equals ec.ClientId
                          where _br.ClientIdParent == 0
                          select _br).ToList();
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

                    sessionlistbranch sessionlistbranch = new Models.sessionlistbranch(LS);
                    Session["sessionlistbranch"] = sessionlistbranch;
                }
                return View(ece);
            }
        }

        public ActionResult insertclientspecialities(int SpecialityId)
        {
            sessionclientspecialities ind = (sessionclientspecialities)Session["sessionclientspecialities"];
            EditionClientSpecialities EditionClientSpecialities = new EditionClientSpecialities();
            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int Client = ind.ClId;
                int Edition = ind.EId;

                byte clienttypeid = 0;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "ANUNCIANTE"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from edc in db.EditionClients
                          where edc.ClientId == Client
                          && edc.EditionId == Edition
                          select edc).ToList();
                if (ec.LongCount() > 0)
                {

                    var ecs = (from _ecs in db.EditionClientSpecialities
                               where _ecs.ClientId == Client
                               && _ecs.SpecialityId == SpecialityId
                               && _ecs.ClientTypeId == clienttypeid
                               select _ecs).ToList();
                    if (ecs.LongCount() == 0)
                    {
                        EditionClientSpecialities.ClientId = Client;
                        EditionClientSpecialities.EditionId = Edition;
                        EditionClientSpecialities.SpecialityId = SpecialityId;
                        EditionClientSpecialities.ClientTypeId = clienttypeid;

                        db.EditionClientSpecialities.Add(EditionClientSpecialities);
                        db.SaveChanges();

                        int ClientTypeId = Convert.ToInt32(clienttypeid);

                        ActivityLog._insertEditionClientSpecialities(Client, Edition, SpecialityId, ClientTypeId, ApplicationId, UsersId);
                    }
                    else
                    {
                        foreach (EditionClientSpecialities _ecs in ecs)
                        {
                            EditionClientSpecialities.ClientId = _ecs.ClientId;
                            EditionClientSpecialities.EditionId = Edition;
                            EditionClientSpecialities.SpecialityId = _ecs.SpecialityId;
                            EditionClientSpecialities.ClientTypeId = _ecs.ClientTypeId;
                            EditionClientSpecialities.AdversDescription = _ecs.AdversDescription;
                            EditionClientSpecialities.Quantity = _ecs.Quantity;

                            db.EditionClientSpecialities.Add(EditionClientSpecialities);
                            db.SaveChanges();

                            int ClientTypeId = Convert.ToInt32(clienttypeid);

                            ActivityLog._insertEditionClientSpecialities(Client, Edition, SpecialityId, ClientTypeId, ApplicationId, UsersId);
                        }
                    }
                }
                else
                {
                    EditionClients.ClientId = Client;
                    EditionClients.EditionId = Edition;
                    EditionClients.ClientTypeId = clienttypeid;

                    db.EditionClients.Add(EditionClients);
                    db.SaveChanges();

                    ActivityLog._inserteditionclient(Client, Edition, Convert.ToInt32(clienttypeid), ApplicationId, UsersId);

                    var ecs = (from _ecs in db.EditionClientSpecialities
                               where _ecs.EditionId == Edition
                               && _ecs.ClientId == Client
                               && _ecs.SpecialityId == SpecialityId
                               select _ecs).ToList();
                    if (ecs.LongCount() == 0)
                    {
                        EditionClientSpecialities.ClientId = Client;
                        EditionClientSpecialities.EditionId = Edition;
                        EditionClientSpecialities.SpecialityId = SpecialityId;
                        EditionClientSpecialities.ClientTypeId = clienttypeid;

                        db.EditionClientSpecialities.Add(EditionClientSpecialities);
                        db.SaveChanges();

                        int ClientTypeId = Convert.ToInt32(clienttypeid);

                        ActivityLog._insertEditionClientSpecialities(Client, Edition, SpecialityId, ClientTypeId, ApplicationId, UsersId);

                        var spec = (from s in db.Specialities
                                    where s.Active == true
                                    && s.SpecialityId == SpecialityId
                                    select s).ToList();
                        if (spec.LongCount() > 0)
                        {

                        }
                    }
                }
                return RedirectToAction("clientspecialities", "SalesModule");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult deleteclientspecialities(int SpecialityId)
        {
            sessionclientspecialities ind = (sessionclientspecialities)Session["sessionclientspecialities"];
            EditionClientSpecialities EditionClientSpecialities = new EditionClientSpecialities();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int Client = ind.ClId;
                int Edition = ind.EId;


                var esca = db.EditionSpecialityClientAdvers.Where(x => x.ClientId == Client && x.EditionId == Edition && x.SpecialityId == SpecialityId).ToList();

                if (esca.LongCount() > 0)
                {
                    foreach (EditionSpecialityClientAdvers _esca in esca)
                    {
                        var delete = db.EditionSpecialityClientAdvers.SingleOrDefault(x => x.AdvertId == _esca.AdvertId && x.ClientId == _esca.ClientId && x.EditionId == _esca.EditionId && x.SpecialityId == _esca.SpecialityId && x.ClientTypeId == _esca.ClientTypeId);
                        db.EditionSpecialityClientAdvers.Remove(delete);
                        db.SaveChanges();

                        ActivityLog._deleteEditionSpecialityClientAdvers(_esca.ClientId, _esca.EditionId, _esca.SpecialityId, _esca.ClientTypeId, _esca.AdvertId, ApplicationId, UsersId);
                    }
                }

                var ecs = (from _ecs in db.EditionClientSpecialities
                           where _ecs.EditionId == Edition
                           && _ecs.ClientId == Client
                           && _ecs.SpecialityId == SpecialityId
                           select _ecs).ToList();
                if (ecs.LongCount() > 0)
                {
                    foreach (EditionClientSpecialities _ecs in ecs)
                    {
                        var delete = db.EditionClientSpecialities.SingleOrDefault(x => x.SpecialityId == _ecs.SpecialityId && x.EditionId == _ecs.EditionId && x.ClientId == _ecs.ClientId && x.ClientTypeId == _ecs.ClientTypeId);
                        db.EditionClientSpecialities.Remove(delete);
                        db.SaveChanges();

                        ActivityLog._deleteEditionClientSpecialities(_ecs.ClientId, _ecs.EditionId, _ecs.SpecialityId, _ecs.ClientTypeId, ApplicationId, UsersId);
                    }

                }
            }

            return RedirectToAction("clientspecialities", "SalesModule");
        }

        public ActionResult clientbrands(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];
            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                sessionclientbrands sessionclientbrands = new sessionclientbrands(Country, Client, Edition, Book);
                Session["sessionclientbrands"] = sessionclientbrands;

                var cb = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @clientId=" + Client + "").ToList();

                return View(cb);
            }
            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;

                sessionclientbrands sessionclientbrands = new sessionclientbrands(Country, Client, Edition, Book);
                Session["sessionclientbrands"] = sessionclientbrands;

                var cb = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @clientId=" + Client + "").ToList();

                return View(cb);
            }
            else
            {
                var cb = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @clientId=" + 0 + "").ToList();

                return View(cb);
            }
        }

        public ActionResult insertclientbrands(int BrandId)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];
            ClientBrands ClientBrands = new ClientBrands();
            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;


            if (ind != null)
            {
                int Client = ind.ClId;
                int Edition = ind.EId;

                byte clienttypeid = 0;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "ANUNCIANTE"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from edc in db.EditionClients
                          where edc.ClientId == Client
                          && edc.EditionId == Edition
                          select edc).ToList();
                if (ec.LongCount() > 0)
                {

                    var cb = (from _cb in db.ClientBrands
                              where _cb.EditionId == Edition
                              && _cb.ClientId == Client
                              && _cb.BrandId == BrandId
                              select _cb).ToList();
                    if (cb.LongCount() == 0)
                    {
                        ClientBrands.Active = true;
                        ClientBrands.BrandId = BrandId;
                        ClientBrands.ClientId = Client;
                        ClientBrands.EditionId = Edition;
                        ClientBrands.ExpireDescription = null;
                        ClientBrands.Page = null;
                        ClientBrands.ClientTypeId = clienttypeid;

                        db.ClientBrands.Add(ClientBrands);
                        db.SaveChanges();

                        int? ClientBrandTypeId = null;
                        int ClientTypeId = Convert.ToInt32(clienttypeid);
                        ActivityLog._insertClientBrands(BrandId, Client, ClientBrandTypeId, Edition, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                else
                {
                    EditionClients.ClientId = Client;
                    EditionClients.EditionId = Edition;
                    EditionClients.ClientTypeId = clienttypeid;

                    db.EditionClients.Add(EditionClients);
                    db.SaveChanges();

                    ActivityLog._inserteditionclient(Client, Edition, Convert.ToInt32(clienttypeid), ApplicationId, UsersId);

                    var cb = (from _cb in db.ClientBrands
                              where _cb.EditionId == Edition
                              && _cb.ClientId == Client
                              && _cb.BrandId == BrandId
                              select _cb).ToList();
                    if (cb.LongCount() == 0)
                    {
                        ClientBrands.Active = true;
                        ClientBrands.BrandId = BrandId;
                        ClientBrands.ClientId = Client;
                        ClientBrands.EditionId = Edition;
                        ClientBrands.ExpireDescription = null;
                        ClientBrands.Page = null;
                        ClientBrands.ClientTypeId = clienttypeid;

                        db.ClientBrands.Add(ClientBrands);
                        db.SaveChanges();

                        int? ClientBrandTypeId = null;
                        int ClientTypeId = clienttypeid;

                        ActivityLog._insertClientBrands(BrandId, Client, ClientBrandTypeId, Edition, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                return RedirectToAction("clientbrands", "SalesModule");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult deleteclientbrands(int BrandId)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];
            ClientBrands ClientBrands = new ClientBrands();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int Client = ind.ClId;
                int Edition = ind.EId;

                var cb = (from _cb in db.ClientBrands
                          where _cb.EditionId == Edition
                          && _cb.ClientId == Client
                          && _cb.BrandId == BrandId
                          select _cb).ToList();
                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        var delete = db.ClientBrands.SingleOrDefault(x => x.BrandId == _cb.BrandId && x.ClientId == _cb.ClientId && x.EditionId == _cb.EditionId && x.ClientTypeId == _cb.ClientTypeId);
                        db.ClientBrands.Remove(delete);
                        db.SaveChanges();

                        int ClientBrandTypeId = Convert.ToInt32(_cb.ClientBrandTypeId);
                        int ClientTypeId = _cb.ClientTypeId;


                        ActivityLog._insertClientBrands(_cb.BrandId, _cb.ClientId, ClientBrandTypeId, _cb.EditionId, ClientTypeId, ApplicationId, UsersId);
                    }
                }
            }
            return RedirectToAction("clientbrands", "SalesModule");
        }

        public ActionResult clientbranch(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionclientbranch ind = (sessionclientbranch)Session["sessionclientbranch"];
            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                sessionclientbranch sessionclientbranch = new sessionclientbranch(Country, Client, Edition, Book);
                Session["sessionclientbranch"] = sessionclientbranch;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "SUCURSAL"
                          select clientt).ToList();
                if (ct.LongCount() > 0)
                {
                    foreach (ClientTypes _ct in ct)
                    {
                        var cl = (from suc in db.Clients
                                  where suc.ClientIdParent == Client
                                  && suc.Active == true
                                  orderby suc.CompanyName ascending
                                  select suc).ToList();

                        return View(cl);
                    }
                }
            }
            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "SUCURSAL"
                          select clientt).ToList();
                if (ct.LongCount() > 0)
                {
                    foreach (ClientTypes _ct in ct)
                    {
                        var cl = (from suc in db.Clients
                                  where suc.ClientIdParent == Client
                                  && suc.Active == true
                                  orderby suc.CompanyName ascending
                                  select suc).ToList();

                        return View(cl);
                    }
                }
            }
            else
            {
                var cl = (from suc in db.Clients
                          where suc.ClientIdParent == 0
                          select suc).ToList();

                return View(cl);
            }

            return View();
        }

        public ActionResult inserteditionclient(string clientid, string editionid)
        {
            int ClientId = int.Parse(clientid);
            int EditionId = int.Parse(editionid);

            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ct = (from _ct in db.ClientTypes
                      where _ct.TypeName == "SUCURSAL"
                      select _ct).ToList();
            if (ct.LongCount() > 0)
            {
                foreach (ClientTypes _ct in ct)
                {
                    var edc = (from ed in db.EditionClients
                               where ed.ClientId == ClientId
                               && ed.EditionId == EditionId
                               && ed.ClientTypeId == _ct.ClientTypeId
                               select ed).ToList();
                    if (edc.LongCount() == 0)
                    {

                        EditionClients.ClientId = ClientId;
                        EditionClients.EditionId = EditionId;
                        EditionClients.ClientTypeId = _ct.ClientTypeId;
                        EditionClients.Page = null;

                        db.EditionClients.Add(EditionClients);
                        db.SaveChanges();

                        ActivityLog._inserteditionclient(ClientId, EditionId, _ct.ClientTypeId, ApplicationId, UsersId);
                    }
                }
            }

            var c = (from clients in db.Clients
                     where clients.ClientId == ClientId
                     && clients.Active == false
                     select clients).ToList();
            if (c.LongCount() > 0)
            {
                foreach (Clients _c in c)
                {
                    _c.Active = true;

                    db.SaveChanges();

                    ActivityLog._updateactiveclient(ClientId, true, ApplicationId, UsersId);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteeditionclients(string clientid, string editionid)
        {
            int ClientId = int.Parse(clientid);
            int EditionId = int.Parse(editionid);


            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var ct = (from _ct in db.ClientTypes
                      where _ct.TypeName == "SUCURSAL"
                      select _ct).ToList();
            if (ct.LongCount() > 0)
            {
                foreach (ClientTypes _ct in ct)
                {

                    var ecs = (from _ecs in db.EditionClientSpecialities
                               where _ecs.ClientId == ClientId
                               && _ecs.EditionId == EditionId
                               && _ecs.ClientTypeId == _ct.ClientTypeId
                               select _ecs).ToList();
                    if (ecs.LongCount() > 0)
                    {
                        foreach (EditionClientSpecialities _ecs in ecs)
                        {
                            var delete = db.EditionClientSpecialities.SingleOrDefault(x => x.ClientId == _ecs.ClientId && x.EditionId == _ecs.EditionId && x.ClientTypeId == _ecs.ClientTypeId && x.SpecialityId == _ecs.SpecialityId);
                            db.EditionClientSpecialities.Remove(delete);
                            db.SaveChanges();

                            ActivityLog._deleteEditionClientSpecialities(_ecs.ClientId, _ecs.EditionId, _ecs.SpecialityId, _ecs.ClientTypeId, ApplicationId, UsersId);
                        }
                    }

                    var ec = (from _ec in db.EditionClients
                              where _ec.ClientId == ClientId
                              && _ec.EditionId == EditionId
                              && _ec.ClientTypeId == _ct.ClientTypeId
                              select _ec).ToList();
                    if (ec.LongCount() > 0)
                    {
                        foreach (EditionClients _ec in ec)
                        {
                            try
                            {
                                var delete = db.EditionClients.SingleOrDefault(x => x.ClientId == _ec.ClientId && x.EditionId == _ec.EditionId && x.ClientTypeId == _ec.ClientTypeId);
                                db.EditionClients.Remove(delete);
                                db.SaveChanges();

                                ActivityLog._deleteeditionclient(_ec.ClientId, _ec.EditionId, _ec.ClientTypeId, ApplicationId, UsersId);
                            }
                            catch (Exception e)
                            {
                                string msj = e.Message;
                            }
                        }
                    }

                    var c = (from clients in db.Clients
                             where clients.ClientId == ClientId
                             && clients.Active == true
                             select clients).ToList();
                    if (c.LongCount() > 0)
                    {
                        foreach (Clients _c in c)
                        {
                            _c.Active = false;

                            db.SaveChanges();

                            ActivityLog._updateactiveclient(ClientId, false, ApplicationId, UsersId);
                        }
                    }

                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getspecialitiesbybranch(string client, string edition)
        {
            Session["sessionclientid"] = null;

            int ClientId = int.Parse(client);
            int EditionId = int.Parse(edition);

            List<Clients> LS = new List<Clients>();
            Clients Clients = new Models.Clients();


            var br = (from _br in db.Clients
                      join ec in db.EditionClients
                      on _br.ClientId equals ec.ClientId
                      where _br.ClientIdParent == ClientId
                      select _br).ToList();
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
            }

            sessionclientid sessionclientid = new sessionclientid(ClientId, EditionId, LS);
            Session["sessionclientid"] = sessionclientid;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertclientspecialitiesbranch(int SpecialityId)
        {
            sessionclientid ind = (sessionclientid)Session["sessionclientid"];
            EditionClientSpecialities EditionClientSpecialities = new EditionClientSpecialities();
            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int Client = ind.ClientId;
                int Edition = ind.EditionId;

                byte clienttypeid = 0;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "SUCURSAL"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from _ec in db.EditionClients
                          where _ec.ClientId == Client
                          && _ec.EditionId == Edition
                          && _ec.ClientTypeId == clienttypeid
                          select _ec).ToList();
                if (ec.LongCount() > 0)
                {
                    var ecs = (from _ecs in db.EditionClientSpecialities
                               where _ecs.EditionId == Edition
                               && _ecs.ClientId == Client
                               && _ecs.SpecialityId == SpecialityId
                               select _ecs).ToList();
                    if (ecs.LongCount() == 0)
                    {
                        EditionClientSpecialities.ClientId = Client;
                        EditionClientSpecialities.EditionId = Edition;
                        EditionClientSpecialities.SpecialityId = SpecialityId;
                        EditionClientSpecialities.ClientTypeId = clienttypeid;

                        db.EditionClientSpecialities.Add(EditionClientSpecialities);
                        db.SaveChanges();

                        int ClientTypeId = clienttypeid;
                        ActivityLog._insertEditionClientSpecialities(Client, Edition, SpecialityId, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                else
                {

                    EditionClients.ClientId = Client;
                    EditionClients.EditionId = Edition;
                    EditionClients.ClientTypeId = clienttypeid;

                    db.EditionClients.Add(EditionClients);
                    db.SaveChanges();

                    ActivityLog._inserteditionclient(Client, Edition, Convert.ToInt32(clienttypeid), ApplicationId, UsersId);


                    var ecs = (from _ecs in db.EditionClientSpecialities
                               where _ecs.EditionId == Edition
                               && _ecs.ClientId == Client
                               && _ecs.SpecialityId == SpecialityId
                               select _ecs).ToList();
                    if (ecs.LongCount() == 0)
                    {
                        EditionClientSpecialities.ClientId = Client;
                        EditionClientSpecialities.EditionId = Edition;
                        EditionClientSpecialities.SpecialityId = SpecialityId;
                        EditionClientSpecialities.ClientTypeId = clienttypeid;

                        db.EditionClientSpecialities.Add(EditionClientSpecialities);
                        db.SaveChanges();

                        int ClientTypeId = clienttypeid;
                        ActivityLog._insertEditionClientSpecialities(Client, Edition, SpecialityId, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                return RedirectToAction("clientspecialities", "SalesModule");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult deleteclientspecialitiesbranch(int SpecialityId)
        {
            sessionclientid ind = (sessionclientid)Session["sessionclientid"];
            EditionClientSpecialities EditionClientSpecialities = new EditionClientSpecialities();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int Client = ind.ClientId;
                int Edition = ind.EditionId;

                var ecs = (from _ecs in db.EditionClientSpecialities
                           where _ecs.EditionId == Edition
                           && _ecs.ClientId == Client
                           && _ecs.SpecialityId == SpecialityId
                           select _ecs).ToList();
                if (ecs.LongCount() > 0)
                {
                    foreach (EditionClientSpecialities _ecs in ecs)
                    {
                        var delete = db.EditionClientSpecialities.SingleOrDefault(x => x.SpecialityId == _ecs.SpecialityId && x.EditionId == _ecs.EditionId && x.ClientId == _ecs.ClientId && x.ClientTypeId == _ecs.ClientTypeId);
                        db.EditionClientSpecialities.Remove(delete);
                        db.SaveChanges();

                        ActivityLog._deleteEditionClientSpecialities(_ecs.ClientId, _ecs.EditionId, _ecs.SpecialityId, _ecs.ClientTypeId, ApplicationId, UsersId);
                    }

                }
            }

            return RedirectToAction("clientspecialities", "SalesModule");
        }

        public ActionResult insertbranch(string BranchName, string Client, string ShortName, string Description, string Edition)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            byte alphabetid = 0;

            sessionclientbranch ind = (sessionclientbranch)Session["sessionclientbranch"];

            Clients Clients = new Clients();
            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var name = BranchName.Substring(0, 1);

            var alph = (from leters in db.Alphabet
                        where leters.Letter == name
                        select leters).ToList();
            foreach (Alphabet al in alph)
            {
                alphabetid = al.AlphabetId;
            }

            byte CId = Convert.ToByte(ind.CId);

            var cll = (from _cl in db.Clients
                       where _cl.CompanyName == BranchName.Trim()
                       && _cl.CountryId == CId
                       && _cl.ClientIdParent == ClientId
                       && _cl.ClientCode == "Sucursal"
                       select _cl).ToList();
            if (cll.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Clients.Active = true;
                Clients.ClientIdParent = ClientId;
                Clients.ClientCode = "Sucursal";
                Clients.CompanyName = BranchName.Trim();
                Clients.ShortName = ShortName;
                Clients.CountryId = Convert.ToByte(ind.CId);
                Clients.AlphabetId = alphabetid;

                db.Clients.Add(Clients);
                db.SaveChanges();

                int ClientIdSuc = 0;

                var c = (from cl in db.Clients
                         where cl.CompanyName == BranchName.Trim()
                         && cl.ClientIdParent == ClientId
                         select cl).ToList();
                if (c.LongCount() > 0)
                {
                    foreach (Clients _c in c)
                    {
                        ClientIdSuc = _c.ClientId;
                    }

                    string ClientCode = "Sucursal";
                    byte CountryId = Convert.ToByte(ind.CId);

                    ActivityLog._insertclient(ClientIdSuc, ClientId, ClientCode, BranchName, ShortName, Description, CountryId, alphabetid, ApplicationId, UsersId);

                    var ct = (from _ct in db.ClientTypes
                              where _ct.TypeName == "SUCURSAL"
                              select _ct).ToList();
                    if (ct.LongCount() > 0)
                    {
                        foreach (ClientTypes _ct in ct)
                        {
                            EditionClients.ClientId = ClientIdSuc;
                            EditionClients.EditionId = EditionId;
                            EditionClients.ClientTypeId = _ct.ClientTypeId;
                            EditionClients.Page = null;

                            db.EditionClients.Add(EditionClients);
                            db.SaveChanges();

                            ActivityLog._inserteditionclient(ClientIdSuc, EditionId, _ct.ClientTypeId, ApplicationId, UsersId);
                        }
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult editclient(string CompanyName, string ShortName, string Country, string Client)
        {

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientId = int.Parse(Client);
            int CountryId = int.Parse(Country);

            var c = (from cl in db.Clients
                     where cl.ClientId == ClientId
                     && cl.CountryId == CountryId
                     select cl).ToList();

            if (c.LongCount() > 0)
            {
                foreach (Clients _c in c)
                {
                    _c.CompanyName = CompanyName.Trim();
                    _c.ShortName = ShortName.Trim();

                    db.SaveChanges();

                    byte AlphabetId = Convert.ToByte(_c.AlphabetId);

                    ActivityLog._editclient(ClientId, CountryId, CompanyName, ShortName, ApplicationId, UsersId, AlphabetId);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult informationbyclient(int? Id)
        {
            if (Id != null)
            {
                var ad = (from ca in db.ClientAddresses
                          join c in db.Clients
                              on ca.ClientId equals c.ClientId
                          join a in db.Addresses
                              on ca.AddressId equals a.AddressId
                          join s in db.States
                            on a.StateId equals s.StateId
                          where ca.ClientId == Id
                          && a.Active == true
                          select new JoinClientAddressesPhones { ClientAddresses = ca, Addresses = a, Clients = c, States = s }).OrderBy(x => x.Addresses.City).ToList();

                var cl = (from c in db.Clients
                          where c.ClientId == Id
                          && c.ClientIdParent == null
                          select c).ToList();
                if (cl.LongCount() > 0)
                {
                    foreach (Clients _c in cl)
                    {
                        ViewData["ClientIdS"] = _c.ClientId;
                        ViewData["CompanyNameS"] = "---";
                    }
                }
                else
                {
                    var cls = (from c in db.Clients
                               where c.ClientId == Id
                               select c).ToList();
                    foreach (Clients _c in cls)
                    {
                        ViewData["ClientIdS"] = _c.ClientId;
                        ViewData["CompanyNameS"] = _c.CompanyName;
                    }
                }

                return View(ad);
            }
            return View();
        }

        public ActionResult editinformation(string Address, string Suburb, string City, string ZipC, string Email, string Web, string Country, string AddressId, string State)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int CountryId = int.Parse(Country);
            int AddressIdd = int.Parse(AddressId);
            int StateId = int.Parse(State);

            var addres = (from ad in db.Addresses
                          where ad.AddressId == AddressIdd
                          select ad).ToList();

            if (addres.LongCount() > 0)
            {
                foreach (Addresses _addres in addres)
                {
                    _addres.Address = Address.Trim();
                    _addres.Suburb = Suburb.Trim();
                    _addres.City = City.Trim();
                    _addres.Email = Email.Trim();
                    _addres.Web = Web.Trim();
                    _addres.ZipCode = ZipC.Trim();
                    _addres.StateId = StateId;

                    db.SaveChanges();

                    ActivityLog._editinformation(AddressIdd, Address, Suburb, City, Email, Web, ZipC, CountryId, ApplicationId, UsersId);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertaddress(string Address, string Suburb, string City, string ZipCode, string Email, string Web, string Client, string Edition, string Country, string State)
        {
            Addresses Addresses = new Addresses();
            ClientAddresses ClientAddresses = new Models.ClientAddresses();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            byte CountryId = Convert.ToByte(Country);
            int StateId = int.Parse(State);

            var addr = (from a in db.Addresses
                        where a.Address == Address.Trim()
                        && a.Suburb == Suburb.Trim()
                        && a.City == City.Trim()
                        && a.CountryId == CountryId
                        && a.Email == Email.Trim()
                        && a.Suburb == Suburb.Trim()
                        && a.Web == Web.Trim()
                        && a.ZipCode == ZipCode.Trim()
                        && a.StateId == StateId
                        select a).ToList();

            if (addr.LongCount() == 0)
            {
                Addresses.Active = true;
                Addresses.Address = Address.Trim();
                Addresses.City = City.Trim();
                Addresses.CountryId = CountryId;
                Addresses.Email = Email.Trim();
                Addresses.Suburb = Suburb.Trim();
                Addresses.Web = Web.Trim();
                Addresses.ZipCode = ZipCode.Trim();
                Addresses.StateId = StateId;

                db.Addresses.Add(Addresses);
                db.SaveChanges();
            }


            var addres = (from a in db.Addresses
                          where a.Address == Address.Trim()
                          && a.Suburb == Suburb.Trim()
                          && a.City == City.Trim()
                          && a.CountryId == CountryId
                          && a.Email == Email.Trim()
                          && a.Web == Web.Trim()
                          && a.ZipCode == ZipCode.Trim()
                          && a.StateId == StateId
                          select a).ToList();

            if (addres.LongCount() > 0)
            {
                int AddressId = 0;

                foreach (Addresses _addres in addres)
                {
                    AddressId = _addres.AddressId;
                }

                ActivityLog._insertAddresses(AddressId, Address, Suburb, City, CountryId, Email, Web, ZipCode, ApplicationId, UsersId);

                ClientAddresses.AddressId = AddressId;
                ClientAddresses.ClientId = ClientId;

                db.ClientAddresses.Add(ClientAddresses);
                db.SaveChanges();

                ActivityLog._insertClientAddresses(AddressId, ClientId, ApplicationId, UsersId);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteaddress(int? AddressId, int? ClientId)
        {
            var cp = db.ClientPhones.Where(x => x.AddressId == AddressId).ToList();

            if (cp.LongCount() > 0)
            {
                foreach (ClientPhones _cp in cp)
                {
                    var delete = db.ClientPhones.SingleOrDefault(x => x.AddressId == _cp.AddressId && x.ClientPhoneId == _cp.ClientPhoneId && x.ClientId == _cp.ClientId);
                    db.ClientPhones.Remove(delete);
                    db.SaveChanges();
                }
            }

            var ca = db.ClientAddresses.Where(x => x.AddressId == AddressId).ToList();

            if (ca.LongCount() > 0)
            {
                foreach (ClientAddresses _ca in ca)
                {
                    var delete = db.ClientAddresses.SingleOrDefault(x => x.AddressId == _ca.AddressId && x.ClientId == _ca.ClientId);
                    db.ClientAddresses.Remove(delete);
                    db.SaveChanges();
                }
            }

            var a = db.Addresses.Where(x => x.AddressId == AddressId).ToList();

            if (a.LongCount() > 0)
            {
                foreach (Addresses _a in a)
                {
                    var deletes = db.Addresses.SingleOrDefault(x => x.AddressId == _a.AddressId);
                    db.Addresses.Remove(deletes);
                    db.SaveChanges();
                }
            }



            return RedirectToAction("informationbyclient", "SalesModule", new { Id = ClientId });
        }

        public ActionResult clientphones(int? AddressId, int? ClientId)
        {
            if (AddressId != null)
            {
                var cp = (from _cp in db.ClientPhones
                          join pt in db.PhoneTypes
                          on _cp.PhoneTypeId equals pt.PhoneTypeId
                          where _cp.ClientId == ClientId
                          && _cp.AddressId == AddressId
                          orderby _cp.Number ascending
                          select new JoinPhoneTypes { PhoneTypes = pt, ClientPhones = _cp }).ToList();

                var c = (from cl in db.Clients
                         where cl.ClientId == ClientId
                         && cl.ClientIdParent == null
                         select cl).ToList();
                if (c.LongCount() > 0)
                {
                    foreach (Clients _c in c)
                    {
                        ViewData["CompanyNameP"] = "---";
                        ViewData["ClientIdP"] = _c.ClientId;
                    }
                }
                else
                {
                    var _cls = (from cls in db.Clients
                                where cls.ClientId == ClientId
                                select cls).ToList();
                    foreach (Clients _c in _cls)
                    {
                        ViewData["ClientIdP"] = _c.ClientId;
                        ViewData["CompanyNameP"] = _c.CompanyName;
                    }
                }


                var addr = (from a in db.Addresses
                            where a.AddressId == AddressId
                            select a).ToList();
                foreach (Addresses _addr in addr)
                {
                    ViewData["AddressId"] = _addr.AddressId;
                }

                return View(cp);
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult editphones(string Number, string Lada, string ClientPhone, string Country, string Client, string Address, string PhoneType)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ClientPhoneId = int.Parse(ClientPhone);
            int CountryId = int.Parse(Country);
            int ClientId = int.Parse(Client);
            int AddressId = int.Parse(Address);
            byte PhoneTypeId = Convert.ToByte(PhoneType);

            var ca = (from _ca in db.ClientAddresses
                      where _ca.ClientId == ClientId
                      && _ca.AddressId == AddressId
                      select _ca).ToList();

            if (ca.LongCount() > 0)
            {
                var cp = (from _cp in db.ClientPhones
                          where _cp.ClientPhoneId == ClientPhoneId
                          && _cp.ClientId == ClientId
                          && _cp.AddressId == AddressId
                          && _cp.PhoneTypeId == PhoneTypeId
                          select _cp).ToList();

                if (cp.LongCount() > 0)
                {
                    foreach (ClientPhones _cp in cp)
                    {
                        _cp.Number = Number.Trim();
                        if ((Lada == "") || (Lada == string.Empty))
                        {
                            _cp.Lada = null;
                        }
                        else
                        {
                            _cp.Lada = Lada.Trim();
                        }
                        _cp.Active = true;
                    }
                    try
                    {
                        db.SaveChanges();

                        ActivityLog._editphones(ClientPhoneId, ClientId, AddressId, PhoneTypeId, Number, Lada, ApplicationId, UsersId);
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult insertphone(string Number, string Lada, string Client, string Address, string TypeId)
        {
            ClientPhones ClientPhones = new Models.ClientPhones();
            byte PhoneTypeId = Convert.ToByte(TypeId);
            int ClientId = int.Parse(Client);
            int AddressId = int.Parse(Address);

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var cp = (from _cp in db.ClientPhones
                      where _cp.AddressId == AddressId
                      && _cp.ClientId == ClientId
                      && _cp.PhoneTypeId == PhoneTypeId
                      && _cp.Number == Number.Trim()
                      && _cp.Lada == Lada.Trim()
                      select _cp).ToList();
            if (cp.LongCount() == 0)
            {
                ClientPhones.Active = true;
                ClientPhones.AddressId = AddressId;
                ClientPhones.ClientId = ClientId;
                if ((Lada == "") || (Lada == string.Empty))
                {
                    ClientPhones.Lada = null;
                }
                else
                {
                    ClientPhones.Lada = Lada.Trim();
                }
                ClientPhones.Number = Number.Trim();
                ClientPhones.PhoneTypeId = PhoneTypeId;

                try
                {
                    db.ClientPhones.Add(ClientPhones);
                    db.SaveChanges();

                    var cps = (from _cp in db.ClientPhones
                               where _cp.AddressId == AddressId
                               && _cp.ClientId == ClientId
                               && _cp.PhoneTypeId == PhoneTypeId
                               && _cp.Number == Number.Trim()
                               && _cp.Lada == Lada.Trim()
                               select _cp).ToList();
                    if (cps.LongCount() > 0)
                    {
                        int ClientPhoneId = 0;
                        foreach (ClientPhones _cps in cps)
                        {
                            ClientPhoneId = _cps.ClientPhoneId;
                        }
                        ActivityLog._insertphone(ClientPhoneId, AddressId, ClientId, PhoneTypeId, Number, Lada, ApplicationId, UsersId);
                    }
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult deletereferenceadvers()
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchpp(string BrandName)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];

            if (string.IsNullOrEmpty(BrandName))
            {
                Session["sessionsearchbrand"] = null;

                return RedirectToAction("clientbrands");
            }
            else
            {
                List<joinClientBrands> LJ = new List<joinClientBrands>();
                joinClientBrands joinClientBrands = new joinClientBrands();

                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;
                var cb = (from _cb in db.ClientBrands
                          join b in db.Brands
                              on _cb.BrandId equals b.BrandId
                          join c in db.Clients
                              on _cb.ClientId equals c.ClientId
                          where _cb.ClientId == Client
                          && _cb.EditionId == Edition
                          && b.BrandName.ToUpper().StartsWith(BrandName.ToUpper())
                          orderby b.BrandName ascending
                          select new joinClientBrands { ClientBrands = _cb, Brands = b, Clients = c }).OrderBy(x => x.Brands.BrandName).ToList();

                if (cb.LongCount() > 0)
                {
                    return View("clientbrands", cb);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult searchbrandcat(string BrandName)
        {

            sessionsearchbrand sessionsearchbrand = new sessionsearchbrand(BrandName);
            Session["sessionsearchbrand"] = sessionsearchbrand;

            return RedirectToAction("clientbrands");
        }

        public JsonResult getnumberofbrands(string Values)
        {
            sessiongetnumbersofbrands sessiongetnumbersofbrands = new sessiongetnumbersofbrands(Values);
            Session["sessiongetnumbersofbrands"] = sessiongetnumbersofbrands;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getnumberofbrandsasocaite(string Values)
        {
            sessiongetnumbetofbrandsasociate sessiongetnumbetofbrandsasociate = new sessiongetnumbetofbrandsasociate(Values);
            Session["sessiongetnumbetofbrandsasociate"] = sessiongetnumbetofbrandsasociate;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertclientbrandtype(string brandid, string clientid, string editionid)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int BrandId = int.Parse(brandid);
            int ClientId = int.Parse(clientid);
            int EditionId = int.Parse(editionid);

            var ct = (from _ct in db.ClientBrands
                      where _ct.BrandId == BrandId
                      && _ct.ClientId == ClientId
                      && _ct.EditionId == EditionId
                      select _ct).ToList();

            if (ct.LongCount() > 0)
            {
                foreach (ClientBrands _ct in ct)
                {
                    String Description = "Distribuidor autorizado";
                    var cbt = (from _cbt in db.ClientBrandTypes
                               where _cbt.Description.ToUpper().Equals(Description.ToUpper())
                               select _cbt).ToList();
                    if (cbt.LongCount() > 0)
                    {
                        foreach (ClientBrandTypes _cbt in cbt)
                        {
                            _ct.ClientBrandTypeId = _cbt.ClientBrandTypeId;

                            db.SaveChanges();

                            ActivityLog._insertclientbrandtypeRep(BrandId, ClientId, EditionId, _cbt.ClientBrandTypeId, ApplicationId, UsersId);
                        }
                    }
                }
            }

            return View();
        }

        public ActionResult deleteclientbrandtype(string brandid, string clientid, string editionid)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int BrandId = int.Parse(brandid);
            int ClientId = int.Parse(clientid);
            int EditionId = int.Parse(editionid);

            var ct = (from _ct in db.ClientBrands
                      where _ct.BrandId == BrandId
                      && _ct.ClientId == ClientId
                      && _ct.EditionId == EditionId
                      select _ct).ToList();
            if (ct.LongCount() > 0)
            {
                foreach (ClientBrands _ct in ct)
                {
                    _ct.ClientBrandTypeId = null;

                    db.SaveChanges();

                    ActivityLog.deleteclientbrandtype(BrandId, ClientId, EditionId, ApplicationId, UsersId);
                }
            }
            return View();
        }

        public ActionResult insertclientbrandtypeRep(string brandid, string clientid, string editionid)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int BrandId = int.Parse(brandid);
            int ClientId = int.Parse(clientid);
            int EditionId = int.Parse(editionid);

            var ct = (from _ct in db.ClientBrands
                      where _ct.BrandId == BrandId
                      && _ct.ClientId == ClientId
                      && _ct.EditionId == EditionId
                      select _ct).ToList();

            if (ct.LongCount() > 0)
            {
                foreach (ClientBrands _ct in ct)
                {
                    String Description = "Representante Exclusivo";
                    var cbt = (from _cbt in db.ClientBrandTypes
                               where _cbt.Description.ToUpper().Equals(Description.ToUpper())
                               select _cbt).ToList();
                    if (cbt.LongCount() > 0)
                    {
                        foreach (ClientBrandTypes _cbt in cbt)
                        {
                            _ct.ClientBrandTypeId = _cbt.ClientBrandTypeId;

                            db.SaveChanges();

                            ActivityLog._insertclientbrandtypeRep(BrandId, ClientId, EditionId, _cbt.ClientBrandTypeId, ApplicationId, UsersId);
                        }
                    }

                }
            }

            return View();
        }

        public ActionResult searchspecialities(string Description)
        {
            sessionsearchspeciality sessionsearchspeciality = new sessionsearchspeciality(Description);
            Session["sessionsearchspeciality"] = sessionsearchspeciality;

            return RedirectToAction("clientspecialities");
        }

        public ActionResult searchspecialitiesasoc(string Description)
        {
            sessionclientspecialities ind = (sessionclientspecialities)Session["sessionclientspecialities"];

            int Country = ind.CId;
            int Client = ind.ClId;
            int Edition = ind.EId;
            int Book = ind.BId;

            int len = Description.Length;

            if (len == 3)
            {
                var ece = (from ecs in db.EditionClientSpecialities
                           join c in db.Clients
                           on ecs.ClientId equals c.ClientId
                           join s in db.Specialities
                           on ecs.SpecialityId equals s.SpecialityId
                           where ecs.ClientId == Client
                           && ecs.EditionId == Edition
                           && s.Description.ToUpper().Trim().StartsWith(Description.ToUpper().Trim())
                           select new JoinEditionClientSpecialities { EditionClientSpecialities = ecs, Specialities = s, Clients = c }).ToList();

                return View("clientspecialities", ece);
            }
            if (len > 3)
            {
                var ece = (from ecs in db.EditionClientSpecialities
                           join c in db.Clients
                           on ecs.ClientId equals c.ClientId
                           join s in db.Specialities
                           on ecs.SpecialityId equals s.SpecialityId
                           where ecs.ClientId == Client
                           && ecs.EditionId == Edition
                           && s.Description.ToUpper().Trim().Contains(Description.ToUpper().Trim())
                           select new JoinEditionClientSpecialities { EditionClientSpecialities = ecs, Specialities = s, Clients = c }).ToList();

                return View("clientspecialities", ece);
            }
            else
            {
                return RedirectToAction("clientspecialities");
            }
        }

        public ActionResult searchspecialitiesbybranch(string Description)
        {
            sessionspecialitybranch sessionspecialitybranch = new sessionspecialitybranch(Description);
            Session["sessionspecialitybranch"] = sessionspecialitybranch;
            return RedirectToAction("clientspecialities");
        }

        public ActionResult searchspecialitiesbybranchasoc(string Description)
        {

            if (string.IsNullOrEmpty(Description.Trim()))
            {
                Session["sessionsearchspecialitybybranchasoc"] = null;

                return RedirectToAction("clientspecialities");
            }
            else
            {
                sessionsearchspecialitybybranchasoc sessionsearchspecialitybybranchasoc = new sessionsearchspecialitybybranchasoc(Description);
                Session["sessionsearchspecialitybybranchasoc"] = sessionsearchspecialitybybranchasoc;

                return RedirectToAction("clientspecialities");
            }
        }

        public ActionResult deleteclientphone(int? ClientPhoneId)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var cp = (from _cp in db.ClientPhones
                      where _cp.ClientPhoneId == ClientPhoneId
                      select _cp).ToList();

            if (cp.LongCount() > 0)
            {
                foreach (ClientPhones _cp in cp)
                {
                    var delete = db.ClientPhones.SingleOrDefault(x => x.ClientPhoneId == _cp.ClientPhoneId && x.ClientId == _cp.ClientId && x.AddressId == _cp.AddressId && _cp.PhoneTypeId == _cp.PhoneTypeId);
                    db.ClientPhones.Remove(delete);
                    db.SaveChanges();


                }
            }

            return RedirectToAction("clientphones");
        }

        public JsonResult insertreferenceadvers(string Client, string Edition, string Advers, string Speciality, string Quantity)
        {
            if (Client != null)
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                int ApplicationId = p.ApplicationId;
                int UsersId = p.userId;

                int ClientId = int.Parse(Client);
                int EditionId = int.Parse(Edition);
                int SpecialityId = int.Parse(Speciality);

                var ecs = db.EditionClientSpecialities.Where(x => x.ClientId == ClientId && x.SpecialityId == SpecialityId && x.AdversDescription != Advers).ToList();

                if (ecs.LongCount() > 0)
                {
                    foreach (EditionClientSpecialities _ecs in ecs)
                    {
                        if ((Advers.Trim() == String.Empty) || (Advers.Trim() == "") && (Quantity.Trim() == String.Empty) || (Quantity.Trim() == ""))
                        {
                            _ecs.AdversDescription = null;
                            _ecs.Quantity = null;
                        }
                        else
                        {
                            _ecs.AdversDescription = Advers.Trim();
                            _ecs.Quantity = Quantity.Trim();
                        }

                        try
                        {
                            db.SaveChanges();

                            ActivityLog._updateeditionclientspecialities(_ecs.ClientId, _ecs.EditionId, _ecs.SpecialityId, ApplicationId, UsersId, Advers, Quantity);
                        }
                        catch (Exception)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertclnormal(string Client, string Edition)
        {
            if ((Client != null) && (Edition != null))
            {
                int ClientId = int.Parse(Client);
                int EditionId = int.Parse(Edition);

                var dta = db.ClientTypes.Where(x => x.TypeName == "ANUNCIANTE").ToList();

                byte ClientTypeIdA = dta[0].ClientTypeId;

                var dtn = db.ClientTypes.Where(x => x.TypeName == "NORMAL").ToList();

                byte ClientTypeIdN = dtn[0].ClientTypeId;

                var eds = db.EditionClientSpecialities.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (eds.LongCount() > 0)
                {
                    foreach (EditionClientSpecialities _eds in eds)
                    {
                        if (_eds.ClientTypeId == ClientTypeIdA)
                        {
                            _eds.ClientTypeId = ClientTypeIdN;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                string msj = e.InnerException.Message;
                            }
                        }
                    }
                }

                var cb = db.ClientBrands.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        if (_cb.ClientTypeId == ClientTypeIdA)
                        {
                            _cb.ClientTypeId = ClientTypeIdN;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                string msj = e.InnerException.Message;
                            }
                        }
                    }
                }

                var edc = db.EditionClients.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (edc.LongCount() > 0)
                {
                    foreach (EditionClients _edc in edc)
                    {
                        if (_edc.ClientTypeId == ClientTypeIdA)
                        {
                            _edc.ClientTypeId = ClientTypeIdN;

                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                string msj = e.InnerException.Message;
                            }
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        /*          INSERTAR REGISTROS EN LA NUEVA EDICIÓN          */

        public JsonResult inseditionclientspecialitiesnewedition(string Speciality, string Client, string Edition, string Advers, string Quantity, string TypeName)
        {
            EditionClientSpecialities EditionClientSpecialities = new Models.EditionClientSpecialities();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int SpecialityId = int.Parse(Speciality);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            byte ClientTypeId = 0;

            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == TypeName
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                ClientTypeId = _ct.ClientTypeId;
            }

            var ecs = db.EditionClientSpecialities.Where(x => x.ClientId == ClientId && x.SpecialityId == SpecialityId && x.ClientTypeId == ClientTypeId).ToList();

            if (ecs.LongCount() == 0)
            {
                EditionClientSpecialities.ClientId = ClientId;
                EditionClientSpecialities.ClientTypeId = ClientTypeId;
                EditionClientSpecialities.EditionId = EditionId;
                EditionClientSpecialities.SpecialityId = SpecialityId;

                if (!string.IsNullOrEmpty(Advers))
                {
                    EditionClientSpecialities.AdversDescription = Advers;
                }
                else
                {
                    EditionClientSpecialities.AdversDescription = null;
                }

                if (!string.IsNullOrEmpty(Quantity))
                {
                    EditionClientSpecialities.Quantity = Quantity;
                }
                else
                {
                    EditionClientSpecialities.Quantity = null;
                }

                db.EditionClientSpecialities.Add(EditionClientSpecialities);
                db.SaveChanges();

                ActivityLog._insertEditionClientSpecialities(ClientId, EditionId, SpecialityId, ClientTypeId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                foreach (EditionClientSpecialities _ecs in ecs)
                {
                    EditionClientSpecialities.ClientId = _ecs.ClientId;
                    EditionClientSpecialities.EditionId = EditionId;
                    EditionClientSpecialities.SpecialityId = _ecs.SpecialityId;
                    EditionClientSpecialities.ClientTypeId = _ecs.ClientTypeId;
                    EditionClientSpecialities.AdversDescription = _ecs.AdversDescription;
                    EditionClientSpecialities.Quantity = _ecs.Quantity;

                    db.EditionClientSpecialities.Add(EditionClientSpecialities);
                    db.SaveChanges();

                    int ClientTypeIds = Convert.ToInt32(_ecs.ClientTypeId);

                    ActivityLog._insertEditionClientSpecialities(_ecs.ClientId, EditionId, _ecs.SpecialityId, ClientTypeIds, ApplicationId, UsersId);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult insertclientbrandsnewedition(string Brand, string Client, string Edition)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];
            ClientBrands ClientBrands = new ClientBrands();
            EditionClients EditionClients = new Models.EditionClients();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            if (ind != null)
            {
                int ClientId = int.Parse(Client);
                int EditionId = int.Parse(Edition);
                int BrandId = int.Parse(Brand);

                byte clienttypeid = 0;

                var ct = (from clientt in db.ClientTypes
                          where clientt.TypeName == "ANUNCIANTE"
                          select clientt).ToList();
                foreach (ClientTypes _ct in ct)
                {
                    clienttypeid = _ct.ClientTypeId;
                }

                var ec = (from edc in db.EditionClients
                          where edc.ClientId == ClientId
                          && edc.EditionId == EditionId
                          select edc).ToList();
                if (ec.LongCount() > 0)
                {

                    var cb = (from _cb in db.ClientBrands
                              where _cb.EditionId != EditionId
                              && _cb.ClientId == ClientId
                              && _cb.BrandId == BrandId
                              select _cb).ToList();
                    if (cb.LongCount() > 0)
                    {
                        foreach (ClientBrands _cb in cb)
                        {
                            ClientBrands.Active = _cb.Active;
                            ClientBrands.BrandId = _cb.BrandId;
                            ClientBrands.ClientId = _cb.ClientId;
                            ClientBrands.EditionId = EditionId;
                            ClientBrands.ExpireDescription = _cb.ExpireDescription;
                            ClientBrands.Page = null;
                            ClientBrands.ClientTypeId = _cb.ClientTypeId;

                            db.ClientBrands.Add(ClientBrands);
                            db.SaveChanges();

                            int? ClientBrandTypeId = null;
                            int ClientTypeId = Convert.ToInt32(clienttypeid);

                            ActivityLog._insertClientBrands(BrandId, ClientId, ClientBrandTypeId, EditionId, ClientTypeId, ApplicationId, UsersId);
                        }
                    }
                    else
                    {
                        ClientBrands.Active = true;
                        ClientBrands.BrandId = BrandId;
                        ClientBrands.ClientId = ClientId;
                        ClientBrands.EditionId = EditionId;
                        ClientBrands.ExpireDescription = null;
                        ClientBrands.Page = null;
                        ClientBrands.ClientTypeId = clienttypeid;

                        db.ClientBrands.Add(ClientBrands);
                        db.SaveChanges();

                        int? ClientBrandTypeId = null;
                        int ClientTypeId = Convert.ToInt32(clienttypeid);

                        ActivityLog._insertClientBrands(BrandId, ClientId, ClientBrandTypeId, EditionId, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                else
                {
                    EditionClients.ClientId = ClientId;
                    EditionClients.EditionId = EditionId;
                    EditionClients.ClientTypeId = clienttypeid;

                    db.EditionClients.Add(EditionClients);
                    db.SaveChanges();

                    ActivityLog._inserteditionclient(ClientId, EditionId, Convert.ToInt32(clienttypeid), ApplicationId, UsersId);

                    var cb = (from _cb in db.ClientBrands
                              where _cb.EditionId == EditionId
                              && _cb.ClientId == ClientId
                              && _cb.BrandId == BrandId
                              select _cb).ToList();
                    if (cb.LongCount() == 0)
                    {
                        ClientBrands.Active = true;
                        ClientBrands.BrandId = BrandId;
                        ClientBrands.ClientId = ClientId;
                        ClientBrands.EditionId = EditionId;
                        ClientBrands.ExpireDescription = null;
                        ClientBrands.Page = null;
                        ClientBrands.ClientTypeId = clienttypeid;

                        db.ClientBrands.Add(ClientBrands);
                        db.SaveChanges();

                        int? ClientBrandTypeId = null;
                        int ClientTypeId = clienttypeid;

                        ActivityLog._insertClientBrands(BrandId, ClientId, ClientBrandTypeId, EditionId, ClientTypeId, ApplicationId, UsersId);
                    }
                }
                return RedirectToAction("clientbrands", "SalesModule");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public JsonResult inserteditionclientcategoriesnewedition(string Category, string Client, string Edition)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            EditionClientCategories EditionClientCategories = new Models.EditionClientCategories();

            int CategoryId = int.Parse(Category);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var ecc = db.EditionClientCategories.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.CategoryId == CategoryId).ToList();

            if (ecc.LongCount() == 0)
            {
                EditionClientCategories = new EditionClientCategories();

                EditionClientCategories.CategoryId = CategoryId;
                EditionClientCategories.ClientId = ClientId;
                EditionClientCategories.EditionId = EditionId;

                db.EditionClientCategories.Add(EditionClientCategories);
                db.SaveChanges();

                ActivityLog._inserteditionclientcategories(ClientId, CategoryId, EditionId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult inserteditionclientheterogeneouscategoriesnewedition(string HeterogeneousCategory, string Client, string Edition)
        {
            EditionClientHeterogeneousCategories EditionClientHeterogeneousCategories = new Models.EditionClientHeterogeneousCategories();
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int HeterogeneousCategoryId = int.Parse(HeterogeneousCategory);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var ecc = db.EditionClientHeterogeneousCategories.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.HeterogeneousCategoryId == HeterogeneousCategoryId).ToList();

            if (ecc.LongCount() == 0)
            {
                EditionClientHeterogeneousCategories = new EditionClientHeterogeneousCategories();

                EditionClientHeterogeneousCategories.HeterogeneousCategoryId = HeterogeneousCategoryId;
                EditionClientHeterogeneousCategories.ClientId = ClientId;
                EditionClientHeterogeneousCategories.EditionId = EditionId;

                db.EditionClientHeterogeneousCategories.Add(EditionClientHeterogeneousCategories);
                db.SaveChanges();

                ActivityLog._inserteditionclientheterogeneouscategories(ClientId, HeterogeneousCategoryId, EditionId, ApplicationId, UsersId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult editclientbrands(string Brand, string Client, string Edition, string ExpireDescription)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int BrandId = int.Parse(Brand);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            byte ClientTypeId = 0;

            var ct = (from clientt in db.ClientTypes
                      where clientt.TypeName == "ANUNCIANTE"
                      select clientt).ToList();
            foreach (ClientTypes _ct in ct)
            {
                ClientTypeId = _ct.ClientTypeId;
            }

            var cb = db.ClientBrands.Where(x => x.ClientId == ClientId && x.BrandId == BrandId && x.ClientTypeId == ClientTypeId).ToList();

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



        /*          CLASIFICACIÓN HOMOGENEOUSCATEGORIES*/

        public ActionResult ClasificationProducts(int? ProductId)
        {
            indexsalesmodule ind = (indexsalesmodule)Session["indexsalesmodule"];

            int _clientid = ind.ClId;

            ind.PId = ProductId;

            var cc = db.Database.SqlQuery<HomogeneousCategoriesByClientByProduct>("plm_spGetProductHomogeneousCategoriesByClientByProduct  @ProductId =" + ProductId + ",  @ClientId= " + _clientid + "").ToList();

            return View(cc);
        }

        public JsonResult getlevel4(string HomogeneousCategory)
        {
            List<GetHomogeneousCategories> Cats = new List<GetHomogeneousCategories>();
            SearchCategory SearchCategory = (SearchCategory)Session["SearchCategory"];

            if (SearchCategory != null)
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetHomogeneousCategories @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @TextSearch='" + SearchCategory.CategoryName + "'").ToList();
            }
            else
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetHomogeneousCategories @HomogeneousCategoryId=" + HomogeneousCategoryId + "").ToList();
            }

            return Json(Cats, JsonRequestBehavior.AllowGet);
        }

        public JsonResult saveCategories(String ListItems, string ArraySize)
        {

            dynamic d = JsonConvert.DeserializeObject(ListItems);

            int Size = int.Parse(ArraySize);


            for (var i = 0; i <= Size - 1; i++)
            {
                var Id = Convert.ToInt32(d[i]["Id"]);
                var CId = Convert.ToInt32(d[i]["CId"]);
                var PId = Convert.ToInt32(d[i]["PId"]);
                var HCId = Convert.ToInt32(d[i]["HCId"]);

                string a = insertProductLeafCategories(CId, PId, Id, HCId);
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public string insertProductLeafCategories(int ClientId, int ProductId, int CategoryId, int HomogeneousCategoryId)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            ClientProductLeafCategories CPLC = new ClientProductLeafCategories();


            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

            if (cp.LongCount() > 0)
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId).ToList();

                if (cpl.LongCount() == 0)
                {
                    CPLC = new ClientProductLeafCategories();

                    CPLC.ClientId = ClientId;
                    CPLC.HomogeneousCategoryId = HomogeneousCategoryId;
                    CPLC.LeafCategoryId = CategoryId;
                    CPLC.ProductId = ProductId;

                    db.ClientProductLeafCategories.Add(CPLC);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, HomogeneousCategoryId, CategoryId, 1, ApplicationId, UsersId);

                    return "Ok";
                }
                else
                {
                    return "_exist";
                }
            }
            else
            {
                return "_cpnotexist";
            }
        }

        public JsonResult deleteProductLeafCategories(string Client, string Product, string Category, string HomogeneousCategory)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

            try
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId).ToList();

                if (cpl.LongCount() > 0)
                {
                    var delete = db.ClientProductLeafCategories.SingleOrDefault(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId);

                    db.ClientProductLeafCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, HomogeneousCategoryId, CategoryId, 4, ApplicationId, UsersId);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string message = e.Message;

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult searchCategories(string CategoryName)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                SearchCategorySM SearchCategorySM = new SearchCategorySM(CategoryName);
                Session["SearchCategorySM"] = SearchCategorySM;
            }
            else
            {
                SearchCategorySM SearchCategorySM = new SearchCategorySM(CategoryName);
                Session["SearchCategorySM"] = SearchCategorySM;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getproductstatus(string Product, string Client)
        {
            List<Status> LS = new List<Status>();
            Status S = new Status();

            List<int> LI = new List<int>();

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            var s = db.Status.Where(x => x.Active == true).OrderBy(x => x.StatusName).ToList();

            var ps = db.ProductStatus.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

            if (ps.LongCount() > 0)
            {

                var _result = db.Database.SqlQuery<StatusByProduct>("plm_spGetProductStatus @ProductId=" + ProductId + ", @ClientId=" + ClientId + "").ToList();

                if (_result.LongCount() > 0)
                {
                    foreach (StatusByProduct _sbp in _result)
                    {
                        S = new Status();

                        S.Active = Convert.ToBoolean(_sbp.Active);
                        S.StatusId = Convert.ToInt32(_sbp.StatusId);
                        if (_sbp.Reference == 1)
                        {
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " checked='checked' disabled='disabled' onclick='insertproductstatus($(this))'/> &nbsp;&nbsp;" + _sbp.StatusName;
                        }
                        else
                        {
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " onclick='insertproductstatus($(this))' disabled='disabled'/> &nbsp;&nbsp;" + _sbp.StatusName;
                        }
                        LS.Add(S);
                    }
                }

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                LS = new List<Status>();

                foreach (Status _s in s)
                {
                    S = new Status();

                    S.Active = _s.Active;
                    S.StatusId = _s.StatusId;
                    S.StatusName = "<input type='checkbox' value=" + _s.StatusId + " onclick='insertproductstatus($(this))' disabled='disabled'/> &nbsp;&nbsp;" + _s.StatusName;

                    LS.Add(S);
                }

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertmentionatedproducts(string Product, string Client, string Edition)
        {
            MentionatedProducts MentionatedProducts = new Models.MentionatedProducts();

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var pp = db.ParticipantProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();
            if (pp.LongCount() > 0)
            {
                var ppc = db.ParticipantProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId && x.HTMLContent != null).ToList();
                if (ppc.LongCount() > 0)
                {
                    return Json("_existContent", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var delete = db.ParticipantProducts.SingleOrDefault(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId);
                    db.ParticipantProducts.Remove(delete);
                    db.SaveChanges();
                }
            }

            var mp = db.MentionatedProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            if (mp.LongCount() == 0)
            {
                MentionatedProducts = new MentionatedProducts();

                MentionatedProducts.ClientId = ClientId;
                MentionatedProducts.EditionId = EditionId;
                MentionatedProducts.ProductId = ProductId;

                db.MentionatedProducts.Add(MentionatedProducts);
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult deletementionatedproducts(string Product, string Client, string Edition)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var mp = db.MentionatedProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();
            if (mp.LongCount() > 0)
            {
                var delete = db.MentionatedProducts.SingleOrDefault(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId);
                db.MentionatedProducts.Remove(delete);
                db.SaveChanges();

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}