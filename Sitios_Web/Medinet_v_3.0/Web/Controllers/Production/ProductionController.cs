﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Models;
namespace Web.Controllers.Production
{
    public class ProductionController : Controller
    {
        private DataTable table = new DataTable();
        Medinet db = new Medinet();
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
                return RedirectToAction("FilterIndex", "Production", new { id = _counTries.countryid });
            }
        }
        public ActionResult FilterIndex(int id)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            var _getBook = (from _b in db.plm_vwProductsByEdition
                            where _b.CountryId == id
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
            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            return View(_l_plm_vwProductsByEdition);
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
        public JsonResult getEditionsPage(int CountryId, int BookId)
        {
            List<plm_spGetEditionsByBook_Result> _getEditionsByBook = db.Database.SqlQuery<plm_spGetEditionsByBook_Result>
                ("plm_spGetEditionsByBook @CountryId=" + CountryId + ", @BookId=" + BookId + "").OrderByDescending(model => model.NumberEdition).ToList();
            plm_spGetEditionsByBook_Result _plm_spGetEditionsByBook = new plm_spGetEditionsByBook_Result();
            List<plm_spGetEditionsByBook_Result> _lplm_spGetEditionsByBook = new List<plm_spGetEditionsByBook_Result>();
            foreach (var _rowEdition in _getEditionsByBook)
            {
                var _editionType = (from _e in db.Editions
                                    join _et in db.EditionTypes
                                    on _e.EditionTypeId equals _et.EditionTypeId
                                    where _e.EditionId == _rowEdition.EditionId
                                    && _e.BookId == _rowEdition.BookId
                                    && _e.CountryId == _rowEdition.CountryId
                                    select _et).ToList();
                if (_editionType.SingleOrDefault().TypeName != "ELECTRÓNICO")
                {
                    _plm_spGetEditionsByBook = new plm_spGetEditionsByBook_Result();
                    _plm_spGetEditionsByBook.NumberEdition = _rowEdition.NumberEdition;
                    _plm_spGetEditionsByBook.EditionId = _rowEdition.EditionId;
                    _plm_spGetEditionsByBook.CountryId = _rowEdition.CountryId;
                    _plm_spGetEditionsByBook.BookId = _rowEdition.BookId;
                    _plm_spGetEditionsByBook.ISBN = _rowEdition.ISBN;
                    _lplm_spGetEditionsByBook.Add(_plm_spGetEditionsByBook);
                }
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
        public ActionResult Content(int id, int ed, int ad, int ud)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            SessionCountryByDivision _SessionCountryByDivision = new SessionCountryByDivision(id, ed, ad, ud);
            Session["SessionCountryByDivision"] = _SessionCountryByDivision;
            plm_vwProductsByEdition _plm_vwProductsByEdition = new plm_vwProductsByEdition();
            List<plm_vwProductsByEdition> _lplm_vwProductsByEdition = new List<plm_vwProductsByEdition>();
            var _getProductsByEdition = (from _p in db.plm_vwProductsByEdition
                                         where _p.CountryId == id
                                         && _p.BookId == ed
                                         && _p.EditionId == ad
                                         && _p.DivisionId == ud
                                         select _p).ToList();
            foreach (var _row in _getProductsByEdition)
            {
                _plm_vwProductsByEdition = new plm_vwProductsByEdition();
                var _contentType = (from _pp in db.ParticipantProducts
                                    join _ct in db.ContentTypes
                                    on _pp.ContentTypeId equals _ct.ContentTypeId
                                    where _pp.EditionId == _row.EditionId
                                    && _pp.ProductId == _row.ProductId
                                    && _pp.PharmaFormId == _row.PharmaFormId
                                    && _pp.CategoryId == _row.CategoryId
                                    && _pp.DivisionId == _row.DivisionId
                                    select new { _ct.ContentType, _pp.HTMLContent }).ToList();
                foreach (var _rowContent in _contentType)
                {
                    if (_rowContent.HTMLContent == null)
                    {
                        _plm_vwProductsByEdition.WithoutContent = "Sin contenido";
                    }
                    _plm_vwProductsByEdition.ContentType = _rowContent.ContentType;
                }
                var _productcategories = (from _pc in db.ProductCategories
                                          where _pc.ProductId == _row.ProductId
                                          && _pc.PharmaFormId == _row.PharmaFormId
                                          && _pc.CategoryId == _row.CategoryId
                                          && _pc.DivisionId == _row.DivisionId
                                          select new { _pc.SanitaryRegister, _pc.SSFraction }).ToList();
                foreach (var _rowcategories in _productcategories)
                {
                    _plm_vwProductsByEdition.SanitaryRegister = _rowcategories.SanitaryRegister;
                    _plm_vwProductsByEdition.SSFraction = _rowcategories.SSFraction;
                }
                _plm_vwProductsByEdition.ProductId = _row.ProductId;
                _plm_vwProductsByEdition.PharmaFormId = _row.PharmaFormId;
                _plm_vwProductsByEdition.CategoryId = _row.CategoryId;
                _plm_vwProductsByEdition.DivisionId = _row.DivisionId;
                _plm_vwProductsByEdition.Brand = _row.Brand;
                _plm_vwProductsByEdition.PharmaForm = _row.PharmaForm;
                _plm_vwProductsByEdition.CategoryName = _row.CategoryName;
                _plm_vwProductsByEdition.ProductDescription = _row.ProductDescription;
                _plm_vwProductsByEdition.ProductType = _row.ProductType;
                _lplm_vwProductsByEdition.Add(_plm_vwProductsByEdition);
            }
            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            ViewData["DivisionName"] = db.Divisions.FirstOrDefault(model => model.DivisionId == ud).Description;
            ViewData["BookName"] = db.Books.FirstOrDefault(model => model.BookId == ed).Description;
            ViewData["EditionNumber"] = db.Editions.FirstOrDefault(model => model.EditionId == ad).NumberEdition;
            ViewData["ShortName"] = db.Books.FirstOrDefault(model => model.BookId == ed).ShortName;
            return View(_lplm_vwProductsByEdition);
        }
        [HttpPost]
        public ActionResult SaveDivisionImage()
        {
            var _Image = Request.Files[0];
            string[] _divisionId = Request.Form.GetValues("DivisionId");
            string DivisionId = String.Join(",", _divisionId);
            int _DivisionId = int.Parse(DivisionId);

            var _vlDivisionImage = (from _divisionImage in db.DivisionImages
                                    where _divisionImage.DivisionId == _DivisionId
                                    select _divisionImage).ToList();
            if (_vlDivisionImage.LongCount() == 0)
            {
                DivisionImages _DivisionImages = new DivisionImages();
                _DivisionImages.DivisionId = _DivisionId;
                _DivisionImages.ImageName = _Image.FileName;
                _DivisionImages.ImageTypeId = 1;
                _DivisionImages.BaseUrl = null;
                _DivisionImages.Active = true;
                db.Entry(_DivisionImages).State = EntityState.Added;
                db.SaveChanges();
                var _pathneWDivisionImage = System.IO.Path.Combine(Server.MapPath("~/DivisionImages"), _Image.FileName);
                _Image.SaveAs(_pathneWDivisionImage);
            }
            else
            {
                var _pathDivisionImage = System.IO.Path.Combine(Server.MapPath("~/DivisionImages"), _Image.FileName);
                _Image.SaveAs(_pathDivisionImage);
                foreach (var _update in _vlDivisionImage)
                {
                    if (_update.ImageName.Trim() != _Image.FileName.Trim())
                    {
                        string _previusPathDivisionImage = Request.MapPath("~/DivisionImages/" + _update.ImageName);
                        if (System.IO.File.Exists(_previusPathDivisionImage))
                        {
                            System.IO.File.Delete(_previusPathDivisionImage);
                        }
                    }
                    _update.ImageName = _Image.FileName;
                    db.SaveChanges();
                }
            }
            DivisionImages _vlDivisionImages = new DivisionImages();
            List<DivisionImages> _l_DivisionImages = new List<DivisionImages>();
            var _getContent = (from _d in db.DivisionImages
                               where _d.DivisionId == _DivisionId
                               && _d.ImageTypeId == 1
                               select _d).ToList();
            foreach (var _row in _getContent)
            {
                _vlDivisionImages = new DivisionImages();
                _vlDivisionImages.ImageName = _row.ImageName;
                _l_DivisionImages.Add(_vlDivisionImages);
            }
            return Json(_l_DivisionImages, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deleteDivisionImage(int DivisionId)
        {
            var _divisionImage = db.DivisionImages.Where(model => model.DivisionId == DivisionId).ToList();
            foreach (var _delete in _divisionImage)
            {
                var _pathRemoveDivisionImage = System.IO.Path.Combine(Server.MapPath("~/DivisionImages"), _delete.ImageName);
                if (System.IO.File.Exists(_pathRemoveDivisionImage))
                {
                    System.IO.File.Delete(_pathRemoveDivisionImage);
                }
                db.DivisionImages.Remove(_delete);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
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
                _plm_vwProductsByEdition.LongCount = _getEditionsByproducts.LongCount();
                _lplm_vwProductsByEdition.Add(_plm_vwProductsByEdition);
            }
            return Json(_lplm_vwProductsByEdition, JsonRequestBehavior.AllowGet);
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
        public ActionResult itsNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes CRUDContentTypes = new CRUDContentTypes();
            CRUDContentTypes._updateNew(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult notNew(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();
            _CRUDContentTypes._deleteNew(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult withChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();
            _CRUDContentTypes._updateChanges(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult withoutChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();
            _CRUDContentTypes._deleteChanges(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult withOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();
            _CRUDContentTypes._updatewithOrthographicChanges(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult withoutOrthographicChanges(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId)
        {
            CRUDContentTypes _CRUDContentTypes = new CRUDContentTypes();
            _CRUDContentTypes._deletewithOrthographicChanges(ProductId, PharmaFormId, CategoryId, DivisionId, EditionId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getItems(int EditionId, int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            List<plm_spGetAttributeGroups_Result> _getAttributeGroups = db.Database.SqlQuery<plm_spGetAttributeGroups_Result>
            ("plm_spGetAttributeGroups"
            + " @productId=" + ProductId
            + ",@categoryId=" + CategoryId
            + ",@pharmaFormId=" + PharmaFormId
            + ",@divisionId=" + DivisionId
            + ",@editionId=" + EditionId + "").ToList();
            plm_spGetAttributeGroups_Result _plm_spGetAttributeGroups = new plm_spGetAttributeGroups_Result();
            List<plm_spGetAttributeGroups_Result> _lplm_spGetAttributeGroups = new List<plm_spGetAttributeGroups_Result>();
            foreach (var _row in _getAttributeGroups)
            {
                _plm_spGetAttributeGroups = new plm_spGetAttributeGroups_Result();
                _plm_spGetAttributeGroups.AttributeGroupId = _row.AttributeGroupId;
                _plm_spGetAttributeGroups.AttributeGroupName = _row.AttributeGroupName;
                _plm_spGetAttributeGroups.AttributeGroupOrder = _row.AttributeGroupOrder;
                _lplm_spGetAttributeGroups.Add(_plm_spGetAttributeGroups);
            }
            if (_lplm_spGetAttributeGroups.LongCount() != 0)
            {
                return Json(_lplm_spGetAttributeGroups, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getIpp(int EditionId, int ProductId, int DivisionId, int CategoryId, int PharmaFormId)
        {
            var _getIpp = (from _pp in db.ParticipantProducts
                           where _pp.EditionId == EditionId
                           && _pp.ProductId == ProductId
                           && _pp.DivisionId == DivisionId
                           && _pp.CategoryId == CategoryId
                           && _pp.PharmaFormId == PharmaFormId
                           select _pp).ToList();
            ParticipantProducts _ParticipantProducts = new ParticipantProducts();
            List<ParticipantProducts> _lParticipantProducts = new List<ParticipantProducts>();
            foreach (var _row in _getIpp)
            {
                _ParticipantProducts = new ParticipantProducts();
                _ParticipantProducts.HTMLContent = _row.HTMLContent;
                _lParticipantProducts.Add(_ParticipantProducts);
            }
            if (_getIpp.LongCount() != 0)
            {
                return Json(_lParticipantProducts, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getAtributes(int CountryId, int BookId, int EditionId, int DivisionId, int ProductId, int PharmaFormId, int CategoryId)
        {
            List<plm_spGetModifiedAttributeGroupsByProduct_Result> _getModifiedAttributeGroupsByProduct = db.Database.SqlQuery<plm_spGetModifiedAttributeGroupsByProduct_Result>
                ("plm_spGetModifiedAttributeGroupsByProduct"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId
                + ",@CountryId=" + CountryId
                + ",@BookId=" + BookId + "").ToList();
            plm_spGetModifiedAttributeGroupsByProduct_Result _plm_spGetModifiedAttributeGroupsByProduct_Result = new plm_spGetModifiedAttributeGroupsByProduct_Result();
            List<plm_spGetModifiedAttributeGroupsByProduct_Result> _lplm_spGetModifiedAttributeGroupsByProduct_Result = new List<plm_spGetModifiedAttributeGroupsByProduct_Result>();
            List<plm_spGetAttributeGroups_Result> _getAttribute = db.Database.SqlQuery<plm_spGetAttributeGroups_Result>
                ("plm_spGetAttributeGroups"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId + "").ToList();
            foreach (var _row in _getModifiedAttributeGroupsByProduct)
            {
                _plm_spGetModifiedAttributeGroupsByProduct_Result = new plm_spGetModifiedAttributeGroupsByProduct_Result();
                foreach (var _rows in _getAttribute)
                {
                    if (_rows.AttributeGroupName == _row.AttributeGroupName)
                    {
                        _plm_spGetModifiedAttributeGroupsByProduct_Result.check = "true";
                    }
                }
                _plm_spGetModifiedAttributeGroupsByProduct_Result.AttributeGroupId = _row.AttributeGroupId;
                _plm_spGetModifiedAttributeGroupsByProduct_Result.AttributeGroupName = _row.AttributeGroupName;
                _plm_spGetModifiedAttributeGroupsByProduct_Result.ModifiedAttributeGroup = _row.ModifiedAttributeGroup;
                _lplm_spGetModifiedAttributeGroupsByProduct_Result.Add(_plm_spGetModifiedAttributeGroupsByProduct_Result);
            }
            if (_getModifiedAttributeGroupsByProduct.LongCount() != 0)
            {
                return Json(_lplm_spGetModifiedAttributeGroupsByProduct_Result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult saveAttributeGroup(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int AttributeGroupId)
        {
            List<plm_spCRUDModifiedAttributeGroups> _plm_spCRUDModifiedAttributeGroups = db.Database.SqlQuery<plm_spCRUDModifiedAttributeGroups>
                ("EXECUTE dbo.plm_spCRUDModifiedAttributeGroups"
                + " @CRUDType=" + 0
                + ",@attributeGroupId=" + AttributeGroupId
                + ",@editionId=" + EditionId
                + ",@productId=" + ProductId
                + ",@divisionId=" + DivisionId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult removeAttributeGroup(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId, int AttributeGroupId)
        {
            List<plm_spCRUDModifiedAttributeGroups> _plm_spCRUDModifiedAttributeGroups = db.Database.SqlQuery<plm_spCRUDModifiedAttributeGroups>
                ("EXECUTE dbo.plm_spCRUDModifiedAttributeGroups"
                + " @CRUDType=" + 3
                + ",@attributeGroupId=" + AttributeGroupId
                + ",@editionId=" + EditionId
                + ",@productId=" + ProductId
                + ",@divisionId=" + DivisionId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId + "").ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPresentation(int EditionId, int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            List<plm_spGetPresentationsByEditionByProduct> _getPresentations = db.Database.SqlQuery<plm_spGetPresentationsByEditionByProduct>
                ("EXECUTE dbo.plm_spGetPresentationsByEditionByProduct"
                + " @productId=" + ProductId
                + ",@categoryId=" + CategoryId
                + ",@pharmaFormId=" + PharmaFormId
                + ",@divisionId=" + DivisionId
                + ",@editionId=" + EditionId + "").ToList();
            if (_getPresentations.LongCount() != 0)
            {
                plm_spGetPresentationsByEditionByProduct _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                List<plm_spGetPresentationsByEditionByProduct> _l_plm_spGetPresentationsByEditionByProduct = new List<plm_spGetPresentationsByEditionByProduct>();
                foreach (var _row in _getPresentations)
                {
                    _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                    _plm_spGetPresentationsByEditionByProduct.PresentationId = _row.PresentationId;
                    var _getProductImage = (from _p in db.ProductImages
                                            where _p.PresentationId == _row.PresentationId
                                            select _p).ToList();
                    foreach (var _rowImg in _getProductImage)
                    {
                        _plm_spGetPresentationsByEditionByProduct.ProductImageId = _rowImg.ProductImageId;
                        _plm_spGetPresentationsByEditionByProduct.ProductShot = _rowImg.ProductShot;
                        _plm_spGetPresentationsByEditionByProduct.View = "true";
                    }
                    _plm_spGetPresentationsByEditionByProduct.QtyExternalPack = _row.QtyExternalPack;
                    _plm_spGetPresentationsByEditionByProduct.ExternalPackId = _row.ExternalPackId;
                    _plm_spGetPresentationsByEditionByProduct.ExternalPackName = _row.ExternalPackName;
                    _plm_spGetPresentationsByEditionByProduct.QtyInternalPack = _row.QtyInternalPack;
                    _plm_spGetPresentationsByEditionByProduct.InternalPackId = _row.InternalPackId;
                    _plm_spGetPresentationsByEditionByProduct.InternalPackName = _row.InternalPackName;
                    _plm_spGetPresentationsByEditionByProduct.ContentUnitId = _row.ContentUnitId;
                    _plm_spGetPresentationsByEditionByProduct.QtyContentUnit = _row.QtyContentUnit;
                    _plm_spGetPresentationsByEditionByProduct.ContentUnitName = _row.ContentUnitName;
                    _plm_spGetPresentationsByEditionByProduct.QtyWeightUnit = _row.QtyWeightUnit;
                    _plm_spGetPresentationsByEditionByProduct.WeightUnitId = _row.WeightUnitId;
                    _plm_spGetPresentationsByEditionByProduct.WeightUnitName = _row.WeightUnitName;
                    _plm_spGetPresentationsByEditionByProduct.Presentation = _row.Presentation;
                    _plm_spGetPresentationsByEditionByProduct.LongCount = _getPresentations.LongCount();
                    _l_plm_spGetPresentationsByEditionByProduct.Add(_plm_spGetPresentationsByEditionByProduct);
                }
                return Json(_l_plm_spGetPresentationsByEditionByProduct, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getExternalPack()
        {
            var _getElements = (from _ep in db.ExternalPacks
                                orderby _ep.ExternalPackName
                                select _ep).ToList();
            ExternalPacks _ExternalPacks = new ExternalPacks();
            List<ExternalPacks> _l_ExternalPacks = new List<ExternalPacks>();
            foreach (var _row in _getElements)
            {
                _ExternalPacks = new ExternalPacks();
                _ExternalPacks.ExternalPackId = _row.ExternalPackId;
                _ExternalPacks.ExternalPackName = _row.ExternalPackName;
                _l_ExternalPacks.Add(_ExternalPacks);
            }
            return Json(_l_ExternalPacks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getInternalPack()
        {
            var _getElements = (from _ip in db.InternalPacks
                                orderby _ip.InternalPackName
                                select _ip).ToList();
            InternalPacks _InternalPacks = new InternalPacks();
            List<InternalPacks> _l_InternalPacks = new List<InternalPacks>();
            foreach (var _row in _getElements)
            {
                _InternalPacks = new InternalPacks();
                _InternalPacks.InternalPackId = _row.InternalPackId;
                _InternalPacks.InternalPackName = _row.InternalPackName;
                _l_InternalPacks.Add(_InternalPacks);
            }
            return Json(_l_InternalPacks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getContentUnits()
        {
            var _getElements = (from _cu in db.ContentUnits
                                orderby _cu.UnitName
                                select _cu).ToList();
            ContentUnits _ContentUnits = new ContentUnits();
            List<ContentUnits> _l_ContentUnits = new List<ContentUnits>();
            foreach (var _row in _getElements)
            {
                _ContentUnits = new ContentUnits();
                _ContentUnits.ContentUnitId = _row.ContentUnitId;
                _ContentUnits.UnitName = _row.UnitName;
                _l_ContentUnits.Add(_ContentUnits);
            }
            return Json(_l_ContentUnits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getWeightUnits()
        {
            var _getElements = (from _wu in db.WeightUnits
                                orderby _wu.UnitName
                                select _wu).ToList();
            WeightUnits _WeightUnits = new WeightUnits();
            List<WeightUnits> _l_WeightUnits = new List<WeightUnits>();
            foreach (var _row in _getElements)
            {
                _WeightUnits = new WeightUnits();
                _WeightUnits.WeightUnitId = _row.WeightUnitId;
                _WeightUnits.UnitName = _row.UnitName;
                _l_WeightUnits.Add(_WeightUnits);
            }
            return Json(_l_WeightUnits, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddPresentation()
        {
            string[] _editionId = Request.Form.GetValues("EditionId");
            string[] _divisionId = Request.Form.GetValues("DivisionId");
            string[] _categoryId = Request.Form.GetValues("CategoryId");
            string[] _productId = Request.Form.GetValues("ProductId");
            string[] _pharmaFormId = Request.Form.GetValues("PharmaFormId");
            string[] _qtyExternalPack = Request.Form.GetValues("QtyExternalPack");
            string[] _externalPackId = Request.Form.GetValues("ExternalPackId");
            string[] _qtyInternalPack = Request.Form.GetValues("QtyInternalPack");
            string[] _internalPackId = Request.Form.GetValues("InternalPackId");
            string[] _qtyContentUnit = Request.Form.GetValues("QtyContentUnit");
            string[] _contentUnitId = Request.Form.GetValues("ContentUnitId");
            string[] _qtyWeightUnit = Request.Form.GetValues("QtyWeightUnit");
            string[] _weightUnitId = Request.Form.GetValues("WeightUnitId");
            string EditionId = String.Join(",", _editionId);
            int _EditionId = int.Parse(EditionId);
            string DivisionId = String.Join(",", _divisionId);
            int _DivisionId = int.Parse(DivisionId);
            string CategoryId = String.Join(",", _categoryId);
            int _CategoryId = int.Parse(CategoryId);
            string ProductId = String.Join(",", _productId);
            int _ProductId = int.Parse(ProductId);
            string PharmaFormId = String.Join(",", _pharmaFormId);
            int _PharmaFormId = int.Parse(PharmaFormId);
            string QtyExternalPack = String.Join(",", _qtyExternalPack);
            int? _QtyExternalPack = int.Parse(QtyExternalPack);
            string ExternalPackId = String.Join(",", _externalPackId);
            int? _ExternalPackId = int.Parse(ExternalPackId);
            string QtyInternalPack = String.Join(",", _qtyInternalPack);
            int? _QtyInternalPack = int.Parse(QtyInternalPack);
            string InternalPackId = String.Join(",", _internalPackId);
            int? _InternalPackId = int.Parse(InternalPackId);
            string QtyContentUnit = String.Join(",", _qtyContentUnit);
            int? _QtyContentUnit = int.Parse(QtyContentUnit);
            string ContentUnitId = String.Join(",", _contentUnitId);
            int? _ContentUnitId = int.Parse(ContentUnitId);
            string QtyWeightUnit = String.Join(",", _qtyWeightUnit);
            int? _QtyWeightUnit = int.Parse(QtyWeightUnit);
            string WeightUnitId = String.Join(",", _weightUnitId);
            int? _WeightUnitId = int.Parse(WeightUnitId);
            if (_QtyExternalPack == 0)
            {
                _QtyExternalPack = null;
            }
            if (_ExternalPackId == 0)
            {
                _ExternalPackId = null;
            }
            if (_QtyInternalPack == 0)
            {
                _QtyInternalPack = null;
            }
            if (_InternalPackId == 0)
            {
                _InternalPackId = null;
            }
            if (_QtyContentUnit == 0)
            {
                _QtyContentUnit = null;
            }
            if (_ContentUnitId == 0)
            {
                _ContentUnitId = null;
            }
            if (_QtyWeightUnit == 0)
            {
                _QtyWeightUnit = null;
            }
            if (_WeightUnitId == 0)
            {
                _WeightUnitId = null;
            }
            Presentations _Presentations = new Presentations();
            _Presentations.DivisionId = _DivisionId;
            _Presentations.CategoryId = _CategoryId;
            _Presentations.ProductId = _ProductId;
            _Presentations.PharmaFormId = _PharmaFormId;
            _Presentations.QtyExternalPack = _QtyExternalPack;
            _Presentations.ExternalPackId = _ExternalPackId;
            _Presentations.QtyInternalPack = _QtyInternalPack;
            _Presentations.InternalPackId = _InternalPackId;
            _Presentations.QtyContentUnit = _QtyContentUnit.ToString();
            _Presentations.ContentUnitId = _ContentUnitId;
            _Presentations.QtyWeightUnit = _QtyWeightUnit.ToString();
            _Presentations.WeightUnitId = _WeightUnitId;
            var _itemExternalPack = "";
            if (_Presentations.ExternalPackId != null)
            {
                var _getExternalPack = (from _ex in db.ExternalPacks
                                        where _ex.ExternalPackId == _Presentations.ExternalPackId
                                        select _ex).ToList();
                foreach (var _row in _getExternalPack)
                {
                    _itemExternalPack = _row.ExternalPackName + ",";
                }
            }
            var _itemInternalPack = "";
            if (_Presentations.InternalPackId != null)
            {
                var _getInternalPack = (from _in in db.InternalPacks
                                        where _in.InternalPackId == _Presentations.InternalPackId
                                        select _in).ToList();
                foreach (var _row in _getInternalPack)
                {
                    _itemInternalPack = _row.InternalPackName + ",";
                }
            }
            var _itemContentUnit = "";
            if (_Presentations.ContentUnitId != null)
            {
                var _getContentUnit = (from _cu in db.ContentUnits
                                       where _cu.ContentUnitId == _Presentations.ContentUnitId
                                       select _cu).ToList();
                foreach (var _row in _getContentUnit)
                {
                    _itemContentUnit = _row.UnitName + ",";
                }
            }
            var _itemWeightUnit = "";
            if (_Presentations.WeightUnitId != null)
            {
                var _getWeightUnit = (from _w in db.WeightUnits
                                      where _w.WeightUnitId == _Presentations.WeightUnitId
                                      select _w).ToList();
                foreach (var _row in _getWeightUnit)
                {
                    _itemWeightUnit = _row.UnitName;
                }
            }
            string _contentPresentation = "";
            _contentPresentation = _Presentations.QtyExternalPack.ToString().Trim()
                + " " +
                _itemExternalPack.Trim()
                + " " +
                _Presentations.QtyInternalPack.ToString().Trim()
                + " " +
                _itemInternalPack.Trim()
                + " " +
                _Presentations.QtyContentUnit.Trim()
                + " " +
                _itemContentUnit.Trim()
                + " " +
                _Presentations.QtyWeightUnit.Trim()
                + " " +
                _itemWeightUnit.Trim();
            _Presentations.Presentation = _contentPresentation.Trim();
            _Presentations.Active = true;
            if (_Presentations.QtyContentUnit == "")
            {
                _Presentations.QtyContentUnit = null;
            }
            if (_Presentations.QtyWeightUnit == "")
            {
                _Presentations.QtyWeightUnit = null;
            }
            db.Entry(_Presentations).State = EntityState.Added;
            db.SaveChanges();
            var _getJson = (from _p in db.Presentations
                            where _p.PresentationId == _Presentations.PresentationId
                            select _p).ToList();
            foreach (var _row in _getJson)
            {
                _row.JSONFormat = "{" + "PresentationId:" + _row.PresentationId + "," + "Presentation:" + "" + _row.Presentation + "" + "}";
                db.SaveChanges();
            }
            if (Request.Files.Count != 0)
            {
                var _image400px = Request.Files.Get("File400");
                var _image384px = Request.Files.Get("File384");
                var _image320px = Request.Files.Get("File320");
                var _onlyFileName = "";
                if (_image320px != null)
                {
                    _onlyFileName = _image320px.FileName;
                }
                if (_onlyFileName == "")
                {
                    if (_image384px != null)
                    {
                        _onlyFileName = _image384px.FileName;
                    }
                }
                if (_onlyFileName == "")
                {
                    if (_image400px != null)
                    {
                        _onlyFileName = _image400px.FileName;
                    }
                }
                ProductImageSizes _wProductImageSizes = new ProductImageSizes();
                List<ProductImageSizes> _l_ProductImageSizes = new List<ProductImageSizes>();
                if (_image400px != null)
                {
                    var _path400px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _onlyFileName);
                    _image400px.SaveAs(_path400px);
                    var _size400px = (from _z in db.ImageSizes
                                      where _z.Size == "400 x 400"
                                      select _z).ToList();
                    foreach (var _row in _size400px)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                if (_image384px != null)
                {
                    var _path384px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _onlyFileName);
                    _image384px.SaveAs(_path384px);
                    var _size384 = (from _z in db.ImageSizes
                                    where _z.Size == "384 x 512"
                                    select _z).ToList();
                    foreach (var _row in _size384)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                if (_image320px != null)
                {
                    var _path320px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _onlyFileName);
                    _image320px.SaveAs(_path320px);
                    var _size320 = (from _z in db.ImageSizes
                                    where _z.Size == "320 x 480"
                                    select _z).ToList();
                    foreach (var _row in _size320)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                ProductImages _ProductImages = new ProductImages();
                _ProductImages.ProductShot = _onlyFileName;
                DateTime myDateTime = DateTime.Now;
                string dateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.DateTime sqlFormattedDate = System.DateTime.Parse(dateTime);
                _ProductImages.LastUpdate = sqlFormattedDate;
                _ProductImages.BaseURL = "";
                _ProductImages.Active = true;
                _ProductImages.PresentationId = _Presentations.PresentationId;
                db.Entry(_ProductImages).State = EntityState.Added;
                db.SaveChanges();
                foreach (var _row in _l_ProductImageSizes)
                {
                    ProductImageSizes _ProductImageSizes = new ProductImageSizes();
                    _ProductImageSizes.ImageSizeId = _row.ImageSizeId;
                    _ProductImageSizes.ProductImageId = _ProductImages.ProductImageId;
                    db.Entry(_ProductImageSizes).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            EditionPresentations _EditionPresentations = new EditionPresentations();
            _EditionPresentations.EditionId = _EditionId;
            _EditionPresentations.PresentationId = _Presentations.PresentationId;
            db.Entry(_EditionPresentations).State = EntityState.Added;
            db.SaveChanges();
            List<plm_spGetPresentationsByEditionByProduct> _getPresentations = db.Database.SqlQuery<plm_spGetPresentationsByEditionByProduct>
                ("EXECUTE dbo.plm_spGetPresentationsByEditionByProduct"
                + " @productId=" + _ProductId
                + ",@categoryId=" + _CategoryId
                + ",@pharmaFormId=" + _PharmaFormId
                + ",@divisionId=" + _DivisionId
                + ",@editionId=" + _EditionId + "").ToList();
            var getResults = _getPresentations.Where(model => model.PresentationId == _Presentations.PresentationId).ToList();
            plm_spGetPresentationsByEditionByProduct _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
            List<plm_spGetPresentationsByEditionByProduct> _l_plm_spGetPresentationsByEditionByProduct = new List<plm_spGetPresentationsByEditionByProduct>();
            foreach (var _row in getResults)
            {
                _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                _plm_spGetPresentationsByEditionByProduct.PresentationId = _row.PresentationId;
                var _getProductImage = (from _p in db.ProductImages
                                        where _p.PresentationId == _row.PresentationId
                                        select _p).ToList();
                foreach (var _rowImg in _getProductImage)
                {
                    _plm_spGetPresentationsByEditionByProduct.ProductImageId = _rowImg.ProductImageId;
                    _plm_spGetPresentationsByEditionByProduct.ProductShot = _rowImg.ProductShot;
                    _plm_spGetPresentationsByEditionByProduct.View = "true";
                }
                _plm_spGetPresentationsByEditionByProduct.QtyExternalPack = _row.QtyExternalPack;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackId = _row.ExternalPackId;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackName = _row.ExternalPackName;
                _plm_spGetPresentationsByEditionByProduct.QtyInternalPack = _row.QtyInternalPack;
                _plm_spGetPresentationsByEditionByProduct.InternalPackId = _row.InternalPackId;
                _plm_spGetPresentationsByEditionByProduct.InternalPackName = _row.InternalPackName;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitId = _row.ContentUnitId;
                _plm_spGetPresentationsByEditionByProduct.QtyContentUnit = _row.QtyContentUnit;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitName = _row.ContentUnitName;
                _plm_spGetPresentationsByEditionByProduct.QtyWeightUnit = _row.QtyWeightUnit;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitId = _row.WeightUnitId;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitName = _row.WeightUnitName;
                _plm_spGetPresentationsByEditionByProduct.Presentation = _row.Presentation;
                _plm_spGetPresentationsByEditionByProduct.LongCount = _getPresentations.LongCount();
                _l_plm_spGetPresentationsByEditionByProduct.Add(_plm_spGetPresentationsByEditionByProduct);
            }
            return Json(_l_plm_spGetPresentationsByEditionByProduct, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUpdateExternalPack(int ExternalPackId)
        {
            var _getElements = (from _ep in db.ExternalPacks
                                orderby _ep.ExternalPackName
                                select _ep).ToList();
            ExternalPacks _ExternalPacks = new ExternalPacks();
            List<ExternalPacks> _l_ExternalPacks = new List<ExternalPacks>();
            foreach (var _row in _getElements)
            {
                _ExternalPacks = new ExternalPacks();
                if (_row.ExternalPackId == ExternalPackId)
                {
                    //_ExternalPacks.True = "True";
                }
                _ExternalPacks.ExternalPackId = _row.ExternalPackId;
                _ExternalPacks.ExternalPackName = _row.ExternalPackName;
                _l_ExternalPacks.Add(_ExternalPacks);
            }
            return Json(_l_ExternalPacks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUpdateInternalPack(int InternalPackId)
        {
            var _getElements = (from _ip in db.InternalPacks
                                orderby _ip.InternalPackName
                                select _ip).ToList();
            InternalPacks _InternalPacks = new InternalPacks();
            List<InternalPacks> _l_InternalPacks = new List<InternalPacks>();
            foreach (var _row in _getElements)
            {
                _InternalPacks = new InternalPacks();
                if (_row.InternalPackId == InternalPackId)
                {
                    //_InternalPacks.True = "True";
                }
                _InternalPacks.InternalPackId = _row.InternalPackId;
                _InternalPacks.InternalPackName = _row.InternalPackName;
                _l_InternalPacks.Add(_InternalPacks);
            }
            return Json(_l_InternalPacks, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUpdateContentUnits(int ContentUnitId)
        {
            var _getElements = (from _cu in db.ContentUnits
                                orderby _cu.UnitName
                                select _cu).ToList();
            ContentUnits _ContentUnits = new ContentUnits();
            List<ContentUnits> _l_ContentUnits = new List<ContentUnits>();
            foreach (var _row in _getElements)
            {
                _ContentUnits = new ContentUnits();
                if (_row.ContentUnitId == ContentUnitId)
                {
                    //_ContentUnits.True = "True";
                }
                _ContentUnits.ContentUnitId = _row.ContentUnitId;
                _ContentUnits.UnitName = _row.UnitName;
                _l_ContentUnits.Add(_ContentUnits);
            }
            return Json(_l_ContentUnits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUpdateWeightUnits(int WeightUnitId)
        {
            var _getElements = (from _wu in db.WeightUnits
                                orderby _wu.UnitName
                                select _wu).ToList();
            WeightUnits _WeightUnits = new WeightUnits();
            List<WeightUnits> _l_WeightUnits = new List<WeightUnits>();
            foreach (var _row in _getElements)
            {
                _WeightUnits = new WeightUnits();
                if (_row.WeightUnitId == WeightUnitId)
                {
                    //_WeightUnits.True = "True";
                }
                _WeightUnits.WeightUnitId = _row.WeightUnitId;
                _WeightUnits.UnitName = _row.UnitName;
                _l_WeightUnits.Add(_WeightUnits);
            }
            return Json(_l_WeightUnits, JsonRequestBehavior.AllowGet);
        }
        public JsonResult oneProductImages(int ProductImageId)
        {
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            var _getElement = (from _productImages in db.ProductImages
                               where _productImages.ProductImageId == ProductImageId
                               select _productImages).ToList();
            if (_getElement.LongCount() != 0)
            {
                foreach (var _row in _getElement)
                {
                    _ProductImagesInfo = new ProductImagesInfo();
                    _ProductImagesInfo.ProductImageId = _row.ProductImageId;
                    _ProductImagesInfo.ProductImageName = _row.ProductShot;
                    var _getImageSize = (from _productImagesSizes in db.ProductImageSizes
                                         join _imagesSizes in db.ImageSizes
                                             on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                         where _productImagesSizes.ProductImageId == _row.ProductImageId
                                         && _imagesSizes.Size == "320 x 480"
                                         select new { _productImagesSizes, _imagesSizes }).ToList();
                    if (_getImageSize.LongCount() != 0)
                    {
                        foreach (var _rowSizes in _getImageSize)
                        {
                            _ProductImagesInfo.ImageSizeId = _rowSizes._productImagesSizes.ImageSizeId;
                            var _path320px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _row.ProductShot);
                            if (System.IO.File.Exists(_path320px))
                            {
                                _ProductImagesInfo.Exist = "Exist";
                            }
                            else
                            {
                                _ProductImagesInfo.Exist = "NotFiles";
                            }
                        }
                    }
                    else
                    {
                        _ProductImagesInfo.Exist = "NotExist";
                    }
                }
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            else
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.Exist = "NotExist";
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public JsonResult updatePresentation(int PresentationId, int EditionId, int DivisionId, int CategoryId, int ProductId, int PharmaFormId,
                                             string QtyExternalPack, int? ExternalPackId, int? QtyInternalPack, int? InternalPackId, string QtyContentUnit,
                                             int? ContentUnitId, string QtyWeightUnit, int? WeightUnitId)
        {
            if (QtyExternalPack == "0")
            {
                QtyExternalPack = "";
            }
            if (ExternalPackId == 0)
            {
                ExternalPackId = null;
            }
            if (QtyInternalPack == 0)
            {
                QtyInternalPack = null;
            }
            if (InternalPackId == 0)
            {
                InternalPackId = null;
            }
            if (QtyContentUnit == "0")
            {
                QtyContentUnit = "";
            }
            if (ContentUnitId == 0)
            {
                ContentUnitId = null;
            }
            if (QtyWeightUnit == "0")
            {
                QtyWeightUnit = "";
            }
            if (WeightUnitId == 0)
            {
                WeightUnitId = null;
            }
            var _getElements = (from _presentation in db.Presentations
                                where _presentation.PresentationId == PresentationId
                                && _presentation.DivisionId == DivisionId
                                && _presentation.CategoryId == CategoryId
                                && _presentation.ProductId == ProductId
                                && _presentation.PharmaFormId == PharmaFormId
                                select _presentation).ToList();
            var _itemExternalPack = "";
            if (ExternalPackId != null)
            {
                var _getExternalPack = (from _ex in db.ExternalPacks
                                        where _ex.ExternalPackId == ExternalPackId
                                        select _ex).ToList();
                foreach (var _row in _getExternalPack)
                {
                    _itemExternalPack = _row.ExternalPackName + ",";
                }
            }
            var _itemInternalPack = "";
            if (InternalPackId != null)
            {
                var _getInternalPack = (from _in in db.InternalPacks
                                        where _in.InternalPackId == InternalPackId
                                        select _in).ToList();
                foreach (var _row in _getInternalPack)
                {
                    _itemInternalPack = _row.InternalPackName + ",";
                }
            }
            var _itemContentUnit = "";
            if (ContentUnitId != null)
            {
                var _getContentUnit = (from _cu in db.ContentUnits
                                       where _cu.ContentUnitId == ContentUnitId
                                       select _cu).ToList();
                foreach (var _row in _getContentUnit)
                {
                    _itemContentUnit = _row.UnitName + ",";
                }
            }
            var _itemWeightUnit = "";
            if (WeightUnitId != null)
            {
                var _getWeightUnit = (from _w in db.WeightUnits
                                      where _w.WeightUnitId == WeightUnitId
                                      select _w).ToList();
                foreach (var _row in _getWeightUnit)
                {
                    _itemWeightUnit = _row.UnitName;
                }
            }
            string _contentPresentation = "";
            _contentPresentation = QtyExternalPack.ToString().Trim()
                + " " +
                _itemExternalPack.Trim()
                + " " +
                QtyInternalPack.ToString().Trim()
                + " " +
                _itemInternalPack.Trim()
                + " " +
                QtyContentUnit.Trim()
                + " " +
                _itemContentUnit.Trim()
                + " " +
                QtyWeightUnit.Trim()
                + " " +
                _itemWeightUnit.Trim();
            foreach (var _update in _getElements)
            {
                if (QtyExternalPack == "")
                {
                    QtyExternalPack = "0";
                }
                int? _QtyExternalPack = int.Parse(QtyExternalPack);
                if (_QtyExternalPack == 0)
                {
                    _QtyExternalPack = null;
                }
                _update.QtyExternalPack = _QtyExternalPack;
                _update.ExternalPackId = ExternalPackId;
                _update.QtyInternalPack = QtyInternalPack;
                _update.InternalPackId = InternalPackId;
                _update.QtyContentUnit = QtyContentUnit;
                _update.ContentUnitId = ContentUnitId;
                _update.QtyWeightUnit = QtyWeightUnit;
                _update.WeightUnitId = WeightUnitId;
                _update.Presentation = _contentPresentation;
                _update.JSONFormat = "{" + "PresentationId:" + _update.PresentationId + "," + "Presentation:" + "" + _contentPresentation + "" + "}";
                db.SaveChanges();
            }
            List<plm_spGetPresentationsByEditionByProduct> _getPresentations = db.Database.SqlQuery<plm_spGetPresentationsByEditionByProduct>
                            ("EXECUTE dbo.plm_spGetPresentationsByEditionByProduct"
                            + " @productId=" + ProductId
                            + ",@categoryId=" + CategoryId
                            + ",@pharmaFormId=" + PharmaFormId
                            + ",@divisionId=" + DivisionId
                            + ",@editionId=" + EditionId + "").ToList();
            plm_spGetPresentationsByEditionByProduct _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
            List<plm_spGetPresentationsByEditionByProduct> _l_plm_spGetPresentationsByEditionByProduct = new List<plm_spGetPresentationsByEditionByProduct>();
            foreach (var _row in _getPresentations)
            {
                _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                _plm_spGetPresentationsByEditionByProduct.PresentationId = _row.PresentationId;
                var _getProductImage = (from _p in db.ProductImages
                                        where _p.PresentationId == _row.PresentationId
                                        select _p).ToList();
                foreach (var _rowImg in _getProductImage)
                {
                    _plm_spGetPresentationsByEditionByProduct.ProductImageId = _rowImg.ProductImageId;
                    _plm_spGetPresentationsByEditionByProduct.ProductShot = _rowImg.ProductShot;
                    _plm_spGetPresentationsByEditionByProduct.View = "true";
                }
                _plm_spGetPresentationsByEditionByProduct.QtyExternalPack = _row.QtyExternalPack;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackId = _row.ExternalPackId;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackName = _row.ExternalPackName;
                _plm_spGetPresentationsByEditionByProduct.QtyInternalPack = _row.QtyInternalPack;
                _plm_spGetPresentationsByEditionByProduct.InternalPackId = _row.InternalPackId;
                _plm_spGetPresentationsByEditionByProduct.InternalPackName = _row.InternalPackName;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitId = _row.ContentUnitId;
                _plm_spGetPresentationsByEditionByProduct.QtyContentUnit = _row.QtyContentUnit;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitName = _row.ContentUnitName;
                _plm_spGetPresentationsByEditionByProduct.QtyWeightUnit = _row.QtyWeightUnit;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitId = _row.WeightUnitId;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitName = _row.WeightUnitName;
                _plm_spGetPresentationsByEditionByProduct.Presentation = _row.Presentation;
                _plm_spGetPresentationsByEditionByProduct.LongCount = _getPresentations.LongCount();
                _l_plm_spGetPresentationsByEditionByProduct.Add(_plm_spGetPresentationsByEditionByProduct);
            }
            return Json(_l_plm_spGetPresentationsByEditionByProduct, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddPresentationImages()
        {
            string[] _editionId = Request.Form.GetValues("EditionId");
            string[] _divisionId = Request.Form.GetValues("DivisionId");
            string[] _categoryId = Request.Form.GetValues("CategoryId");
            string[] _productId = Request.Form.GetValues("ProductId");
            string[] _pharmaFormId = Request.Form.GetValues("PharmaFormId");
            string[] _presentationId = Request.Form.GetValues("PresentationId");
            string EditionId = String.Join(",", _editionId);
            int _EditionId = int.Parse(EditionId);
            string DivisionId = String.Join(",", _divisionId);
            int _DivisionId = int.Parse(DivisionId);
            string CategoryId = String.Join(",", _categoryId);
            int _CategoryId = int.Parse(CategoryId);
            string ProductId = String.Join(",", _productId);
            int _ProductId = int.Parse(ProductId);
            string PharmaFormId = String.Join(",", _pharmaFormId);
            int _PharmaFormId = int.Parse(PharmaFormId);
            string PresentationId = String.Join(",", _presentationId);
            int _PresentationId = int.Parse(PresentationId);
            if (Request.Files.Count != 0)
            {
                var _image400px = Request.Files.Get("_ThreeFile");
                var _image384px = Request.Files.Get("_twOfile");
                var _image320px = Request.Files.Get("_oneFile");
                var _onlyFileName = "";
                if (_image320px != null)
                {
                    _onlyFileName = _image320px.FileName;
                }
                if (_onlyFileName == "")
                {
                    if (_image384px != null)
                    {
                        _onlyFileName = _image384px.FileName;
                    }
                }
                if (_onlyFileName == "")
                {
                    if (_image400px != null)
                    {
                        _onlyFileName = _image400px.FileName;
                    }
                }
                ProductImageSizes _wProductImageSizes = new ProductImageSizes();
                List<ProductImageSizes> _l_ProductImageSizes = new List<ProductImageSizes>();
                if (_image400px != null)
                {
                    var _path400px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _onlyFileName);
                    _image400px.SaveAs(_path400px);
                    var _size400px = (from _z in db.ImageSizes
                                      where _z.Size == "400 x 400"
                                      select _z).ToList();
                    foreach (var _row in _size400px)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                if (_image384px != null)
                {
                    var _path384px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _onlyFileName);
                    _image384px.SaveAs(_path384px);
                    var _size384 = (from _z in db.ImageSizes
                                    where _z.Size == "384 x 512"
                                    select _z).ToList();
                    foreach (var _row in _size384)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                if (_image320px != null)
                {
                    var _path320px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _onlyFileName);
                    _image320px.SaveAs(_path320px);
                    var _size320 = (from _z in db.ImageSizes
                                    where _z.Size == "320 x 480"
                                    select _z).ToList();
                    foreach (var _row in _size320)
                    {
                        _wProductImageSizes = new ProductImageSizes();
                        _wProductImageSizes.ImageSizeId = _row.ImageSizeId;
                        _l_ProductImageSizes.Add(_wProductImageSizes);
                    }
                }
                ProductImages _ProductImages = new ProductImages();
                _ProductImages.ProductShot = _onlyFileName;
                DateTime myDateTime = DateTime.Now;
                string dateTime = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                System.DateTime sqlFormattedDate = System.DateTime.Parse(dateTime);
                _ProductImages.LastUpdate = sqlFormattedDate;
                _ProductImages.BaseURL = "";
                _ProductImages.Active = true;
                _ProductImages.PresentationId = _PresentationId;
                db.Entry(_ProductImages).State = EntityState.Added;
                db.SaveChanges();
                foreach (var _row in _l_ProductImageSizes)
                {
                    ProductImageSizes _ProductImageSizes = new ProductImageSizes();
                    _ProductImageSizes.ImageSizeId = _row.ImageSizeId;
                    _ProductImageSizes.ProductImageId = _ProductImages.ProductImageId;
                    db.Entry(_ProductImageSizes).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            List<plm_spGetPresentationsByEditionByProduct> _getPresentations = db.Database.SqlQuery<plm_spGetPresentationsByEditionByProduct>
                            ("EXECUTE dbo.plm_spGetPresentationsByEditionByProduct"
                            + " @productId=" + _ProductId
                            + ",@categoryId=" + _CategoryId
                            + ",@pharmaFormId=" + _PharmaFormId
                            + ",@divisionId=" + _DivisionId
                            + ",@editionId=" + _EditionId + "").ToList();
            plm_spGetPresentationsByEditionByProduct _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
            List<plm_spGetPresentationsByEditionByProduct> _l_plm_spGetPresentationsByEditionByProduct = new List<plm_spGetPresentationsByEditionByProduct>();
            foreach (var _row in _getPresentations)
            {
                _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                _plm_spGetPresentationsByEditionByProduct.PresentationId = _row.PresentationId;
                var _getProductImage = (from _p in db.ProductImages
                                        where _p.PresentationId == _row.PresentationId
                                        select _p).ToList();
                foreach (var _rowImg in _getProductImage)
                {
                    _plm_spGetPresentationsByEditionByProduct.ProductImageId = _rowImg.ProductImageId;
                    _plm_spGetPresentationsByEditionByProduct.ProductShot = _rowImg.ProductShot;
                    _plm_spGetPresentationsByEditionByProduct.View = "true";
                }
                _plm_spGetPresentationsByEditionByProduct.QtyExternalPack = _row.QtyExternalPack;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackId = _row.ExternalPackId;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackName = _row.ExternalPackName;
                _plm_spGetPresentationsByEditionByProduct.QtyInternalPack = _row.QtyInternalPack;
                _plm_spGetPresentationsByEditionByProduct.InternalPackId = _row.InternalPackId;
                _plm_spGetPresentationsByEditionByProduct.InternalPackName = _row.InternalPackName;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitId = _row.ContentUnitId;
                _plm_spGetPresentationsByEditionByProduct.QtyContentUnit = _row.QtyContentUnit;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitName = _row.ContentUnitName;
                _plm_spGetPresentationsByEditionByProduct.QtyWeightUnit = _row.QtyWeightUnit;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitId = _row.WeightUnitId;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitName = _row.WeightUnitName;
                _plm_spGetPresentationsByEditionByProduct.Presentation = _row.Presentation;
                _plm_spGetPresentationsByEditionByProduct.LongCount = _getPresentations.LongCount();
                _l_plm_spGetPresentationsByEditionByProduct.Add(_plm_spGetPresentationsByEditionByProduct);
            }
            return Json(_l_plm_spGetPresentationsByEditionByProduct, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deletePresentationImage(int EditionId, int DivisionId, int ProductId, int CategoryId, int PharmaFormId, int PresentationId, int ProductImageId)
        {
            var _contentProductImageSizes = (from _p in db.Presentations
                                             join _pi in db.ProductImages
                                             on _p.PresentationId equals _pi.PresentationId
                                             join _pz in db.ProductImageSizes
                                             on _pi.ProductImageId equals _pz.ProductImageId
                                             where _p.PresentationId == PresentationId
                                             select new { _pz, _pi }).ToList();
            foreach (var _row in _contentProductImageSizes)
            {
                var imageName = "";
                imageName = _row._pi.ProductShot;
                if (_row._pz.ImageSizes.Size == "400 x 400")
                {
                    var _path400px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), imageName);
                    if (System.IO.File.Exists(_path400px))
                    {
                        System.IO.File.Delete(_path400px);
                    }
                }
                if (_row._pz.ImageSizes.Size == "384 x 512")
                {
                    var _path384px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), imageName);
                    if (System.IO.File.Exists(_path384px))
                    {
                        System.IO.File.Delete(_path384px);
                    }
                }
                if (_row._pz.ImageSizes.Size == "320 x 480")
                {
                    var _path320px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), imageName);
                    if (System.IO.File.Exists(_path320px))
                    {
                        System.IO.File.Delete(_path320px);
                    }
                }
                var _productImagesSizes = db.ProductImageSizes.Where(model => model.ProductImageId == _row._pz.ProductImageId && model.ImageSizeId == _row._pz.ImageSizeId).ToList();
                foreach (var _remove in _productImagesSizes)
                {
                    db.ProductImageSizes.Remove(_remove);
                    db.SaveChanges();
                }
            }
            var _productImages = db.ProductImages.Where(model => model.PresentationId == PresentationId).ToList();
            foreach (var _remove in _productImages)
            {
                db.ProductImages.Remove(_remove);
                db.SaveChanges();
            }
            List<plm_spGetPresentationsByEditionByProduct> _getPresentations = db.Database.SqlQuery<plm_spGetPresentationsByEditionByProduct>
                            ("EXECUTE dbo.plm_spGetPresentationsByEditionByProduct"
                            + " @productId=" + ProductId
                            + ",@categoryId=" + CategoryId
                            + ",@pharmaFormId=" + PharmaFormId
                            + ",@divisionId=" + DivisionId
                            + ",@editionId=" + EditionId + "").ToList();
            plm_spGetPresentationsByEditionByProduct _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
            List<plm_spGetPresentationsByEditionByProduct> _l_plm_spGetPresentationsByEditionByProduct = new List<plm_spGetPresentationsByEditionByProduct>();
            foreach (var _row in _getPresentations)
            {
                _plm_spGetPresentationsByEditionByProduct = new plm_spGetPresentationsByEditionByProduct();
                _plm_spGetPresentationsByEditionByProduct.PresentationId = _row.PresentationId;
                var _getProductImage = (from _p in db.ProductImages
                                        where _p.PresentationId == _row.PresentationId
                                        select _p).ToList();
                foreach (var _rowImg in _getProductImage)
                {
                    _plm_spGetPresentationsByEditionByProduct.ProductImageId = _rowImg.ProductImageId;
                    _plm_spGetPresentationsByEditionByProduct.ProductShot = _rowImg.ProductShot;
                    _plm_spGetPresentationsByEditionByProduct.View = "true";
                }
                _plm_spGetPresentationsByEditionByProduct.QtyExternalPack = _row.QtyExternalPack;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackId = _row.ExternalPackId;
                _plm_spGetPresentationsByEditionByProduct.ExternalPackName = _row.ExternalPackName;
                _plm_spGetPresentationsByEditionByProduct.QtyInternalPack = _row.QtyInternalPack;
                _plm_spGetPresentationsByEditionByProduct.InternalPackId = _row.InternalPackId;
                _plm_spGetPresentationsByEditionByProduct.InternalPackName = _row.InternalPackName;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitId = _row.ContentUnitId;
                _plm_spGetPresentationsByEditionByProduct.QtyContentUnit = _row.QtyContentUnit;
                _plm_spGetPresentationsByEditionByProduct.ContentUnitName = _row.ContentUnitName;
                _plm_spGetPresentationsByEditionByProduct.QtyWeightUnit = _row.QtyWeightUnit;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitId = _row.WeightUnitId;
                _plm_spGetPresentationsByEditionByProduct.WeightUnitName = _row.WeightUnitName;
                _plm_spGetPresentationsByEditionByProduct.Presentation = _row.Presentation;
                _plm_spGetPresentationsByEditionByProduct.LongCount = _getPresentations.LongCount();
                _l_plm_spGetPresentationsByEditionByProduct.Add(_plm_spGetPresentationsByEditionByProduct);
            }
            return Json(_l_plm_spGetPresentationsByEditionByProduct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult saveOneProductImage()
        {
            var _oneFile = Request.Files[0];
            string[] _productImageId = Request.Form.GetValues("ProductImageId");
            string ProductImageId = String.Join(",", _productImageId);
            int _ProductImageId = int.Parse(ProductImageId);
            // You get the path in which the image is to be saved
            var _path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _oneFile.FileName);
            // Save the image
            _oneFile.SaveAs(_path);
            var _updateProductImages = (from _productImages in db.ProductImages
                                        where _productImages.ProductImageId == _ProductImageId
                                        select _productImages).ToList();
            foreach (var _update in _updateProductImages)
            {
                // Rename 384 x 512 image if it exists in the repository
                var _sourceImgFile384x512 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _update.ProductShot);
                var _destinationImgFile384x512 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _oneFile.FileName);
                if (System.IO.File.Exists(_sourceImgFile384x512))
                {
                    // Rename file
                    System.IO.File.Move(_sourceImgFile384x512, _destinationImgFile384x512);
                }
                // Rename 400 x 400 image if it exists in the repository
                var _sourceImgFile400x400 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _update.ProductShot);
                var _destinationImgFile400x400 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _oneFile.FileName);
                if (System.IO.File.Exists(_sourceImgFile400x400))
                {
                    // Rename file
                    System.IO.File.Move(_sourceImgFile400x400, _destinationImgFile400x400);
                }
                // If the previous image exists it is deleted to replace it
                if (_update.ProductShot.Trim() != _oneFile.FileName.Trim())
                {
                    var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _update.ProductShot);
                    if (System.IO.File.Exists(_pathprevius))
                    {
                        // Remove
                        System.IO.File.Delete(_pathprevius);
                    }
                }
                // The record is updated with the new name of the image
                _update.ProductShot = _oneFile.FileName;
                db.SaveChanges();
            }
            // Get the elements
            var _vlproductImageSize = (from _productImages in db.ProductImages
                                       join _productImagesSizes in db.ProductImageSizes
                                       on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                       join _imagesSizes in db.ImageSizes
                                       on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                       where _productImages.ProductImageId == _ProductImageId
                                       && _imagesSizes.Size == "320 x 480"
                                       select _productImagesSizes).ToList();
            // If the join record does not exist it is inserted
            if (_vlproductImageSize.LongCount() == 0)
            {
                ProductImageSizes _ProductImageSizes = new ProductImageSizes();
                _ProductImageSizes.ProductImageId = _ProductImageId;
                var _imageSize = (from _imageSizes in db.ImageSizes
                                  where _imageSizes.Size == "320 x 480"
                                  select _imageSizes).ToList();
                foreach (var _sizeId in _imageSize)
                {
                    _ProductImageSizes.ImageSizeId = _sizeId.ImageSizeId;
                }
                // The record is added
                db.Entry(_ProductImageSizes).State = EntityState.Added;
                db.SaveChanges();
            }
            // Get the items to send them to view
            var _getElements = (from _productImages in db.ProductImages
                                join _productImagesSizes in db.ProductImageSizes
                                on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                join _imagesSizes in db.ImageSizes
                                on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                where _imagesSizes.Size == "320 x 480"
                                && _productImages.ProductImageId == _ProductImageId
                                select new { _productImages, _productImagesSizes, _imagesSizes }).ToList();
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            foreach (var _row in _getElements)
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.ImageSizeId = _row._productImagesSizes.ImageSizeId;
                _ProductImagesInfo.ProductImageId = _row._productImages.ProductImageId;
                _ProductImagesInfo.ProductImageName = _row._productImages.ProductShot;
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deleteOneProductImage(int ProductImageId)
        {
            var _productImage = (from _productimages in db.ProductImages
                                 join _productimagesSizes in db.ProductImageSizes
                                 on _productimages.ProductImageId equals _productimagesSizes.ProductImageId
                                 join _imagesSizes in db.ImageSizes
                                 on _productimagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                 where _productimages.ProductImageId == ProductImageId
                                 && _imagesSizes.Size == "320 x 480"
                                 select new { _productimagesSizes, _productimages }).ToList();
            foreach (var _row in _productImage)
            {
                var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _row._productimages.ProductShot);
                if (System.IO.File.Exists(_pathprevius))
                {
                    System.IO.File.Delete(_pathprevius);
                }
                var _element = db.ProductImageSizes.Where(model => model.ProductImageId == _row._productimages.ProductImageId
                    && model.ImageSizeId == _row._productimagesSizes.ImageSizeId).ToList();
                foreach (var _remove in _element)
                {
                    db.ProductImageSizes.Remove(_remove);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult twoProductImages(int ProductImageId)
        {
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            var _getElement = (from _productImages in db.ProductImages
                               where _productImages.ProductImageId == ProductImageId
                               select _productImages).ToList();
            if (_getElement.LongCount() != 0)
            {
                foreach (var _row in _getElement)
                {
                    _ProductImagesInfo = new ProductImagesInfo();
                    _ProductImagesInfo.ProductImageId = _row.ProductImageId;
                    _ProductImagesInfo.ProductImageName = _row.ProductShot;
                    var _getImageSize = (from _productImagesSizes in db.ProductImageSizes
                                         join _imagesSizes in db.ImageSizes
                                             on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                         where _productImagesSizes.ProductImageId == _row.ProductImageId
                                         && _imagesSizes.Size == "384 x 512"
                                         select new { _productImagesSizes, _imagesSizes }).ToList();
                    if (_getImageSize.LongCount() != 0)
                    {
                        foreach (var _rowSizes in _getImageSize)
                        {
                            _ProductImagesInfo.ImageSizeId = _rowSizes._productImagesSizes.ImageSizeId;
                            var _path384px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _row.ProductShot);
                            if (System.IO.File.Exists(_path384px))
                            {
                                _ProductImagesInfo.Exist = "Exist";
                            }
                            else
                            {
                                _ProductImagesInfo.Exist = "NotFiles";
                            }
                        }
                    }
                    else
                    {
                        _ProductImagesInfo.Exist = "NotExist";
                    }
                }
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            else
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.Exist = "NotExist";
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult saveTwoProductImage()
        {
            var _twOfile = Request.Files[0];
            string[] _productImageId = Request.Form.GetValues("ProductImageId");
            string ProductImageId = String.Join(",", _productImageId);
            int _ProductImageId = int.Parse(ProductImageId);
            // You get the path in which the image is to be saved
            var _path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _twOfile.FileName);
            // Save the image
            _twOfile.SaveAs(_path);
            var _updateProductImages = (from _productImages in db.ProductImages
                                        where _productImages.ProductImageId == _ProductImageId
                                        select _productImages).ToList();
            foreach (var _update in _updateProductImages)
            {
                // Rename 320 x 480 image if it exist in the repository
                var _sourceImgFile320x480 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _update.ProductShot);
                var _destinationImgFile320x480 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _twOfile.FileName);
                if (System.IO.File.Exists(_sourceImgFile320x480))
                {
                    // rename file
                    System.IO.File.Move(_sourceImgFile320x480, _destinationImgFile320x480);
                }
                // Rename 400 x 400 image if it exists in the repository
                var _sourceImgFile400x400 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _update.ProductShot);
                var _destinationImgFile400x400 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _twOfile.FileName);
                if (System.IO.File.Exists(_sourceImgFile400x400))
                {
                    // Rename file
                    System.IO.File.Move(_sourceImgFile400x400, _destinationImgFile400x400);
                }
                // If the previous image exists it is deleted to replace it
                if (_update.ProductShot.Trim() != _twOfile.FileName.Trim())
                {
                    var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _update.ProductShot);
                    if (System.IO.File.Exists(_pathprevius))
                    {
                        // Remove
                        System.IO.File.Delete(_pathprevius);
                    }
                }
                // The record is updated with the new name of the image
                _update.ProductShot = _twOfile.FileName;
                db.SaveChanges();
            }
            // Get the elements
            var _vlproductImageSize = (from _productImages in db.ProductImages
                                       join _productImagesSizes in db.ProductImageSizes
                                       on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                       join _imagesSizes in db.ImageSizes
                                       on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                       where _productImages.ProductImageId == _ProductImageId
                                       && _imagesSizes.Size == "384 x 512"
                                       select _productImagesSizes).ToList();
            // If the join record does not exist it is inserted
            if (_vlproductImageSize.LongCount() == 0)
            {
                ProductImageSizes _ProductImageSizes = new ProductImageSizes();
                _ProductImageSizes.ProductImageId = _ProductImageId;
                var _imageSize = (from _imageSizes in db.ImageSizes
                                  where _imageSizes.Size == "384 x 512"
                                  select _imageSizes).ToList();
                foreach (var _sizeId in _imageSize)
                {
                    _ProductImageSizes.ImageSizeId = _sizeId.ImageSizeId;
                }
                // The record is added
                db.Entry(_ProductImageSizes).State = EntityState.Added;
                db.SaveChanges();
            }
            // Get the items to send them to view
            var _getElements = (from _productImages in db.ProductImages
                                join _productImagesSizes in db.ProductImageSizes
                                on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                join _imagesSizes in db.ImageSizes
                                on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                where _imagesSizes.Size == "384 x 512"
                                && _productImages.ProductImageId == _ProductImageId
                                select new { _productImages, _productImagesSizes, _imagesSizes }).ToList();
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            foreach (var _row in _getElements)
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.ImageSizeId = _row._productImagesSizes.ImageSizeId;
                _ProductImagesInfo.ProductImageId = _row._productImages.ProductImageId;
                _ProductImagesInfo.ProductImageName = _row._productImages.ProductShot;
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deletetwOProductImage(int ProductImageId)
        {
            var _productImage = (from _productimages in db.ProductImages
                                 join _productimagesSizes in db.ProductImageSizes
                                 on _productimages.ProductImageId equals _productimagesSizes.ProductImageId
                                 join _imagesSizes in db.ImageSizes
                                 on _productimagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                 where _productimages.ProductImageId == ProductImageId
                                 && _imagesSizes.Size == "384 x 512"
                                 select new { _productimagesSizes, _productimages }).ToList();
            foreach (var _row in _productImage)
            {
                var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _row._productimages.ProductShot);
                if (System.IO.File.Exists(_pathprevius))
                {
                    System.IO.File.Delete(_pathprevius);
                }
                var _element = db.ProductImageSizes.Where(model => model.ProductImageId == _row._productimages.ProductImageId
                    && model.ImageSizeId == _row._productimagesSizes.ImageSizeId).ToList();
                foreach (var _remove in _element)
                {
                    db.ProductImageSizes.Remove(_remove);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult threeProductImages(int ProductImageId)
        {
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            var _getElement = (from _productImages in db.ProductImages
                               where _productImages.ProductImageId == ProductImageId
                               select _productImages).ToList();
            if (_getElement.LongCount() != 0)
            {
                foreach (var _row in _getElement)
                {
                    _ProductImagesInfo = new ProductImagesInfo();
                    _ProductImagesInfo.ProductImageId = _row.ProductImageId;
                    _ProductImagesInfo.ProductImageName = _row.ProductShot;
                    var _getImageSize = (from _productImagesSizes in db.ProductImageSizes
                                         join _imagesSizes in db.ImageSizes
                                             on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                         where _productImagesSizes.ProductImageId == _row.ProductImageId
                                         && _imagesSizes.Size == "400 x 400"
                                         select new { _productImagesSizes, _imagesSizes }).ToList();
                    if (_getImageSize.LongCount() != 0)
                    {
                        foreach (var _rowSizes in _getImageSize)
                        {
                            _ProductImagesInfo.ImageSizeId = _rowSizes._productImagesSizes.ImageSizeId;
                            var _path400px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _row.ProductShot);
                            if (System.IO.File.Exists(_path400px))
                            {
                                _ProductImagesInfo.Exist = "Exist";
                            }
                            else
                            {
                                _ProductImagesInfo.Exist = "NotFiles";
                            }
                        }
                    }
                    else
                    {
                        _ProductImagesInfo.Exist = "NotExist";
                    }
                }
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            else
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.Exist = "NotExist";
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult saveThreeProductImage()
        {
            var _fwOfile = Request.Files[0];
            string[] _productImageId = Request.Form.GetValues("ProductImageId");
            string ProductImageId = String.Join(",", _productImageId);
            int _ProductImageId = int.Parse(ProductImageId);
            // You get the path in which the image is to be saved
            var _path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _fwOfile.FileName);
            // Save the image
            _fwOfile.SaveAs(_path);
            var _updateProductImages = (from _productImages in db.ProductImages
                                        where _productImages.ProductImageId == _ProductImageId
                                        select _productImages).ToList();
            foreach (var _update in _updateProductImages)
            {
                // Rename 340 x 480 image if it exist in the repository
                var _sourceImgFile320x480 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _update.ProductShot);
                var _destinationImgFile320x480 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), _fwOfile.FileName);
                if (System.IO.File.Exists(_sourceImgFile320x480))
                {
                    // rename file
                    System.IO.File.Move(_sourceImgFile320x480, _destinationImgFile320x480);
                }
                // Rename 384 x 512 image if it exists in the repository
                var _sourceImgFile384x512 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _update.ProductShot);
                var _destinationImgFile384x512 = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), _fwOfile.FileName);
                if (System.IO.File.Exists(_sourceImgFile384x512))
                {
                    // Rename file
                    System.IO.File.Move(_sourceImgFile384x512, _destinationImgFile384x512);
                }
                // If the previous image exists it is deleted to replace it
                if (_update.ProductShot.Trim() != _fwOfile.FileName.Trim())
                {
                    var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _update.ProductShot);
                    if (System.IO.File.Exists(_pathprevius))
                    {
                        // Remove
                        System.IO.File.Delete(_pathprevius);
                    }
                }
                // The record is updated with the new name of the image
                _update.ProductShot = _fwOfile.FileName;
                db.SaveChanges();
            }
            // Get the elements
            var _vlproductImageSize = (from _productImages in db.ProductImages
                                       join _productImagesSizes in db.ProductImageSizes
                                       on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                       join _imagesSizes in db.ImageSizes
                                       on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                       where _productImages.ProductImageId == _ProductImageId
                                       && _imagesSizes.Size == "400 x 400"
                                       select _productImagesSizes).ToList();
            // If the join record does not exist it is inserted
            if (_vlproductImageSize.LongCount() == 0)
            {
                ProductImageSizes _ProductImageSizes = new ProductImageSizes();
                _ProductImageSizes.ProductImageId = _ProductImageId;
                var _imageSize = (from _imageSizes in db.ImageSizes
                                  where _imageSizes.Size == "400 x 400"
                                  select _imageSizes).ToList();
                foreach (var _sizeId in _imageSize)
                {
                    _ProductImageSizes.ImageSizeId = _sizeId.ImageSizeId;
                }
                // The record is added
                db.Entry(_ProductImageSizes).State = EntityState.Added;
                db.SaveChanges();
            }
            // Get the items to send them to view
            var _getElements = (from _productImages in db.ProductImages
                                join _productImagesSizes in db.ProductImageSizes
                                on _productImages.ProductImageId equals _productImagesSizes.ProductImageId
                                join _imagesSizes in db.ImageSizes
                                on _productImagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                where _imagesSizes.Size == "400 x 400"
                                && _productImages.ProductImageId == _ProductImageId
                                select new { _productImages, _productImagesSizes, _imagesSizes }).ToList();
            ProductImagesInfo _ProductImagesInfo = new ProductImagesInfo();
            List<ProductImagesInfo> _l_ProductImagesInfo = new List<ProductImagesInfo>();
            foreach (var _row in _getElements)
            {
                _ProductImagesInfo = new ProductImagesInfo();
                _ProductImagesInfo.ImageSizeId = _row._productImagesSizes.ImageSizeId;
                _ProductImagesInfo.ProductImageId = _row._productImages.ProductImageId;
                _ProductImagesInfo.ProductImageName = _row._productImages.ProductShot;
                _l_ProductImagesInfo.Add(_ProductImagesInfo);
            }
            return Json(_l_ProductImagesInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult deleteThreeProductImage(int ProductImageId)
        {
            var _productImage = (from _productimages in db.ProductImages
                                 join _productimagesSizes in db.ProductImageSizes
                                 on _productimages.ProductImageId equals _productimagesSizes.ProductImageId
                                 join _imagesSizes in db.ImageSizes
                                 on _productimagesSizes.ImageSizeId equals _imagesSizes.ImageSizeId
                                 where _productimages.ProductImageId == ProductImageId
                                 && _imagesSizes.Size == "400 x 400"
                                 select new { _productimagesSizes, _productimages }).ToList();
            foreach (var _row in _productImage)
            {
                var _pathprevius = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), _row._productimages.ProductShot);
                if (System.IO.File.Exists(_pathprevius))
                {
                    System.IO.File.Delete(_pathprevius);
                }
                var _element = db.ProductImageSizes.Where(model => model.ProductImageId == _row._productimages.ProductImageId
                    && model.ImageSizeId == _row._productimagesSizes.ImageSizeId).ToList();
                foreach (var _remove in _element)
                {
                    db.ProductImageSizes.Remove(_remove);
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deletePresentation(int EditionId, int DivisionId, int ProductId, int CategoryId, int PharmaFormId, int PresentationId)
        {
            var _editionPresentations = db.EditionPresentations.Where(model => model.EditionId == EditionId && model.PresentationId == PresentationId).ToList();
            foreach (var _remove in _editionPresentations)
            {
                db.EditionPresentations.Remove(_remove);
                db.SaveChanges();
            }
            var _contentProductImageSizes = (from _p in db.Presentations
                                             join _pi in db.ProductImages
                                             on _p.PresentationId equals _pi.PresentationId
                                             join _pz in db.ProductImageSizes
                                             on _pi.ProductImageId equals _pz.ProductImageId
                                             where _p.PresentationId == PresentationId
                                             select new { _pz, _pi }).ToList();
            foreach (var _row in _contentProductImageSizes)
            {
                var imageName = "";
                imageName = _row._pi.ProductShot;
                if (_row._pz.ImageSizes.Size == "400 x 400")
                {
                    var _path400px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/400x400"), imageName);
                    if (System.IO.File.Exists(_path400px))
                    {
                        System.IO.File.Delete(_path400px);
                    }
                }
                if (_row._pz.ImageSizes.Size == "384 x 512")
                {
                    var _path384px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/384x512"), imageName);
                    if (System.IO.File.Exists(_path384px))
                    {
                        System.IO.File.Delete(_path384px);
                    }
                }
                if (_row._pz.ImageSizes.Size == "320 x 480")
                {
                    var _path320px = System.IO.Path.Combine(Server.MapPath("~/ProductImages/320x480"), imageName);
                    if (System.IO.File.Exists(_path320px))
                    {
                        System.IO.File.Delete(_path320px);
                    }
                }
                var _productImagesSizes = db.ProductImageSizes.Where(model => model.ProductImageId == _row._pz.ProductImageId && model.ImageSizeId == _row._pz.ImageSizeId).ToList();
                foreach (var _remove in _productImagesSizes)
                {
                    db.ProductImageSizes.Remove(_remove);
                    db.SaveChanges();
                }
            }
            var _productImages = db.ProductImages.Where(model => model.PresentationId == PresentationId).ToList();
            foreach (var _remove in _productImages)
            {
                db.ProductImages.Remove(_remove);
                db.SaveChanges();
            }
            var _presentation = db.Presentations.Where(model => model.PresentationId == PresentationId).ToList();
            foreach (var _remove in _presentation)
            {
                db.Presentations.Remove(_remove);
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexPage()
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
                return RedirectToAction("FilterPage", "Production", new { id = _counTries.countryid });
            }
        }
        public ActionResult FilterPage(int id)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            var _getBook = (from _b in db.plm_vwProductsByEdition
                            where _b.CountryId == id
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
            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            return View(_l_plm_vwProductsByEdition);
        }
        public ActionResult Pagination(int id, int ed, int ad)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            int ud = 0;
            SessionCountryByDivision _SessionCountryByDivision = new SessionCountryByDivision(id, ed, ad, ud);
            Session["SessionCountryByDivision"] = _SessionCountryByDivision;
            var _getProductsByEdition = (from _p in db.plm_vwProductsByEdition
                                         where _p.CountryId == id
                                         && _p.BookId == ed
                                         && _p.EditionId == ad
                                         select _p).ToList();
            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            ViewData["BookName"] = db.Books.FirstOrDefault(model => model.BookId == ed).Description;
            ViewData["EditionNumber"] = db.Editions.FirstOrDefault(model => model.EditionId == ad).NumberEdition;
            ViewData["ShortName"] = db.Books.FirstOrDefault(model => model.BookId == ed).ShortName;
            ViewData["AllProducts"] = _getProductsByEdition.LongCount();
            return View(_getProductsByEdition);
        }
        public ActionResult savePage(int ProductId, int PharmaFormId, int CategoryId, int DivisionId, int EditionId, string page)
        {
            var _page = (from _pp in db.ParticipantProducts
                         where _pp.EditionId == EditionId
                         && _pp.ProductId == ProductId
                         && _pp.PharmaFormId == PharmaFormId
                         && _pp.CategoryId == CategoryId
                         && _pp.DivisionId == DivisionId
                         select _pp).ToList();
            foreach (var _row in _page)
            {
                if (page == "")
                {
                    _row.Page = null;
                    db.SaveChanges();
                }
                else
                {
                    _row.Page = page;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public FileResult ExcelProductsByEdition(int id, int ed)
        {
            var bind = new GridView();

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

            DataColumn Contenido = new DataColumn();
            Contenido.DataType = typeof(string);
            Contenido.ColumnName = "Contenido modificado";

            DataColumn Laboratorio = new DataColumn();
            Laboratorio.DataType = typeof(string);
            Laboratorio.ColumnName = "Laboratorio";

            DataColumn Edición = new DataColumn();
            Edición.DataType = typeof(string);
            Edición.ColumnName = "Edición";

            DataColumn Obra = new DataColumn();
            Obra.DataType = typeof(string);
            Obra.ColumnName = "Obra";

            table.Columns.Add(Categoría);
            table.Columns.Add(Producto);
            table.Columns.Add(Forma);
            table.Columns.Add(Tipo);
            table.Columns.Add(Descripción);
            table.Columns.Add(Contenido);
            table.Columns.Add(Laboratorio);
            table.Columns.Add(Edición);
            table.Columns.Add(Obra);

            var _getElements = (from _products in db.plm_vwProductsByEdition
                                where _products.EditionId == id
                                && _products.DivisionId == ed
                                orderby _products.CategoryName, _products.Brand
                                select _products).ToList();
            ContentTypes _ContentTypes = new ContentTypes();
            foreach (var _row in _getElements)
            {
                var _getContents = (from _participantProducts in db.ParticipantProducts
                                    join _contentType in db.ContentTypes
                                    on _participantProducts.ContentTypeId equals _contentType.ContentTypeId
                                    where _participantProducts.ProductId == _row.ProductId
                                    && _participantProducts.DivisionId == _row.DivisionId
                                    && _participantProducts.CategoryId == _row.CategoryId
                                    && _participantProducts.PharmaFormId == _row.PharmaFormId
                                    && _participantProducts.EditionId == _row.EditionId
                                    select new { _participantProducts, _contentType }).ToList();
                foreach (var _content in _getContents)
                {
                    _ContentTypes.ContentType = _content._contentType.ContentType;
                }
                table.Rows.Add(
                   _row.CategoryName,
                   _row.Brand,
                   _row.PharmaForm,
                   _row.ProductType,
                   _row.ProductDescription,
                   _ContentTypes.ContentType,
                   _row.DivisionName,
                   _row.NumberEdition,
                   _row.BookName);
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
            bind.DataSource = table;
            FileInfo newFile = new FileInfo(Request.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
            ExcelPackage pkg = new ExcelPackage(newFile);
            if (newFile.Exists)
            {
                System.IO.File.Delete(Server.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
                newFile.Delete();
                pkg.Workbook.Worksheets.Delete("Reporte Excel");
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte Excel");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:K1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            else
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte Excel");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:K1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            pkg.SaveAs(newFile);
            var filepath = System.IO.Path.Combine(Server.MapPath("~/ReportesXLS/ReporteExcel.xlsx"));
            return File(filepath, "application/vnd.ms-excel", "ReporteExcel.xlsx");
        }
    }
}