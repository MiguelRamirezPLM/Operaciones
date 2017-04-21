using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agronet.Models;
using Agronet.Models.Sessions;
using System.IO;

namespace Agronet.Controllers.Production
{
    public class ProductionController : Controller
    {

        DEAQ db = new DEAQ();

        public ActionResult Index(string CountryId, string BookId, string EditionId, string DivisionId, string ProductName)
        {
            sessionPROD ind = (sessionPROD)Session["sessionPROD"];
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Book = int.Parse(BookId);
                int Edition = int.Parse(EditionId);
                int Division = int.Parse(DivisionId);

                sessionPROD sessionPROD = new sessionPROD(Country, Book, Edition, Division);
                Session["sessionPROD"] = sessionPROD;

                List<ProductsByDivision> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameSM SearchProductNameSM = new SearchProductNameSM(ProductName);
                    Session["SearchProductNameSM"] = SearchProductNameSM;
                }
                else
                {
                    Session["SearchProductNameSM"] = null;
                }


                return View(LS);
            }

            if (ind != null)
            {
                int Country = ind.CountryId;
                int Book = ind.BookId;
                int Edition = ind.EditionId;
                int Division = ind.DivisionId;

                sessionPROD sessionPROD = new sessionPROD(Country, Book, Edition, Division);
                Session["sessionPROD"] = sessionPROD;

                List<ProductsByDivision> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameSM SearchProductNameSM = new SearchProductNameSM(ProductName);
                    Session["SearchProductNameSM"] = SearchProductNameSM;
                }
                else
                {
                    Session["SearchProductNameSM"] = null;
                }

                return View(LS);
            }
            else
            {
                int Country = 0;
                int Edition = 0;
                int Division = 0;

                List<ProductsByDivision> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + "").ToList();

                return View(LS);
            }
        }

        public ActionResult ProductShots(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            sessionPS sessionPS = new sessionPS(ProductId, PharmaFormId, CategoryId, DivisionId);
            Session["sessionPS"] = sessionPS;

            List<GetProductImages> LS = db.Database.SqlQuery<GetProductImages>("plm_spGetProductImagesByKey @ProductId=" + ProductId + ", @PharmaFormId=" + PharmaFormId + ", @CategoryId=" + CategoryId + ", @DivisionId=" + DivisionId + "").ToList();

            return View(LS);
        }

        public ActionResult ShowImages(string ProductShot, int? ProductImageId, int SizeId)
        {
            List<ProductImageSizes> LPIS = db.ProductImageSizes.Where(x => x.ProductImageId == ProductImageId && x.ImageSizeId == SizeId).ToList();

            if (LPIS.LongCount() > 0)
            {
                foreach (ProductImageSizes item in LPIS)
                {
                    List<ImageSizes> LIS = db.ImageSizes.Where(x => x.ImageSizeId == SizeId).ToList();

                    foreach (ImageSizes item1 in LIS)
                    {
                        var root = Path.Combine(Server.MapPath("~/App_Data/uploads/ProductShots"), item1.Size);
                        var path = Path.Combine(root, ProductShot);
                        if (System.IO.File.Exists(path))
                        {
                            return File(path, "image/png");
                        }
                        else
                        {
                            ProductShot = "not_available.png";
                            var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                            return File(rootnot, "image/png");
                        }
                    }
                }
            }
            else
            {
                ProductShot = "not_available.png";
                var rootnot = Path.Combine(Server.MapPath("~/App_Data/uploads/Templates"), ProductShot);
                return File(rootnot, "image/png");
            }

            return View();
        }
    }
}