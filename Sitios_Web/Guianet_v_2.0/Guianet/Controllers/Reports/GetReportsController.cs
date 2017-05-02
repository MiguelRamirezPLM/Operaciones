using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guianet.Models;
using System.IO;
using Guianet.Models.Sessions;

namespace Guianet.Controllers.Reports
{
    public class GetReportsController : Controller
    {
        GuiaEntities db = new GuiaEntities();
        PLMUsers dbusers = new PLMUsers();

        CountriesUsers c = (CountriesUsers)System.Web.HttpContext.Current.Session["CountriesUsers"];

        public ActionResult GetProductsByClientSM(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

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

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }



            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReportProductsByEdition(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsByEdition @EditionId=" + editionid + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetBranchsByClientSM(string id)
        {
            SessionBranchSM index = (SessionBranchSM)Session["SessionBranchSM"];
            int editionid = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetBrandsByClient>("plm_spGetParticipantBranchsByClient @EditionId=" + editionid + ", @ClientId=" + ClientId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBranchByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetBrandsByClient> cm = new List<GetBrandsByClient>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("BranchByClient", cm);
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

        public ActionResult GetAddressByClientSM(string id, int? ClientId, int? EditionId)
        {
            if ((ClientId == null) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            //var b = db.Clients.Where(x => x.ClientIdParent == ClientId).ToList();

            List<ClientTypes> LC = db.ClientTypes.Where(x => x.TypeName == "SUCURSAL").ToList();

            byte ClientTypeId = LC[0].ClientTypeId;

            var b = (from c in db.Clients
                     join ec in db.EditionClients
                         on c.ClientId equals ec.ClientId
                     where ec.EditionId == EditionId
                     && ec.ClientTypeId == ClientTypeId
                     && c.ClientIdParent == ClientId
                     select c).ToList();


            var br = db.ClientBrands.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

            var ad = db.ClientAdverts.Where(x => x.ClientId == ClientId).ToList();

            var pr = db.ParticipantProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId).ToList();

            var plm = db.Database.SqlQuery<ICAddress>("plm_spGetAddressesByClient @ClientId=" + ClientId + ", @Report=S").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetInformationClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ICAddress> cm = new List<ICAddress>();

            cm = plm.ToList();

            cm[0].Branchs = Convert.ToInt32(b.LongCount());
            cm[0].Brands = Convert.ToInt32(br.LongCount());
            cm[0].Adverts = Convert.ToInt32(ad.LongCount());
            cm[0].Products = Convert.ToInt32(pr.LongCount());

            var plmad = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

            plmad = plmad.Where(x => x.CategoryThree != null).OrderBy(x => x.CategoryThree).ToList();

            if (plm.LongCount() > 0)
            {
                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }


            ReportDataSource rd = new ReportDataSource("AddressesByClient", cm);

            rd.Name = "AddressesByClient";
            rd.Value = cm;

            lr.DataSources.Add(rd);


            ReportDataSource rd1 = new ReportDataSource();

            rd1.Name = "Adverts";
            rd1.Value = plmad;

            lr.DataSources.Add(rd1);

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

        public ActionResult GetProductsByClientWithCategoriesSM(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsCategoriesByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReportProductsByEditionWithCategories(string id)
        {
            indexsalesmodule index = (indexsalesmodule)Session["indexsalesmodule"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductCategoriesByEdition @EditionId=" + editionid + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReportBrandsByClient(string id)
        {
            SessionBrandsSM index = (SessionBrandsSM)Session["SessionBrandsSM"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ClientBrandsCls> cm = new List<ClientBrandsCls>();
            ClientBrandsCls ClientBrandsCls = new ClientBrandsCls();

            foreach (ClientBrandsCls _plm in plm)
            {
                ClientBrandsCls = new ClientBrandsCls();

                ClientBrandsCls.BrandId = _plm.BrandId;
                ClientBrandsCls.BrandName = _plm.BrandName;
                ClientBrandsCls.ExpireDescription = _plm.ExpireDescription;

                if (_plm.Distributor == 1)
                {
                    ClientBrandsCls.Type = "Distribuidor autorizado";
                }

                if (_plm.Representative == 1)
                {
                    ClientBrandsCls.Type = "Representante exclusivo";
                }

                cm.Add(ClientBrandsCls);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var cn = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.CompanyName).ToList();

                cm[0].CompanyName = cn[0];

                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();

                cm[0].NumberEdition = ed[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }


            ReportDataSource rd = new ReportDataSource("GetBrandsByClient", cm);
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

        public ActionResult GetReportBrandsByEdition(string id)
        {
            SessionBrandsSM index = (SessionBrandsSM)Session["SessionBrandsSM"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ClientBrandsCls> cm = new List<ClientBrandsCls>();
            ClientBrandsCls ClientBrandsCls = new ClientBrandsCls();

            foreach (ClientBrandsCls _plm in plm)
            {
                ClientBrandsCls = new ClientBrandsCls();

                ClientBrandsCls.BrandId = _plm.BrandId;
                ClientBrandsCls.BrandName = _plm.BrandName;
                ClientBrandsCls.ExpireDescription = _plm.ExpireDescription;
                ClientBrandsCls.CompanyName = _plm.CompanyName;

                if (_plm.Distributor == 1)
                {
                    ClientBrandsCls.Type = "Distribuidor autorizado";
                }

                if (_plm.Representative == 1)
                {
                    ClientBrandsCls.Type = "Representante exclusivo";
                }

                cm.Add(ClientBrandsCls);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();

                cm[0].NumberEdition = ed[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }
            ReportDataSource rd = new ReportDataSource("GetBrandsByClient", cm);
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

        public ActionResult GetReportAdvertsByClient(string id)
        {
            SessionSpecialitiesSM index = (SessionSpecialitiesSM)Session["SessionSpecialitiesSM"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                GetClientAdverts.QtyAdvers = _plm.QtyAdvers;


                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetReportAdvertsByEdition(string id)
        {
            SessionSpecialitiesSM index = (SessionSpecialitiesSM)Session["SessionSpecialitiesSM"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetGeneralReport(string id)
        {
            SessionReports index = (SessionReports)Session["SessionReports"];
            SearchDate SearchDate = (SearchDate)Session["SearchDate"];

            DateTime Date = Convert.ToDateTime(SearchDate.Date);
            int EditionId = index.EId;
            int UserId = index.UId;
            int BookId = index.BId;

            string DateTime = Date.ToString();

            DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
            DateTime = DateTime.Replace("/", "-");

            var plm = db.Database.SqlQuery<GetReportEditionClients>("plm_spGetReportEditionClients @Date='" + DateTime.ToString() + "', @UserId=" + UserId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetGeneralReportEditionClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetReportEditionClients> cm = new List<GetReportEditionClients>();
            GetReportEditionClients GetReportEditionClients = new GetReportEditionClients();

            foreach (GetReportEditionClients _plm in plm)
            {
                GetReportEditionClients = new GetReportEditionClients();

                GetReportEditionClients.ClientId = _plm.ClientId;
                GetReportEditionClients.ClientTypeId = _plm.ClientTypeId;
                GetReportEditionClients.CompanyName = _plm.CompanyName;
                GetReportEditionClients.Date = DateTime;
                GetReportEditionClients.EditionId = _plm.EditionId;
                GetReportEditionClients.NumberEdition = _plm.NumberEdition;
                GetReportEditionClients.Page = _plm.Page;
                GetReportEditionClients.TypeName = _plm.TypeName;


                cm.Add(GetReportEditionClients);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();

                cm[0].NumberEdition = ed[0];

                var us = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].UserName = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

            }

            ReportDataSource rd = new ReportDataSource("GeneralReportEditionClients", cm);
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

        public ActionResult GetReassingCategoriesReport(string id, int ProductId)
        {
            SessionProductId index = (SessionProductId)Session["SessionProductId"];

            indexsalesmodule ind = (indexsalesmodule)Session["indexsalesmodule"];
            int EditionId = ind.EId;

            int ClientId = ind.ClId;

            ReassignCategoriesClass RC = new ReassignCategoriesClass();

            RC.Asociated = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + "").ToList();

            RC.Reassign = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIR").ToList();

            RC.AddLI = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIA").ToList();

            RC.Add = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=Ventas").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetReassignCategories.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ReassignCategoriesClass> cm = new List<ReassignCategoriesClass>();
            GetReportEditionClients GetReportEditionClients = new GetReportEditionClients();


            if (RC.Asociated.LongCount() > 0)
            {
                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();
                RC.Asociated[0].NumberEdition = ed[0];

                var cl = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.ShortName).ToList();

                RC.Asociated[0].CompanyName = cl[0];

                var p = db.Products.Where(x => x.ProductId == ProductId).Select(x => x.ProductName).ToList();

                RC.Asociated[0].ProductName = p[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                RC.Asociated[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            cm.Add(RC);

            ReportDataSource rd = new ReportDataSource();

            rd.Name = "ReassignCategories";
            rd.Value = cm[0].Reassign;

            lr.DataSources.Add(rd);


            ReportDataSource rd1 = new ReportDataSource();

            rd1.Name = "AsociatedCats";
            rd1.Value = cm[0].Asociated;

            lr.DataSources.Add(rd1);



            ReportDataSource rd2 = new ReportDataSource();

            rd2.Name = "Add";
            rd2.Value = cm[0].Add;

            lr.DataSources.Add(rd2);

            ReportDataSource rd3 = new ReportDataSource();

            rd3.Name = "AddLI";
            rd3.Value = cm[0].AddLI;

            lr.DataSources.Add(rd3);

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


        /*                                      LI                                          */

        public ActionResult GetReassingCategoriesReportLI(string id, int ProductId)
        {
            SessionClasification ind = (SessionClasification)Session["SessionClasification"];
            int EditionId = ind.EId;

            int ClientId = ind.ClId;

            ReassignCategoriesClass RC = new ReassignCategoriesClass();

            RC.Asociated = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + "").ToList();

            RC.Reassign = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIR").ToList();

            RC.AddLI = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIA").ToList();

            RC.Add = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=Ventas").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetReassignCategories.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ReassignCategoriesClass> cm = new List<ReassignCategoriesClass>();
            GetReportEditionClients GetReportEditionClients = new GetReportEditionClients();


            if (RC.Asociated.LongCount() > 0)
            {
                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();
                RC.Asociated[0].NumberEdition = ed[0];

                var cl = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.ShortName).ToList();

                RC.Asociated[0].CompanyName = cl[0];

                var p = db.Products.Where(x => x.ProductId == ProductId).Select(x => x.ProductName).ToList();

                RC.Asociated[0].ProductName = p[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                RC.Asociated[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            cm.Add(RC);

            ReportDataSource rd = new ReportDataSource();

            rd.Name = "ReassignCategories";
            rd.Value = cm[0].Reassign;

            lr.DataSources.Add(rd);


            ReportDataSource rd1 = new ReportDataSource();

            rd1.Name = "AsociatedCats";
            rd1.Value = cm[0].Asociated;

            lr.DataSources.Add(rd1);



            ReportDataSource rd2 = new ReportDataSource();

            rd2.Name = "Add";
            rd2.Value = cm[0].Add;

            lr.DataSources.Add(rd2);

            ReportDataSource rd3 = new ReportDataSource();

            rd3.Name = "AddLI";
            rd3.Value = cm[0].AddLI;

            lr.DataSources.Add(rd3);

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

        public ActionResult GetProductsByClientWithCategoriesLI(string id)
        {
            SessionClasification index = (SessionClasification)Session["SessionClasification"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsCategoriesByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }
            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReportProductsByEditionWithCategoriesLI(string id)
        {
            SessionClasification index = (SessionClasification)Session["SessionClasification"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductCategoriesByEdition @EditionId=" + editionid + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }
            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetCategoriesByProductReportLI(string id, int? ProductId)
        {
            SessionClasification ind = (SessionClasification)Session["SessionClasification"];
            int EditionId = ind.EId;

            int ClientId = ind.ClId;

            ReassignCategoriesClass RC = new ReassignCategoriesClass();

            RC.Asociated = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + "").ToList();

            //RC.Reassign = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIR").ToList();

            //RC.AddLI = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIA").ToList();

            //RC.Add = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=Ventas").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetCategoriesByProduct.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ReassignCategoriesClass> cm = new List<ReassignCategoriesClass>();
            GetReportEditionClients GetReportEditionClients = new GetReportEditionClients();


            if (RC.Asociated.LongCount() > 0)
            {
                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();
                RC.Asociated[0].NumberEdition = ed[0];

                var cl = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.ShortName).ToList();

                RC.Asociated[0].CompanyName = cl[0];

                var p = db.Products.Where(x => x.ProductId == ProductId).Select(x => x.ProductName).ToList();

                RC.Asociated[0].ProductName = p[0];
            }

            cm.Add(RC);


            ReportDataSource rd = new ReportDataSource();

            rd.Name = "AsociatedCats";
            rd.Value = cm[0].Asociated;

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

        /*                                      PRODUCCIÓN                                          */

        public ActionResult GetBranchsByClientProd(string id)
        {
            SessionBranchProd index = (SessionBranchProd)Session["SessionBranchProd"];
            int editionid = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetBrandsByClient>("plm_spGetParticipantBranchsByClient @EditionId=" + editionid + ", @ClientId=" + ClientId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBranchByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetBrandsByClient> cm = new List<GetBrandsByClient>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }
            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("BranchByClient", cm);
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

        public ActionResult GetReportAdvertsByClientProd(string id)
        {
            SessionAdvertsProd index = (SessionAdvertsProd)Session["SessionAdvertsProd"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                GetClientAdverts.QtyAdvers = _plm.QtyAdvers;


                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetReportAdvertsByClientLI(string id)
        {
            SessionClasification index = (SessionClasification)Session["SessionClasification"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                GetClientAdverts.QtyAdvers = _plm.QtyAdvers;


                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetReportAdvertsByEditionLI(string id)
        {
            SessionClasification index = (SessionClasification)Session["SessionClasification"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }
            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetReportAdvertsByEditionProd(string id)
        {
            SessionAdvertsProd index = (SessionAdvertsProd)Session["SessionAdvertsProd"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<GetClientAdverts>("plm_spGetReportAdvertsByClient @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAdvertsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetClientAdverts> cm = new List<GetClientAdverts>();
            GetClientAdverts GetClientAdverts = new GetClientAdverts();

            int Count = 0;

            foreach (GetClientAdverts _plm in plm)
            {
                GetClientAdverts = new GetClientAdverts();

                GetClientAdverts.EditionId = _plm.EditionId;
                GetClientAdverts.NumberEdition = _plm.NumberEdition;
                GetClientAdverts.ClientId = _plm.ClientId;
                GetClientAdverts.CompanyName = _plm.CompanyName;
                GetClientAdverts.CategoryThreeId = _plm.CategoryThreeId;
                GetClientAdverts.CategoryThree = _plm.CategoryThree;

                GetClientAdverts.AdvertId = _plm.AdvertId;
                GetClientAdverts.AdvertName = _plm.AdvertName;
                GetClientAdverts.AdvertDescription = _plm.AdvertDescription;
                GetClientAdverts.AdvertFile = _plm.AdvertFile;
                GetClientAdverts.Description = _plm.Description;

                cm.Add(GetClientAdverts);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }
            ReportDataSource rd = new ReportDataSource("AdvertsByClient", cm);
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

        public ActionResult GetReportBrandsByClientProd(string id)
        {
            SessionBrandsProd index = (SessionBrandsProd)Session["SessionBrandsProd"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @ClientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ClientBrandsCls> cm = new List<ClientBrandsCls>();
            ClientBrandsCls ClientBrandsCls = new ClientBrandsCls();

            foreach (ClientBrandsCls _plm in plm)
            {
                ClientBrandsCls = new ClientBrandsCls();

                ClientBrandsCls.BrandId = _plm.BrandId;
                ClientBrandsCls.BrandName = _plm.BrandName;
                ClientBrandsCls.ExpireDescription = _plm.ExpireDescription;

                if (_plm.Distributor == 1)
                {
                    ClientBrandsCls.Type = "Distribuidor autorizado";
                }

                if (_plm.Representative == 1)
                {
                    ClientBrandsCls.Type = "Representante exclusivo";
                }

                cm.Add(ClientBrandsCls);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var cn = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.CompanyName).ToList();

                cm[0].CompanyName = cn[0];

                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();

                cm[0].NumberEdition = ed[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            ReportDataSource rd = new ReportDataSource("GetBrandsByClient", cm);
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

        public ActionResult GetReportBrandsByEditionProd(string id)
        {
            SessionBrandsProd index = (SessionBrandsProd)Session["SessionBrandsProd"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int BookId = index.BId;

            var plm = db.Database.SqlQuery<ClientBrandsCls>("plm_spGetBrandsByClient @EditionId=" + EditionId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetBrandsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ClientBrandsCls> cm = new List<ClientBrandsCls>();
            ClientBrandsCls ClientBrandsCls = new ClientBrandsCls();

            foreach (ClientBrandsCls _plm in plm)
            {
                ClientBrandsCls = new ClientBrandsCls();

                ClientBrandsCls.BrandId = _plm.BrandId;
                ClientBrandsCls.BrandName = _plm.BrandName;
                ClientBrandsCls.ExpireDescription = _plm.ExpireDescription;
                ClientBrandsCls.CompanyName = _plm.CompanyName;

                if (_plm.Distributor == 1)
                {
                    ClientBrandsCls.Type = "Distribuidor autorizado";
                }

                if (_plm.Representative == 1)
                {
                    ClientBrandsCls.Type = "Representante exclusivo";
                }

                cm.Add(ClientBrandsCls);
            }

            if (plm.LongCount() > 0)
            {
                cm[0].Count = Convert.ToInt32(plm.LongCount());

                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();

                cm[0].NumberEdition = ed[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                cm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            ReportDataSource rd = new ReportDataSource("GetBrandsByClient", cm);
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

        public ActionResult GetProductsByClientProd(string id)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

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

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsByEdition @EditionId=" + editionid + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductsByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetProductsByClientWithCategoriesProd(string id)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];
            int EditionId = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsCategoriesByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;

            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReportProductsByEditionWithCategoriesProd(string id)
        {
            SessionProduction index = (SessionProduction)Session["SessionProduction"];
            int editionid = index.EId;
            int clientid = index.ClId;
            int book = index.BId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductCategoriesByEdition @EditionId=" + editionid + "").ToList();

            var plms = plm.Select(x => x.ProductId).Distinct().ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetProductCategoriesByEdition.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
                plm[0].CountProduct = Convert.ToInt32(plms.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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

        public ActionResult GetReassingCategoriesReportProd(string id, int ProductId)
        {
            SessionProductId index = (SessionProductId)Session["SessionProductId"];

            SessionProduction ind = (SessionProduction)Session["SessionProduction"];
            int EditionId = ind.EId;

            int ClientId = ind.ClId;

            ReassignCategoriesClass RC = new ReassignCategoriesClass();

            RC.Asociated = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + "").ToList();

            RC.Reassign = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIR").ToList();

            RC.AddLI = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=LIA").ToList();

            RC.Add = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ProductId='" + ProductId + "', @ClientId=" + ClientId + ", @Module=Ventas").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetReassignCategories.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<ReassignCategoriesClass> cm = new List<ReassignCategoriesClass>();
            GetReportEditionClients GetReportEditionClients = new GetReportEditionClients();


            if (RC.Asociated.LongCount() > 0)
            {
                var ed = db.Editions.Where(x => x.EditionId == EditionId).Select(x => x.NumberEdition).ToList();
                RC.Asociated[0].NumberEdition = ed[0];

                var cl = db.Clients.Where(x => x.ClientId == ClientId).Select(x => x.ShortName).ToList();

                RC.Asociated[0].CompanyName = cl[0];

                var p = db.Products.Where(x => x.ProductId == ProductId).Select(x => x.ProductName).ToList();

                RC.Asociated[0].ProductName = p[0];

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                RC.Asociated[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }

            cm.Add(RC);

            ReportDataSource rd = new ReportDataSource();

            rd.Name = "ReassignCategories";
            rd.Value = cm[0].Reassign;

            lr.DataSources.Add(rd);


            ReportDataSource rd1 = new ReportDataSource();

            rd1.Name = "AsociatedCats";
            rd1.Value = cm[0].Asociated;

            lr.DataSources.Add(rd1);



            ReportDataSource rd2 = new ReportDataSource();

            rd2.Name = "Add";
            rd2.Value = cm[0].Add;

            lr.DataSources.Add(rd2);

            ReportDataSource rd3 = new ReportDataSource();

            rd3.Name = "AddLI";
            rd3.Value = cm[0].AddLI;

            lr.DataSources.Add(rd3);

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


        /*                                      REPORTES GENERALES                                          */

        public List<ClassReport> LU(int UserId, string DateTime, string Table)
        {
            List<ClassReport> LAL = new List<ClassReport>();

            var HashKey = System.Configuration.ConfigurationManager.AppSettings["HashKey"].ToString();

            LAL = dbusers.Database.SqlQuery<ClassReport>("plm_spGetActivityByApplicationByUser @HashKey='" + HashKey + "', @UserId=" + UserId + ", @Date='" + DateTime + "',@Table='" + Table + "'").ToList();

            return LAL;
        }

        public List<GetReportEditionClientProducts> LECP(string DateTime1, int EditionId, int ClientId, int ProductId)
        {
            List<GetReportEditionClientProducts> ECP = db.Database.SqlQuery<GetReportEditionClientProducts>("plm_spGetReportEditionClientProducts @Date='" + DateTime1.ToString() + "', @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

            return ECP;
        }

        public List<GetReportClientAdvertsCategories> LCAC(string DateTime1, int EditionId, int ClientId, int AdvertId, int CategoryThreeId, int AdvertTypeId)
        {
            List<GetReportClientAdvertsCategories> CAC = db.Database.SqlQuery<GetReportClientAdvertsCategories>("plm_spGetReportClientAdvertCategories @Date='" + DateTime1.ToString() + "', @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @AdvertId=" + AdvertId + ", @CategoryThreeId=" + CategoryThreeId + ", @AdvertTypeId=" + AdvertTypeId + "").ToList();

            return CAC;
        }

        public List<GetReportClientBrands> LCB(string DateTime1, int EditionId, int ClientId, int BrandId)
        {
            List<GetReportClientBrands> CB = db.Database.SqlQuery<GetReportClientBrands>("plm_spGetReportClientBrands @Date='" + DateTime1.ToString() + "', @EditionId=" + EditionId + ", @ClientId=" + ClientId + ", @BrandId=" + BrandId + "").ToList();

            return CB;
        }

        public ActionResult GetGeneralReportEditionClientProducts(string id)
        {
            SessionReports index = (SessionReports)Session["SessionReports"];
            SearchDate SearchDate = (SearchDate)Session["SearchDate"];

            int EditionId = index.EId;
            int UserId = index.UId;
            int BookId = index.BId;

            List<GetReportEditionClientProducts> plm = new List<GetReportEditionClientProducts>();

            List<GetReportEditionClientProducts> cm = new List<GetReportEditionClientProducts>();

            List<GetReportEditionClientProducts> ECP = new List<GetReportEditionClientProducts>();

            if (SearchDate != null)
            {


                DateTime Date = Convert.ToDateTime(SearchDate.Date);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "EditionClientProducts");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {

                        List<ListFieldsToReport> LS = GetProductId(_al.PrimaryKeyAffected);

                        int ProductId = LS[0].ID;

                        List<ListFieldsToReport> LSE = GetEditionId(_al.PrimaryKeyAffected, LS[0].Attribute);

                        int EditionIdS = LSE[0].ID;

                        List<ListFieldsToReport> LSC = GetClientId(_al.PrimaryKeyAffected, LS[0].Attribute, LSE[0].Attribute);

                        int ClientId = LSC[0].ID;

                        ECP = LECP(_al.Date, EditionId, ClientId, ProductId);

                        if (ECP.LongCount() > 0)
                        {
                            plm.Add(ECP[0]);
                        }
                    }
                }


                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime.Replace("-", "/");

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }
            else
            {

                DateTime Date = Convert.ToDateTime(System.DateTime.Now);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "EditionClientProducts");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {

                        List<ListFieldsToReport> LS = GetProductId(_al.PrimaryKeyAffected);

                        int ProductId = LS[0].ID;

                        List<ListFieldsToReport> LSE = GetEditionId(_al.PrimaryKeyAffected, LS[0].Attribute);

                        int EditionIdS = LSE[0].ID;

                        List<ListFieldsToReport> LSC = GetClientId(_al.PrimaryKeyAffected, LS[0].Attribute, LSE[0].Attribute);

                        int ClientId = LSC[0].ID;

                        ECP = LECP(_al.Date, EditionId, ClientId, ProductId);

                        if (ECP.LongCount() > 0)
                        {
                            plm.Add(ECP[0]);
                        }
                    }
                }


                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = DateTime1;//Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/0" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = DateTime1;// Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;// Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }



            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetGeneralReportEditionClientProducts.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            ReportDataSource rd = new ReportDataSource("GeneralReportEditionClientProducts", cm);
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

            Session["SearchDate"] = null;

            return File(renderedBytes, mimeType);
        }

        public List<ListFieldsToReport> GetProductId(string PrimaryKeyAffected)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();
            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(ProductId,", "");

            int ProductId = Convert.ToInt32(field);

            l.ID = ProductId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetEditionId(string PrimaryKeyAffected, string Attribute)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(EditionId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetClientId(string PrimaryKeyAffected, string Attribute, string Attribute1)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + ")";

            field = field.Replace("(ClientId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public ActionResult GetGeneralReportClientAdverts(string id)
        {
            SessionReports index = (SessionReports)Session["SessionReports"];
            SearchDate SearchDate = (SearchDate)Session["SearchDate"];

            int EditionId = index.EId;
            int UserId = index.UId;
            int BookId = index.BId;

            List<GetReportClientAdvertsCategories> plm = new List<GetReportClientAdvertsCategories>();

            List<GetReportClientAdvertsCategories> cm = new List<GetReportClientAdvertsCategories>();

            List<GetReportClientAdvertsCategories> CAC = new List<GetReportClientAdvertsCategories>();

            if (SearchDate != null)
            {


                DateTime Date = Convert.ToDateTime(SearchDate.Date);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "ClientAdvertCategories");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {

                        List<ListFieldsToReport> LSC = GetClientIdAdverts(_al.PrimaryKeyAffected);

                        int ClientId = LSC[0].ID;

                        List<ListFieldsToReport> LSA = GetAdvertId(_al.PrimaryKeyAffected, LSC[0].Attribute);

                        int AdvertId = LSA[0].ID;

                        List<ListFieldsToReport> LSEA = GetEditionIdAdverts(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute);

                        int EditionIds = LSEA[0].ID;

                        List<ListFieldsToReport> LSEC = GetCategoryThreeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute);

                        int CategoryThreeId = LSEC[0].ID;

                        List<ListFieldsToReport> LSAT = GetAdvertTypeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute, LSEC[0].Attribute);

                        int AdvertTypeId = LSAT[0].ID;

                        CAC = LCAC(_al.Date, EditionId, ClientId, AdvertId, CategoryThreeId, AdvertTypeId);

                        if (CAC.LongCount() > 0)
                        {
                            plm.Add(CAC[0]);
                        }
                    }
                }

                plm = plm.OrderBy(x => x.CompanyName).ThenBy(z => z.AdvertName).ToList();

                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime.Replace("-", "/");

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }
            else
            {

                DateTime Date = Convert.ToDateTime(System.DateTime.Now);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "ClientAdvertCategories");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {
                        List<ListFieldsToReport> LSC = GetClientIdAdverts(_al.PrimaryKeyAffected);

                        int ClientId = LSC[0].ID;

                        List<ListFieldsToReport> LSA = GetAdvertId(_al.PrimaryKeyAffected, LSC[0].Attribute);

                        int AdvertId = LSA[0].ID;

                        List<ListFieldsToReport> LSEA = GetEditionIdAdverts(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute);

                        int EditionIds = LSEA[0].ID;

                        List<ListFieldsToReport> LSEC = GetCategoryThreeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute);

                        int CategoryThreeId = LSEC[0].ID;

                        List<ListFieldsToReport> LSAT = GetAdvertTypeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute, LSEC[0].Attribute);

                        int AdvertTypeId = LSAT[0].ID;

                        CAC = LCAC(_al.Date, EditionId, ClientId, AdvertId, CategoryThreeId, AdvertTypeId);

                        if (CAC.LongCount() > 0)
                        {
                            plm.Add(CAC[0]);
                        }
                    }
                }

                plm = plm.OrderBy(x => x.CompanyName).ThenBy(z => z.AdvertName).ToList();

                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = DateTime1;//Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/0" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = DateTime1;// Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;// Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }



            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetGeneralReportClientAdvertsCategories.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            ReportDataSource rd = new ReportDataSource("GetReportClientAdvertsCategories", cm);
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

            Session["SearchDate"] = null;

            return File(renderedBytes, mimeType);
        }

        public List<ListFieldsToReport> GetClientIdAdverts(string PrimaryKeyAffected)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();


            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(ClientId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetAdvertId(string PrimaryKeyAffected, string Attribute)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(AdvertId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetEditionIdAdverts(string PrimaryKeyAffected, string Attribute, string Attribute1)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(EditionId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetCategoryThreeId(string PrimaryKeyAffected, string Attribute, string Attribute1, string Attribute2)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute2, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(CategoryThreeId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetAdvertTypeId(string PrimaryKeyAffected, string Attribute, string Attribute1, string Attribute2, string Attribute3)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute2, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute3, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(AdvertTypeId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public ActionResult GetGeneralReportClientBrands(string id)
        {
            SessionReports index = (SessionReports)Session["SessionReports"];
            SearchDate SearchDate = (SearchDate)Session["SearchDate"];

            int EditionId = index.EId;
            int UserId = index.UId;
            int BookId = index.BId;

            List<GetReportClientBrands> plm = new List<GetReportClientBrands>();

            List<GetReportClientBrands> cm = new List<GetReportClientBrands>();

            List<GetReportClientBrands> CB = new List<GetReportClientBrands>();

            if (SearchDate != null)
            {

                DateTime Date = Convert.ToDateTime(SearchDate.Date);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "ClientBrands");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {

                        List<ListFieldsToReport> LSC = GetClientIdBrands(_al.PrimaryKeyAffected);

                        int ClientId = LSC[0].ID;


                        List<ListFieldsToReport> LSEA = GetEditionIdBrands(_al.PrimaryKeyAffected, LSC[0].Attribute);

                        int EditionIds = LSEA[0].ID;


                        List<ListFieldsToReport> LSA = GetBrandId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSEA[0].Attribute);

                        int BrandId = LSA[0].ID;


                        //List<ListFieldsToReport> LSEC = GetClientTypeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute);

                        //int ClientTypeId = LSEC[0].ID;

                        CB = LCB(_al.Date, EditionId, ClientId, BrandId);

                        if (CB.LongCount() > 0)
                        {
                            plm.Add(CB[0]);
                        }

                    }
                }

                plm = plm.OrderBy(x => x.CompanyName).ThenBy(z => z.BrandName).ToList();

                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime.Replace("-", "/");

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }
            else
            {

                DateTime Date = Convert.ToDateTime(System.DateTime.Now);

                string DateTime = Date.ToString();

                DateTime = DateTime.Replace(" 12:00:00 a. m.", "");
                DateTime = DateTime.Replace("/", "-");

                String DateTime1 = Date.Year + "-" + Date.Month + "-" + Date.Day;

                List<ClassReport> LAL = LU(UserId, DateTime1, "ClientBrands");

                if (LAL.LongCount() > 0)
                {
                    foreach (ClassReport _al in LAL)
                    {
                        List<ListFieldsToReport> LSC = GetClientIdBrands(_al.PrimaryKeyAffected);

                        int EditionIds = LSC[0].ID;




                        List<ListFieldsToReport> LSEA = GetEditionIdBrands(_al.PrimaryKeyAffected, LSC[0].Attribute);

                        int ClientId = LSEA[0].ID;


                        List<ListFieldsToReport> LSA = GetBrandId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSEA[0].Attribute);

                        int BrandId = LSA[0].ID;


                        //List<ListFieldsToReport> LSEC = GetClientTypeId(_al.PrimaryKeyAffected, LSC[0].Attribute, LSA[0].Attribute, LSEA[0].Attribute);

                        //int ClientTypeId = LSEC[0].ID;


                        CB = LCB(_al.Date, EditionId, ClientId, BrandId);

                        if (CB.LongCount() > 0)
                        {
                            plm.Add(CB[0]);
                        }
                    }
                }

                plm = plm.OrderBy(x => x.CompanyName).ThenBy(z => z.BrandName).ToList();

                if (plm.LongCount() > 0)
                {
                    cm = plm.ToList();

                    cm[0].Count = Convert.ToInt32(plm.LongCount());

                    var us = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                    cm[0].Executive = (us[0].Name + " " + us[0].LastName + " " + us[0].SecondLastName).ToString();

                    cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);

                    int Day = System.DateTime.Now.Month;

                    if ((Day != 10) && (Day != 11) && (Day != 12))
                    {
                        cm[0].DateOfReport = DateTime1;//Convert.ToString(System.DateTime.Now.Day + "/0" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;//Convert.ToString(DateTime.Now.Day + "/0" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }
                    else
                    {
                        cm[0].DateOfReport = DateTime1;// Convert.ToString(System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year);
                        cm[0].Date = DateTime1;// Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                    }

                    string NickName = System.Configuration.ConfigurationManager.AppSettings["AdminReports"];

                    var usr = dbusers.Users.Where(x => x.NickName == NickName).ToList();

                    cm[0].UserName = (usr[0].Name + " " + usr[0].LastName + "" + usr[0].SecondLastName);
                }

            }



            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetGeneralReportClientBrands.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            ReportDataSource rd = new ReportDataSource("GetReportClientBrands", cm);
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

            Session["SearchDate"] = null;

            return File(renderedBytes, mimeType);
        }

        public List<ListFieldsToReport> GetEditionIdBrands(string PrimaryKeyAffected, string Attribute)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(ClientId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetClientIdBrands(string PrimaryKeyAffected)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(EditionId,", "");

            int BrandId = Convert.ToInt32(field);

            l.ID = BrandId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetBrandId(string PrimaryKeyAffected, string Attribute, string Attribute1)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(BrandId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }

        public List<ListFieldsToReport> GetClientTypeId(string PrimaryKeyAffected, string Attribute, string Attribute1, string Attribute2)
        {
            List<ListFieldsToReport> LS = new List<ListFieldsToReport>();
            ListFieldsToReport l = new ListFieldsToReport();

            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute1, "");
            PrimaryKeyAffected = PrimaryKeyAffected.Replace(Attribute2, "");

            string field = PrimaryKeyAffected.Substring(0, PrimaryKeyAffected.IndexOf(")"));

            l.Attribute = field + "),";

            field = field.Replace("(CategoryThreeId,", "");

            int EditionId = Convert.ToInt32(field);

            l.ID = EditionId;

            LS.Add(l);

            return LS;
        }



        /*                                      REPORTE PRODUCTOS INTERNACIONALES                                          */

        public ActionResult GetInternationalProductsByClients(string id)
        {
            SessionIP index = (SessionIP)Session["SessionIP"];
            int editionid = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            //var plm = db.Database.SqlQuery<GetBrandsByClient>("plm_spGetParticipantBranchsByClient @EditionId=" + editionid + ", @ClientId=" + ClientId + "").ToList();

            var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + ClientId + ", @EditionId=" + editionid + ", @Report=SI").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetInternationalProductsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetProductsByClient> cm = new List<GetProductsByClient>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
            }

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetInternationalProductsByClient", cm);
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

        public ActionResult GetInternationalProductsByClientsProd(string id)
        {
            SessionIPProd index = (SessionIPProd)Session["SessionIPProd"];
            int editionid = index.EId;
            int ClientId = index.ClId;
            int book = index.BId;

            //var plm = db.Database.SqlQuery<GetBrandsByClient>("plm_spGetParticipantBranchsByClient @EditionId=" + editionid + ", @ClientId=" + ClientId + "").ToList();

            var plm = db.Database.SqlQuery<GetProductsByClient>("plm_spGetInternationalProductsByClient @clientId=" + ClientId + ", @EditionId=" + editionid + ", @Report=SI").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetInternationalProductsByClient.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetProductsByClient> cm = new List<GetProductsByClient>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());
            }

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetInternationalProductsByClient", cm);
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

        public ActionResult GetInternationalClients(string id, int? EditionId)
        {

            if ((!Request.IsAuthenticated) || (EditionId == null))
            {
                return RedirectToAction("Logout", "Login");
            }

            List<ClientTypes> LC = db.ClientTypes.Where(x => x.TypeName == "INTERNACIONAL").ToList();

            var plm = db.Database.SqlQuery<GetInternationalClientsRpt>("plm_spGetInternationalClients @EditionId=" + EditionId + ",@ClientTypeId=" + LC[0].ClientTypeId + ",@Type=P").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetInternationalClients.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetInternationalClientsRpt> cm = new List<GetInternationalClientsRpt>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                List<Users> LU = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;

                List<Editions> LE = db.Editions.Where(x => x.EditionId == EditionId).ToList();

                plm[0].NumberEdition = LE[0].NumberEdition;
            }

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("GetInternationalClients", cm);
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



        /*                                      CATEGORÍAS SOLICITADAS                                                      */

        public ActionResult GetAddedWorksByClientId(string id, int ClientId)
        {
            var plm = db.Database.SqlQuery<GetAddedWorksByClientId>("plm_spGetWorksByClientByEdition @clientId=" + ClientId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAddedWorksByClientId.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetAddedWorksByClientId> cm = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm1 = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm2 = new List<GetAddedWorksByClientId>();


            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var dst = plm.Select(x => x.ProductId).Distinct().ToList();

                plm[0].CountP = Convert.ToInt32(dst.LongCount());

                List<Users> LU = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;

                cm2 = GetData(LU, ClientId, Convert.ToInt32(plm.LongCount()), Convert.ToInt32(dst.LongCount()));
            }

            cm = plm.Where(x => x.Active == false).ToList();
            cm1 = plm.Where(x => x.Active == true).ToList();


            ReportDataSource rd = new ReportDataSource("GetAddedWorksByClientId", cm);
            lr.DataSources.Add(rd);

            ReportDataSource rd1 = new ReportDataSource("GetAddedWorksByClientIdClosed", cm1);
            lr.DataSources.Add(rd1);

            ReportDataSource rd2 = new ReportDataSource("GetAddedWorksByClientIdData", cm2);
            lr.DataSources.Add(rd2);


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

        public ActionResult GetAddedWorksByEditionId(string id, int ClientId, int EditionId)
        {

            List<ClientTypes> ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

            var plm = db.Database.SqlQuery<GetAddedWorksByClientId>("plm_spGetWorksByClientByEdition @clientId=" + ClientId + ", @EditionId=" + EditionId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAddedWorksByEditionId.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetAddedWorksByClientId> cm = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm1 = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm2 = new List<GetAddedWorksByClientId>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var dst = plm.Select(x => x.ProductId).Distinct().ToList();

                plm[0].CountP = Convert.ToInt32(dst.LongCount());

                List<Users> LU = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;

                cm2 = GetDataByEdition(LU, Convert.ToInt32(plm.LongCount()), Convert.ToInt32(dst.LongCount()), EditionId);

            }

            cm = plm.Where(x => x.Active == false).ToList();
            cm1 = plm.Where(x => x.Active == true).ToList();

            ReportDataSource rd = new ReportDataSource("GetAddedWorksByClientId", cm);
            lr.DataSources.Add(rd);

            ReportDataSource rd1 = new ReportDataSource("GetAddedWorksByClientIdClosed", cm1);
            lr.DataSources.Add(rd1);

            ReportDataSource rd2 = new ReportDataSource("GetAddedWorksByClientIdData", cm2);
            lr.DataSources.Add(rd2);

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

        public List<GetAddedWorksByClientId> GetData(List<Users> LU, int ClientId, int Count, int CountP)
        {
            List<GetAddedWorksByClientId> LS = new List<GetAddedWorksByClientId>();
            GetAddedWorksByClientId GetAddedWorksByClientId = new GetAddedWorksByClientId();

            string CompanyName = GetCompanyName(ClientId);


            GetAddedWorksByClientId = new GetAddedWorksByClientId();

            GetAddedWorksByClientId.Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            GetAddedWorksByClientId.CompanyName = CompanyName;
            GetAddedWorksByClientId.Count = Count;
            GetAddedWorksByClientId.CountP = CountP;

            LS.Add(GetAddedWorksByClientId);

            return LS;
        }

        public string GetCompanyName(int ClientId)
        {
            List<Clients> LC = db.Clients.Where(x => x.ClientId == ClientId).ToList();

            String CompanyName = LC[0].CompanyName;

            return CompanyName;
        }

        public List<GetAddedWorksByClientId> GetDataByEdition(List<Users> LU, int Count, int CountP, int EditionId)
        {
            List<GetAddedWorksByClientId> LS = new List<GetAddedWorksByClientId>();
            GetAddedWorksByClientId GetAddedWorksByClientId = new GetAddedWorksByClientId();

            int NumberOfEdition = GetNumberOfEdition(EditionId);

            GetAddedWorksByClientId = new GetAddedWorksByClientId();

            GetAddedWorksByClientId.Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            GetAddedWorksByClientId.Count = Count;
            GetAddedWorksByClientId.CountP = CountP;
            GetAddedWorksByClientId.NumberEdition = NumberOfEdition;

            LS.Add(GetAddedWorksByClientId);

            return LS;
        }

        public int GetNumberOfEdition(int EditionId)
        {
            List<Editions> LE = db.Editions.Where(x => x.EditionId == EditionId).ToList();

            int NumberOfEdition = LE[0].NumberEdition;

            return NumberOfEdition;
        }

        public ActionResult GetGlobarReportWorks(string id, string NickName)
        {
            List<Users> LU = new List<Users>();
            List<Users> LU1 = new List<Users>();

            SessionReport SessionReport = (SessionReport)Session["SessionReport"];

            LU1 = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

            String Adviser = String.Empty;

            if (SessionReport != null)
            {
                NickName = SessionReport.NickName;
            }

            if (string.IsNullOrEmpty(NickName))
            {
                LU = dbusers.Users.Where(x => x.UserId == c.userId).ToList();
            }
            else
            {
                LU = dbusers.Users.Where(x => x.NickName == NickName).ToList();
                Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            }

            var plm = db.Database.SqlQuery<GetAddedWorksByClientId>("plm_spGetGlobalReportWorks @NickName=" + LU[0].NickName + "").ToList();

            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/GetReport"), "GetAddedWorksGlobal.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("error");
            }

            List<GetAddedWorksByClientId> cm = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm1 = new List<GetAddedWorksByClientId>();
            List<GetAddedWorksByClientId> cm2 = new List<GetAddedWorksByClientId>();

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var dst = plm.Select(x => x.ProductId).Distinct().ToList();

                plm[0].CountP = Convert.ToInt32(dst.LongCount());

                plm[0].Adviser = LU1[0].Name + " " + LU1[0].LastName + " " + LU1[0].SecondLastName;

                cm2 = GetGlobalData(LU1, Convert.ToInt32(plm.LongCount()), Convert.ToInt32(dst.LongCount()), Adviser);

            }

            cm = plm.Where(x => x.Active == false).ToList();
            cm1 = plm.Where(x => x.Active == true).ToList();

            ReportDataSource rd = new ReportDataSource("GetAddedWorksByClientId", cm);
            lr.DataSources.Add(rd);

            ReportDataSource rd1 = new ReportDataSource("GetAddedWorksByClientIdClosed", cm1);
            lr.DataSources.Add(rd1);

            ReportDataSource rd2 = new ReportDataSource("GetAddedWorksByClientIdData", cm2);
            lr.DataSources.Add(rd2);

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

        public List<GetAddedWorksByClientId> GetGlobalData(List<Users> LU, int Count, int CountP, string Adviser)
        {
            List<GetAddedWorksByClientId> LS = new List<GetAddedWorksByClientId>();
            GetAddedWorksByClientId GetAddedWorksByClientId = new GetAddedWorksByClientId();

            GetAddedWorksByClientId = new GetAddedWorksByClientId();

            GetAddedWorksByClientId.Adviser = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            GetAddedWorksByClientId.Count = Count;
            GetAddedWorksByClientId.CountP = CountP;

            if (!string.IsNullOrEmpty(Adviser))
            {
                GetAddedWorksByClientId.Adviser1 = Adviser;
            }
            else
            {
                GetAddedWorksByClientId.Adviser1 = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            }

            LS.Add(GetAddedWorksByClientId);

            return LS;
        }

        public JsonResult GetUser(string User, string NickName)
        {

            if ((string.IsNullOrEmpty(User)) || (User == "0"))
            {
                Session["SessionReport"] = null;
            }
            else
            {
                int UserId = int.Parse(User);

                List<Users> LU = dbusers.Users.Where(x => x.UserId == UserId).ToList();

                SessionReport SessionReport = new SessionReport(LU[0].NickName, UserId);
                Session["SessionReport"] = SessionReport;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /*                                  PRODUCTOS DESCRITOS                                                             */

        public ActionResult GetDescribedProductsByClient(string id, int? ClientId, int? EditionId)
        {
            //  int EditionId = index.EId;
            //  int ClientId = index.ClId;

            var plm = db.Database.SqlQuery<GetProductsByClientReport>("plm_spGetProductsByClientToReport @clientId=" + ClientId + ", @EditionId=" + EditionId + "").ToList();

            plm = plm.Where(x => x.MP == "SI").ToList();

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

            if (plm.LongCount() > 0)
            {
                plm[0].Count = Convert.ToInt32(plm.LongCount());

                var u = dbusers.Users.Where(x => x.UserId == c.userId).ToList();

                plm[0].Adviser = u[0].Name + " " + u[0].LastName + " " + u[0].SecondLastName;
            }



            List<GetProductsByClientReport> cm = new List<GetProductsByClientReport>();

            cm = plm.ToList();

            ReportDataSource rd = new ReportDataSource("ProductsByClient", cm);
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