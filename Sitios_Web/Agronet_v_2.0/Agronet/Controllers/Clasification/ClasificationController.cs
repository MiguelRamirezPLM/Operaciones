using Agronet.Models;
using Agronet.Models.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agronet.Controllers.Clasification
{
    public class ClasificationController : Controller
    {
        DEAQ db = new DEAQ();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId, string BookId, string EditionId, string DivisionId, string ProductName)
        {
            sessionVM ind = (sessionVM)Session["sessionVM"];

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

                sessionVM sessionVM = new sessionVM(Country, Book, Edition, Division);
                Session["sessionVM"] = sessionVM;

                List<plm_vwProductsByEdition> LS = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + ", @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();

                return View(LS);
            }

            if (ind != null)
            {
                int Country = ind.CountryId;
                int Book = ind.BookId;
                int Edition = ind.EditionId;
                int Division = ind.DivisionId;

                sessionVM sessionVM = new sessionVM(Country, Book, Edition, Division);
                Session["sessionVM"] = sessionVM;

                List<plm_vwProductsByEdition> LS = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + ", @TypeInEdition=P, @ProductName='" + ProductName + "'").ToList();

                return View(LS);
            }
            else
            {
                int Country = 0;
                int Edition = 0;
                int Division = 0;

                List<plm_vwProductsByEdition> LS = db.Database.SqlQuery<plm_vwProductsByEdition>("plm_spGetParticipantProductsByEdition @DivisionId=" + Division + ", @EditionId=" + Edition + ", @CountryId=" + Country + "").ToList();

                return View(LS);
            }
        }

        public ActionResult ActiveSubstances(int? ProductId, int? PharmaFormId, int? CategoryId, string ActiveSubstanceName)
        {
            ClasificationProds ind = (ClasificationProds)Session["ClasificationProds"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (ProductId != null)
            {
                List<ActiveSubstances> LA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + ProductId + ", @ActiveSubstanceName='" + ActiveSubstanceName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LA);
            }

            if (ind != null)
            {

                ProductId = ind.PId;
                PharmaFormId = ind.PFId;
                CategoryId = ind.CatId;

                List<ActiveSubstances> LA = db.Database.SqlQuery<ActiveSubstances>("plm_spGetPActiveSubstances @ProductId=" + ProductId + ", @ActiveSubstanceName='" + ActiveSubstanceName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LA);
            }

            else
            {
                return RedirectToAction("Logout", "Login");
            }

        }

        public JsonResult AddProductSubstances(string Product, string ActiveSubstance)
        {
            int ProductId = int.Parse(Product);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);

            List<ProductSubstances> LPS = db.ProductSubstances.Where(x => x.ProductId == ProductId && x.ActiveSubstanceId == ActiveSubstanceId).ToList();

            if (LPS.LongCount() == 0)
            {
                ProductSubstances ProductSubstances = new ProductSubstances();

                ProductSubstances.ActiveSubstanceId = Convert.ToInt32(ActiveSubstanceId);
                ProductSubstances.ProductId = Convert.ToInt32(ProductId);

                db.ProductSubstances.Add(ProductSubstances);
                db.SaveChanges();

                ActivityLog.ProductSubstances(ProductId, ActiveSubstanceId, 1);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductSubstances(string Product, string ActiveSubstance)
        {
            int ProductId = int.Parse(Product);
            int ActiveSubstanceId = int.Parse(ActiveSubstance);

            List<ProductSubstances> LPS = db.ProductSubstances.Where(x => x.ProductId == ProductId && x.ActiveSubstanceId == ActiveSubstanceId).ToList();

            if (LPS.LongCount() > 0)
            {
                var delete = db.ProductSubstances.SingleOrDefault(x => x.ProductId == ProductId && x.ActiveSubstanceId == ActiveSubstanceId);

                db.ProductSubstances.Remove(delete);
                db.SaveChanges();

                ActivityLog.ProductSubstances(ProductId, ActiveSubstanceId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Crops(int? ProductId, int? PharmaFormId, int? CategoryId, string CropName)
        {
            ClasificationProds ind = (ClasificationProds)Session["ClasificationProds"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (ProductId != null)
            {
                List<Crops> LA = db.Database.SqlQuery<Crops>("plm_spGetPProductCrops @ProductId=" + ProductId + ", @CropName='" + CropName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LA);
            }

            if (ind != null)
            {

                ProductId = ind.PId;
                PharmaFormId = ind.PFId;
                CategoryId = ind.CatId;

                List<Crops> LA = db.Database.SqlQuery<Crops>("plm_spGetPProductCrops @ProductId=" + ProductId + ", @CropName='" + CropName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LA);
            }

            else
            {
                return RedirectToAction("Logout", "Login");
            }

        }

        public JsonResult AddProductCrops(string Product, string Crop)
        {
            int ProductId = int.Parse(Product);
            int CropId = int.Parse(Crop);

            List<ProductCrops> LPS = db.ProductCrops.Where(x => x.ProductId == ProductId && x.CropId == CropId).ToList();

            if (LPS.LongCount() == 0)
            {
                ProductCrops ProductCrops = new ProductCrops();

                ProductCrops.CropId = Convert.ToInt32(CropId);
                ProductCrops.ProductId = Convert.ToInt32(ProductId);

                db.ProductCrops.Add(ProductCrops);
                db.SaveChanges();

                ActivityLog.ProductCrops(ProductId, CropId, 1);
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductCrops(string Product, string Crop)
        {
            int ProductId = int.Parse(Product);
            int CropId = int.Parse(Crop);

            List<ProductCrops> LPS = db.ProductCrops.Where(x => x.ProductId == ProductId && x.CropId == CropId).ToList();

            if (LPS.LongCount() > 0)
            {
                var delete = db.ProductCrops.SingleOrDefault(x => x.ProductId == ProductId && x.CropId == CropId);

                db.ProductCrops.Remove(delete);
                db.SaveChanges();

                ActivityLog.ProductCrops(ProductId, CropId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgrochemicalUses(int? ProductId, int? PharmaFormId, int? CategoryId, string AgrochemicalUseName)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (!string.IsNullOrEmpty(AgrochemicalUseName))
            {
                List<AgrochemicalUses> LA = db.Database.SqlQuery<AgrochemicalUses>("plm_spGetAgrochemicalUserSearch @param='" + AgrochemicalUseName + "'").ToList();

                return View(LA);
            }
            else
            {
                List<AgrochemicalUses> LA = db.AgrochemicalUses.Where(x => x.Active == true).OrderBy(x => x.ParentId).OrderBy(x => x.AgrochemicalUseName).ToList();

                return View(LA);
            }

        }

        public ActionResult AddProductAgrochemicalUses(int Id)
        {
            ClasificationProds ind = (ClasificationProds)Session["ClasificationProds"];

            int ProductId = Convert.ToInt32(ind.PId);

            List<ProductAgrochemicalUses> LPAU = db.ProductAgrochemicalUses.Where(x => x.ProductId == ProductId && x.AgrochemicalUseId == Id).ToList();

            if (LPAU.LongCount() == 0)
            {
                ProductAgrochemicalUses ProductAgrochemicalUses = new ProductAgrochemicalUses();

                ProductAgrochemicalUses.AgrochemicalUseId = Id;
                ProductAgrochemicalUses.ProductId = ProductId;

                db.ProductAgrochemicalUses.Add(ProductAgrochemicalUses);
                db.SaveChanges();

                ActivityLog.ProductAgrochemicalUses(ProductId, Id, 1);
            }

            return RedirectToAction("AgrochemicalUses", new { ProductId = ind.PId, PharmaFormId = ind.PFId, CategoryId = ind.CatId });
        }

        public JsonResult DeleteProductAgrochemicalUses(string Product, string AgrochemicalUse)
        {
            int ProductId = int.Parse(Product);
            int AgrochemicalUseId = int.Parse(AgrochemicalUse);

            List<ProductAgrochemicalUses> LPS = db.ProductAgrochemicalUses.Where(x => x.ProductId == ProductId && x.AgrochemicalUseId == AgrochemicalUseId).ToList();

            if (LPS.LongCount() > 0)
            {
                var delete = db.ProductAgrochemicalUses.SingleOrDefault(x => x.ProductId == ProductId && x.AgrochemicalUseId == AgrochemicalUseId);

                db.ProductAgrochemicalUses.Remove(delete);
                db.SaveChanges();

                ActivityLog.ProductAgrochemicalUses(ProductId, AgrochemicalUseId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Seeds(int? ProductId, int? PharmaFormId, int? CategoryId, string SeedName)
        {
            ClasificationProds ind = (ClasificationProds)Session["ClasificationProds"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (ProductId != null)
            {
                List<Seeds> LC = db.Database.SqlQuery<Seeds>("plm_spGetPProductSeeds @ProductId=" + ProductId + ", @SeedName='" + SeedName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LC);
            }

            if (ind != null)
            {

                ProductId = ind.PId;
                PharmaFormId = ind.PFId;
                CategoryId = ind.CatId;

                List<Seeds> LC = db.Database.SqlQuery<Seeds>("plm_spGetPProductSeeds @ProductId=" + ProductId + ", @SeedName='" + SeedName + "'").ToList();

                ClasificationProds ClasificationProds = new ClasificationProds(ProductId, PharmaFormId, CategoryId);
                Session["ClasificationProds"] = ClasificationProds;

                return View(LC);
            }

            else
            {
                return RedirectToAction("Logout", "Login");
            }

        }

        public JsonResult AddProductSeeds(string Product, string Seed)
        {
            int ProductId = int.Parse(Product);
            int SeedId = int.Parse(Seed);

            List<ProductSeeds> LPS = db.ProductSeeds.Where(x => x.ProductId == ProductId && x.SeedId == SeedId).ToList();

            if (LPS.LongCount() == 0)
            {
                ProductSeeds ProductSeeds = new ProductSeeds();

                ProductSeeds.SeedId = Convert.ToInt32(SeedId);
                ProductSeeds.ProductId = Convert.ToInt32(ProductId);

                db.ProductSeeds.Add(ProductSeeds);
                db.SaveChanges();

                ActivityLog.ProductSeeds(ProductId, SeedId, 1);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProductSeeds(string Product, string Seed)
        {
            int ProductId = int.Parse(Product);
            int SeedId = int.Parse(Seed);

            List<ProductSeeds> LPS = db.ProductSeeds.Where(x => x.ProductId == ProductId && x.SeedId == SeedId).ToList();

            if (LPS.LongCount() > 0)
            {
                var delete = db.ProductSeeds.SingleOrDefault(x => x.ProductId == ProductId && x.SeedId == SeedId);

                db.ProductSeeds.Remove(delete);
                db.SaveChanges();

                ActivityLog.ProductSeeds(ProductId, SeedId, 4);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}