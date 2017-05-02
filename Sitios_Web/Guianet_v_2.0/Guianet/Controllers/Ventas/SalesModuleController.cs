using Guianet.Models;
using Guianet.Models.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;

namespace Guianet.Controllers.Ventas
{
    public class SalesModuleController : Controller
    {
        GuiaEntities db = new GuiaEntities();
        CRUD CRUD = new CRUD();
        GetData GetData = new GetData();
        ActivityLog ActivityLog = new ActivityLog();
        PLMUsers dbUsers = new PLMUsers();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string ProductName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            if (CountryId != null)
            {
                int count = 0;
                int country = int.Parse(CountryId);
                int _clientid = int.Parse(ClientId);
                int edition = int.Parse(EditionId);
                int book = int.Parse(BookId);

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetProductsByClient @clientId=" + _clientid + ", @EditionId=" + EditionId + ", @ProductName='" + ProductName + "'").ToList();

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
                    SearchProductNameSM SearchProductNameSM = new SearchProductNameSM(ProductName);
                    Session["SearchProductNameSM"] = SearchProductNameSM;
                }
                else
                {
                    Session["SearchProductNameSM"] = null;
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
                int book = index.BId;

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetProductsByClient @clientId=" + _clientid + ", @EditionId=" + edition + ", @ProductName='" + ProductName + "'").ToList();
                ViewData["Count"] = 0;

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameSM SearchProductNameSM = new SearchProductNameSM(ProductName);
                    Session["SearchProductNameSM"] = SearchProductNameSM;
                }
                else
                {
                    Session["SearchProductNameSM"] = null;
                }
                indexsalesmodule indexsalesmodule = new indexsalesmodule(country, _clientid, edition, book, null);
                Session["indexsalesmodule"] = indexsalesmodule;

                return View(plm);
            }
            else
            {

                var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetProductsByClient @clientId=" + 0 + ", @EditionId=" + 0 + "").ToList();
                ViewData["Count"] = 0;
                return View(plm);
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

                List<ClientProducts> LC = db.Database.SqlQuery<ClientProducts>("plm_spCRUDClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'").ToList();

                if (LC.LongCount() > 0)
                {
                    var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");

                    ActivityLog.createClientProducts(ProductId, ClientId, Register, 2);
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

                        var _result = db.Database.ExecuteSqlCommand("plm_spCRUDProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ProductName='" + ProductName + "'");

                        ActivityLog.editproduct(ProductName, ProductId, AlphabetId);

                        return Json("OKBC", JsonRequestBehavior.AllowGet);
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

            var _results = db.Database.ExecuteSqlCommand("plm_spCRUDProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ProductName='" + ProductName + "'");

            ActivityLog.editproduct(ProductName, ProductId, AlphabetId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /************************           ADD PRODUCTS           ***************************************/

        public JsonResult getManufactures(string Country)
        {
            int CountryId = int.Parse(Country);

            List<Manufacturers> LM = db.Database.SqlQuery<Manufacturers>("plm_spGetManufactures @CountryId=" + CountryId + "").ToList();

            return Json(LM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDistributors(string Country)
        {
            int CountryId = int.Parse(Country);

            List<Distributors> LD = db.Database.SqlQuery<Distributors>("plm_spGetDistributors @CountryId=" + CountryId + "").ToList();

            return Json(LD, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addProduct(string Product, string Register, string BarCode, string Client, string Manufacture, string Distributor, bool Printed, string Edition)
        {
            List<ClientProducts> LC = new List<ClientProducts>();
            int ProductId = 0;
            int ManufactureId = int.Parse(Manufacture);
            int DistributorId = int.Parse(Distributor);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            string Letter = Product.Substring(0, 1);
            int AlphabetId = GetData.GetAlphabetId(Letter);

            if (!string.IsNullOrEmpty(Register))
            {
                var cp = db.ClientProducts.Where(x => x.RegisterSanitary.ToUpper().Trim() == Register.ToUpper().Trim()).ToList();
                if (cp.LongCount() > 0)
                {
                    return Json("_RegisterSanitaryExist", JsonRequestBehavior.AllowGet);
                }
            }

            if (!string.IsNullOrEmpty(BarCode))
            {
                var bc = db.BarCodes.Where(x => x.BarCode.ToUpper().Trim() == BarCode.ToUpper().Trim()).ToList();
                if (bc.LongCount() > 0)
                {
                    return Json("_alreadyexistBarCode", JsonRequestBehavior.AllowGet);
                }
            }


            var _prodss = db.Database.SqlQuery<Products>("plm_spCRUDProducts @CRUDType=" + CRUD.Read + ", @ProductName='" + Product.Trim() + "'").ToList();

            if (_prodss.LongCount() == 0)
            {
                var _insprods = db.Database.SqlQuery<int>("plm_spCRUDProducts @CRUDType=" + CRUD.Create + ", @ProductName='" + Product.Trim() + "', @AlphabetId=" + AlphabetId + ", @ManufactureId=" + ManufactureId + "").ToList();

                ProductId = _insprods[0];

                ActivityLog.createProducts(ProductId, Product, AlphabetId, ManufactureId);
            }
            else
            {
                ProductId = _prodss[0].ProductId;
            }

            try
            {

                LC = db.Database.SqlQuery<ClientProducts>("plm_spCRUDClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'").ToList();

                if (LC.LongCount() == 0)
                {
                    var _insCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Create + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");

                    ActivityLog.createClientProducts(ProductId, ClientId, Register, 1);
                }
                else
                {
                    var cp = db.ClientProducts.Where(x => x.ProductId == ProductId).Select(x => x.RegisterSanitary).Distinct().ToList();

                    if (cp.LongCount() > 0)
                    {
                        Register = LC[0].RegisterSanitary;

                        var _resultInsertCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Update + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");

                        ActivityLog.createClientProducts(ProductId, ClientId, Register, 2);
                    }
                }
            }
            catch (SqlException e)
            {
                string message = e.Message;

                if (message == "_RegisterSanitaryExist")
                {
                    return Json("_RegisterSanitaryExist", JsonRequestBehavior.AllowGet);
                }

                if (message == "_NotExistClientProducts")
                {
                    var _insCP = db.Database.ExecuteSqlCommand("plm_spCRUDClientProducts @CRUDType=" + CRUD.Create + ", @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @RegisterSanitary='" + Register + "'");

                    ActivityLog.createClientProducts(ProductId, ClientId, Register, 1);
                }
            }

            if (!string.IsNullOrEmpty(BarCode))
            {
                try
                {
                    var _resultBC = db.Database.SqlQuery<BarCodes>("plm_spCRUDBarCodes @CRUDType=" + CRUD.Read + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

                    if (_resultBC.LongCount() == 0)
                    {
                        try
                        {
                            var _insertBC = db.Database.SqlQuery<int>("plm_spCRUDBarCodes @CRUDType=" + CRUD.Create + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

                            int BarCodeId = _insertBC[0];

                            ActivityLog.createBarCodes(BarCodeId, BarCode, ClientId, ProductId, 1);

                            ActivityLog.createClientProductBarCodes(BarCodeId, ClientId, ProductId, 1);
                        }
                        catch (Exception ex)
                        {
                            string _errormessage = ex.Message;

                            if (_errormessage == "_alreadyexistBarCode")
                            {
                                return Json("_alreadyexistBarCode", JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
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
                            var _resultBC = db.Database.SqlQuery<int>("plm_spCRUDBarCodes @CRUDType=" + CRUD.Update + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

                            int BarCodeId = _resultBC[0];

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
                            var _resultBC = db.Database.SqlQuery<int>("plm_spCRUDBarCodes @CRUDType=" + CRUD.Create + ", @BarCode='" + BarCode + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

                            int BarCodeId = _resultBC[0];

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

                    var _resultBC = db.ClientProductBarCodes.Where(x => x.ProductId == ProductId).Select(x => x.BarCodeId).Distinct().ToList();

                    int BarCodeId = _resultBC[0];

                    var cpbc = db.ClientProductBarCodes.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.BarCodeId == BarCodeId).ToList();

                    if (cpbc.LongCount() == 0)
                    {
                        var bc = db.BarCodes.Where(x => x.BarCodeId == BarCodeId).Select(x => x.BarCode).Distinct().ToList();

                        var _resultBCs = db.Database.ExecuteSqlCommand("plm_spCRUDBarCodes @CRUDType=" + CRUD.Create + ", @BarCode='" + bc[0] + "', @ClientId=" + ClientId + ", @ProductId=" + ProductId + "");
                    }
                }
                catch (Exception e)
                {

                }
            }


            if (DistributorId != 0)
            {
                var _dp = db.Database.SqlQuery<DistributorProducts>("plm_spCRUDDistributorProducts @CRUDType=" + CRUD.Read + ", @DistributorId=" + DistributorId + ", @ProductId=" + ProductId + "").ToList();

                if (_dp.LongCount() == 0)
                {

                    var _insdp = db.Database.ExecuteSqlCommand("plm_spCRUDDistributorProducts @CRUDType=" + CRUD.Create + ", @DistributorId=" + DistributorId + ", @ProductId=" + ProductId + "");

                    ActivityLog.createDistributorProducts(DistributorId, ProductId, 1);
                }
            }

            if (ManufactureId != 0)
            {
                var _prd = db.Products.Where(x => x.ProductId == ProductId).ToList();

                if (_prd.LongCount() > 0)
                {
                    foreach (Products _prds in _prd)
                    {
                        _prds.ManufacturerId = ManufactureId;

                        db.SaveChanges();

                        ActivityLog.UpdateManufacture(ProductId, ManufactureId);
                    }
                }
            }

            var _ecp = db.Database.SqlQuery<EditionClientProducts>("plm_spCRUDEditionClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "").ToList();

            if (_ecp.LongCount() == 0)
            {
                try
                {

                    var _insecp = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClientProducts @CRUDType=" + CRUD.Create + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "");

                    ActivityLog.createEditionClientProducts(ProductId, EditionId, ClientId, null, 1);

                }
                catch (Exception e)
                {

                }
            }


            var _pp = db.Database.SqlQuery<ParticipantProducts>("plm_spCRUDParticipantProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "").ToList();

            if (_pp.LongCount() == 0)
            {
                var _inspp = db.Database.ExecuteSqlCommand("plm_spCRUDParticipantProducts @CRUDType=" + CRUD.Create + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "");

                ActivityLog.createParticipantProducts(ProductId, EditionId, ClientId, 1);
            }



            if (Printed == true)
            {
                var ebcp = db.Database.SqlQuery<EditionBookClientProducts>("plm_spCRUDEditionBookClientProducts @CRUDType=" + CRUD.Read + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "").ToList();

                if (ebcp.LongCount() == 0)
                {
                    var _insebcp = db.Database.ExecuteSqlCommand("plm_spCRUDEditionBookClientProducts @CRUDType=" + CRUD.Create + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + "");

                    ActivityLog.createEditionBookClientProducts(ProductId, EditionId, ClientId);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /************************           PRODUCT CHANGES           ***************************************/

        public JsonResult insertProductChanges(string Product, string Client, string Edition, string Operation)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            var _st = db.Status.Where(x => x.StatusName == "Producto con Cambios").Select(x => x.StatusId).ToList();

            int StatusId = _st[0];

            if (Operation == "Insert")
            {
                var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts _ecp in ecp)
                    {
                        _ecp.StatusId = StatusId;

                        db.SaveChanges();

                        ActivityLog.createEditionClientProducts(ProductId, EditionId, ClientId, StatusId, 2);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts _ecp in ecp)
                    {
                        _ecp.StatusId = null;

                        db.SaveChanges();

                        ActivityLog.createEditionClientProducts(ProductId, EditionId, ClientId, null, 2);
                    }
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertProductWithoutChanges(string Product, string Client, string Edition, string Operation, string Country)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int CountryId = int.Parse(Country);

            var _st = db.Status.Where(x => x.StatusName == "Producto sin Cambios").Select(x => x.StatusId).ToList();

            int StatusId = _st[0];

            if (Operation == "Insert")
            {
                var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts _ecp in ecp)
                    {
                        _ecp.StatusId = StatusId;

                        db.SaveChanges();

                        ActivityLog.createEditionClientProducts(ProductId, EditionId, ClientId, StatusId, 2);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                var eds = db.Editions.Where(x => x.CountryId == CountryId && x.EditionId == EditionId - 1).Select(x => x.EditionId).ToList();

                try
                {
                    var trs = db.Database.ExecuteSqlCommand("plm_spTransInsertContentsByEditionByClientByProduct @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @EditionId=" + EditionId + ", @EditionIdAnt=" + eds[0] + "");
                }
                catch (Exception e)
                {

                }


            }
            else
            {
                var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts _ecp in ecp)
                    {
                        _ecp.StatusId = null;

                        db.SaveChanges();

                        ActivityLog.createEditionClientProducts(ProductId, EditionId, ClientId, null, 2);
                    }
                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /************************           SIDEF           ***************************************/

        public JsonResult inserproductSIDEF(string Product, string Edition, string Client)
        {
            int ProductId = int.Parse(Product);
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);


            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spInsertParticipantProducts @ClientId=" + ClientId + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @Type=SIDEF");

                var pp = db.ParticipantProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (pp.LongCount() > 0)
                {
                    ActivityLog.createParticipantProducts(ProductId, EditionId, ClientId, 1);
                }
                else
                {
                    ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 1, null);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteproductSIDEF(string Product, string Edition, string Client)
        {
            int ProductId = int.Parse(Product);
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);


            try
            {
                var ecp = db.EditionClientProductShots.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                if (ecp.LongCount() > 0)
                {
                    ActivityLog.createEditionClientProductShots(ProductId, EditionId, ClientId, 4);
                }
                else
                {
                    var ecps = db.EditionClientProductSIDEF.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

                    if (ecps.LongCount() > 0)
                    {
                        ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 4, null);
                    }
                }

                var _result = db.Database.ExecuteSqlCommand("plm_spDeleteParticipantProducts @ClientId=" + ClientId + ", @ProductId=" + ProductId + ", @EditionId=" + EditionId + ", @Type=SIDEF");

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }


        /************************           PARTICIPANTPRODUCTS           ***************************************/

        public JsonResult insertparticipantproducts(string Product, string Client, string Edition)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = p.ApplicationId;
            int UsersId = p.userId;

            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spInsertParticipantProducts @EditionId = " + EditionId + ", @ClientId = " + ClientId + ", @ProductId = " + ProductId + ", @Type=PARTICIPANT");

                ActivityLog.createParticipantProducts(ProductId, EditionId, ClientId, 1);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteparticipantproducts(string Product, string Client, string Edition)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spDeleteParticipantProducts @EditionId = " + EditionId + ", @ClientId = " + ClientId + ", @ProductId = " + ProductId + ", @Type=PARTICIPANT");

                ActivityLog.createParticipantProducts(ProductId, EditionId, ClientId, 4);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertprintedproducts(string Product, string Client, string Edition)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spInsertParticipantProducts @EditionId = " + EditionId + ", @ClientId = " + ClientId + ", @ProductId = " + ProductId + ", @Type=PRINTED");

                ActivityLog.createEditionBookClientProducts(ProductId, EditionId, ClientId, 1, null);

                ActivityLog.createParticipantProducts(ProductId, EditionId, ClientId, 1);

                ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 4, null);

                ActivityLog.createEditionClientProductShots(ProductId, EditionId, ClientId, 1);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteprinteddproducts(string Product, string Client, string Edition)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spDeleteParticipantProducts @EditionId = " + EditionId + ", @ClientId = " + ClientId + ", @ProductId = " + ProductId + ", @Type=PRINTED");

                ActivityLog.createEditionClientProductShots(ProductId, EditionId, ClientId, 4);

                ActivityLog.createEditionClientProductSIDEF(ProductId, EditionId, ClientId, 4, null);

                ActivityLog.createEditionBookClientProducts(ProductId, EditionId, ClientId, 4, null);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException ex)
            {
                string message = ex.Message;

                return Json(message, JsonRequestBehavior.AllowGet);
            }
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



        /************************           CLASIFICATION           ***************************************/

        public ActionResult ClasificationProducts(int? ProductId)
        {
            indexsalesmodule ind = (indexsalesmodule)Session["indexsalesmodule"];
            if ((!Request.IsAuthenticated) || (ind == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            int _clientid = ind.ClId;

            ind.PId = ProductId;

            var cc = db.Database.SqlQuery<HomogeneousCategoriesByClientByProduct>("plm_spGetProductCategoriesByClientByProduct  @ProductId =" + ProductId + ",  @ClientId= " + _clientid + "").ToList();

            return View(cc);
        }

        public JsonResult getlevel4(string HomogeneousCategory, string Client, string Product)
        {
            List<GetHomogeneousCategories> Cats = new List<GetHomogeneousCategories>();
            SearchCategorySM SearchCategory = (SearchCategorySM)Session["SearchCategorySM"];

            if (SearchCategory != null)
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);
                int ClientId = int.Parse(Client);
                int ProductId = int.Parse(Product);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategoryThree @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @TextSearch='" + SearchCategory.CategoryName + "', @ClientId=" + ClientId + ",@ProductId=" + ProductId + "").ToList();
            }
            else
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);
                int ClientId = int.Parse(Client);
                int ProductId = int.Parse(Product);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategoryThree @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @ClientId=" + ClientId + ",@ProductId=" + ProductId + "").ToList();
            }

            return Json(Cats, JsonRequestBehavior.AllowGet);
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

        public JsonResult saveCategories(String ListItems, string ArraySize, string Edition)
        {
            dynamic d = JsonConvert.DeserializeObject(ListItems);

            int Size = int.Parse(ArraySize);
            int EditionId = int.Parse(Edition);

            for (var i = 0; i <= Size - 1; i++)
            {
                var Id = Convert.ToInt32(d[i]["Id"]);
                var CId = Convert.ToInt32(d[i]["CId"]);
                var PId = Convert.ToInt32(d[i]["PId"]);
                var HCId = Convert.ToInt32(d[i]["HCId"]);

                string a = insertProductLeafCategories(CId, PId, Id, HCId);
                bool r = AddEditionCategoryThree(EditionId, HCId);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public string insertProductLeafCategories(int ClientId, int ProductId, int LeafCategoryId, int CategoryThreeId)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            ClientProductLeafCategories CPLC = new ClientProductLeafCategories();


            var _rc = db.ReassignCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId).ToList();

            if (_rc.LongCount() > 0)
            {
                var delete = db.ReassignCategories.SingleOrDefault(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId);

                db.ReassignCategories.Remove(delete);
                db.SaveChanges();

                var usr = dbUsers.Users.Where(x => x.UserId == UsersId).ToList();

                string NickName = usr[0].NickName;

                ActivityLog.ReassignCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 4, NickName);
            }


            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

            if (cp.LongCount() > 0)
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.CategoryThreeId == CategoryThreeId && x.LeafCategoryId == LeafCategoryId).ToList();

                if (cpl.LongCount() == 0)
                {
                    CPLC = new ClientProductLeafCategories();

                    CPLC.ClientId = ClientId;
                    CPLC.LeafCategoryId = LeafCategoryId;
                    CPLC.CategoryThreeId = CategoryThreeId;
                    CPLC.ProductId = ProductId;
                    CPLC.Module = "Ventas";

                    db.ClientProductLeafCategories.Add(CPLC);

                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 1);

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

        public bool AddEditionCategoryThree(int EditionId, int CategoryThreeId)
        {
            List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (ec.LongCount() == 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "");

                ActivityLog.EditionCategoryThree(EditionId, CategoryThreeId, null, null, 1);
            }

            return true;
        }

        public JsonResult deleteProductLeafCategories(string Client, string Product, string Category, string LeafCategory, string Edition)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);
            int CategoryThreeId = int.Parse(Category);
            int LeafCategoryId = int.Parse(LeafCategory);
            int EditionId = int.Parse(Edition);

            try
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId).ToList();

                if (cpl.LongCount() > 0)
                {
                    var delete = db.ClientProductLeafCategories.SingleOrDefault(x => x.ClientId == ClientId && x.ProductId == ProductId && x.CategoryThreeId == CategoryThreeId && x.LeafCategoryId == LeafCategoryId);

                    db.ClientProductLeafCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 4);
                }

                List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

                if (ec.LongCount() > 0)
                {

                    List<ClientProductLeafCategories> LS = db.ClientProductLeafCategories.Where(x => x.CategoryThreeId == CategoryThreeId).ToList();

                    if (LS.LongCount() == 0)
                    {
                        var result = db.Database.ExecuteSqlCommand("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Delete + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "");

                        ActivityLog.EditionCategoryThree(EditionId, CategoryThreeId, null, null, 4);
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string message = e.Message;

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }




        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANDS          ************************ */

        public ActionResult Brands(string CountryId, string ClientId, string EditionId, string BookId, string BrandName)
        {
            SessionBrandsSM ind = (SessionBrandsSM)Session["SessionBrandsSM"];

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

                SessionBrandsSM SessionBrandsSM = new SessionBrandsSM(country, _clientid, edition, book, null);
                Session["SessionBrandsSM"] = SessionBrandsSM;

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

                SessionBrandsSM SessionBrandsSM = new SessionBrandsSM(country, _clientid, edition, book, null);
                Session["SessionBrandsSM"] = SessionBrandsSM;

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

        public ActionResult insertclientbrands(int BrandId)
        {
            SessionBrandsSM ind = (SessionBrandsSM)Session["SessionBrandsSM"];
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

                if (ec.LongCount() == 0)
                {
                    EditionClients.ClientId = Client;
                    EditionClients.EditionId = Edition;
                    EditionClients.ClientTypeId = clienttypeid;
                    EditionClients.AddedDate = DateTime.Now;

                    db.EditionClients.Add(EditionClients);
                    db.SaveChanges();

                    ActivityLog.createEditionClients(Client, Edition, Convert.ToInt32(clienttypeid), null, 1);
                }

                var cb = (from _cb in db.ClientBrands
                          where _cb.EditionId == Edition
                          && _cb.ClientId == Client
                          && _cb.BrandId == BrandId
                          select _cb).ToList();
                if (cb.LongCount() == 0)
                {
                    ClientBrands.BrandId = BrandId;
                    ClientBrands.ClientId = Client;
                    ClientBrands.EditionId = Edition;
                    ClientBrands.ExpireDescription = null;
                    ClientBrands.Page = null;
                    ClientBrands.AddedDate = DateTime.Now;

                    db.ClientBrands.Add(ClientBrands);
                    db.SaveChanges();

                    int? ClientBrandTypeId = null;
                    int ClientTypeId = Convert.ToInt32(clienttypeid);

                    ActivityLog._insertClientBrands(BrandId, Client, ClientBrandTypeId, Edition, ClientTypeId, 1);
                }
                return RedirectToAction("Brands", "SalesModule");
            }
            else
            {
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult deleteclientbrands(int BrandId)
        {
            SessionBrandsSM ind = (SessionBrandsSM)Session["SessionBrandsSM"];
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
                        var delete = db.ClientBrands.SingleOrDefault(x => x.BrandId == _cb.BrandId && x.ClientId == _cb.ClientId && x.EditionId == _cb.EditionId);
                        db.ClientBrands.Remove(delete);
                        db.SaveChanges();

                        int ClientBrandTypeId = Convert.ToInt32(_cb.ClientBrandTypeId);

                        ActivityLog._insertClientBrands(BrandId, Client, ClientBrandTypeId, Edition, null, 4);
                    }
                }
            }
            return RedirectToAction("Brands", "SalesModule");
        }

        public JsonResult Distributors(string Brand, string Edition, string Client, string Operation)
        {
            int BrandId = int.Parse(Brand);
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);

            var cbt = db.ClientBrandTypes.Where(x => x.Description.ToUpper().Trim() == "Distribuidor autorizado".ToUpper().Trim()).Select(x => x.ClientBrandTypeId).ToList();

            byte ClientBrandTypeId = cbt[0];

            if (Operation == "INSERT")
            {
                var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        _cb.ClientBrandTypeId = ClientBrandTypeId;

                        db.SaveChanges();

                        ActivityLog._insertClientBrands(BrandId, ClientId, ClientBrandTypeId, EditionId, null, 2);
                    }
                }
            }
            else
            {
                var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        _cb.ClientBrandTypeId = null;

                        db.SaveChanges();

                        ActivityLog._insertClientBrands(BrandId, ClientId, null, EditionId, null, 2);
                    }
                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Representative(string Brand, string Edition, string Client, string Operation)
        {
            int BrandId = int.Parse(Brand);
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);

            var cbt = db.ClientBrandTypes.Where(x => x.Description.ToUpper().Trim() == "Representante exclusivo".ToUpper().Trim()).Select(x => x.ClientBrandTypeId).ToList();

            byte ClientBrandTypeId = cbt[0];

            if (Operation == "INSERT")
            {
                var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        _cb.ClientBrandTypeId = ClientBrandTypeId;

                        db.SaveChanges();

                        ActivityLog._insertClientBrands(BrandId, ClientId, ClientBrandTypeId, EditionId, null, 2);
                    }
                }
            }
            else
            {
                var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId && x.EditionId == EditionId).ToList();

                if (cb.LongCount() > 0)
                {
                    foreach (ClientBrands _cb in cb)
                    {
                        _cb.ClientBrandTypeId = null;

                        db.SaveChanges();

                        ActivityLog._insertClientBrands(BrandId, ClientId, null, EditionId, null, 2);
                    }
                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchBrand(string BrandName)
        {
            if (!string.IsNullOrEmpty(BrandName))
            {
                SearchBrandsSM SearchBrandsSM = new SearchBrandsSM(BrandName);
                Session["SearchBrandsSM"] = SearchBrandsSM;
            }
            else
            {
                Session["SearchBrandsSM"] = null;
            }
            return RedirectToAction("Brands", "SalesModule");
        }

        public JsonResult EditClientBrands(string Client, string Edition, string Brand, string Expire)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int BrandId = int.Parse(Brand);

            var cb = db.ClientBrands.Where(x => x.BrandId == BrandId && x.ClientId == ClientId && x.EditionId == EditionId).ToList();

            if (cb.LongCount() > 0)
            {
                foreach (ClientBrands _cb in cb)
                {
                    if (!string.IsNullOrEmpty(Expire))
                    {
                        _cb.ExpireDescription = Expire.Trim();
                    }
                    else
                    {
                        _cb.ExpireDescription = null;
                    }

                    db.SaveChanges();

                    ActivityLog.EditClientBrands(EditionId, ClientId, BrandId, Expire, 2, null);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BrandImages(int? BrandId, int? CountryId)
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


        /************************* ************************ ************************ ************************ ************************ */
        /************************               ADVERTS          ************************ */

        public ActionResult Adverts(string CountryId, string ClientId, string EditionId, string BookId, string CategoryName)
        {
            SessionSpecialitiesSM ind = (SessionSpecialitiesSM)Session["SessionSpecialitiesSM"];

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

                SessionSpecialitiesSM SessionSpecialitiesSM = new SessionSpecialitiesSM(country, _clientid, edition, book, null);
                Session["SessionSpecialitiesSM"] = SessionSpecialitiesSM;

                var _result = db.Database.SqlQuery<AdvertsByClient>("plm_spGetAdvertsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + ", @CategoryName='" + CategoryName + "'").ToList();


                return View(_result);

            }
            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionSpecialitiesSM SessionSpecialitiesSM = new SessionSpecialitiesSM(country, _clientid, edition, book, null);
                Session["SessionSpecialitiesSM"] = SessionSpecialitiesSM;

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

        public JsonResult AddAdverts(string Edition, string Client, string AdvertName, string Description)
        {
            int EditionId = int.Parse(Edition);
            int ClientId = int.Parse(Client);
            int AdvertId = 0;

            try
            {
                var ct = db.ClientTypes.Where(x => x.TypeName == "ANUNCIANTE").ToList();

                var _ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

                if (_ec.LongCount() == 0)
                {
                    var _reultec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ClientTypeId=" + ct[0].ClientTypeId + "");

                    ActivityLog.createEditionClients(ClientId, EditionId, Convert.ToInt32(ct[0].ClientTypeId), null, 1);
                }

                var _result = db.Database.SqlQuery<Adverts>("plm_spCRUDAdverts @CRUDType=" + CRUD.Read + ", @AdvertDescription='" + Description + "', @AdvertName='" + AdvertName + "'").ToList();

                if (_result.LongCount() == 0)
                {
                    var ad = db.Adverts.Where(x => x.AdvertName.ToUpper().Trim() == AdvertName.ToUpper().Trim()).ToList();

                    if (ad.LongCount() > 0)
                    {
                        return Json("_existAdvertName", JsonRequestBehavior.AllowGet);
                    }

                    var _resultins = db.Database.SqlQuery<int>("plm_spCRUDAdverts @CRUDType=" + CRUD.Create + ", @AdvertDescription='" + Description + "', @AdvertName='" + AdvertName + "'").ToList();

                    AdvertId = _resultins[0];

                    ActivityLog._Adverts(AdvertId, AdvertName, Description, null, 1);

                }
                else
                {
                    var ad = db.Adverts.Where(x => x.AdvertName.ToUpper().Trim() == AdvertName.ToUpper().Trim()).ToList();

                    if (ad.LongCount() > 0)
                    {
                        return Json("_existAdvertName", JsonRequestBehavior.AllowGet);
                    }

                    AdvertId = _result[0].AdvertId;
                }

                var ca = db.ClientAdverts.Where(x => x.AdvertId == AdvertId && x.ClientId == ClientId).ToList();

                if (ca.LongCount() == 0)
                {
                    ClientAdverts ClientAdverts = new ClientAdverts();

                    ClientAdverts.AdvertId = AdvertId;
                    ClientAdverts.ClientId = ClientId;

                    db.ClientAdverts.Add(ClientAdverts);
                    db.SaveChanges();

                    ActivityLog.createClientAdverts(ClientId, AdvertId, 1);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (SqlException e)
            {
                string message = e.Message;

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult AdvertsByClient(int? AdvertId, int? ClientId, int? EditionId, string CategoryName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddAdverts indS = (SessionAddAdverts)Session["SessionAddAdverts"];
            if ((AdvertId != null) && (ClientId != null) && (EditionId != null))
            {
                SessionSpecialitiesSM ind = (SessionSpecialitiesSM)Session["SessionSpecialitiesSM"];

                ind.PId = AdvertId;

                var _result = db.Database.SqlQuery<CategoriesByAdvert>("plm_spGetCategoriesByEditionByClientByAdvert @ClientId=" + ClientId + ", @EditionId=" + EditionId + ", @AdvertId='" + AdvertId + "', @CategoryName='" + CategoryName + "'").ToList();

                SessionAddAdverts SessionAddAdverts = new SessionAddAdverts(ClientId, EditionId, AdvertId);
                Session["SessionAddAdverts"] = SessionAddAdverts;

                if (!string.IsNullOrEmpty(CategoryName))
                {
                    SearchAdvertsAsocSM SearchAdvertsAsocSM = new SearchAdvertsAsocSM(CategoryName);
                    Session["SearchAdvertsAsocSM"] = SearchAdvertsAsocSM;
                }
                else
                {
                    Session["SearchAdvertsAsocSM"] = null;
                }

                return View(_result);
            }
            if (indS != null)
            {
                SessionSpecialitiesSM ind = (SessionSpecialitiesSM)Session["SessionSpecialitiesSM"];

                ind.PId = indS.AId;

                var _result = db.Database.SqlQuery<CategoriesByAdvert>("plm_spGetCategoriesByEditionByClientByAdvert @ClientId=" + indS.CId + ", @EditionId=" + indS.EId + ", @AdvertId='" + indS.AId + "', @CategoryName='" + CategoryName + "'").ToList();

                if (!string.IsNullOrEmpty(CategoryName))
                {
                    SearchAdvertsAsocSM SearchAdvertsAsocSM = new SearchAdvertsAsocSM(CategoryName);
                    Session["SearchAdvertsAsocSM"] = SearchAdvertsAsocSM;
                }
                else
                {
                    Session["SearchAdvertsAsocSM"] = null;
                }

                return View(_result);
            }
            else
            {
                var _result = db.Database.SqlQuery<CategoriesByAdvert>("plm_spGetCategoriesByEditionByClientByAdvert @ClientId=" + 0 + ", @EditionId=" + 0 + ", @AdvertId='" + 0 + "'").ToList();

                return View(_result);

            }



        }


        public JsonResult AddCategoriesToAdverts(string CategoryThree, string Client, string Edition, string AdvertType, string Advert)
        {
            int CategoryThreeId = int.Parse(CategoryThree);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int AdvertTypeId = int.Parse(AdvertType);
            int AdvertId = int.Parse(Advert);

            var _result = db.Database.SqlQuery<ClientAdvertCategories>("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Read + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (_result.LongCount() == 0)
            {
                var _resultins = db.Database.ExecuteSqlCommand("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Create + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "");

                ActivityLog._ClientAdvertCategories(ClientId, AdvertId, EditionId, CategoryThreeId, AdvertTypeId, null, 1);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertPPCategoriesToAdverts(string CategoryThree, string Client, string Edition, string AdvertType, string Advert)
        {
            int CategoryThreeId = int.Parse(CategoryThree);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int AdvertTypeId = int.Parse(AdvertType);
            int AdvertId = int.Parse(Advert);

            try
            {
                var _result = db.Database.SqlQuery<ClientAdvertCategories>("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Read + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

                if (_result.LongCount() == 0)
                {
                    var _resultins = db.Database.ExecuteSqlCommand("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Create + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "");

                    ActivityLog._ClientAdvertCategories(ClientId, AdvertId, EditionId, CategoryThreeId, AdvertTypeId, null, 1);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult DeletePPAdverts(string CategoryThree, string Client, string Edition, string AdvertType, string Advert)
        {
            int CategoryThreeId = int.Parse(CategoryThree);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int AdvertTypeId = int.Parse(AdvertType);
            int AdvertId = int.Parse(Advert);

            try
            {
                var _result = db.Database.SqlQuery<ClientAdvertCategories>("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Read + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

                if (_result.LongCount() > 0)
                {
                    var _resultins = db.Database.ExecuteSqlCommand("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Delete + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "");

                    ActivityLog._ClientAdvertCategories(ClientId, AdvertId, EditionId, CategoryThreeId, AdvertTypeId, null, 4);
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult DeleteAdverts(string CategoryThree, string Client, string Edition, string AdvertType, string Advert)
        {
            int CategoryThreeId = int.Parse(CategoryThree);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            int AdvertTypeId = int.Parse(AdvertType);
            int AdvertId = int.Parse(Advert);

            var _result = db.Database.SqlQuery<ClientAdvertCategories>("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Read + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (_result.LongCount() > 0)
            {
                var _resultins = db.Database.ExecuteSqlCommand("plm_spCRUDClientAdvertCategories @CRUDType=" + CRUD.Delete + ", @AdvertId=" + AdvertId + ", @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertTypeId=" + AdvertTypeId + ", @CategoryThreeId=" + CategoryThreeId + "");

                ActivityLog._ClientAdvertCategories(ClientId, AdvertId, EditionId, CategoryThreeId, AdvertTypeId, null, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchAdverts(string CategoryName)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                SearchAdvertsSM SearchAdvertsSM = new SearchAdvertsSM(CategoryName);
                Session["SearchAdvertsSM"] = SearchAdvertsSM;
            }
            else
            {
                Session["SearchAdvertsSM"] = null;
            }

            return RedirectToAction("AdvertsByClient", "SalesModule");
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
                var _resultEdit = db.Database.ExecuteSqlCommand("plm_spCRUDAdverts @AdvertId=" + AdvertId + ", @CRUDType=" + CRUD.Update + ", @AdvertName='" + Name + "', @AdvertDescription='" + Description + "'");

                ActivityLog._Adverts(AdvertId, Name, Description, null, 2);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addAdvertImage(HttpPostedFileBase file, string Advert)
        {
            int AdvertId = int.Parse(Advert);
            String ImageName = file.FileName;
            String Ext = Path.GetExtension(file.FileName);

            ImageName = ImageName.Replace(Ext, "");

            ImageName = ClearString(ImageName);

            ImageName = ImageName + Ext;

            var root = Path.Combine(Server.MapPath("~/App_Data/AdvertImages"), ImageName);

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
            _string = _string.Replace(" ", "_");
            _string = _string.Replace(",", "");

            return _string;
        }

        public ActionResult AdvertsImages(int AdvertId, int CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathPA = System.Configuration.ConfigurationManager.AppSettings["Path"];

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            var ad = db.Adverts.Where(x => x.AdvertId == AdvertId).ToList();

            if (ad.LongCount() > 0)
            {
                string AdvertFile = ad[0].AdvertFile;

                if (!string.IsNullOrEmpty(AdvertFile))
                {
                    var root = Path.Combine(PathP, CountryName, "advertimages", AdvertFile);
                    if (System.IO.File.Exists(root))
                    {
                        return File(root, "image/png");
                    }
                    else
                    {
                        AdvertFile = "not_available.png";
                        root = Path.Combine(PathPA, "Uploads", AdvertFile);
                        return File(root, "image/png");
                    }
                }
                else
                {
                    AdvertFile = "not_available.png";
                    var root = Path.Combine(PathPA, "Uploads", AdvertFile);
                    return File(root, "image/png");
                }
            }
            else
            {
                string AdvertFile = "not_available.png";
                var root = Path.Combine(PathPA, "Uploads", AdvertFile);
                return File(root, "image/png");
            }
        }

        /************************* ************************ ************************ ************************ ************************ */
        /************************               BRANCH          ************************ */

        public ActionResult Branchs(string CountryId, string ClientId, string EditionId, string BookId)
        {
            SessionBranchSM ind = (SessionBranchSM)Session["SessionBranchSM"];

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

                SessionBranchSM SessionBranchSM = new SessionBranchSM(country, _clientid, edition, book, null);
                Session["SessionBranchSM"] = SessionBranchSM;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + "").ToList();

                return View(_result);

            }
            if (ind != null)
            {
                int country = ind.CId;
                int _clientid = ind.ClId;
                int edition = ind.EId;
                int book = ind.BId;

                SessionBranchSM SessionBranchSM = new SessionBranchSM(country, _clientid, edition, book, null);
                Session["SessionBranchSM"] = SessionBranchSM;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + "").ToList();

                return View(_result);
            }
            else
            {
                int _clientid = 0;
                int edition = 0;

                var _result = db.Database.SqlQuery<BranchsByClients>("plm_spGetBranchsByClient @ClientId=" + _clientid + ", @EditionId=" + edition + "").ToList();

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

        public JsonResult InsertParticipantClient(string Client, string Edition)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            try
            {
                var ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (ec.LongCount() == 0)
                {
                    var _resultinsec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Create + "");

                    var pt = db.ClientTypes.Where(x => x.TypeName == "SUCURSAL").Select(x => x.ClientTypeId).ToList();

                    ActivityLog.createEditionClients(ClientId, EditionId, pt[0], null, 1);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteParticipantClients(string Client, string Edition)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            try
            {
                var ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (ec.LongCount() > 0)
                {
                    var _resultinsec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Delete + "");

                    var pt = db.ClientTypes.Where(x => x.TypeName == "SUCURSAL").Select(x => x.ClientTypeId).ToList();

                    ActivityLog.createEditionClients(ClientId, EditionId, pt[0], null, 4);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AddBranchs(string Client, string CompanyName, string ShortName, string Country, string Edition)
        {
            int ClientId = int.Parse(Client);
            int CountryId = int.Parse(Country);
            int EditionId = int.Parse(Edition);
            string Letter = CompanyName.Substring(0, 1);

            int AlphabetId = GetData.GetAlphabetId(Letter);

            try
            {

                var c = db.Database.SqlQuery<Clients>("plm_spCRUDClients @CompanyName='" + CompanyName + "', @ShortName='" + ShortName + "', @CRUDType=" + CRUD.Read + ", @CountryId=" + CountryId + "").ToList();

                if (c.LongCount() == 0)
                {
                    var _resultinsc = db.Database.SqlQuery<int>("plm_spCRUDClients @CompanyName='" + CompanyName + "', @ShortName='" + ShortName + "', @CRUDType=" + CRUD.Create + ", @CountryId=" + CountryId + ", @AlphabetId=" + AlphabetId + ", @ClientId=" + ClientId + ", @Code=''").ToList();

                    ClientId = _resultinsc[0];

                    ActivityLog.CreateBranch(ClientId, CompanyName, ShortName, null, CountryId, AlphabetId, 1);

                    var ec = db.Database.SqlQuery<EditionClients>("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Read + "").ToList();

                    if (ec.LongCount() == 0)
                    {
                        var _resultinsec = db.Database.ExecuteSqlCommand("plm_spCRUDEditionClients @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @CRUDType=" + CRUD.Create + "");

                        var pt = db.ClientTypes.Where(x => x.TypeName == "SUCURSAL").Select(x => x.ClientTypeId).ToList();

                        ActivityLog.createEditionClients(ClientId, EditionId, pt[0], null, 1);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("_existsClient", JsonRequestBehavior.AllowGet);
                }


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

            SessionBranchSM ind = (SessionBranchSM)Session["SessionBranchSM"];

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
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            SessionAddressId SessionAddressId = new Models.Sessions.SessionAddressId(AddressId, CountryId);
            Session["SessionAddressId"] = SessionAddressId;

            var cp = db.Database.SqlQuery<GetClientPhones>("plm_spGetPhonesByClientByAddress @ClientId=" + ClientId + ", @AddressId=" + AddressId + "").ToList();

            return View(cp);
        }

        public JsonResult EditPhones(string ClientPhone, string Lada, string Number, string Client)
        {
            SessionAddressId SessionAddressId = (SessionAddressId)Session["SessionAddressId"];

            int AddressId = Convert.ToInt32(SessionAddressId.AddressId);
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
            SessionAddressId SessionAddressId = (SessionAddressId)Session["SessionAddressId"];

            int AddressId = Convert.ToInt32(SessionAddressId.AddressId);
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
            SessionAddressId SessionAddressId = (SessionAddressId)Session["SessionAddressId"];

            int AddressId = Convert.ToInt32(SessionAddressId.AddressId);
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

        public JsonResult SavePId(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            SessionProductId SessionProductId = new SessionProductId(ClientId, ProductId);
            Session["SessionProductId"] = SessionProductId;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<GerReassingCategories> result = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeletedCategories(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<GerReassingCategories> result = db.Database.SqlQuery<GerReassingCategories>("plm_spGetClientProductLeafCategoriesByClientByProduct @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditClientData(string Client, string RS, string SN)
        {
            int ClientId = int.Parse(Client);

            var cl = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (cl.LongCount() > 0)
            {
                foreach (Clients _cl in cl)
                {
                    _cl.CompanyName = RS.Trim();
                    _cl.ShortName = SN.Trim();

                    db.SaveChanges();

                    ActivityLog.EditBranch(ClientId, RS, SN, null, 2);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetManufacturersandDistributorts(string Product, string Country)
        {

            int ProductId = int.Parse(Product);
            int CountryId = int.Parse(Country);
            string _Manufacturer = string.Empty;
            string _Distributor = string.Empty;

            int ManufacturerId = 0;
            int DistributorId = 0;

            var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (p.LongCount() > 0)
            {
                ManufacturerId = Convert.ToInt32(p[0].ManufacturerId);

                if (ManufacturerId != 0)
                {
                    var m = db.Manufacturers.Where(x => x.ManufacturerId == ManufacturerId).Select(x => x.Manufacturer).ToList();

                    _Manufacturer = m[0];
                }
            }

            var dp = db.DistributorProducts.Where(x => x.ProductId == ProductId).ToList();

            if (dp.LongCount() > 0)
            {
                DistributorId = Convert.ToInt32(dp[0].DistributorId);

                if (DistributorId != 0)
                {
                    var d = db.Distributors.Where(x => x.DistributorId == DistributorId).Select(x => x.Distributor).ToList();

                    _Distributor = d[0];
                }
            }

            var result = new { Manufacturer = _Manufacturer, ManufacturerId = ManufacturerId, Distributor = _Distributor, DistributorId = DistributorId };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveManufacturers(string Product, string Manufacturer, string Distributor)
        {
            int ProductId = int.Parse(Product);
            int ManufacturerId = int.Parse(Manufacturer);
            int DistributorId = int.Parse(Distributor);


            var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (p.LongCount() > 0)
            {
                foreach (Products _p in p)
                {
                    if (ManufacturerId != 0)
                    {
                        _p.ManufacturerId = ManufacturerId;

                        db.SaveChanges();

                        ActivityLog.UpdateManufacture(ProductId, ManufacturerId);
                    }
                }
            }

            if (DistributorId != 0)
            {
                var dp = db.DistributorProducts.Where(x => x.ProductId == ProductId && x.DistributorId == DistributorId).ToList();

                if (dp.LongCount() == 0)
                {
                    DistributorProducts DistributorProducts = new DistributorProducts();

                    DistributorProducts.DistributorId = DistributorId;
                    DistributorProducts.ProductId = ProductId;

                    db.DistributorProducts.Add(DistributorProducts);
                    db.SaveChanges();

                    ActivityLog.createDistributorProducts(DistributorId, ProductId, 1);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteManufacturer(string Product)
        {
            int ProductId = int.Parse(Product);

            var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

            foreach (Products _p in p)
            {
                _p.ManufacturerId = null;

                db.SaveChanges();

                ActivityLog.UpdateManufacture(ProductId, null);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDistributor(string Product, string Distributor)
        {
            int ProductId = int.Parse(Product);
            int DistributorId = int.Parse(Distributor);

            var dp = db.DistributorProducts.Where(x => x.ProductId == ProductId && x.DistributorId == DistributorId).ToList();

            if (dp.LongCount() > 0)
            {
                var delete = db.DistributorProducts.SingleOrDefault(x => x.ProductId == ProductId && x.DistributorId == DistributorId);
                db.DistributorProducts.Remove(delete);
                db.SaveChanges();

                ActivityLog.createDistributorProducts(DistributorId, ProductId, 4);
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

            SessionSMSP ind = (SessionSMSP)Session["SessionSMSP"];

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
                        SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionSMSP"] = SessionSMSP;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
                    }
                    else
                    {
                        SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionSMSP"] = SessionSMSP;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, null);
                    Session["SessionSMSP"] = SessionSMSP;

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
                        SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, null);
                        Session["SessionSMSP"] = SessionSMSP;

                        LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + country + ", @ClientTypeId=" + _ClientTypeId + ", @CompanyName='" + CompanyName + "'").ToList();
                    }
                    else
                    {
                        SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                        Session["SessionSMSP"] = SessionSMSP;

                        LC = GetData.GetClientTypes(Type, country, _ClientTypeId, CompanyName, edition);
                    }
                }
                else
                {
                    SessionSMSP SessionSMSP = new SessionSMSP(country, 1, edition, book, _ClientTypeId, Convert.ToInt32(Type));
                    Session["SessionSMSP"] = SessionSMSP;

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

        [HttpPost]
        public ActionResult AddPDFFile(HttpPostedFileBase file, string Client, string Edition, string Order, string Description, string ClientType, string Country)
        {
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);
            String ProductAdvertName = file.FileName;
            String Ext = Path.GetExtension(file.FileName);
            int OrderId = int.Parse(Order);
            byte ClientTypeId = Convert.ToByte(ClientType);
            int CountryId = int.Parse(Country);

            string CompanyName = string.Empty;

            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LC.LongCount() > 0)
            {
                if (!string.IsNullOrEmpty(LC[0].ShortName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else if (!string.IsNullOrEmpty(LC[0].CompanyName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else
                {
                    CompanyName = "Nueva Carpeta";
                }
            }
            else
            {
                CompanyName = "Nueva Carpeta";
            }


            ProductAdvertName = ProductAdvertName.Replace(Ext, "");

            ProductAdvertName = ClearString(ProductAdvertName);

            CompanyName = ClearString(CompanyName).ToLower();

            ProductAdvertName = ProductAdvertName + Ext;

            String path = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            path = Path.Combine(path, CountryName, "advertproductimages", CompanyName);

            if (System.IO.Directory.Exists(path))
            {
                var root = Path.Combine(path, ProductAdvertName);

                file.SaveAs(root);
            }
            else
            {
                Directory.CreateDirectory(path);

                var root = Path.Combine(path, ProductAdvertName);

                file.SaveAs(root);
            }


            var ec = (from edc in db.EditionClients
                      where edc.ClientId == ClientId
                        && edc.EditionId == EditionId
                      select edc).ToList();

            if (ec.LongCount() == 0)
            {
                EditionClients EditionClients = new Models.EditionClients();

                EditionClients.ClientId = ClientId;
                EditionClients.EditionId = EditionId;
                EditionClients.ClientTypeId = ClientTypeId;
                EditionClients.AddedDate = DateTime.Now;

                db.EditionClients.Add(EditionClients);
                db.SaveChanges();

                ActivityLog.createEditionClients(ClientId, EditionId, ClientTypeId, null, 1);
            }

            int ProductAdvertId = 0;

            List<ProductAdverts> LP = db.Database.SqlQuery<ProductAdverts>("plm_spCRUDProductAdverts @CRUDType=" + CRUD.Read + ", @ProductAdvertName='" + ProductAdvertName + "'").ToList();

            if (LP.LongCount() == 0)
            {
                var result = db.Database.SqlQuery<int>("plm_spCRUDProductAdverts @CRUDType=" + CRUD.Create + ", @ProductAdvertName='" + ProductAdvertName + "', @Description='" + Description + "', @Order=" + OrderId + "").ToList();

                ProductAdvertId = result[0];

                ActivityLog.ProductAdverts(ProductAdvertId, ProductAdvertName, Description, OrderId, 1);
            }
            else
            {
                ProductAdvertId = LP[0].ProductAdvertId;
            }

            List<ClientProductAdverts> LCPA = db.ClientProductAdverts.Where(x => x.ClientId == ClientId && x.ProductAdvertId == ProductAdvertId).ToList();

            if (LCPA.LongCount() == 0)
            {
                ClientProductAdverts ClientProductAdverts = new ClientProductAdverts();

                ClientProductAdverts.ClientId = ClientId;
                ClientProductAdverts.ProductAdvertId = ProductAdvertId;

                db.ClientProductAdverts.Add(ClientProductAdverts);
                db.SaveChanges();

                ActivityLog.ClientProductAdverts(ClientId, ProductAdvertId, 1);
            }

            List<EditionClientProductAdverts> LECPA = db.EditionClientProductAdverts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductAdvertId == ProductAdvertId).ToList();

            if (LECPA.LongCount() == 0)
            {
                EditionClientProductAdverts EditionClientProductAdverts = new EditionClientProductAdverts();

                EditionClientProductAdverts.ClientId = ClientId;
                EditionClientProductAdverts.EditionId = EditionId;
                EditionClientProductAdverts.ProductAdvertId = ProductAdvertId;

                db.EditionClientProductAdverts.Add(EditionClientProductAdverts);
                db.SaveChanges();

                ActivityLog.EditionClientProductAdverts(ClientId, ProductAdvertId, EditionId, 1);
            }



            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductAdverts(int? ClientId)
        {
            SessionSMSP ind = (SessionSMSP)Session["SessionSMSP"];

            if ((!Request.IsAuthenticated) || (ClientId == null) || (ind == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            ind.ClId = Convert.ToInt32(ClientId);
            int EditionId = ind.EId;

            List<ProductAdverts> LPA = db.Database.SqlQuery<ProductAdverts>("plm_spGetProductAdverts @ClientId=" + ClientId + ",@EditionId=" + EditionId + "").ToList();

            return View(LPA);
        }

        public ActionResult ProductAdvertsImages(string ProductAdvertName, int? ClientId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathPA = System.Configuration.ConfigurationManager.AppSettings["Path"];

            string CompanyName = string.Empty;

            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LC.LongCount() > 0)
            {
                if (!string.IsNullOrEmpty(LC[0].ShortName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else if (!string.IsNullOrEmpty(LC[0].CompanyName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else
                {
                    CompanyName = "Nueva Carpeta";
                }
            }
            else
            {
                CompanyName = "Nueva Carpeta";
            }

            CompanyName = ClearString(CompanyName).ToLower();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            if (!string.IsNullOrEmpty(ProductAdvertName))
            {
                var root = Path.Combine(PathP, CountryName, "advertproductimages", CompanyName, ProductAdvertName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "image/png");
                }
                else
                {
                    ProductAdvertName = "not_available.png";
                    root = Path.Combine(PathPA, "Uploads", ProductAdvertName);
                    return File(root, "image/png");
                }
            }
            else
            {
                ProductAdvertName = "not_available.png";
                var root = Path.Combine(PathPA, "Uploads", ProductAdvertName);
                return File(root, "image/png");
            }
        }

        public ActionResult GetPDFS(string ProductAdvertName, int? ClientId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            String PathPA = System.Configuration.ConfigurationManager.AppSettings["Path"];

            string CompanyName = string.Empty;

            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LC.LongCount() > 0)
            {
                if (!string.IsNullOrEmpty(LC[0].ShortName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else if (!string.IsNullOrEmpty(LC[0].CompanyName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else
                {
                    CompanyName = "Nueva Carpeta";
                }
            }
            else
            {
                CompanyName = "Nueva Carpeta";
            }

            CompanyName = ClearString(CompanyName).ToLower();

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            if (!string.IsNullOrEmpty(ProductAdvertName))
            {
                var root = Path.Combine(PathP, CountryName, "advertproductimages", CompanyName, ProductAdvertName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "application/pdf");
                }
                else
                {
                    ProductAdvertName = "not_available.png";
                    root = Path.Combine(PathPA, "Uploads", ProductAdvertName);
                    return File(root, "image/png");
                }
            }
            else
            {
                ProductAdvertName = "not_available.png";
                var root = Path.Combine(PathPA, "Uploads", ProductAdvertName);
                return File(root, "image/png");
            }
        }

        public FileResult Download(string ProductAdvertName, string Client, string Country)
        {
            int ClientId = int.Parse(Client);
            int CountryId = int.Parse(Country);

            string CompanyName = string.Empty;

            List<Countries> LCC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            String CountryName = ReplaceCountryName(LCC[0].CountryName);

            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LC.LongCount() > 0)
            {
                if (!string.IsNullOrEmpty(LC[0].ShortName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else if (!string.IsNullOrEmpty(LC[0].CompanyName))
                {
                    CompanyName = LC[0].CompanyName.Trim();
                }
                else
                {
                    CompanyName = "Nueva Carpeta";
                }
            }
            else
            {
                CompanyName = "Nueva Carpeta";
            }

            CompanyName = ClearString(CompanyName).ToLower();

            String PathPA = System.Configuration.ConfigurationManager.AppSettings["PathPS"];

            var root = Path.Combine(PathPA, CountryName, "advertproductimages", CompanyName, ProductAdvertName);

            if (System.IO.File.Exists(root))
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + ProductAdvertName + "\"");
                Response.WriteFile(root);
                Response.End();
                return null;
            }
            else
            {
                String Paths = System.Configuration.ConfigurationManager.AppSettings["Path"];

                ProductAdvertName = "not_available.png";
                root = Path.Combine(Paths, "Uploads", ProductAdvertName);

                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + "not_available.png" + "\"");
                Response.WriteFile(root);
                Response.End();
                return null;
            }

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

        public ActionResult GetClientImages(int? ClientId, int? CountryId)
        {
            String PathP = System.Configuration.ConfigurationManager.AppSettings["PathPS"].ToString();
            //String PathE = System.Configuration.ConfigurationManager.AppSettings["Path"].ToString();

            List<Clients> LCC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            if (LCC.LongCount() > 0)
            {
                List<Countries> LC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

                String CountryName = ReplaceCountryName(LC[0].CountryName);

                List<ImageSizes> ims = db.ImageSizes.Where(x => x.ImageSizeId == 1).ToList();

                //string root = Server.MapPath("~/App_Data/DivisionImages");

                var root = Path.Combine(PathP, CountryName, "DivisionImages", ims[0].ImageSize);

                string FileName = LCC[0].Image;

                root = Path.Combine(root, FileName);

                if (System.IO.File.Exists(root))
                {
                    return File(root, "image/png");
                }
                else
                {
                    root = Server.MapPath("~/App_Data/Uploads");

                    FileName = "not_available.png";

                    root = Path.Combine(root, FileName);

                    return File(root, "image/png");
                }


            }
            else
            {
                string root = Server.MapPath("~/App_Data/Uploads");

                string FileName = "not_available.png";

                root = Path.Combine(root, FileName);

                return File(root, "image/png");
            }
        }

        public JsonResult DeleteProductAdverts(string ProductAdvert, string Client, string Edition)
        {
            int ProductAdvertId = int.Parse(ProductAdvert);
            int ClientId = int.Parse(Client);
            int EditionId = int.Parse(Edition);

            List<EditionClientProductAdverts> LECP = db.EditionClientProductAdverts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductAdvertId == ProductAdvertId).ToList();

            if (LECP.LongCount() > 0)
            {
                var Delete = db.EditionClientProductAdverts.SingleOrDefault(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductAdvertId == ProductAdvertId);
                db.EditionClientProductAdverts.Remove(Delete);
                db.SaveChanges();

                ActivityLog.EditionClientProductAdverts(ClientId, ProductAdvertId, EditionId, 4);
            }

            List<ClientProductAdverts> LCPA = db.ClientProductAdverts.Where(x => x.ClientId == ClientId && x.ProductAdvertId == ProductAdvertId).ToList();

            if (LCPA.LongCount() > 0)
            {
                var Delete = db.ClientProductAdverts.SingleOrDefault(x => x.ClientId == ClientId && x.ProductAdvertId == ProductAdvertId);

                db.ClientProductAdverts.Remove(Delete);
                db.SaveChanges();

                ActivityLog.ClientProductAdverts(ClientId, ProductAdvertId, 4);
            }

            List<ProductAdverts> LPA = db.ProductAdverts.Where(x => x.ProductAdvertId == ProductAdvertId).ToList();

            if (LPA.LongCount() > 0)
            {
                var Delete = db.ProductAdverts.SingleOrDefault(x => x.ProductAdvertId == ProductAdvertId);
                db.ProductAdverts.Remove(Delete);
                db.SaveChanges();

                ActivityLog.ProductAdverts(ProductAdvertId, LPA[0].ProductAdvertName, LPA[0].Description, LPA[0].Order, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}