using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;


namespace GuiaNet.Models
{
    public class insertcategoriessincelevel1
    {
        ActivityLog ActivityLog = new ActivityLog();

        Guia db = new Guia();

        public int Categoryid = 0;
        public int CategoryidSL2 = 0;
        public int CategoryidSL3 = 0;

        public string _insertlevel1(string valuelevel, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel = valuelevel.ToUpper();

            string level = valuelevel;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var level1 = (from l1 in db.Categories
                          where l1.ParentId == null
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
                return _description;
            }
            else
            {
                Categories = new Models.Categories();

                Categories.Active = true;
                Categories.Description = valuelevel.Trim();
                Categories.Level = 1;

                db.Categories.Add(Categories);
                db.SaveChanges();

                var catss = (from _catts in db.Categories
                             where _catts.Description == valuelevel
                             select _catts).ToList();
                if (catss.LongCount() > 0)
                {
                    int levell = 0;
                    foreach (Categories _catss in catss)
                    {
                        Categoryid = _catss.CategoryId;
                        levell = _catss.Level;
                    }
                    //ActivityLog._insertcategorylevel1(Categoryid, valuelevel, levell, ApplicationId, UsersId);
                    string _results = "true";
                    return _results;
                }
                else
                {
                    string _results = "error";
                    return _results;
                }

            }
        }

        public string _insertlevel2(string valuelevel2, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel2 = valuelevel2.ToUpper();

            string level = valuelevel2;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var level2 = (from l1 in db.Categories
                          where l1.ParentId == Categoryid
                          select l1).ToList();
            foreach (Categories c in level2)
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

            foreach (Categories c2 in lcat)
            {
                if (level.Equals(c2.Description.Trim()))
                {
                    _description = c2.Description.Trim();
                    categoryid = c2.CategoryId;
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
                return _description;
            }
            else
            {
                Categories = new Models.Categories();
                Categories.Active = true;
                Categories.Description = valuelevel2.Trim();
                Categories.Level = 2;
                Categories.ParentId = Categoryid;

                db.Categories.Add(Categories);
                db.SaveChanges();

                var catss = (from _catts in db.Categories
                             where _catts.Description == valuelevel2
                             && _catts.ParentId == Categoryid
                             && _catts.Level == 2
                             select _catts).ToList();
                if (catss.LongCount() > 0)
                {
                    int levell = 0;
                    int? parentid = 0;
                    foreach (Categories _catss in catss)
                    {
                        Categoryid = _catss.CategoryId;
                        levell = _catss.Level;
                        parentid = _catss.ParentId;
                    }
                    //ActivityLog._insertcategorylevel(Categoryid, parentid, valuelevel2, levell, ApplicationId, UsersId);
                    string _result = "true";
                    return _result;
                }
                else
                {
                    string _results = "error";
                    return _results;
                }

            }
        }

        public string _insertlevel3(string valuelevel3, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel3 = valuelevel3.ToUpper();

            string level = valuelevel3;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var level2 = (from l1 in db.Categories
                          where l1.ParentId == Categoryid
                          select l1).ToList();
            foreach (Categories c in level2)
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

            foreach (Categories c2 in lcat)
            {
                if (level.Equals(c2.Description.Trim()))
                {
                    _description = c2.Description.Trim();
                    categoryid = c2.CategoryId;
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
                return _description;
            }
            else
            {
                Categories = new Models.Categories();
                Categories.Active = true;
                Categories.Description = valuelevel3.Trim();
                Categories.Level = 3;
                Categories.ParentId = Categoryid;

                db.Categories.Add(Categories);
                db.SaveChanges();

                var catss = (from _catts in db.Categories
                             where _catts.Description == valuelevel3
                             && _catts.ParentId == Categoryid
                             && _catts.Level == 3
                             select _catts).ToList();
                if (catss.LongCount() > 0)
                {
                    int levell = 0;
                    int? parentid = 0;
                    foreach (Categories _catss in catss)
                    {
                        Categoryid = _catss.CategoryId;
                        levell = _catss.Level;
                        parentid = _catss.ParentId;
                    }
                    //ActivityLog._insertcategorylevel(Categoryid, parentid, valuelevel2, levell, ApplicationId, UsersId);
                    string _result = "true";
                    return _result;
                }
                else
                {
                    string _result = "error";
                    return _result;
                }
            }
        }

        public string _insertlevel4(string valuelevel4, int ApplicationId, int UsersId)
        {
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

            var level2 = (from l1 in db.Categories
                          where l1.ParentId == Categoryid
                          select l1).ToList();
            foreach (Categories c in level2)
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

            foreach (Categories c2 in lcat)
            {
                if (level.Equals(c2.Description.Trim()))
                {
                    _description = c2.Description.Trim();
                    categoryid = c2.CategoryId;
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
                return _description;
            }
            else
            {
                Categories = new Models.Categories();
                Categories.Active = true;
                Categories.Description = valuelevel4.Trim();
                Categories.Level = 4;
                Categories.ParentId = Categoryid;

                db.Categories.Add(Categories);
                db.SaveChanges();

                var catss = (from _catts in db.Categories
                             where _catts.Description == valuelevel4
                             && _catts.ParentId == Categoryid
                             && _catts.Level == 4
                             select _catts).ToList();
                if (catss.LongCount() > 0)
                {
                    int levell = 0;
                    int? parentid = 0;
                    foreach (Categories _catss in catss)
                    {
                        Categoryid = _catss.CategoryId;
                        levell = _catss.Level;
                        parentid = _catss.ParentId;
                    }
                    //ActivityLog._insertcategorylevel(Categoryid, parentid, valuelevel2, levell, ApplicationId, UsersId);
                    string _result = "true";
                    return _result;
                }
                else
                {
                    string _result = "error";
                    return _result;
                }
            }
        }

        public string _insertlevel2n2(string valuelevel2, int catid, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel2 = valuelevel2.ToUpper();

            string level = valuelevel2;
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
                return _description;
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == catid
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 1)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel2.Trim();
                        Categories.Level = 2;
                        Categories.ParentId = catid;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel2
                                     && _catts.ParentId == catid
                                     && _catts.Level == 2
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {

                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                CategoryidSL2 = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(CategoryidSL2, parentid, valuelevel2, levell, ApplicationId, UsersId);
                            string _results = "true";
                            return _results;
                        }
                    }
                }
            }
            return "error";
        }

        public string _insertlevel3n2(string valuelevel3, int catid, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel3 = valuelevel3.ToUpper();

            string level = valuelevel3;
            string descriptiondatabase = string.Empty;
            string _description = string.Empty;
            int categoryid = 0;

            level = level.ToUpper();

            level = classreplace.replacelevel(level);

            level = level.Trim();

            var level1 = (from l1 in db.Categories
                          where l1.ParentId == CategoryidSL2
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
                return _description;
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == CategoryidSL2
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 2)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel3.Trim();
                        Categories.Level = 3;
                        Categories.ParentId = CategoryidSL2;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel3
                                     && _catts.ParentId == CategoryidSL2
                                     && _catts.Level == 3
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {
                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                CategoryidSL2 = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(CategoryidSL2, parentid, valuelevel3, levell, ApplicationId, UsersId);
                            string _results = "true";
                            return _results;
                        }
                        else
                        {
                            string _results = "error";
                            return _results;
                        }

                    }
                }
            }
            return "error";
        }

        public string _insertlevel4n2(string valuelevel4, int catid, int ApplicationId, int UsersId)
        {
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
                          where l1.ParentId == CategoryidSL2
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
                return _description;
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == CategoryidSL2
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 3)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel4.Trim();
                        Categories.Level = 4;
                        Categories.ParentId = CategoryidSL2;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel4
                                     && _catts.ParentId == CategoryidSL2
                                     && _catts.Level == 4
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {
                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                CategoryidSL2 = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(CategoryidSL2, parentid, valuelevel4, levell, ApplicationId, UsersId);
                            string _results = "true";
                            return _results;
                        }
                    }
                }
            }
            return "error";
        }

        public string _insertlevel3n3(string valuelevel3, int catid, int ApplicationId, int UsersId)
        {
            Categories Categories = new Models.Categories();
            List<Categories> lcat = new List<Models.Categories>();

            valuelevel3 = valuelevel3.ToUpper();

            string level = valuelevel3;
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
                return _description;
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == catid
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 2)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel3.Trim();
                        Categories.Level = 3;
                        Categories.ParentId = catid;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel3
                                     && _catts.ParentId == catid
                                     && _catts.Level == 3
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {

                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                CategoryidSL3 = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(CategoryidSL3, parentid, valuelevel2, levell, ApplicationId, UsersId);
                            string _results = "true";
                            return _results;
                        }
                    }
                }
            }
            return "error";
        }

        public string _insertlevel4n3(string valuelevel4, int catid, int ApplicationId, int UsersId)
        {
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
                          where l1.ParentId == CategoryidSL3
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
                return _description;
            }
            else
            {
                var cattss = (from catl in db.Categories
                              where catl.CategoryId == CategoryidSL3
                              select catl).ToList();
                foreach (Categories _cattss in cattss)
                {
                    if (_cattss.Level == 3)
                    {
                        Categories = new Models.Categories();
                        Categories.Active = true;
                        Categories.Description = valuelevel4.Trim();
                        Categories.Level = 4;
                        Categories.ParentId = CategoryidSL3;

                        db.Categories.Add(Categories);
                        db.SaveChanges();

                        var catss = (from _catts in db.Categories
                                     where _catts.Description == valuelevel4
                                     && _catts.ParentId == CategoryidSL3
                                     && _catts.Level == 4
                                     select _catts).ToList();
                        if (catss.LongCount() > 0)
                        {

                            int levell = 0;
                            int? parentid = 0;
                            foreach (Categories _catss in catss)
                            {
                                CategoryidSL3 = _catss.CategoryId;
                                levell = _catss.Level;
                                parentid = _catss.ParentId;
                            }
                            //ActivityLog._insertcategorylevel(CategoryidSL3, parentid, valuelevel2, levell, ApplicationId, UsersId);
                            string _results = "true";
                            return _results;
                        }
                    }
                }
            }
            return "error";
        }
    }
}