using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Guianet.Models;
using Guianet.Models.Sessions;
using Newtonsoft.Json;
using System.Text;

namespace Guianet.Controllers.LI
{

    public class LIController : Controller
    {
        GuiaEntities db = new GuiaEntities();
        ActivityLog ActivityLog = new ActivityLog();
        PLMUsers dbUsers = new PLMUsers();
        CRUD CRUD = new CRUD();
        GetData GetData = new GetData();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string ProductName)
        {
            SessionClasification ind = (SessionClasification)Session["SessionClasification"];
            SessionTypesLI Types = (SessionTypesLI)Session["SessionTypesLI"];

            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int Client = int.Parse(ClientId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + Client + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                SessionClasification SessionClasification = new SessionClasification(Country, Client, Edition, Book, null);
                Session["SessionClasification"] = SessionClasification;

                if (Types != null)
                {
                    if (Types.Types == "Clasif")
                    {
                        li = li.Where(x => x.Qtty != 0).ToList();
                    }

                    if (Types.Types == "Ok")
                    {
                        li = li.Where(x => x.Validation != 0).ToList();
                    }

                    if (Types.Types == "NotClasif")
                    {
                        li = li.Where(x => x.Qtty == 0).ToList();
                    }

                    if (Types.Types == "NotOk")
                    {
                        li = li.Where(x => x.Validation == 0).ToList();
                    }

                    if (Types.Types == "CCS")
                    {

                        li = (from l in li
                              join w in db.Works
                                  on l.ProductId equals w.ProductId
                              where w.ClientId == Client
                              select l).Distinct().ToList();

                    }

                    if (Types.Types == "NotCCS")
                    {

                        li = (from l in li
                              from w in db.Works.Where(x => x.ProductId == l.ProductId && x.ClientId == Client).DefaultIfEmpty()
                              where w == null
                              select l).Distinct().ToList();

                    }
                }

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameCL SearchProductNameCL = new SearchProductNameCL(ProductName);
                    Session["SearchProductNameCL"] = SearchProductNameCL;
                }
                else
                {
                    Session["SearchProductNameCL"] = null;
                }

                return View(li);
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int Client = ind.ClId;
                int Edition = ind.EId;
                int Book = ind.BId;

                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + Client + ", @EditionId=" + Edition + ", @ProductName='" + ProductName + "'").ToList();

                SessionClasification SessionClasification = new SessionClasification(Country, Client, Edition, Book, null);
                Session["SessionClasification"] = SessionClasification;

                if (Types != null)
                {
                    if (Types.Types == "Clasif")
                    {
                        li = li.Where(x => x.Qtty != 0).ToList();
                    }

                    if (Types.Types == "Ok")
                    {
                        li = li.Where(x => x.Validation != 0).ToList();
                    }

                    if (Types.Types == "NotClasif")
                    {
                        li = li.Where(x => x.Qtty == 0).ToList();
                    }

                    if (Types.Types == "NotOk")
                    {
                        li = li.Where(x => x.Validation == 0).ToList();
                    }

                    if (Types.Types == "CCS")
                    {

                        li = (from l in li
                              join w in db.Works
                                  on l.ProductId equals w.ProductId
                              where w.ClientId == Client
                              select l).Distinct().ToList();

                    }

                    if (Types.Types == "NotCCS")
                    {
                        li = (from l in li
                              from w in db.Works.Where(x => x.ProductId == l.ProductId && x.ClientId == Client).DefaultIfEmpty()
                              where w == null
                              select l).Distinct().ToList();
                    }
                }

                if (!string.IsNullOrEmpty(ProductName))
                {
                    SearchProductNameCL SearchProductNameCL = new SearchProductNameCL(ProductName);
                    Session["SearchProductNameCL"] = SearchProductNameCL;
                }
                else
                {
                    Session["SearchProductNameCL"] = null;
                }

                return View(li);
            }
            else
            {
                var li = db.Database.SqlQuery<ParticipantProductsClasif>("plm_spGetParticipantProductsToClasification @ClientId=" + 0 + ", @EditionId=" + 0 + "").ToList();

                return View(li);
            }

        }

        public ActionResult ClasificationProducts(int? ProductId)
        {
            SessionClasification ind = (SessionClasification)Session["SessionClasification"];

            if (ind == null)
            {
                return RedirectToAction("Logout", "Login");
            }

            int _clientid = ind.ClId;

            ind.PId = ProductId;

            var cc = db.Database.SqlQuery<HomogeneousCategoriesByClientByProduct>("plm_spGetProductCategoriesByClientByProduct  @ProductId =" + ProductId + ",  @ClientId= " + _clientid + "").ToList();

            return View(cc);
        }

        public JsonResult getlevel4(string HomogeneousCategory, string Client, string Product)
        {
            List<GetHomogeneousCategories> Cats = new List<GetHomogeneousCategories>();
            SearchCategoryCL SearchCategory = (SearchCategoryCL)Session["SearchCategoryCL"];

            if (SearchCategory != null)
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);
                int ClientId = int.Parse(Client);
                int ProductId = int.Parse(Product);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategoryThree @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @TextSearch='" + SearchCategory.CategoryName + "', @ClientId=" + ClientId + ",@ProductId=" + ProductId + "").ToList();
            }
            else
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);
                int ClientId = int.Parse(Client);
                int ProductId = int.Parse(Product);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategoryThree @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @ClientId=" + ClientId + ",@ProductId=" + ProductId + "").ToList();
            }

            return Json(Cats, JsonRequestBehavior.AllowGet);
        }

        public JsonResult saveCategories(String ListItems, string ArraySize, string Edition)
        {
            dynamic d = JsonConvert.DeserializeObject(ListItems);

            int Size = int.Parse(ArraySize);
            int EditionId = int.Parse(Edition);

            for (var i = 0; i <= Size - 1; i++)
            {
                var Id = Convert.ToInt32(d[i]["Id"]);
                var CId = Convert.ToInt32(d[i]["CId"]);
                var PId = Convert.ToInt32(d[i]["PId"]);
                var HCId = Convert.ToInt32(d[i]["HCId"]);

                string a = insertProductLeafCategories(CId, PId, Id, HCId);
                bool r = AddEditionCategoryThree(EditionId, HCId);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public bool AddEditionCategoryThree(int EditionId, int CategoryThreeId)
        {
            List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (ec.LongCount() == 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Create + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "");

                ActivityLog.EditionCategoryThree(EditionId, CategoryThreeId, null, null, 1);
            }

            return true;
        }

        public string insertProductLeafCategories(int ClientId, int ProductId, int LeafCategoryId, int CategoryThreeId)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            ClientProductLeafCategories CPLC = new ClientProductLeafCategories();


            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

            if (cp.LongCount() > 0)
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.CategoryThreeId == CategoryThreeId && x.LeafCategoryId == LeafCategoryId).ToList();

                if (cpl.LongCount() == 0)
                {
                    CPLC = new ClientProductLeafCategories();

                    CPLC.ClientId = ClientId;
                    CPLC.LeafCategoryId = LeafCategoryId;
                    CPLC.CategoryThreeId = CategoryThreeId;
                    CPLC.ProductId = ProductId;
                    CPLC.Module = "LI";

                    db.ClientProductLeafCategories.Add(CPLC);

                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 1);

                    return "Ok";
                }
                else
                {
                    return "_exist";
                }
            }
            else
            {
                return "_cpnotexist";
            }
        }

        public JsonResult deleteProductLeafCategories(string Client, string Product, string LeafCategory, string CategoryThree, string Edition)
        {
            ReassignCategories RC = new ReassignCategories();

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);
            int LeafCategoryId = int.Parse(LeafCategory);
            int CategoryThreeId = int.Parse(CategoryThree);
            int EditionId = int.Parse(Edition);

            var usr = dbUsers.Users.Where(x => x.UserId == UsersId).ToList();

            string NickName = usr[0].NickName;

            try
            {
                var cplV = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId && x.Module == "Ventas").ToList();

                if (cplV.LongCount() > 0)
                {

                    var _rc = db.ReassignCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId).ToList();

                    if (_rc.LongCount() == 0)
                    {
                        RC = new ReassignCategories();

                        RC.CategoryThreeId = CategoryThreeId;
                        RC.ClientId = ClientId;
                        RC.LeafCategoryId = LeafCategoryId;
                        RC.ProductId = ProductId;
                        RC.Date = DateTime.Now;
                        RC.NickName = NickName.Trim();

                        db.ReassignCategories.Add(RC);
                        db.SaveChanges();

                        ActivityLog.ReassignCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 1, NickName);
                    }
                }

                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId).ToList();

                if (cpl.LongCount() > 0)
                {
                    var delete = db.ClientProductLeafCategories.SingleOrDefault(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == LeafCategoryId && x.CategoryThreeId == CategoryThreeId);

                    db.ClientProductLeafCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, LeafCategoryId, CategoryThreeId, 4);
                }

                List<GetEditionCategoryThree> ec = db.Database.SqlQuery<GetEditionCategoryThree>("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Read + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "").ToList();

                if (ec.LongCount() > 0)
                {

                    List<ClientProductLeafCategories> LS = db.ClientProductLeafCategories.Where(x => x.CategoryThreeId == CategoryThreeId).ToList();

                    if (LS.LongCount() == 0)
                    {
                        var result = db.Database.ExecuteSqlCommand("plm_spCRUDEditionCategoryThree @CRUDType=" + CRUD.Delete + ", @EditionId=" + EditionId + ", @CategoryThreeId=" + CategoryThreeId + "");

                        ActivityLog.EditionCategoryThree(EditionId, CategoryThreeId, null, null, 4);
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                string message = e.Message;

                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult searchCategories(string CategoryName)
        {
            if (!string.IsNullOrEmpty(CategoryName))
            {
                SearchCategoryCL SearchCategoryCL = new SearchCategoryCL(CategoryName);
                Session["SearchCategoryCL"] = SearchCategoryCL;
            }
            else
            {
                SearchCategoryCL SearchCategoryCL = new SearchCategoryCL(CategoryName);
                Session["SearchCategoryCL"] = SearchCategoryCL;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getproductstatus(string Product, string Client)
        {
            List<Status> LS = new List<Status>();
            Status S = new Status();

            List<int> LI = new List<int>();

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            var s = db.Status.Where(x => x.Active == true).OrderBy(x => x.StatusName).ToList();

            var ps = db.Database.SqlQuery<StatusByProduct>("plm_spGetProductStatus @ProductId=" + ProductId + ", @ClientId=" + ClientId + "").ToList();

            if (ps.LongCount() > 0)
            {

                var _result = db.Database.SqlQuery<StatusByProduct>("plm_spGetProductStatus @ProductId=" + ProductId + ", @ClientId=" + ClientId + "").ToList();

                if (_result.LongCount() > 0)
                {
                    foreach (StatusByProduct _sbp in _result)
                    {
                        S = new Status();

                        S.Active = Convert.ToBoolean(_sbp.Active);
                        S.StatusId = Convert.ToInt32(_sbp.StatusId);
                        if (_sbp.Reference == 1)
                        {
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " checked='checked' onclick='insertproductstatus($(this))'/> &nbsp;&nbsp;" + _sbp.StatusName;
                        }
                        else
                        {
                            S.StatusName = "<input type='checkbox' value=" + _sbp.StatusId + " onclick='insertproductstatus($(this))'/> &nbsp;&nbsp;" + _sbp.StatusName;
                        }
                        LS.Add(S);
                    }
                }

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                LS = new List<Status>();

                foreach (Status _s in s)
                {
                    S = new Status();

                    S.Active = _s.Active;
                    S.StatusId = _s.StatusId;
                    S.StatusName = "<input type='checkbox' value=" + _s.StatusId + " onclick='insertproductstatus($(this))'/> &nbsp;&nbsp;" + _s.StatusName;

                    LS.Add(S);
                }

                return Json(LS, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult insertproductstatus(string Product, string Client, string Status, string Operation)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;


            ProductStatus ProductStatus = new Models.ProductStatus();
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);
            int StatusId = int.Parse(Status);

            if (Operation == "Insert")
            {
                var ps = db.ProductStatus.Where(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId).ToList();

                if (ps.LongCount() == 0)
                {
                    ProductStatus = new ProductStatus();

                    ProductStatus.ClientId = ClientId;
                    ProductStatus.ProductId = ProductId;
                    ProductStatus.StatusId = StatusId;

                    db.ProductStatus.Add(ProductStatus);
                    db.SaveChanges();

                    ActivityLog._ProductStatus(ClientId, ProductId, StatusId, 1, ApplicationId, UsersId);
                }
            }
            else
            {
                var ps = db.ProductStatus.Where(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId).ToList();

                if (ps.LongCount() > 0)
                {
                    var delete = db.ProductStatus.SingleOrDefault(x => x.StatusId == StatusId && x.ProductId == ProductId && x.ClientId == ClientId);

                    db.ProductStatus.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ProductStatus(ClientId, ProductId, StatusId, 4, ApplicationId, UsersId);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePId(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            SessionProductId SessionProductId = new SessionProductId(ClientId, ProductId);
            Session["SessionProductId"] = SessionProductId;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<GerReassingCategories> result = db.Database.SqlQuery<GerReassingCategories>("plm_spGetReassignCategoriesByClientByProduct @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeletedCategories(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<GerReassingCategories> result = db.Database.SqlQuery<GerReassingCategories>("plm_spGetClientProductLeafCategoriesByClientByProduct @ClientId=" + ClientId + ", @ProductId=" + ProductId + "").ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VoBoLI(int? ProductId, int? EditionId, int? ClientId)
        {
            var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            if (ecp.LongCount() > 0)
            {
                foreach (EditionClientProducts _ecp in ecp)
                {
                    _ecp.Validation = true;

                    db.SaveChanges();

                    ActivityLog.editEditionClientProducts(ProductId, EditionId, ClientId, 2, true);
                }
            }

            return RedirectToAction("Index", "LI");
        }

        public ActionResult RemoveVoBoLI(int? ProductId, int? EditionId, int? ClientId)
        {
            var ecp = db.EditionClientProducts.Where(x => x.ClientId == ClientId && x.EditionId == EditionId && x.ProductId == ProductId).ToList();

            if (ecp.LongCount() > 0)
            {
                foreach (EditionClientProducts _ecp in ecp)
                {
                    _ecp.Validation = false;

                    db.SaveChanges();

                    ActivityLog.editEditionClientProducts(ProductId, EditionId, ClientId, 2, false);
                }
            }

            return RedirectToAction("Index", "LI");
        }

        public ActionResult Categories(string TextSearch)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout", "Login");
            }

            List<GetHomogeneousCategories> LS = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategories @TextSearch='" + TextSearch + "'").ToList();

            return View(LS);
        }

        public JsonResult Getfourlevel(string CategoryThree)
        {

            int CategoryThreeId = int.Parse(CategoryThree);

            List<GetHomogeneousCategories> LS = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetCategories @CategoryThreeId=" + CategoryThreeId + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddCategory4Level(string CategoryName, string CategoryI)
        {
            int CategoryThreeId = int.Parse(CategoryI);

            List<LeafCategories> LS = db.Database.SqlQuery<LeafCategories>("plm_spCRUDCategories @CRUDType=" + CRUD.Read + ", @TableName=LeafCategories, @CategoryName='" + CategoryName.Trim() + "', @CategoryThreeId=" + CategoryThreeId + "").ToList();

            if (LS.LongCount() == 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDCategories @CRUDType=" + CRUD.Create + ", @TableName=LeafCategories, @CategoryName='" + CategoryName.Trim() + "', @CategoryThreeId=" + CategoryThreeId + "");

                List<LeafCategories> LSS = db.LeafCategories.Where(x => x.LeafCategory == CategoryName.Trim()).ToList();

                ActivityLog.LeafCategories(LSS[0].LeafCategoryId, CategoryName, 4, 1);

                ActivityLog.LeafHomogeneousCategories(LSS[0].LeafCategoryId, CategoryThreeId, 1);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int CategoryId = LS[0].LeafCategoryId;

                var lc = db.LeafHomogeneousCategories.Where(x => x.CategoryThreeId == CategoryThreeId && x.LeafCategoryId == CategoryId).ToList();

                if (lc.LongCount() == 0)
                {
                    LeafHomogeneousCategories LeafHomogeneousCategories = new LeafHomogeneousCategories();

                    LeafHomogeneousCategories.CategoryThreeId = CategoryThreeId;
                    LeafHomogeneousCategories.LeafCategoryId = CategoryId;

                    db.LeafHomogeneousCategories.Add(LeafHomogeneousCategories);
                    db.SaveChanges();

                    ActivityLog.LeafHomogeneousCategories(CategoryId, CategoryThreeId, 1);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("exists", JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult AddCategory3Level(string CategoryName)
        {
            List<CategoriesThree> LS = db.Database.SqlQuery<CategoriesThree>("plm_spCRUDCategories @CRUDType=" + CRUD.Read + ", @TableName=CategoryThree, @CategoryName='" + CategoryName.Trim() + "'").ToList();

            if (LS.LongCount() == 0)
            {
                var result = db.Database.ExecuteSqlCommand("plm_spCRUDCategories @CRUDType=" + CRUD.Create + ", @TableName=CategoryThree, @CategoryName='" + CategoryName.Trim() + "'");

                List<CategoriesThree> LSS = db.CategoriesThree.Where(x => x.CategoryThree == CategoryName.Trim()).ToList();

                ActivityLog.CategoryThree(LSS[0].CategoryThreeId, CategoryName, 3, 1);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("exists", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetType(string Type)
        {
            if (!string.IsNullOrEmpty(Type))
            {
                SessionTypesLI SessionTypesLI = new SessionTypesLI(null, Type);
                Session["SessionTypesLI"] = SessionTypesLI;
            }
            else
            {
                Session["SessionTypesLI"] = null;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuantity(string Type, string Num)
        {

            if (string.IsNullOrEmpty(Type))
            {
                Type = null;
            }

            if (!string.IsNullOrEmpty(Num))
            {
                int Quantity = int.Parse(Num);

                SessionTypesLI SessionTypesLI = new SessionTypesLI(Quantity, Type);
                Session["SessionTypesLI"] = SessionTypesLI;
            }
            else
            {
                SessionTypesLI SessionTypesLI = new SessionTypesLI(null, Type);
                Session["SessionTypesLI"] = SessionTypesLI;
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public string AddWork(string Work, string WorkDescriptionLevelThree, int ProductId, int ClientId)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            List<Users> LU = dbUsers.Users.Where(x => x.UserId == UsersId).ToList();

            string UserName = LU[0].Name + " " + LU[0].LastName + " " + LU[0].SecondLastName;
            string NickName = LU[0].NickName;

            List<Roles> LR = (from au in dbUsers.ApplicationUsers
                              join r in dbUsers.Roles
                                  on au.RoleId equals r.RoleId
                              where au.ApplicationId == ApplicationId
                              && au.UserId == UsersId
                              select r).ToList();

            string Module = string.Empty;

            if (LR[0].Description == "Administrador")
            {
                Module = "Administrador";
            }
            else if (LR[0].Description == "Diagramador")
            {
                Module = "Producción";
            }
            else if (LR[0].Description == "Laboratorio de Información")
            {
                Module = "LI";
            }
            else if (LR[0].Description == "Vendedor")
            {
                Module = "Ventas";
            }

            Work = Work.ToUpper().Trim();

            string WorkDescriptionLT = string.Empty;



            List<Works> LW = db.Database.SqlQuery<Works>("plm_spCRUDWorks @CRUDType=" + CRUD.Read + ", @WorkDescription='" + Work + "', @ProductId=" + ProductId + ", @ClientId=" + ClientId + "").ToList();

            if (LW.LongCount() == 0)
            {
                //if (!string.IsNullOrEmpty(WorkDescriptionLevelThree))
                //{
                //    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDWorks @CRUDType=" + CRUD.Create + ", @WorkDescription='" + Work + "', @UserName='" + UserName + "', @Module='" + Module + "', @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @NickName=" + NickName + ", @WorkDescriptionLevelThree=" + WorkDescriptionLevelThree + "");
                //}
                //else
                //{
                //    var _result = db.Database.ExecuteSqlCommand("plm_spCRUDWorks @CRUDType=" + CRUD.Create + ", @WorkDescription='" + Work + "', @UserName='" + UserName + "', @Module='" + Module + "', @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @NickName=" + NickName + "");
                //}

                var _result = db.Database.ExecuteSqlCommand("plm_spCRUDWorks @CRUDType=" + CRUD.Create + ", @WorkDescription='" + Work + "', @UserName='" + UserName + "', @Module='" + Module + "', @ProductId=" + ProductId + ", @ClientId=" + ClientId + ", @NickName=" + NickName + ", @WorkDescriptionLevelThree='" + WorkDescriptionLevelThree + "'");

                return "Ok";
            }
            else
            {
                return Work;
            }
        }

        public JsonResult GetWorks()
        {
            List<Work> LW = GetData.GetWorks();

            return Json(LW, JsonRequestBehavior.AllowGet);
        }

        public JsonResult disableWork(string Work, string Product, string Client)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int WorkId = int.Parse(Work);

            //String Message = GetMessage(UsersId, ApplicationId, WorkId);

            if (!string.IsNullOrEmpty(Product))
            {
                int ProductId = int.Parse(Product);
                int ClientId = int.Parse(Client);

                List<Works> LW = db.Works.Where(x => x.WorkId == WorkId).ToList();

                if (LW.LongCount() > 0)
                {
                    foreach (Works _w in LW)
                    {
                        _w.Active = false;

                        db.SaveChanges();
                    }
                }

                //EmailCloseWork(Message);

                List<Works> LWS = db.Works.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.Active == true).ToList();

                if (LWS.LongCount() > 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<Works> LW = db.Works.Where(x => x.WorkId == WorkId).ToList();

                if (LW.LongCount() > 0)
                {
                    foreach (Works _w in LW)
                    {
                        _w.Active = false;

                        db.SaveChanges();
                    }
                }

                //EmailCloseWork(Message);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCategoryByProductByClient(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<Works> LW = db.Works.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).OrderBy(x => x.WorkDescription).Distinct().ToList();

            LW = LW.OrderBy(x => x.WorkDescription).ToList();

            String Categories = String.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append("<table style =\"width:100%\" class=\"table-hover\">");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th style =\"width:40%\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Categoría Nivel 3</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:40%\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Categoría Nivel 4</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:10%;text-align:center\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Activa</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:10%;text-align:center\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Detalle</label>");
            sb.Append("</th>");
            sb.Append("</tr>");
            sb.Append("<tbody>");

            foreach (Works item in LW)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<label style=\"font-size:12px; font-weight:100\">&bull; <span id=\"lblWorkDescriptionLevelThree\">" + item.WorkDescriptionLevelThree + "</span></label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label style=\"font-size:12px; font-weight:100\">&bull; <span id=\"lblWorkDescription\">" + item.WorkDescription + "</span></label>");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center\">");
                if (item.Active == true)
                {
                    sb.Append("<button class=\"btn btn-success btn-sm ldr\" onclick=\"CloseWorkLI($(this))\" id=\"DisableWorkLI\" value=\"" + item.WorkId + "\"><span class=\"glyphicon glyphicon-ok\"></span></button>");
                }
                else
                {
                    sb.Append("<button class=\"btn btn-warning btn-sm ldr\"><span class=\"glyphicon glyphicon-remove\"></span></button>");
                }
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center\">");
                sb.Append("<button class=\"btn btn-primary btn-sm ldr\" onclick=\"AddComentToAddCategory($(this))\" id=\"" + item.WorkId + "\" value=\"" + item.WorkDescription + "\"><span class=\"glyphicon glyphicon-question-sign\"></span></button>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</thead>");

            sb.Append("</table>");

            //if (LW.LongCount() == 1)
            //{
            //    foreach (Works item in LW)
            //    {
            //        Categories = "&bull; " + LW[0].WorkDescription;

            //    }   
            //}
            //else if ((LW.LongCount() > 1) || (LW.LongCount() == 0))
            //{
            //    foreach (Works item in LW)
            //    {

            //        Categories = Categories + "&bull; " + item.WorkDescription + "<br /> ";

            //    }
            //}

            Categories = sb.ToString();

            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoryByProductByClientSM(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<Works> LW = db.Works.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).OrderBy(x => x.WorkDescription).Distinct().ToList();

            LW = LW.OrderBy(x => x.WorkDescription).ToList();

            String Categories = String.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append("<table style =\"width:100%\" class=\"table-hover\">");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th style =\"width:40%\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Categoría Nivel 3</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:40%\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Categoría Nivel 4</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:10%;text-align:center\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Activa</label>");
            sb.Append("</th>");
            sb.Append("<th style =\"width:10%;text-align:center\">");
            sb.Append("<label style=\"font-size: 14px; color: #003870\">Detalle</label>");
            sb.Append("</th>");
            sb.Append("</tr>");
            sb.Append("<tbody>");

            foreach (Works item in LW)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<label style=\"font-size:12px; font-weight:100\">&bull; " + item.WorkDescriptionLevelThree + "</label>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<label style=\"font-size:12px; font-weight:100\">&bull; " + item.WorkDescription + "</label>");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center\">");
                if (item.Active == true)
                {
                    sb.Append("<button class=\"btn btn-success btn-sm ldr\"><span class=\"glyphicon glyphicon-ok\"></span></button>");
                }
                else
                {
                    sb.Append("<button class=\"btn btn-warning btn-sm ldr\"><span class=\"glyphicon glyphicon-remove\"></span></button>");
                }
                sb.Append("</td>");
                sb.Append("<td style=\"text-align:center\">");
                if (!string.IsNullOrEmpty(item.Details))
                {
                    sb.Append("<button class=\"btn btn-primary btn-sm ldr\" onclick=\"openComentToAddCategory($(this).val())\" value=\"" + item.WorkId + "\"><span class=\"glyphicon glyphicon-question-sign\"></span></button>");
                }
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</thead>");

            sb.Append("</table>");

            //if (LW.LongCount() == 1)
            //{
            //    foreach (Works item in LW)
            //    {
            //        Categories = "&bull; " + LW[0].WorkDescription;

            //    }   
            //}
            //else if ((LW.LongCount() > 1) || (LW.LongCount() == 0))
            //{
            //    foreach (Works item in LW)
            //    {

            //        Categories = Categories + "&bull; " + item.WorkDescription + "<br /> ";

            //    }
            //}

            Categories = sb.ToString();

            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailsByWork(string Work)
        {
            int WorkId = int.Parse(Work);

            List<Works> LW = db.Works.Where(x => x.WorkId == WorkId).ToList();

            String Details = LW[0].Details;

            if (!string.IsNullOrEmpty(Details))
            {
                return Json(Details, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveDetails(string Work, string Details)
        {
            int WorkId = int.Parse(Work);

            List<Works> LW = db.Works.Where(x => x.WorkId == WorkId).ToList();

            foreach (Works item in LW)
            {
                if (!string.IsNullOrEmpty(Details))
                {
                    item.Details = Details.Trim();
                }
                else
                {
                    item.Details = null;
                }

                db.SaveChanges();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoriestoAdd(string Product, string Client)
        {
            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            List<Works> LWS = new List<Works>();
            Works W = new Works();

            List<Works> LW = db.Works.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.Active == true).OrderBy(x => x.WorkDescription).Distinct().ToList();

            foreach (Works item in LW)
            {
                W = new Works();

                W.Active = item.Active;
                W.AddedDate = item.AddedDate;
                W.ClientId = item.ClientId;
                W.Details = item.Details;
                W.Module = item.Module;
                W.ProductId = item.ProductId;
                W.UserName = item.UserName;
                W.WorkDescription = item.WorkDescription;
                W.WorkId = item.WorkId;

                LWS.Add(W);
            }


            return Json(LWS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveNews(String ListItems, string Size, string Client, string Product)
        {
            int SizeId = int.Parse(Size);
            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);

            dynamic d = JsonConvert.DeserializeObject(ListItems);

            String error = String.Empty;

            for (var i = 0; i <= SizeId - 1; i++)
            {
                string item = d[i]["Category4"];
                string Category3 = d[i]["Category3"];

                string response = AddWork(item, Category3, ProductId, ClientId);

                if (response != "Ok")
                {
                    error = error + response + " ";
                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                var result = new { Result = false, Category = error };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Work> LS = new List<Work>();
                try
                {
                    LS = GetData.GetWorks();
                }
                catch (Exception e)
                {

                }
                var result = new { Result = true, List = LS };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveNewsNew(string Work, string WorkN3, string Product, string Client)
        {

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            string result = AddWork(Work, WorkN3, ProductId, ClientId);

            if (result != "Ok")
            {
                var response = new { Result = false, Category = result };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Work> LS = GetData.GetWorks();

                var response = new { Result = true, List = LS };

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        /*          SEND EMAIL CLOSE PILLBOOK             */

        static void EmailCloseWork(object Message)
        {

            //Se crea un hilo que iniciará llamando al método TareaLenta
            System.Threading.Thread tarea =
                new System.Threading.Thread(
                    new System.Threading.ParameterizedThreadStart(CloseWork));
            //Se inicia la otra tarea

            tarea.Start(Message);

        }

        static void CloseWork(object Message)
        {

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            /*-------------------------CORREO RECEPTOR-------------------------*/

            mmsg.To.Add("miguel.ramirez@plmlatina.com");
            //mmsg.To.Add("erick.morales@plmlatina.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            /*-------------------------ASUNTO-------------------------*/


            mmsg.Subject = "Cierre de Tarea";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;


            /*-------------------------CORREO A ENVÍAR COPIA-------------------------*/

            //mmsg.Bcc.Add("beatriz.vazquez@plmlatina.com"); //Opcional

            //mmsg.CC.Add("nalleli.lopez@plmlatina.com, luis.mendoza@plmlatina.com");


            /*-------------------------CUERPO DEL CORREO-------------------------*/

            mmsg.Body = Message.ToString();

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Enviar Correo como HTML


            /*-------------------------CORREO EMISOR-------------------------*/


            ////mmsg.From = new System.Net.Mail.MailAddress(mail);

            mmsg.From = new System.Net.Mail.MailAddress("Guianet@plmlatina.com");


            /*-------------------------OBJETO TIPO CLIENTE DE CORREO-------------------------*/

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


            /*-------------------------CREDENCIALES DEL CORREO EMISOR-------------------------*/

            cliente.Credentials =
                new System.Net.NetworkCredential("Guianet@plmlatina.com", "Guianet_System");

            cliente.Host = "plmlatina.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }

        public static string GetMessage(int UsersId, int ApplicationId, int WorkId)
        {
            PLMUsers plm = new PLMUsers();
            GuiaEntities db = new GuiaEntities();
            GetData GetData = new Models.GetData();

            var usr = plm.Users.Where(x => x.UserId == UsersId).ToList();

            List<Roles> rl = (from au in plm.ApplicationUsers
                              join r in plm.Roles
                                  on au.RoleId equals r.RoleId
                              where au.UserId == UsersId
                              && au.ApplicationId == ApplicationId
                              select r).ToList();

            string Module = "";

            if (rl[0].Description == "Diagramador")
            {
                Module = "Producción";
            }

            if (rl[0].Description == "Vendedor")
            {
                Module = "Ventas";
            }
            else
            {
                Module = rl[0].Description;
            }

            string mail = string.Empty;

            foreach (Users _usr in usr)
            {
                mail = _usr.Email;
            }

            List<Work> LW = GetData.GetWorkById(WorkId);

            StringBuilder sb = new StringBuilder();

            sb.Append("<h3 style=\"color:#003870;\">Ha sido cerrada una tarea.</h3>");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<br />");
            //sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            //sb.Append("<div style='margin-left:50px'>");
            //sb.Append("&bull; El Usuario: <b>" + usr[0].Name.Trim() + " " +
            //            usr[0].LastName.Trim() + " " +
            //            usr[0].SecondLastName.Trim() +
            //            " (" + usr[0].NickName + ")" + "</b> " +
            //            " del Módulo: <b>" + Module + "</b>" +
            //          " ha cerrado la Tarea: <label style='color: #065977'><b><i>" + LW[0].WorkDescription + "</i></b>" +
            //          " que fue activada por <i>" + LW[0].UserName + "</i> del Módulo <i>" + LW[0].Module + "</i> el <i>" + LW[0].Added + "</i></label></div>");


            sb.Append("<table style=\"width:100%;border-bottom:none; border-top:none;border-left:none; border-right:none\" border=\"1\">");
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align:center;border-bottom:none; border-top:none;border-left:none; border-right:none\" colspan=\"2\"><h3 style=\"color:#003870;\">" + LW[0].WorkDescription + "</h3></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align:center;border-bottom:none; border-top:none;border-left:none; border-right:none\" colspan=\"2\"><br /><br /></td>");
            sb.Append("<br />");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Cliente:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + LW[0].CompanyName + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Producto:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + LW[0].ProductName + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align:center;border-bottom:none; border-top:none;border-left:none; border-right:none\" colspan=\"2\"><br /><br /></td>");
            sb.Append("<br />");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Usuario que cerró Tarea:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + usr[0].Name.Trim() + " " + usr[0].LastName.Trim() + " " + usr[0].SecondLastName.Trim() + " (" + usr[0].NickName + ")" + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Módulo que cerró Tarea:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + Module + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"text-align:center;border-bottom:none; border-top:none;border-left:none; border-right:none\" colspan=\"2\"><br /><br /></td>");
            sb.Append("<br />");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Usuario que activó Tarea:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + LW[0].UserName + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Módulo que activó Tarea:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\">" + LW[0].Module + "</span></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=\"width:30%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"color:#003870;font-weight:bold\">Fecha en que se activó la Tarea:</span></td>");
            sb.Append("<td style=\"width:70%;border-bottom-color:#003870; border-top:none;border-left:none; border-right:none\"><span style=\"font-weight:bold\"> " + LW[0].Added + "</span></td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            String Message = sb.ToString();

            return Message;
        }

    }

}