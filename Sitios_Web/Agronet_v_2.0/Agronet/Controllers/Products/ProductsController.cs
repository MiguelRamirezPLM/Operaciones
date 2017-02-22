using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agronet.Models;
using Agronet.Models.Sessions;

namespace Agronet.Controllers.Products
{
    public class ProductsController : Controller
    {
        DEAQ db = new DEAQ();

        public ActionResult Index(string CountryId, string BookId, string EditionId, string DivisionId, string ProductName)
        {
            sessionSM ind = (sessionSM)Session["sessionSM"];
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

                sessionSM sessionSM = new sessionSM(Country, Book, Edition, Division);
                Session["sessionSM"] = sessionSM;

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

                sessionSM sessionSM = new sessionSM(Country, Book, Edition, Division);
                Session["sessionSM"] = sessionSM;

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

        public JsonResult GetPharmaForms()
        {
            List<PharmaForms> LS = db.Database.SqlQuery<PharmaForms>("plm_spGetPharmaForms").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories()
        {
            List<Categories> LC = db.Database.SqlQuery<Categories>("plm_spGetCategories").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProduct(string Product,
                                     string PharmaForm,
                                     string Category,
                                     bool Participant,
                                     bool Mentionated,
                                     string Division,
                                     string Edition,
                                     string Country,
                                     string Register,
                                     string Description,
                                     string ProductI)
        {

            int PharmaFormId = int.Parse(PharmaForm);
            int CategoryId = int.Parse(Category);
            int DivisionId = int.Parse(Division);
            int EditionId = int.Parse(Edition);
            int CountryId = int.Parse(Country);

            int? ProductId = 0;

            if (!string.IsNullOrEmpty(ProductI))
            {
                ProductId = int.Parse(ProductI);
            }

            int LaboratoryId = GetLaboratoryId(DivisionId);

            try
            {
                if (ProductId != 0)
                {
                    var _result = db.Database.ExecuteSqlCommand("plm_spTransCRUDProducts @PharmaFormId=" + PharmaFormId + "" +
                                                                                    ", @CategoryId=" + CategoryId + "" +
                                                                                    ", @DivisionId=" + DivisionId + "" +
                                                                                    ", @EditionId=" + EditionId + "" +
                                                                                    ", @ProductId=" + ProductId + "" +
                                                                                    ", @Participant=" + Participant + "" +
                                                                                    ", @Mentionated=" + Mentionated + "");
                }
                else
                {
                    var _result = db.Database.ExecuteSqlCommand("plm_spTransCRUDProducts @ProductName='" + Product.Trim() + "'" +
                                                                    ", @Description='" + Description.Trim() + "'" +
                                                                    ", @CountryId=" + CountryId + "" +
                                                                    ", @LaboratoryId=" + LaboratoryId + "" +
                                                                    ", @Register='" + Register.Trim() + "'" +
                                                                    ", @PharmaFormId=" + PharmaFormId + "" +
                                                                    ", @CategoryId=" + CategoryId + "" +
                                                                    ", @DivisionId=" + DivisionId + "" +
                                                                    ", @EditionId=" + EditionId + "" +
                                                                    ", @Participant=" + Participant + "" +
                                                                    ", @Mentionated=" + Mentionated + "");
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public int GetLaboratoryId(int DivisionId)
        {

            List<Divisions> LD = db.Database.SqlQuery<Divisions>("plm_spGetDivisionById @divisionId=" + DivisionId + "").ToList();

            int LaboratoryId = Convert.ToInt32(LD[0].LaboratoryId);

            return LaboratoryId;
        }

        public JsonResult GetProductsToAddPharmaForm(string CountryId, string EditionId, string DivisionId)
        {
            int Country = int.Parse(CountryId);
            int Edition = int.Parse(EditionId);
            int Division = int.Parse(DivisionId);

            List<ProductsToAddPharmaForms> LS = db.Database.SqlQuery<ProductsByDivision>("plm_spGetProductsByDivision @CountryId=" + Country + ", @DivisionId=" + Division + ", @EditionId=" + Edition + "").Select(x => new ProductsToAddPharmaForms { ProductId = x.ProductId, ProductName = x.ProductName }).ToList();

            ProductsToAddPharmaForms ProductsToAddPharmaForms = new ProductsToAddPharmaForms();
            List<ProductsToAddPharmaForms> LP = new List<ProductsToAddPharmaForms>();

            foreach (ProductsToAddPharmaForms item in LS)
            {
                var itm = LP.Where(x => x.ProductId == item.ProductId).ToList();

                if (itm.LongCount() == 0)
                {
                    ProductsToAddPharmaForms = new ProductsToAddPharmaForms();

                    ProductsToAddPharmaForms.ProductId = item.ProductId;
                    ProductsToAddPharmaForms.ProductName = item.ProductName;

                    LP.Add(ProductsToAddPharmaForms);
                }
            }

            LP = LP.OrderBy(x => x.ProductName).ToList();

            return Json(LP, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPharmaFormToAdd(string Product)
        {
            int ProductId = int.Parse(Product);

            List<PharmaForms> LS = db.Database.SqlQuery<PharmaForms>("plm_spGetPharmaFormsByProduct @ProductId=" + ProductId + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoriesToAdd(string Product, string PharmaForm, string Division)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int DivisionId = int.Parse(Division);

            List<Categories> LC = db.Database.SqlQuery<Categories>("plm_spGetCategoriesByProduct @ProductId=" + ProductId + ", @PharmaFormId=" + PharmaFormId + ", @DivisionId=" + DivisionId + "").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddPharmaForm(string Product, string PharmaForm, string Edition, string Division, string Category)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);

            try
            {
                var _result = db.Database.ExecuteSqlCommand("plm_spTransCRUDProducts @PharmaFormId=" + PharmaFormId + "" +
                                                                                    ", @CategoryId=" + CategoryId + "" +
                                                                                    ", @DivisionId=" + DivisionId + "" +
                                                                                    ", @EditionId=" + EditionId + "" +
                                                                                    ", @ProductId=" + ProductId + "");
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SIDEF(string Product, string PharmaForm, string Edition, string Division, string Category, string Operation)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);

            List<ParticipantProducts> LPP = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LPP.LongCount() > 0)
            {
                foreach (ParticipantProducts item in LPP)
                {
                    if (Operation == "Insert")
                    {
                        item.Active = true;
                    }
                    else
                    {
                        item.Active = null;
                    }

                    db.SaveChanges();
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Participant(string Product, string PharmaForm, string Edition, string Division, string Category, string Operation)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);


            List<MentionatedProducts> LMP = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LMP.LongCount() > 0)
            {
                var delete = db.MentionatedProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                db.MentionatedProducts.Remove(delete);
                db.SaveChanges();
            }



            List<ParticipantProducts> LPP = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LPP.LongCount() == 0)
            {
                if (Operation == "Insert")
                {
                    ParticipantProducts ParticipantProducts = new ParticipantProducts();

                    ParticipantProducts.CategoryId = CategoryId;
                    ParticipantProducts.DivisionId = DivisionId;
                    ParticipantProducts.EditionId = EditionId;
                    ParticipantProducts.PharmaFormId = PharmaFormId;
                    ParticipantProducts.ProductId = ProductId;

                    db.ParticipantProducts.Add(ParticipantProducts);
                    db.SaveChanges();


                    List<EditionDivisionProducts> LEDP = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                    if (LEDP.LongCount() == 0)
                    {
                        EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();

                        EditionDivisionProducts.CategoryId = CategoryId;
                        EditionDivisionProducts.DivisionId = DivisionId;
                        EditionDivisionProducts.EditionId = EditionId;
                        EditionDivisionProducts.PharmaFormId = PharmaFormId;
                        EditionDivisionProducts.ProductId = ProductId;

                        db.EditionDivisionProducts.Add(EditionDivisionProducts);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var delete = db.ParticipantProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                    db.ParticipantProducts.Remove(delete);
                    db.SaveChanges();
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<EditionDivisionProducts> LEDP = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                if (LEDP.LongCount() > 0)
                {
                    var deleteedp = db.EditionDivisionProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                    db.EditionDivisionProducts.Remove(deleteedp);
                    db.SaveChanges();
                }


                var delete = db.ParticipantProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                db.ParticipantProducts.Remove(delete);
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult MentionatedProducts(string Product, string PharmaForm, string Edition, string Division, string Category, string Operation)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);

            List<ParticipantProducts> LPP = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LPP.LongCount() > 0)
            {
                var delete = db.ParticipantProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                db.ParticipantProducts.Remove(delete);
                db.SaveChanges();
            }

            List<MentionatedProducts> LMP = db.MentionatedProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LMP.LongCount() == 0)
            {
                MentionatedProducts MentionatedProducts = new MentionatedProducts();

                MentionatedProducts.CategoryId = CategoryId;
                MentionatedProducts.DivisionId = DivisionId;
                MentionatedProducts.EditionId = EditionId;
                MentionatedProducts.PharmaFormId = PharmaFormId;
                MentionatedProducts.ProductId = ProductId;

                db.MentionatedProducts.Add(MentionatedProducts);
                db.SaveChanges();


                List<EditionDivisionProducts> LEDP = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

                if (LEDP.LongCount() == 0)
                {
                    EditionDivisionProducts EditionDivisionProducts = new EditionDivisionProducts();

                    EditionDivisionProducts.CategoryId = CategoryId;
                    EditionDivisionProducts.DivisionId = DivisionId;
                    EditionDivisionProducts.EditionId = EditionId;
                    EditionDivisionProducts.PharmaFormId = PharmaFormId;
                    EditionDivisionProducts.ProductId = ProductId;

                    db.EditionDivisionProducts.Add(EditionDivisionProducts);
                    db.SaveChanges();
                }

            }
            else
            {
                List<EditionDivisionProducts> LEDP = db.EditionDivisionProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();
                if (LEDP.LongCount() > 0)
                {
                    var deleteedp = db.EditionDivisionProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                    db.EditionDivisionProducts.Remove(deleteedp);
                    db.SaveChanges();
                }

                var delete = db.MentionatedProducts.SingleOrDefault(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId);
                db.MentionatedProducts.Remove(delete);
                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ProductChanges(string Product, string PharmaForm, string Edition, string Division, string Category, string Operation)
        {
            int ProductId = int.Parse(Product);
            int PharmaFormId = int.Parse(PharmaForm);
            int EditionId = int.Parse(Edition);
            int DivisionId = int.Parse(Division);
            int CategoryId = int.Parse(Category);

            List<NewProducts> LNP = db.NewProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LNP.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            List<ParticipantProducts> LMP = db.ParticipantProducts.Where(x => x.CategoryId == CategoryId && x.DivisionId == DivisionId && x.EditionId == EditionId && x.PharmaFormId == PharmaFormId && x.ProductId == ProductId).ToList();

            if (LMP.LongCount() > 0)
            {
                foreach (ParticipantProducts PP in LMP)
                {
                    if (Operation == "Insert")
                    {
                        byte ContentTypeId = Convert.ToByte(System.Configuration.ConfigurationManager.AppSettings["Cambios"]);

                        PP.ContentTypeId = ContentTypeId;
                    }
                    else
                    {
                        PP.ContentTypeId = null;
                    }
                    db.SaveChanges();
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("_errorPP", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RowsPerPage(string Value)
        {
            int Count = int.Parse(Value);

            sessionRPP sessionRPP = new sessionRPP(Count);
            Session["sessionRPP"] = sessionRPP;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string term, string country)
        {
            int CountryId = int.Parse(country);

            List<ClassAutocomplete> LS = db.Database.SqlQuery<ClassAutocomplete>("plm_spGetProductsByAutocomplete @CountryId=" + CountryId + ", @ProductName='" + term + "'").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditProducts(string Product, string ProductName, string Description, string Register, string Division, string Country)
        {
            int ProductId = int.Parse(Product);
            int DivisionId = int.Parse(Division);
            int CountryId = int.Parse(Country);

            var p = db.Products.Where(x => x.ProductId == ProductId).ToList();

            if (p.LongCount() > 0)
            {
                foreach (Agronet.Models.Products _p in p)
                {
                    _p.ProductName = ProductName.Trim();

                    if (!string.IsNullOrEmpty(Description))
                    {
                        _p.Description = Description.Trim();
                    }
                    else
                    {
                        _p.Description = null;
                    }

                    _p.Register = Register.Trim();

                    db.SaveChanges();
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}