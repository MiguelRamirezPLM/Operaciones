using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.IO;
using GuiaNet.Models;
using System.Data.Objects;

namespace GuiaNet.Controllers.Reports
{
    public class GetReportsController : Controller
    {
        Guia db = new Guia();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetReport(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = (from view in db.plm_vwReportProductsByClient
                       where view.EditionId == editionid
                       && view.ClientId == clientid
                       && view.Paticipante == "P"
                       select new GetProductsByClientsR
                       {
                           BookId = view.BookId,
                           BookName = view.BookName,
                           ClientId = view.ClientId,
                           ClientIdParent = view.ClientIdParent,
                           CompanyName = view.CompanyName,
                           EditionId = view.EditionId,
                           NumberEdition = view.NumberEdition,
                           ParentIdP = view.ParentIdP,
                           Paticipante = view.Paticipante,
                           ProductId = view.ProductId,
                           ProductName = view.ProductName,
                           ShortName = view.ShortName,
                           TypeId = view.TypeId
                       }).OrderBy(x => x.ProductName).ToList();


            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductsbyclient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetProductsByClientsR> cm = new List<GetProductsByClientsR>();

            GetProductsByClientsR _cm = new Models.GetProductsByClientsR();

            foreach (GetProductsByClientsR _plm in plm)
            {
                _cm = new GetProductsByClientsR();

                _cm.BookId = _plm.BookId;
                _cm.BookName = _plm.BookName;
                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.EditionId = _plm.EditionId;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ParentIdP = _plm.ParentIdP;
                _cm.Paticipante = _plm.Paticipante;
                _cm.ProductId = _plm.ProductId;
                _cm.ProductName = _plm.ProductName;
                _cm.ShortName = _plm.ShortName;
                _cm.TypeId = _plm.TypeId;
                _cm.QuantityOfProducts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            //cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetProductsByClient", cm);
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

        public ActionResult GetReportCategoriesbyClient(string id)
        {
            indexclasification index = (indexclasification)Session["indexclasification"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<plm_vwCategoryByClients>("plm_spGetCategoryByClients  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getcategoriesbyclients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwCategoryByClients> cm = new List<plm_vwCategoryByClients>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("getcategoriesbyclient", cm);
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

        public ActionResult GetReportMedicalProductsbyClient(string id)
        {
            sessionmedicalproducts sessionmp = (sessionmedicalproducts)Session["sessionmedicalproducts"];
            int editionid = sessionmp.EId;
            int clientid = sessionmp.ClId;
            int book = sessionmp.BId;

            var plm = db.Database.SqlQuery<plm_vwMedicalProductsByClient>("plm_spGetMedicalProductsByClients  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getreportmedicalproductsbyclient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwMedicalProductsByClient> cm = new List<plm_vwMedicalProductsByClient>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("MedicalProductsDataSet", cm);
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

        public ActionResult GetReportIL(string id)
        {
            sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = (from view in db.plm_vwReportProductsByClient
                       where view.EditionId == editionid
                       && view.ClientId == clientid
                       orderby view.ProductName ascending
                       select view).ToList();
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductsbyclient-IL.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwReportProductsByClient> cm = new List<plm_vwReportProductsByClient>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GuiaProductsByClient", cm);
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

        public ActionResult GetReportCategoriesbyClientIL(string id)
        {
            sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = db.Database.SqlQuery<plm_vwCategoryByClients>("plm_spGetCategoryByClients  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getcategoriesbyclients-IL.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwCategoryByClients> cm = new List<plm_vwCategoryByClients>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("getcategoriesbyclient", cm);
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

        public ActionResult GetReportProductCategoriesIL(string id)
        {
            sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = db.Database.SqlQuery<plm_vwProductCategoryByClients>("plm_spGetCategoryByProducts  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductcategoriesbyclients-IL.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwProductCategoryByClients> cm = new List<plm_vwProductCategoryByClients>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("getproductcategoriesbyclient", cm);
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

        public ActionResult GetReportHeterogeneousCategoriesbyClient(string id)
        {
            indexclasification ind = (indexclasification)Session["indexclasification"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = db.Database.SqlQuery<plm_vwHeterogeneousCategoryByClients>("plm_spGetEditionHeterogeneousCategoryByClients  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getHeterogeneousCategoryByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwHeterogeneousCategoryByClients> cm = new List<plm_vwHeterogeneousCategoryByClients>();

            plm_vwHeterogeneousCategoryByClients _cm = new plm_vwHeterogeneousCategoryByClients();

            foreach (plm_vwHeterogeneousCategoryByClients _plm in plm)
            {
                _cm = new plm_vwHeterogeneousCategoryByClients();

                _cm.Active = _plm.Active;
                _cm.ActiveNodo = _plm.ActiveNodo;
                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.Description = _plm.Description;
                _cm.DescriptionNodo = _plm.DescriptionNodo;
                _cm.EditionId = _plm.EditionId;
                _cm.HeterogeneousCategoryId = _plm.HeterogeneousCategoryId;
                _cm.HeterogeneousCategoryIdNodo = _plm.HeterogeneousCategoryIdNodo;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ParentId = _plm.ParentId;
                _cm.ParentIdNodo = _plm.ParentIdNodo;
                _cm.ShortName = _plm.ShortName;
                _cm.Counts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            ReportDataSource rd = new ReportDataSource("getHeterogeneousCategoryByClients", cm);
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


        /*    */
        public ActionResult GetReportBrandsByClient(string id)
        {
            sessionclientbrands ind = (sessionclientbrands)Session["sessionclientbrands"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = (from cb in db.ClientBrands
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Brands
                           on cb.BrandId equals b.BrandId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       join cbt in db.ClientBrandTypes
                       on cb.ClientBrandTypeId equals cbt.ClientBrandTypeId into bcd
                       from cbt in bcd.DefaultIfEmpty()
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       select new GetBrandsByClients { BrandId = b.BrandId, BrandName = b.BrandName, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, Description = cbt.Description, Logo = b.Logo }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetBrandsByClients> cm = new List<GetBrandsByClients>();

            GetBrandsByClients GetBrandsByClients = new GetBrandsByClients();

            foreach (GetBrandsByClients _plm in plm)
            {
                GetBrandsByClients = new Models.GetBrandsByClients();

                GetBrandsByClients.BrandId = _plm.BrandId;
                GetBrandsByClients.BrandName = _plm.BrandName;
                GetBrandsByClients.ClientId = _plm.ClientId;
                GetBrandsByClients.CompanyName = _plm.CompanyName;
                GetBrandsByClients.EditionId = _plm.EditionId;
                GetBrandsByClients.NumberEdition = _plm.NumberEdition;
                GetBrandsByClients.Description = _plm.Description;

                var bi = db.BrandImageSizes.Where(x => x.BrandId == _plm.BrandId && x.Logo != null).ToList();
                if (bi.LongCount() > 0)
                {
                    GetBrandsByClients.Logo = "SI";
                }
                else
                {
                    GetBrandsByClients.Logo = "NO";
                }

                GetBrandsByClients.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetBrandsByClients);
            }

            cm = cm.OrderBy(x => x.BrandName).ToList();

            ReportDataSource rd = new ReportDataSource("DataSet1", cm);
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

        public ActionResult GetReportEspecialitiesByClient(string id)
        {
            sessionclientspecialities ind = (sessionclientspecialities)Session["sessionclientspecialities"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = (from cb in db.EditionClientSpecialities
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Specialities
                           on cb.SpecialityId equals b.SpecialityId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       orderby b.Description ascending
                       select new GetSpecialitiesbyClient { SpecialityId = b.SpecialityId, Description = b.Description, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, AdversDescription = cb.AdversDescription, Quantity = cb.Quantity }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetEspecialitiesByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetSpecialitiesbyClient> cm = new List<GetSpecialitiesbyClient>();

            GetSpecialitiesbyClient GetSpecialitiesbyClient = new GetSpecialitiesbyClient();

            foreach (GetSpecialitiesbyClient _plm in plm)
            {
                GetSpecialitiesbyClient = new Models.GetSpecialitiesbyClient();

                GetSpecialitiesbyClient.SpecialityId = _plm.SpecialityId;
                GetSpecialitiesbyClient.Description = _plm.Description;
                GetSpecialitiesbyClient.ClientId = _plm.ClientId;
                GetSpecialitiesbyClient.CompanyName = _plm.CompanyName;
                GetSpecialitiesbyClient.EditionId = _plm.EditionId;
                GetSpecialitiesbyClient.NumberEdition = _plm.NumberEdition;
                GetSpecialitiesbyClient.AdversDescription = _plm.AdversDescription;
                GetSpecialitiesbyClient.Quantity = _plm.Quantity;
                GetSpecialitiesbyClient.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetSpecialitiesbyClient);
            }

            cm = cm.OrderBy(x => x.Description).ToList();

            ReportDataSource rd = new ReportDataSource("GetSpecialities", cm);
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

        public ActionResult GetReportEspecialitiesByBranch(string id)
        {
            sessionclientid ind = (sessionclientid)Session["sessionclientid"];
            int editionid = ind.EditionId;
            int clientid = ind.ClientId;

            var plm = (from cb in db.EditionClientSpecialities
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Specialities
                           on cb.SpecialityId equals b.SpecialityId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       orderby b.Description ascending
                       select new GetSpecialitiesbyClient { SpecialityId = b.SpecialityId, Description = b.Description, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, AdversDescription = cb.AdversDescription, Quantity = cb.Quantity }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetEspecialitiesByBranch.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetSpecialitiesbyClient> cm = new List<GetSpecialitiesbyClient>();

            GetSpecialitiesbyClient GetSpecialitiesbyClient = new GetSpecialitiesbyClient();

            foreach (GetSpecialitiesbyClient _plm in plm)
            {
                GetSpecialitiesbyClient = new Models.GetSpecialitiesbyClient();

                GetSpecialitiesbyClient.SpecialityId = _plm.SpecialityId;
                GetSpecialitiesbyClient.Description = _plm.Description;
                GetSpecialitiesbyClient.ClientId = _plm.ClientId;
                GetSpecialitiesbyClient.CompanyName = _plm.CompanyName;
                GetSpecialitiesbyClient.EditionId = _plm.EditionId;
                GetSpecialitiesbyClient.NumberEdition = _plm.NumberEdition;
                GetSpecialitiesbyClient.AdversDescription = _plm.AdversDescription;
                GetSpecialitiesbyClient.Quantity = _plm.Quantity;

                var cl = db.Clients.Where(x => x.ClientId == clientid).ToList();

                foreach (Clients _cl in cl)
                {
                    var _cll = db.Clients.Where(x => x.ClientId == _cl.ClientIdParent).ToList();

                    foreach (Clients cll in _cll)
                    {
                        GetSpecialitiesbyClient.CId = cll.CompanyName;
                    }
                }

                GetSpecialitiesbyClient.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetSpecialitiesbyClient);
            }

            cm = cm.OrderBy(x => x.Description).ToList();

            ReportDataSource rd = new ReportDataSource("GetSpecialities", cm);
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

        public ActionResult GetReportHeterogeneousCategoriesbyClientReference(string id)
        {
            sessionreferenceheterogeneouscategories ind = (sessionreferenceheterogeneouscategories)Session["sessionreferenceheterogeneouscategories"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = db.Database.SqlQuery<plm_vwHeterogeneousCategoryByClients>("plm_spGetEditionHeterogeneousCategoryByClients  @editionId =" + editionid + ",  @clientId='" + clientid + "'").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getHeterogeneousCategoryByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<plm_vwHeterogeneousCategoryByClients> cm = new List<plm_vwHeterogeneousCategoryByClients>();

            plm_vwHeterogeneousCategoryByClients _cm = new plm_vwHeterogeneousCategoryByClients();

            foreach (plm_vwHeterogeneousCategoryByClients _plm in plm)
            {
                _cm = new plm_vwHeterogeneousCategoryByClients();

                _cm.Active = _plm.Active;
                _cm.ActiveNodo = _plm.ActiveNodo;
                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.Description = _plm.Description;
                _cm.DescriptionNodo = _plm.DescriptionNodo;
                _cm.EditionId = _plm.EditionId;
                _cm.HeterogeneousCategoryId = _plm.HeterogeneousCategoryId;
                _cm.HeterogeneousCategoryIdNodo = _plm.HeterogeneousCategoryIdNodo;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ParentId = _plm.ParentId;
                _cm.ParentIdNodo = _plm.ParentIdNodo;
                _cm.ShortName = _plm.ShortName;
                _cm.Counts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            cm = cm.OrderBy(x => x.Description).ToList();

            ReportDataSource rd = new ReportDataSource("getHeterogeneousCategoryByClients", cm);
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

        public ActionResult GetReportEspecialitiesByClientReference(string id)
        {
            sessionreferencespecialities ind = (sessionreferencespecialities)Session["sessionreferencespecialities"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = (from cb in db.EditionClientSpecialities
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Specialities
                           on cb.SpecialityId equals b.SpecialityId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       orderby b.Description ascending
                       select new GetSpecialitiesbyClient { SpecialityId = b.SpecialityId, Description = b.Description, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, AdversDescription = cb.AdversDescription, Quantity = cb.Quantity }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetEspecialitiesByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetSpecialitiesbyClient> cm = new List<GetSpecialitiesbyClient>();

            GetSpecialitiesbyClient GetSpecialitiesbyClient = new GetSpecialitiesbyClient();

            foreach (GetSpecialitiesbyClient _plm in plm)
            {
                GetSpecialitiesbyClient = new Models.GetSpecialitiesbyClient();

                GetSpecialitiesbyClient.SpecialityId = _plm.SpecialityId;
                GetSpecialitiesbyClient.Description = _plm.Description;
                GetSpecialitiesbyClient.ClientId = _plm.ClientId;
                GetSpecialitiesbyClient.CompanyName = _plm.CompanyName;
                GetSpecialitiesbyClient.EditionId = _plm.EditionId;
                GetSpecialitiesbyClient.NumberEdition = _plm.NumberEdition;
                GetSpecialitiesbyClient.AdversDescription = _plm.AdversDescription;
                GetSpecialitiesbyClient.Quantity = _plm.Quantity;
                GetSpecialitiesbyClient.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetSpecialitiesbyClient);
            }

            cm = cm.OrderBy(x => x.Description).ToList();

            ReportDataSource rd = new ReportDataSource("GetSpecialities", cm);
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

        public ActionResult GetReportEspecialitiesByBranchReference(string id)
        {
            sessionspbybranch ind = (sessionspbybranch)Session["sessionspbybranch"];
            int editionid = ind.EditionId;
            int clientid = ind.ClientId;

            var plm = (from cb in db.EditionClientSpecialities
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Specialities
                           on cb.SpecialityId equals b.SpecialityId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       orderby b.Description ascending
                       select new GetSpecialitiesbyClient { SpecialityId = b.SpecialityId, Description = b.Description, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, AdversDescription = cb.AdversDescription, Quantity = cb.Quantity }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetEspecialitiesByBranch.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetSpecialitiesbyClient> cm = new List<GetSpecialitiesbyClient>();

            GetSpecialitiesbyClient GetSpecialitiesbyClient = new GetSpecialitiesbyClient();

            foreach (GetSpecialitiesbyClient _plm in plm)
            {
                GetSpecialitiesbyClient = new Models.GetSpecialitiesbyClient();

                GetSpecialitiesbyClient.SpecialityId = _plm.SpecialityId;
                GetSpecialitiesbyClient.Description = _plm.Description;
                GetSpecialitiesbyClient.ClientId = _plm.ClientId;
                GetSpecialitiesbyClient.CompanyName = _plm.CompanyName;
                GetSpecialitiesbyClient.EditionId = _plm.EditionId;
                GetSpecialitiesbyClient.NumberEdition = _plm.NumberEdition;
                GetSpecialitiesbyClient.AdversDescription = _plm.AdversDescription;
                GetSpecialitiesbyClient.Quantity = _plm.Quantity;
                var cl = db.Clients.Where(x => x.ClientId == clientid).ToList();

                foreach (Clients _cl in cl)
                {
                    var _cll = db.Clients.Where(x => x.ClientId == _cl.ClientIdParent).ToList();

                    foreach (Clients cll in _cll)
                    {
                        GetSpecialitiesbyClient.CId = cll.CompanyName;
                    }
                }
                GetSpecialitiesbyClient.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetSpecialitiesbyClient);
            }

            cm = cm.OrderBy(x => x.Description).ToList();

            ReportDataSource rd = new ReportDataSource("GetSpecialities", cm);
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

        public ActionResult GetReportBrandsByClientReference(string id)
        {
            sessionbrandimages ind = (sessionbrandimages)Session["sessionbrandimages"];
            int editionid = ind.EId;
            int clientid = ind.ClId;
            int book = ind.BId;

            var plm = (from cb in db.ClientBrands
                       join c in db.Clients
                           on cb.ClientId equals c.ClientId
                       join b in db.Brands
                           on cb.BrandId equals b.BrandId
                       join e in db.Editions
                       on cb.EditionId equals e.EditionId
                       join cbt in db.ClientBrandTypes
                       on cb.ClientBrandTypeId equals cbt.ClientBrandTypeId into bcd
                       from cbt in bcd.DefaultIfEmpty()
                       where c.ClientId == clientid
                       && e.EditionId == editionid
                       select new GetBrandsByClients { BrandId = b.BrandId, BrandName = b.BrandName, ClientId = c.ClientId, CompanyName = c.CompanyName, EditionId = e.EditionId, NumberEdition = e.NumberEdition, Description = cbt.Description, Logo = b.Logo }).ToList();

            LocalReport lr = new LocalReport();

            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetBrandsByClients> cm = new List<GetBrandsByClients>();

            GetBrandsByClients GetBrandsByClients = new GetBrandsByClients();

            foreach (GetBrandsByClients _plm in plm)
            {
                GetBrandsByClients = new Models.GetBrandsByClients();

                GetBrandsByClients.BrandId = _plm.BrandId;
                GetBrandsByClients.BrandName = _plm.BrandName;
                GetBrandsByClients.ClientId = _plm.ClientId;
                GetBrandsByClients.CompanyName = _plm.CompanyName;
                GetBrandsByClients.EditionId = _plm.EditionId;
                GetBrandsByClients.NumberEdition = _plm.NumberEdition;
                GetBrandsByClients.Description = _plm.Description;

                var bi = db.BrandImageSizes.Where(x => x.BrandId == _plm.BrandId && x.Logo != null).ToList();
                if (bi.LongCount() > 0)
                {
                    GetBrandsByClients.Logo = "SI";
                }
                else
                {
                    GetBrandsByClients.Logo = "NO";
                }
                GetBrandsByClients.Count = Convert.ToInt32(plm.LongCount());

                cm.Add(GetBrandsByClients);
            }

            cm = cm.OrderBy(x => x.BrandName).ToList();


            ReportDataSource rd = new ReportDataSource("DataSet1", cm);
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


        /*              PRODUCTS            */

        public ActionResult GetReportProductsByEdition(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByEditionR>("plm_spGetProductsByEdition @EditionId=" + editionid + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductsbyedition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetProductsByEditionR> cm = new List<GetProductsByEditionR>();
            GetProductsByEditionR _cm = new GetProductsByEditionR();

            foreach (GetProductsByEditionR _plm in plm)
            {
                _cm = new GetProductsByEditionR();

                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.EditionId = _plm.EditionId;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ProductName = _plm.ProductName;
                _cm.QuantityOfProducts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            ReportDataSource rd = new ReportDataSource("GetProductsByEdition", cm);
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


        /*          PRODUCCION      */

        public ActionResult GetReportProduction(string id)
        {
            sessionproduction index = (sessionproduction)Session["sessionproduction"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = (from view in db.plm_vwReportProductsByClient
                       where view.EditionId == editionid
                       && view.ClientId == clientid
                       && view.Paticipante == "P"
                       select new GetProductsByClientsR
                       {
                           BookId = view.BookId,
                           BookName = view.BookName,
                           ClientId = view.ClientId,
                           ClientIdParent = view.ClientIdParent,
                           CompanyName = view.CompanyName,
                           EditionId = view.EditionId,
                           NumberEdition = view.NumberEdition,
                           ParentIdP = view.ParentIdP,
                           Paticipante = view.Paticipante,
                           ProductId = view.ProductId,
                           ProductName = view.ProductName,
                           ShortName = view.ShortName,
                           TypeId = view.TypeId
                       }).OrderBy(x => x.ProductName).ToList();


            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductsbyclient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetProductsByClientsR> cm = new List<GetProductsByClientsR>();

            GetProductsByClientsR _cm = new Models.GetProductsByClientsR();

            foreach (GetProductsByClientsR _plm in plm)
            {
                _cm = new GetProductsByClientsR();

                _cm.BookId = _plm.BookId;
                _cm.BookName = _plm.BookName;
                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.EditionId = _plm.EditionId;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ParentIdP = _plm.ParentIdP;
                _cm.Paticipante = _plm.Paticipante;
                _cm.ProductId = _plm.ProductId;
                _cm.ProductName = _plm.ProductName;
                _cm.ShortName = _plm.ShortName;
                _cm.TypeId = _plm.TypeId;
                _cm.QuantityOfProducts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            //cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetProductsByClient", cm);
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

        public ActionResult GetReportProductsByEditionProduction(string id)
        {
            sessionproduction index = (sessionproduction)Session["sessionproduction"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByEditionR>("plm_spGetProductsByEdition @EditionId=" + editionid + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "getproductsbyedition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }
            List<GetProductsByEditionR> cm = new List<GetProductsByEditionR>();
            GetProductsByEditionR _cm = new GetProductsByEditionR();

            foreach (GetProductsByEditionR _plm in plm)
            {
                _cm = new GetProductsByEditionR();

                _cm.ClientId = _plm.ClientId;
                _cm.ClientIdParent = _plm.ClientIdParent;
                _cm.CompanyName = _plm.CompanyName;
                _cm.EditionId = _plm.EditionId;
                _cm.NumberEdition = _plm.NumberEdition;
                _cm.ProductName = _plm.ProductName;
                _cm.QuantityOfProducts = Convert.ToInt32(plm.LongCount());

                cm.Add(_cm);
            }

            ReportDataSource rd = new ReportDataSource("GetProductsByEdition", cm);
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


        //PRODUCTSTATUS

         public ActionResult GetReportProductStatus(string id)
         {
             sessionClasificationHC index = (sessionClasificationHC)Session["sessionClasificationHC"];
             int editionid = index.EId;
             int clientid = index.ClId;
             int book = index.BId;

             var EId = db.Editions.Where(x => x.EditionId == editionid).Select(x => x.NumberEdition).ToList();

             var plm = db.Database.SqlQuery<GetProductStatusByClient>("plm_spGetReportProductStatusByClient @ClientId=" + clientid + "").ToList();

             LocalReport lr = new LocalReport();
             string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductStatus.rdlc");
             if (System.IO.File.Exists(path))
             {
                 lr.ReportPath = path;
             }
             else
             {
                 return View("error");
             }

             List<GetProductStatusByClient> cm = new List<GetProductStatusByClient>();
             GetProductStatusByClient _cm = new GetProductStatusByClient();

             foreach(GetProductStatusByClient _plm in plm)
             {
                 _cm = new GetProductStatusByClient();

                 _cm.ClientId = _plm.ClientId;
                 _cm.CompanyName = _plm.CompanyName;
                 _cm.ProductId = _plm.ProductId;
                 _cm.ProductName = _plm.ProductName;
                 _cm.StatusId = _plm.StatusId;
                 _cm.StatusName = _plm.StatusName;
                 _cm.NumberEdition = Convert.ToInt32(EId[0]);

                 cm.Add(_cm);
             }

             ReportDataSource rd = new ReportDataSource("DSProductStatus", cm);
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

         public ActionResult GetReportProductByClientHC(string id)
         {
             sessionClasificationHC index = (sessionClasificationHC)Session["sessionClasificationHC"];
             int editionid = index.EId;
             int clientid = index.ClId;
             int book = index.BId;

             var EId = db.Editions.Where(x => x.EditionId == editionid).Select(x => x.NumberEdition).ToList();

             var CID = db.Clients.Where(x => x.ClientId == clientid).Select(x => x.CompanyName).ToList();

             var plm = db.Database.SqlQuery<GetParticipantProducts>("plm_spGetParticipantProductsToClasification @ClientId=" + clientid + ", @EditionId=" + editionid + "").ToList();

             LocalReport lr = new LocalReport();
             string path = Path.Combine(Server.MapPath("~/GetReport"), "GetParticipantProducts.rdlc");
             if (System.IO.File.Exists(path))
             {
                 lr.ReportPath = path;
             }
             else
             {
                 return View("error");
             }

             List<GetParticipantProducts> cm = new List<GetParticipantProducts>();
             GetParticipantProducts _cm = new GetParticipantProducts();

             foreach (GetParticipantProducts _plm in plm)
             {
                 _cm = new GetParticipantProducts();

                 _cm.CompanyName = CID[0];
                 _cm.ProductId = _plm.ProductId;
                 _cm.ProductName = _plm.ProductName;
                 _cm.StatusName = _plm.StatusName;
                 _cm.NumberEdition = Convert.ToInt32(EId[0]);

                 cm.Add(_cm);
             }

             ReportDataSource rd = new ReportDataSource("GetParticipantProducts", cm);
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