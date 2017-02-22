using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guianet.Models;
using Guianet.Models.Sessions;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using HtmlAgilityPack;

namespace Guianet.Controllers.Production
{
    public class ProductionController : Controller
    {
        PLMUsers PLMUsers = new PLMUsers();
        GuiaEntities db = new GuiaEntities();
        GetData GetData = new GetData();
        ActivityLog ActivityLog = new ActivityLog();
        CRUD CRUD = new CRUD();
        SegmentContent SegmentContent = new SegmentContent();
        public List<HTMLAttributes> ListAttributesAsoc = new List<HTMLAttributes>();
        ClassReplace ClassReplace = new ClassReplace();



        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string ProductName)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + Client + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                SessionProduction SessionProduction = new SessionProduction(Country, Client, Edition, Book, null);
                Session["SessionProduction"] = SessionProduction;


                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameCL SearchProductNameCL = new SearchProductNameCL(ProductName);
                    Session["SearchProductNameCL"] = SearchProductNameCL;
                }
                else
                {
                    Session["SearchProductNameCL"] = null;
                }

                return View(li);
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + Client + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                SessionProduction SessionProduction = new SessionProduction(Country, Client, Edition, Book, null);
                Session["SessionProduction"] = SessionProduction;

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameCL SearchProductNameCL = new SearchProductNameCL(ProductName);
                    Session["SearchProductNameCL"] = SearchProductNameCL;
                }
                else
                {
                    Session["SearchProductNameCL"] = null;
                }

                return View(li);
            }
            else
            {
                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + 0 + ", @EditionId=" + 0 + "").ToList();

                return View(li);
            }

        }

        public JsonResult EditProduct(string Product, string ProductName, string Register, string BarCode, string Client, string BarCodeI)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            string Letter = ProductName.Substring(0, 1);

            int AlphabetId = GetData.GetAlphabetId(Letter);

            var _prods = db.Products.Where(x => x.ProductName.Trim() == ProductName.Trim() && x.ProductId != ProductId).ToList();

            if (_prods.LongCount() > 0)
            {
                return Json("_existProduct", JsonRequestBehavior.AllowGet);
            }

            try
            {
                if (!string.IsNullOrEmpty(Register.Trim()))
                {
                    var _resultCP = db.Database.SqlQuery<ClientProducts>("plm_spCRUDClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'").ToList();

                    if (_resultCP.LongCount() > 0)
                    {
                        if (!string.IsNullOrEmpty(Register))
                        {
                            var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");
                        }
                        else
                        {
                            var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + null + "'");
                        }

                        ActivityLog.createClientProducts(ProductId, ClientId, Register, 2);
                    }
                }
                else
                {
                    var _resultCP = db.Database.SqlQuery<ClientProducts>("plm_spCRUDClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + null + "'").ToList();

                    if (_resultCP.LongCount() > 0)
                    {
                        if (!string.IsNullOrEmpty(Register))
                        {
                            var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");
                        }
                        else
                        {
                            var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + null + "'");
                        }
                        ActivityLog.createClientProducts(ProductId, ClientId, Register, 2);
                    }
                }

            }
            catch (SqlException e)
            {
                string _error = e.Message;

                if (_error == "_RegisterSanitaryExist")
                {
                    return Json("_RegisterSanitaryExist", JsonRequestBehavior.AllowGet);
                }
            }
            if (!string.IsNullOrEmpty(BarCode.Trim()))
            {
                if (!string.IsNullOrEmpty(BarCodeI))
                {
                    int BarCodeId = int.Parse(BarCodeI);

                    try
                    {
                        var _resultBC = db.Database.SqlQuery<BarCodes>("plm_spCRUDBarCodes @CRUDType=" + CRUD.Read + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + ", @BarCodeId=" + BarCodeId + "").ToList();
                    }
                    catch (SqlException e)
                    {
                        string message = e.Message;

                        if (message == "_alreadyexistBarCode")
                        {
                            return Json("_alreadyexistBarCode", JsonRequestBehavior.AllowGet);
                        }

                        if (message == "_updateBarCode")
                        {
                            try
                            {
                                var _resultBC = db.Database.ExecuteSqlCommand("plm_spCRUDBarCodes @CRUDType=" + CRUD.Update + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + ", @BarCodeId=" + BarCodeId + "");

                                ActivityLog.createBarCodes(BarCodeId, BarCode, ClientId, ProductId, 2);
                            }
                            catch (Exception ex)
                            {
                                string _errormessage = ex.Message;

                                if (_errormessage == "_existBarCode")
                                {
                                    return Json("_existBarCode", JsonRequestBehavior.AllowGet);
                                }
                            }
                        }

                        if (message == "_notexistBarCode")
                        {
                            try
                            {
                                var _resultBC = db.Database.ExecuteSqlCommand("plm_spCRUDBarCodes @CRUDType=" + CRUD.Create + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + ", @BarCodeId=" + BarCodeId + "");

                                ActivityLog.createBarCodes(BarCodeId, BarCode, ClientId, ProductId, 1);

                                ActivityLog.createClientProductBarCodes(BarCodeId, ClientId, ProductId, 1);
                            }
                            catch (Exception ex)
                            {
                                string _errormessage = ex.Message;
                            }
                        }
                    }
                }
                else
                {
                    try
                    {
                        var _resultBC = db.Database.ExecuteSqlCommand("plm_spCRUDBarCodes @CRUDType=" + CRUD.Create + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "");

                        var bc = db.BarCodes.Where(x => x.BarCode.ToUpper().Trim() == BarCode.ToUpper().Trim()).ToList();

                        ActivityLog.createBarCodes(bc[0].BarCodeId, BarCode, ClientId, ProductId, 1);

                        ActivityLog.createClientProductBarCodes(bc[0].BarCodeId, ClientId, ProductId, 1);
                    }
                    catch (SqlException ex)
                    {
                        string message = ex.Message;

                        if (message == "_alreadyexistBarCode")
                        {
                            return Json("_alreadyexistBarCode", JsonRequestBehavior.AllowGet);
                        }
                    }

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(BarCodeI))
                {
                    int BarCodeId = int.Parse(BarCodeI);

                    return Json("_barcodeempty", JsonRequestBehavior.AllowGet);
                }
            }

            var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ProductName='" + ProductName + "'");

            ActivityLog.editproduct(ProductName, ProductId, AlphabetId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddHTML(int? ProductId)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            if ((!Request.IsAuthenticated) || (ProductId == null) || (ind == null))
            {
                return RedirectToAction("Logout", "Login");
            }


            ind.PId = ProductId;

            List<HTMLAttributes> LA = GetAttributes(ProductId, ind.ClId, ind.EId);

            return View(LA);
        }

        public static string Cleanstring(string _string)
        {

            _string = _string.Replace("á", "a");
            _string = _string.Replace("é", "e");
            _string = _string.Replace("í", "i");
            _string = _string.Replace("ó", "o");
            _string = _string.Replace("ú", "u");
            _string = _string.Replace("ü", "u");

            _string = _string.Replace("-", "_");
            _string = _string.Replace(".", "");
            _string = _string.Replace(",", "");
            //_string = _string.Replace("", "");
            //_string = _string.Replace("", "");

            return _string;
        }


        /************************           PRODUCTSTATUS           ***************************************/

        public JsonResult getproductstatus(string Product, string Client)
        {
            List<Status> LS = new List<Status>();
            Status S = new Status();

            List<int> LI = new List<int>();

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            var s = db.Status.Where(x => x.Active == true).OrderBy(x => x.StatusName).ToList();

            var ps = db.Database.SqlQuery<StatusByProduct>("plm_spGetProductStatus @ProductId=" + ProductId + ", @ClientId=" + ClientId + "").ToList();

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
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " checked='checked' onclick='insertproductstatusProd($(this))'/> &nbsp;&nbsp;" + _sbp.StatusName;
                        }
                        else
                        {
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " onclick='insertproductstatusProd($(this))' /> &nbsp;&nbsp;" + _sbp.StatusName;
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
                    S.StatusName = "<input type='checkbox' value=" + _s.StatusId + " onclick='insertproductstatusProd($(this))'/> &nbsp;&nbsp;" + _s.StatusName;

                    LS.Add(S);
                }

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertproductstatus(string Product, string Client, string Status, string Operation)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;


            ProductStatus ProductStatus = new Models.ProductStatus();
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int StatusId = int.Parse(Status);

            if (Operation == "Insert")
            {
                var ps = db.ProductStatus.Where(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId).ToList();

                if (ps.LongCount() == 0)
                {
                    ProductStatus = new ProductStatus();

                    ProductStatus.ClientId = ClientId;
                    ProductStatus.ProductId = ProductId;
                    ProductStatus.StatusId = StatusId;

                    db.ProductStatus.Add(ProductStatus);
                    db.SaveChanges();

                    ActivityLog._ProductStatus(ClientId, ProductId, StatusId, 1, ApplicationId, UsersId);
                }
            }
            else
            {
                var ps = db.ProductStatus.Where(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId).ToList();

                if (ps.LongCount() > 0)
                {
                    var delete = db.ProductStatus.SingleOrDefault(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId);

                    db.ProductStatus.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ProductStatus(ClientId, ProductId, StatusId, 4, ApplicationId, UsersId);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANCH          ************************ */

        public ActionResult Branchs(string CountryId, string ClientId, string EditionId, string BookId)
        {
            SessionBranchProd ind = (SessionBranchProd)Session["SessionBranchProd"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                SessionBranchProd SessionBranchProd = new SessionBranchProd(country, _clientid, edition, book, null);
                Session["SessionBranchProd"] = SessionBranchProd;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ",@Participant=Participant").ToList();

                return View(_result);

            }
            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionBranchProd SessionBranchProd = new SessionBranchProd(country, _clientid, edition, book, null);
                Session["SessionBranchProd"] = SessionBranchProd;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ",@Participant=Participant").ToList();

                return View(_result);
            }
            else
            {
                int _clientid = 0;
                int edition = 0;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ",@Participant=Participant").ToList();

                return View(_result);
            }
        }

        public JsonResult EditBranch(string Client, string CompanyName, string ShortName)
        {
            int ClientId = int.Parse(Client);

            try
            {
                var c = db.Database.SqlQuery<Clients>("plm_spCRUDClients @CompanyName='" + CompanyName + "', @ShortName='" + ShortName + "', @CRUDType=" + CRUD.Read + ", @ClientId=" + ClientId + "").ToList();

                if (c.LongCount() > 0)
                {
                    var namec = db.Database.SqlQuery<Clients>("plm_spCRUDClients @CompanyName='" + CompanyName + "', @ShortName='" + ShortName + "', @CRUDType=" + CRUD.Read + "").ToList();

                    if (namec.LongCount() > 0)
                    {
                        return Json("_existsClient", JsonRequestBehavior.AllowGet);
                    }
                }

                if (c.LongCount() == 0)
                {
                    var _resultinsc = db.Database.ExecuteSqlCommand("plm_spCRUDClients @CompanyName='" + CompanyName + "', @ShortName='" + ShortName + "', @CRUDType=" + CRUD.Update + ", @ClientId=" + ClientId + "");

                    ActivityLog.EditBranch(ClientId, CompanyName, ShortName, null, 2);

                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADDRESSES          ************************ */

        public ActionResult ClientAddresses(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionBranchProd ind = (SessionBranchProd)Session["SessionBranchProd"];

            int _clientid = ind.ClId;

            if (_clientid != ClientId)
            {
                SessionBrnachId SessionBrnachId = new Models.Sessions.SessionBrnachId(ClientId);
                Session["SessionBrnachId"] = SessionBrnachId;
            }
            else
            {
                Session["SessionBrnachId"] = null;
            }

            var ca = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(ca);
        }

        public JsonResult SaveChangedAddress(string AddressIdd, string Address, string Suburb, string City, string State, string ZipCode, string Mail, string Web, string Location, string StateI)
        {
            var AddressId = int.Parse(AddressIdd);
            int StateId = int.Parse(StateI);

            var _addr = db.Addresses.Where(x => x.AddressId == AddressId).ToList();

            var _resultinsa = db.Database.ExecuteSqlCommand("plm_spCRUDAddresses @CRUDType=" + CRUD.Update +
                                                                        ", @Address='" + Address.Trim() + "'" +
                                                                        ", @ZipCode='" + ZipCode.Trim() + "'" +
                                                                        ", @City='" + City.Trim() + "'" +
                                                                        ", @Email='" + Mail.Trim() + "'" +
                                                                        ", @Web='" + Web.Trim() + "'" +
                                                                        ", @StateId='" + StateId + "'" +
                                                                        ", @Suburb='" + Suburb + "'" +
                                                                        ", @Location='" + Location.Trim() + "'" +
                                                                        ", @AddressId=" + AddressId + "");

            ActivityLog._insertAddresses(AddressId, Address, Suburb, City, _addr[0].CountryId, Mail, Web, ZipCode, 2);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddAddress(string Address, string Suburb, string City, string StateI, string ZipCode, string Mail, string Web, string Country, string Client, string Location)
        {
            ClientAddresses ClientAddresses = new Models.ClientAddresses();

            int StateId = int.Parse(StateI);
            int CountryId = int.Parse(Country);
            int ClientId = int.Parse(Client);

            try
            {
                var a = db.Database.SqlQuery<Addresses>("plm_spCRUDAddresses @CRUDType=" + CRUD.Read + ", @Address='" + Address + "', @ZipCode='" + ZipCode + "', @City='" + City + "', @Email='" + Mail + "', @Web='" + Web + "'").ToList();

                if (a.LongCount() == 0)
                {
                    var _resultinsa = db.Database.SqlQuery<int>("plm_spCRUDAddresses @CRUDType=" + CRUD.Create + ", @Address='" + Address.Trim() + "', @ZipCode='" + ZipCode.Trim() + "', @City='" + City.Trim() + "', @Email='" + Mail.Trim() + "', @Web='" + Web.Trim() + "', @CountryId=" + CountryId + ", @StateId=" + StateId + ", @Suburb='" + Suburb + "', @Location='" + Location.Trim() + "'").ToList();

                    int AddressId = _resultinsa[0];

                    ActivityLog._insertAddresses(AddressId, Address, Suburb, City, CountryId, Mail, Web, ZipCode, 1);

                    var ca = db.ClientAddresses.Where(x => x.AddressId == AddressId && x.ClientId == ClientId).ToList();

                    if (ca.LongCount() == 0)
                    {
                        ClientAddresses = new ClientAddresses();

                        ClientAddresses.AddressId = _resultinsa[0];
                        ClientAddresses.ClientId = ClientId;

                        db.ClientAddresses.Add(ClientAddresses);
                        db.SaveChanges();

                        ActivityLog._insertClientAddresses(AddressId, ClientId, 1);
                    }
                }
            }
            catch (Exception e)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAddresses(string Address, string Client)
        {
            int AddressesId = int.Parse(Address);
            int ClientId = int.Parse(Client);

            var cp = db.ClientPhones.Where(x => x.AddressId == AddressesId && x.ClientId == ClientId).ToList();

            if (cp.LongCount() > 0)
            {
                return Json("_existPhone", JsonRequestBehavior.AllowGet);
            }

            var ca = db.ClientAddresses.Where(x => x.AddressId == AddressesId && x.ClientId == ClientId).ToList();

            if (ca.LongCount() > 0)
            {
                var delete = db.ClientAddresses.SingleOrDefault(x => x.ClientId == ClientId && x.AddressId == AddressesId);

                db.ClientAddresses.Remove(delete);
                db.SaveChanges();

                ActivityLog._insertClientAddresses(AddressesId, ClientId, 4);
            }

            try
            {
                var aR = db.Database.SqlQuery<Addresses>("plm_spCRUDAddresses @CRUDType=" + CRUD.Read + ", @AddressId=" + AddressesId + "").ToList();
                if (aR.LongCount() > 0)
                {

                    var a = db.Database.ExecuteSqlCommand("plm_spCRUDAddresses @CRUDType=" + CRUD.Delete + ", @AddressId=" + AddressesId + "");

                    ActivityLog._insertAddresses(AddressesId, aR[0].Address, aR[0].Suburb, aR[0].City, aR[0].CountryId, aR[0].Email, aR[0].Web, aR[0].ZipCode, 4);
                }

            }
            catch (Exception e)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientPhones(int? ClientId, int? AddressId, int? CountryId)
        {
            SessionAddressIdProd SessionAddressIdProd = new Models.Sessions.SessionAddressIdProd(AddressId, CountryId);
            Session["SessionAddressIdProd"] = SessionAddressIdProd;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public JsonResult EditPhones(string ClientPhone, string Lada, string Number, string Client)
        {
            SessionAddressIdProd SessionAddressIdProd = (SessionAddressIdProd)Session["SessionAddressIdProd"];

            int AddressId = Convert.ToInt32(SessionAddressIdProd.AddressId);
            int ClientPhoneId = int.Parse(ClientPhone);
            int ClientId = int.Parse(Client);

            var cp = db.Database.SqlQuery<ClientPhones>("plm_spCRUDClientPhones @CRUDType=" + CRUD.Read + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @ClientPhoneId=" + ClientPhoneId + "").ToList();

            if (cp.LongCount() > 0)
            {
                var _updatecp = db.Database.ExecuteSqlCommand("plm_spCRUDClientPhones @CRUDType=" + CRUD.Update + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @ClientPhoneId=" + ClientPhoneId + ", @Lada='" + Lada.Trim() + "', @Number='" + Number.Trim() + "'");

                ActivityLog._editphones(ClientPhoneId, ClientId, AddressId, cp[0].PhoneTypeId, Number.Trim(), Lada.Trim());

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPhone(string Lada, string Number, string PhoneType, string Client)
        {
            SessionAddressIdProd SessionAddressIdProd = (SessionAddressIdProd)Session["SessionAddressIdProd"];

            int AddressId = Convert.ToInt32(SessionAddressIdProd.AddressId);
            int ClientId = int.Parse(Client);
            int PhoneTypeId = int.Parse(PhoneType);

            if (!string.IsNullOrEmpty(Lada))
            {
                var cp = db.Database.SqlQuery<int>("plm_spCRUDClientPhones @CRUDType=" + CRUD.Create + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @Lada='" + Lada.Trim() + "', @Number='" + Number.Trim() + "', @PhoneTypeId=" + PhoneTypeId + "").ToList();

                int ClientPhoneId = cp[0];

                ActivityLog._insertphone(ClientPhoneId, AddressId, ClientId, PhoneTypeId, Number.Trim(), Lada.Trim(), 1);
            }
            else
            {
                var cp = db.Database.SqlQuery<int>("plm_spCRUDClientPhones @CRUDType=" + CRUD.Create + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @Number='" + Number.Trim() + "', @PhoneTypeId=" + PhoneTypeId + "").ToList();

                int ClientPhoneId = cp[0];

                ActivityLog._insertphone(ClientPhoneId, AddressId, ClientId, PhoneTypeId, Number.Trim(), Lada.Trim(), 1);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePhones(string ClientPhone, string Client)
        {
            SessionAddressIdProd SessionAddressIdProd = (SessionAddressIdProd)Session["SessionAddressIdProd"];

            int AddressId = Convert.ToInt32(SessionAddressIdProd.AddressId);
            int ClientPhoneId = int.Parse(ClientPhone);
            int ClientId = int.Parse(Client);

            var _resultCP = db.Database.SqlQuery<ClientPhones>("plm_spCRUDClientPhones @ClientId=" + ClientId + ", @AddressId=" + AddressId + ",@ClientPhoneId=" + ClientPhoneId + ", @CRUDType=" + CRUD.Read + "").ToList();
            if (_resultCP.LongCount() > 0)
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spCRUDClientPhones @ClientId=" + ClientId + ", @AddressId=" + AddressId + ",@ClientPhoneId=" + ClientPhoneId + ", @CRUDType=" + CRUD.Delete + "");


                ActivityLog._insertphone(ClientPhoneId, AddressId, ClientId, _resultCP[0].PhoneTypeId, _resultCP[0].Number.Trim(), _resultCP[0].Lada.Trim(), 4);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANDS          ************************ */

        public ActionResult Brands(string CountryId, string ClientId, string EditionId, string BookId, string BrandName)
        {
            SessionBrandsProd ind = (SessionBrandsProd)Session["SessionBrandsProd"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                SessionBrandsProd SessionBrandsProd = new SessionBrandsProd(country, _clientid, edition, book, null);
                Session["SessionBrandsProd"] = SessionBrandsProd;

                var _result = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ", @BrandName='" + BrandName + "'").ToList();

                if (!string.IsNullOrEmpty(BrandName))
                {
                    SearchBrandsAsocSM SearchBrandsAsocSM = new SearchBrandsAsocSM(BrandName);
                    Session["SearchBrandsAsocSM"] = SearchBrandsAsocSM;
                }
                else
                {
                    Session["SearchBrandsAsocSM"] = null;
                }

                return View(_result);

            }
            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionBrandsProd SessionBrandsProd = new SessionBrandsProd(country, _clientid, edition, book, null);
                Session["SessionBrandsProd"] = SessionBrandsProd;

                var _result = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ", @BrandName='" + BrandName + "'").ToList();

                if (!string.IsNullOrEmpty(BrandName))
                {
                    SearchBrandsAsocSM SearchBrandsAsocSM = new SearchBrandsAsocSM(BrandName);
                    Session["SearchBrandsAsocSM"] = SearchBrandsAsocSM;
                }
                else
                {
                    Session["SearchBrandsAsocSM"] = null;
                }

                return View(_result);
            }
            else
            {
                int _clientid = 0;
                int edition = 0;

                var _result = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + "").ToList();

                return View(_result);
            }
        }

        public JsonResult SaveBrand(string Brand, string Name, string Description, string Client, string Edition, string BrandType)
        {
            int BrandId = int.Parse(Brand);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            byte BrandTypeId = Convert.ToByte(BrandType);

            var b = db.Brands.Where(x => x.BrandId == BrandId).ToList();

            if (b.LongCount() > 0)
            {
                foreach (Brands _b in b)
                {
                    _b.BrandName = Name.Trim();

                    db.SaveChanges();

                    ActivityLog.Brands(BrandId, _b.BrandName, "", 2);
                }
            }


            var cb = db.ClientBrands.Where(x => x.ClientId == ClientId && x.BrandId == BrandId).ToList();

            if (cb.LongCount() > 0)
            {
                foreach (ClientBrands _cb in cb)
                {
                    if (!string.IsNullOrEmpty(Description))
                    {
                        _cb.ExpireDescription = Description.Trim();

                        if (BrandTypeId != 0)
                        {
                            _cb.ClientBrandTypeId = BrandTypeId;

                            ActivityLog.EditClientBrands(EditionId, ClientId, BrandId, null, 2, BrandTypeId);
                        }
                        else
                        {
                            _cb.ClientBrandTypeId = null;

                            ActivityLog.EditClientBrands(EditionId, ClientId, BrandId, Description, 2, null);
                        }
                    }
                    else
                    {
                        _cb.ExpireDescription = null;

                        if (BrandTypeId != 0)
                        {
                            _cb.ClientBrandTypeId = BrandTypeId;

                            ActivityLog.EditClientBrands(EditionId, ClientId, BrandId, null, 2, BrandTypeId);
                        }
                        else
                        {
                            _cb.ClientBrandTypeId = null;

                            ActivityLog.EditClientBrands(EditionId, ClientId, BrandId, null, 2, null);
                        }


                    }

                    db.SaveChanges();
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BrandImages(int? BrandId, int? ClientId, int? EditionId)
        {
            SessionBrandsProd ind = (SessionBrandsProd)Session["SessionBrandsProd"];

            ind.PId = BrandId;

            var br = db.Database.SqlQuery<BrandImagesByEditionByClientByProduct>("plm_spGetBrandImagesByEditionByClientByProduct @EditionId=" + EditionId + ", @BrandId=" + BrandId + ",@ClientId=" + ClientId + "").ToList();

            return View(br);
        }

        public ActionResult GetBrandImages(int? BrandId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathPA = System.Configuration.ConfigurationManager.AppSettings["Path"];

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            var bi = db.Brands.Where(x => x.BrandId == BrandId).ToList();

            if (bi.LongCount() > 0)
            {
                string ProductShot = bi[0].Logo;

                if (!string.IsNullOrEmpty(ProductShot))
                {
                    var root = Path.Combine(PathP, CountryName, "brandimages", ProductShot);
                    if (System.IO.File.Exists(root))
                    {
                        return File(root, "image/png");
                    }
                    else
                    {
                        ProductShot = "not_available.png";
                        root = Path.Combine(PathPA, "Uploads", ProductShot);
                        return File(root, "image/png");
                    }
                }
                else
                {
                    ProductShot = "not_available.png";
                    var root = Path.Combine(PathPA, "Uploads", ProductShot);
                    return File(root, "image/png");
                }
            }
            else
            {
                string ProductShot = "not_available.png";
                var root = Path.Combine(PathPA, "Uploads", ProductShot);
                return File(root, "image/png");
            }
        }

        [HttpPost]
        public ActionResult InsertBrandImage(HttpPostedFileBase file, string Client, string Edition, string Brand, string Country)
        {
            int CountryId = int.Parse(Country);

            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathPA = System.Configuration.ConfigurationManager.AppSettings["Path"];

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int BrandId = int.Parse(Brand);

            string Ext = Path.GetExtension(file.FileName);
            string FileName = (file.FileName).Replace(Ext, "");

            FileName = ClearString(FileName) + Ext;

            //var root = Path.Combine(Server.MapPath("~/App_Data/BrandImages"), FileName);
            var root = Path.Combine(PathP, CountryName, "brandimages", FileName);


            var cb = db.ClientBrands.Where(x => x.EditionId == EditionId && x.ClientId == ClientId && x.BrandId == BrandId).ToList();

            if (cb.LongCount() > 0)
            {
                var b = db.Brands.Where(x => x.BrandId == BrandId).ToList();

                if (b.LongCount() > 0)
                {
                    foreach (Brands _b in b)
                    {
                        _b.Logo = FileName.Trim();

                        db.SaveChanges();

                        ActivityLog.Brands(BrandId, _b.BrandName, FileName, 2);

                        file.SaveAs(root);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public static string ClearString(string _string)
        {
            _string = _string.Replace("Á", "A");
            _string = _string.Replace("É", "E");
            _string = _string.Replace("Í", "I");
            _string = _string.Replace("Ó", "O");
            _string = _string.Replace("Ú", "U");
            _string = _string.Replace("Ü", "U");


            _string = _string.Replace("á", "a");
            _string = _string.Replace("é", "e");
            _string = _string.Replace("í", "i");
            _string = _string.Replace("ó", "o");
            _string = _string.Replace("ú", "u");
            _string = _string.Replace("ü", "u");


            _string = _string.Replace(".", "");
            _string = _string.Replace("-", "_");
            _string = _string.Replace(",", "");
            _string = _string.Replace(" ", "_");

            return _string;
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADVERTS          ************************ */

        public ActionResult Adverts(string CountryId, string ClientId, string EditionId, string BookId, string CategoryName)
        {
            SessionAdvertsProd ind = (SessionAdvertsProd)Session["SessionAdvertsProd"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                SessionAdvertsProd SessionAdvertsProd = new SessionAdvertsProd(country, _clientid, edition, book, null);
                Session["SessionAdvertsProd"] = SessionAdvertsProd;

                var _result = db.Database.SqlQuery<AdvertsByClient>("plm_spGetAdvertsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ", @CategoryName='" + CategoryName + "'").ToList();


                return View(_result);

            }
            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionAdvertsProd SessionAdvertsProd = new SessionAdvertsProd(country, _clientid, edition, book, null);
                Session["SessionAdvertsProd"] = SessionAdvertsProd;

                var _result = db.Database.SqlQuery<AdvertsByClient>("plm_spGetAdvertsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ", @CategoryName='" + CategoryName + "'").ToList();

                return View(_result);
            }
            else
            {
                int _clientid = 0;
                int edition = 0;

                var _result = db.Database.SqlQuery<AdvertsByClient>("plm_spGetAdvertsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + "").ToList();

                return View(_result);
            }
        }

        [HttpPost]
        public ActionResult addAdvertImage(HttpPostedFileBase file, string Advert, string Country)
        {
            int CountryId = int.Parse(Country);

            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            int AdvertId = int.Parse(Advert);
            String ImageName = file.FileName;
            String Ext = Path.GetExtension(file.FileName);

            ImageName = ImageName.Replace(Ext, "");

            ImageName = ClearString(ImageName);

            ImageName = ImageName + Ext;

            //var root = Path.Combine(Server.MapPath("~/App_Data/AdvertImages"), ImageName);

            var root = Path.Combine(PathP, CountryName, "advertimages", ImageName);

            file.SaveAs(root);

            try
            {
                var _result = db.Database.SqlQuery<Adverts>("plm_spCRUDAdverts @CRUDType=" + CRUD.Read + ", @AdvertId=" + AdvertId + "").ToList();

                if (_result.LongCount() > 0)
                {
                    var update = db.Database.ExecuteSqlCommand("plm_spCRUDAdverts @CRUDType=" + CRUD.Update + ", @Field=AdvertFile, @AdvertFile='" + ImageName + "', @AdvertId=" + AdvertId + "");

                    ActivityLog._Adverts(AdvertId, _result[0].AdvertName, _result[0].AdvertDescription, ImageName, 2);
                }
            }
            catch (Exception e)
            {

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditAdvert(string Advert, string Name, string Description)
        {
            int AdvertId = int.Parse(Advert);

            var a = db.Adverts.Where(x => x.AdvertName.ToUpper().Trim() == Name.ToUpper().Trim() && x.AdvertId != AdvertId).ToList();

            if (a.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var _result = db.Database.SqlQuery<Adverts>("plm_spCRUDAdverts @AdvertId=" + AdvertId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (_result.LongCount() > 0)
            {
                var _resultEdit = db.Database.ExecuteSqlCommand("plm_spCRUDAdverts @AdvertId=" + AdvertId + ", @CRUDType=" + CRUD.Update + ", @AdvertName='" + Name.Trim() + "', @AdvertDescription='" + Description.Trim() + "'");

                ActivityLog._Adverts(AdvertId, Name, Description, null, 2);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               PRODUCTSHOTS          ************************ */

        public ActionResult ProductShots(int? ProductId, int? ClientId, int? EditionId)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            if ((!Request.IsAuthenticated) || (ind == null) || (ProductId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            ind.PId = ProductId;

            var _result = db.Database.SqlQuery<GetProductShots>("plm_spGetEditionClientProductShots @ClientId=" + ClientId + ", @EditionId=" + EditionId + ", @ProductId=" + ProductId + "").ToList();

            return View(_result);
        }

        [HttpPost]
        public ActionResult AddProductShots(HttpPostedFileBase file, string Client, string Edition, string Product, string ImageSize, string Country)
        {
            String PathPS = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();

            int CountryId = int.Parse(Country);

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);


            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            byte ImageSizeId = Convert.ToByte(ImageSize);
            int EditionClientProductShotId = 0;

            string Ext = Path.GetExtension(file.FileName);
            string FileName = (file.FileName).Replace(Ext, "");

            FileName = ClearString(FileName) + Ext;

            var sidef = db.EditionClientProductSIDEF.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            var ims = db.ImageSizes.Where(x => x.ImageSizeId == ImageSizeId).ToList();

            if (sidef.LongCount() > 0)
            {
                foreach (EditionClientProductSIDEF _sidef in sidef)
                {
                    _sidef.ImageName = FileName;

                    db.SaveChanges();

                    ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 2, FileName);
                }

                //var roots = Server.MapPath("~/App_Data/ProductShots/SIDEF");

                var roots = Path.Combine(PathPS, CountryName, "productshots", ims[0].ImageSize);

                if (System.IO.Directory.Exists(roots))
                {
                    roots = Path.Combine(roots, FileName.Trim());

                    file.SaveAs(roots);
                }
                else
                {
                    Directory.CreateDirectory(roots);

                    roots = Path.Combine(roots, FileName.Trim());

                    file.SaveAs(roots);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ecps = db.EditionClientProductShots.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecps.LongCount() > 0)
                {
                    foreach (EditionClientProductShots _ecps in ecps)
                    {
                        _ecps.ProductShot = FileName;

                        db.SaveChanges();

                        EditionClientProductShotId = _ecps.EditionClientProductShotId;
                    }
                }
                else
                {
                    EditionClientProductShots EditionClientProductShots = new EditionClientProductShots();

                    EditionClientProductShots.Active = true;
                    EditionClientProductShots.AddedDate = DateTime.Now;
                    EditionClientProductShots.ClientId = ClientId;
                    EditionClientProductShots.EditionId = EditionId;
                    EditionClientProductShots.ProductId = ProductId;
                    EditionClientProductShots.ProductShot = FileName;

                    db.EditionClientProductShots.Add(EditionClientProductShots);
                    db.SaveChanges();

                    var _ecps = db.EditionClientProductShots.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                    if (_ecps.LongCount() > 0)
                    {
                        foreach (EditionClientProductShots _ecps1 in _ecps)
                        {
                            _ecps1.ProductShot = FileName;

                            db.SaveChanges();

                            EditionClientProductShotId = _ecps1.EditionClientProductShotId;

                            ActivityLog.EditionClientProductShots(EditionClientProductShotId, EditionId, ClientId, ProductId, FileName, 1);
                        }
                    }
                }
            }

            var epss = db.EditionProductShotSizes.Where(x => x.EditionClientProductShotId == EditionClientProductShotId && x.ImageSizeId == ImageSizeId).ToList();

            if (epss.LongCount() == 0)
            {
                EditionProductShotSizes EditionProductShotSizes = new EditionProductShotSizes();

                EditionProductShotSizes.EditionClientProductShotId = EditionClientProductShotId;
                EditionProductShotSizes.ImageSizeId = ImageSizeId;

                db.EditionProductShotSizes.Add(EditionProductShotSizes);
                db.SaveChanges();

                ActivityLog.EditionProductShotSizes(EditionClientProductShotId, ImageSizeId, 1);
            }

            //var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), ims[0].ImageSize);

            var root = Path.Combine(PathPS, CountryName, "productshots", ims[0].ImageSize);

            if (System.IO.Directory.Exists(root))
            {
                root = Path.Combine(root, FileName.Trim());

                file.SaveAs(root);
            }
            else
            {
                Directory.CreateDirectory(root);

                root = Path.Combine(root, FileName.Trim());

                file.SaveAs(root);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductShots(int? EditionClientProductShotId, byte ImageSizeId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathE = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            string ProductShot = "";

            var ims = db.ImageSizes.Where(x => x.ImageSizeId == ImageSizeId).ToList();

            if (ims.LongCount() > 0)
            {
                //var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), ims[0].ImageSize);

                var root = Path.Combine(PathP, CountryName, "productshots", ims[0].ImageSize);

                var ecps = db.EditionClientProductShots.Where(x => x.EditionClientProductShotId == EditionClientProductShotId).ToList();

                if (ecps.LongCount() > 0)
                {
                    if (!string.IsNullOrEmpty(ecps[0].ProductShot))
                    {
                        string Ext = Path.GetExtension(ecps[0].ProductShot);
                        string FileName = (ecps[0].ProductShot).Replace(Ext, "");

                        FileName = ClearString(FileName) + Ext;

                        root = Path.Combine(root, FileName);

                        if (System.IO.File.Exists(root))
                        {
                            ProductShot = FileName;
                            return File(root, "image/png");
                        }
                        else
                        {
                            ProductShot = "not_available.png";
                            //root = Path.Combine(Server.MapPath("~/App_Data/Uploads"), ProductShot);

                            root = Path.Combine(PathE, "Uploads", ProductShot);

                            return File(root, "image/png");
                        }
                    }
                    else
                    {
                        ProductShot = "not_available.png";
                        root = Path.Combine(PathE, "Uploads", ProductShot);
                        return File(root, "image/png");
                    }
                }
                else
                {
                    ProductShot = "not_available.png";
                    root = Path.Combine(PathE, "Uploads", ProductShot);
                    return File(root, "image/png");
                }

            }
            else
            {
                ProductShot = "not_available.png";
                var root = Path.Combine(PathE, "Uploads", ProductShot);
                return File(root, "image/png");
            }
        }

        public ActionResult GetProductShotsSIDEF(int? EditionClientProductShotId, int ClientId, int EditionId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathE = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            string ProductShot = "";

            //var root = Server.MapPath("~/App_Data/ProductShots/SIDEF");

            var root = Path.Combine(PathP, CountryName, "productshots", "SIDEF");

            var ecps = db.EditionClientProductSIDEF.Where(x => x.ProductId == EditionClientProductShotId && x.EditionId == EditionId && x.ClientId == ClientId).ToList();

            if (ecps.LongCount() > 0)
            {
                if (!string.IsNullOrEmpty(ecps[0].ImageName))
                {
                    string Ext = Path.GetExtension(ecps[0].ImageName);
                    string FileName = (ecps[0].ImageName).Replace(Ext, "");

                    FileName = ClearString(FileName) + Ext;

                    root = Path.Combine(root, FileName);

                    if (System.IO.File.Exists(root))
                    {
                        ProductShot = FileName;
                        return File(root, "image/png");
                    }
                    else
                    {
                        ProductShot = "not_available.png";
                        root = Path.Combine(PathE, "Uploads", ProductShot);

                        return File(root, "image/png");
                    }
                }
                else
                {
                    ProductShot = "not_available.png";
                    root = Path.Combine(PathE, "Uploads", ProductShot);
                    return File(root, "image/png");
                }
            }
            else
            {
                ProductShot = "not_available.png";
                root = Path.Combine(PathE, "Uploads", ProductShot);
                return File(root, "image/png");
            }
        }

        public ActionResult GetPreviewPS(int? ProductId, int? ClientId, int? EditionId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathE = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);


            string ProductShot = "";
            int EditionClientProductShotId = 0;

            var ecps = db.EditionClientProductShots.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            if (ecps.LongCount() > 0)
            {
                EditionClientProductShotId = ecps[0].EditionClientProductShotId;
            }

            var epss = db.EditionProductShotSizes.Where(x => x.EditionClientProductShotId == EditionClientProductShotId).OrderBy(x => x.ImageSizeId).ToList();

            if (epss.LongCount() > 0)
            {
                foreach (EditionProductShotSizes _epss in epss)
                {
                    var ims = db.ImageSizes.Where(x => x.ImageSizeId == _epss.ImageSizeId).ToList();

                    //var root = Path.Combine(Server.MapPath("~/App_Data/ProductShots"), ims[0].ImageSize);

                    var root = Path.Combine(PathP, CountryName, "productshots", ims[0].ImageSize);

                    if (System.IO.Directory.Exists(root))
                    {
                        if (!string.IsNullOrEmpty(ecps[0].ProductShot))
                        {
                            string Ext = Path.GetExtension(ecps[0].ProductShot);
                            string FileName = (ecps[0].ProductShot).Replace(Ext, "");

                            FileName = ClearString(FileName) + Ext;

                            root = Path.Combine(root, FileName);

                            if (System.IO.File.Exists(root))
                            {
                                ProductShot = FileName;
                                return File(root, "image/png");
                            }
                            else
                            {
                                ProductShot = "not_available.png";
                                var path = Path.Combine(PathE, "Uploads", ProductShot);
                                return File(root, "image/png");
                            }
                        }
                        else
                        {
                            ProductShot = "not_available.png";
                            var path = Path.Combine(PathE, "Uploads", ProductShot);
                            return File(root, "image/png");
                        }

                    }
                    else
                    {
                        ProductShot = "not_available.png";
                        var path = Path.Combine(PathE, "Uploads", ProductShot);
                        return File(root, "image/png");
                    }
                }
            }
            else
            {
                //var root = Server.MapPath("~/App_Data/ProductShots/SIDEF");

                var root = Path.Combine(PathP, CountryName, "productshots", "SIDEF");

                var ecpss = db.EditionClientProductSIDEF.Where(x => x.ProductId == ProductId && x.EditionId == EditionId && x.ClientId == ClientId).ToList();

                if (ecpss.LongCount() > 0)
                {
                    if (!string.IsNullOrEmpty(ecpss[0].ImageName))
                    {
                        string Ext = Path.GetExtension(ecpss[0].ImageName);
                        string FileName = (ecpss[0].ImageName).Replace(Ext, "");

                        FileName = ClearString(FileName) + Ext;

                        root = Path.Combine(root, FileName);

                        if (System.IO.File.Exists(root))
                        {
                            ProductShot = FileName;
                            return File(root, "image/png");
                        }
                    }
                    else
                    {
                        ProductShot = "not_available.png";
                        root = Path.Combine(PathE, "Uploads", ProductShot);
                        return File(root, "image/png");
                    }
                }
                else
                {
                    ProductShot = "not_available.png";
                    root = Path.Combine(PathE, "Uploads", ProductShot);
                    return File(root, "image/png");
                }
            }

            return View();
        }

        public JsonResult DeleteProductShot(string EditionClientProductShot, string SizeI)
        {
            int EditionClientProductShotId = int.Parse(EditionClientProductShot);
            byte ImageSizeId = Convert.ToByte(SizeI);

            var epss = db.EditionProductShotSizes.Where(x => x.EditionClientProductShotId == EditionClientProductShotId && x.ImageSizeId == ImageSizeId).ToList();

            if (epss.LongCount() > 0)
            {
                var delete = db.EditionProductShotSizes.SingleOrDefault(x => x.EditionClientProductShotId == EditionClientProductShotId && x.ImageSizeId == ImageSizeId);

                db.EditionProductShotSizes.Remove(delete);
                db.SaveChanges();

                ActivityLog.EditionProductShotSizes(EditionClientProductShotId, ImageSizeId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductShotSIDEF(string Product, string Edition, string Client)
        {
            int ProductId = int.Parse(Product);
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);


            var epss = db.EditionClientProductSIDEF.Where(x => x.ProductId == ProductId && x.EditionId == EditionId && x.ClientId == ClientId).ToList();

            if (epss.LongCount() > 0)
            {
                foreach (EditionClientProductSIDEF _epss in epss)
                {
                    _epss.ImageName = null;

                    db.SaveChanges();
                }

                ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 2, null);
                //var delete = db.EditionClientProductSIDEF.SingleOrDefault(x => x.EditionClientProductShotId == EditionClientProductShotId && x.ImageSizeId == ImageSizeId);

                //db.EditionProductShotSizes.Remove(delete);
                //db.SaveChanges();

                //ActivityLog.EditionProductShotSizes(EditionClientProductShotId, ImageSizeId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               PAGINAR CLIENTES          ************************ */

        public ActionResult ClientsByEdition(string CountryId, string EditionId, string BookId)
        {
            SessionEditionClients ind = (SessionEditionClients)Session["SessionEditionClients"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

                var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                SessionEditionClients SessionEditionClients = new SessionEditionClients(Country, Edition, Book);
                Session["SessionEditionClients"] = SessionEditionClients;

                return View(ec);
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

                var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                SessionEditionClients SessionEditionClients = new SessionEditionClients(Country, Edition, Book);
                Session["SessionEditionClients"] = SessionEditionClients;

                return View(ec);
            }

            else
            {
                int Country = 0;
                int Edition = 0;

                var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + "").ToList();

                return View(ec);
            }
        }

        public JsonResult addClientPage(string Client, string Page, string Edition)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var ec = db.EditionClients.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

            if (ec.LongCount() > 0)
            {
                foreach (EditionClients _ec in ec)
                {
                    if (!string.IsNullOrEmpty(Page))
                    {
                        if (_ec.Page != Page)
                        {
                            _ec.Page = Page.Trim();

                            ActivityLog.createEditionClients(ClientId, EditionId, _ec.ClientTypeId, Page.Trim(), 1);
                        }
                    }
                    else
                    {
                        _ec.Page = null;

                        ActivityLog.createEditionClients(ClientId, EditionId, _ec.ClientTypeId, null, 1);
                    }

                    db.SaveChanges();


                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditClient(string Client, string CompanyName, string ShortName)
        {
            int ClientId = int.Parse(Client);

            var cl = db.Clients.Where(x => x.ClientId != ClientId && x.CompanyName.ToUpper().Trim() == CompanyName.ToUpper().Trim()).ToList();

            if (cl.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var c = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (c.LongCount() > 0)
            {
                foreach (Clients _c in c)
                {
                    _c.CompanyName = CompanyName.Trim();
                    _c.ShortName = ShortName.Trim();

                    db.SaveChanges();

                    ActivityLog.EditBranch(ClientId, CompanyName.Trim(), ShortName.Trim(), null, 2);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               PAGINAR PRODUCTOS          ************************ */

        public JsonResult AddProductPage(string Product, string Client, string Edition, string Page)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var ebcp = db.EditionBookClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            if (ebcp.LongCount() > 0)
            {
                foreach (EditionBookClientProducts _ebcp in ebcp)
                {
                    if (!string.IsNullOrEmpty(Page))
                    {
                        _ebcp.Page = Page.Trim();

                        db.SaveChanges();

                        ActivityLog.createEditionBookClientProducts(ProductId, EditionId, ClientId, 2, Page.Trim());
                    }
                    else
                    {
                        _ebcp.Page = null;

                        db.SaveChanges();

                        ActivityLog.createEditionBookClientProducts(ProductId, EditionId, ClientId, 2, null);
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               SEGMENTAR HTML POR RUBROS          ************************ */

        public List<HTMLAttributes> GetAttributes(int? ProductId, int ClientId, int EditionId)
        {
            List<HTMLAttributes> ListAttributes = new List<HTMLAttributes>();

            HTMLAttributes HTMLAttributes = new Models.HTMLAttributes();

            SessionProduction ind = (SessionProduction)Session["SessionProduction"];
            //int ClientId = int.Parse(Client);
            //int EditionId = int.Parse(Edition);
            //int ProductId = int.Parse(Product);

            var pp = (from _partp in db.ParticipantProducts
                      where _partp.EditionId == EditionId
                      && _partp.ClientId == ClientId
                      && _partp.ProductId == ProductId
                      select _partp).ToList();

            int i, end;
            String attributeDescription = "";
            String titulo = "<Titulo>";

            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    String attribute = _pp.XMLContent;

                    String attr = "";

                    while (attribute != null)
                    {
                        i = attribute.IndexOf("<Titulo>");
                        if (i >= 0)
                        {
                            end = attribute.IndexOf("</Titulo>", i);
                            attributeDescription = attribute.Substring(i + titulo.Length, end - (i + titulo.Length));
                            attribute = attribute.Substring(end);


                            if (attributeDescription != "")
                            {
                                attr = attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                                var ats = (from _at in db.Attributes
                                           where _at.Description == attr
                                           select _at).ToList();
                                if (ats.LongCount() == 0)
                                {
                                    HTMLAttributes = new Models.HTMLAttributes();

                                    HTMLAttributes.AttributeName = attr.Trim();
                                    HTMLAttributes.Exist = "";

                                    ListAttributes.Add(HTMLAttributes);
                                }
                                else
                                {
                                    HTMLAttributes = new Models.HTMLAttributes();

                                    HTMLAttributes.AttributeName = attr.Trim();
                                    HTMLAttributes.Exist = "<span class='glyphicon glyphicon-ok'></span>";

                                    ListAttributes.Add(HTMLAttributes);
                                    ListAttributesAsoc.Add(HTMLAttributes);
                                }
                            }
                        }
                        else
                        {
                            attribute = null;
                        }
                    }
                    ListAttributes = ListAttributes.OrderBy(x => x.AttributeName).ToList();

                    ListAttributesAsoc = ListAttributesAsoc.OrderBy(x => x.AttributeName).ToList();
                    SessionAttributeGroup SessionAttributeGroup = new SessionAttributeGroup(ListAttributesAsoc);
                    Session["SessionAttributeGroup"] = SessionAttributeGroup;
                }
            }

            ListAttributes = ListAttributes.Distinct().ToList();

            return ListAttributes;
        }

        public JsonResult AddNewAttributes(String ListItems, string Size, string Edition)
        {
            dynamic List = JsonConvert.DeserializeObject(ListItems);

            int ArraySize = int.Parse(Size);
            int EditionId = int.Parse(Edition);

            for (var i = 0; i <= ArraySize - 1; i++)
            {
                var Attribute = Convert.ToString(List[i]["Value"]);

                GetAttributesFromBD(Attribute, EditionId);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public bool GetAttributesFromBD(string Attribute, int EditionId)
        {
            List<Attributes> LA = new List<Attributes>();
            Attributes Attributes = new Models.Attributes();

            String Description = CleanAttribute(Attribute.ToUpper().Trim());

            Attribute = Attribute.ToUpper();

            var attr = db.Attributes.Where(x => x.Active == true).OrderBy(x => x.Description).ToList();

            foreach (Attributes _attr in attr)
            {
                Attributes = new Attributes();

                Attributes.Active = _attr.Active;
                Attributes.AttributeId = _attr.AttributeId;
                Attributes.Description = CleanAttribute(_attr.Description.ToUpper().Trim());

                LA.Add(Attributes);
            }


            var attrasoc = LA.Where(x => x.Description == Description).ToList();

            if (attrasoc.LongCount() > 0)
            {
                foreach (Attributes _attr in attrasoc)
                {
                    var attrs = db.Attributes.Where(x => x.AttributeId == _attr.AttributeId).ToList();

                    if (attrs.LongCount() > 0)
                    {
                        foreach (Attributes _attrs in attrs)
                        {
                            var ea = db.EditionAttributes.Where(x => x.AttributeId == _attrs.AttributeId && x.EditionId == EditionId).ToList();

                            if (ea.LongCount() == 0)
                            {
                                EditionAttributes EditionAttributes = new EditionAttributes();

                                EditionAttributes.AttributeId = _attrs.AttributeId;
                                EditionAttributes.EditionId = EditionId;

                                db.EditionAttributes.Add(EditionAttributes);
                                db.SaveChanges();

                                ActivityLog.EditionAttributes(_attrs.AttributeId, EditionId, 1);
                            }
                        }
                    }
                }
            }
            else
            {
                Attributes AttrCLS = new Attributes();

                AttrCLS.Active = true;
                AttrCLS.Description = Attribute.Trim();

                db.Attributes.Add(AttrCLS);
                db.SaveChanges();

                var asocattr = db.Attributes.Where(x => x.Description == Description).ToList();

                if (asocattr.LongCount() > 0)
                {
                    foreach (Attributes _asocattr in asocattr)
                    {
                        ActivityLog.Attributes(_asocattr.AttributeId, Description, 1);

                        var ea = db.EditionAttributes.Where(x => x.AttributeId == _asocattr.AttributeId && x.EditionId == EditionId).ToList();

                        if (ea.LongCount() == 0)
                        {
                            EditionAttributes EditionAttributes = new EditionAttributes();

                            EditionAttributes.AttributeId = _asocattr.AttributeId;
                            EditionAttributes.EditionId = EditionId;

                            db.EditionAttributes.Add(EditionAttributes);
                            db.SaveChanges();

                            ActivityLog.EditionAttributes(_asocattr.AttributeId, EditionId, 1);
                        }
                    }
                }
            }

            return true;
        }

        public static String CleanAttribute(String attribute)
        {

            attribute = attribute.Replace(",", "");
            attribute = attribute.Replace("..", "");
            attribute = attribute.Replace("  ", " ");

            attribute = attribute.Replace("á", "a");
            attribute = attribute.Replace("Á", "A");
            attribute = attribute.Replace("é", "e");
            attribute = attribute.Replace("É", "E");
            attribute = attribute.Replace("í", "i");
            attribute = attribute.Replace("Í", "I");
            attribute = attribute.Replace("ó", "o");
            attribute = attribute.Replace("Ó", "O");
            attribute = attribute.Replace("Ú", "U");
            attribute = attribute.Replace("ú", "u");
            attribute = attribute.Replace("ü", "u");
            attribute = attribute.Replace("Ü", "U");

            return attribute;
        }

        public JsonResult AddEditionAttributeGroup(string AttributeGroup, string Attribute, string Edition)
        {
            EditionAttributeGroup EditionAttributeGroup = new Models.EditionAttributeGroup();

            int AttributeGroupId = int.Parse(AttributeGroup);
            int AttributeId = int.Parse(Attribute);
            int EditionId = int.Parse(Edition);

            var eag = db.EditionAttributeGroup.Where(x => x.AttributeGroupId == AttributeGroupId && x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();

            if (eag.LongCount() == 0)
            {
                var delete = db.EditionAttributeGroup.SingleOrDefault(x => x.AttributeId == AttributeId && x.EditionId == EditionId);
                if (delete != null)
                {
                    db.EditionAttributeGroup.Remove(delete);
                    db.SaveChanges();

                    ActivityLog.EditionAttributeGroup(null, AttributeId, EditionId, 4);
                }
            }

            var ea = db.EditionAttributes.Where(x => x.AttributeId == AttributeId && x.EditionId == EditionId).ToList();

            if (ea.LongCount() == 0)
            {
                EditionAttributes EditionAttributes = new EditionAttributes();

                EditionAttributes.AttributeId = AttributeId;
                EditionAttributes.EditionId = EditionId;

                db.EditionAttributes.Add(EditionAttributes);
                db.SaveChanges();

                ActivityLog.EditionAttributes(AttributeId, EditionId, 1);

            }

            EditionAttributeGroup = new EditionAttributeGroup();

            EditionAttributeGroup.AttributeGroupId = AttributeGroupId;
            EditionAttributeGroup.AttributeId = AttributeId;
            EditionAttributeGroup.EditionId = EditionId;

            db.EditionAttributeGroup.Add(EditionAttributeGroup);
            db.SaveChanges();

            ActivityLog.EditionAttributeGroup(AttributeGroupId, AttributeId, EditionId, 4);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertProductContents(string Client, string Edition, string Product)
        {
            ProductContents ProductContents = new Models.ProductContents();

            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);

            try
            {
                var _pc = (from _ppc in db.ProductContents
                           where _ppc.ProductId == ProductId
                           && _ppc.EditionId == EditionId
                           select _ppc).ToList();
                if (_pc.LongCount() > 0)
                {
                    foreach (ProductContents _ppc in _pc)
                    {
                        var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _ppc.ProductId && x.ClientId == _ppc.ClientId && x.EditionId == _ppc.EditionId && x.AttributeId == _ppc.AttributeId);

                        db.ProductContents.Remove(delete);
                        db.SaveChanges();
                    }
                    ActivityLog._deleteproductcontents(EditionId, ClientId, ProductId, 4);
                }
            }
            catch (Exception e)
            {
                String msj = e.Message;
            }

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == ClientId
                      && _pp.EditionId == EditionId
                      && _pp.ProductId == ProductId
                      select _pp).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    int i, end, iContent, fContent;

                    String attributeId = "";
                    String attribute = _pp.XMLContent;

                    String attr = "";
                    String content = "";
                    String attributeDescription = "";
                    String titulo = "<Titulo>";
                    String contenido = "<Contenido>";
                    String plainContent = "";

                    while (attribute != null)
                    {
                        i = attribute.IndexOf("<Titulo>");
                        if (i >= 0)
                        {
                            end = attribute.IndexOf("</Titulo>", i);
                            attributeDescription = attribute.Substring(i + titulo.Length, end - (i + titulo.Length));
                            attribute = attribute.Substring(end);

                            if (attributeDescription != "")
                            {
                                attr = attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                                var attrs = (from atr in db.Attributes
                                             where atr.Description == attr.Trim()
                                             select atr).ToList();
                                if (attrs.LongCount() > 0)
                                {
                                    foreach (Attributes _attrs in attrs)
                                    {
                                        attributeId = _attrs.AttributeId.ToString();
                                    }
                                    attributeDescription = "";
                                }
                            }

                            iContent = attribute.IndexOf("<Contenido>", 0);
                            if (iContent >= 0)
                            {
                                fContent = attribute.IndexOf("</Contenido>", iContent);
                                content = attribute.Substring(iContent + contenido.Length, fContent - (iContent + contenido.Length));
                                content = content.Trim();

                                attribute = attribute.Substring(iContent, attribute.Length - iContent);
                            }
                            else
                            {
                                attribute = null;
                            }
                            if ((attributeId != "") && (content != ""))
                            {
                                plainContent = content;
                                plainContent = cleanImage(plainContent);
                                plainContent = cleanTable(plainContent);

                                int AttributeId = int.Parse(attributeId);

                                var pc = (from pcont in db.ProductContents
                                          where pcont.ClientId == ClientId
                                          && pcont.EditionId == EditionId
                                          && pcont.ProductId == ProductId
                                          && pcont.AttributeId == AttributeId
                                          select pcont).ToList();

                                if (pc.LongCount() == 0)
                                {

                                    ProductContents = new ProductContents();

                                    ProductContents.AttributeId = AttributeId;
                                    ProductContents.Content = content;
                                    ProductContents.PlainContent = plainContent;
                                    ProductContents.EditionId = EditionId;
                                    ProductContents.ClientId = ClientId;
                                    ProductContents.ProductId = ProductId;
                                    ProductContents.HtmlContent = null;

                                    db.ProductContents.Add(ProductContents);
                                    db.SaveChanges();



                                    attributeId = "";
                                    content = "";
                                    plainContent = "";
                                }
                            }
                        }
                        else
                        {
                            attribute = null;
                        }
                    }
                }
                ActivityLog._insertproductcontents(EditionId, ClientId, ProductId, 1);
            }
            insertHTMLContent(ProductId, EditionId, ClientId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void insertHTMLContent(int _productid, int _editionid, int _clienid)
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            String S;
            String target = gettargetattr();

            String targetF = "</span>";
            String attrNombre = "";
            String attrContent = null;
            int i = 0, f = 0, iaux = 0;

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == _clienid
                      && _pp.ProductId == _productid
                      && _pp.EditionId == _editionid
                      select _pp).ToList();
            if (pp.LongCount() > 0)
            {
                foreach (ParticipantProducts _pp in pp)
                {
                    S = _pp.HTMLContent;

                    int attributeId = 0;

                    int ff = 0;

                    while (S != null)
                    {
                        i = S.IndexOf(target);
                        if (i >= 0)
                        {
                            f = S.IndexOf(targetF, i);
                            ff = S.IndexOf(">", i + target.Length);
                            attrNombre = S.Substring(ff + 1, f - (ff + 1));
                            attrNombre = cleanattr(attrNombre);

                            iaux = S.IndexOf(target, f);

                            if (iaux >= 0)
                            {
                                attrContent = S.Substring(i, iaux - i);
                                S = S.Substring(iaux, S.Length - iaux);
                            }
                            else
                            {
                                attrContent = S.Substring(i, S.Length - i);
                                S = null;
                            }

                            attrContent = attrContent.Replace("</div>", "");
                            attrContent = attrContent.Replace("</body>", "");
                            attrContent = attrContent.Replace("</html>", "");
                            attrContent = attrContent.Replace("'", "\"");

                            attrNombre = attrNombre.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "");

                            var attrs = (from atr in db.Attributes
                                         where atr.Description == attrNombre.Trim()
                                         select atr).ToList();
                            if (attrs.LongCount() > 0)
                            {
                                foreach (Attributes _attrs in attrs)
                                {
                                    attributeId = _attrs.AttributeId;
                                }
                                attrNombre = "";
                            }

                            var _pc = (from _ppc in db.ProductContents
                                       where _ppc.ProductId == _productid
                                       && _ppc.EditionId == _editionid
                                       && _ppc.ClientId == _clienid
                                       && _ppc.AttributeId == attributeId
                                       select _ppc).ToList();
                            if (_pc.LongCount() > 0)
                            {
                                foreach (ProductContents _ppc in _pc)
                                {
                                    _ppc.HtmlContent = attrContent.Trim();

                                    db.SaveChanges();

                                    ActivityLog._updateproductcontents(attributeId, _editionid, _clienid, _productid, 1);
                                }
                            }
                        }
                        else
                        {
                            S = null;
                        }
                    }
                }
            }
        }

        public static String cleanattr(String attrNombre)
        {
            attrNombre = attrNombre.Replace("&aacute;", "á");
            attrNombre = attrNombre.Replace("&eacute;", "é");
            attrNombre = attrNombre.Replace("&iacute;", "í");
            attrNombre = attrNombre.Replace("&oacute;", "ó");
            attrNombre = attrNombre.Replace("&uacute;", "ú");

            attrNombre = attrNombre.Replace("&Aacute;", "Á");
            attrNombre = attrNombre.Replace("&Eacute;", "É");
            attrNombre = attrNombre.Replace("&Iacute;", "Í");
            attrNombre = attrNombre.Replace("&Oacute;", "Ó");
            attrNombre = attrNombre.Replace("&OACUTE;", "Ó");
            attrNombre = attrNombre.Replace("&Uacute;", "Ú");

            attrNombre = attrNombre.Replace("&Ntilde;", "Ñ");
            attrNombre = attrNombre.Replace("&ntilde;", "ñ");
            attrNombre = attrNombre.Replace("&NTILDE;", "Ñ");

            attrNombre = attrNombre.Replace("&uml;", "ü");
            attrNombre = attrNombre.Replace("&Uml;", "Ü");
            attrNombre = attrNombre.Replace("&uuml;", "ü");
            attrNombre = attrNombre.Replace("&Uuml;", "Ü");

            attrNombre = attrNombre.Replace("<BR />", " ");
            attrNombre = attrNombre.Replace("&nbsp;", " ");
            attrNombre = attrNombre.Replace("&NBSP;", " ");
            attrNombre = attrNombre.Replace("<br />", " ");
            attrNombre = attrNombre.Replace("<br />", " ");
            attrNombre = attrNombre.Replace("&#174;", "®");
            attrNombre = attrNombre.Replace("¡", "");
            attrNombre = attrNombre.Replace("!", "");

            attrNombre = attrNombre.Replace(":", "");
            attrNombre = attrNombre.ToUpper();
            attrNombre = attrNombre.Trim();

            return attrNombre;
        }

        public String gettargetattr()
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            string pname = string.Empty;
            string prop = string.Empty;
            string attribute = string.Empty;
            string labname = string.Empty;

            String File = "";

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }


            //var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attribute = xtr.ReadInnerXml();
                    attribute = attribute.Replace("</p>", "");
                    attribute = attribute.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    labname = xtr.ReadInnerXml();
                    labname = labname.Replace("</p>", "");
                }
            }
            xtr.Close();

            String _string = "";
            for (int i = attribute.Length - 1; i > -1; i--)
            {
                _string += attribute[i];
            }

            var _str = _string.Substring(0, 1);
            _string = _string.Replace(_str, "");

            for (int i = _str.Length - 1; i > -1; i--)
            {
                attribute += _str[i];
            }

            attribute = attribute.Replace(">>", "");

            return attribute;
        }

        public static String cleanImage(String S)
        {
            String imagen;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Imagen ");
                if (i >= 0)
                {
                    f = S.IndexOf("/>", i);
                    imagen = S.Substring(i, (f + 2) - i);
                    S = S.Replace(imagen, " ");
                    content = S;
                }
                else S = null;
            }
            return content;
        }

        public static String cleanTable(String S)
        {
            String table;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Tabla ");
                if (i >= 0)
                {
                    f = S.IndexOf("</Tabla>", i);
                    table = S.Substring(i, (f + 8) - i);
                    S = S.Replace(table, " ");
                    content = S;
                }
                else S = null;
            }
            return content;
        }

        public JsonResult GetHTMLContentAsc(string Client, string Edition, string Product, string Country)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            int CountryId = int.Parse(Country);

            var pp = (from _pp in db.ParticipantProducts
                      where _pp.ClientId == ClientId
                      && _pp.ProductId == ProductId
                      && _pp.EditionId == EditionId
                      select _pp).ToList();

            String HTMLContent = pp[0].HTMLContent;

            List<Countries> LC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            string CountryName = ReplaceCountryName(LC[0].CountryName);

            HTMLContent = HTMLContent.Replace("<img src=\"img", "<img src=\"http://www.plmconnection.com/plmservices/HealthSuppliersGuideEngine/" + CountryName + "/img");

            return Json(HTMLContent, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContent(string Client, string Edition, string Product)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);

            Getattributes Getattributes = new Models.Getattributes();
            List<Getattributes> LPC = new List<Getattributes>();

            var pc = (from _pc in db.ProductContents
                      where _pc.ClientId == ClientId
                      && _pc.EditionId == EditionId
                      && _pc.ProductId == ProductId
                      select _pc).ToList();

            if (pc.LongCount() > 0)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                foreach (ProductContents _pc in pc)
                {
                    doc.LoadHtml(_pc.HtmlContent);

                    var d = doc.DocumentNode;

                    String content = d.InnerHtml;

                    content = clearname(content);

                    var attr = (from _at in db.Attributes
                                where _at.AttributeId == _pc.AttributeId
                                select _at).ToList();
                    String AttributeName = "";
                    foreach (Attributes _attr in attr)
                    {
                        AttributeName = _attr.Description.Trim();
                    }

                    int ln = AttributeName.Length;

                    if (d.InnerHtml.ToUpper().Contains("<TABLE"))
                    {
                        content = d.InnerHtml;

                        content = clearname(content);
                    }

                    if (d.InnerHtml.ToUpper().Contains("<IMG"))
                    {
                        content = d.InnerHtml;

                        content = clearname(content);
                    }

                    String Value = String.Empty;
                    String tag = "";
                    String tags = "";
                    String cierre = "";
                    int j, i, k;
                    String _string = content;
                    i = _string.IndexOf(_string, 0);
                    if (i >= 0)
                    {
                        j = _string.IndexOf("</span>", i);
                        Value = _string.Substring(i, j - i);
                        cierre = Value;

                        k = cierre.Replace(".", "").ToUpper().Trim().IndexOf(AttributeName, i);
                        tag = cierre.Substring(i, k - i);
                        tags = tag;

                        if (cierre != string.Empty)
                        {
                            cierre = cierre.Replace(tags, "");
                            string target = tags + cierre;
                            string close = tags + cierre.ToUpper() + ":";

                            close = close.Replace("::", ":");

                            if (content.ToUpper().Trim().StartsWith(close.ToUpper().Trim()))
                            {
                                content = content.Trim().Replace(target, tags);
                            }
                            else
                            {
                                close = close.Replace(":", "");
                                content = content.Trim().Replace(target, tags);
                            }

                        }
                    }

                    Getattributes = new Getattributes();

                    Getattributes.Content = content;
                    Getattributes.AttributeName = AttributeName.ToUpper().Trim();

                    LPC.Add(Getattributes);
                }

                LPC = LPC.OrderBy(x => x.AttributeName).ToList();

                return Json(LPC, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public static string clearname(string _string)
        {
            _string = _string.Replace("&uacute;", "ú");
            _string = _string.Replace("&oacute;", "ó");
            _string = _string.Replace("&iacute;", "í");
            _string = _string.Replace("&eacute;", "é");
            _string = _string.Replace("&aacute;", "á");
            _string = _string.Replace("&Uacute;", "Ú");
            _string = _string.Replace("&Oacute;", "Ó");
            _string = _string.Replace("&Iacute;", "Í");
            _string = _string.Replace("&Eacute;", "É");
            _string = _string.Replace("&Aacute;", "Á");
            _string = _string.Replace("&#174;", "®");
            _string = _string.Replace("\r", "");
            _string = _string.Replace("\n", "");
            _string = _string.Replace("\t", "");
            _string = _string.Replace("&#9;", "");
            _string = _string.Replace("&NTILDE;", "Ñ");
            _string = _string.Replace("&ntilde;", "ñ");

            return _string;
        }

        [HttpPost]
        public ActionResult SaveHTMLFile(HttpPostedFileBase file, string Product, string Client, string Edition)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            string pname = string.Empty;
            string prop = string.Empty;
            string attribute = string.Empty;
            string labname = string.Empty;

            String File = "";

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }

            //var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("</p>", "");
                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attribute = xtr.ReadInnerXml();
                    attribute = attribute.Replace("</p>", "");
                    attribute = attribute.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    labname = xtr.ReadInnerXml();
                    labname = labname.Replace("</p>", "");
                }
            }
            xtr.Close();


            ind.PId = ProductId;

            string html = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            var CL = from client in db.Clients
                     where client.ClientId == ClientId
                     select client;

            var prod = (from product in db.Products
                        where product.ProductId == ProductId
                        select product).ToList();

            foreach (Clients _client in CL)
            {
                if (_client.ShortName == null)
                {
                    html = _client.CompanyName;
                }
                if (_client.ShortName == string.Empty)
                {
                    html = _client.CompanyName;
                }
                else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                {
                    html = _client.ShortName;
                }
            }

            foreach (Products pr in prod)
            {
                html = html + "_" + pr.ProductName;
            }

            html = ClassReplace.replacehtmlfilename(html);

            html = html.ToLower();

            html = html + "_tmp" + extension;
            //var path = Path.Combine(Server.MapPath("~/App_Data/HTML"), html);
            var path = Path.Combine(PathP, "HTML", html);
            //var desc = Path.Combine(Server.MapPath("~/App_Data/XML"));
            var desc = Path.Combine(PathP, "XML");
            try
            {
                file.SaveAs(path);
            }
            catch (Exception)
            {

            }



            if (System.IO.File.Exists(path))
            {
                FileInfo FI = new FileInfo(path);
                String filename = FI.Name;
                String directoryname = FI.DirectoryName;

                String concatfiledir = directoryname + "\\" + filename;

                filename = filename.Replace(".html", "");
                filename = filename.Replace(".htm", "");



                String _return = SegmentContent.getxml(concatfiledir, filename, desc, pname, prop, attribute, labname);

                if (_return == "errorProductName")
                {
                    return Json("errorProductName", JsonRequestBehavior.AllowGet);
                }

                else if (_return == "errorPropag")
                {
                    return Json("errorPropag", JsonRequestBehavior.AllowGet);
                }

                else if (_return == "errorLaboratory")
                {
                    return Json("errorLaboratory", JsonRequestBehavior.AllowGet);
                }

                else if (_return == "errorAttributes")
                {
                    return Json("errorAttributes", JsonRequestBehavior.AllowGet);
                }
                else if (_return == "Error_File")
                {
                    return Json("Error_File", JsonRequestBehavior.AllowGet);

                }



                String check = checkXML(desc, filename);
                if (check == "_error")
                {
                    try
                    {
                        string xmlpath = desc + @"\" + filename + ".xml";
                        System.IO.File.Delete(path);
                        System.IO.File.Delete(xmlpath);
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string xmlpath = desc + @"\" + filename + ".xml";

                    string destinyPath = desc + @"\" + filename + ".xml";

                    destinyPath = destinyPath.Replace("_tmp", "");

                    System.IO.File.Copy(xmlpath, destinyPath, true);

                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (Exception)
                    {

                    }
                    System.IO.File.Delete(xmlpath);

                    path = path.Replace("_tmp", "");
                    xmlpath = xmlpath.Replace("_tmp", "");

                    file.SaveAs(path);

                    StreamReader SR = System.IO.File.OpenText(path);
                    String content = SR.ReadToEnd();

                    bool _response = SegmentContent.inserthtml(ClientId, EditionId, ProductId, content);


                    SR.Close();

                    //string path1 = path.Replace(".html", ".htm");

                    //if (System.IO.File.Exists(path1))
                    //{
                    //    System.IO.File.Delete(path1);
                    //}

                    filename = filename.Replace("_tmp", "");

                    //var source = Path.Combine(Server.MapPath("~/App_Data/XML"), filename + ".xml");
                    var source = Path.Combine(PathP, "XML", filename + ".xml");
                    StreamReader StreamR;
                    StreamR = System.IO.File.OpenText(source);
                    String _content = StreamR.ReadToEnd();

                    bool response = SegmentContent.insertxml(ClientId, EditionId, ProductId, _content);

                    if ((_response == true) && (response == true))
                    {
                        ActivityLog._updatecontentparticipantproducts(EditionId, ClientId, ProductId, ApplicationId, UsersId);
                    }

                    StreamR.Close();

                    var pc = (from _pc in db.ProductContents
                              where _pc.ClientId == ClientId
                              && _pc.EditionId == EditionId
                              && _pc.ProductId == ProductId
                              select _pc).ToList();
                    if (pc.LongCount() > 0)
                    {
                        foreach (ProductContents _pc in pc)
                        {
                            var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _pc.ProductId && x.EditionId == _pc.EditionId && x.ClientId == _pc.ClientId && x.AttributeId == _pc.AttributeId);
                            db.ProductContents.Remove(delete);
                            db.SaveChanges();
                        }
                        ActivityLog._deleteproductcontents(EditionId, ClientId, ProductId, 4);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public string checkXML(string desc, string filename)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(desc + @"\");
            FileInfo fl = new FileInfo(desc + "\\" + filename + ".xml");
            try
            {
                XmlDocument cdoc = new XmlDocument();
                cdoc.Load(fl.FullName);


            }
            catch (Exception e)
            {
                String message = e.Message;
                return "_error";
            }
            return "ok";
        }

        public ActionResult getxml(int Id)
        {
            SessionProduction ind = (SessionProduction)Session["SessionProduction"];

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            if (ind != null)
            {
                int ClientId = ind.ClId;

                String FIleName = "";

                var client = (from c in db.Clients
                              where c.ClientId == ClientId
                              select c).ToList();
                foreach (Clients _client in client)
                {
                    if (_client.ShortName == null)
                    {
                        FIleName = _client.CompanyName;
                    }
                    if (_client.ShortName == string.Empty)
                    {
                        FIleName = _client.CompanyName;
                    }
                    else if ((_client.ShortName != null) && (_client.ShortName != string.Empty))
                    {
                        FIleName = _client.ShortName;
                    }
                }

                var product = (from p in db.Products
                               where p.ProductId == Id
                               select p).ToList();
                foreach (Products _product in product)
                {
                    FIleName = FIleName + "_" + _product.ProductName.Trim();
                }

                FIleName = ClassReplace.replacehtmlfilename(FIleName);

                FIleName = FIleName + ".xml";

                FIleName = FIleName.Replace("_.xml", ".xml");

                //var root = Path.Combine(Server.MapPath("~/App_Data/XML"), FIleName);
                var root = Path.Combine(PathP, "XML", FIleName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "text/xml");
                }
                else
                {
                    var path = Path.Combine(PathP, "XML", "errorfile.html");

                    return PartialView("errorfile");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public JsonResult GetHTMLFiles(string FileName, string Country)
        {
            string File = getfilename();
            int CountryId = int.Parse(Country);

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            var root = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts", File, "HTML", FileName);

            StreamReader s = System.IO.File.OpenText(root);
            String Content = s.ReadToEnd();

            List<Countries> LC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            string CountryName = ReplaceCountryName(LC[0].CountryName);

            Content = Content.Replace("<img src=\"img", "<img src=\"http://www.plmconnection.com/plmservices/HealthSuppliersGuideEngine/" + CountryName + "/img");

            return Json(Content, JsonRequestBehavior.AllowGet);
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               SEGMENTAR HTML POR PRODUCTOS          ************************ */

        public ActionResult SegmentHTML(string CountryId, string EditionId, string BookId)
        {
            SessionSegmentHTML ind = (SessionSegmentHTML)Session["SessionSegmentHTML"];

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];

                if (cu == null)
                {
                    return RedirectToAction("Logout", "Login");
                }

                int UsersId = cu.userId;

                int Country = int.Parse(CountryId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                //var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + "").ToList();

                SessionSegmentHTML SessionSegmentHTML = new SessionSegmentHTML(Country, Edition, Book);
                Session["SessionSegmentHTML"] = SessionSegmentHTML;

                String File = "";

                var ctr = (from c in PLMUsers.Users
                           join count in PLMUsers.CountriesUser
                               on c.CountryId equals count.CountryId
                           where c.UserId == UsersId
                           select count).ToList();
                if (ctr.LongCount() > 0)
                {
                    foreach (CountriesUser _ctr in ctr)
                    {
                        File = "targetsallfile_" + _ctr.ID + ".xml";
                    }
                }


                //var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

                if (System.IO.File.Exists(root))
                {
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(root);

                    XmlNode node;
                    node = myXmlDocument.DocumentElement;

                    XmlReader xtr = XmlReader.Create(root);
                    while (xtr.Read())
                    {
                        if (xtr.Name == "TargetDescription")
                        {
                            String pname = xtr.ReadInnerXml();

                            pname = pname.Replace("> </p>", "");
                            pname = pname.Replace("</p>", "");

                            ViewData["ProductNametargetall"] = pname;
                        }

                        if (xtr.Name == "TargetDescriptionPropag")
                        {
                            String bpropag = xtr.ReadInnerXml();

                            bpropag = bpropag.Replace("</p>", "");

                            ViewData["BPropagandaall"] = bpropag;
                        }

                        if (xtr.Name == "TargetDescriptionAttribute")
                        {
                            String attr = xtr.ReadInnerXml();

                            attr = attr.Replace("</p>", "");
                            attr = attr.Replace("</span>", "");

                            ViewData["Attributell"] = attr;
                        }

                        if (xtr.Name == "TargetDescriptionLaboratory")
                        {
                            String Laboratory = xtr.ReadInnerXml();

                            Laboratory = Laboratory.Replace("</p>", "");
                            Laboratory = Laboratory.Replace("</span>", "");

                            ViewData["LaboratoryL"] = Laboratory;
                        }
                    }
                    xtr.Close();
                }
                else
                {
                    ViewData["validation"] = false;
                }
                List<HTMLProductsAttributes> LS = getlistresume();


                return View(LS);
            }

            if (ind != null)
            {
                CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
                int UsersId = cu.userId;

                int Country = ind.CId;
                int Edition = ind.EId;
                int Book = ind.BId;

                //var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + "").ToList();

                SessionSegmentHTML SessionSegmentHTML = new SessionSegmentHTML(Country, Edition, Book);
                Session["SessionSegmentHTML"] = SessionSegmentHTML;

                String File = "";

                var ctr = (from c in PLMUsers.Users
                           join count in PLMUsers.CountriesUser
                               on c.CountryId equals count.CountryId
                           where c.UserId == UsersId
                           select count).ToList();
                if (ctr.LongCount() > 0)
                {
                    foreach (CountriesUser _ctr in ctr)
                    {
                        File = "targetsallfile_" + _ctr.ID + ".xml";
                    }
                }


                //var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

                var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

                if (System.IO.File.Exists(root))
                {
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(root);

                    XmlNode node;
                    node = myXmlDocument.DocumentElement;

                    XmlReader xtr = XmlReader.Create(root);
                    while (xtr.Read())
                    {
                        if (xtr.Name == "TargetDescription")
                        {
                            String pname = xtr.ReadInnerXml();

                            pname = pname.Replace("> </p>", "");
                            pname = pname.Replace("</p>", "");

                            ViewData["ProductNametargetall"] = pname;
                        }

                        if (xtr.Name == "TargetDescriptionPropag")
                        {
                            String bpropag = xtr.ReadInnerXml();

                            bpropag = bpropag.Replace("</p>", "");

                            ViewData["BPropagandaall"] = bpropag;
                        }

                        if (xtr.Name == "TargetDescriptionAttribute")
                        {
                            String attr = xtr.ReadInnerXml();

                            attr = attr.Replace("</p>", "");
                            attr = attr.Replace("</span>", "");

                            ViewData["Attributell"] = attr;
                        }

                        if (xtr.Name == "TargetDescriptionLaboratory")
                        {
                            String Laboratory = xtr.ReadInnerXml();

                            Laboratory = Laboratory.Replace("</p>", "");
                            Laboratory = Laboratory.Replace("</span>", "");

                            ViewData["LaboratoryL"] = Laboratory;
                        }
                    }
                    xtr.Close();
                }
                else
                {
                    ViewData["validation"] = false;
                }
                List<HTMLProductsAttributes> LS = getlistresume();

                return View(LS);
            }

            else
            {
                int Country = 0;
                int Edition = 0;

                //var ec = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByEdition @EditionId=" + Edition + ", @CountryId=" + Country + "").ToList();

                List<HTMLProductsAttributes> LS = new List<HTMLProductsAttributes>();

                return View(LS);
            }
        }

        public List<HTMLProductsAttributes> getlistresume()
        {
            List<HTMLProductsAttributes> LS = new List<HTMLProductsAttributes>();
            HTMLProductsAttributes item = new HTMLProductsAttributes();

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            String File = "resumeproducts.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            //var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", Folder, File);

            //var root = Path.Combine(map, File);

            if (System.IO.File.Exists(root))
            {
                XmlDocument myXmlDocument = new XmlDocument();
                myXmlDocument.Load(root);

                XmlNode node;
                node = myXmlDocument.DocumentElement;

                XmlReader xtr = XmlReader.Create(root);
                while (xtr.Read())
                {
                    if (xtr.Name == "ProductName")
                    {
                        String pname = xtr.ReadInnerXml();

                        pname = pname.Replace("> </p>", "");
                        pname = pname.Replace("</p>", "");

                        item = new HTMLProductsAttributes();

                        item.HTMLName = pname;

                    }
                    if (xtr.Name == "QuantityOfAttributes")
                    {
                        String Attributes = xtr.ReadInnerXml();
                        int QuantityOfAttributes = int.Parse(Attributes);

                        item.QuantityOfAttributes = QuantityOfAttributes;

                        LS.Add(item);
                    }

                }
                xtr.Close();
            }
            return LS;
        }

        public ActionResult segmentallcontent(HttpPostedFileBase file)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            string pname = string.Empty;
            string prop = string.Empty;
            string attr = string.Empty;
            string Laboratory = string.Empty;

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "";

            var ctr = (from c in PLMUsers.Users
                       join count in PLMUsers.CountriesUser
                           on c.CountryId equals count.CountryId
                       where c.UserId == UsersId
                       select count).ToList();
            if (ctr.LongCount() > 0)
            {
                foreach (CountriesUser _ctr in ctr)
                {
                    File = "targetsallfile_" + _ctr.ID + ".xml";
                }
            }

            //var root = Server.MapPath("~/App_Data/Templates/XMLFILES/" + File);

            var root = Path.Combine(PathP, "Templates", "XMLFILES", File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(root);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(root);
            while (xtr.Read())
            {
                if (xtr.Name == "TargetDescription")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("></p>", "");
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");

                }

                if (xtr.Name == "TargetDescriptionPropag")
                {
                    prop = xtr.ReadInnerXml();
                    prop = prop.Replace("></p>", "");
                    prop = prop.Replace("</p>", "");

                }

                if (xtr.Name == "TargetDescriptionAttribute")
                {
                    attr = xtr.ReadInnerXml();

                    attr = attr.Replace("</p>", "");
                    attr = attr.Replace("</span>", "");
                }

                if (xtr.Name == "TargetDescriptionLaboratory")
                {
                    Laboratory = xtr.ReadInnerXml();

                    Laboratory = Laboratory.Replace("</p>", "");
                    Laboratory = Laboratory.Replace("</span>", "");
                }

            }

            xtr.Close();

            String FileName = file.FileName;
            String Extention = Path.GetExtension(file.FileName);

            //var rootcopy = Server.MapPath("/App_Data/SegmentProducts/CompleteHtml");

            var rootcopy = Path.Combine(PathP, "SegmentProducts", "CompleteHtml");

            try
            {
                if (System.IO.Directory.Exists(rootcopy))
                {
                    rootcopy = Path.Combine(rootcopy, FileName);

                    file.SaveAs(rootcopy);
                }
                else
                {
                    Directory.CreateDirectory(rootcopy);

                    rootcopy = Path.Combine(rootcopy, FileName);

                    file.SaveAs(rootcopy);
                }

            }
            catch (Exception e)
            {
                String msg = e.Message;

                return Json(msg, JsonRequestBehavior.AllowGet);
            }

            //var rootgetfile = Path.Combine(Server.MapPath("~/App_Data/SegmentProducts/CompleteHtml"), FileName);

            var rootgetfile = Path.Combine(PathP, "SegmentProducts", "CompleteHtml", FileName);

            if (System.IO.File.Exists(rootgetfile))
            {
                string resp = "";
                try
                {
                    resp = gethtml(pname, prop, rootgetfile, attr, Laboratory);
                }
                catch (Exception e)
                {
                    string msg = e.Message;

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }


                if (resp == "_error")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    FileName = FileName.Replace(Extention, "");

                    //string path2 = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts/" + FileName + "/XML");

                    var path2 = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts", FileName, "XML");

                    List<String> check = VERIFYXML(path2);

                    if (check.LongCount() > 0)
                    {
                        String Filen = "resumeproducts.xml";
                        String Foldern = "";
                        var usr = (from _usr in PLMUsers.Users
                                   where _usr.UserId == UsersId
                                   select _usr).ToList();
                        foreach (Users _usr in usr)
                        {
                            Foldern = _usr.NickName;
                        }

                        //var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Foldern);

                        var map = Path.Combine(PathP, "Templates", "XMLFILES", Foldern);

                        var rootmap = Path.Combine(map, Filen);

                        // System.IO.File.Delete(rootmap);

                        getfilehtml(attr);

                        List<HTMLProductsAttributes> LS = getlistresume();

                        SessionErrorFiles SessionErrorFiles = new SessionErrorFiles(check);
                        Session["SessionErrorFiles"] = SessionErrorFiles;

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        getfilehtml(attr);

                        List<HTMLProductsAttributes> LS = getlistresume();

                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public List<String> VERIFYXML(string desc)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(desc + @"\");
            List<String> Litems = new List<String>();
            foreach (FileInfo fln in dir.GetFiles())
            {
                try
                {
                    XmlDocument cdoc = new XmlDocument();
                    cdoc.Load(fln.FullName);
                }
                catch (Exception e)
                {
                    Litems.Add(fln.Name);
                }
            }
            return Litems;
        }

        public string gethtml(String pname, String prop, String rootgetfile, String attr, String Laboratory)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            //string temp = Path.Combine(Server.MapPath("~/App_Data/Templates"), "IndesignTemplate.htm");

            var temp = Path.Combine(PathP, "Templates", "IndesignTemplate.htm");

            string token3 = "<p class='t-tulo-prod-nuevo'>";
            string s = "";
            string path3 = "";
            int cont = 0;
            int count = 0;
            do
            {
                string path = rootgetfile;
                string pathD = path.Substring(0, path.LastIndexOf("\\") + 1);

                DirectoryInfo directory = new DirectoryInfo(path);

                FileInfo item = new FileInfo(path);

                cont++;
                string filename = item.Name.Substring(0, item.Name.IndexOf("."));

                SessionFileName SessionFileName = new SessionFileName(filename);
                Session["SessionFileName"] = SessionFileName;

                //string path2 = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts");

                string path2 = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts");


                string allcontent = getHtmlContent(item.FullName);

                string template = "";
                try
                {
                    template = getHtmlContent((temp));
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                }

                string name, pf, inf;

                int ini, ini2, ini3, next;
                int fin, fin2, med, med2;
                int len, len2, len3;

                len = pname.Length;
                len3 = token3.Length;

                next = 0;
                inf = "";
                ini3 = 0;
                if (allcontent.IndexOf(token3, next) != -1)
                {
                    ini3 = allcontent.IndexOf(token3, next);
                    inf = allcontent.Substring(ini3, (allcontent.IndexOf("<", (ini3 + len3)) - ini3));
                }

                for (int i = 0; i < allcontent.Length; i++)
                {
                    try
                    {

                        //}
                        //catch(Exception e)
                        //{

                        //}

                        if (allcontent.IndexOf(pname, next) != 1)
                        {
                            StreamWriter SW;

                            String rootbeg = "<Root>";
                            String rootend = "</Root>";
                            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

                            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
                            int UsersId = cu.userId;

                            String File = "filename.xml";
                            String Folder = "";
                            var usr = (from _usr in PLMUsers.Users
                                       where _usr.UserId == UsersId
                                       select _usr).ToList();
                            foreach (Users _usr in usr)
                            {
                                Folder = _usr.NickName;
                            }

                            // var rr = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

                            var rr = Path.Combine(PathP, "Templates", "XMLFILES", Folder);

                            var map = "";

                            if (System.IO.Directory.Exists(rr))
                            {
                                map = Path.Combine(rr, File);
                            }
                            else
                            {
                                DirectoryInfo dir = System.IO.Directory.CreateDirectory(rr);
                                map = Path.Combine(rr, File);
                            }

                            SW = System.IO.File.CreateText(map);
                            SW.Write(xml);
                            SW.Write(rootbeg);

                            SW.Write("<TargetName>");
                            SW.Write("Producto");
                            SW.Write("</TargetName>");

                            SW.Write("<ProductName>");
                            SW.Write(filename);
                            SW.Write("</ProductName>");

                            SW.Write(rootend);
                            SW.Close();


                            count = count + 1;

                            ini = allcontent.IndexOf(pname, next);
                            med = allcontent.IndexOf(">", ini);
                            fin = allcontent.IndexOf("</p", med);

                            name = allcontent.Substring(med + 1, (fin - med) - 1);
                            name = ClassReplace.quitAccentsFileName(name);
                            int xx = allcontent.IndexOf(pname, fin);
                            ini2 = allcontent.IndexOf(prop, next);

                            if (ini2 > 0 && ini2 < allcontent.IndexOf(pname, fin))
                            {
                                med2 = allcontent.IndexOf(">", ini2);
                                fin2 = allcontent.IndexOf("</", med2);
                                pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                                pf = ClassReplace.quitAccentsFileName(pf);
                                pf = ClassReplace.cleanName(pf);
                            }
                            else
                                pf = string.Empty;



                            next = allcontent.IndexOf(pname, fin);

                            System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                            if (next > 0)
                            {
                                string file = allcontent.Substring(ini, (next - ini));

                                file = ClassReplace.quitAccents(file);
                                file = ClassReplace.changeImage(file, filename);
                                file = ClassReplace.findEmail(file, filename);
                                file = ClassReplace.findUrl(file, filename);

                                file = file.Replace("xml:lang='es-ES'>", "");
                                file = file.Replace("<p class=\"Antetitulo\">Información nueva</p>", "");

                                file = file.Replace("				<colgroup>", "");
                                file = file.Replace("					<col />", "");
                                file = file.Replace("				</colgroup>", "");



                                file = file.Replace("class=\"Ningún-estilo-de-tabla\"", "");
                                file = file.Replace("class=\"Ning&uacute;n-estilo-de-tabla\"", "");
                                file = file.Replace("Ning&uacute;n-estilo-de-tabla", "Ningun-estilo-de-tabla");
                                file = file.Replace(" lang=\"en-US\"", "");
                                file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACIÓN NUEVA</p>", "");
                                file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACI&Oacute;N NUEVA</p>", "");
                                file = file.Replace(" lang=\"es-ES\"", "");
                                file = file.Replace("<td class=\"Tabla-b-sica Tabla\" />", "<td class=\"Tabla-b-sica Tabla\"> </td>");
                                file = file.Replace("                                      </p>", "</p>");
                                file = file.Replace("\n\n\n", "");
                                file = file.Replace("</span><span class=\"bold-entrada\">", "");
                                file = file.Replace("<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\" />", "<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\"> </td>");
                                file = file.Replace("<p class=\"pf_especial_titulo_prod\">", "<p class=\"pf_normal\">");
                                file = file.Replace("T-tulo-prod--nuevo", "T-tulo");


                                ClassReplace.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                                sb.Replace("@@@Contenido@@@", inf + file);

                                inf = ClassReplace.getString(file, token3);
                            }
                            else
                            {
                                string file = allcontent.Substring(ini);

                                file = ClassReplace.quitAccents(file);
                                ClassReplace.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
                                sb.Replace("@@@Contenido@@@", inf + file);

                                i = allcontent.Length;
                            }

                            path3 = path2;

                            //path3 = path3 + "\\" + filename + "\\HTML";

                            path3 = Path.Combine(path3, filename, "HTML");

                            if (System.IO.Directory.Exists(path3))
                            {
                                var root1 = path2;

                                ClassReplace.saveHtmlFile(path3,
                                name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());

                                String file = name.Trim() + "_" + pf.Trim();
                                String desc = Path.Combine(root1, filename, "XML");

                                if (System.IO.Directory.Exists(desc))
                                {
                                    file = file + ".htm";
                                    path3 = Path.Combine(path3, file);
                                    try
                                    {
                                        string _response = SegmentContent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                                    }
                                    catch (Exception e)
                                    {
                                        string msg = e.Message;
                                    }
                                }
                                else
                                {
                                    System.IO.Directory.CreateDirectory(desc);

                                    file = file + ".htm";
                                    path3 = Path.Combine(path3, file);
                                    SegmentContent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                                }
                            }
                            else
                            {
                                var root1 = path2;

                                var root = Path.Combine(path2, filename, "HTML");

                                //System.IO.Directory.CreateDirectory(path2 + "\\" + filename + "\\HTML");

                                System.IO.Directory.CreateDirectory(root);

                                ClassReplace.saveHtmlFile(root,
                                name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());

                                String file = name.Trim() + "_" + pf.Trim();
                                String desc = Path.Combine(root1, filename, "XML");
                                if (System.IO.Directory.Exists(desc))
                                {
                                    file = file + ".htm";
                                    path3 = Path.Combine(path3, file);
                                    SegmentContent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                                }
                                else
                                {
                                    System.IO.Directory.CreateDirectory(desc);

                                    file = file + ".htm";
                                    path3 = Path.Combine(path3, file);
                                    SegmentContent.getxml(path3, file, desc, pname, prop, attr, Laboratory);
                                }

                            }
                            name = "";
                            pf = "";
                        }
                        else
                        {
                            return "_error";
                        }
                    }
                    catch (Exception e)
                    {
                        string msg = e.Message;
                    }

                }
            }
            while (s.ToLower() == "s");

            if (count == 0)
            {
                return "_error";
            }
            else
            {
                return "";
            }
        }

        public string getfilehtml(String attr)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            attr = attr.Replace("\t", "");
            String _string = "";
            for (int i = attr.Length - 1; i > -1; i--)
            {
                _string += attr[i];
            }

            var _str = _string.Substring(0, 1);
            _string = _string.Replace(_str, "");

            for (int i = _str.Length - 1; i > -1; i--)
            {
                attr += _str[i];
            }

            attr = attr.Replace(">>", "");

            StreamWriter SW;

            String rootbeg = "<Root>";
            String rootend = "</Root>";
            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "resumeproducts.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            //var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

            var map = Path.Combine(PathP, "Templates", "XMLFILES", Folder);

            var rootmap = Path.Combine(map, File);

            //string root = Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts");

            var root = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts");

            SessionFileName sessionfn = (SessionFileName)Session["SessionFileName"];

            List<HTMLProductsAttributes> LS = new List<HTMLProductsAttributes>();
            HTMLProductsAttributes item = new HTMLProductsAttributes();

            if (sessionfn != null)
            {
                root = root + "\\" + sessionfn.filename;

                root = Path.Combine(root, "HTML");

                DirectoryInfo DI = new DirectoryInfo(root);

                FileInfo[] FI = DI.GetFiles();

                if (FI.Length != 0)
                {
                    SW = System.IO.File.CreateText(rootmap);
                    SW.Write(xml);
                    SW.Write(rootbeg);
                    foreach (FileInfo _fi in FI)
                    {
                        if ((_fi.Name != null) || (_fi.Name != String.Empty))
                        {
                            SW.Write("<TargetName>");
                            SW.Write("Producto");
                            SW.Write("</TargetName>");

                            SW.Write("<ProductName>");
                            SW.Write(_fi.Name);
                            SW.Write("</ProductName>");
                        }

                        var completepath = Path.Combine(root, _fi.Name);

                        StreamReader SR = System.IO.File.OpenText(completepath);
                        String content = SR.ReadToEnd();
                        String Info = "";
                        int count = 0;
                        if (!String.IsNullOrEmpty(content))
                        {
                            String target = attr.Trim();
                            String endtag = "</span>";
                            String attrcontent = null;
                            String tagfooter = "<p class=\"pie";
                            int beg, end, tagend, infaux, footer;

                            while (content != null)
                            {
                                beg = content.IndexOf(target);
                                if (beg >= 0)
                                {


                                    end = content.IndexOf(endtag, beg);
                                    tagend = content.IndexOf(">", beg + target.Length);
                                    Info = content.Substring(tagend + 1, end - (tagend + 1));

                                    infaux = content.IndexOf(target, end);

                                    if (!String.IsNullOrEmpty(Info))
                                    {
                                        count = count + 1;
                                    }

                                    if (infaux >= 0)
                                    {
                                        attrcontent = content.Substring(end + endtag.Length, infaux - (end + endtag.Length));
                                        content = content.Substring(infaux, content.Length - infaux);
                                    }
                                    else
                                    {
                                        footer = content.IndexOf(tagfooter, end);
                                        if (footer >= 0)
                                        {
                                            attrcontent = content.Substring(end, footer - end);
                                        }
                                        else
                                        {
                                            attrcontent = content.Substring(end, content.Length - end);
                                            content = null;
                                        }
                                    }
                                }
                                else
                                {
                                    content = null;
                                }
                            }
                        }
                        SW.Write("<QuantityOfAttributes>");
                        SW.Write(count);
                        SW.Write("</QuantityOfAttributes>");
                        item.QuantityOfAttributes = count;

                        SR.Close();
                    }
                    SW.Write(rootend);
                    SW.Close();
                }
            }
            //return View("segmentallproducts", LS);
            return "ok";
        }

        public string getHtmlContent(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentException("The source path cannot be null or empty.");

            StreamReader sr = new StreamReader(sourcePath, System.Text.Encoding.UTF8);
            string strHtml = sr.ReadToEnd();

            sr.Close();
            sr.Dispose();

            return strHtml;
        }

        public ActionResult asociateContent(string Product, string Client, string FileName, string Edition)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            CountriesUsers pr = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = pr.ApplicationId;
            int UsersId = pr.userId;

            int _clienid = int.Parse(Client);
            int _productid = int.Parse(Product);
            int _editionid = int.Parse(Edition);

            SessionFileName SessionFileName = (SessionFileName)Session["SessionFileName"];
            String Folder = getfilename();

            String FIleName = "";

            var client = (from c in db.Clients
                          where c.ClientId == _clienid
                          select c).ToList();
            foreach (Clients _client in client)
            {
                FIleName = FIleName + _client.CompanyName;
            }

            var product = (from p in db.Products
                           where p.ProductId == _productid
                           select p).ToList();
            foreach (Products _product in product)
            {
                FIleName = FIleName + "_" + _product.ProductName.Trim();
            }

            FIleName = ClassReplace.replacehtmlfilename(FIleName);

            FIleName = FIleName + ".htm";

            //string path = Path.Combine(Server.MapPath("/App_Data/SegmentProducts/SegmentedProducts"), Folder, "HTML");

            string path = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts", Folder, "HTML");

            path = Path.Combine(path, FileName);

            //var pathxml = Path.Combine(Server.MapPath("~/App_Data/SegmentProducts/SegmentedProducts"), Folder, "XML");

            string pathxml = Path.Combine(PathP, "SegmentProducts", "SegmentedProducts", Folder, "XML");

            FileName = FileName.Replace(".htm", "");
            FileName = FileName.Replace(".html", "");

            string xmlfile = FileName + ".xml";

            pathxml = Path.Combine(pathxml, xmlfile);

            //var deschtml = Path.Combine(Server.MapPath("~/App_Data/HTML"));
            //var descxml = Path.Combine(Server.MapPath("~/App_Data/XML"));

            var deschtml = Path.Combine(PathP, "HTML");
            var descxml = Path.Combine(PathP, "XML");

            var pathdest = deschtml + "\\" + FIleName;

            string xml = FIleName.Replace(".htm", "").Replace(".html", "");

            xml = xml + ".xml";

            string pathdesc = Path.Combine(descxml, xml);

            if ((System.IO.File.Exists(pathdest)) || (System.IO.File.Exists(pathdesc)))
            {
                try
                {
                    if (System.IO.File.Exists(pathdest))
                    {
                        System.IO.File.Delete(pathdest);
                    }
                    if (System.IO.File.Exists(pathdesc))
                    {
                        System.IO.File.Delete(pathdesc);
                    }
                    if (System.IO.File.Exists(pathdest + "l"))
                    {
                        System.IO.File.Delete(pathdest + "l");
                    }

                }
                catch (Exception e)
                {

                }
            }
            try
            {
                System.IO.File.Copy(path, pathdest);
                System.IO.File.Copy(pathxml, pathdesc);
            }
            catch (Exception e)
            {

            }

            if (System.IO.File.Exists(pathdest))
            {
                FileInfo FI = new FileInfo(pathdest);
                String filename = FI.Name;
                String directoryname = FI.DirectoryName;

                String concatfiledir = directoryname + "\\" + filename;

                filename = filename.Replace(".html", "");
                filename = filename.Replace(".htm", "");


                String check = checkXMLallproducts(pathdesc);

                if (check == "_error")
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StreamReader SR = System.IO.File.OpenText(pathdest);
                    String content = SR.ReadToEnd();

                    bool _response = SegmentContent.inserthtml(_clienid, _editionid, _productid, content);

                    SR.Close();

                    var source = pathdesc;
                    StreamReader StreamR;
                    StreamR = System.IO.File.OpenText(source);
                    String _content = StreamR.ReadToEnd();

                    bool response = SegmentContent.insertxml(_clienid, _editionid, _productid, _content);

                    if ((_response == true) && (response == true))
                    {
                        ActivityLog._updatecontentparticipantproducts(_editionid, _clienid, _productid, ApplicationId, UsersId);
                    }

                    StreamR.Close();

                    var pc = (from _pc in db.ProductContents
                              where _pc.ClientId == _clienid
                              && _pc.EditionId == _editionid
                              && _pc.ProductId == _productid
                              select _pc).ToList();
                    if (pc.LongCount() > 0)
                    {
                        foreach (ProductContents _pc in pc)
                        {
                            var delete = db.ProductContents.SingleOrDefault(x => x.ProductId == _pc.ProductId && x.EditionId == _pc.EditionId && x.ClientId == _pc.ClientId && x.AttributeId == _pc.AttributeId);
                            db.ProductContents.Remove(delete);
                            db.SaveChanges();
                        }
                        ActivityLog._deleteproductcontents(_editionid, _clienid, _productid, 4);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }

        public string checkXMLallproducts(string descxml)
        {
            FileInfo fl = new FileInfo(descxml);
            try
            {
                XmlDocument cdoc = new XmlDocument();
                cdoc.Load(fl.FullName);
            }
            catch (Exception e)
            {
                String message = e.Message;
                return "_error";
            }
            return "ok";
        }

        public String getfilename()
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            CountriesUsers cu = (CountriesUsers)Session["CountriesUsers"];
            int UsersId = cu.userId;

            String File = "filename.xml";
            String Folder = "";
            var usr = (from _usr in PLMUsers.Users
                       where _usr.UserId == UsersId
                       select _usr).ToList();
            foreach (Users _usr in usr)
            {
                Folder = _usr.NickName;
            }

            //var map = Server.MapPath("~/App_Data/Templates/XMLFILES/" + Folder);

            var map = Path.Combine(PathP, "Templates", "XMLFILES", Folder);

            var rootmap = Path.Combine(map, File);

            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(rootmap);

            XmlNode node;
            node = myXmlDocument.DocumentElement;

            XmlReader xtr = XmlReader.Create(rootmap);

            String pname = "";
            while (xtr.Read())
            {
                if (xtr.Name == "ProductName")
                {
                    pname = xtr.ReadInnerXml();
                    pname = pname.Replace("> </p>", "");
                    pname = pname.Replace("</p>", "");
                }
            }
            xtr.Close();

            return pname;
        }

        public JsonResult checkcontent(string Product, string Client, string Edition)
        {
            if (Product != null)
            {
                int EditionId = int.Parse(Edition);
                int ProductId = int.Parse(Product);
                int ClientId = int.Parse(Client);

                var pp = (from _pp in db.ParticipantProducts
                          where _pp.ProductId == ProductId
                          && _pp.EditionId == EditionId
                          && _pp.ClientId == ClientId
                          && _pp.HTMLContent != null
                          && _pp.XMLContent != null
                          select _pp).ToList();
                if (pp.LongCount() > 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetXMLCNT(string Edition, string Client, string Product)
        {
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);

            var pp = db.ParticipantProducts.Where(x => x.EditionId == EditionId && x.ClientId == ClientId && x.ProductId == ProductId).Select(x => x.XMLContent).ToList();

            String XML = pp[0];

            XML = XML.Replace("<", "< ");
            XML = XML.Replace(">", " >");

            XML = XML.Replace("< ?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"? >", "<b>< ?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"? ></b>");
            XML = XML.Replace("< ?xm", "<br>< ?xm");
            XML = XML.Replace("< Root >", "<br>< Root >");
            XML = XML.Replace("< Root >", "< Root ><br>");
            XML = XML.Replace("< Producto >", "< Producto ><br>");
            XML = XML.Replace("< /BasePropaganda >", "< /BasePropaganda ><br>");

            XML = XML.Replace("< /Titulo >", "< /Titulo ><br>");
            XML = XML.Replace("< Rubro >", "< Rubro ><br>");
            XML = XML.Replace("< Rubro >", "<br>< Rubro >");
            XML = XML.Replace("< /Rubro >", "< /Rubro ><br>");
            XML = XML.Replace("< /Rubro >", "<br>< /Rubro >");
            XML = XML.Replace("< /Root >", "<br>< /Root >");


            XML = XML.Replace("< Root >", "<b>< Root ></b>");
            XML = XML.Replace("< Producto >", "&nbsp;&nbsp;<b>< Producto ></b>");
            XML = XML.Replace("< BasePropaganda >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< BasePropaganda ></b>");
            XML = XML.Replace("< Titulo >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< Titulo ></b>");
            XML = XML.Replace("< Rubro >", "&nbsp;&nbsp;&nbsp;&nbsp;<b>< Rubro ></b>");
            XML = XML.Replace("< Contenido >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< Contenido ></b>");
            XML = XML.Replace("< Titulo >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< Titulo ></b>");
            XML = XML.Replace("< Contenido >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< Contenido ></b>");
            XML = XML.Replace("< Nombre >", "&nbsp;&nbsp;&nbsp;&nbsp;<b>< Nombre ></b>");

            XML = XML.Replace("< /Root >", "<b>< /Root ></b>");
            XML = XML.Replace("< /Producto >", "&nbsp;&nbsp;<b>< /Producto ></b>");
            XML = XML.Replace("< /BasePropaganda >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< /BasePropaganda ></b>");
            XML = XML.Replace("< /Titulo >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< /Titulo ></b>");
            XML = XML.Replace("< /Rubro >", "&nbsp;&nbsp;&nbsp;&nbsp;<b>< /Rubro ></b>");
            XML = XML.Replace("< /Contenido >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< /Contenido ></b>");
            XML = XML.Replace("< /Nombre >", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>< /Nombre ></b>");



            return Json(XML, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParticipantClient(string Client)
        {
            int ClientId = int.Parse(Client);

            if (ClientId == 0)
            {
                Session["SessionClientId"] = null;
            }
            else
            {
                SessionClientId SessionClientId = new SessionClientId(ClientId);
                Session["SessionClientId"] = SessionClientId;
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }



        /************************* ************************ ************************ ************************ ************************ */
        /************************               SPECIALPRODUCTS          ************************ */



        public ActionResult SpecialProducts(string CountryId, string EditionId, string BookId, string ClientTypeId, string CompanyName, string Type)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionPRSP ind = (SessionPRSP)Session["SessionPRSP"];

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
                        SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionPRSP"] = SessionPRSP;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
                    }
                    else
                    {
                        SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionPRSP"] = SessionPRSP;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, null);
                    Session["SessionPRSP"] = SessionPRSP;

                    LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
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
                        SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionPRSP"] = SessionPRSP;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
                    }
                    else
                    {
                        SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionPRSP"] = SessionPRSP;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionPRSP SessionPRSP = new SessionPRSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                    Session["SessionPRSP"] = SessionPRSP;

                    LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
                }

                return View(LC);
            }
            else
            {
                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult ProductAdverts(int? ClientId)
        {
            SessionPRSP ind = (SessionPRSP)Session["SessionPRSP"];

            if ((!Request.IsAuthenticated) || (ClientId == null) || (ind == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            ind.ClId = Convert.ToInt32(ClientId);
            int EditionId = ind.EId;

            List<ProductAdverts> LPA = db.Database.SqlQuery<ProductAdverts>("plm_spGetProductAdverts @ClientId=" + ClientId + ",@EditionId=" + EditionId + "").ToList();

            return View(LPA);
        }

        public static string ReplaceCountryName(string CountryName)
        {
            CountryName = CountryName.Replace("Á", "A");
            CountryName = CountryName.Replace("É", "E");
            CountryName = CountryName.Replace("Í", "I");
            CountryName = CountryName.Replace("Ó", "O");
            CountryName = CountryName.Replace("Ú", "U");
            CountryName = CountryName.Replace("Ü", "U");

            CountryName = CountryName.Replace("á", "a");
            CountryName = CountryName.Replace("é", "e");
            CountryName = CountryName.Replace("í", "i");
            CountryName = CountryName.Replace("ó", "o");
            CountryName = CountryName.Replace("ú", "u");
            CountryName = CountryName.Replace("ü", "u");

            return CountryName;
        }


        /************************* ************************ ************************ ************************ ************************ */
        /************************               EDITIONCATEGORYTHREE          ************************ */

        //
        public ActionResult CategoryThree(string CountryId, string EditionId, string BookId)
        {
            SessionEditionCategoryThree ind = (SessionEditionCategoryThree)Session["SessionEditionCategoryThree"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

                List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + Edition + "").OrderBy(x => x.CategoryThree).ToList();

                SessionEditionCategoryThree SessionEditionCategoryThree = new SessionEditionCategoryThree(Country, Edition, Book);
                Session["SessionEditionCategoryThree"] = SessionEditionCategoryThree;

                return View(ec);
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

                List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + Edition + "").OrderBy(x => x.CategoryThree).ToList();

                SessionEditionCategoryThree SessionEditionCategoryThree = new SessionEditionCategoryThree(Country, Edition, Book);
                Session["SessionEditionCategoryThree"] = SessionEditionCategoryThree;

                return View(ec);
            }

            else
            {
                int Edition = 0;

                List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree  @CRUDType=" + CRUD.Read + ", @EditionId=" + Edition + "").ToList();

                return View(ec);
            }
        }

        public JsonResult addCategoryPage(string CategoryThree, string Edition, string ProductPage, string CategoryPage)
        {
            int CategoryThreeId = int.Parse(CategoryThree);
            int EditionId = int.Parse(Edition);

            if (string.IsNullOrEmpty(ProductPage))
            {
                ProductPage = null;
            }
            else
            {
                ProductPage = ProductPage.Trim();
            }

            if (string.IsNullOrEmpty(CategoryPage))
            {
                CategoryPage = null;
            }
            else
            {
                CategoryPage = CategoryPage.Trim();
            }

            List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + Edition + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (ec.LongCount() > 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Update + ", @EditionId=" + Edition + ", @CategoryThreeId=" + CategoryThreeId + ", @ProductPage='" + ProductPage + "', @CategoryPage='" + CategoryPage + "'");

                ActivityLog.EditionCategoryThree(EditionId, CategoryThreeId, ProductPage, CategoryPage, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}