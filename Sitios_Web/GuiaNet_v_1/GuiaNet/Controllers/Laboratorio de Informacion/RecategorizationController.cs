using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;

namespace GuiaNet.Controllers.Laboratorio_de_Informacion
{
    public class RecategorizationController : Controller
    {
        Guia db = new Guia();
        MatchCategories MatchCategories = new MatchCategories();
        ClientCategories ClientCategories = new ClientCategories();
        ClientProductCategories ClientProductCategories = new ClientProductCategories();
        HeterogeneousCategories HeterogeneousCategories = new HeterogeneousCategories();
        ActivityLog ActivityLog = new ActivityLog();
        classreplace classreplace = new classreplace();
        insertcategoriessincelevel1 insertcategoriessincelevel1 = new insertcategoriessincelevel1();
        EditionClientCategories EditionClientCategories = new EditionClientCategories();
        EditionClientHeterogeneousCategories EditionClientHeterogeneousCategories = new EditionClientHeterogeneousCategories();

        public ActionResult categories(string Description)
        {
            seachcategory seachcat = (seachcategory)Session["seachcategory"];
            sessionrecategorization _sbool = (sessionrecategorization)Session["sessionrecategorization"];
            searchhcategory seachcats = (searchhcategory)Session["searchhcategory"];

            if (Description != null)
            {
                if (Description == string.Empty)
                {
                    seachcategory seachcategory = new seachcategory(Description);
                    Session["seachcategory"] = seachcategory;
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                    return View("categories", _response);
                }
                else
                {
                    var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + Description + "'").OrderBy(x => x.ParentId).ToList();
                    seachcategory seachcategory = new seachcategory(Description);
                    if (_sbool != null)
                    {
                        if (_sbool.flag == true)
                        {
                            Session["seachcategory"] = seachcategory;
                            return View("categories", _response);
                        }
                        else
                        {
                            bool flag = false;
                            sessionrecategorization _bool = new sessionrecategorization(flag);
                            Session["sessionrecategorization"] = _bool;
                            Session["seachcategory"] = seachcategory;
                            return View("categories");
                        }
                    }
                    else
                    {
                        bool flag = false;
                        sessionrecategorization _bool = new sessionrecategorization(flag);
                        Session["sessionrecategorization"] = _bool;
                        Session["seachcategory"] = seachcategory;
                        return View("categories", _response);
                    }
                }
            }
            if (seachcats != null)
            {
                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }
            if (seachcat != null)
            {
                seachcategory seachcategory = new seachcategory(Description);
                Session["seachcategory"] = seachcategory;
                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }
            if (Description == null)
            {
                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }
            else
            {
                var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
                return View("categories", _response);
            }
        }

        public ActionResult hcategories(string Description)
        {
            sessionrecategorization _sbool = (sessionrecategorization)Session["sessionrecategorization"];
            searchhcategory seachcat = (searchhcategory)Session["searchhcategory"];

            if (Description != null)
            {
                if (Description == string.Empty)
                {
                    return RedirectToAction("categories");
                }
                else
                {
                    var _response = db.Database.SqlQuery<HeterogeneousCategories>("plm_spGetHeterogeneousCategories @param='" + Description + "'").OrderBy(x => x.ParentId).ToList();
                    searchhcategory seachcategory = new searchhcategory(Description);
                    Session["searchhcategory"] = seachcategory;
                    bool flag = true;
                    sessionrecategorization _bool = new sessionrecategorization(flag);
                    Session["sessionrecategorization"] = _bool;

                    //_sbool.flag = true;

                    return View("categories");
                }
            }
            if (seachcat != null)
            {
                var desc = seachcat.Description;
                var _response = db.Database.SqlQuery<HeterogeneousCategories>("plm_spGetHeterogeneousCategories @param='" + desc + "'").OrderBy(x => x.ParentId).ToList();

                _sbool.flag = true;

                return View("categories", _response);
            }
            if (Description == null)
            {

                List<HeterogeneousCategories> all = new List<HeterogeneousCategories>();
                all = db.HeterogeneousCategories.OrderBy(a => a.ParentId).Take(0).ToList();
                ViewData["Count"] = null;
                return View("categories", all);
            }
            else
            {
                List<HeterogeneousCategories> all = new List<HeterogeneousCategories>();
                all = db.HeterogeneousCategories.OrderBy(a => a.ParentId).OrderBy(a => a.Description).Take(0).ToList();
                ViewData["Count"] = null;
                return View("categories", all);
            }
        }

        public ActionResult recat(string cat)
        {
            int CategoryId = int.Parse(cat);
            var catt = (from cats in db.Categories
                        where cats.ParentId == CategoryId
                        select cats).ToList();

            if (catt.LongCount() > 0)
            {
                return Json("_error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lev = System.Configuration.ConfigurationManager.AppSettings["Level"];
                int level = int.Parse(lev);
                var cattt = (from cats in db.Categories
                             where cats.CategoryId == CategoryId
                             && cats.Level == level
                             select cats).ToList();
                if (cattt.LongCount() == 0)
                {
                    return Json("_error", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    sessionrecatid sessionrecatid = new sessionrecatid(CategoryId);
                    Session["sessionrecatid"] = sessionrecatid;


                    string Description = string.Empty;
                    var catts = (from cats in db.Categories
                                 where cats.CategoryId == CategoryId
                                 select cats).ToList();
                    if (catts.LongCount() > 0)
                    {
                        foreach (Categories c in catts)
                        {
                            Description = c.Description;
                        }
                        searchcategoryref searchcategoryref = new Models.searchcategoryref(Description);
                        Session["searchcategoryref"] = searchcategoryref;
                    }

                    return Json(CategoryId, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult recategorization(int Id)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            sessionrecatid sessionrecatid = (sessionrecatid)Session["sessionrecatid"];
            var cat = (from _cat in db.Categories
                       where _cat.ParentId == Id
                       select _cat).ToList();
            if (cat.LongCount() > 0)
            {
                ViewData["hetcat"] = "La Categoria NO puede ser recategorizada si tiene Subcategorías";
                return View("categories");
            }
            else
            {
                if (sessionrecatid != null)
                {
                    int categoryid = sessionrecatid.CategoryId;

                    var mc = (from matchc in db.MatchCategories
                              where matchc.CategoryId == categoryid
                              && matchc.HeterogeneousCategoryId == Id
                              select matchc).ToList();

                    if (mc.LongCount() == 0)
                    {
                        MatchCategories.CategoryId = categoryid;
                        MatchCategories.HeterogeneousCategoryId = Id;
                        MatchCategories.AddedDate = DateTime.Now;

                        db.MatchCategories.Add(MatchCategories);
                        db.SaveChanges();

                        ActivityLog._insertMatchCategories(categoryid, Id, ApplicationId, UsersId);
                    }


                    var chc = (from clienthc in db.ClientHeterogeneousCategories
                               where clienthc.HeterogeneousCategoryId == Id
                               select clienthc).ToList();
                    if (chc.LongCount() > 0)
                    {
                        foreach (ClientHeterogeneousCategories _chc in chc)
                        {
                            ClientCategories = new Models.ClientCategories();

                            var clientc = (from ccat in db.ClientCategories
                                           where ccat.CategoryId == categoryid
                                           && ccat.ClientId == _chc.ClientId
                                           select ccat).ToList();
                            if (clientc.LongCount() == 0)
                            {
                                ClientCategories.CategoryId = categoryid;
                                ClientCategories.ClientId = _chc.ClientId;

                                db.ClientCategories.Add(ClientCategories);
                                db.SaveChanges();

                                ActivityLog._insertclientcategories(categoryid, _chc.ClientId, ApplicationId, UsersId);


                            }
                        }

                        var ccats = (from ccc in db.EditionClientHeterogeneousCategories
                                     where ccc.HeterogeneousCategoryId == Id
                                     select ccc).ToList();
                        if (ccats.LongCount() > 0)
                        {
                            foreach (EditionClientHeterogeneousCategories _ccat in ccats)
                            {
                                EditionClientCategories = new Models.EditionClientCategories();

                                var ccatss = (from ccc in db.EditionClientCategories
                                              where ccc.CategoryId == categoryid
                                              && ccc.ClientId == _ccat.ClientId
                                              && ccc.EditionId == _ccat.EditionId
                                              select ccc).ToList();
                                if (ccatss.LongCount() == 0)
                                {
                                    EditionClientCategories.CategoryId = categoryid;
                                    EditionClientCategories.ClientId = _ccat.ClientId;
                                    EditionClientCategories.EditionId = _ccat.EditionId;

                                    db.EditionClientCategories.Add(EditionClientCategories);
                                    db.SaveChanges();

                                    ActivityLog._inserteditionclientcategories(_ccat.ClientId, Id, _ccat.EditionId, ApplicationId, UsersId);
                                }
                              
                            }
                        }

                        var hc = (from hcat in db.HeterogeneousCategories
                                  where hcat.HeterogeneousCategoryId == Id
                                  select hcat).ToList();
                        if (hc.LongCount() > 0)
                        {
                            foreach (HeterogeneousCategories hhc in hc)
                            {
                                hhc.Active = false;

                                db.SaveChanges();

                                ActivityLog._updateHeterogeneousCategories(hhc.HeterogeneousCategoryId, hhc.ParentId, hhc.Description, hhc.Level, ApplicationId, UsersId);

                                var hcct = (from cath in db.HeterogeneousCategories
                                            where cath.HeterogeneousCategoryId == hhc.ParentId
                                            select cath).ToList();
                                if (hcct.LongCount() > 0)
                                {
                                    foreach (HeterogeneousCategories _hcct in hcct)
                                    {
                                        var hcct1 = (from cath in db.HeterogeneousCategories
                                                     where cath.ParentId == _hcct.HeterogeneousCategoryId
                                                     && cath.Active == true
                                                     select cath).ToList();
                                        if (hcct1.LongCount() == 0)
                                        {
                                            var hccat = (from cath in db.HeterogeneousCategories
                                                         where cath.HeterogeneousCategoryId == _hcct.HeterogeneousCategoryId
                                                         && cath.Active == true
                                                         select cath).ToList();
                                            if (hccat.LongCount() > 0)
                                            {
                                                foreach (HeterogeneousCategories _hccat in hccat)
                                                {
                                                    _hccat.Active = false;

                                                    db.SaveChanges();

                                                    ActivityLog._updateHeterogeneousCategories(_hccat.HeterogeneousCategoryId, _hccat.ParentId, _hccat.Description, _hccat.Level, ApplicationId, UsersId);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var cphc = (from clientphc in db.ClientProductHeterogeneousCategories
                                where clientphc.HeterogeneousCategoryId == Id
                                select clientphc).ToList();
                    if (cphc.LongCount() > 0)
                    {
                        foreach (ClientProductHeterogeneousCategories _cphc in cphc)
                        {
                            ClientProductCategories = new Models.ClientProductCategories();

                            var clientpc = (from cpcat in db.ClientProductCategories
                                            where cpcat.CategoryId == categoryid
                                            && cpcat.ClientId == _cphc.ClientId
                                            && cpcat.ProductId == _cphc.ProductId
                                            select cpcat).ToList();
                            if (clientpc.LongCount() == 0)
                            {
                                ClientProductCategories.CategoryId = categoryid;
                                ClientProductCategories.ClientId = _cphc.ClientId;
                                ClientProductCategories.ProductId = _cphc.ProductId;

                                db.ClientProductCategories.Add(ClientProductCategories);
                                db.SaveChanges();

                                ActivityLog._insertClientProductCategories(categoryid, _cphc.ClientId, _cphc.ProductId, ApplicationId, UsersId);
                            }
                        }
                    }
                    return RedirectToAction("categories", "Recategorization");
                }
                else
                {
                    return RedirectToAction("categories", "Recategorization");
                }
            }
        }

        public ActionResult addnewcategory()
        {
            var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + "" + "'").OrderBy(x => x.ParentId).ToList();
            return View(_response);
        }

        public JsonResult getlevel2(string lev1)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();
            int level = int.Parse(lev1);
            var level2 = (from l2 in db.Categories
                          where l2.ParentId == level
                          select l2).OrderBy(x => x.Description);

            foreach (Categories c in level2)
            {
                Categories = new Categories();

                Categories.Active = c.Active;
                Categories.CategoryId = c.CategoryId;
                Categories.Description = c.Description;
                Categories.Level = c.Level;
                Categories.ParentId = c.ParentId;

                lcat.Add(Categories);
            }

            return Json(lcat, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getlevel3(string lev2)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();
            int level = int.Parse(lev2);

            var level3 = (from l3 in db.Categories
                          where l3.ParentId == level
                          select l3).OrderBy(x => x.Description);

            foreach (Categories c in level3)
            {
                Categories = new Categories();

                Categories.Active = c.Active;
                Categories.CategoryId = c.CategoryId;
                Categories.Description = c.Description;
                Categories.Level = c.Level;
                Categories.ParentId = c.ParentId;

                lcat.Add(Categories);
            }

            return Json(lcat, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getlevel4(string lev3)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            int level = int.Parse(lev3);

            var level3 = (from l3 in db.Categories
                          where l3.ParentId == level
                          select l3).OrderBy(x => x.Description);

            foreach (Categories c in level3)
            {
                Categories = new Categories();

                Categories.Active = c.Active;
                Categories.CategoryId = c.CategoryId;
                Categories.Description = c.Description;
                Categories.Level = c.Level;
                Categories.ParentId = c.ParentId;

                lcat.Add(Categories);
            }

            return Json(lcat, JsonRequestBehavior.AllowGet);
        }

        public ActionResult insertcategory(string lev1, string lev2, string lev3, string lev4)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            int level1 = int.Parse(lev1);
            int level2 = int.Parse(lev2);
            int level3 = int.Parse(lev3);

            lev4 = lev4.ToUpper();
            lev4 = lev4.Trim();

            string level = lev4;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;
            int catid = int.Parse(lev3);

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var levl1 = (from l1 in db.Categories
                         where l1.ParentId == catid
                         select l1).ToList();
            foreach (Categories c in levl1)
            {
                Categories = new Categories();
                Categories.Active = c.Active;
                Categories.CategoryId = c.CategoryId;
                descriptiondatabase = c.Description;
                descriptiondatabase = classreplace.replacedatabase(descriptiondatabase);
                Categories.Description = descriptiondatabase;
                Categories.Level = c.Level;
                Categories.IdArbol = c.IdArbol;
                Categories.ParentId = c.ParentId;
                lcat.Add(Categories);
            }

            foreach (Categories c1 in lcat)
            {
                if (level.Equals(c1.Description.Trim()))
                {
                    _description = c1.Description.Trim();
                    categoryid = c1.CategoryId;
                }
            }

            var cat = (from cats in lcat
                       where cats.Description == _description
                       select cats).ToList();
            if (cat.LongCount() > 0)
            {
                var cats = (from catss in db.Categories
                            where catss.CategoryId == categoryid
                            select catss).ToList();
                foreach (Categories cc in cats)
                {
                    _description = cc.Description.Trim();
                }
                return Json(_description, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Categories = new Models.Categories();
                Categories.Active = true;
                Categories.Level = 4;
                Categories.Description = lev4.ToUpper();
                Categories.ParentId = level3;

                db.Categories.Add(Categories);
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult insertlevel1(string valuelevel, string valuelevel2, string valuelevel3, string valuelevel4)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            var _response = insertcategoriessincelevel1._insertlevel1(valuelevel, ApplicationId, UsersId);
            if (_response == "true")
            {
                var _responselev2 = insertcategoriessincelevel1._insertlevel2(valuelevel2, ApplicationId, UsersId);
                if (_responselev2 == "true")
                {
                    var _responselev3 = insertcategoriessincelevel1._insertlevel3(valuelevel3, ApplicationId, UsersId);
                    if (_responselev3 == "true")
                    {
                        var _responselev4 = insertcategoriessincelevel1._insertlevel4(valuelevel4, ApplicationId, UsersId);
                        if (_responselev4 == "true")
                        {
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(_responselev4, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(_responselev3, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(_responselev2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var jsonresult = new { Category = _response, Level = 1 };
                return Json(jsonresult, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult insertlevel2(string valuelevel2, string CategoryId, string valuelevel3, string valuelevel4)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int catid = int.Parse(CategoryId);
            var _responsel2 = insertcategoriessincelevel1._insertlevel2n2(valuelevel2, catid, ApplicationId, UsersId);
            if (_responsel2 == "true")
            {
                var _responsel3 = insertcategoriessincelevel1._insertlevel3n2(valuelevel3, catid, ApplicationId, UsersId);
                if (_responsel3 == "true")
                {
                    var _responsel4 = insertcategoriessincelevel1._insertlevel4n2(valuelevel4, catid, ApplicationId, UsersId);
                    if (_responsel4 == "true")
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(_responsel4, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(_responsel3, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(_responsel2, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult insertlevel3(string CategoryId, string valuelevel3, string valuelevel4)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int catid = int.Parse(CategoryId);

            var _responsel3 = insertcategoriessincelevel1._insertlevel3n3(valuelevel3, catid, ApplicationId, UsersId);
            if (_responsel3 == "true")
            {
                var _responsel4 = insertcategoriessincelevel1._insertlevel4n3(valuelevel4, catid, ApplicationId, UsersId);
                if (_responsel4 == "true")
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(_responsel4, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(_responsel3, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult insertlevel4(string CategoryId, string valuelevel4)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            int catid = int.Parse(CategoryId);

            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel4 = valuelevel4.ToUpper();

            string level = valuelevel4;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var level1 = (from l1 in db.Categories
                          where l1.ParentId == catid
                          select l1).ToList();
            foreach (Categories c in level1)
            {
                Categories = new Categories();
                Categories.Active = c.Active;
                Categories.CategoryId = c.CategoryId;
                descriptiondatabase = c.Description;
                descriptiondatabase = classreplace.replacedatabase(descriptiondatabase);
                Categories.Description = descriptiondatabase;
                Categories.Level = c.Level;
                Categories.IdArbol = c.IdArbol;
                Categories.ParentId = c.ParentId;
                lcat.Add(Categories);
            }

            foreach (Categories c1 in lcat)
            {
                if (level.Equals(c1.Description.Trim()))
                {
                    _description = c1.Description.Trim();
                    categoryid = c1.CategoryId;
                }
            }

            var cat = (from cats in lcat
                       where cats.Description == _description
                       select cats).ToList();
            if (cat.LongCount() > 0)
            {
                var cats = (from catss in db.Categories
                            where catss.CategoryId == categoryid
                            select catss).ToList();
                foreach (Categories cc in cats)
                {
                    _description = cc.Description.Trim();
                }
                return Json(_description, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == catid
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 3)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel4.Trim();
                        Categories.Level = 4;
                        Categories.ParentId = catid;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel4
                                     && _catts.ParentId == catid
                                     && _catts.Level == 4
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {

                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                categoryid = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(categoryid, parentid, valuelevel2, levell, ApplicationId, UsersId);
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult searchcategory(string Description)
        {

            searchcategoryreference searchcategoryreference = new searchcategoryreference(Description);
            Session["searchcategoryreference"] = searchcategoryreference;

            var _response = db.Database.SqlQuery<Categories>("plm_spGetCategoriestree @param='" + Description + "'").OrderBy(x => x.ParentId).ToList();

            return View("addnewcategory", _response);
        }

        public ActionResult getheterogeneouscat(string categid)
        {
            try
            {
                int Categoryid = int.Parse(categid);

                sessionclassif sessionclassif = new sessionclassif(Categoryid);
                Session["sessionclassif"] = sessionclassif;

                //return View("categories", JsonRequestBehavior.AllowGet);
                //return Redirect::to('user/login')->with('message', 'Login Failed');
                //return View("categories");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult deletematch(int? HeterogeneousCategoryId, int? HeterogeneousCategoryIdNodo)
        {
            CountriesUsers user = (CountriesUsers)Session["CountriesUsers"];
            int ApplicationId = user.ApplicationId;
            int UsersId = user.userId;

            if (HeterogeneousCategoryIdNodo != null)
            {
                sessionclassif sessionclassif = (sessionclassif)Session["sessionclassif"];

                int _CategoryId = sessionclassif.Categoryid;

                var roll = (from match in db.MatchCategories
                            join hc in db.ClientHeterogeneousCategories
                            on match.HeterogeneousCategoryId equals hc.HeterogeneousCategoryId
                            where match.CategoryId == _CategoryId
                            && match.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo
                            select new joinrollback { ClientHeterogeneousCategories = hc, MatchCategories = match }).ToList();
                if (roll.LongCount() > 0)
                {
                    foreach (joinrollback _roll in roll)
                    {

                        var ecc = (from eccat in db.EditionClientCategories
                                   where eccat.CategoryId == _roll.MatchCategories.CategoryId
                                   && eccat.ClientId == _roll.ClientHeterogeneousCategories.ClientId
                                   select eccat).ToList();
                        if (ecc.LongCount() > 0)
                        {
                            foreach (EditionClientCategories _ecc in ecc)
                            {
                                var delete = db.EditionClientCategories.SingleOrDefault(x => x.ClientId == _roll.ClientHeterogeneousCategories.ClientId && x.CategoryId == _roll.MatchCategories.CategoryId && x.EditionId == _ecc.EditionId);
                                db.EditionClientCategories.Remove(delete);
                                db.SaveChanges();

                                int Id = Convert.ToInt32(HeterogeneousCategoryIdNodo);
                                ActivityLog._deleteditionclientcategories(_ecc.ClientId, Id, _ecc.EditionId, ApplicationId, UsersId);
                            }

                        }
                        var cc = (from clientc in db.ClientCategories
                                  where clientc.CategoryId == _roll.MatchCategories.CategoryId
                                  && clientc.ClientId == _roll.ClientHeterogeneousCategories.ClientId
                                  select clientc).ToList();
                        if (cc.LongCount() > 0)
                        {
                            foreach (ClientCategories _cc in cc)
                            {
                                var delete = db.ClientCategories.SingleOrDefault(x => x.ClientId == _roll.ClientHeterogeneousCategories.ClientId && x.CategoryId == _roll.MatchCategories.CategoryId);
                                db.ClientCategories.Remove(delete);
                                db.SaveChanges();

                                int Id = Convert.ToInt32(HeterogeneousCategoryIdNodo);
                                ActivityLog._deleteclientcategories(Id, _cc.ClientId, ApplicationId, UsersId);
                            }
                        }
                    }
                }


                var mc = (from match in db.MatchCategories
                          where match.CategoryId == _CategoryId
                          && match.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo
                          select match).ToList();
                if (mc.LongCount() > 0)
                {
                    foreach (MatchCategories _mc in mc)
                    {
                        var delete = db.MatchCategories.SingleOrDefault(x => x.CategoryId == _CategoryId && x.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo);
                        db.MatchCategories.Remove(delete);
                        db.SaveChanges();

                        int Id = Convert.ToInt32(HeterogeneousCategoryIdNodo);
                        ActivityLog._deleteMatchCategories(_CategoryId, Id, ApplicationId, UsersId);
                    }

                    var htc = (from hetcat in db.HeterogeneousCategories
                               where hetcat.HeterogeneousCategoryId == HeterogeneousCategoryIdNodo
                               select hetcat).ToList();
                    if (htc.LongCount() > 0)
                    {
                        foreach (HeterogeneousCategories _htc in htc)
                        {
                            _htc.Active = true;

                            db.SaveChanges();

                            ActivityLog._updateHeterogeneousCategoriesactive(_htc.HeterogeneousCategoryId, _htc.ParentId, _htc.Description, _htc.Level, ApplicationId, UsersId);

                            var htc1 = (from hetcat1 in db.HeterogeneousCategories
                                        where hetcat1.HeterogeneousCategoryId == _htc.ParentId
                                        select hetcat1).ToList();
                            if (htc1.LongCount() > 0)
                            {
                                foreach (HeterogeneousCategories _htc1 in htc1)
                                {
                                    _htc1.Active = true;

                                    db.SaveChanges();

                                    ActivityLog._updateHeterogeneousCategoriesactive(_htc1.HeterogeneousCategoryId, _htc1.ParentId, _htc1.Description, _htc1.Level, ApplicationId, UsersId);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                sessionclassif sessionclassif = (sessionclassif)Session["sessionclassif"];
                int _CategoryId = sessionclassif.Categoryid;

                var roll = (from match in db.MatchCategories
                            join hc in db.ClientHeterogeneousCategories
                            on match.HeterogeneousCategoryId equals hc.HeterogeneousCategoryId
                            where match.CategoryId == _CategoryId
                            && match.HeterogeneousCategoryId == HeterogeneousCategoryId
                            select new joinrollback { ClientHeterogeneousCategories = hc, MatchCategories = match }).ToList();
                if (roll.LongCount() > 0)
                {
                    foreach (joinrollback _roll in roll)
                    {

                        var ecc = (from eccat in db.EditionClientCategories
                                   where eccat.CategoryId == _roll.MatchCategories.CategoryId
                                   && eccat.ClientId == _roll.ClientHeterogeneousCategories.ClientId
                                   select eccat).ToList();
                        if (ecc.LongCount() > 0)
                        {
                            foreach (EditionClientCategories _ecc in ecc)
                            {
                                var delete = db.EditionClientCategories.SingleOrDefault(x => x.ClientId == _roll.ClientHeterogeneousCategories.ClientId && x.CategoryId == _roll.MatchCategories.CategoryId);
                                db.EditionClientCategories.Remove(delete);
                                db.SaveChanges();

                                int Id = Convert.ToInt32(HeterogeneousCategoryId);
                                ActivityLog._deleteditionclientcategories(_ecc.ClientId, Id, _ecc.EditionId, ApplicationId, UsersId);
                            }

                        }
                        var cc = (from clientc in db.ClientCategories
                                  where clientc.CategoryId == _roll.MatchCategories.CategoryId
                                  && clientc.ClientId == _roll.ClientHeterogeneousCategories.ClientId
                                  select clientc).ToList();
                        if (cc.LongCount() > 0)
                        {
                            foreach (ClientCategories _cc in cc)
                            {
                                var delete = db.ClientCategories.SingleOrDefault(x => x.ClientId == _roll.ClientHeterogeneousCategories.ClientId && x.CategoryId == _roll.MatchCategories.CategoryId);
                                db.ClientCategories.Remove(delete);
                                db.SaveChanges();

                                int Id = Convert.ToInt32(HeterogeneousCategoryId);
                                ActivityLog._deleteclientcategories(Id, _cc.ClientId, ApplicationId, UsersId);
                            }
                        }
                    }
                }


                var mc = (from match in db.MatchCategories
                          where match.CategoryId == _CategoryId
                          && match.HeterogeneousCategoryId == HeterogeneousCategoryId
                          select match).ToList();
                if (mc.LongCount() > 0)
                {
                    foreach (MatchCategories _mc in mc)
                    {
                        var delete = db.MatchCategories.SingleOrDefault(x => x.CategoryId == _CategoryId && x.HeterogeneousCategoryId == HeterogeneousCategoryId);
                        db.MatchCategories.Remove(delete);
                        db.SaveChanges();

                        int Id = Convert.ToInt32(HeterogeneousCategoryId);
                        ActivityLog._deleteMatchCategories(_CategoryId, Id, ApplicationId, UsersId);
                    }

                    var htc = (from hetcat in db.HeterogeneousCategories
                               where hetcat.HeterogeneousCategoryId == HeterogeneousCategoryId
                               select hetcat).ToList();
                    if (htc.LongCount() > 0)
                    {
                        foreach (HeterogeneousCategories _htc in htc)
                        {
                            _htc.Active = true;

                            db.SaveChanges();

                            ActivityLog._updateHeterogeneousCategoriesactive(_htc.HeterogeneousCategoryId, _htc.ParentId, _htc.Description, _htc.Level, ApplicationId, UsersId);

                            var htc1 = (from hetcat1 in db.HeterogeneousCategories
                                        where hetcat1.HeterogeneousCategoryId == _htc.ParentId
                                        select hetcat1).ToList();
                            if (htc1.LongCount() > 0)
                            {
                                foreach (HeterogeneousCategories _htc1 in htc1)
                                {
                                    _htc1.Active = true;

                                    db.SaveChanges();

                                    ActivityLog._updateHeterogeneousCategoriesactive(_htc1.HeterogeneousCategoryId, _htc1.ParentId, _htc1.Description, _htc1.Level, ApplicationId, UsersId);
                                }
                            }
                        }
                    }
                }
            }

            return RedirectToAction("categories", "Recategorization");
        }

        public ActionResult getlevelbyshowform(string Catid)
        {
            int Categoryid = int.Parse(Catid);

            var cats = (from cat in db.Categories
                        where cat.CategoryId == Categoryid
                        select cat).ToList();
            if (cats.LongCount() > 0)
            {
                int level = 0;
                foreach (Categories _cats in cats)
                {
                    level = _cats.Level;
                }
                return Json(level, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult getparentbycategoryrecat(string categid)
        {
            int CategoryId = int.Parse(categid);

            int Categoryparent1 = 0;
            int Categoryparent2 = 0;
            int Categoryparent3 = 0;
            var cats1 = (from cat1 in db.Categories
                         where cat1.CategoryId == CategoryId
                         select cat1).ToList();
            foreach (Categories _cats1 in cats1)
            {
                var cats2 = (from cat2 in db.Categories
                             where cat2.CategoryId == _cats1.ParentId
                             select cat2).ToList();
                foreach (Categories _cats2 in cats2)
                {
                    Categoryparent1 = _cats2.CategoryId;

                    var cats3 = (from cat3 in db.Categories
                                 where cat3.CategoryId == _cats2.ParentId
                                 select cat3).ToList();
                    foreach (Categories _cats3 in cats3)
                    {
                        Categoryparent2 = _cats3.CategoryId;

                        var cats4 = (from cat4 in db.Categories
                                     where cat4.CategoryId == _cats3.ParentId
                                     select cat4).ToList();
                        foreach (Categories _cats4 in cats4)
                        {
                            Categoryparent3 = _cats4.CategoryId;
                        }
                    }
                }
            }
            var _response = new { level1 = Categoryparent3, level2 = Categoryparent2, level3 = Categoryparent1 };
            return Json(_response, JsonRequestBehavior.AllowGet);
        }
    }
}