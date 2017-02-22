using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guianet.Models;
using Guianet.Models.Sessions;

namespace Guianet.Controllers.InternationalProducts
{
    public class IPController : Controller
    {
        GuiaEntities db = new GuiaEntities();
        CRUD CRUD = new CRUD();
        GetData GetData = new GetData();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string ProductName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }
            SessionIP index = (SessionIP)Session["SessionIP"];
            if (CountryId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + _clientid + ", @EditionId=" + EditionId + ", @InternationalProductName='" + ProductName + "'").ToList();

                foreach (GetProductsByClient p in plm)
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

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchIP SearchIP = new SearchIP(ProductName);
                    Session["SearchIP"] = SearchIP;
                }
                else
                {
                    Session["SearchIP"] = null;
                }

                SessionIP SessionIP = new SessionIP(country, _clientid, edition, book, null);
                Session["SessionIP"] = SessionIP;
                return View(plm);
            }
            if (index != null)
            {
                int country = index.CId;
                int _clientid = index.ClId;
                int edition = index.EId;
                int book = index.BId;

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + _clientid + ", @EditionId=" + edition + ", @InternationalProductName='" + ProductName + "'").ToList();
                ViewData["Count"] = 0;

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchIP SearchIP = new SearchIP(ProductName);
                    Session["SearchIP"] = SearchIP;
                }
                else
                {
                    Session["SearchIP"] = null;
                }

                SessionIP SessionIP = new SessionIP(country, _clientid, edition, book, null);
                Session["SessionIP"] = SessionIP;

                return View(plm);
            }
            else
            {

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + 0 + ", @EditionId=" + 0 + "").ToList();
                ViewData["Count"] = 0;
                return View(plm);
            }
        }

        public JsonResult AddProduct(string Product, string Client, string Edition)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int InternationalProductId = 0;
            string Letter = Product.Substring(0, 1);
            int AlphabetId = GetData.GetAlphabetId(Letter);

            var ct = db.ClientTypes.Where(x => x.TypeName == "INTERNACIONAL").ToList();

            var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "").ToList();

            if (_ec.LongCount() == 0)
            {
                var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);
            }

            var _result = db.Database.SqlQuery<Guianet.Models.InternationalProducts>("plm_spCRUDInternationalProducts @InternationalProductName='" + Product.Trim() + "', @CRUDType=" + CRUD.Read + "").ToList();

            if (_result.LongCount() == 0)
            {
                var ins = db.Database.SqlQuery<int>("plm_spCRUDInternationalProducts @InternationalProductName='" + Product.Trim() + "', @CRUDType=" + CRUD.Create + ", @AlphabetId=" + AlphabetId + "").ToList();

                InternationalProductId = ins[0];

                ActivityLog.InternationalProducts(InternationalProductId, Product.Trim(), AlphabetId, 1);
            }
            else
            {
                InternationalProductId = _result[0].InternationalProductId;
            }

            var icp = db.InternationalClientProducts.Where(x => x.ClientId == ClientId && x.InternationalProductId == InternationalProductId).ToList();

            if (icp.LongCount() == 0)
            {
                InternationalClientProducts InternationalClientProducts = new InternationalClientProducts();

                InternationalClientProducts.ClientId = ClientId;
                InternationalClientProducts.InternationalProductId = InternationalProductId;

                db.InternationalClientProducts.Add(InternationalClientProducts);
                db.SaveChanges();

                ActivityLog.InternationalClientProducts(InternationalProductId, ClientId, 1);

            }

            var iecp = db.InternationalEditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.InternationalProductId == InternationalProductId).ToList();

            if (iecp.LongCount() == 0)
            {
                InternationalEditionClientProducts InternationalEditionClientProducts = new InternationalEditionClientProducts();

                InternationalEditionClientProducts.ClientId = ClientId;
                InternationalEditionClientProducts.EditionId = EditionId;
                InternationalEditionClientProducts.InternationalProductId = InternationalProductId;

                db.InternationalEditionClientProducts.Add(InternationalEditionClientProducts);
                db.SaveChanges();

                ActivityLog.InternationalEditionClientProducts(InternationalProductId, ClientId, EditionId, 1);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProduct(string Product, string Client, string InternationalProduct)
        {
            string Letter = Product.Substring(0, 1);
            int AlphabetId = GetData.GetAlphabetId(Letter);

            int InternationalProductId = int.Parse(InternationalProduct);

            var _prods = db.InternationalProducts.Where(x => x.InternationalProductName.ToUpper().Trim() == Product.ToUpper().Trim() && x.InternationalProductId != InternationalProductId).ToList();

            if (_prods.LongCount() > 0)
            {
                return Json("_existProduct", JsonRequestBehavior.AllowGet);
            }

            var ins = db.Database.ExecuteSqlCommand("plm_spCRUDInternationalProducts @InternationalProductName='" + Product.Trim() + "', @CRUDType=" + CRUD.Update + ", @InternationalProductId=" + InternationalProductId + "");

            ActivityLog.InternationalProducts(InternationalProductId, Product.Trim(), AlphabetId, 2);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InternationalClients(string CountryId, string EditionId, string BookId,string CompanyName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            List<ClientTypes> CT = db.ClientTypes.Where(x => x.TypeName == "INTERNACIONAL").ToList();

            SessionIC ind = (SessionIC)Session["SessionIC"];

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                //int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                SessionIC SessionIC = new SessionIC(country, null, edition, book, null);
                Session["SessionIC"] = SessionIC;

                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + edition + ", @ClientTypeId=" + CT[0].ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();

                return View(LS);
            }
            if (ind != null)
            {
                int country = ind.CId;
                // int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionIC SessionIC = new SessionIC(country, null, edition, book, null);
                Session["SessionIC"] = SessionIC;

                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + edition + ", @ClientTypeId=" + CT[0].ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();

                return View(LS);
            }
            else
            {
                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LS);
            }
        }

        public ActionResult AddressInternationalClients(int? ClientId)
        {
            SessionIC ind = (SessionIC)Session["SessionIC"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ca = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            ind.ClId = ClientId;

            return View(ca);
        }

        public ActionResult ClientPhones(int? ClientId, int? AddressId, int? CountryId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if ((ClientId == null) || (AddressId == null) || (CountryId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddressId SessionAddressId = new Models.Sessions.SessionAddressId(AddressId, CountryId);
            Session["SessionAddressId"] = SessionAddressId;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public JsonResult GetStatesByCountry(string Country)
        {
            int CountryId = int.Parse(Country);

            List<States> s = new List<States>();
            States st = new States();

            var ss = db.States.Where(x => x.Active == true && x.CountryId == CountryId).OrderBy(x => x.StateName).ToList();

            foreach (States _s in ss)
            {
                st = new States();

                st.StateName = _s.StateName;
                st.StateId = _s.StateId;
                st.CountryId = _s.CountryId;
                st.Active = _s.Active;

                s.Add(st);
            }

            return Json(s, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InternationalClientsProd(string CountryId, string EditionId, string BookId, string CompanyName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            List<ClientTypes> CT = db.ClientTypes.Where(x => x.TypeName == "INTERNACIONAL").ToList();

            SessionICProd ind = (SessionICProd)Session["SessionICProd"];

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                SessionICProd SessionICProd = new SessionICProd(country, null, edition, book, null);
                Session["SessionICProd"] = SessionICProd;

                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + edition + ", @ClientTypeId=" + CT[0].ClientTypeId + ",@Type=P, @CompanyName='" + CompanyName + "'").ToList();

                return View(LS);
            }
            if (ind != null)
            {
                int country = ind.CId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionICProd SessionICProd = new SessionICProd(country, null, edition, book, null);
                Session["SessionICProd"] = SessionICProd;

                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + edition + ", @ClientTypeId=" + CT[0].ClientTypeId + ",@Type=P, @CompanyName='" + CompanyName + "'").ToList();

                return View(LS);
            }
            else
            {
                List<GetInternationalClients> LS = db.Database.SqlQuery<GetInternationalClients>("plm_spGetInternationalClients @EditionId=" + 0 + ", @ClientTypeId=" + 0 + ", @Type=P").ToList();

                return View(LS);
            }
        }

        public ActionResult AddressInternationalClientsProd(int? ClientId)
        {
            SessionICProd ind = (SessionICProd)Session["SessionICProd"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            var ca = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            ind.ClId = ClientId;

            return View(ca);
        }

        public ActionResult ClientPhonesProd(int? ClientId, int? AddressId, int? CountryId)
        {
            if ((ClientId == null) || (AddressId == null))
            {
                return RedirectToAction("Logout", "Login");
            }
            SessionAddressId SessionAddressId = new Models.Sessions.SessionAddressId(AddressId, CountryId);
            Session["SessionAddressId"] = SessionAddressId;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public JsonResult checkparticipantproductIP(string Edition, string Client, string InternationalProduct, string Operation)
        {
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);
            int InternationalProductId = int.Parse(InternationalProduct);

            if (Operation == "Insert")
            {

                var ct = db.ClientTypes.Where(x => x.TypeName == "INTERNACIONAL").ToList();

                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() == 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);
                }

                var iecp = db.InternationalEditionClientProducts.Where(x => x.EditionId == EditionId && x.ClientId == ClientId && x.InternationalProductId == InternationalProductId).ToList();

                if (iecp.LongCount() == 0)
                {
                    InternationalEditionClientProducts InternationalEditionClientProducts = new InternationalEditionClientProducts();

                    InternationalEditionClientProducts.ClientId = ClientId;
                    InternationalEditionClientProducts.EditionId = EditionId;
                    InternationalEditionClientProducts.InternationalProductId = InternationalProductId;

                    db.InternationalEditionClientProducts.Add(InternationalEditionClientProducts);
                    db.SaveChanges();

                    ActivityLog.InternationalEditionClientProducts(InternationalProductId, ClientId, EditionId, 1);
                }
            }
            else
            {
                var iecp = db.InternationalEditionClientProducts.Where(x => x.EditionId == EditionId && x.ClientId == ClientId && x.InternationalProductId == InternationalProductId).ToList();

                if (iecp.LongCount() > 0)
                {
                    var delete = db.InternationalEditionClientProducts.SingleOrDefault(x => x.EditionId == EditionId && x.ClientId == ClientId && x.InternationalProductId == InternationalProductId);
                    db.InternationalEditionClientProducts.Remove(delete);
                    db.SaveChanges();

                    ActivityLog.InternationalEditionClientProducts(InternationalProductId, ClientId, EditionId, 4);

                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Geolocalization(string CountryId, string ClientId, string EditionId, string BookId, string ClientTypeId, string CompanyName, string Type)
        {
            SessionICLI ind = (SessionICLI)Session["SessionICLI"];

            if(!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout","Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);
                byte _ClientTypeId = Convert.ToByte(ClientTypeId);

                //SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId);
                //Session["SessionICLI"] = SessionICLI;

                List<GetClients> LC = new List<GetClients>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionICLI"] = SessionICLI;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();
                    }
                    else
                    {
                        SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionICLI"] = SessionICLI;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, null);
                    Session["SessionICLI"] = SessionICLI;

                    LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();
                }

                return View(LC);
            }
            if (ind != null)
            {
                int country = ind.CId;
                int edition = ind.EId;
                int book = ind.BId;
                byte _ClientTypeId = Convert.ToByte(ind.CTId);

                if ((ind.Type != null) && (string.IsNullOrEmpty(Type)))
                {
                    Type = ind.Type.ToString();
                }

                List<GetClients> LC = new List<GetClients>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionICLI"] = SessionICLI;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();
                    }
                    else
                    {
                        SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionICLI"] = SessionICLI;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionICLI SessionICLI = new SessionICLI(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                    Session["SessionICLI"] = SessionICLI;

                    LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "', @EditionId=" + edition + "").ToList();
                }

                return View(LC);
            }
            else
            {
                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public JsonResult updateGeolocalization(string Address, string Latitud, string Longitud)
        {
            int AddressId = int.Parse(Address);

            var addr = db.Addresses.Where(x => x.AddressId == AddressId && x.Active == true).ToList();

            if (addr.LongCount() > 0)
            {
                foreach (Addresses _addr in addr)
                {
                    if (!string.IsNullOrEmpty(Longitud))
                    {
                        _addr.Longitud = Longitud.Trim();
                    }
                    else
                    {
                        _addr.Longitud = null;
                    }

                    if (!string.IsNullOrEmpty(Latitud))
                    {
                        _addr.Latitud = Latitud.Trim();
                    }
                    else
                    {
                        _addr.Latitud = null;
                    }

                    db.SaveChanges();

                    ActivityLog.Geolocalization(AddressId, Latitud, Longitud, 2);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientDescription(string Client, string Description)
        {
            int ClientId = int.Parse(Client);

            var cl = db.Clients.Where(x => x.ClientId == ClientId && x.Active == true).ToList();

            if (cl.LongCount() > 0)
            {
                foreach (Clients _cl in cl)
                {
                    if (!string.IsNullOrEmpty(Description))
                    {
                        _cl.Description = Description.Trim();
                    }
                    else
                    {
                        _cl.Description = null;
                    }
                }

                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexProd(string CountryId, string ClientId, string EditionId, string BookId, string ProductName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }
            SessionIPProd index = (SessionIPProd)Session["SessionIPProd"];
            if (CountryId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + _clientid + ", @EditionId=" + EditionId + ", @InternationalProductName='" + ProductName + "'").ToList();

                foreach (GetProductsByClient p in plm)
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

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchIPProd SearchIPProd = new SearchIPProd(ProductName);
                    Session["SearchIPProd"] = SearchIPProd;
                }
                else
                {
                    Session["SearchIPProd"] = null;
                }

                SessionIPProd SessionIPProd = new SessionIPProd(country, _clientid, edition, book, null);
                Session["SessionIPProd"] = SessionIPProd;
                return View(plm);
            }
            if (index != null)
            {
                int country = index.CId;
                int _clientid = index.ClId;
                int edition = index.EId;
                int book = index.BId;

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + _clientid + ", @EditionId=" + edition + ", @InternationalProductName='" + ProductName + "'").ToList();
                ViewData["Count"] = 0;

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchIPProd SearchIPProd = new SearchIPProd(ProductName);
                    Session["SearchIPProd"] = SearchIPProd;
                }
                else
                {
                    Session["SearchIPProd"] = null;
                }

                SessionIPProd SessionIPProd = new SessionIPProd(country, _clientid, edition, book, null);
                Session["SessionIPProd"] = SessionIPProd;

                return View(plm);
            }
            else
            {

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + 0 + ", @EditionId=" + 0 + "").ToList();
                ViewData["Count"] = 0;
                return View(plm);
            }
        }

        public ActionResult Address(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionICLI ind = (SessionICLI)Session["SessionICLI"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public JsonResult Participant(string Client, string Edition, string Operation)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "INTERNACIONAL").ToList();

            if (Operation == "Insert")
            {
                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() == 0)
                {
                    try
                    {
                        var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                        ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {

                var iecp = db.InternationalEditionClientProducts.Where(x => x.EditionId == EditionId && x.ClientId == ClientId).ToList();

                if (iecp.LongCount() > 0)
                {
                    foreach (InternationalEditionClientProducts item in iecp)
                    {
                        var delete = db.InternationalEditionClientProducts.SingleOrDefault(x => x.EditionId == EditionId && x.ClientId == ClientId && x.InternationalProductId == item.InternationalProductId);
                        db.InternationalEditionClientProducts.Remove(delete);
                        db.SaveChanges();

                        ActivityLog.InternationalEditionClientProducts(item.InternationalProductId, ClientId, EditionId, 4);
                    }

                }

                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() > 0)
                {
                    try
                    {
                        var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Delete + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                        ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 4);

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception e)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuantityHospitals(string Type, string Num)
        {

            if (string.IsNullOrEmpty(Type))
            {
                Type = null;
            }

            if (!string.IsNullOrEmpty(Num))
            {
                int Quantity = int.Parse(Num);

                SessionTypesLIH SessionTypesLIH = new SessionTypesLIH(Quantity, Type);
                Session["SessionTypesLIH"] = SessionTypesLIH;
            }
            else
            {
                SessionTypesLIH SessionTypesLIH = new SessionTypesLIH(null, Type);
                Session["SessionTypesLIH"] = SessionTypesLIH;
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}