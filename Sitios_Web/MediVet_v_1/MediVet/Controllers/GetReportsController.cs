using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediVet.Models;

namespace MediVet.Controllers
{
    public class GetReportsController : Controller
    {
        PEV db = new PEV();
        //
        // GET: /GetReports/
        public ActionResult GetReportProductsByDivision(string id)
        {
            _indexsession index = (_indexsession)Session["_indexsession"];
            int editionid = index.EId;
            int DivisionId = index.DId;
            int book = index.BId;
            int CountryId = index.CId;

            var plm = (from _plm in db.plm_vwProductsByEdition
                       where _plm.EditionId == editionid
                       && _plm.CountryId == CountryId
                       && _plm.DivisionId == DivisionId
                       && _plm.TypeInEdition != null
                       orderby _plm.DivisionName ascending
                       select new classreportproductsbydivisionSM
                       {
                           CategoryId = _plm.CategoryId,
                           CategoryName = _plm.CategoryName,
                           Description = _plm.ProductDescription,
                           DivisionName = _plm.DivisionName,
                           NumberEdition = _plm.NumberEdition,
                           PharmaForm = _plm.PharmaForm,
                           PharmaFormId = _plm.PharmaFormId,
                           ProductId = _plm.ProductId,
                           ProductName = _plm.ProductName,
                           RegisterSanitary = _plm.RegisterSanitary,
                       }).OrderBy(x => x.ProductName).ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductsByDivision.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }


            List<classreportproductsbydivisionSM> cm = new List<classreportproductsbydivisionSM>();
            classreportproductsbydivisionSM _cm = new classreportproductsbydivisionSM();

            foreach (classreportproductsbydivisionSM _plm in plm)
            {
                _cm = new classreportproductsbydivisionSM();

                _cm.CategoryName = _plm.CategoryName;
                _cm.Description = _plm.Description;
                _cm.DivisionName = _plm.DivisionName;
                _cm.PharmaForm = _plm.PharmaForm;
                _cm.ProductName = _plm.ProductName;
                _cm.RegisterSanitary = _plm.RegisterSanitary;

                var sd = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId && x.Active == true).ToList();

                if (sd.LongCount() > 0)
                {
                    _cm.Active = "SI";
                }
                else
                {
                    _cm.Active = "NO";
                }

                var np = db.NewProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (np.LongCount() > 0)
                {
                    _cm.NewP = "N";
                }

                var pp = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (pp.LongCount() > 0)
                {
                    _cm.Type = "P";
                }

                var mp = db.MentionatedProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (mp.LongCount() > 0)
                {
                    _cm.Type = "M";
                }

                var ed = db.Editions.Where(x => x.EditionId == editionid).ToList();
                if (ed.LongCount() > 0)
                {
                    foreach (Editions _ed in ed)
                    {
                        _cm.NumberEdition = _ed.NumberEdition;
                    }
                }

                _cm.Count = Convert.ToInt32(plm.LongCount());

                if ((np.LongCount() > 0) || (pp.LongCount() > 0) || (mp.LongCount() > 0))
                {
                    cm.Add(_cm);
                }
            }

            ReportDataSource rd = new ReportDataSource("GetProductsByDivision", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        public ActionResult GetReportProductsByEditionSM(string id)
        {
            _indexsession index = (_indexsession)Session["_indexsession"];
            int editionid = index.EId;
            int DivisionId = index.DId;
            int book = index.BId;
            int CountryId = index.CId;

            var plm = (from _plm in db.plm_vwProductsByEdition
                       where _plm.EditionId == editionid
                       && _plm.TypeInEdition != null
                       orderby _plm.DivisionName ascending
                       select new classreportproductsbyeditionSM
                       {
                           BarCode = _plm.BarCode,
                           BookActive = _plm.BookActive,
                           BookId = _plm.BookId,
                           BookName = _plm.BookName,
                           CategoryActive = _plm.CategoryActive,
                           CategoryId = _plm.CategoryId,
                           CategoryName = _plm.CategoryName,
                           CountryId = _plm.CountryId,
                           CountryName = _plm.CountryName,
                           DivisionActive = _plm.DivisionActive,
                           DivisionId = _plm.DivisionId,
                           DivisionName = _plm.DivisionName,
                           DivisionShortName = _plm.DivisionShortName,
                           EditionActive = _plm.EditionActive,
                           EditionId = _plm.EditionId,
                           ISBN = _plm.ISBN,
                           LabActive = _plm.LabActive,
                           LaboratoryId = _plm.LaboratoryId,
                           LaboratoryName = _plm.LaboratoryName,
                           NewProduct = _plm.NewProduct,
                           NumberEdition = _plm.NumberEdition,
                           Page = _plm.Page,
                           PharmaActive = _plm.PharmaActive,
                           PharmaForm = _plm.PharmaForm,
                           PharmaFormId = _plm.PharmaFormId,
                           ProductActive = _plm.ProductActive,
                           ProductDescription = _plm.ProductDescription,
                           ProductId = _plm.ProductId,
                           ProductName = _plm.ProductName,
                           TypeInEdition = _plm.TypeInEdition
                       }).OrderBy(x => x.DivisionShortName).ToList();


            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetReportProductsByEditionSM.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }


            List<classreportproductsbyeditionSM> cm = new List<classreportproductsbyeditionSM>();
            classreportproductsbyeditionSM _cm = new classreportproductsbyeditionSM();

            foreach (classreportproductsbyeditionSM _plm in plm)
            {
                _cm = new classreportproductsbyeditionSM();

                _cm.BarCode = _cm.BarCode;
                _cm.BookActive = _plm.BookActive;
                _cm.BookId = _plm.BookId;
                _cm.BookName = _plm.BookName;
                _cm.CategoryActive = _plm.CategoryActive;
                _cm.CategoryId = _plm.CategoryId;
                _cm.CategoryName = _plm.CategoryName;
                _cm.CountryId = _plm.CountryId;
                _cm.CountryName = _plm.CountryName;
                _cm.DivisionActive = _plm.DivisionActive;
                _cm.DivisionId = _plm.DivisionId;
                _cm.DivisionName = _plm.DivisionName;
                _cm.DivisionShortName = _plm.DivisionShortName;
                _cm.EditionActive = _plm.EditionActive;
                _cm.EditionId = _plm.EditionId;
                _cm.ISBN = _plm.ISBN;
                _cm.LabActive = _plm.LabActive;
                _cm.LaboratoryId = _plm.LaboratoryId;
                _cm.LaboratoryName = _plm.LaboratoryName;

                var sd = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId && x.Active == true).ToList();

                if (sd.LongCount() > 0)
                {
                    _cm.Active = "SI";
                }
                else
                {
                    _cm.Active = "NO";
                }

                if (_plm.NewProduct != null)
                {
                    _cm.NewProduct = "N";
                }
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.Page = _plm.Page;
                _cm.PharmaActive = _plm.PharmaActive;
                _cm.PharmaForm = _plm.PharmaForm;
                _cm.PharmaFormId = _plm.PharmaFormId;
                _cm.ProductActive = _plm.ProductActive;
                _cm.ProductDescription = _plm.ProductDescription;
                _cm.ProductId = _plm.ProductId;
                _cm.ProductName = _plm.ProductName;
                _cm.TypeInEdition = _plm.TypeInEdition;

                _cm.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);

            }
            //cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetProductsByEditionSM", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        public ActionResult GetReportProductsByDivisionProd(string id)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];

            int editionid = index.EId;
            int DivisionId = index.DId;
            int book = index.BId;
            int CountryId = index.CId;

            var plm = (from _plm in db.plm_vwProductsByEdition
                       where _plm.EditionId == editionid
                       && _plm.CountryId == CountryId
                       && _plm.DivisionId == DivisionId
                       && _plm.TypeInEdition != null
                       orderby _plm.DivisionName ascending
                       select new classreportproductsbydivisionSM
                       {
                           CategoryId = _plm.CategoryId,
                           CategoryName = _plm.CategoryName,
                           Description = _plm.ProductDescription,
                           DivisionName = _plm.DivisionName,
                           NumberEdition = _plm.NumberEdition,
                           PharmaForm = _plm.PharmaForm,
                           PharmaFormId = _plm.PharmaFormId,
                           ProductId = _plm.ProductId,
                           ProductName = _plm.ProductName,
                           RegisterSanitary = _plm.RegisterSanitary,
                       }).OrderBy(x => x.ProductName).ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductsByDivision.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }


            List<classreportproductsbydivisionSM> cm = new List<classreportproductsbydivisionSM>();
            classreportproductsbydivisionSM _cm = new classreportproductsbydivisionSM();

            foreach (classreportproductsbydivisionSM _plm in plm)
            {
                _cm = new classreportproductsbydivisionSM();

                _cm.CategoryName = _plm.CategoryName;
                _cm.Description = _plm.Description;
                _cm.DivisionName = _plm.DivisionName;
                _cm.PharmaForm = _plm.PharmaForm;
                _cm.ProductName = _plm.ProductName;
                _cm.RegisterSanitary = _plm.RegisterSanitary;

                var sd = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId && x.Active == true).ToList();

                if (sd.LongCount() > 0)
                {
                    _cm.Active = "SI";
                }
                else
                {
                    _cm.Active = "NO";
                }

                var np = db.NewProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (np.LongCount() > 0)
                {
                    _cm.NewP = "N";
                }

                var pp = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (pp.LongCount() > 0)
                {
                    _cm.Type = "P";
                }

                var mp = db.MentionatedProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId).ToList();
                if (mp.LongCount() > 0)
                {
                    _cm.Type = "M";
                }

                var ed = db.Editions.Where(x => x.EditionId == editionid).ToList();
                if (ed.LongCount() > 0)
                {
                    foreach (Editions _ed in ed)
                    {
                        _cm.NumberEdition = _ed.NumberEdition;
                    }
                }

                _cm.Count = Convert.ToInt32(plm.LongCount());

                if ((np.LongCount() > 0) || (pp.LongCount() > 0) || (mp.LongCount() > 0))
                {
                    cm.Add(_cm);
                }
            }

            ReportDataSource rd = new ReportDataSource("GetProductsByDivision", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        public ActionResult GetReportProductsByEditionProd(string id)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];

            int editionid = index.EId;
            int DivisionId = index.DId;
            int book = index.BId;
            int CountryId = index.CId;

            var plm = (from _plm in db.plm_vwProductsByEdition
                       where _plm.EditionId == editionid
                       && _plm.TypeInEdition != null
                       orderby _plm.DivisionName ascending
                       select new classreportproductsbyeditionSM
                       {
                           BarCode = _plm.BarCode,
                           BookActive = _plm.BookActive,
                           BookId = _plm.BookId,
                           BookName = _plm.BookName,
                           CategoryActive = _plm.CategoryActive,
                           CategoryId = _plm.CategoryId,
                           CategoryName = _plm.CategoryName,
                           CountryId = _plm.CountryId,
                           CountryName = _plm.CountryName,
                           DivisionActive = _plm.DivisionActive,
                           DivisionId = _plm.DivisionId,
                           DivisionName = _plm.DivisionName,
                           DivisionShortName = _plm.DivisionShortName,
                           EditionActive = _plm.EditionActive,
                           EditionId = _plm.EditionId,
                           ISBN = _plm.ISBN,
                           LabActive = _plm.LabActive,
                           LaboratoryId = _plm.LaboratoryId,
                           LaboratoryName = _plm.LaboratoryName,
                           NewProduct = _plm.NewProduct,
                           NumberEdition = _plm.NumberEdition,
                           Page = _plm.Page,
                           PharmaActive = _plm.PharmaActive,
                           PharmaForm = _plm.PharmaForm,
                           PharmaFormId = _plm.PharmaFormId,
                           ProductActive = _plm.ProductActive,
                           ProductDescription = _plm.ProductDescription,
                           ProductId = _plm.ProductId,
                           ProductName = _plm.ProductName,
                           TypeInEdition = _plm.TypeInEdition
                       }).OrderBy(x => x.DivisionShortName).ToList();


            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetReportProductsByEditionSM.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }


            List<classreportproductsbyeditionSM> cm = new List<classreportproductsbyeditionSM>();
            classreportproductsbyeditionSM _cm = new classreportproductsbyeditionSM();

            foreach (classreportproductsbyeditionSM _plm in plm)
            {
                _cm = new classreportproductsbyeditionSM();

                _cm.BarCode = _cm.BarCode;
                _cm.BookActive = _plm.BookActive;
                _cm.BookId = _plm.BookId;
                _cm.BookName = _plm.BookName;
                _cm.CategoryActive = _plm.CategoryActive;
                _cm.CategoryId = _plm.CategoryId;
                _cm.CategoryName = _plm.CategoryName;
                _cm.CountryId = _plm.CountryId;
                _cm.CountryName = _plm.CountryName;
                _cm.DivisionActive = _plm.DivisionActive;
                _cm.DivisionId = _plm.DivisionId;
                _cm.DivisionName = _plm.DivisionName;
                _cm.DivisionShortName = _plm.DivisionShortName;
                _cm.EditionActive = _plm.EditionActive;
                _cm.EditionId = _plm.EditionId;
                _cm.ISBN = _plm.ISBN;
                _cm.LabActive = _plm.LabActive;
                _cm.LaboratoryId = _plm.LaboratoryId;
                _cm.LaboratoryName = _plm.LaboratoryName;

                var sd = db.ParticipantProducts.Where(x => x.ProductId == _plm.ProductId && x.PharmaFormId == _plm.PharmaFormId && x.EditionId == editionid && x.DivisionId == DivisionId && x.CategoryId == _plm.CategoryId && x.Active == true).ToList();

                if (sd.LongCount() > 0)
                {
                    _cm.Active = "SI";
                }
                else
                {
                    _cm.Active = "NO";
                }

                if (_plm.NewProduct != null)
                {
                    _cm.NewProduct = "N";
                }
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.Page = _plm.Page;
                _cm.PharmaActive = _plm.PharmaActive;
                _cm.PharmaForm = _plm.PharmaForm;
                _cm.PharmaFormId = _plm.PharmaFormId;
                _cm.ProductActive = _plm.ProductActive;
                _cm.ProductDescription = _plm.ProductDescription;
                _cm.ProductId = _plm.ProductId;
                _cm.ProductName = _plm.ProductName;
                _cm.TypeInEdition = _plm.TypeInEdition;

                _cm.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);

            }
            //cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetProductsByEditionSM", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>0.1in</MarginLeft>" +
            "  <MarginRight>0.1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
    }
}