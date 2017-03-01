using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Web.Models.Sessions;
using Web.Models;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace Web.Controllers.Sales
{
    public class SalesController : Controller
    {
        Medinet db = new Medinet();
        CRUD CRUD = new CRUD();
        Inserts Inserts = new Inserts();
        getData _data = new getData();
        PLMUsers plm = new PLMUsers();
        PLMUserActions Action = new PLMUserActions();
        PLMUserTables Tables = new PLMUserTables();
        private DataTable table = new DataTable();
        private DataTable table2 = new DataTable();
        private DataTable ParticipantPro = new DataTable();

        public ActionResult Index()
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            if (_counTries.country.LongCount() >= 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("FilterIndex", "Production", new { id = _counTries.countryid }); // cambiar para Ventas 
            }
        }

        public ActionResult Content(int id, int ed, int ad, int ud)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            int Country = id;
            int Book = ed;
            int EditionType = 1; //cambiar
            int Edition = ad;
            int Division = ud;

            List<plm_spGetProductsToEditByDivisionMedinet3> LS = new List<plm_spGetProductsToEditByDivisionMedinet3>();
            LS = db.Database.SqlQuery<plm_spGetProductsToEditByDivisionMedinet3>("plm_spGetProductsToEditByDivisionMedinet3 @divisionId=" + Division + ", @countryId= " + Country + ", @bookId= " + Book + ", @editionId= " + Edition + "").ToList();

            SessionCountryByDivision _SessionCountryByDivision = new SessionCountryByDivision(id, ed, ad, ud);
            Session["SessionCountryByDivision"] = _SessionCountryByDivision;


            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            ViewData["DivisionName"] = db.Divisions.FirstOrDefault(model => model.DivisionId == ud).Description;
            ViewData["BookName"] = db.Books.FirstOrDefault(model => model.BookId == ed).Description;
            ViewData["EditionNumber"] = db.Editions.FirstOrDefault(model => model.EditionId == ad).NumberEdition;
            ViewData["ShortName"] = db.Books.FirstOrDefault(model => model.BookId == ed).ShortName;

            ViewData["CountryId"] = Country;
            ViewData["BookId"] = Book;
            ViewData["EditionId"] = Edition;
            ViewData["DivisionId"] = Division;

            SessionM sessionSM = new SessionM(Country, Book, EditionType, Edition, Division);
            Session["sessionSM"] = sessionSM;
            return View(LS);
        }

        public JsonResult getBooks(int CountryId)
        {
            var _getBook = (from _b in db.plm_vwProductsByEdition
                            where _b.CountryId == CountryId
                            select new { _b.BookId, _b.BookName, _b.CountryId }).Distinct().ToList().OrderBy(model => model.BookName);
            plm_vwProductsByEdition _plm_vwProductsByEdition = new plm_vwProductsByEdition();
            List<plm_vwProductsByEdition> _l_plm_vwProductsByEdition = new List<plm_vwProductsByEdition>();
            foreach (var _row in _getBook)
            {
                _plm_vwProductsByEdition = new plm_vwProductsByEdition();
                _plm_vwProductsByEdition.BookName = _row.BookName;
                _plm_vwProductsByEdition.BookId = _row.BookId;
                _plm_vwProductsByEdition.CountryId = _row.CountryId;
                _l_plm_vwProductsByEdition.Add(_plm_vwProductsByEdition);
            }
            return Json(_l_plm_vwProductsByEdition, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEditions(int CountryId, int BookId, int EditionType)
        {
            int EditionTypeId;
            if (EditionType ==(- 20))
             {
               List<plm_spGetEditionsByBook_Result> _getEditionsByBook = db.Database.SqlQuery<plm_spGetEditionsByBook_Result>
                ("plm_spGetEditionsByBook @CountryId=" + CountryId + ", @BookId=" + BookId + "").OrderBy(model => model.NumberEdition).ToList();
               plm_spGetEditionsByBook_Result _plm_spGetEditionsByBook = new plm_spGetEditionsByBook_Result();
               List<plm_spGetEditionsByBook_Result> _lplm_spGetEditionsByBook = new List<plm_spGetEditionsByBook_Result>();
               foreach (var _rowEdition in _getEditionsByBook)
               {
                   _plm_spGetEditionsByBook = new plm_spGetEditionsByBook_Result();
                   _plm_spGetEditionsByBook.NumberEdition = _rowEdition.NumberEdition;
                   _plm_spGetEditionsByBook.EditionId = _rowEdition.EditionId;
                   _plm_spGetEditionsByBook.CountryId = _rowEdition.CountryId;
                   _plm_spGetEditionsByBook.BookId = _rowEdition.BookId;
                   _plm_spGetEditionsByBook.ISBN = _rowEdition.ISBN;
                   _lplm_spGetEditionsByBook.Add(_plm_spGetEditionsByBook);
               }
               return Json(_lplm_spGetEditionsByBook, JsonRequestBehavior.AllowGet);
             }
           else
           {
               //EditionTypeId = int.Parse(EditionType);
               EditionTypeId = EditionType;
               List<Editions> LE = new List<Editions>();
               Editions E = new Editions();

               var e = db.Editions.Where(x => x.BookId == BookId && x.CountryId == CountryId && x.EditionTypeId == EditionTypeId).OrderBy(x => x.NumberEdition).ToList();

               foreach (Editions _e in e)
               {
                   E = new Editions();

                   E.Active = _e.Active;
                   E.BookId = _e.BookId;
                   E.CountryId = _e.CountryId;
                   E.EditionId = _e.EditionId;
                   E.EditionTypeId = _e.EditionTypeId;
                   E.ISBN = _e.ISBN;
                   E.NumberEdition = _e.NumberEdition;

                   LE.Add(E);
               }

               return Json(LE, JsonRequestBehavior.AllowGet);
           }
            
        }

        public ActionResult getLaboratories(int CountryId, int BookId, int EditionId)
        {
            var _getDivisionsByEdition = db.Database.SqlQuery<Divisions>("plm_spGetDivisionsByCountry @countryId=" + CountryId + "").ToList();
            plm_spGetDivisionsByEditionByCountry_Result _plm_spGetDivisionsByEditionByCountry = new plm_spGetDivisionsByEditionByCountry_Result();
            List<plm_spGetDivisionsByEditionByCountry_Result> _lplm_spGetDivisionsByEditionByCountry = new List<plm_spGetDivisionsByEditionByCountry_Result>();
            foreach (var _row in _getDivisionsByEdition)
            {
                _plm_spGetDivisionsByEditionByCountry = new plm_spGetDivisionsByEditionByCountry_Result();
                _plm_spGetDivisionsByEditionByCountry.DivisionId = _row.DivisionId;
                _plm_spGetDivisionsByEditionByCountry.DivisionName = _row.Description;
                _plm_spGetDivisionsByEditionByCountry.CountryId = _row.CountryId;
                _plm_spGetDivisionsByEditionByCountry.EditionId = EditionId;
                _plm_spGetDivisionsByEditionByCountry.BookId = BookId;
                _lplm_spGetDivisionsByEditionByCountry.Add(_plm_spGetDivisionsByEditionByCountry);
            }
            return Json(_lplm_spGetDivisionsByEditionByCountry, JsonRequestBehavior.AllowGet);

            //List<Divisions> LD = db.Database.SqlQuery<Divisions>("plm_spGetDivisionsByCountry @countryId=" + CountryId + "").ToList();
            //return Json(LD, JsonRequestBehavior.AllowGet);         
        }

        public JsonResult getParticipantProducts(int ProductId, int PharmaFormId, int Categoryid, int DivisionId)
        {
            List<plm_spGetEditionsByProducts_Result> _getEditionsByproducts = db.Database.SqlQuery<plm_spGetEditionsByProducts_Result>
                ("plm_spGetEditionsByProducts @ProductId=" + ProductId + ", @CategoryId=" + Categoryid + ", @PharmaFormId=" + PharmaFormId + ", @DivisionId=" + DivisionId + "").ToList();
            plm_spGetEditionsByProducts_Result _plm_vwProductsByEdition = new plm_spGetEditionsByProducts_Result();
            List<plm_spGetEditionsByProducts_Result> _lplm_vwProductsByEdition = new List<plm_spGetEditionsByProducts_Result>();
            foreach (var _row in _getEditionsByproducts)
            {
                _plm_vwProductsByEdition = new plm_spGetEditionsByProducts_Result();
                _plm_vwProductsByEdition.ShortName = _row.ShortName;
                _plm_vwProductsByEdition.NumberEdition = _row.NumberEdition;
                _plm_vwProductsByEdition.TypeName = _row.TypeName;
                if (_row.ISBN == null)
                {
                    _plm_vwProductsByEdition.ISBN = "";
                }
                else
                {
                    _plm_vwProductsByEdition.ISBN = _row.ISBN;
                }
                if (_row.ParentId != null)
                {
                    var _parent = (from _e in db.Editions
                                   join _b in db.Books
                                       on _e.BookId equals _b.BookId
                                   where _e.EditionId == _row.ParentId
                                   select new { _e.NumberEdition, _b.ShortName }).ToList();
                    foreach (var _rowParent in _parent)
                    {
                        _plm_vwProductsByEdition.ParentName = _rowParent.ShortName;
                        _plm_vwProductsByEdition.NumberEditionParent = _rowParent.NumberEdition.ToString();
                    }
                }
                else
                {
                    _plm_vwProductsByEdition.ParentName = "";
                    _plm_vwProductsByEdition.NumberEditionParent = "";
                }
                _lplm_vwProductsByEdition.Add(_plm_vwProductsByEdition);
            }
            return Json(_lplm_vwProductsByEdition, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEditionParticipantProduct(int ProductId, int PharmaFormId, int Categoryid, int DivisionId, int CountryId)
        {
            
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProductLines(int ProductId, int PharmaFormId, int Categoryid, int DivisionId)
        {
            List<plm_spGetProductByProductLines_Result> LS = new List<plm_spGetProductByProductLines_Result>();
            LS = db.Database.SqlQuery<plm_spGetProductByProductLines_Result>("plm_spGetProductByProductLines @ProductId=" + ProductId + ", @CategoryId=" + Categoryid + ", @PharmaFormId=" + PharmaFormId + ", @DivisionId=" + DivisionId + "").ToList();

            if (LS.LongCount() > 0)
            {
                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                LS.Add(new plm_spGetProductByProductLines_Result() { ProductLine = "Sin línea de producto " });
                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult saveSanitary(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, string SanitaryRegister)
        {
            var _productCategories = (from _pc in db.ProductCategories
                                      where _pc.ProductId == ProductId
                                      && _pc.PharmaFormId == PharmaFormId
                                      && _pc.CategoryId == CategoryId
                                      && _pc.DivisionId == DivisionId
                                      select _pc).ToList();
            foreach (var _row in _productCategories)
            {
                if (SanitaryRegister == "")
                {
                    _row.SanitaryRegister = null;
                    db.SaveChanges();
                }
                else
                {
                    _row.SanitaryRegister = SanitaryRegister;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult saveFraction(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, string Fraction)
        {
            var _productCategories = (from _pc in db.ProductCategories
                                      where _pc.ProductId == ProductId
                                      && _pc.PharmaFormId == PharmaFormId
                                      && _pc.CategoryId == CategoryId
                                      && _pc.DivisionId == DivisionId
                                      select _pc).ToList();
            foreach (var _row in _productCategories)
            {
                if (Fraction == "")
                {
                    _row.SSFraction = null;
                    db.SaveChanges();
                }
                else
                {
                    _row.SSFraction = Fraction;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult itsNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult notNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult withChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(string ProductName,
                                         string Division,
                                         string PharmaForm,
                                         string Country,
                                         string ProductType,
                                         string Category,
                                         string Edition,
                                         string Description,
                                         string Register,
                                         string SS,
                                         string Fabricante,
                                         int UserId,
                                         string HashKey)
        {

            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            int DivisionId = int.Parse(Division);
            int AlphabetId = _data.getAlphabetId(ProductName);
            int LaboratoryId = int.Parse(Fabricante); //_data.getLaboratoryId(DivisionId); modificar 
            int PharmaFormId = int.Parse(PharmaForm);
            int CountryId = int.Parse(Country);
            int ProductTypeId = int.Parse(ProductType);
            int CategoryId = int.Parse(Category);
            int EditionId = int.Parse(Edition);
            int ProductId;
            string primaryKeyAffected;
            string FieldsAffected;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            

            var qryprod = db.Products.Where(x => x.CountryId == CountryId && x.AlphabetId == AlphabetId && x.LaboratoryId == LaboratoryId && x.ProductTypeId == ProductTypeId && x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim()).ToList();

            if (qryprod.LongCount() > 0)
            {
                ProductId = qryprod[0].ProductId;
            }
            else
            {
                ProductId = Inserts.inserproduct(CountryId, LaboratoryId, AlphabetId, ProductTypeId, ProductName, Description, UserId, HashKey);
            }

            try
            {
                //var _responsedivisioncategories = db.Database.SqlQuery<DivisionCategories>("plm_spCRUDDivisionCategories @CRUDType=" + CRUD.Read + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "").ToList();
                var _responsedivisioncategories = db.DivisionCategories.Where(x => x.DivisionId == DivisionId && x.CategoryId == CategoryId).ToList();

                if (_responsedivisioncategories.LongCount() == 0)
                {
                    bool _response = Inserts.insertDivisionCategories(DivisionId, CategoryId, UserId, HashKey);
                }

                bool _responseproductpharmaforms = Inserts.insertproductpharmaforms(ProductId, PharmaFormId, UserId, HashKey);

                bool _responsepc = Inserts.insertproductcategories(ProductId, PharmaFormId, CategoryId, DivisionId, UserId, HashKey);

                var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                if (pc.LongCount() > 0)
                {
                    primaryKeyAffected = "(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                    foreach (ProductCategories _pc in pc)
                    {
                        if (!string.IsNullOrEmpty(Register))
                        {
                            _pc.SanitaryRegister = Register.Trim();
                            FieldsAffected = "(SanitaryRegister," + Register.Trim() + ")";
                        }
                        else
                        {
                            _pc.SanitaryRegister = null;
                            FieldsAffected = null;
                        }

                        if (!string.IsNullOrEmpty(SS))
                        {
                            _pc.SSFraction = SS.Trim();
                            if (!string.IsNullOrEmpty(FieldsAffected))
                            {
                                FieldsAffected = FieldsAffected + ";(SSFraction," + SS.Trim() + ")";
                            }
                            else
                            {
                                FieldsAffected = "(SSFractionm," + SS.Trim() + ")";
                            }
                            
                        }
                        else
                        {
                            _pc.SSFraction = null;
                            FieldsAffected = FieldsAffected;
                        }

                        if (!string.IsNullOrEmpty(FieldsAffected))
                        {
                            _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductCategories + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                        }



                        db.SaveChanges();
                    }
                }

                bool _resultnewproducts = Inserts.insertnewproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);

                bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() == 0)
                {
                    bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //string message = e.InnerException.Message;

                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult getproductstoaddpf(string CountryId, string BookId, string EditionId, string DivisionId)
        {
            int Country = int.Parse(CountryId);
            int Book = int.Parse(BookId);
            int Edition = int.Parse(EditionId);
            int Division = int.Parse(DivisionId);

            List<plm_spGetProductsToEditByDivision> LS = new List<plm_spGetProductsToEditByDivision>();

            LS = db.Database.SqlQuery<plm_spGetProductsToEditByDivision>("plm_spGetProductsByDivision @divisionId =" + Division + ", @countryId =" + Country + ", @bookId =" + Book + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getpharmaformswithoutproducts(string Product)
        {
            int ProductId = int.Parse(Product);

            List<plm_spGetPharmaFormsWithoutProduct> LPF = new List<plm_spGetPharmaFormsWithoutProduct>();

            LPF = db.Database.SqlQuery<plm_spGetPharmaFormsWithoutProduct>("plm_spGetPharmaFormsWithoutProduct @productId =" + ProductId + "").ToList();

            return Json(LPF, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LaboratoryInformation(int? Division)
        {
            if ((!Request.IsAuthenticated) || (Division == null))
            {
                return RedirectToAction("Logout", "Login");
            }
            List<DivisionInformation> LDI = db.Database.SqlQuery<DivisionInformation>("plm_spGetDivisionInformation @divisionid=" + Division + "").ToList();

            return View(LDI);
        }

        public JsonResult EditDivision(string Division, string Description, string ShortName, int UserId, string HashKey)
        {
            int DivisionId = int.Parse(Division);
            string primaryKeyAffected;
            string FieldsAffected;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            var dvs = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            if (dvs.LongCount() > 0)
            {
                foreach (Divisions _dvs in dvs)
                {
                    _dvs.Description = Description.Trim();
                    _dvs.ShortName = ShortName.Trim();

                    db.SaveChanges();

                    try
                    {
                        primaryKeyAffected = "(DivisionId," + DivisionId + ")";
                        FieldsAffected = "(Description," + Description + ");(ShortName," + ShortName + ")";
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Divisions + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    
                    }
                    catch (Exception e)
                    {
                string message = e.Message;

                     Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditLaboratoryInformation(string AddressI,
                                                   string Address,
                                                   string Email,
                                                   string City,
                                                   string Suburb,
                                                   string ZipCode,
                                                   string Phone,
                                                   string Fax,
                                                   string Web,
                                                   string State,
                                                   string Contact,
                                                   int UserId, 
                                                   string HashKey)
        {
            int DivisionInfId = int.Parse(AddressI);

            var di = db.DivisionInformation.Where(x => x.DivisionInfId == DivisionInfId).ToList();
            string primaryKeyAffected;
            string FieldsAffected = null;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            primaryKeyAffected = "(DivisionInfId," + DivisionInfId + ")";

            foreach (DivisionInformation _di in di)
            {
                if (!string.IsNullOrEmpty(Address))
                {
                    _di.Address = Address.Trim();
                    FieldsAffected = "(Address," + _di.Address + ");";
                }
                else
                {
                    _di.Address = null;
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    _di.Email = Email.Trim();
                    FieldsAffected = FieldsAffected + "(Email," + _di.Email + ");";
                }
                else
                {
                    _di.Email = null;
                }

                if (!string.IsNullOrEmpty(City))
                {
                    _di.City = City.Trim();
                    FieldsAffected = FieldsAffected + "(City," + _di.City + ");";
                }
                else
                {
                    _di.City = null;
                }

                if (!string.IsNullOrEmpty(Suburb))
                {
                    _di.Suburb = Suburb.Trim();
                    FieldsAffected = FieldsAffected + "(Suburb," + _di.Suburb + ");";
                }
                else
                {
                    _di.Suburb = null;
                }

                if (!string.IsNullOrEmpty(ZipCode))
                {
                    _di.ZipCode = ZipCode.Trim();
                    FieldsAffected = FieldsAffected + "(ZipCode," + _di.ZipCode + ");";
                }
                else
                {
                    _di.ZipCode = null;
                }

                if (!string.IsNullOrEmpty(Phone))
                {
                    _di.Telephone = Phone.Trim();
                    FieldsAffected = FieldsAffected + "(Telephone," + _di.Telephone + ");";
                }
                else
                {
                    _di.Telephone = null;
                }

                if (!string.IsNullOrEmpty(Fax))
                {
                    _di.Fax = Fax.Trim();
                    FieldsAffected = FieldsAffected + "(Fax," + _di.Fax + ");";
                }
                else
                {
                    _di.Fax = null;
                }

                if (!string.IsNullOrEmpty(Web))
                {
                    _di.Web = Web.Trim();
                    FieldsAffected = FieldsAffected + "(Web," + _di.Web + ");";
                }
                else
                {
                    _di.Web = null;
                }

                if (!string.IsNullOrEmpty(State))
                {
                    _di.State = State.Trim();
                    FieldsAffected = FieldsAffected + "(State," + _di.State + ");";
                }
                else
                {
                    _di.State = null;
                }

                if (!string.IsNullOrEmpty(Contact))
                {
                    _di.Contact = Contact.Trim();
                    FieldsAffected = FieldsAffected + "(Contact," + _di.Contact + ");";
                }
                else
                {
                    _di.Contact = null;
                }

                db.SaveChanges();
                try
                {
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.DivisionInformation + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();

                }
                catch (Exception e)
                {
                    string message = e.Message;

                    Json(false, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddAddress(string Address,
                                     string Suburb,
                                     string ZipCode,
                                     string Phone,
                                     string Fax,
                                     string Web,
                                     string Email,
                                     string City,
                                     string State,
                                     string Contact,
                                     string Division,
                                     int UserId,
                                     string HashKey)
        {

            int DivisionId = int.Parse(Division);
            int divisionInfId = 0;
            string primaryKeyAffected;
            string FieldsAffected = null;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            try
            {
                var di = db.DivisionInformation.Where(x =>  x.Suburb == Suburb && x.ZipCode == ZipCode && x.Telephone == Phone && x.Fax == Fax && x.Email == Email && x.City == City && x.State == State && x.Contact == Contact && x.DivisionId == DivisionId).ToList();
               
                if (di.LongCount() == 0)
                {
                    var result = db.Database.SqlQuery<CreateProduct_result>("plm_spCRUDDivisionInformation @divisionId=" + DivisionId +
                                                                    ", @divisionInfId='" + divisionInfId + "'" +
                                                                    ", @address='" + Address.Trim() + "'" +
                                                                    ", @suburb='" + Suburb.Trim() + "'" +
                                                                    ", @zipCode='" + ZipCode.Trim() + "'" +
                                                                    ", @telephone='" + Phone.Trim() + "'" +
                                                                    ", @fax='" + Fax.Trim() + "'" +
                                                                    ", @web='" + Web.Trim() + "'" +
                                                                    ", @email='" + Email.Trim() + "'" +
                                                                    ", @city='" + City.Trim() + "'" +
                                                                    ", @state='" + State.Trim() + "'" +
                                                                    ", @contact='" + Contact + "'" +
                                                                    ", @CRUDType=" + CRUD.Create + 
                                                                    ", @active=" + true + "").ToList();
                    var DivInf  = db.DivisionInformation.Where(x =>  x.Suburb == Suburb && x.ZipCode == ZipCode && x.Telephone == Phone && x.Fax == Fax && x.Email == Email && x.City == City && x.State == State && x.Contact == Contact && x.DivisionId == DivisionId).ToList();
                    divisionInfId = DivInf[0].DivisionInfId;
                    primaryKeyAffected = "(DivisionInfId," + divisionInfId + ")";
                    FieldsAffected = "(DivisionId," + DivisionId + ");(Address," + Address.Trim() + ");(Suburb," + Suburb.Trim() + ");(ZipCode," + ZipCode.Trim() + ");(Telephone," + Phone.Trim()
                                      + ");(Fax," + Fax.Trim() + ");(Web," + Web.Trim() + ");(Email," + Email.Trim() + ");(City," + City.Trim() + ");(State," + State.Trim() + ");(Contact," + Contact + ")";

                    try
                    {
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.DivisionInformation + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();

                    }
                    catch (Exception e)
                    {
                        string message = e.Message;

                        Json(false, JsonRequestBehavior.AllowGet);
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult DeleteAddress(string Division, int UserId, string HashKey)
        {
            int DivisionInfId = int.Parse(Division);
            var DivInf = db.DivisionInformation.Where(x => x.DivisionInfId == DivisionInfId ).ToList();
            string primaryKeyAffected = "(DivisionInfId," + DivisionInfId + ")";
            string FieldsAffected = null;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            FieldsAffected = FieldsAffected = "(DivisionId," + DivInf[0].DivisionId + ");(Address," + DivInf[0].Address + ");(Suburb," + DivInf[0].Suburb + ");(ZipCode," + DivInf[0].ZipCode + ");(Telephone," + DivInf[0].Telephone
                                      + ");(Fax," + DivInf[0].Fax + ");(Web," + DivInf[0].Web + ");(Email," + DivInf[0].Email + ");(City," + DivInf[0].City + ");(State," + DivInf[0].State + ");(Contact," + DivInf[0].Contact + ")";
             

            var result = db.Database.ExecuteSqlCommand("plm_spCRUDDivisionInformation @CRUDType=" + CRUD.Delete + ", @divisionInfId=" + DivisionInfId + "");

            try
            {
                _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.DivisionInformation + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();

            }
            catch (Exception e)
            {
                string message = e.Message;

                Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeProducts(int? Division, int? CountryId, int? DivisionSearch, string BrandName)
        {
            if ((!Request.IsAuthenticated) || (Division == null) || (CountryId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            List<plm_spGetDivisionProductsToEdit> LS = new List<plm_spGetDivisionProductsToEdit>();

            if (DivisionSearch != null)
            {
                if (!string.IsNullOrEmpty(BrandName))
                {
                    LS = db.Database.SqlQuery<plm_spGetDivisionProductsToEdit>("plm_spGetDivisionProductsToEdit @divisionId=" + Division + ", @countryId=" + CountryId + ", @divisionSearch=" + DivisionSearch + ", @brand='" + BrandName.Trim() + "'").ToList();

                    SessionDivisionId SessionDivisionId = new SessionDivisionId(DivisionSearch, BrandName);
                    Session["SessionDivisionId"] = SessionDivisionId;

                    return View(LS);
                }
                else
                {
                    LS = db.Database.SqlQuery<plm_spGetDivisionProductsToEdit>("plm_spGetDivisionProductsToEdit @divisionId=" + Division + ", @countryId=" + CountryId + ", @divisionSearch=" + DivisionSearch + "").ToList();

                    SessionDivisionId SessionDivisionId = new SessionDivisionId(DivisionSearch, null);
                    Session["SessionDivisionId"] = SessionDivisionId;

                    return View(LS);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(BrandName))
                {
                    LS = db.Database.SqlQuery<plm_spGetDivisionProductsToEdit>("plm_spGetDivisionProductsToEdit @divisionId=" + Division + ", @countryId=" + CountryId + ", @brand='" + BrandName.Trim() + "'").ToList();

                    SessionDivisionId SessionDivisionId = new SessionDivisionId(DivisionSearch, BrandName);
                    Session["SessionDivisionId"] = SessionDivisionId;

                    return View(LS);
                }
                else
                {
                    LS = db.Database.SqlQuery<plm_spGetDivisionProductsToEdit>("plm_spGetDivisionProductsToEdit @divisionId=" + Division + ", @countryId=" + CountryId + "").ToList();

                    Session["SessionDivisionId"] = null;

                    return View(LS);
                }
            }
        }

        public JsonResult ChangeProductsToDivision(String ListItems, string ArraySize, string Division, string Edition, int UserId, string HashKey)
        {
            int DivisionId = int.Parse(Division);
            int EditionId = int.Parse(Edition);

            int Size = int.Parse(ArraySize);

            dynamic items = JsonConvert.DeserializeObject(ListItems);
            for (var i = 0; i <= Size - 1; i++)
            {
                int DId = Convert.ToInt32(items[i]["DId"]);
                int PId = Convert.ToInt32(items[i]["PId"]);
                int PFId = Convert.ToInt32(items[i]["PFId"]);
                int CId = Convert.ToInt32(items[i]["CId"]);


                var _responsedivisioncategories = db.Database.SqlQuery<DivisionCategories>("plm_spCRUDDivisionCategories @CRUDType=" + CRUD.Read + ", @divisionId=" + DivisionId + ", @categoryId=" + CId + "").ToList();

                if (_responsedivisioncategories.LongCount() == 0)
                {
                    bool _response = Inserts.insertDivisionCategories(DivisionId, CId, UserId, HashKey);
                }

                try
                {
                    var _result = db.Database.SqlQuery<ReadProductCategories>("plm_spCRUDProductCategories @divisionId = " + DivisionId + ", @categoryId= " + CId + ", @pharmaFormId= " + PFId + ",@productId= " + PId + ", @CRUDType=" + CRUD.Read + "").ToList();

                    if (_result.LongCount() == 0)
                    {
                        bool _responsepc = Inserts.insertproductcategories(PId, PFId, CId, DivisionId, UserId, HashKey);
                    }

                    var _resultedp = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CId + ", @pharmaFormId= " + PFId + ",@productId=" + PId + ", @CRUDType=" + CRUD.Read + "").ToList();

                    if (_resultedp.LongCount() == 0)
                    {
                        bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CId, PFId, PId, UserId, HashKey);
                    }
                }
                catch (Exception e)
                {

                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult participantproducts(string Product, string PharmaForm, string Category, string Edition, string Division, int UserId, string HashKey)
        {
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);

            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            var _responseEDP = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (_responseEDP.LongCount() == 0)
            {
                bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
            }

            LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (LPP.LongCount() == 0)
            {
                bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteparticipantproducts(string Product, string PharmaForm, string Category, string Edition, string Division, int UserId, string HashKey)
        {
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);
            string primaryKeyAffected;
            string FieldsAffected;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            try
            {
                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();
            }
            catch (Exception e)
            {
                string message = e.InnerException.Message;
            }


            if (LPP.LongCount() > 0)
            {
                try
                {
                    db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "").ToList();
                    primaryKeyAffected = "(EditionId," + LPP[0].EditionId + ");(DivisionId," + LPP[0].DivisionId + ");(CategoryId," + LPP[0].CategoryId
                                          + ");(PharmaFormId," + LPP[0].PharmaFormId + ");(ProductId," + LPP[0].ProductId + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                    
                    //var _response = db.Database.ExecuteSqlCommand("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "");
                    //_ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.NewProducts + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                    ////var _responseEDP = db.Database.ExecuteSqlCommand("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "");
                    //bool _resultnewproducts = Inserts.deletenewproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);
                    
                }
                catch (Exception e)
                {
                    string message = e.InnerException.Message;
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditProducts(string Product,
                                       string ProductType,
                                       string ProductName, 
                                       string Description, 
                                       string Register,
                                       string SS,
                                       string PharmaForm,
                                       string Category,
                                       string Division,
                                       string Country,
                                       int UserId,
                                       string HashKey)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int CountryId = int.Parse(Country);
            int LaboratoryId = _data.getLaboratoryId(DivisionId);
            byte ProductTypeId = Convert.ToByte(ProductType);
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            string primaryKeyAffected;
            string FieldsAffected;
            bool BrandEqual;
            bool TPEqual;
            bool RegisterEqual;
            bool SSEqual;

           

            var p = db.Products.Where(x => x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim() && x.ProductId != ProductId && x.CountryId == CountryId && x.ProductTypeId == ProductTypeId && x.LaboratoryId == LaboratoryId).ToList();

            if (p.LongCount() > 0)
            {
                return Json("_existProduct", JsonRequestBehavior.AllowGet);
            }


            //var pcs = db.ProductCategories.Where(x => x.CategoryId != CategoryId && x.PharmaFormId != PharmaFormId && x.ProductId != ProductId && x.SanitaryRegister == Register).ToList();

            //if (pcs.LongCount() > 0)
            //{
            //    return Json("_existRegister", JsonRequestBehavior.AllowGet);
            //}


            var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (pc.LongCount() > 0)
            {
                primaryKeyAffected = "(DivisionId," + DivisionId + ");(CategoryId," + CategoryId + ");(PharmaFormId," + PharmaFormId + ");(ProductId," + ProductId + ")";
                foreach (ProductCategories _pc in pc)
                {

                    if ((RegisterEqual = String.Equals(_pc.SanitaryRegister, Register.Trim(), StringComparison.Ordinal)) ==false)
                    {
                            _pc.SanitaryRegister = Register.Trim();
                            FieldsAffected = "(SanitaryRegister," + Register.Trim() + ")";
                        
                    }

                    else
                    {
                        FieldsAffected = null;
                    }

                    if ((SSEqual = String.Equals(_pc.SSFraction, SS.Trim(), StringComparison.Ordinal)) == false)
                    {
                          _pc.SSFraction = SS.Trim();
                            if (!string.IsNullOrEmpty(FieldsAffected))
                            {
                                FieldsAffected = FieldsAffected + ";(SSFraction," + SS.Trim() + ")";
                            }
                            else
                            {
                                FieldsAffected = "(SSFraction," + SS.Trim() + ")";
                            }

                    }

                    else
                    {
                        FieldsAffected = null;
                    }

                    if (!string.IsNullOrEmpty(FieldsAffected))
                    {
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductCategories + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    }
                    db.SaveChanges();
                }
            }

            var prod = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (prod.LongCount() > 0)
            {
                primaryKeyAffected = "(ProductId," + ProductId + ")";
                FieldsAffected = null;
                foreach (Products _prod in prod)
                {

                    if ((BrandEqual = String.Equals(_prod.Brand, ProductName.ToUpper().Trim(), StringComparison.Ordinal)) == false)
                    {
                        _prod.Brand = ProductName.ToUpper().Trim();
                        FieldsAffected = "(Brand," + ProductName.ToUpper().Trim() + ")";
                    }

                    if (_prod.ProductTypeId != ProductTypeId)
                    {
                        _prod.ProductTypeId = ProductTypeId;

                        if (BrandEqual ==false )
                        {
                            FieldsAffected = FieldsAffected + ";(ProductTypeId," + ProductTypeId + ")";
                        }

                        else
                        {
                            FieldsAffected = "(ProductTypeId," + ProductTypeId + ")";
                        }
                    }

                    else
                    {
                        FieldsAffected = FieldsAffected;
                    }

                    if ((BrandEqual = String.Equals(_prod.Description, Description.Trim().Trim(), StringComparison.Ordinal)) == false)
                    {
                         _prod.Description = Description.Trim();

                            if (!string.IsNullOrEmpty(FieldsAffected))
                            {
                                FieldsAffected =  FieldsAffected + ";(Description," + Description.Trim() + ")";
                            }
                            else
                            {
                                FieldsAffected = "(Description," + Description.Trim() + ")";
                            }
                        
                    }

                    else
                    {
                        FieldsAffected = FieldsAffected;
                    }

                    

                    if (!string.IsNullOrEmpty(FieldsAffected))
                    {
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Products + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    }
                    db.SaveChanges();
                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addpharmaformtoproduct(string Product, string PharmaForm, string Division, string Edition, int UserId, string HashKey)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = 101;
            int DivisionId = int.Parse(Division);
            int EditionId = int.Parse(Edition);
            

            try
            {
                 var PPF = db.ProductPharmaForms.Where(x => x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();


                 if (PPF.LongCount() == 0)
                {
                    bool _result = Inserts.insertproductpharmaforms(ProductId, PharmaFormId, UserId, HashKey);
                }

                var _responseproductcategories = db.Database.SqlQuery<SP_ProductCategories>("plm_spCRUDProductCategories @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId= " + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responseproductcategories.LongCount() == 0)
                {
                    bool _resultproductcategories = Inserts.insertproductcategories(ProductId, PharmaFormId, CategoryId, DivisionId, UserId, HashKey);
                }

                var _responseparticipantproducts = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responseparticipantproducts.LongCount() == 0)
                {
                    bool _resultparticipantproducts = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                var _responsenewproducts = db.Database.SqlQuery<NewProducts>("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responsenewproducts.LongCount() == 0)
                {
                    bool _resultnewproducts = Inserts.insertnewproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                var _responseeditiondivisionproducts = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responseeditiondivisionproducts.LongCount() == 0)
                {
                    bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }
            }
            catch (Exception ec)
            {
                //string message = ec.InnerException.Message;
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OffMarketProduct(int Product, int PharmaForm, int Division, int Category, int UserId, string HashKey)
        {
            string primaryKeyAffected = "(DivisionId," + Division + ");(CategoryId," + Category + ");(PharmaFormId," + PharmaForm + ");(ProductId," + Product + ")";

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            List<OffMarkets> LOM = new List<OffMarkets>();
            var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductOffMarkets @DivisionId = " + Division + ", @CategoryId= " + Category + ", @PharmaFormId= " + PharmaForm + ",@ProductId= " + Product + ",@CRUDType=" + CRUD.Create + "");
            _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteOffMarketProduct(int Product, int PharmaForm, int Division, int Category, int UserId, string HashKey)
        {
            string primaryKeyAffected = "(DivisionId," + Division + ");(CategoryId," + Category + ");(PharmaFormId," + PharmaForm + ");(ProductId," + Product + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>(); 
            List<OffMarkets> LOM = new List<OffMarkets>();
            var _response = db.Database.ExecuteSqlCommand("plm_spCRUDProductOffMarkets @DivisionId = " + Division + ", @CategoryId= " + Category + ", @PharmaFormId= " + PharmaForm + ",@ProductId= " + Product + ",@CRUDType=" + CRUD.Delete + "");
            _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult productchanges(string Product, string PharmaForm, string Category, string Edition, string Division, string ContentType, int UserId, string HashKey)
        {

            string FieldsAffected = "(ContentTypeId," + ContentType + ")";
            string primaryKeyAffected = "(EditionId," + Edition + ");(DivisionId," + Division + ");(CategoryId," + Category + ");(PharmaFormId," + PharmaForm + ");(ProductId," + Product + ")";
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
            if (!string.IsNullOrEmpty(ContentType))
            {
                int ContentTypeId = int.Parse(ContentType);
                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);
                int EditionId = int.Parse(Edition);
                int DivisionId = int.Parse(Division);

               
                List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() > 0)
                {
                    try
                    {
                        List<NewProducts> Np = new List<NewProducts>();
                        Np = db.Database.SqlQuery<NewProducts>("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                        if (Np.LongCount() > 0)
                        {
                            return Json("_errorNP", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + ", @modifiedContent=" + ContentTypeId + "").ToList();

                            _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                        }
                    }
                    catch (SqlException ex)
                    {
                        string message = ex.InnerException.Message.ToString();
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("_errorPP", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                int ProductId = int.Parse(Product);
                int PharmaFormId = int.Parse(PharmaForm);
                int CategoryId = int.Parse(Category);
                int EditionId = int.Parse(Edition);
                int DivisionId = int.Parse(Division);

                List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() > 0)
                {
                    try
                    {
                        List<NewProducts> Np = new List<NewProducts>();
                        Np = db.Database.SqlQuery<NewProducts>("plm_spCRUDNewProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                        if (Np.LongCount() > 0)
                        {
                            return Json("_errorNP", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Update + "").ToList();
                            FieldsAffected = "(ContentTypeId,4)";
                            _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ParticipantProducts + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                        }
                    }
                    catch (SqlException ex)
                    {
                        string message = ex.InnerException.Message.ToString();
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("_errorPP", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult rowsPerPage(string Value)
        {
            if (Value == "0")
            {
                Session["SessionRows"] = null;
            }
            else
            {
                SessionRows SessionRows = new SessionRows(Value);
                Session["SessionRows"] = SessionRows;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProductSidef(string Product, string PharmaForm, string Category, string Edition, string Division, string Operation, int UserId, string HashKey)
        {
            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);
            int EditionProductShotId;
            string FieldsAffected;
            string primaryKeyAffected;

            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            var pst = db.ProductShotTypes.Where(x => x.TypeName.ToUpper().Trim() == ("SOLO").ToUpper().Trim()).ToList();

            int PSTypeId = pst[0].PSTypeId;

            if (Operation == "Insert")
            {
                try
                {

                    LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                    if (LPP.LongCount() == 0)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }

                    var _result = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    if (_result.LongCount() == 0)
                    {
                        var _response = db.Database.ExecuteSqlCommand("plm_spCRUDEditionProductShots @CRUDType=" + CRUD.Create +
                                                                            ", @editionId=" + EditionId +
                                                                            ", @divisionId=" + DivisionId +
                                                                            ", @categoryId=" + CategoryId +
                                                                            ", @productId=" + ProductId +
                                                                            ", @pharmaFormId=" + PharmaFormId +
                                                                            ", @psTypeId=" + PSTypeId +
                                                                            ", @editionProductShotId=" + 0 +
                                                                            ", @active=" + true + "");
                        var SID = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                        EditionProductShotId = SID[0].EditionProductShotId;
                        primaryKeyAffected = "(EditionProductShotId," + EditionProductShotId + ")";
                        FieldsAffected = "(EditionId," + Edition + ");(DivisionId," + Division + ");(CategoryId," + Category + ");(PharmaFormId," + PharmaForm + ");(ProductId," + Product + ")"; ;
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.EditionProductShots + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    }
                }
                catch (Exception e)
                {
                    string message = e.Message;
                }

            }
            else
            {
                var _result = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                if (_result.LongCount() > 0)
                {
                    var _response = db.Database.ExecuteSqlCommand("plm_spCRUDEditionProductShots @CRUDType=" + CRUD.Delete +
                                                                        ", @editionProductShotId=" + _result[0].EditionProductShotId + "");
                    EditionProductShotId = _result[0].EditionProductShotId;
                    primaryKeyAffected = "(EditionProductShotId," + EditionProductShotId + ")";
                    FieldsAffected = "(EditionId," + Edition + ");(DivisionId," + Division + ");(CategoryId," + Category + ");(PharmaFormId," + PharmaForm + ");(ProductId," + Product + ")"; ;
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.EditionProductShots + ",@operationId=" + Action.Eliminar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                }
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }
   

        public JsonResult getBookEdition(int CountryId, int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            List<GetBookToEditionByContry_Result> _List = new List<GetBookToEditionByContry_Result>();
            _List = db.Database.SqlQuery<GetBookToEditionByContry_Result>("plm_spGetBookToEditionByContry @CountryId = " + CountryId + ", @ProductId = " + @ProductId + ",@PharmaFormId=" + PharmaFormId + ",@CategoryId=" + CategoryId + ",@DivisionId=" + DivisionId + "").ToList();
            
           


            return Json(_List, JsonRequestBehavior.AllowGet);
        }

        public FileResult ExcelProductsByEdition(int id, int ed, int ad, int ud, int uId)
        {
            var bind = new GridView();
            string Use= "";
            string _contentype = "";
            string Sidef = "SI";
            string Nuevo = "SI";

            List<plm_spGetProductsToEditByDivisionMedinet3> LS = new List<plm_spGetProductsToEditByDivisionMedinet3>();
            LS = db.Database.SqlQuery<plm_spGetProductsToEditByDivisionMedinet3>("plm_spGetProductsToEditByDivisionMedinet3 @divisionId=" + ud + ", @countryId= " + id + ", @bookId= " + ed + ", @editionId= " + ad + "").ToList();
            var User = (from _u in plm.Users
                        where _u.UserId == uId
                        select new {_u.Name, _u.LastName, _u.SecondLastName }).ToList();
            foreach (var _reg in User)
            {
                Use = _reg.Name + " " + _reg.LastName + " " + _reg.SecondLastName;
            }

            var Edi = (from _e in db.Editions
                           where _e.EditionId == ad
                           select _e.NumberEdition).Distinct().ToList();
            var Book = (from _b in db.Books
                            where _b.BookId == ed
                            select _b.Description).Distinct().ToList();

            DataColumn Usuario = new DataColumn();
            Usuario.DataType = typeof(string);
            Usuario.ColumnName = "Listado de Productos Participantes";
            table2.Columns.Add(Usuario);
            table2.Rows.Add("Obra: " + Book[0]);
            table2.Rows.Add("Edición: " + Edi[0]);
            table2.Rows.Add("Laboratorio: " + LS[0].DivisionName);
            table2.Rows.Add("Usuario: " + Use);
            table2.Rows.Add("Fecha: " + DateTime.Now);
            

            DataColumn Categoría = new DataColumn();
            Categoría.DataType = typeof(string);
            Categoría.ColumnName = "Categoría";


            DataColumn Producto = new DataColumn();
            Producto.DataType = typeof(string);
            Producto.ColumnName = "Producto";

            DataColumn Forma = new DataColumn();
            Forma.DataType = typeof(string);
            Forma.ColumnName = "Forma farmaceutica";

            DataColumn Tipo = new DataColumn();
            Tipo.DataType = typeof(string);
            Tipo.ColumnName = "Tipo de producto";

            DataColumn Descripción = new DataColumn();
            Descripción.DataType = typeof(string);
            Descripción.ColumnName = "Descripción";

            DataColumn New = new DataColumn();
            New.DataType = typeof(string);
            New.ColumnName = "Producto Nuevo";

            DataColumn Contenido = new DataColumn();
            Contenido.DataType = typeof(string);
            Contenido.ColumnName = "Contenido modificado";

            DataColumn ProductShot = new DataColumn();
            ProductShot.DataType = typeof(string);
            ProductShot.ColumnName = "ProductShot";
           
            table.Columns.Add(Categoría);
            table.Columns.Add(Producto);
            table.Columns.Add(Forma);
            table.Columns.Add(Tipo);
            table.Columns.Add(Descripción);
            table.Columns.Add(New);
            table.Columns.Add(Contenido);
            table.Columns.Add(ProductShot);

            var _ct= (from  _contentType in db.ContentTypes
                       select  _contentType).ToList();

            foreach (var item in LS)
            {

                if (item.Participant > 0)
                {
                    foreach (var _row in _ct)
                    {
                        if (item.ModifiedContent == _row.ContentTypeId)
                        {
                            if (item.ModifiedContent == 1)
                            {
                                _contentype = _ct[3].ContentType;
                            }
                            else
                            {
                                _contentype = _row.ContentType;
                            }
                            
                        }
                    }

                    if (item.Sidef == 0 )
                    {
                        Sidef = "NO";
                    }
                    else
                    {
                        Sidef = "SI";
                    }
                    if(item.NewProduct ==0)
                    {
                        Nuevo = "NO";
                    }
                    else
                    {
                        Nuevo = "SI";
                    }

                   table.Rows.Add(
                   item.CategoryName,
                   item.Brand,
                   item.PharmaForm,
                   item.ProductType,
                   item.ProductDescription,
                   Nuevo,
                   _contentype,
                   Sidef);
                }
            }

            string Directory = Request.MapPath("~/ReportesXLS");
            if (System.IO.Directory.Exists(Directory))
            {
                //
            }
            else
            {   // Crea el directorio
                System.IO.Directory.CreateDirectory(Directory);
            }
            bind.DataSource = table2;
            bind.DataSource = table;
            
            FileInfo newFile = new FileInfo(Request.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
            ExcelPackage pkg = new ExcelPackage(newFile);
            if (newFile.Exists)
            {
                System.IO.File.Delete(Server.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
                newFile.Delete();
                pkg.Workbook.Worksheets.Delete("Reporte Excel");
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte Excel");
                worksheet.Cells["A1"].LoadFromDataTable(table2, true);
                using (ExcelRange range = worksheet.Cells["A1:A6"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 14;
                    range.Style.Font.Name = "Arial Narrow";
                }

                worksheet.Cells["A8"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A8:K8"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                    range.Style.Font.Size = 12;
                    range.AutoFitColumns();
                    range.Style.Font.Name = "Arial Narrow";
                }
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            else
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte Excel");
                worksheet.Cells["A1"].LoadFromDataTable(table2, true);
                using (ExcelRange range = worksheet.Cells["A1:A6"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 14;
                    range.Style.Font.Name = "Arial Narrow";
                }
                worksheet.Cells["A8"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A8:K8"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                    range.Style.Font.Size = 12;
                    range.AutoFitColumns();
                    range.Style.Font.Name = "Arial Narrow";
                }
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            pkg.SaveAs(newFile);
            var filepath = System.IO.Path.Combine(Server.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
            return File(filepath, "application/vnd.ms-excel", "ReporteExcel.xlsx");
        }

        public JsonResult GetFamilyContents(string Edition, string Division)
        {
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);

            var ptC = db.PrefixTypes.Where(x => x.TypeName.ToUpper().Trim() == ("Contenido").ToUpper().Trim()).Select(x => x.PrefixTypeId).ToList();

            List<ContentFamiliesByDivision> LS = new List<ContentFamiliesByDivision>();

            try
            {
                LS = db.Database.SqlQuery<ContentFamiliesByDivision>("plm_spGetContentFamiliesByDivision @editionId=" + EditionId + ", @divisionId=" + DivisionId + "").ToList();
            }
            catch (Exception e)
            {

            }
            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFamilyProductShots(string Edition, string Division)
        {
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);

            var ptPS = db.PrefixTypes.Where(x => x.TypeName.ToUpper().Trim() == ("ProductShot").ToUpper().Trim()).Select(x => x.PrefixTypeId).ToList();

            List<ContentFamiliesByDivision> LS = new List<ContentFamiliesByDivision>();

            LS = db.Database.SqlQuery<ContentFamiliesByDivision>("plm_spGetPSFamiliesByDivision @editionId=" + EditionId + ", @divisionId=" + DivisionId + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFamilyContent(string Edition, string Product, string PharmaForm, string Category, string Division, int UserId, string HashKey)
        {

            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);

            var ptC = db.PrefixTypes.Where(x => x.TypeName.ToUpper().Trim() == ("Contenido").ToUpper().Trim()).Select(x => x.PrefixTypeId).ToList();

            var fc = db.Database.SqlQuery<FamilyPrefixes>("plm_spGetFamilyPrefixesByEdition @editionId=" + EditionId + ", @prefixTypeId=" + ptC[0] + "").ToList();

            if (fc.LongCount() > 0)
            {
                List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() == 0)
                {
                    bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                var _responseEDP = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responseEDP.LongCount() == 0)
                {
                    bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                foreach (FamilyPrefixes _fc in fc)
                {
                    int CurrentValue = _fc.CurrentValue + 1;
                    int FamilyId = 0;
                    int PrefixId = _fc.PrefixId;
                    String FamilyString = "[" + _fc.PrefixName + "]" + CurrentValue;

                    //var _rins = db.Database.ExecuteSqlCommand("plm_spCRUDFamilies @prefixId=" + _fc.PrefixId + ", @familyString='" + FamilyString + "', @active=" + true + ", @familyId=" + 0 + "");

                    var fm = db.Families.Where(x => x.PrefixId == PrefixId && x.FamilyString.ToUpper().Trim() == FamilyString.ToUpper().Trim()).ToList();

                    if (fm.LongCount() == 0)
                    {
                        Families Families = new Families();

                        Families.Active = true;
                        Families.FamilyString = FamilyString;
                        Families.PrefixId = PrefixId;

                        db.Families.Add(Families);
                        db.SaveChanges();

                        var _rins = db.Families.Where(x => x.PrefixId == PrefixId && x.FamilyString.ToUpper().Trim() == FamilyString.ToUpper().Trim()).ToList();

                        FamilyId = _rins[0].FamilyId;
                    }
                    else
                    {
                        FamilyId = fm[0].FamilyId;
                    }

                    try
                    {


                        var _ru = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyPrefixes @CRUDType=" + CRUD.Update +
                                                                                        ", @prefixId=" + PrefixId +
                                                                                        ", @editionId=" + EditionId +
                                                                                        ", @prefixName='" + _fc.PrefixName + "'" +
                                                                                        ", @currentValue=" + CurrentValue +
                                                                                        ", @prefixDescription='" + _fc.PrefixDescription + "'" +
                                                                                        ", @active=" + true + "");

                        var _ri = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyProducts @CRUDType=" + CRUD.Create +
                                                                                          ", @editionId=" + EditionId +
                                                                                          ", @familyId=" + FamilyId +
                                                                                          ", @divisionId=" + DivisionId +
                                                                                          ", @categoryId=" + CategoryId +
                                                                                          ", @pharmaFormId=" + PharmaFormId +
                                                                                          ", @productId=" + ProductId + "");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddFamilyProductShot(string Edition, string Product, string PharmaForm, string Category, string Division, int UserId, string HashKey)
        {
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int EditionProductShotId = 0;

            var ptPS = db.PrefixTypes.Where(x => x.TypeName.ToUpper().Trim() == ("ProductShot").ToUpper().Trim()).Select(x => x.PrefixTypeId).ToList();

            var fc = db.Database.SqlQuery<FamilyPrefixes>("plm_spGetFamilyPrefixesByEdition @editionId=" + EditionId + ", @prefixTypeId=" + ptPS[0] + "").ToList();

            if (fc.LongCount() > 0)
            {
                List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() == 0)
                {
                    bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                var _responseEDP = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (_responseEDP.LongCount() == 0)
                {
                    bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
                }

                var edp = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                if (edp.LongCount() == 0)
                {
                    try
                    {


                        var _rieps = db.Database.ExecuteSqlCommand("plm_spCRUDEditionProductShots @CRUDType=" + CRUD.Create +
                                                                                                  ", @editionId=" + EditionId +
                                                                                                  ", @divisionId=" + DivisionId +
                                                                                                  ", @categoryId=" + CategoryId +
                                                                                                  ", @pharmaFormId=" + PharmaFormId +
                                                                                                  ", @productId=" + ProductId +
                                                                                                  ", @psTypeId=" + 1 +
                                                                                                  ", @editionProductShotId=" + 0 +
                                                                                                  ", @active=" + true + "");
                    }
                    catch (Exception e)
                    {

                    }
                    var edpId = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    EditionProductShotId = edpId[0].EditionProductShotId;
                }
                else
                {
                    EditionProductShotId = edp[0].EditionProductShotId;
                }

                foreach (FamilyPrefixes _fc in fc)
                {
                    int CurrentValue = _fc.CurrentValue + 1;
                    int FamilyId = 0;
                    int PrefixId = _fc.PrefixId;
                    String FamilyString = "[" + _fc.PrefixName + "]" + CurrentValue;

                    //var _rins = db.Database.ExecuteSqlCommand("plm_spCRUDFamilies @prefixId=" + _fc.PrefixId + ", @familyString='" + FamilyString + "', @active=" + true + ", @familyId=" + 0 + "");

                    var fm = db.Families.Where(x => x.PrefixId == PrefixId && x.FamilyString.ToUpper().Trim() == FamilyString.ToUpper().Trim()).ToList();

                    if (fm.LongCount() == 0)
                    {
                        Families Families = new Families();

                        Families.Active = true;
                        Families.FamilyString = FamilyString;
                        Families.PrefixId = PrefixId;

                        db.Families.Add(Families);
                        db.SaveChanges();

                        var _rins = db.Families.Where(x => x.PrefixId == PrefixId && x.FamilyString.ToUpper().Trim() == FamilyString.ToUpper().Trim()).ToList();

                        FamilyId = _rins[0].FamilyId;
                    }
                    else
                    {
                        FamilyId = fm[0].FamilyId;
                    }

                    try
                    {


                        var _ru = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyPrefixes @CRUDType=" + CRUD.Update +
                                                                                        ", @prefixId=" + PrefixId +
                                                                                        ", @editionId=" + EditionId +
                                                                                        ", @prefixName='" + _fc.PrefixName + "'" +
                                                                                        ", @currentValue=" + CurrentValue +
                                                                                        ", @prefixDescription='" + _fc.PrefixDescription + "'" +
                                                                                        ", @active=" + true + "");

                        var _ri = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyProductShots @CRUDType=" + CRUD.Create +
                                                                                          ", @familyId=" + FamilyId +
                                                                                          ", @editionProductShotId=" + EditionProductShotId + "");
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertFamilyProducts(string Edition, string Product, string PharmaForm, string Category, string Division, string Family, int UserId, string HashKey)
        {
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int FamilyId = int.Parse(Family);

            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (LPP.LongCount() == 0)
            {
                bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
            }

            var _responseEDP = db.Database.SqlQuery<EditionDivisionProducts>("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (_responseEDP.LongCount() == 0)
            {
                bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId, UserId, HashKey);
            }

            var _ri = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyProducts @CRUDType=" + CRUD.Create +
                                                                                          ", @editionId=" + EditionId +
                                                                                          ", @familyId=" + FamilyId +
                                                                                          ", @divisionId=" + DivisionId +
                                                                                          ", @categoryId=" + CategoryId +
                                                                                          ", @pharmaFormId=" + PharmaFormId +
                                                                                          ", @productId=" + ProductId + "");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertFamilyProductShot(string Edition, string Product, string PharmaForm, string Category, string Division, string Family, int UserId, string HashKey)
        {
            int FamilyId = int.Parse(Family);
            int EditionId = int.Parse(Edition);
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);

            int EditionProductShotId = 0;

            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (LPP.LongCount() == 0)
            {
                bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId,  UserId,  HashKey);
            }

            var edp = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (edp.LongCount() == 0)
            {
                try
                {


                    var _rieps = db.Database.ExecuteSqlCommand("plm_spCRUDEditionProductShots @CRUDType=" + CRUD.Create +
                                                                                              ", @editionId=" + EditionId +
                                                                                              ", @divisionId=" + DivisionId +
                                                                                              ", @categoryId=" + CategoryId +
                                                                                              ", @pharmaFormId=" + PharmaFormId +
                                                                                              ", @productId=" + ProductId +
                                                                                              ", @psTypeId=" + 1 +
                                                                                              ", @editionProductShotId=" + 0 +
                                                                                              ", @active=" + true + "");
                }
                catch (Exception e)
                {

                }
                var edpId = db.EditionProductShots.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                EditionProductShotId = edpId[0].EditionProductShotId;
            }
            else
            {
                EditionProductShotId = edp[0].EditionProductShotId;
            }

            var _ri = db.Database.ExecuteSqlCommand("plm_spCRUDFamilyProductShots @CRUDType=" + CRUD.Create +
                                                                                          ", @familyId=" + FamilyId +
                                                                                          ", @editionProductShotId=" + EditionProductShotId + "");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProductPrices (int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            List<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result> Prices = new List<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>();
            try
            {
                Prices = db.Database.SqlQuery<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>("plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId + ", @CRUDType =" + CRUD.Read + "").ToList();

                if (Prices.LongCount() == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            

            return Json(Prices, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProductPrices(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int PresentationId, int EANId, string BarCode, float? Price, int? SourceNameId, int? UpdatePriceByPriceSource, int UserId, string HashKey)
        {
            try
            {
                List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();
                string primaryKeyAffected;
                string FieldsAffected;

                if (SourceNameId != null && Price != null )
                {
                    if ( UpdatePriceByPriceSource == 1)
                    {
                        var Update = db.Database.SqlQuery<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>("plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId + ", @CRUDType =" + CRUD.Update + ", @presentationId =" + PresentationId + ", @barCodeId =" + EANId + ", @barCode ='" + BarCode.Trim() + "' , @price =" + Price + ", @priceSourceId=" + SourceNameId + ", @priceless=" + UpdatePriceByPriceSource + "").ToList();
                        primaryKeyAffected = "(BarCodeId," + EANId + ")";
                        FieldsAffected = "(BarCode," + BarCode.Trim() + ")";
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                        primaryKeyAffected = "(PresentationId," + PresentationId + ");(BarCodeId," + EANId + ");(PriceSourceId," + SourceNameId + ")";
                        FieldsAffected = "(Price," + Price + ")";
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductPrices + ",@operationId=" + Action.Agregar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    
                     }
                    else
                    {
                        
                        UpdatePriceByPriceSource = 0;
                        var Update = db.Database.SqlQuery<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>("plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId + ", @CRUDType =" + CRUD.Update + ", @presentationId =" + PresentationId + ", @barCodeId =" + EANId + ", @barCode ='" + BarCode.Trim() + "' , @price =" + Price + ", @priceSourceId=" + SourceNameId + "").ToList();
                        primaryKeyAffected = "(BarCodeId," + EANId + ")";
                        FieldsAffected = "(BarCode," + BarCode.Trim() + ")";
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                        primaryKeyAffected = "(PresentationId," + PresentationId + ");(BarCodeId," + EANId + ");(PriceSourceId," + SourceNameId + ")";
                        FieldsAffected = "(Price," + Price + ")";
                        _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductPrices + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    
                    }

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult getPresentations(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            try
            {
              var  Presentations = db.Database.SqlQuery<plm_spGetPresentationByDivisionByProduct>("plm_spGetPresentationByDivisionByProduct @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId  + "").ToList(); 
                return Json(Presentations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            
        }

        public JsonResult getPriceSources(string TextPrice)
        {
            var PS = (from _ps in db.PriceSources
                      select new { _ps.PriceSourceId, _ps.SourceName }).Distinct().ToList().OrderBy(model => model.SourceName).ToList();
            return Json(PS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProductBarCodes(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int PresentationId, string EAN, float? Price, int? PriceSourceId, int UserId, string HashKey)
        {
            int priceless = 0;
            string primaryKeyAffected;
            string FieldsAffected;
            int BarcodeId = 0;
            List<ActivityLogInfo> _ActivityLogs = new List<ActivityLogInfo>();

            try
            {
                if (Price != null)
                {
                    priceless = 1;
                    var Create = db.Database.SqlQuery<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>("plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId + ", @CRUDType =" + CRUD.Create + ", @presentationId =" + PresentationId + ", @barCode ='" + EAN.Trim() + "' , @price =" + Price + ", @priceSourceId=" + PriceSourceId + ", @priceless=" + priceless + "").ToList();

                    var bc = db.BarCodes.Where(x => x.BarCode == EAN.Trim()).ToList();
                    BarcodeId = bc[0].BarCodeId;
                    primaryKeyAffected = "(BarCodeId," + BarcodeId + ")";
                    FieldsAffected = "(BarCode," + EAN.Trim() + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();

                    primaryKeyAffected = "(BarCodeId," + BarcodeId + ");(PresentationId," + PresentationId + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductBarCodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" +"").ToList();

                    primaryKeyAffected = "(BarCodeId," + BarcodeId + ");(PresentationId," + PresentationId + ");(PriceSourceId," + PriceSourceId + ")";
                    FieldsAffected = "(Price," + Price + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductPrices + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Create  = db.Database.SqlQuery<plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result>("plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources @divisionId = " + DivisionId + ", @categoryId	= " + CategoryId + ", @pharmaFormId = " + PharmaFormId + ",@productId =" + ProductId + ", @CRUDType =" + CRUD.Create + ", @presentationId =" + PresentationId + ", @barCode ='" + EAN.Trim() + "'" + "").ToList();

                    var bc = db.BarCodes.Where(x => x.BarCode == EAN.Trim()).ToList();
                    BarcodeId = bc[0].BarCodeId;
                    primaryKeyAffected = "(BarCodeId," + BarcodeId + ")";
                    FieldsAffected = "(BarCode," + EAN.Trim() + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.Barcodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "',@fieldsAffected='" + FieldsAffected + "'" + "").ToList();

                    primaryKeyAffected = "(BarCodeId," + BarcodeId + ");(PresentationId," + PresentationId + ")";
                    _ActivityLogs = plm.Database.SqlQuery<ActivityLogInfo>("dbo.plm_spCRUDActivityLogs @CRUDType =" + CRUD.Create + ",@userId=" + UserId + ",@tableId=" + Tables.ProductBarCodes + ",@operationId=" + Action.Actualizar + ",@hashKey='" + HashKey + "',@primaryKeyAffected='" + primaryKeyAffected + "'" + "").ToList();
                }

            }

            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}