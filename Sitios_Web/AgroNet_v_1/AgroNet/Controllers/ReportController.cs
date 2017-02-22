using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroNet.Models;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace AgroNet.Controllers.Ventas
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        DEAQ db = new DEAQ();
        public ActionResult Index()
        {
            SearchPages SearchPages = (SearchPages)Session["SearchPages"];
            int EditionId =23;
            int DivisionId = 10;

            var plm = (from view in db.plm_vwProductsByEdition
                       where view.EditionId == EditionId
                       && view.DivisionId == DivisionId
                       select view).ToList();
            return View(plm);
        }

        public ActionResult GetReport(string id)
        {            
            Search search = (Search)Session["Search"];
            int EditionId = int.Parse(search.EId);
            int DivisionId = int.Parse(search.DId);
            int Book = int.Parse(search.BId);
            if(Book == 1)
            {
                var plm = (from view in db.plm_vwProductsByEdition
                           where view.EditionId == EditionId
                           && view.DivisionId == DivisionId
                           orderby view.ProductName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportProducts.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("error");
                }
                List<plm_vwProductsByEdition> cm = new List<plm_vwProductsByEdition>();

                cm = plm.ToList();

                ReportDataSource rd = new ReportDataSource("ReportData", cm);
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
            else
            {
                var plm = (from view in db.plm_vwProductsByEdition
                           where view.EditionId == EditionId
                           && view.DivisionId == DivisionId
                           orderby view.ProductName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportProductsDIPO.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("error");
                }
                List<plm_vwProductsByEdition> cm = new List<plm_vwProductsByEdition>();

                cm = plm.ToList();

                ReportDataSource rd = new ReportDataSource("ReportData", cm);
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

        public ActionResult GetGeneralReport(string id)
        {
            Search search = (Search)Session["Search"];
            int EditionId = int.Parse(search.EId);
            int DivisionId = int.Parse(search.DId);
            int Book = int.Parse(search.BId);
            if (Book == 1)
            {
                var plm = (from view in db.plm_vwProductsByEdition
                           where view.EditionId == EditionId
                           orderby view.DivisionShortName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportGeneral.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }
                List<plm_vwProductsByEdition> cm = new List<plm_vwProductsByEdition>();

                cm = plm.ToList();

                ReportDataSource rd = new ReportDataSource("ReportData", cm);
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
                "  <MarginTop>0.2in</MarginTop>" +
                "  <MarginLeft>0.1in</MarginLeft>" +
                "  <MarginRight>0.1in</MarginRight>" +
                "  <MarginBottom>0.1in</MarginBottom>" +
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
            else
            {
                var plm = (from view in db.plm_vwProductsByEdition
                           where view.EditionId == EditionId
                           orderby view.DivisionShortName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportGeneralDIPO.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("Index");
                }
                List<plm_vwProductsByEdition> cm = new List<plm_vwProductsByEdition>();

                cm = plm.ToList();

                ReportDataSource rd = new ReportDataSource("ReportData", cm);
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
                "  <MarginTop>0.2in</MarginTop>" +
                "  <MarginLeft>0.1in</MarginLeft>" +
                "  <MarginRight>0.1in</MarginRight>" +
                "  <MarginBottom>0.1in</MarginBottom>" +
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
}
