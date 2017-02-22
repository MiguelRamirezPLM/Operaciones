using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Web.Models.Sessions;
using Web.Models;
using Web.Models.Class;

namespace Web.Controllers.Sales
{
    public class SalesController : Controller
    {
        Medinet db = new Medinet();
        CRUD CRUD = new CRUD();
        Inserts Inserts = new Inserts();
        getData _data = new getData();

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

            List<plm_spGetProductsToEditByDivision> LS = new List<plm_spGetProductsToEditByDivision>();
            LS = db.Database.SqlQuery<plm_spGetProductsToEditByDivision>("plm_spGetProductsToEditByDivision @divisionId=" + Division + ", @countryId= " + Country + ", @bookId= " + Book + ", @editionId= " + Edition + "").ToList();

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

        public JsonResult getEditions(int CountryId, int BookId)
        {
            List<plm_spGetEditionsByBook_Result> _getEditionsByBook = db.Database.SqlQuery<plm_spGetEditionsByBook_Result>
                ("plm_spGetEditionsByBook @CountryId=" + CountryId + ", @BookId=" + BookId + "").OrderByDescending(model => model.NumberEdition).ToList();
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

        public ActionResult getLaboratories(int CountryId, int BookId, int EditionId)
        {
            var _getDivisionsByEdition = db.Database.SqlQuery<plm_spGetDivisionsByEditionByCountry_Result>("plm_spGetDivisionsByEditionByCountry @countryId=" + CountryId + ",@editionId=" + EditionId).ToList();
            plm_spGetDivisionsByEditionByCountry_Result _plm_spGetDivisionsByEditionByCountry = new plm_spGetDivisionsByEditionByCountry_Result();
            List<plm_spGetDivisionsByEditionByCountry_Result> _lplm_spGetDivisionsByEditionByCountry = new List<plm_spGetDivisionsByEditionByCountry_Result>();
            foreach (var _row in _getDivisionsByEdition)
            {
                _plm_spGetDivisionsByEditionByCountry = new plm_spGetDivisionsByEditionByCountry_Result();
                _plm_spGetDivisionsByEditionByCountry.DivisionId = _row.DivisionId;
                _plm_spGetDivisionsByEditionByCountry.DivisionName = _row.DivisionName;
                _plm_spGetDivisionsByEditionByCountry.CountryId = _row.CountryId;
                _plm_spGetDivisionsByEditionByCountry.EditionId = EditionId;
                _plm_spGetDivisionsByEditionByCountry.BookId = BookId;
                _lplm_spGetDivisionsByEditionByCountry.Add(_plm_spGetDivisionsByEditionByCountry);
            }
            return Json(_lplm_spGetDivisionsByEditionByCountry, JsonRequestBehavior.AllowGet);
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

        public JsonResult getProductLines(int ProductId, int PharmaFormId, int Categoryid, int DivisionId)
        {
            List<plm_spGetProductByProductLines_Result> LS = new List<plm_spGetProductByProductLines_Result>();
            LS = db.Database.SqlQuery<plm_spGetProductByProductLines_Result>("plm_spGetProductByProductLines @ProductId=" + ProductId + ", @CategoryId=" + Categoryid + ", @PharmaFormId=" + PharmaFormId + ", @DivisionId=" + DivisionId + "").ToList();
            return Json(LS, JsonRequestBehavior.AllowGet);
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

        public JsonResult Createproduct(string ProductName,
                                         string Division,
                                         string PharmaForm,
                                         string Country,
                                         string ProductType,
                                         string Category,
                                         string Edition,
                                         string Description,
                                         string Register,
                                         string SS)
        {

            List<SP_ParticipantProducts> LPP = new List<SP_ParticipantProducts>();

            int DivisionId = int.Parse(Division);
            int AlphabetId = _data.getAlphabetId(ProductName);
            int LaboratoryId = _data.getLaboratoryId(DivisionId);
            int PharmaFormId = int.Parse(PharmaForm);
            int CountryId = int.Parse(Country);
            int ProductTypeId = int.Parse(ProductType);
            int CategoryId = int.Parse(Category);
            int EditionId = int.Parse(Edition);
            int ProductId;

            var qryprod = db.Products.Where(x => x.CountryId == CountryId && x.AlphabetId == AlphabetId && x.LaboratoryId == LaboratoryId && x.ProductTypeId == ProductTypeId && x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim()).ToList();

            if (qryprod.LongCount() > 0)
            {
                ProductId = qryprod[0].ProductId;
            }
            else
            {
                ProductId = Inserts.inserproduct(CountryId, LaboratoryId, AlphabetId, ProductTypeId, ProductName, Description);
            }

            try
            {
                var _responsedivisioncategories = db.Database.SqlQuery<DivisionCategories>("plm_spCRUDDivisionCategories @CRUDType=" + CRUD.Read + ", @divisionId=" + DivisionId + ", @categoryId=" + CategoryId + "").ToList();

                if (_responsedivisioncategories.LongCount() == 0)
                {
                    bool _response = Inserts.insertDivisionCategories(DivisionId, CategoryId);
                }

                bool _responseproductpharmaforms = Inserts.insertproductpharmaforms(ProductId, PharmaFormId);

                bool _responsepc = Inserts.insertproductcategories(ProductId, PharmaFormId, CategoryId, DivisionId);

                bool _resultnewproducts = Inserts.insertnewproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);

                bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);

                LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

                if (LPP.LongCount() == 0)
                {
                    bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);
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

        public JsonResult EditDivision(string Division, string Description, string ShortName)
        {
            int DivisionId = int.Parse(Division);

            var dvs = db.Divisions.Where(x => x.DivisionId == DivisionId).ToList();

            if (dvs.LongCount() > 0)
            {
                foreach (Divisions _dvs in dvs)
                {
                    _dvs.Description = Description.Trim();
                    _dvs.ShortName = ShortName.Trim();

                    db.SaveChanges();
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
                                                   string Contact)
        {
            int DivisionInfId = int.Parse(AddressI);

            var di = db.DivisionInformation.Where(x => x.DivisionInfId == DivisionInfId).ToList();

            foreach (DivisionInformation _di in di)
            {
                if (!string.IsNullOrEmpty(Address))
                {
                    _di.Address = Address.Trim();
                }
                else
                {
                    _di.Address = null;
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    _di.Email = Email.Trim();
                }
                else
                {
                    _di.Email = null;
                }

                if (!string.IsNullOrEmpty(City))
                {
                    _di.City = City.Trim();
                }
                else
                {
                    _di.City = null;
                }

                if (!string.IsNullOrEmpty(Suburb))
                {
                    _di.Suburb = Suburb.Trim();
                }
                else
                {
                    _di.Suburb = null;
                }

                if (!string.IsNullOrEmpty(ZipCode))
                {
                    _di.ZipCode = ZipCode.Trim();
                }
                else
                {
                    _di.ZipCode = null;
                }

                if (!string.IsNullOrEmpty(Phone))
                {
                    _di.Telephone = Phone.Trim();
                }
                else
                {
                    _di.Telephone = null;
                }

                if (!string.IsNullOrEmpty(Fax))
                {
                    _di.Fax = Fax.Trim();
                }
                else
                {
                    _di.Fax = null;
                }

                if (!string.IsNullOrEmpty(Web))
                {
                    _di.Web = Web.Trim();
                }
                else
                {
                    _di.Web = null;
                }

                if (!string.IsNullOrEmpty(State))
                {
                    _di.State = State.Trim();
                }
                else
                {
                    _di.State = null;
                }

                if (!string.IsNullOrEmpty(Contact))
                {
                    _di.Contact = Contact.Trim();
                }
                else
                {
                    _di.Contact = null;
                }

                db.SaveChanges();
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
                                     string Division)
        {

            int DivisionId = int.Parse(Division);
            int divisionInfId = 0;

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
                                                                    ", @CRUDType=" + CRUD.Create + ", @active=" + true + "").ToList();
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

        public JsonResult DeleteAddress(string Division)
        {
            int DivisionInfId = int.Parse(Division);

            var result = db.Database.ExecuteSqlCommand("plm_spCRUDDivisionInformation @CRUDType=" + CRUD.Delete + ", @divisionInfId=" + DivisionInfId + "");

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

        public JsonResult ChangeProductsToDivision(String ListItems, string ArraySize, string Division, string Edition)
        {
            int DivisionId = int.Parse(Division);
            int EditionId = int.Parse(Edition);

            int Size = int.Parse(ArraySize);




            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult participantproducts(string Product, string PharmaForm, string Category, string Edition, string Division)
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
                bool _resulteditiondivisionproducts = Inserts.inserteditiondivisionproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);
            }

            LPP = db.Database.SqlQuery<SP_ParticipantProducts>("plm_spCRUDParticipantProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Read + "").ToList();

            if (LPP.LongCount() == 0)
            {
                bool _response = Inserts.insertparticipantproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult deleteparticipantproducts(string Product, string PharmaForm, string Category, string Edition, string Division)
        {
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);
            int PharmaFormId = int.Parse(PharmaForm);
            int ProductId = int.Parse(Product);

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

                    var _responseEDP = db.Database.ExecuteSqlCommand("plm_spCRUDEditionDivisionProducts @editionId = " + EditionId + ", @divisionId = " + DivisionId + ", @categoryId= " + CategoryId + ", @pharmaFormId= " + PharmaFormId + ",@productId=" + ProductId + ", @CRUDType=" + CRUD.Delete + "");

                    bool _resultnewproducts = Inserts.deletenewproducts(EditionId, DivisionId, CategoryId, PharmaFormId, ProductId);
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

        public JsonResult EditProducts(string Product, string ProductType, string ProductName, string Description, string Register, string COFEPRIS, string PharmaForm, string Category, string Division, string Country)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int CountryId = int.Parse(Country);
            int LaboratoryId = _data.getLaboratoryId(DivisionId);
            byte ProductTypeId = Convert.ToByte(ProductType);
            

            var p = db.Products.Where(x => x.Brand.ToUpper().Trim() == ProductName.ToUpper().Trim() && x.ProductId != ProductId && x.CountryId == CountryId && x.ProductTypeId == ProductTypeId && x.LaboratoryId == LaboratoryId).ToList();

            if (p.LongCount() > 0)
            {
                return Json("_existProduct", JsonRequestBehavior.AllowGet);
            }


            var pcs = db.ProductCategories.Where(x => x.CategoryId != CategoryId && x.PharmaFormId != PharmaFormId && x.ProductId != ProductId && x.SanitaryRegister == Register).ToList();

            if (pcs.LongCount() > 0)
            {
                return Json("_existRegister", JsonRequestBehavior.AllowGet);
            }


            var pc = db.ProductCategories.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (pc.LongCount() > 0)
            {
                foreach (ProductCategories _pc in pc)
                {
                    if (!string.IsNullOrEmpty(COFEPRIS))
                    {
                        _pc.SSFraction = COFEPRIS.Trim();
                    }
                    else
                    {
                        _pc.SSFraction = null;
                    }

                    if (!string.IsNullOrEmpty(Register))
                    {
                        _pc.SanitaryRegister = Register.Trim();
                    }
                    else
                    {
                        _pc.SanitaryRegister = null;
                    }


                    db.SaveChanges();
                }
            }

            var prod = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (prod.LongCount() > 0)
            {
                foreach (Products _prod in prod)
                {
                    _prod.Brand = ProductName.ToUpper().Trim();

                    if (!string.IsNullOrEmpty(Description))
                    {
                        _prod.Description = Description.Trim();
                    }
                    else
                    {
                        _prod.Description = null;
                    }

                    db.SaveChanges();
                }
            }





            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}