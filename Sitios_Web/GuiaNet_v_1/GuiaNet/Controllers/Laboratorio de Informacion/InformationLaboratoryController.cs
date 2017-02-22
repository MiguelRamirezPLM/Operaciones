using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace GuiaNet.Controllers.Laboratorio_de_Informacion
{
    public class InformationLaboratoryController : Controller
    {
        Guia db = new Guia();
        ClientProductCategories ClientProductCategories = new ClientProductCategories();
        EditionClientProducts EditionClientProducts = new EditionClientProducts();
        ActivityLog ActivityLog = new ActivityLog();

        public ActionResult Index(string CountryId, string ClientId, string EditionId, string BookId, string Type)
        {
            sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];

            if (ClientId != null)
            {
                List<ParticipantProductsClas> LS = new List<ParticipantProductsClas>();
                int count = 0;
                int _clienid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);

                if (!string.IsNullOrEmpty(Type))
                {
                    int TypeId = int.Parse(Type);
                    if (TypeId == 0)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty == TypeId).ToList();
                    }
                    else if (TypeId == 1)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty > 0).ToList();
                    }
                    else
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                    }
                }
                else
                {
                    LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                }

                foreach (ParticipantProductsClas p in LS)
                {
                    count = count + 1;
                }
                if (count > 0)
                {
                    ViewData["Count"] = count;
                }
                else
                {
                    ViewData["Count"] = null;
                }
                bool flag = false;
                string param = string.Empty;
                int? productid = null;
                sessioninformationlaboratoryproducts session = new sessioninformationlaboratoryproducts(_countryid, _clienid, _editionid, _book, flag, param, productid);
                Session["sessioninformationlaboratoryproducts"] = session;
                return View(LS);
            }
            if (ind != null)
            {
                List<ParticipantProductsClas> LS = new List<ParticipantProductsClas>();

                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                if (!string.IsNullOrEmpty(Type))
                {
                    int TypeId = int.Parse(Type);
                    if (TypeId == 0)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty == TypeId).ToList();
                    }
                    else if (TypeId == 1)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty > 0).ToList();
                    }
                    else
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                    }
                }
                else
                {
                    LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                }

                ViewData["Count"] = 0;
                return View(LS);
            }
            else
            {

                List<ParticipantProductsClas> LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + 0 + ", @EditionId=" + 0 + "").ToList();

                ViewData["Count"] = 0;
                return View(LS);
            }

        }

        public ActionResult clasification(int? productid, string Description)
        {
            sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            seachcategory seachcat = (seachcategory)Session["seachcategory"];
            if (ind != null)
            {
                if (productid != null)
                {
                    ind.PId = productid;
                }
                if (Description != null)
                {
                    if (Description == string.Empty)
                    {
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("clasification", _response);
                    }
                    else
                    {
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + Description + "'").OrderBy(x => x.ParentId).ToList();
                        seachcategory seachcategory = new seachcategory(Description);
                        Session["seachcategory"] = seachcategory;
                        return View("clasification", _response);
                    }
                }
                if (seachcat != null)
                {
                    var desc = seachcat.Description;
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                    return View("clasification", _response);
                }
                if (Description == null)
                {
                    if (ind != null)
                    {
                        bool flag = ind.flag;
                        if (flag == true)
                        {
                            var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                            return View("clasification", _response);
                        }
                        else
                        {
                            var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                            return View("clasification", _response);
                        }
                    }
                    else
                    {
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("clasification", _response);
                    }
                }
                else
                {
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                    return View("clasification", _response);
                }
            }
            else
            {
                Response.Redirect(Url.Action("Logout", "Login"));
                return View();
            }
        }

        public ActionResult searchasociatecategories(string Description)
        {
            if (Description != null)
            {
                if (Description == string.Empty)
                {
                    sessioninformationlaboratoryproducts index = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
                    index.flag = false;
                    index.param = Description;
                    return RedirectToAction("clasification", "InformationLaboratory");
                }
                else
                {
                    sessioninformationlaboratoryproducts index = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
                    index.flag = true;
                    index.param = Description;
                    int clientid = index.ClId;
                    var cats = (from cat in db.Categories
                                join cc in db.ClientCategories
                                on cat.CategoryId equals cc.CategoryId
                                where cat.Description.StartsWith(Description)
                                && cc.ClientId == clientid
                                select cat).ToList();
                    return RedirectToAction("clasification", "InformationLaboratory");
                }
            }
            else
            {
                return RedirectToAction("clasification", "InformationLaboratory");
            }
        }

        public ActionResult insertproductcategoriess(int Id)
        {
            seachcategory seachcat = (seachcategory)Session["seachcategory"];
            sessioninformationlaboratoryproducts index = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            int _clientid = index.ClId;
            int editionid = index.EId;
            int _productid = Convert.ToInt32(index.PId);
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            var cats = (from cat in db.Categories
                        where cat.ParentId == Id
                        select cat).ToList();
            if (cats.LongCount() > 0)
            {
                if (seachcat != null)
                {
                    ViewData["parentid"] = "La Categoria NO puede asociarse si tiene Subcategorías";
                    var desc = seachcat.Description;
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                    return View("clasification", _response);
                }
                else
                {
                    ViewData["parentid"] = "La Categoria NO puede asociarse si tiene Subcategorías";
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                    return View("clasification", _response);
                }

            }
            else
            {
                var cp = (from clientp in db.ClientProductCategories
                          where clientp.CategoryId == Id
                          && clientp.ClientId == _clientid
                          && clientp.ProductId == _productid
                          select clientp).ToList();
                if (cp.LongCount() == 0)
                {
                    ClientProductCategories.CategoryId = Id;
                    ClientProductCategories.ClientId = _clientid;
                    ClientProductCategories.ProductId = _productid;

                    db.ClientProductCategories.Add(ClientProductCategories);
                    db.SaveChanges();

                    ActivityLog._insertclientproductcategories(_clientid, _productid, Id, ApplicationId, UsersId);
                }


                var ecp = (from editioncp in db.EditionClientProducts
                           where editioncp.CategoryId == Id
                           && editioncp.ClientId == _clientid
                           && editioncp.EditionId == editionid
                           && editioncp.ProductId == _productid
                           select editioncp).ToList();
                if (ecp.LongCount() == 0)
                {
                    EditionClientProducts EditionClientProducts = new EditionClientProducts();

                    EditionClientProducts.CategoryId = Id;
                    EditionClientProducts.ClientId = _clientid;
                    EditionClientProducts.EditionId = editionid;
                    EditionClientProducts.ProductId = _productid;

                    db.EditionClientProducts.Add(EditionClientProducts);
                    db.SaveChanges();

                    //foreach(EditionClientProducts _ecp in ecp)
                    //{
                    //    _ecp.CategoryId = Id;
                    //}
                    //db.SaveChanges();

                    ActivityLog._inserteditionclientproducts(_clientid, _productid, Id, editionid, ApplicationId, UsersId);
                }

                if ((cp.LongCount() > 0) && (ecp.LongCount() > 0))
                {
                    ViewData["existcat"] = "La Categoria ya esta Asociada al Producto";
                    if (seachcat != null)
                    {
                        var desc = seachcat.Description;
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();
                        return View("clasification", _response);
                    }
                    else
                    {
                        var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                        return View("clasification", _response);
                    }
                }
                else
                {
                    return RedirectToAction("clasification", "InformationLaboratory");
                }
            }
        }

        public ActionResult deleteproductcategories(int categoryid, int? categoryidnodo)
        {
            sessioninformationlaboratoryproducts index = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
            int _clientid = index.ClId;
            int _editionid = index.EId;
            int _productid = Convert.ToInt32(index.PId);

            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            if (categoryidnodo != null)
            {
                var ecp = (from editioncp in db.EditionClientProducts
                           where editioncp.CategoryId == categoryidnodo
                           && editioncp.ClientId == _clientid
                           && editioncp.ProductId == _productid
                           select editioncp).ToList();
                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts e in ecp)
                    {
                        var delete = db.EditionClientProducts.SingleOrDefault(x => x.CategoryId == categoryidnodo && x.ClientId == _clientid && x.ProductId == _productid);
                        db.EditionClientProducts.Remove(delete);
                        db.SaveChanges();

                        int categoryidn = Convert.ToInt32(categoryidnodo);
                        ActivityLog._deleteeditionclientproducts(_clientid, _productid, categoryidn, _editionid, ApplicationId, UsersId);

                    }
                }

                var cc = (from clientpc in db.ClientProductCategories
                          where clientpc.CategoryId == categoryidnodo
                          && clientpc.ClientId == _clientid
                          && clientpc.ProductId == _productid
                          select clientpc).ToList();
                foreach (ClientProductCategories c in cc)
                {
                    var delete = db.ClientProductCategories.SingleOrDefault(x => x.CategoryId == categoryidnodo && x.ClientId == _clientid && x.ProductId == _productid);
                    db.ClientProductCategories.Remove(delete);
                    db.SaveChanges();
                    int categoryidn = Convert.ToInt32(categoryidnodo);

                    ActivityLog._deleteclientproductcategories(_clientid, _productid, categoryidn, ApplicationId, UsersId);
                }
                return RedirectToAction("clasification", "InformationLaboratory");
            }
            else
            {
                var ecp = (from editioncp in db.EditionClientProducts
                           where editioncp.CategoryId == categoryid
                           && editioncp.ClientId == _clientid
                           && editioncp.EditionId == _editionid
                           && editioncp.ProductId == _productid
                           select editioncp).ToList();
                if (ecp.LongCount() > 0)
                {
                    foreach (EditionClientProducts e in ecp)
                    {
                        var delete = db.EditionClientProducts.SingleOrDefault(x => x.CategoryId == categoryid && x.ClientId == _clientid && x.ProductId == _productid && x.EditionId == _editionid);
                        db.EditionClientProducts.Remove(delete);
                        db.SaveChanges();

                        ActivityLog._deleteeditionclientproducts(_clientid, _productid, categoryid, _editionid, ApplicationId, UsersId);
                    }
                }

                var cc = (from clientpc in db.ClientProductCategories
                          where clientpc.CategoryId == categoryid
                          && clientpc.ClientId == _clientid
                          && clientpc.ProductId == _productid
                          select clientpc).ToList();
                foreach (ClientProductCategories c in cc)
                {
                    var delete = db.ClientProductCategories.SingleOrDefault(x => x.CategoryId == categoryid && x.ClientId == _clientid && x.ProductId == _productid);
                    db.ClientProductCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._deleteclientproductcategories(_clientid, _productid, categoryid, ApplicationId, UsersId);
                }

                return RedirectToAction("clasification", "InformationLaboratory");
            }
        }

        public ActionResult searchproduct(string ProductName)
        {
            if (ProductName != null)
            {
                if (ProductName == string.Empty)
                {
                    return RedirectToAction("Index", "InformationLaboratory");
                }
                else
                {
                    sessioninformationlaboratoryproducts ind = (sessioninformationlaboratoryproducts)Session["sessioninformationlaboratoryproducts"];
                    int _clientid = ind.ClId;
                    int _editionid = ind.EId;
                    int count = 0;
                    int typeid = 0;
                    var type = (from types in db.ProductTypes
                                where types.Description == "Productos"
                                select types).ToList();
                    foreach (ProductTypes t in type)
                    {
                        typeid = t.TypeId;
                    }

                    int size = ProductName.Length;

                    if (size <= 3)
                    {
                        List<ParticipantProductsClas> LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clientid + ", @EditionId=" + _editionid + "")
                                                                                                        .Where(x => x.ProductName.ToUpper().Trim()
                                                                                                        .Replace("Á", "A")
                                                                                                        .Replace("É", "E")
                                                                                                        .Replace("Í", "I")
                                                                                                        .Replace("Ó", "O")
                                                                                                        .Replace("Ú", "U")
                                                                                                        .Replace("Ü", "U").StartsWith(ProductName.ToUpper().Trim()
                                                                                                        .Replace("Á", "A")
                                                                                                        .Replace("É", "E")
                                                                                                        .Replace("Í", "I")
                                                                                                        .Replace("Ó", "O")
                                                                                                        .Replace("Ú", "U")
                                                                                                        .Replace("Ü", "U"))).ToList();
                        foreach (ParticipantProductsClas p in LS)
                        {
                            count = count + 1;
                        }
                        ViewData["Countresults"] = count;
                        return View("Index", LS);
                    }
                    else
                    {
                        List<ParticipantProductsClas> LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clientid + ", @EditionId=" + _editionid + "").Where(x => x.ProductName.ToUpper().Trim()
                                                                                                        .Replace("Á", "A")
                                                                                                        .Replace("É", "E")
                                                                                                        .Replace("Í", "I")
                                                                                                        .Replace("Ó", "O")
                                                                                                        .Replace("Ú", "U")
                                                                                                        .Replace("Ü", "U").Contains(ProductName.ToUpper().Trim()
                                                                                                        .Replace("Á", "A")
                                                                                                        .Replace("É", "E")
                                                                                                        .Replace("Í", "I")
                                                                                                        .Replace("Ó", "O")
                                                                                                        .Replace("Ú", "U")
                                                                                                        .Replace("Ü", "U"))).ToList();
                        foreach (ParticipantProductsClas p in LS)
                        {
                            count = count + 1;
                        }
                        ViewData["Countresults"] = count;
                        return View("Index", LS);
                    }

                }
            }
            return RedirectToAction("Index", "InformationLaboratory");
        }

        public ActionResult ClasificationHC(string CountryId, string ClientId, string EditionId, string BookId, string Type)
        {
            sessionClasificationHC ind = (sessionClasificationHC)Session["sessionClasificationHC"];

            Session["SearchCategory"] = null;

            if (ClientId != null)
            {
                List<ParticipantProductsClas> LS = new List<ParticipantProductsClas>();
                int count = 0;
                int _clienid = int.Parse(ClientId);
                int _editionid = int.Parse(EditionId);
                int _countryid = int.Parse(CountryId);
                int _book = int.Parse(BookId);

                if (!string.IsNullOrEmpty(Type))
                {
                    int TypeId = int.Parse(Type);
                    if (TypeId == 0)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty == TypeId).ToList();
                    }
                    else if (TypeId == 1)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty > 0).ToList();
                    }
                    else
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                    }
                }
                else
                {
                    LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                }

                foreach (ParticipantProductsClas p in LS)
                {
                    count = count + 1;
                }
                if (count > 0)
                {
                    ViewData["Count"] = count;
                }
                else
                {
                    ViewData["Count"] = null;
                }
                bool flag = false;
                string param = string.Empty;
                int? productid = null;
                sessionClasificationHC session = new sessionClasificationHC(_countryid, _clienid, _editionid, _book, flag, param, productid);
                Session["sessionClasificationHC"] = session;
                return View(LS);
            }
            if (ind != null)
            {
                List<ParticipantProductsClas> LS = new List<ParticipantProductsClas>();

                int _clienid = ind.ClId;
                int _editionid = ind.EId;
                int _countryid = ind.CId;
                int _book = ind.BId;

                if (!string.IsNullOrEmpty(Type))
                {
                    int TypeId = int.Parse(Type);
                    if (TypeId == 0)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty == TypeId).ToList();
                    }
                    else if (TypeId == 1)
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").Where(x => x.Qtty > 0).ToList();
                    }
                    else
                    {
                        LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                    }
                }
                else
                {
                    LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + _clienid + ", @EditionId=" + _editionid + "").ToList();
                }

                ViewData["Count"] = 0;
                return View(LS);
            }
            else
            {

                List<ParticipantProductsClas> LS = db.Database.SqlQuery<ParticipantProductsClas>("plm_spGetParticipantProductsToClasification @ClientId=" + 0 + ", @EditionId=" + 0 + "").ToList();

                ViewData["Count"] = 0;
                return View(LS);
            }
        }

        public ActionResult ClasificationProducts(int? productid)
        {
            sessionClasificationHC ind = (sessionClasificationHC)Session["sessionClasificationHC"];

            int _clientid = ind.ClId;

            ind.PId = productid;

            var cc = db.Database.SqlQuery<HomogeneousCategoriesByClientByProduct>("plm_spGetProductHomogeneousCategoriesByClientByProduct  @ProductId =" + productid + ",  @ClientId= " + _clientid + "").ToList();

            return View(cc);
        }

        public JsonResult getlevel4(string HomogeneousCategory)
        {
            List<GetHomogeneousCategories> Cats = new List<GetHomogeneousCategories>();
            SearchCategory SearchCategory = (SearchCategory)Session["SearchCategory"];

            if (SearchCategory != null)
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetHomogeneousCategories @HomogeneousCategoryId=" + HomogeneousCategoryId + ", @TextSearch='" + SearchCategory.CategoryName + "'").ToList();
            }
            else
            {
                int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

                Cats = db.Database.SqlQuery<GetHomogeneousCategories>("plm_spGetHomogeneousCategories @HomogeneousCategoryId=" + HomogeneousCategoryId + "").ToList();
            }

            return Json(Cats, JsonRequestBehavior.AllowGet);
        }

        public string insertProductLeafCategories(int ClientId, int ProductId, int CategoryId, int HomogeneousCategoryId)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            ClientProductLeafCategories CPLC = new ClientProductLeafCategories();
            //int ClientId = int.Parse(Client);
            //int ProductId = int.Parse(Product);
            //int CategoryId = int.Parse(Category);
            //int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

            if (cp.LongCount() > 0)
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId).ToList();

                if (cpl.LongCount() == 0)
                {
                    CPLC = new ClientProductLeafCategories();

                    CPLC.ClientId = ClientId;
                    CPLC.HomogeneousCategoryId = HomogeneousCategoryId;
                    CPLC.LeafCategoryId = CategoryId;
                    CPLC.ProductId = ProductId;

                    db.ClientProductLeafCategories.Add(CPLC);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, HomogeneousCategoryId, CategoryId, 1, ApplicationId, UsersId);

                    //return Json(true, JsonRequestBehavior.AllowGet);
                    return "Ok";
                }
                else
                {
                    //return Json("_exist", JsonRequestBehavior.AllowGet);
                    return "_exist";
                }
            }
            else
            {
                //return Json("_cpnotexist", JsonRequestBehavior.AllowGet);
                return "_cpnotexist";
            }
        }

        public JsonResult deleteProductLeafCategories(string Client, string Product, string Category, string HomogeneousCategory)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int ClientId = int.Parse(Client);
            int ProductId = int.Parse(Product);
            int CategoryId = int.Parse(Category);
            int HomogeneousCategoryId = int.Parse(HomogeneousCategory);

            try
            {
                var cpl = db.ClientProductLeafCategories.Where(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId).ToList();

                if (cpl.LongCount() > 0)
                {
                    var delete = db.ClientProductLeafCategories.SingleOrDefault(x => x.ClientId == ClientId && x.ProductId == ProductId && x.LeafCategoryId == CategoryId && x.HomogeneousCategoryId == HomogeneousCategoryId);

                    db.ClientProductLeafCategories.Remove(delete);
                    db.SaveChanges();

                    ActivityLog._ClientProductLeafCategories(ClientId, ProductId, HomogeneousCategoryId, CategoryId, 4, ApplicationId, UsersId);
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
                SearchCategory SearchCategory = new SearchCategory(CategoryName);
                Session["SearchCategory"] = SearchCategory;
            }
            else
            {
                SearchCategory SearchCategory = new SearchCategory(CategoryName);
                Session["SearchCategory"] = SearchCategory;
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult ProductWithoutDescription(string Product, string Client, string Operation)
        //{
        //    try
        //    {
        //        CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
        //        int ApplicationId = user.ApplicationId;
        //        int UsersId = user.userId;

        //        if (Operation == "Insert")
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.ProductWithoutDescription = true;

        //                    db.SaveChanges();
        //                }

        //                ActivityLog._updateClientProducts(ClientId, ProductId, "ProductWithoutDescription", true, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.ProductWithoutDescription = false;

        //                    db.SaveChanges();
        //                }

        //                ActivityLog._updateClientProducts(ClientId, ProductId, "ProductWithoutDescription", false, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult ProductWithoutName(string Product, string Client, string Operation)
        //{
        //    try
        //    {
        //        CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
        //        int ApplicationId = user.ApplicationId;
        //        int UsersId = user.userId;

        //        if (Operation == "Insert")
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.ProductWithoutName = true;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "ProductWithoutName", true, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.ProductWithoutName = false;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "ProductWithoutName", false, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult NoProductInformation(string Product, string Client, string Operation)
        //{
        //    try
        //    {
        //        CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
        //        int ApplicationId = user.ApplicationId;
        //        int UsersId = user.userId;

        //        if (Operation == "Insert")
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.NoProductInformation = true;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "NoProductInformation", true, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.NoProductInformation = false;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "NoProductInformation", false, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult RequierdCategoryOrProductLline(string Product, string Client, string Operation)
        //{
        //    try
        //    {
        //        CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
        //        int ApplicationId = user.ApplicationId;
        //        int UsersId = user.userId;

        //        if (Operation == "Insert")
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.RequierdCategoryOrProductLline = true;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "RequierdCategoryOrProductLline", true, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.RequierdCategoryOrProductLline = false;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "RequierdCategoryOrProductLline", false, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult DuplicatedProduct(string Product, string Client, string Operation)
        //{
        //    try
        //    {
        //        CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
        //        int ApplicationId = user.ApplicationId;
        //        int UsersId = user.userId;

        //        if (Operation == "Insert")
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.DuplicatedProduct = true;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "DuplicatedProduct", true, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            int ProductId = int.Parse(Product);
        //            int ClientId = int.Parse(Client);

        //            var cp = db.ClientProducts.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

        //            if (cp.LongCount() > 0)
        //            {
        //                foreach (ClientProducts _cp in cp)
        //                {
        //                    _cp.DuplicatedProduct = false;

        //                    db.SaveChanges();
        //                }
        //                ActivityLog._updateClientProducts(ClientId, ProductId, "DuplicatedProduct", false, ApplicationId, UsersId);
        //            }

        //            return Json(true, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult getproductstatus(string Product, string Client)
        {
            List<Status> LS = new List<Status>();
            Status S = new Status();

            List<int> LI = new List<int>();

            int ProductId = int.Parse(Product);
            int ClientId = int.Parse(Client);

            var s = db.Status.Where(x => x.Active == true).OrderBy(x => x.StatusName).ToList();

            var ps = db.ProductStatus.Where(x => x.ClientId == ClientId && x.ProductId == ProductId).ToList();

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

        public JsonResult saveCategories(String ListItems, string ArraySize)
        {

            dynamic d = JsonConvert.DeserializeObject(ListItems);

            int Size = int.Parse(ArraySize);


            for (var i = 0; i <= Size - 1; i++)
            {
                var Id = Convert.ToInt32(d[i]["Id"]);
                var CId = Convert.ToInt32(d[i]["CId"]);
                var PId = Convert.ToInt32(d[i]["PId"]);
                var HCId = Convert.ToInt32(d[i]["HCId"]);

                string a = insertProductLeafCategories(CId, PId, Id, HCId);
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}