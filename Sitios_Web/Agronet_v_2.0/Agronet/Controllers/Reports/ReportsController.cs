using Agronet.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agronet.Controllers.Reports
{
    public class ReportsController : Controller
    {
        DEAQ db = new DEAQ();

        public ActionResult GetProductsByDivision(int? EditionId, int? DivisionId, int? BookId)
        {
            if (BookId == 1)
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

                ReportDataSource rd = new ReportDataSource("ProductsByEditionDS", cm);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
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

                ReportDataSource rd = new ReportDataSource("ProductsByEditionDS", cm);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
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

        public ActionResult GetProductsByEdition(int? EditionId, int? DivisionId, int? BookId)
        {
            if (BookId == 1)
            {
                var plm = (from view in db.plm_vwProductsByEdition
                           where view.EditionId == EditionId
                           orderby view.ProductName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportGeneral.rdlc");

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

                ReportDataSource rd = new ReportDataSource("ProductsByEditionDS", cm);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
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
                           orderby view.ProductName ascending
                           select view).ToList();
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/GetReport"), "ReportGeneralDIPO.rdlc");
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

                ReportDataSource rd = new ReportDataSource("ProductsByEditionDS", cm);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;



                string deviceInfo =

                "<DeviceInfo>" +
                "  <OutputFormat>" + "PDF" + "</OutputFormat>" +
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
}