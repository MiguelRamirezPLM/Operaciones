using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;

namespace GuiaNet.Controllers.Ventas
{
    public class MedicalProductsController : Controller
    {
        Guia db = new Guia();
        Products Products = new Products();
        createproduct createproduct = new createproduct();
        EditionClientMedicalProducts EditionClientMedicalProducts = new EditionClientMedicalProducts();
        ActivityLog ActivityLog = new ActivityLog();
        createpresentation createpresentation = new createpresentation();
        classreplace replace = new classreplace();
        Presentations Presentations = new Presentations();
        BarCodes BarCodes = new BarCodes();
        ClientProductBarCodes ClientProductBarCodes = new ClientProductBarCodes();
        ClientProducts ClientProducts = new ClientProducts();
        ClientProductSubstances ClientProductSubstances = new ClientProductSubstances();
        ClientMedicalProductsBarCode ClientMedicalProductsBarCode = new ClientMedicalProductsBarCode();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId)
        {
            sessionmedicalproducts sessionmp = (sessionmedicalproducts)Session["sessionmedicalproducts"];
            if (CountryId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + _clientid + ", @typeid=" + 1 + "").ToList();
                foreach (plm_vwProductsByClientRegisterandBarcode jmp in plm)
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
                sessionmedicalproducts medicalp = new sessionmedicalproducts(country, _clientid, edition, book);
                Session["sessionmedicalproducts"] = medicalp;
                return View(plm);
            }
            if (sessionmp != null)
            {
                int country = sessionmp.CId;
                int _clientid = sessionmp.ClId;
                int edition = sessionmp.EId;
                int book = sessionmp.BId;

                //var ind = (from cps in db.ClientProductSubstances
                //           join p in db.Products
                //           on cps.ProductId equals p.ProductId
                //           join ppf in db.ProductPharmaForms on cps.ProductId equals ppf.ProductId
                //           where cps.PharmaFormId == ppf.PharmaFormId
                //           join pf in db.PharmaceuticalForms on ppf.PharmaFormId equals pf.PharmaFormId
                //           where cps.PharmaFormId == pf.PharmaFormId
                //           join ps in db.ProductSubstances on cps.ActiveSubstanceId equals ps.ActiveSubstanceId
                //           join act in db.ActiveSubstances on cps.ActiveSubstanceId equals act.ActiveSubstanceId
                //           where ps.ActiveSubstanceId == act.ActiveSubstanceId
                //           && ps.PharmaFormId == ppf.PharmaFormId
                //           && ps.ProductId == cps.ProductId
                //           join pre in db.Presentations on ps.PresentationId equals pre.PresentationId
                //           where cps.PresentationId == pre.PresentationId
                //           where cps.ClientId == _clientid
                //           orderby p.ProductName ascending
                //           select new joinmedicalproduct
                //           {
                //               ActiveSubstances = act,
                //               PharmaceuticalForms = pf,
                //               Presentations = pre,
                //               ProductPharmaForms = ppf,
                //               Products = p,
                //               ProductSubstances = ps
                //           }).Distinct().OrderBy(x => x.Products.ProductName).ToList();

                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + _clientid + ", @typeid=" + 1 + "").ToList();

                ViewData["Count"] = 0;
                return View(plm);
            }
            else
            {
                //var ind = (from cps in db.ClientProductSubstances
                //           join p in db.Products
                //           on cps.ProductId equals p.ProductId
                //           join ppf in db.ProductPharmaForms on cps.ProductId equals ppf.ProductId
                //                                            where cps.PharmaFormId == ppf.PharmaFormId
                //           join pf in db.PharmaceuticalForms on ppf.PharmaFormId equals pf.PharmaFormId
                //                                            where cps.PharmaFormId == pf.PharmaFormId
                //           join ps in db.ProductSubstances on cps.ActiveSubstanceId equals ps.ActiveSubstanceId
                //           join act in db.ActiveSubstances on cps.ActiveSubstanceId equals act.ActiveSubstanceId
                //                                            where ps.ActiveSubstanceId == act.ActiveSubstanceId
                //                                            && ps.PharmaFormId == ppf.PharmaFormId
                //                                            && ps.ProductId == cps.ProductId
                //           join pre in db.Presentations on ps.PresentationId equals pre.PresentationId
                //                                        where cps.PresentationId == pre.PresentationId
                //           where cps.ClientId == 0
                //           orderby p.ProductName ascending
                //           select new joinmedicalproduct
                //           {
                //               ActiveSubstances = act,
                //               PharmaceuticalForms = pf,
                //               Presentations = pre,
                //               ProductPharmaForms = ppf,
                //               Products = p,
                //               ProductSubstances = ps
                //           }).ToList();
                ViewData["Count"] = 0;

                var plm = db.Database.SqlQuery<plm_vwProductsByClientRegisterandBarcode>("plm_spGetProductsByClients @clientId=" + 0 + ", @typeid=" + 1 + "").ToList();
                return View(plm);
            }
        }

        public ActionResult createnewproduct(string ProductN, string pharmaformid, string presentationid, string activesubstanceid, string editionid, string clientid, string registers, string barcode)
        {
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
                    bool response = createproduct.insertnewproduct(ProductN, pharmaformid, presentationid, activesubstanceid, editionid, clientid, ApplicationId, UsersId, BarCodeId, registers);

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
                            bool response = createproduct.insertnewproduct(ProductN, pharmaformid, presentationid, activesubstanceid, editionid, clientid, ApplicationId, UsersId, BarCodeId, registers);

                            return Json(response, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        foreach (BarCodes _bcodes in bcodes)
                        {
                            BarCodeId = _bcodes.BarCodeId;
                        }
                        bool response = createproduct.insertnewproduct(ProductN, pharmaformid, presentationid, activesubstanceid, editionid, clientid, ApplicationId, UsersId, BarCodeId, registers);

                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View();
        }

        public ActionResult editproducts(string PName, string ProductId, string regist, string Clientid, string BarCode, string BarCodeId, string pform, string substance, string presentation)
        {            
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;
            int BarCodeIdd = 0;
            int ClientId = int.Parse(Clientid);
            int PharmaFormId = int.Parse(pform);
            int ActiveSubstanceId = int.Parse(substance);
            int PresentationId = int.Parse(presentation);

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
                      where ptypes.Description == "Medicamentos Hospitalarios"
                      select ptypes).ToList();
            foreach (ProductTypes _pt in pt)
            {
                typeid = _pt.TypeId;
            }

            int prodid = int.Parse(ProductId);
            var prods = (from product in db.Products
                         where product.ProductId == prodid
                         && product.TypeId == typeid
                         select product).ToList();
            foreach (Products p in prods)
            {
                p.ProductName = PName.Trim();
            }
            db.SaveChanges();


            if ((regist != string.Empty) && (regist != null))
            {
                var cp = (from clientp in db.ClientProductSubstances
                          where clientp.ProductId == prodid
                          && clientp.ClientId == ClientId
                          && clientp.ActiveSubstanceId == ActiveSubstanceId
                          && clientp.PharmaFormId == PharmaFormId
                          && clientp.PresentationId == PresentationId
                          select clientp).ToList();
                foreach (ClientProductSubstances _cp in cp)
                {
                    _cp.RegisterSanitary = regist;

                    db.SaveChanges();
                }
            }

            if ((BarCodeId != string.Empty) && (BarCodeId != null))
            {
                BarCodeIdd = int.Parse(BarCodeId);

                var bc = (from barcc in db.BarCodes
                          where barcc.BarCodeId == BarCodeIdd
                          select barcc).ToList();
                if (bc.LongCount() > 0)
                {
                    foreach (BarCodes _bc in bc)
                    {
                        var bcc = (from barcc in db.BarCodes
                                  where barcc.BarCode == BarCode.Trim()
                                  select barcc).ToList();
                        if (bcc.LongCount() > 0)
                        {
                            var BarCd = BarCode.Trim();
                            return Json(BarCd, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            _bc.BarCode = BarCode;

                            db.SaveChanges();
                        }

                    }
                }
            }
            else
            {
                var bc = (from barcc in db.BarCodes
                          where barcc.BarCode == BarCode.Trim()
                          select barcc).ToList();
                if (bc.LongCount() > 0)
                {
                    var BarCd = BarCode.Trim();
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

                        var cpbc = (from cpb in db.ClientMedicalProductsBarCode
                                    where cpb.ClientId == ClientId
                                    && cpb.ProductId == prodid
                                    && cpb.BarCodeId == BarCdId
                                    && cpb.ActiveSubstanceId == ActiveSubstanceId
                                    && cpb.PharmaFormId == PharmaFormId
                                    && cpb.PresentationId == PresentationId
                                    select cpb).ToList();
                        if (cpbc.LongCount() == 0)
                        {
                            ClientMedicalProductsBarCode.ClientId = ClientId;
                            ClientMedicalProductsBarCode.BarCodeId = BarCdId;
                            ClientMedicalProductsBarCode.ProductId = prodid;
                            ClientMedicalProductsBarCode.ActiveSubstanceId = ActiveSubstanceId;
                            ClientMedicalProductsBarCode.PresentationId = PresentationId;
                            ClientMedicalProductsBarCode.PharmaFormId = PharmaFormId;

                            db.ClientMedicalProductsBarCode.Add(ClientMedicalProductsBarCode);
                            db.SaveChanges();
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

                        var cpbc = (from cpb in db.ClientMedicalProductsBarCode
                                    where cpb.ClientId == ClientId
                                    && cpb.ProductId == prodid
                                    && cpb.BarCodeId == BarCdId
                                    && cpb.ActiveSubstanceId == ActiveSubstanceId
                                    && cpb.PharmaFormId == PharmaFormId
                                    && cpb.PresentationId == PresentationId
                                    select cpb).ToList();
                        if (cpbc.LongCount() == 0)
                        {
                            ClientMedicalProductsBarCode.ClientId = ClientId;
                            ClientMedicalProductsBarCode.BarCodeId = BarCdId;
                            ClientMedicalProductsBarCode.ProductId = prodid;
                            ClientMedicalProductsBarCode.ActiveSubstanceId = ActiveSubstanceId;
                            ClientMedicalProductsBarCode.PresentationId = PresentationId;
                            ClientMedicalProductsBarCode.PharmaFormId = PharmaFormId;

                            db.ClientMedicalProductsBarCode.Add(ClientMedicalProductsBarCode);
                            db.SaveChanges();
                        }
                    }
                }
            }

            ActivityLog.editproduct(PName, prodid, ApplicationId, UsersId, alphabetid, typeid);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertparticipantproducts(string productid, string clientid, string editionid, string pharmaformid, string activesubstanceid, string presentationid)
        {
            int product = int.Parse(productid);
            int client = int.Parse(clientid);
            int edition = int.Parse(editionid);
            int pharmaform = int.Parse(pharmaformid);
            int activesubstance = int.Parse(activesubstanceid);
            int presentation = int.Parse(presentationid);

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            var cp = (from Clp in db.ClientProducts
                      where Clp.ClientId == client
                      && Clp.ProductId == product
                      select Clp).ToList();
            if (cp.LongCount() == 0)
            {
                ClientProducts.ClientId = client;
                ClientProducts.ProductId = product;

                db.ClientProducts.Add(ClientProducts);
                db.SaveChanges();

                ActivityLog._insertclientproducts(client, product, ApplicationId, UsersId);
            }

            var cps = (from cls in db.ClientProductSubstances
                       where cls.ActiveSubstanceId == activesubstance
                       && cls.ClientId == client
                       && cls.PharmaFormId == pharmaform
                       && cls.PresentationId == presentation
                       && cls.ProductId == product
                       select cls).ToList();
            if (cps.LongCount() == 0)
            {
                ClientProductSubstances.ActiveSubstanceId = activesubstance;
                ClientProductSubstances.ClientId = client;
                ClientProductSubstances.PharmaFormId = pharmaform;
                ClientProductSubstances.PresentationId = presentation;
                ClientProductSubstances.ProductId = product;

                db.ClientProductSubstances.Add(ClientProductSubstances);
                db.SaveChanges();

                ActivityLog._insertclientproductsubstances(activesubstance, client, pharmaform, presentation, product, ApplicationId, UsersId);
            }


            var pp = (from ecmp in db.EditionClientMedicalProducts
                      where ecmp.EditionId == edition
                      && ecmp.ClientId == client
                      && ecmp.ProductId == product
                      && ecmp.PharmaFormId == pharmaform
                      && ecmp.ActiveSubstanceId == activesubstance
                      && ecmp.PresentationId == presentation
                      select ecmp).ToList();
            if (pp.LongCount() == 0)
            {
                byte clientypeid = 0;
                var ct = from clientt in db.ClientTypes
                         where clientt.TypeName == "ANUNCIANTE"
                         select clientt;
                foreach (ClientTypes cct in ct)
                {
                    clientypeid = cct.ClientTypeId;
                }

                try
                {
                    EditionClientMedicalProducts = new Models.EditionClientMedicalProducts();

                    EditionClientMedicalProducts.ClientId = client;
                    EditionClientMedicalProducts.EditionId = edition;
                    EditionClientMedicalProducts.ProductId = product;
                    EditionClientMedicalProducts.PharmaFormId = pharmaform;
                    EditionClientMedicalProducts.ActiveSubstanceId = activesubstance;
                    EditionClientMedicalProducts.PresentationId = presentation;

                    db.EditionClientMedicalProducts.Add(EditionClientMedicalProducts);
                    db.SaveChanges();

                    ActivityLog._insertparticipantmedicalproducts(client, edition, product, pharmaform, activesubstance, presentation, ApplicationId, UsersId);
                }
                catch (Exception e)
                {
                    var exc = e.Message;
                }
            }

            return View();
        }

        public ActionResult deleteparticipantproducts(string productid, string clientid, string editionid, string pharmaformid, string activesubstanceid, string presentationid)
        {
            int product = int.Parse(productid);
            int client = int.Parse(clientid);
            int edition = int.Parse(editionid);
            int pharmaform = int.Parse(pharmaformid);
            int activesubstance = int.Parse(activesubstanceid);
            int presentation = int.Parse(presentationid);

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            var pp = (from ecmp in db.EditionClientMedicalProducts
                      where ecmp.EditionId == edition
                      && ecmp.ClientId == client
                      && ecmp.ProductId == product
                      && ecmp.PharmaFormId == pharmaform
                      && ecmp.ActiveSubstanceId == activesubstance
                      && ecmp.PresentationId == presentation
                      select ecmp).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (EditionClientMedicalProducts p in pp)
                {
                    var delete = db.EditionClientMedicalProducts.SingleOrDefault(x => x.ProductId == product && x.ClientId == client
                        && x.EditionId == edition && x.PharmaFormId == pharmaform && x.ActiveSubstanceId == activesubstance
                        && x.PresentationId == presentation);

                    db.EditionClientMedicalProducts.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._deleteparticipantmedicalproducts(client, edition, product, pharmaform, activesubstance, presentation, ApplicationId, UsersId);
                }
            }
            return View();
        }

        public JsonResult searchpredictive(string term, string country)
        {
            int CountryId = int.Parse(country);
            List<string> Products = new List<string>();
            List<plm_vwMedicalProductsByClient> plm = new List<plm_vwMedicalProductsByClient>();
            var pname = from cp in db.ClientProducts
                        join p in db.Products
                        on cp.ProductId equals p.ProductId
                        join ppf in db.ProductPharmaForms
                        on cp.ProductId equals ppf.ProductId
                        where ppf.ProductId == p.ProductId
                        join c in db.Clients
                        on cp.ClientId equals c.ClientId
                        join pf in db.PharmaceuticalForms
                        on ppf.PharmaFormId equals pf.PharmaFormId
                        where c.CountryId == CountryId
                        select new
                        {
                            ProductName = p.ProductName,
                            CompanyName = c.CompanyName,
                            PharmaForm = pf.PharmaForm
                        };

            var prod = (from prodcs in pname
                        where prodcs.ProductName.ToUpper().StartsWith(term.ToUpper())
                        select prodcs).Distinct().OrderBy(x => x.ProductName).ToList();

            foreach (var p in prod)
            {
                string _response = ("" + p.ProductName + ", " + p.CompanyName + ", " + p.PharmaForm + "");
                Products.Add(_response);
            }
            return Json(Products, JsonRequestBehavior.AllowGet);
        }

        public ActionResult createnewpresentation(string Description)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string _description = Description;

            _description = classreplace.replace(_description.ToUpper());

            _description = _description.Trim();
            string descriptiondatabase = string.Empty;
            string _presentation = string.Empty;
            int presentationid = 0;

            List<Presentations> la = new List<Presentations>();
            Presentations pa = new Presentations();

            var pre = (from pr in db.Presentations
                       select pr).ToList();



            foreach (Presentations pp in pre)
            {
                pa = new Models.Presentations();
                descriptiondatabase = pp.Description.Trim();
                descriptiondatabase = classreplace.descriptiondatabasereplace(descriptiondatabase.ToUpper());
                pa.Description = descriptiondatabase;
                pa.PresentationId = pp.PresentationId;
                la.Add(pa);
            }

            foreach (Presentations PPP in la)
            {
                if (_description.Equals(PPP.Description.Trim()))
                {
                    _presentation = PPP.Description.Trim();
                    presentationid = PPP.PresentationId;
                }
            }
            var pd = (from pps in la
                      where pps.Description.Trim() == _presentation.Trim()
                      select pps).ToList();
            if (pd.LongCount() > 0)
            {
                var psdfs = (from ds in db.Presentations
                             where ds.PresentationId == presentationid
                             select ds).ToList();
                foreach (Presentations pp in psdfs)
                {
                    _presentation = pp.Description;
                }
                return Json(_presentation, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Presentations.Description = Description.Trim();
                Presentations.Active = true;

                db.Presentations.Add(Presentations);
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult searchproduct(string ProductName)
        {
            if (ProductName != null)
            {
                if (ProductName == string.Empty)
                {
                    return RedirectToAction("Index", "MedicalProducts");
                }
                else
                {
                    int count = 0;
                    sessionmedicalproducts sessionmp = (sessionmedicalproducts)Session["sessionmedicalproducts"];
                    int _clientid = sessionmp.ClId;
                    int typeid = 0;
                    var type = (from types in db.ProductTypes
                                where types.Description == "Medicamentos Hospitalarios"
                                select types).ToList();
                    foreach (ProductTypes t in type)
                    {
                        typeid = t.TypeId;
                    }
                    //var ind = (from cps in db.ClientProductSubstances
                    //           join p in db.Products
                    //           on cps.ProductId equals p.ProductId
                    //           join ppf in db.ProductPharmaForms on cps.ProductId equals ppf.ProductId
                    //           where cps.PharmaFormId == ppf.PharmaFormId
                    //           join pf in db.PharmaceuticalForms on ppf.PharmaFormId equals pf.PharmaFormId
                    //           where cps.PharmaFormId == pf.PharmaFormId
                    //           join ps in db.ProductSubstances on cps.ActiveSubstanceId equals ps.ActiveSubstanceId
                    //           join act in db.ActiveSubstances on cps.ActiveSubstanceId equals act.ActiveSubstanceId
                    //           where ps.ActiveSubstanceId == act.ActiveSubstanceId
                    //           && ps.PharmaFormId == ppf.PharmaFormId
                    //           && ps.ProductId == cps.ProductId
                    //           join pre in db.Presentations on ps.PresentationId equals pre.PresentationId
                    //           where cps.PresentationId == pre.PresentationId
                    //           where cps.ClientId == _clientid
                    //           && p.ProductName.StartsWith(ProductName)
                    //           && p.TypeId == typeid
                    //           orderby p.ProductName ascending
                    //           select new joinmedicalproduct
                    //           {
                    //               ActiveSubstances = act,
                    //               PharmaceuticalForms = pf,
                    //               Presentations = pre,
                    //               ProductPharmaForms = ppf,
                    //               Products = p,
                    //               ProductSubstances = ps
                    //           }).Distinct().OrderBy(x => x.Products.ProductName).ToList();

                    //foreach (joinmedicalproduct jmp in ind)
                    //{
                    //    count = count + 1;
                    //}
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
            return RedirectToAction("Index", "MedicalProducts");
        }

        public JsonResult getpresentation()
        {
            List<Presentations> lpf = new List<Presentations>();
            Presentations Presentations = new Models.Presentations();

            var pharmaf = (from pform in db.Presentations
                           orderby pform.Description ascending
                           select pform).ToList();
            foreach (Presentations pf in pharmaf)
            {
                Presentations = new Presentations();

                Presentations.Active = pf.Active;
                Presentations.Description = pf.Description;
                Presentations.PresentationId = pf.PresentationId;

                lpf.Add(Presentations);
            }
            return Json(lpf, JsonRequestBehavior.AllowGet);
        }
    }
}