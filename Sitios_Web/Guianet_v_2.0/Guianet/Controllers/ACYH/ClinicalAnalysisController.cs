using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guianet.Models;
using Guianet.Models.Sessions;

namespace Guianet.Controllers.ACYH
{
    public class ClinicalAnalysisController : Controller
    {

        GuiaEntities db = new GuiaEntities();
        CRUD CRUD = new CRUD();
        ActivityLog ActivityLog = new ActivityLog();
        GetData GetData = new GetData();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName)
        {
            SessionClinicalAnalisysSM ind = (SessionClinicalAnalisysSM)Session["SessionClinicalAnalisysSM"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANÁLISIS CLÍNICOS").ToList();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if (string.IsNullOrEmpty(Letter))
                {
                    Letter = "A";
                }

                SessionClinicalAnalisysSM SessionClinicalAnalisysSM = new SessionClinicalAnalisysSM(country, _clientid, edition, book, null, Letter);
                Session["SessionClinicalAnalisysSM"] = SessionClinicalAnalisysSM;

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "'").ToList();

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        Letter = ind.Letter;
                    }
                    else
                    {
                        Letter = "A";
                    }
                }

                SessionClinicalAnalisysSM SessionClinicalAnalisysSM = new SessionClinicalAnalisysSM(country, _clientid, edition, book, null, Letter);
                Session["SessionClinicalAnalisysSM"] = SessionClinicalAnalisysSM;

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "'").ToList();

                return View(LC);
            }

            else
            {

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public JsonResult ParticipantCA(string Client, string Edition, string Operation)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANÁLISIS CLÍNICOS").ToList();

            if (Operation == "Insert")
            {
                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() == 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);
                }
            }
            else
            {
                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() > 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Delete + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 4);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ParticipantCH(string Client, string Edition, string Operation)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "HOSPITALES").ToList();

            if (Operation == "Insert")
            {
                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() == 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);
                }
            }
            else
            {
                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() > 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Delete + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 4);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditClients(string Client, string CompanyName, string ShortName)
        {
            int ClientId = int.Parse(Client);

            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LC.LongCount() > 0)
            {
                foreach (Clients item in LC)
                {
                    item.CompanyName = CompanyName.Trim();

                    if (!string.IsNullOrEmpty(ShortName))
                    {
                        item.ShortName = ShortName.Trim();
                    }
                    else
                    {
                        item.ShortName = null;
                    }

                    db.SaveChanges();

                    ActivityLog.EditBranch(ClientId, CompanyName, ShortName, null, 2);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Address(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionClinicalAnalisysSM ind = (SessionClinicalAnalisysSM)Session["SessionClinicalAnalisysSM"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public JsonResult SaveChangedAddress(string AddressIdd, string Address, string Suburb, string City, string State, string ZipCode, string Mail, string Web, string Location, string StateI, string Latitud, string Longitud)
        {
            var AddressId = int.Parse(AddressIdd);
            int StateId = int.Parse(StateI);

            var _resultinsa = db.Database.ExecuteSqlCommand("plm_spCRUDAddresses @CRUDType=" + CRUD.Update +
                                                                         ", @Address='" + Address.Trim() + "'" +
                                                                         ", @ZipCode='" + ZipCode.Trim() + "'" +
                                                                         ", @City='" + City.Trim() + "'" +
                                                                         ", @Email='" + Mail.Trim() + "'" +
                                                                         ", @Web='" + Web.Trim() + "'" +
                                                                         ", @StateId='" + StateId + "'" +
                                                                         ", @Suburb='" + Suburb + "'" +
                                                                         ", @Location='" + Location.Trim() + "'" +
                                                                         ", @AddressId=" + AddressId + "" +
                                                                         ", @Latitud='" + Latitud + "'" +
                                                                         ", @Longitud='" + Longitud + "'");

            var _addr = db.Addresses.Where(x => x.AddressId == AddressId).ToList();

            ActivityLog._insertAddresses(AddressId, Address, Suburb, City, _addr[0].CountryId, Mail, Web, ZipCode, 2);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddAddress(string Address, string Suburb, string City, string StateI, string ZipCode, string Mail, string Web, string Country, string Client, string Location)
        {
            ClientAddresses ClientAddresses = new Models.ClientAddresses();

            int? StateId = int.Parse(StateI);
            int CountryId = int.Parse(Country);
            int ClientId = int.Parse(Client);

            var _resultinsa = db.Database.SqlQuery<int>("plm_spCRUDAddresses @CRUDType=" + CRUD.Create +
                                                                          ", @Address='" + Address.Trim() + "'" +
                                                                          ", @ZipCode='" + ZipCode.Trim() + "'" +
                                                                          ", @City='" + City.Trim() + "'" +
                                                                          ", @Email='" + Mail.Trim() + "'" +
                                                                          ", @Web='" + Web.Trim() + "'" +
                                                                          ", @CountryId=" + CountryId +
                                                                          ", @StateId='" + StateId + "'" +
                                                                          ", @Suburb='" + Suburb + "'" +
                                                                          ", @Location='" + Location.Trim() + "'").ToList();

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
            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public JsonResult EditPhones(string ClientPhone, string Lada, string Number, string Client)
        {
            SessionAddressIdCCA SessionAddressIdCCA = (SessionAddressIdCCA)Session["SessionAddressIdCCA"];

            int AddressId = Convert.ToInt32(SessionAddressIdCCA.AddressId);
            int ClientPhoneId = int.Parse(ClientPhone);
            int ClientId = int.Parse(Client);

            try
            {
                var cp = db.Database.SqlQuery<ClientPhones>("plm_spCRUDClientPhones @CRUDType=" + CRUD.Read + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @ClientPhoneId=" + ClientPhoneId + "").ToList();

                if (cp.LongCount() > 0)
                {
                    var _updatecp = db.Database.ExecuteSqlCommand("plm_spCRUDClientPhones @CRUDType=" + CRUD.Update + ", @ClientId=" + ClientId + ", @AddressId=" + AddressId + ", @ClientPhoneId=" + ClientPhoneId + ", @Lada='" + Lada.Trim() + "', @Number='" + Number.Trim() + "'");

                    ActivityLog._editphones(ClientPhoneId, ClientId, AddressId, cp[0].PhoneTypeId, Number.Trim(), Lada.Trim());

                }
            }
            catch (Exception e)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPhone(string Lada, string Number, string PhoneType, string Client)
        {
            SessionAddressIdCCA SessionAddressIdCCA = (SessionAddressIdCCA)Session["SessionAddressIdCCA"];

            int AddressId = Convert.ToInt32(SessionAddressIdCCA.AddressId);
            int ClientId = int.Parse(Client);
            int PhoneTypeId = int.Parse(PhoneType);

            try
            {
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


            }
            catch (Exception e)
            {

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePhones(string ClientPhone, string Client)
        {
            SessionAddressIdCCA SessionAddressIdCCA = (SessionAddressIdCCA)Session["SessionAddressIdCCA"];

            int AddressId = Convert.ToInt32(SessionAddressIdCCA.AddressId);
            int ClientPhoneId = int.Parse(ClientPhone);
            int ClientId = int.Parse(Client);

            try
            {
                var _resultCP = db.Database.SqlQuery<ClientPhones>("plm_spCRUDClientPhones @ClientId=" + ClientId + ", @AddressId=" + AddressId + ",@ClientPhoneId=" + ClientPhoneId + ", @CRUDType=" + CRUD.Read + "").ToList();
                if (_resultCP.LongCount() > 0)
                {
                    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDClientPhones @ClientId=" + ClientId + ", @AddressId=" + AddressId + ",@ClientPhoneId=" + ClientPhoneId + ", @CRUDType=" + CRUD.Delete + "");


                    ActivityLog._insertphone(ClientPhoneId, AddressId, ClientId, _resultCP[0].PhoneTypeId, _resultCP[0].Number.Trim(), _resultCP[0].Lada.Trim(), 4);
                }
            }
            catch (Exception e)
            {

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hospitals(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName)
        {
            SessionHospitalsSM ind = (SessionHospitalsSM)Session["SessionHospitalsSM"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "HOSPITALES").ToList();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if (string.IsNullOrEmpty(Letter))
                {
                    Letter = "A";
                }

                SessionHospitalsSM SessionHospitalsSM = new SessionHospitalsSM(country, _clientid, edition, book, null, Letter);
                Session["SessionHospitalsSM"] = SessionHospitalsSM;

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "'").ToList();

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        Letter = ind.Letter;
                    }
                    else
                    {
                        Letter = "A";
                    }
                }

                SessionHospitalsSM SessionHospitalsSM = new SessionHospitalsSM(country, _clientid, edition, book, null, Letter);
                Session["SessionHospitalsSM"] = SessionHospitalsSM;

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "'").ToList();

                return View(LC);
            }

            else
            {

                List<GetClients> LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult HospitalsAddress(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionHospitalsSM ind = (SessionHospitalsSM)Session["SessionHospitalsSM"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public ActionResult HospitalClientPhones(int? ClientId, int? AddressId, int? CountryId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }



        /**********************             PRODUCCIÓN          ************************************/

        public ActionResult IndexProd(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName)
        {
            SessionClinicalAnalisysProd ind = (SessionClinicalAnalisysProd)Session["SessionClinicalAnalisysProd"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANÁLISIS CLÍNICOS").ToList();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if (string.IsNullOrEmpty(Letter))
                {
                    Letter = "A";
                }

                SessionClinicalAnalisysProd SessionClinicalAnalisysProd = new SessionClinicalAnalisysProd(country, _clientid, edition, book, null, Letter);
                Session["SessionClinicalAnalisysProd"] = SessionClinicalAnalisysProd;

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        Letter = ind.Letter;
                    }
                    else
                    {
                        Letter = "A";
                    }
                }

                SessionClinicalAnalisysProd SessionClinicalAnalisysProd = new SessionClinicalAnalisysProd(country, _clientid, edition, book, null, Letter);
                Session["SessionClinicalAnalisysProd"] = SessionClinicalAnalisysProd;

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                return View(LC);
            }

            else
            {

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult AddressProd(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionClinicalAnalisysProd ind = (SessionClinicalAnalisysProd)Session["SessionClinicalAnalisysProd"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public ActionResult ClientPhonesProd(int? ClientId, int? AddressId, int? CountryId)
        {
            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public ActionResult HospitalsProd(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName)
        {
            SessionHospitalsProd ind = (SessionHospitalsProd)Session["SessionHospitalsProd"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "HOSPITALES").ToList();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if (string.IsNullOrEmpty(Letter))
                {
                    Letter = "A";
                }

                SessionHospitalsProd SessionHospitalsProd = new SessionHospitalsProd(country, _clientid, edition, book, null, Letter);
                Session["SessionHospitalsProd"] = SessionHospitalsProd;

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        Letter = ind.Letter;
                    }
                    else
                    {
                        Letter = "A";
                    }
                }

                SessionHospitalsProd SessionHospitalsProd = new SessionHospitalsProd(country, _clientid, edition, book, null, Letter);
                Session["SessionHospitalsProd"] = SessionHospitalsProd;

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                return View(LC);
            }

            else
            {

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult HospitalsAddressProd(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionHospitalsProd ind = (SessionHospitalsProd)Session["SessionHospitalsProd"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public ActionResult HospitalClientPhonesProd(int? ClientId, int? AddressId, int? CountryId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }



        /**********************             LI              ********************************/

        public ActionResult IndexLI(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName, string Type)
        {
            SessionClinicalAnalisysLI ind = (SessionClinicalAnalisysLI)Session["SessionClinicalAnalisysLI"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANÁLISIS CLÍNICOS").ToList();

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            string NewLetter = Letter;

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if ((string.IsNullOrEmpty(Letter)) || (Letter == "ALL") || (Letter == "NONE"))
                {
                    //Letter = "A";
                    Letter = null;
                }

                //if((Letter == "ALL")||(Letter == "NONE"))
                //{
                //    Letter = null;
                //}

                List<ClientsByEdition> LC = new List<ClientsByEdition>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, null, NewLetter);
                        Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                        LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                    }
                    else
                    {
                        SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, Convert.ToInt32(Type), NewLetter);
                        Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                        LC = GetData.GetAddressType(Type, country, ct[0].ClientTypeId, edition, Letter, CompanyName);
                    }
                }
                else
                {
                    SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, null, NewLetter);
                    Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                    LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                }

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if ((ind.PId != null) && (string.IsNullOrEmpty(Type)))
                {
                    Type = ind.PId.ToString();
                }

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        //Letter = ind.Letter;
                        Letter = ind.Letter;
                        NewLetter = ind.Letter;
                    }
                    else
                    {
                        //Letter = "A";
                        Letter = null;
                    }
                }

                if ((Letter == "ALL") || (Letter == "NONE"))
                {
                    Letter = null;
                }

                List<ClientsByEdition> LC = new List<ClientsByEdition>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, null, NewLetter);
                        Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                        LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                    }
                    else
                    {
                        SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, Convert.ToInt32(Type), NewLetter);
                        Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                        LC = GetData.GetAddressType(Type, country, ct[0].ClientTypeId, edition, Letter, CompanyName);
                    }
                }
                else
                {
                    SessionClinicalAnalisysLI SessionClinicalAnalisysLI = new SessionClinicalAnalisysLI(country, _clientid, edition, book, null, NewLetter);
                    Session["SessionClinicalAnalisysLI"] = SessionClinicalAnalisysLI;

                    LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                }

                return View(LC);
            }

            else
            {

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult AddressLI(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionClinicalAnalisysLI ind = (SessionClinicalAnalisysLI)Session["SessionClinicalAnalisysLI"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public ActionResult ClientPhonesLI(int? ClientId, int? AddressId, int? CountryId)
        {
            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public ActionResult HospitalsLI(string CountryId, string ClientId, string EditionId, string BookId, string Letter, string CompanyName, string Type)
        {
            SessionHospitalsLI ind = (SessionHospitalsLI)Session["SessionHospitalsLI"];

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "HOSPITALES").ToList();

            string NewLetter = Letter;

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int country = int.Parse(CountryId);
                int _clientid = 0;
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                if ((string.IsNullOrEmpty(Letter)) || (NewLetter == "ALL") || (NewLetter == "NONE"))
                {
                    Letter = null;
                }



                //List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                List<ClientsByEdition> LC = new List<ClientsByEdition>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, null, NewLetter);
                        Session["SessionHospitalsLI"] = SessionHospitalsLI;

                        LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                    }
                    else
                    {
                        SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, Convert.ToInt32(Type), NewLetter);
                        Session["SessionHospitalsLI"] = SessionHospitalsLI;

                        LC = GetData.GetAddressType(Type, country, ct[0].ClientTypeId, edition, Letter, CompanyName);
                    }
                }
                else
                {
                    SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, null, NewLetter);
                    Session["SessionHospitalsLI"] = SessionHospitalsLI;

                    LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                }

                return View(LC);
            }

            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                if (string.IsNullOrEmpty(Letter))
                {
                    if (!string.IsNullOrEmpty(ind.Letter))
                    {
                        Letter = ind.Letter;
                        NewLetter = ind.Letter;
                    }
                    else
                    {
                        Letter = null;
                    }
                }

                if ((ind.PId != null) && (string.IsNullOrEmpty(Type)))
                {
                    Type = ind.PId.ToString();
                }

                if ((Letter == "ALL") || (Letter == "NONE"))
                {
                    Letter = null;
                }

                //SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, null, Letter);
                //Session["SessionHospitalsLI"] = SessionHospitalsLI;

                //List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();

                List<ClientsByEdition> LC = new List<ClientsByEdition>();

                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type == "0")
                    {
                        SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, null, NewLetter);
                        Session["SessionHospitalsLI"] = SessionHospitalsLI;

                        LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                    }
                    else
                    {
                        SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, Convert.ToInt32(Type), NewLetter);
                        Session["SessionHospitalsLI"] = SessionHospitalsLI;

                        LC = GetData.GetAddressType(Type, country, ct[0].ClientTypeId, edition, Letter, CompanyName);
                    }
                }
                else
                {
                    SessionHospitalsLI SessionHospitalsLI = new SessionHospitalsLI(country, _clientid, edition, book, null, NewLetter);
                    Session["SessionHospitalsLI"] = SessionHospitalsLI;

                    LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + country + ", @ClientTypeId=" + ct[0].ClientTypeId + ", @EditionId=" + edition + ", @Letter='" + Letter + "', @CompanyName='" + CompanyName + "', @Type=P").ToList();
                }

                return View(LC);
            }

            else
            {

                List<ClientsByEdition> LC = db.Database.SqlQuery<ClientsByEdition>("plm_spGetClientsByType @CountryId=" + 0 + ", @ClientTypeId=" + 0 + "").ToList();

                return View(LC);
            }
        }

        public ActionResult HospitalsAddressLI(int? ClientId)
        {
            if ((ClientId == null) || (!Request.IsAuthenticated))
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionHospitalsLI ind = (SessionHospitalsLI)Session["SessionHospitalsLI"];

            ind.ClId = Convert.ToInt32(ClientId);

            List<ICAddress> LS = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + "").ToList();

            return View(LS);
        }

        public ActionResult HospitalClientPhonesLI(int? ClientId, int? AddressId, int? CountryId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddressIdCCA SessionAddressIdCCA = new Models.Sessions.SessionAddressIdCCA(AddressId, CountryId);
            Session["SessionAddressIdCCA"] = SessionAddressIdCCA;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }
    }
}