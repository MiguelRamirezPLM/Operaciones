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

                //List<ProductsByDivision> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + " , @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();
                List<GetParticipantProducts> LS = db.Database.SqlQuery<GetParticipantProducts>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + ", @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();

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

                //List<ProductsByDivision> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + ", @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();
                List<GetParticipantProducts> LS = db.Database.SqlQuery<GetParticipantProducts>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + ", @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();

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

                List<GetParticipantProducts> LS = db.Database.SqlQuery<GetParticipantProducts>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + ", @TypeInEdition=P").ToList();

                return View(LS);
            }
        }

        public ActionResult ProductShots(int ProductId, int PharmaFormId, int CategoryId, int DivisionId)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

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

        public JsonResult SaveProductShot(HttpPostedFileBase file, string Size, string Product, string PharmaForm, string Category, string Division, string Country)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            byte SizeId = Convert.ToByte(Size);
            int CountryId = int.Parse(Country);
            int ProductImageId = 0;

            string ProductShot = "";
            string Extention = System.IO.Path.GetExtension(file.FileName);

            List<Agronet.Models.Products> LS = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (LS.LongCount() > 0)
            {
                ProductShot = LS[0].ProductName.ToLower();

                ProductShot = ReplaceProductShot(ProductShot.Trim());
            }

            ProductShot = ProductShot.Trim() + Extention.Trim();

            string Path = "C:\\Users\\miguel.ramirez\\Documents\\Visual Studio 2013\\Projects\\Agronet\\Agronet\\App_Data";

            List<ImageSizes> LIS = db.ImageSizes.Where(x => x.ImageSizeId == SizeId).ToList();

            List<Countries> LC = db.Countries.Where(x => x.CountryId == CountryId).ToList();

            Path = System.IO.Path.Combine(Path, LC[0].ID, LIS[0].Size);

            List<ProductImages> LPI = db.ProductImages.Where(x => x.CategoryId == CategoryId &&
                                                                  x.DivisionId == DivisionId &&
                                                                  x.PharmaFormId == PharmaFormId &&
                                                                  x.ProductId == ProductId).ToList();
            if (LPI.LongCount() == 0)
            {
                ProductImages ProductImages = new ProductImages();

                ProductImages.Active = true;
                ProductImages.CategoryId = CategoryId;
                ProductImages.DivisionId = DivisionId;
                ProductImages.PharmaFormId = PharmaFormId;
                ProductImages.ProductId = ProductId;
                ProductImages.ProductShot = ProductShot;

                db.ProductImages.Add(ProductImages);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }


                List<ProductImages> LPI1 = db.ProductImages.Where(x => x.CategoryId == CategoryId &&
                                                                  x.DivisionId == DivisionId &&
                                                                  x.PharmaFormId == PharmaFormId &&
                                                                  x.ProductId == ProductId).ToList();
                foreach (ProductImages item in LPI1)
                {
                    ProductImageId = item.ProductImageId;
                }
            }
            else
            {
                foreach (ProductImages item in LPI)
                {
                    item.ProductShot = ProductShot;

                    db.SaveChanges();

                    ProductImageId = item.ProductImageId;
                }
            }


            List<ProductImageSizes> LPII = db.ProductImageSizes.Where(x => x.ImageSizeId == SizeId && x.ProductImageId == ProductImageId).ToList();

            if (LPII.LongCount() == 0)
            {
                ProductImageSizes ProductImageSizes = new ProductImageSizes();

                ProductImageSizes.ImageSizeId = SizeId;
                ProductImageSizes.ProductImageId = ProductImageId;

                db.ProductImageSizes.Add(ProductImageSizes);
                db.SaveChanges();
            }


            if (System.IO.Directory.Exists(Path))
            {
                Path = System.IO.Path.Combine(Path, ProductShot);
                file.SaveAs(Path);
            }
            else
            {
                System.IO.Directory.CreateDirectory(Path);
                Path = System.IO.Path.Combine(Path, ProductShot);
                file.SaveAs(Path);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public static string ReplaceProductShot(string img)
        {
            img = img.Replace(" ", "_");
            img = img.Replace(",", "_");
            img = img.Replace(", ", "_");
            img = img.Replace(",_", "_");
            img = img.Replace(".", "_");
            img = img.Replace("__", "_");
            img = img.Replace(". ", "_");
            img = img.Replace("/", "_");
            img = img.Replace("Á", "A");
            img = img.Replace("á", "a");
            img = img.Replace("É", "E");
            img = img.Replace("é", "e");
            img = img.Replace("Í", "I");
            img = img.Replace("í", "i");
            img = img.Replace("Ó", "O");
            img = img.Replace("ó", "o");
            img = img.Replace("ú", "u");
            img = img.Replace("ü", "u");
            img = img.Replace("Ü", "U");
            img = img.Replace("Ú", "U");
            img = img.Replace("- ", "-");
            img = img.Replace(" - ", "-");


            return img;
        }

        public ActionResult ShowImagesDetails(int? ProductImageId, int? Country, int? SizeId)
        {
            string Path = "C:\\Users\\miguel.ramirez\\Documents\\Visual Studio 2013\\Projects\\Agronet\\Agronet\\App_Data";

            string Path1 = Path;

            List<Countries> LC = db.Countries.Where(x => x.CountryId == Country).ToList();

            List<ProductImages> LPI = db.ProductImages.Where(x => x.ProductImageId == ProductImageId).ToList();

            List<ImageSizes> LII = db.ImageSizes.Where(x => x.ImageSizeId == SizeId).ToList();

            Path = System.IO.Path.Combine(Path, LC[0].ID, LII[0].Size, LPI[0].ProductShot);

            if (System.IO.File.Exists(Path))
            {
                return File(Path, "Image/png");
            }
            else
            {
                Path1 = System.IO.Path.Combine(Path1, "Uploads", "not_available.png");

                return File(Path1, "Image/png");
            }
        }

        public JsonResult RemoveProductShots(string ProductImage, string Size)
        {
            int ProductImageId = int.Parse(ProductImage);
            int SizeId = int.Parse(Size);

            List<ProductImageSizes> LPIS = db.ProductImageSizes.Where(x => x.ProductImageId == ProductImageId && x.ImageSizeId == SizeId).ToList();

            if (LPIS.LongCount() > 0)
            {
                var Delete = db.ProductImageSizes.SingleOrDefault(x => x.ProductImageId == ProductImageId && x.ImageSizeId == SizeId);
                db.ProductImageSizes.Remove(Delete);
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowProductShots(int? ProductId, int? PharmaFormId, int? CategoryId, int? DivisionId, int? Country)
        {
            string Path = "C:\\Users\\miguel.ramirez\\Documents\\Visual Studio 2013\\Projects\\Agronet\\Agronet\\App_Data";

            List<Countries> LC = db.Countries.Where(x => x.CountryId == Country).ToList();

            int ProductImageId = 0;

            List<ProductImages> LPI = db.ProductImages.Where(x => x.CategoryId == CategoryId &&
                                                                  x.DivisionId == DivisionId &&
                                                                  x.PharmaFormId == PharmaFormId &&
                                                                  x.ProductId == ProductId).ToList();

            if (LPI.LongCount() > 0)
            {
                string Path1 = Path;

                ProductImageId = LPI[0].ProductImageId;

                List<ProductImageSizes> LPIS = db.ProductImageSizes.Where(x => x.ProductImageId == ProductImageId).ToList();

                if (LPIS.LongCount() > 0)
                {
                    foreach (ProductImageSizes item in LPIS)
                    {
                        List<ImageSizes> LII = db.ImageSizes.Where(x => x.ImageSizeId == item.ImageSizeId).ToList();

                        string root = System.IO.Path.Combine(Path1, LC[0].ID, LII[0].Size, LPI[0].ProductShot);

                        if (System.IO.File.Exists(root))
                        {
                            return File(root, "Image/png");
                        }
                    }
                }
                else
                {
                    Path = System.IO.Path.Combine(Path, "Uploads", "not_available.png");

                    return File(Path, "Image/png");
                }
            }

            Path = System.IO.Path.Combine(Path, "Uploads", "not_available.png");

            return File(Path, "Image/png");
        }
    }
}