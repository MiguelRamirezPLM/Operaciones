using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers.Módulo_Producción
{
    public class ProducciónController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();
        private Companies compan = new Companies();
        private Products products = new Products();
        private CompanySections csections = new CompanySections();
        private Sections section = new Sections();
        private CompanySections cs = new CompanySections();
        private ProductIndexes producti = new ProductIndexes();

        public ActionResult Clientes(int id, int ed)
        {
            var x = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {Companies = c, CompanyTypes = ct});
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            return View(x);
        }
        public ActionResult DetalleCliente(int id, int ed)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var z = (from c in db.Companies
                     join cs in db.CompanySections
                     on c.CompanyId equals cs.CompanyId
                     join s in db.Sections
                     on cs.SectionId equals s.SectionId
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs, Sections = s });
            var o = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = o.FirstOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = o.FirstOrDefault().Companies.CompanyId;
            ViewData["CompanyShortName"] = o.FirstOrDefault().Companies.CompanyShortName;
            ViewData["Address"] = o.FirstOrDefault().Companies.Address;
            ViewData["Suburb"] = o.FirstOrDefault().Companies.Suburb;
            ViewData["Ubication"] = o.FirstOrDefault().Companies.Ubication;
            ViewData["PostalCode"] = o.FirstOrDefault().Companies.PostalCode;
            ViewData["Email"] = o.FirstOrDefault().Companies.Email;
            ViewData["Web"] = o.FirstOrDefault().Companies.Web;
            ViewData["Contact"] = o.FirstOrDefault().Companies.Contact;
            var l = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyTypes = ct });
             ViewData["CompanyTypes.Description"] = l.FirstOrDefault().CompanyTypes.Description;
            return View(z);
        }
        public ActionResult Secciones(int id, int ed)
        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = cc.SingleOrDefault().Companies.CompanyId;
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var z = (from c in db.Companies
                     join cs in db.CompanySections
                     on c.CompanyId equals cs.CompanyId
                     join s in db.Sections
                     on cs.SectionId equals s.SectionId
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs, Sections = s });
            return View(z);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Secciones(int id, int ed, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections csections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            if (ModelState.IsValid)
            {
                db.Entry(csections).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("/Secciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + csections.CompanyId);
            }
            return View();
        }
        public ActionResult AsociarMarca(int id, int ed, string palabra)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var w = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     join cb in db.CompanyBrands
                     on c.CompanyId equals cb.CompanyId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     // asociar CompayBrandsIndexes
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyTypes = ct, CompanyBrands = cb, Brands = b });
            var f = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = f.FirstOrDefault().Companies.CompanyName;
            var p = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyId"] = f.FirstOrDefault().Companies.CompanyId;
           var cc = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c});
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            if (!String.IsNullOrEmpty(palabra))
            {
                w = w.Where(c => c.Brands.BrandName.Contains(palabra));
            }
            return View(w);
        }
        public ActionResult InformaciónMarca(int id, int ed)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var m = (from c in db.Companies
                     join cbs in db.CompanyBrandSections
                     on c.CompanyId equals cbs.CompanyId
                     join s in db.Sections
                     on cbs.SectionId equals s.SectionId
                     join b in db.Brands
                     on cbs.BrandId equals b.BrandId
                     join cbi in db.CompanyBrandIndexes
                     on c.CompanyId equals cbi.CompanyId
                     where cbi.BrandId == b.BrandId
                     join ind in db.Indexes
                     on cbi.IndexId equals ind.IndexId
                     where b.BrandId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     { Companies = c, CompanyBrandSections = cbs, Sections = s, Brands = b, CompanyBrandIndexes = cbi, Indexes = ind });
            var w = (from c in db.Companies
                     join cb in db.CompanyBrands
                     on c.CompanyId equals cb.CompanyId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     where b.BrandId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyBrands = cb, Brands = b });
            ViewData["CompanyName"] = w.FirstOrDefault().Companies.CompanyName;
            ViewData["C"] = w.FirstOrDefault().Companies.CompanyId;
            ViewData["I"] = w.FirstOrDefault().Brands.BrandId;
            ViewData["B"] = w.FirstOrDefault().Brands.BrandName;
            ViewData["Section"] = m.FirstOrDefault().Sections.Description;
            ViewData["Index"] = m.FirstOrDefault().Indexes.Description;
            ViewData["SectionId"] = m.FirstOrDefault().Sections.SectionId;
            ViewData["IndexId"] = m.FirstOrDefault().Indexes.IndexId;

            return View(m);
        }
                                          //EditionId  cliente  indice  marca
        public ActionResult AsociarMarcaIndice(int id, int ed, int ad, int ud)
        {
            var ee = (from e in db.Editions
                          where e.EditionId == id
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities{Editions = e});
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var w = (from c in db.Companies
                     join cb in db.CompanyBrandIndexes
                     on c.CompanyId equals cb.CompanyId
                     join ind in db.Indexes
                     on cb.IndexId equals ind.IndexId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     where b.BrandId == ud
                     && c.CompanyId == ed
                     && ind.IndexId == ad
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyBrandIndexes = cb, Indexes = ind, Brands = b });
            var ff = (from b in db.Brands
                      where b.BrandId == ud
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            ViewData["P"] = ff.FirstOrDefault().Brands.BrandId;
            ViewData["B"] = ff.SingleOrDefault().Brands.BrandName;
            ViewBag.IndexId = new SelectList(db.Indexes, "IndexId", "Description", ad);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", ud);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarMarcaIndice(int id, int ed, [Bind(Include = "IndexId,CompanyId,BrandId")] CompanyBrandIndexes companybrandindexes)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanyBrandIndexes
                            where cs.CompanyId == companybrandindexes.CompanyId
                            && cs.IndexId == companybrandindexes.IndexId
                            && cs.BrandId == companybrandindexes.BrandId
                            select cs).ToList();
                foreach (CompanyBrandIndexes css in pppp)
                {
                    var eliminar = db.CompanyBrandIndexes.SingleOrDefault
                        (x => x.CompanyId == companybrandindexes.CompanyId && x.BrandId == companybrandindexes.BrandId && x.IndexId == companybrandindexes.IndexId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanyBrandIndexes
                                where cs.CompanyId == companybrandindexes.CompanyId
                                && cs.IndexId == companybrandindexes.IndexId
                                && cs.BrandId == companybrandindexes.BrandId
                                select cs).ToList();
                    foreach (CompanyBrandIndexes css in rrrr)
                    {
                        var eliminar = db.CompanyBrandIndexes.SingleOrDefault
                         (x => x.CompanyId == companybrandindexes.CompanyId && x.BrandId == companybrandindexes.BrandId && x.IndexId == companybrandindexes.IndexId);
                        db.CompanyBrandIndexes.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(companybrandindexes).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(companybrandindexes).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/InformaciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrandindexes.BrandId);
            }
            return View(companybrandindexes);
        }
                                           //EditionId  cliente  sección  marca
        public ActionResult AsociarMarcaSección(int id, int ed, int ad, int ud)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var a = (from c in db.Companies
                     join cb in db.CompanyBrandSections
                     on c.CompanyId equals cb.CompanyId
                     join s in db.Sections
                     on cb.SectionId equals s.SectionId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     where b.BrandId == ud
                     && c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyBrandSections = cb, Brands = b, Sections = s });
            var ff = (from b in db.Brands
                      where b.BrandId == ud
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            ViewData["P"] = ff.FirstOrDefault().Brands.BrandId;
            ViewData["B"] = ff.SingleOrDefault().Brands.BrandName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description", ad);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", ud);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarMarcaSección(int id, int ed, int ad, int ud, [Bind(Include= "SectionId,CompanyId,BrandId")]CompanyBrandSections companybrandsections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanyBrandSections
                            where cs.CompanyId == companybrandsections.CompanyId
                            && cs.BrandId == companybrandsections.BrandId
                            && cs.SectionId == companybrandsections.SectionId
                            select cs).ToList();
                foreach (CompanyBrandSections css in pppp)
                {
                    var eliminar = db.CompanyBrandSections.SingleOrDefault
                        (x => x.CompanyId == companybrandsections.CompanyId && x.BrandId == companybrandsections.BrandId && x.SectionId == companybrandsections.SectionId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanyBrandSections
                                where cs.CompanyId == companybrandsections.CompanyId
                                && cs.BrandId == companybrandsections.BrandId
                                && cs.SectionId == companybrandsections.SectionId
                                select cs).ToList();
                    foreach (CompanyBrandSections css in rrrr)
                    {
                        var eliminar = db.CompanyBrandSections.SingleOrDefault
                         (x => x.CompanyId == companybrandsections.CompanyId && x.BrandId == companybrandsections.BrandId && x.SectionId == companybrandsections.SectionId);
                        db.CompanyBrandSections.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(companybrandsections).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(companybrandsections).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/InformaciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrandsections.BrandId);
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", 
                                               companybrandsections.CompanyId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description",
                                               companybrandsections.SectionId);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName",
                                             companybrandsections.BrandId);
            return View(companybrandsections);
        }
        public ActionResult AsociarProductos(int id, int ed, string palabra)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
           var w = (from c in db.Companies
                    join p in db.Products
                    on c.CompanyId equals p.CompanyId
                    where c.CompanyId == ed
                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
          var cc = (from c in db.Companies
                    where c.CompanyId == ed
                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyId"] = cc.SingleOrDefault().Companies.CompanyId;
          var pp = (from c in db.Companies
                    where c.CompanyId == ed
                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = pp.SingleOrDefault().Companies.CompanyName;
          var ff = (from c in db.Companies
                    where c.CompanyId == ed
                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyShortName"] = ff.SingleOrDefault().Companies.CompanyShortName;
            if (!String.IsNullOrEmpty(palabra))
            {w = w.Where(j => j.Products.ProductName.Contains(palabra));}
            return View(w);
        }
        public ActionResult AsociarProductoIndex(int id, int ed, int ad)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pp = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyName"] = pp.SingleOrDefault().Companies.CompanyName;
            var ppp = (from c in db.Companies
                       join p in db.Products
                       on c.CompanyId equals p.CompanyId
                       where p.ProductId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyId"] = ppp.SingleOrDefault().Companies.CompanyId;
            ViewData["P"] = pp.SingleOrDefault().Products.ProductId;
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName", ad);
            ViewBag.IndexId = new SelectList(db.Indexes, "IndexId", "Description", ed);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Guarda los cambios
        public ActionResult AsociarProductoIndex(int id, int ed, int ad, [Bind(Include = "ProductId,IndexId")] ProductIndexes productindexes)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var borrar = db.ProductIndexes.SingleOrDefault(x => x.ProductId == ad && x.IndexId == ed);
                db.ProductIndexes.Remove(borrar);
                db.SaveChanges();
                db.Entry(productindexes).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("/InformaciónÍndiceProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + productindexes.ProductId);
            }
            ViewBag.Companyid = new SelectList(db.Products, "ProductId", "ProductName",
                                              productindexes.ProductId);
            ViewBag.SectionId = new SelectList(db.Indexes, "IndexId", "Description",
                                               productindexes.IndexId);
            return View(productindexes);
        }    

        //
        public ActionResult AsociarProductoÍndiceNuevo(int id, int ed)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pp = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyName"] = pp.SingleOrDefault().Companies.CompanyName;
            var ppp = (from c in db.Companies
                       join p in db.Products
                       on c.CompanyId equals p.CompanyId
                       where p.ProductId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyId"] = ppp.SingleOrDefault().Companies.CompanyId;
            ViewData["P"] = pp.SingleOrDefault().Products.ProductId;
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName", ed);
            ViewBag.IndexId = new SelectList(db.Indexes, "IndexId", "Description");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Guarda los cambios
        public ActionResult AsociarProductoÍndiceNuevo(int id, int ed, [Bind(Include = "ProductId,IndexId")] ProductIndexes productindexes)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.ProductIndexes.Add(productindexes);
                db.SaveChanges();
                return RedirectToAction("/InformaciónÍndiceProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + productindexes.ProductId);
            }
            ViewBag.Companyid = new SelectList(db.Products, "ProductId", "ProductName",
                                              productindexes.ProductId);
            ViewBag.SectionId = new SelectList(db.Indexes, "IndexId", "Description",
                                               productindexes.IndexId);
            return View(productindexes);
        }           
        //
                                 //  Edición Sección Producto
        public ActionResult AsociarProductoSección(int id, int ed, int ad)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pp = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyName"] = pp.SingleOrDefault().Companies.CompanyName;
            var ppp = (from c in db.Companies
                       join p in db.Products
                       on c.CompanyId equals p.CompanyId
                       where p.ProductId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyId"] = ppp.SingleOrDefault().Companies.CompanyId;
            ViewData["P"] = pp.SingleOrDefault().Products.ProductId;
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName", ad);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description", ed);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarProductoSección // Modifica los cambios
        (int id, int ed, int ad, [Bind(Include = "ProductId,SectionId")] ProductSections productsections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var borrar = db.ProductSections.SingleOrDefault(x => x.ProductId == ad && x.SectionId == ed);
                db.ProductSections.Remove(borrar);
                db.SaveChanges();
                db.Entry(productsections).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("/InformaciónSecciónProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + productsections.ProductId);
            }
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName",
                                               productsections.ProductId);
            ViewBag.IndexId = new SelectList(db.Sections, "SectionId", "Description",
                                             productsections.SectionId);
            return View(productsections);
        }
        public ActionResult AsociarProductoSecciónNueva(int id, int ed)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pp = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyName"] = pp.SingleOrDefault().Companies.CompanyName;
            var ppp = (from c in db.Companies
                       join p in db.Products
                       on c.CompanyId equals p.CompanyId
                       where p.ProductId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyId"] = ppp.SingleOrDefault().Companies.CompanyId;
            ViewData["P"] = pp.SingleOrDefault().Products.ProductId;
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName", ed);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarProductoSecciónNueva // Asociar producto que no este asociado a una sección
        (int id, int ed, [Bind(Include = "ProductId,SectionId")] ProductSections productsections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.ProductSections.Add(productsections);
                db.SaveChanges();
                return RedirectToAction("/InformaciónSecciónProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + productsections.ProductId);
            }
            ViewBag.Productid = new SelectList(db.Products, "ProductId", "ProductName",
                                               productsections.ProductId);
            ViewBag.IndexId = new SelectList(db.Sections, "SectionId", "Description",
                                             productsections.SectionId);
            return View(productsections);
        }
        public ActionResult InformaciónSecciónProducto(int id, int ed)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pp = (from p in db.Products
                      join ps in db.ProductSections
                      on p.ProductId equals ps.ProductId
                      join s in db.Sections
                      on ps.SectionId equals s.SectionId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p, ProductSections = ps, Sections = s });
            var pr = (from p in db.Products
                      join pt in db.ProductTypes
                          on p.ProductTypeId equals pt.ProductTypeId
                          join c in db.Companies
                          on p.CompanyId equals c.CompanyId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p, ProductTypes = pt, Companies = c });
            ViewData["ProductName"] = pr.SingleOrDefault().Products.ProductName;
            ViewData["Descripción"] = pr.SingleOrDefault().Products.Description;
            ViewData["Tipo"] = pr.SingleOrDefault().ProductTypes.Description;
            ViewData["CompanyName"] = pr.SingleOrDefault().Companies.CompanyName;
            ViewData["ProductId"] = pr.SingleOrDefault().Products.ProductId;
            ViewData["CompanyId"] = pr.SingleOrDefault().Companies.CompanyId;
            return View(pp);
        }
        public ActionResult InformaciónÍndiceProducto(int id, int ed)
        {
            var pp = (from p in db.Products
                      join pi in db.ProductIndexes
                          on p.ProductId equals pi.ProductId
                      join ind in db.Indexes
                          on pi.IndexId equals ind.IndexId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p, ProductIndexes = pi, Indexes = ind });
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var pr = (from p in db.Products
                      join c in db.Companies
                          on p.CompanyId equals c.CompanyId
                      join pt in db.ProductTypes
                          on p.ProductTypeId equals pt.ProductTypeId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p, ProductTypes = pt, Companies = c });
            ViewData["CompanyId"] = pr.SingleOrDefault().Companies.CompanyId;
            ViewData["CompanyName"] = pr.SingleOrDefault().Companies.CompanyName;
            ViewData["ProductName"] = pr.SingleOrDefault().Products.ProductName;
            ViewData["Descripción"] = pr.SingleOrDefault().Products.Description;
            ViewData["Tipo"] = pr.SingleOrDefault().ProductTypes.Description;
            ViewData["ProductId"] = pr.SingleOrDefault().Products.ProductId;
            return View(pp);
        }
        public ActionResult AsociarCliente(int id, int ed, int ad)
        {
            CompanySections csss = db.CompanySections.SingleOrDefault(model => model.CompanyId == ed);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description", ad);
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(csss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarCliente(int id, int ed, int ad, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections csections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanySections
                            where cs.CompanyId == csections.CompanyId
                            && cs.SectionId == csections.SectionId
                            && cs.Active == csections.Active
                            select cs).ToList();
                foreach (CompanySections css in pppp)
                {
                    var eliminar = db.CompanySections.SingleOrDefault(x => x.CompanyId == csections.CompanyId && x.SectionId == csections.SectionId && x.Active == csections.Active);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanySections
                                where cs.CompanyId == csections.CompanyId
                                && cs.SectionId == csections.SectionId
                                && cs.Active == csections.Active
                                select cs).ToList();
                    foreach (CompanySections css in rrrr)
                    {
                        var eliminar = db.CompanySections.SingleOrDefault(x => x.CompanyId == csections.CompanyId && x.SectionId == csections.SectionId && x.Active == csections.Active);
                        db.CompanySections.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(csections).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var eliminar = db.CompanySections.SingleOrDefault(x => x.CompanyId == csections.CompanyId && x.SectionId == ad);
                    db.CompanySections.Remove(eliminar);
                    db.SaveChanges();
                    db.Entry(csections).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/Secciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + csections.CompanyId);
            }
            ViewBag.Companyid = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                               csections.CompanyId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description",
                                               csections.SectionId);
            ViewBag.SectionIdParent = new SelectList(db.Sections, "SectionIdParent", "Description",
                                                     section.SectionIdParent);
            return View(csections);
        }
        public ActionResult AgregarSecciónCliente(int id, int ed)
        {
            Companies csss = db.Companies.SingleOrDefault(model => model.CompanyId == ed);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description");
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(csss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarSecciónCliente(int id, int ed, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections csections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(csections).State = EntityState.Added;
                    db.SaveChanges();
                
                return RedirectToAction("/Secciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + csections.CompanyId);
            }
            return View();
        }
        public ActionResult EliminarSecciónCliente(int id, int ed, int ad)
        {
            CompanySections csss = db.CompanySections.SingleOrDefault(model => model.CompanyId == ed && model.SectionId == ad);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var css = (from c in db.Companies
                       join cs in db.CompanySections
                           on c.CompanyId equals cs.CompanyId
                       join s in db.Sections
                           on cs.SectionId equals s.SectionId
                       where cs.CompanyId == ed && cs.SectionId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs, Sections = s });
            ViewData["SN"] = css.SingleOrDefault().Sections.Description;
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(csss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarSecciónCliente(int id, int ed, int ad, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections csections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Entry(csections).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("/Secciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + csections.CompanyId);
            }
            return View();
        }
    }
}