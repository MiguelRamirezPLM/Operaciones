using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class VentasController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();
        private Companies compan = new Companies();
        private Products products = new Products();
        private CompanySections csections = new CompanySections();
        private Sections section = new Sections();
        private CompanyBrands companyb = new CompanyBrands();

        public ActionResult Clientes(int id, int ed)
        {
            Union_Companies_CompanyTypes_CompanyPhones_Cities u = new Union_Companies_CompanyTypes_CompanyPhones_Cities();
            var x = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     join ce in db.CompanyEditions
                     on c.CompanyId equals ce.CompanyId
                     where c.CompanyId == ed && ce.EditionId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyTypes = ct, CompanyEditions = ce });
            var f = (from c in db.Countries
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Countries = c });
           var cc = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["w"] = f.FirstOrDefault().Countries.CountryName;
            var Editions = (from e in db.Editions
                            where e.EditionId == id
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = Editions.SingleOrDefault().Editions.NumberEdition;
            var Number = (from e in db.Editions
                          where e.EditionId == id
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = Number.SingleOrDefault().Editions.EditionId;
            return View(x);
        }
        public ActionResult Productos(int id, int ed, string palabra)
        {
            Union_Products u = new Union_Products();
            var w = (from p in db.Products
                     join c in db.Companies
                     on p.CompanyId equals c.CompanyId
                     join pt in db.ProductTypes
                     on p.ProductTypeId equals pt.ProductTypeId
                     where p.CompanyId == ed
                     select new Union_Products { prod = p, prodtypes = pt, companies = c });
            if (!String.IsNullOrEmpty(palabra))
            {
                w = w.Where(j => j.prod.ProductName.Contains(palabra));
            }
            var r = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Products { companies = c });
            ViewData["CompanyName"] = r.FirstOrDefault().companies.CompanyName;
            var h = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Products { companies = c });
            ViewData["CompanyId"] = h.FirstOrDefault().companies.CompanyId;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(w);
        }
        public ActionResult EdiciónProducto(int id, int ed)
        {
           var pee = (from p in db.Products
                      join pe in db.ProductEditions
                      on p.ProductId equals pe.ProductId
                      join edd in db.Editions
                      on pe.EditionId equals edd.EditionId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p, ProductEditions = pe, Editions = edd });
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var ccc = (from p in db.Products
                       where p.ProductId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p });
            var com = (from c in db.Companies
                       join p in db.Products
                       on c.CompanyId equals p.CompanyId
                       where p.ProductId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, Products = p });
            ViewData["CompanyName"] = com.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = com.SingleOrDefault().Companies.CompanyId;
            ViewData["ProductName"] = ccc.SingleOrDefault().Products.ProductName;
            ViewData["ProductId"] = ccc.SingleOrDefault().Products.ProductId;
            return View(pee);
        }
        public ActionResult EditarEdicionesParticipantesProducto(int id, int ed, int ad)
        {
            ProductEditions ceee = db.ProductEditions.SingleOrDefault(x => x.ProductId == ad && x.EditionId == ed);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      join p in db.Products 
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var ppp = (from p in db.Products
                       where p.ProductId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p });
            ViewData["Pname"] = ppp.SingleOrDefault().Products.ProductName;
            ViewData["PP"] = ppp.SingleOrDefault().Products.ProductId;
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", ad);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditarEdicionesParticipantesProducto(int id, int ed, int ad, [Bind(Include = "ProductId,EditionId,HtmlFile,HtmlContent")]ProductEditions producteditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.ProductEditions
                            where cs.ProductId == producteditions.ProductId && cs.EditionId == producteditions.EditionId
                            select cs).ToList();
                foreach (ProductEditions css in pppp)
                {
                    var eliminar = db.ProductEditions.SingleOrDefault
                        (x => x.ProductId == producteditions.ProductId && x.EditionId == producteditions.EditionId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.ProductEditions
                                where cs.ProductId == producteditions.ProductId
                                && cs.EditionId == producteditions.EditionId
                                select cs).ToList();
                    foreach (ProductEditions css in rrrr)
                    {
                        var eliminar = db.ProductEditions.SingleOrDefault
                         (x => x.ProductId == producteditions.ProductId && x.EditionId == ed);
                        db.ProductEditions.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(producteditions).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var eliminar = db.ProductEditions.SingleOrDefault
                    (x => x.ProductId == producteditions.ProductId && x.EditionId == ed);
                    db.ProductEditions.Remove(eliminar);
                    db.SaveChanges();
                    db.Entry(producteditions).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/EdiciónProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + producteditions.ProductId);
            }
            return View();
        }
        public ActionResult AgregarEdiciónProducto(int id, int ed)
        {
            Products productss = db.Products.SingleOrDefault(model => model.ProductId == ed);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var ppp = (from p in db.Products
                       where p.ProductId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p });
            ViewData["Pname"] = ppp.SingleOrDefault().Products.ProductName;
            ViewData["PP"] = ppp.SingleOrDefault().Products.ProductId;
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", ed);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition");
            return View(productss);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AgregarEdiciónProducto(int id, int ed, [Bind(Include = "ProductId,EditionId,HtmlFile,HtmlContent")]ProductEditions producteditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(producteditions).State = EntityState.Added;
                    db.SaveChanges();
                
                return RedirectToAction("/EdiciónProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + producteditions.ProductId);
            }
            return View();
        }
        public ActionResult EliminarEdiciónProducto(int id, int ed, int ad)
        {
            ProductEditions ceee = db.ProductEditions.SingleOrDefault(x => x.ProductId == ad && x.EditionId == ed);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      join p in db.Products
                      on c.CompanyId equals p.CompanyId
                      where p.ProductId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var ppp = (from p in db.Products
                       where p.ProductId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = p });
            ViewData["Pname"] = ppp.SingleOrDefault().Products.ProductName;
            ViewData["PP"] = ppp.SingleOrDefault().Products.ProductId;
            var pee = (from pe in db.ProductEditions
                       join edd in db.Editions
                       on pe.EditionId equals edd.EditionId
                       where pe.ProductId == ad
                       && pe.EditionId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { ProductEditions = pe, Editions = edd });
            ViewData["EDd"] = pee.SingleOrDefault().Editions.NumberEdition;
            ViewData["HTFile"] = pee.SingleOrDefault().ProductEditions.HtmlFile;
            @ViewData["HTContent"] = pee.SingleOrDefault().ProductEditions.HtmlContent;
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EliminarEdiciónProducto(int id, int ed, int ad, [Bind(Include = "ProductId,EditionId,HtmlFile,HtmlContent")]ProductEditions producteditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(producteditions).State = EntityState.Deleted;
                    db.SaveChanges();
                
                return RedirectToAction("/EdiciónProducto/" + ee.SingleOrDefault().Editions.EditionId + "/" + producteditions.ProductId);
            }
            return View();
        }
        public ActionResult EditarProductos(int id, int ed) // Vista: Editar productos
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            Products products = db.Products.Find(ed);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                               products.CompanyId);
            ViewBag.ProductIdParent = new SelectList(db.Products, "ProductId", "ProductName",
                                               products.ProductIdParent);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Description",
                                               products.ProductTypeId);
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProductos // Guarda los cambios
        (int id, int? ed, [Bind(Include = "ProductId,ProductIdParent,ProductTypeId,ProductName,Description,CompanyId,ProdId,Active")] Products products)
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/DetalleProducto/" + ff.SingleOrDefault().Editions.EditionId + "/" + products.ProductId);
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                               products.CompanyId);
            ViewBag.ProductIdParent = new SelectList(db.Products, "ProductId", "ProductName",
                                               products.ProductIdParent);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Description",
                                               products.ProductTypeId);
            return View(products);
        }
        public ActionResult AgregarProducto(int id, int ed) // Vista: Agregar productos
        {
            var cc =  (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.ProductIdParent = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Description");
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult AgregarProducto(int id, int ed, [Bind(Include = "ProductId,ProductIdParent,ProductTypeId,ProductName,Description,CompanyId,Active")] Products products)
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;

                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("/AgregarEdiciónProducto/" + ff.SingleOrDefault().Editions.EditionId + "/" + products.ProductId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                              products.CompanyId);
            ViewBag.ProductIdParent = new SelectList(db.Products, "ProductId", "ProductName",
                                              products.ProductIdParent);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Description",
                                              products.ProductTypeId);
            return View(products);
        }
        public ActionResult DetalleProducto(int id, int ed) // Vista: Detalle del producto
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            Products products = db.Products.Find(ed);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                               products.CompanyId);
            ViewBag.ProductIdParent = new SelectList(db.Products, "ProductId", "ProductName",
                                               products.ProductIdParent);
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "ProductTypeId", "Description",
                                               products.ProductTypeId);
            ViewBag.ProductEditions = new SelectList(db.ProductEditions, "ProductId", "EditionId",
                                               products.ProductId);
            return View(products);
        }
        public ActionResult Marcas(int id, int ed, string palabra)
        {
            Union_companies_cb m = new Union_companies_cb();
            var a = (from c in db.Companies
                     join cb in db.CompanyBrands
                     on c.CompanyId equals cb.CompanyId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     join ce in db.CompanyEditions
                     on c.CompanyId equals ce.CompanyId
                     where cb.CompanyId == ed
                     && ce.EditionId == id
                     select new Union_companies_cb { Companies = c, CompanyBrands = cb, Brands = b });
            var Number = (from e in db.Editions
                          where e.EditionId == id
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = Number.SingleOrDefault().Editions.EditionId;
            var edition = (from e in db.Editions
                           where e.EditionId == id
                           select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = edition.Single().Editions.NumberEdition;
            var w = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_companies_cb { Companies = c });
            ViewData["c"] = w.FirstOrDefault().Companies.CompanyName;
            var i = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_companies_cb { Companies = c });
            ViewData["id"] = i.FirstOrDefault().Companies.CompanyId;
           var cc = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyShortName"] = cc.SingleOrDefault().Companies.CompanyShortName;
            if (!String.IsNullOrEmpty(palabra))
            {
                a = a.Where(j => j.Brands.BrandName.Contains(palabra));
            }
            return View(a);
        }
        public ActionResult AgregarMarca(int id, int ed) // Vista: Agregar productos
        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
           var ccc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarMarca(int id, int ed, [Bind(Include = "BrandId,BrandName,Active")] Brands brands)

        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });

            if (ModelState.IsValid)
            {
                db.Brands.Add(brands);
                db.SaveChanges();
                return RedirectToAction("/AsociarMarcaCliente/" + ee.SingleOrDefault().Editions.EditionId + "/" + cc.SingleOrDefault().Companies.CompanyId + "/" + brands.BrandId);
            }
            return View(brands);
        }
        public ActionResult AsociarMarcaCliente(int id, int ed, int ad)
        {
            Brands ceee = db.Brands.SingleOrDefault(x => x.BrandId == ad);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c});
            var rr = (from b in db.Brands
                      where b.BrandId == ad
                      select b);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["BrandName"] = rr.SingleOrDefault().BrandName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarMarcaCliente(int id, int ed, [Bind(Include = "CompanyId,BrandId,Description")] CompanyBrands companybrands)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            if (ModelState.IsValid)
            {
                db.CompanyBrands.Add(companybrands);
                db.SaveChanges();
                return RedirectToAction("/AsociarMarcaSección/" +ee.SingleOrDefault().Editions.EditionId + "/" + cc.SingleOrDefault().Companies.CompanyId + "/" + companybrands.BrandId);
            }
            return View(companybrands);
        }
        public ActionResult AsociarMarcaSección(int id, int ed, int ad)
        {
            CompanyBrands cpb = db.CompanyBrands.SingleOrDefault(x => x.CompanyId == ed && x.BrandId == ad);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var rr = (from b in db.Brands
                      where b.BrandId == ad
                      select b);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["BrandName"] = rr.SingleOrDefault().BrandName;
            ViewBag.sectionId = new SelectList(db.Sections, "SectionId", "Description");
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(cpb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarMarcaSección(int id, int ed, [Bind(Include = "SectionId,CompanyId,BrandId")] CompanyBrandSections companybrandsections)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            if (ModelState.IsValid)
            {
                db.CompanyBrandSections.Add(companybrandsections);
                db.SaveChanges();
                return RedirectToAction("/AsociarMarcaIndex/" + ee.SingleOrDefault().Editions.EditionId + "/" + cc.SingleOrDefault().Companies.CompanyId + "/" + companybrandsections.BrandId);
            }
            return View(companybrandsections);
        }
        public ActionResult AsociarMarcaIndex(int id, int ed, int ad)
        {
            CompanyBrands cbs = db.CompanyBrands.SingleOrDefault(x => x.CompanyId == ed && x.BrandId == ad);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var rr = (from b in db.Brands
                      where b.BrandId == ad
                      select b);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            @ViewData["BrandName"] = rr.SingleOrDefault().BrandName; 
            ViewBag.IndexId = new SelectList(db.Indexes, "IndexId", "Description");
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(cbs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarMarcaIndex(int id, int ed, [Bind(Include = "IndexId,CompanyId,BrandId")] CompanyBrandIndexes Companybrandindexes)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            if (ModelState.IsValid)
            {
                db.CompanyBrandIndexes.Add(Companybrandindexes);
                db.SaveChanges();
                return RedirectToAction("/AgregarEdiciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + Companybrandindexes.CompanyId + "/" + Companybrandindexes.BrandId);
            }
            return View(Companybrandindexes);
        }
        public ActionResult DetalleMarca(int id, int ed) // Vista: Detalle de las marcas
        {
            var w = (from c in db.Companies
                     join cb in db.CompanyBrands
                     on c.CompanyId equals cb.CompanyId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     where b.BrandId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {Companies = c, CompanyBrands = cb, Brands = b});
            ViewData["CompanyName"] = w.FirstOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = w.FirstOrDefault().Companies.CompanyId;
            ViewData["BrandId"] = w.FirstOrDefault().Brands.BrandId;
            ViewData["BrandName"] = w.FirstOrDefault().Brands.BrandName;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            return View(w);
        }
        public ActionResult EditarMarca(int id, int ed) // Vista: Editar marcas
        {
            var w = (from c in db.Companies
                     join cb in db.CompanyBrands
                     on c.CompanyId equals cb.CompanyId
                     join b in db.Brands
                     on cb.BrandId equals b.BrandId
                     where b.BrandId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyBrands = cb, Brands = b });
            var brb = (from b in db.Brands
                       where b.BrandId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            ViewData["BrandName"] = brb.SingleOrDefault().Brands.BrandName;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(w);
        }
        [HttpPost] // Guarda los cambios
        [ValidateAntiForgeryToken]
        public ActionResult EditarMarca(int id, int ed, [Bind(Include = "BrandId,BrandName,Active")] Brands brands)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            if (ModelState.IsValid)
            {
                db.Entry(brands).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/DetalleMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" +  brands.BrandId);
            }
            return View(brands);
        }
        public ActionResult AsociarNuevaMarcaCliente(int id, int ed)
        {
            CompanyBrands ceee = db.CompanyBrands.FirstOrDefault(x => x.CompanyId == ed);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var rr = (from b in db.Brands
                      select b);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName");
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarNuevaMarcaCliente(int id, [Bind(Include = "CompanyId,BrandId,Description")] CompanyBrands companybrands)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            if (ModelState.IsValid)
            {
                db.CompanyBrands.Add(companybrands);
                db.SaveChanges();
                return RedirectToAction("/Marcas/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrands.CompanyId);
            }
            return View(companybrands);
        }
        public ActionResult EdiciónMarca(int id, int ed)
        {
            var pee = (from p in db.Brands
                       join pe in db.CompanyBrandEditions
                       on p.BrandId equals pe.BrandId
                       join edd in db.Editions
                       on pe.EditionId equals edd.EditionId
                       join c in db.Companies
                       on pe.CompanyId equals c.CompanyId
                       where p.BrandId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = p, CompanyBrandEditions = pe, Editions = edd, Companies = c });
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var ccc = (from p in db.Brands
                       where p.BrandId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = p });
            var com = (from c in db.Companies
                       join p in db.CompanyBrandEditions
                           on c.CompanyId equals p.CompanyId
                       where p.BrandId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyBrandEditions = p });

            ViewData["BrandName"] = ccc.SingleOrDefault().Brands.BrandName;
            ViewData["BrandId"] = ccc.SingleOrDefault().Brands.BrandId;
            return View(pee);
        }
        public ActionResult EditarEdicionesParticipantesMarcas(int id, int ed, int ad, int ud)
        {
            CompanyBrandEditions ceee = db.CompanyBrandEditions.SingleOrDefault(x => x.EditionId == ed && x.CompanyId == ad && x.BrandId == ud);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      join cbe in db.CompanyBrandEditions
                      on c.CompanyId equals cbe.CompanyId
                      where cbe.BrandId == ud && cbe.EditionId == ed && cbe.CompanyId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CN"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var bbb = (from b in db.Brands
                       where b.BrandId == ud
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            ViewData["BB"] = bbb.SingleOrDefault().Brands.BrandId;
            ViewData["BrandName"] = bbb.SingleOrDefault().Brands.BrandName;
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEdicionesParticipantesMarcas(int id, int ed, int ad, int ud, [Bind(Include = "EditionId,CompanyId,BrandId")]CompanyBrandEditions companybrandeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanyBrandEditions
                            where cs.CompanyId == companybrandeditions.CompanyId
                            && cs.BrandId == companybrandeditions.BrandId
                            select cs).ToList();
                foreach (CompanyBrandEditions css in pppp)
                {
                    var eliminar = db.CompanyBrandEditions.SingleOrDefault
                        (x => x.CompanyId == companybrandeditions.CompanyId && x.EditionId == companybrandeditions.EditionId
                         && x.BrandId == companybrandeditions.BrandId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanyBrandEditions
                                where cs.CompanyId == companybrandeditions.CompanyId
                                && cs.BrandId == companybrandeditions.BrandId
                                select cs).ToList();
                    foreach (CompanyBrandEditions css in rrrr)
                    {
                        var eliminar = db.CompanyBrandEditions.SingleOrDefault
                        (x => x.CompanyId == companybrandeditions.CompanyId
                        && x.BrandId == companybrandeditions.BrandId);
                        db.CompanyBrandEditions.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(companybrandeditions).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(companybrandeditions).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/EdiciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrandeditions.BrandId);
            }
            return View();
        }
        public ActionResult EliminarEdiciónMarca(int id, int ed, int ad, int ud)
        {
            CompanyBrandEditions ceee = db.CompanyBrandEditions.SingleOrDefault(x => x.EditionId == ed && x.CompanyId == ad && x.BrandId == ud);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      join cbe in db.CompanyBrandEditions
                      on c.CompanyId equals cbe.CompanyId
                      where cbe.BrandId == ud && cbe.EditionId == ed && cbe.CompanyId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CN"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var bbb = (from b in db.Brands
                       where b.BrandId == ud
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            var eddd = (from edr in db.Editions
                        where edr.EditionId == ed
                        select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = edr });
            ViewData["EE"] = eddd.SingleOrDefault().Editions.NumberEdition;
            ViewData["BB"] = bbb.SingleOrDefault().Brands.BrandId;
            ViewData["BrandName"] = bbb.SingleOrDefault().Brands.BrandName;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ad);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", ud);
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarEdiciónMarca(int id, int ed, int ad, int ud, [Bind(Include = "EditionId,CompanyId,BrandId")]CompanyBrandEditions companybrandeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(companybrandeditions).State = EntityState.Deleted;
                    db.SaveChanges();
                
                return RedirectToAction("/EdiciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrandeditions.BrandId);
            }
            return View();
        }
        public ActionResult AgregarEdiciónMarca(int id, int ed, int ad)
        {
            CompanyBrands ceee = db.CompanyBrands.SingleOrDefault(x => x.BrandId == ad && x.CompanyId == ed);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var bbb = (from b in db.Brands
                       where b.BrandId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Brands = b });
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CN"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            ViewData["BB"] = bbb.SingleOrDefault().Brands.BrandId;
            ViewData["BrandName"] = bbb.SingleOrDefault().Brands.BrandName;
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition");
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarEdiciónMarca(int id, int ed, int ad, [Bind(Include = "EditionId,CompanyId,BrandId")]CompanyBrandEditions companybrandeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Entry(companybrandeditions).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("/EdiciónMarca/" + ee.SingleOrDefault().Editions.EditionId + "/" + companybrandeditions.BrandId);
            }
            return View();
        }
        public ActionResult Secciones(int id, int ed, string palabra) // Vista: Secciones del cliente
        {
            Union_Companies_CompanyTypes_CompanyPhones_Cities u = new Union_Companies_CompanyTypes_CompanyPhones_Cities();
            var x = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     join cs in db.CompanySections
                     on c.CompanyId equals cs.CompanyId
                     join s in db.Sections
                     on cs.SectionId equals s.SectionId
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyTypes = ct, CompanySections = cs, Sections = s });
            if (!String.IsNullOrEmpty(palabra)) // Buscador
            {
                x = x.Where(j => j.Sections.Description.Contains(palabra));
            }
            var o = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = o.FirstOrDefault().Companies.CompanyName;
            var f = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyId"] = f.FirstOrDefault().Companies.CompanyId;
           var cc = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyShortName"] = cc.FirstOrDefault().Companies.CompanyShortName;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(x);
        }
        public ActionResult EditarSección(int id, int ed, int ad) // Vista: Editar secciones
        {
            Sections sects = db.Sections.SingleOrDefault(x => x.SectionId == ad);
            var rw = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = rw.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = rw.SingleOrDefault().Companies.CompanyId;
            var rr = (from c in db.Companies
                      join cs in db.CompanySections
                      on c.CompanyId equals cs.CompanyId
                      where c.CompanyId == ed
                      && cs.SectionId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs });
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });

            ViewBag.SectionIdParent = new SelectList(db.Sections, "SectionId", "Description",
                                                     section.SectionIdParent);

            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(sects);
        }
        [HttpPost] // Guarda los cambios
        [ValidateAntiForgeryToken]
        public ActionResult EditarSección(int id, int ed, [Bind(Include = "SectionId,Description,SectionIdParent,Active")] Sections Sections)
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            if (ModelState.IsValid)
            {
                db.Entry(Sections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/DetalleSección/" + ff.SingleOrDefault().Editions.EditionId + "/" + cc.SingleOrDefault().Companies.CompanyId + "/" + Sections.SectionId);
            }
            return View(Sections);
        }
        public ActionResult EliminarSección(int id, int ed, int ad)
        {
            CompanySections sects = db.CompanySections.SingleOrDefault(x => x.CompanyId == ed && x.SectionId == ad);
            var rw = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = rw.SingleOrDefault().Companies.CompanyName;
            ViewData["CCC"] = rw.SingleOrDefault().Companies.CompanyId;
            var rr = (from c in db.Companies
                      join cs in db.CompanySections
                      on c.CompanyId equals cs.CompanyId
                      join s in db.Sections
                      on cs.SectionId equals s.SectionId
                      where c.CompanyId == ed
                      && cs.SectionId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs, Sections = s});
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["SS"] = rr.SingleOrDefault().Sections.Description;
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(sects);
        }
        [HttpPost] // Guarda los cambios
        [ValidateAntiForgeryToken]
        public ActionResult EliminarSección(int id, int ed, int ad, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections companysections)
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            if (ModelState.IsValid)
            {
                db.Entry(companysections).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("/Secciones/" + ff.SingleOrDefault().Editions.EditionId + "/" + companysections.CompanyId);
            }
            return View(companysections);
        }
        public ActionResult AgregarSección(int id, int ed) // Vista: Agregar sección
         {
             var cc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewBag.SectionIdParent = new SelectList(db.Sections, "SectionId", "Description");
           var ccc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View();
        }
         [HttpPost]
        [ValidateAntiForgeryToken] // Guarda los cambios
        public ActionResult AgregarSección(int id, int ed, [Bind(Include = "SectionId,Description,SectionIdParent,Active")] Sections sections)
        {
            var rr = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Sections.Add(sections);
                db.SaveChanges();
                return RedirectToAction("/AsociarSecciónCliente/" + ff.SingleOrDefault().Editions.EditionId + "/" + rr.SingleOrDefault().Companies.CompanyId);
            }
               ViewBag.SectionIdParent = new SelectList(db.Sections, "SectionId", "Description",
                                                       sections.SectionIdParent);
            return View(sections);
        }
        public ActionResult AsociarSecciónCliente(int id, int ed)
         {
             var cc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
             var rr = (from s in db.Sections
                       select s);
             ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
             ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
             ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description", rr.Max(z => z.SectionId));
             var ccc = (from c in db.Companies
                        where c.CompanyId == ed
                        select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
             ViewData["C"] = ccc.SingleOrDefault().Companies.CompanyId;
             var ff = (from e in db.Editions
                       where e.EditionId == id
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
             ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
             ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
             return View();
         }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarSecciónCliente(int id, int ed, [Bind(Include = "CompanyId,SectionId,Active")] CompanySections companysections)
        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.CompanySections.Add(companysections);
                db.SaveChanges();
                return RedirectToAction("/DetalleSección/" + ff.SingleOrDefault().Editions.EditionId + "/" + companysections.CompanyId + "/" + companysections.SectionId);
            }
            return View(companysections);
        }
        public ActionResult DetalleSección(int id, int ed, int ad)
        {
            var x = (from c in db.Companies
                     join cs in db.CompanySections
                     on c.CompanyId equals cs.CompanyId
                     join s in db.Sections
                     on cs.SectionId equals s.SectionId
                     where c.CompanyId == ed
                     && s.SectionId == ad
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanySections = cs, Sections = s });
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyId"] = cc.SingleOrDefault().Companies.CompanyId;
            ViewData["CompanyName"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["SId"] = x.SingleOrDefault().Sections.SectionId;
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            return View(x);
        }
        public ActionResult AsociarSecciónNueva(int id, int ed)
        {
            CompanySections companies = db.CompanySections.SingleOrDefault(x => x.CompanyId == ed);
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
            return View(companies);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsociarSecciónNueva(int id, int ed, [Bind(Include = "SectionId,CompanyId,Active")] CompanySections csections)
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
            ViewBag.Companyid = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                               csections.CompanyId);
            ViewBag.SectionId = new SelectList(db.Sections, "SectionId", "Description",
                                               csections.SectionId);
            ViewBag.SectionIdParent = new SelectList(db.Sections, "SectionIdParent", "Description",
                                                     section.SectionIdParent);
            return View(csections);
        }
        public ActionResult Ediciones(int id, int ed)
        {
            var edd = (from c in db.Companies
                       join ce in db.CompanyEditions
                           on c.CompanyId equals ce.CompanyId
                       join edr in db.Editions
                           on ce.EditionId equals edr.EditionId
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyEditions = ce, Editions = edr });
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var ccc = (from c in db.Companies
                       where c.CompanyId == ed
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CompanyName"] = ccc.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = ccc.SingleOrDefault().Companies.CompanyId;
            return View(edd);
        }
        public ActionResult EditarEdicionesParticipantes(int id, int ed, int ad)
        {
            CompanyEditions ceee = db.CompanyEditions.SingleOrDefault(x => x.CompanyId == ad && x.EditionId == ed);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ad);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditarEdicionesParticipantes(int id, int ed, int ad, [Bind(Include = "CompanyId,EditionId,HtmlFile,HtmlContent")]CompanyEditions companyeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanyEditions
                            where cs.CompanyId == companyeditions.CompanyId
                            && cs.EditionId == companyeditions.EditionId
                            select cs).ToList();
                foreach (CompanyEditions css in pppp)
                {
                    var eliminar = db.CompanyEditions.SingleOrDefault
                        (x => x.CompanyId == companyeditions.CompanyId && x.EditionId == companyeditions.EditionId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanyEditions
                                where cs.CompanyId == companyeditions.CompanyId
                                && cs.EditionId == companyeditions.EditionId
                                select cs).ToList();
                    foreach (CompanyEditions css in rrrr)
                    {
                        var eliminar = db.CompanyEditions.SingleOrDefault
                        (x => x.CompanyId == companyeditions.CompanyId && x.EditionId == ed);
                        db.CompanyEditions.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(companyeditions).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var eliminar = db.CompanyEditions.SingleOrDefault
                    (x => x.CompanyId == companyeditions.CompanyId && x.EditionId == ed);
                    db.CompanyEditions.Remove(eliminar);
                    db.SaveChanges();
                    db.Entry(companyeditions).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/Ediciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyeditions.CompanyId);
            }
            return View();
        }
        public ActionResult AgregarEdiciónCliente(int id, int ed)
        {
            Companies compp = db.Companies.SingleOrDefault(model => model.CompanyId == ed);
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition");
            return View(compp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AgregarEdiciónCliente(int id, int ed, [Bind(Include = "CompanyId,EditionId,HtmlFile,HtmlContent")]CompanyEditions companyeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Entry(companyeditions).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("/Ediciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyeditions.CompanyId);
            }
            return View();
        }
        public ActionResult EliminarEdiciónCliente(int id, int ed, int ad)
        {
            CompanyEditions ceee = db.CompanyEditions.SingleOrDefault(x => x.CompanyId == ad && x.EditionId == ed);

            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            var cc = (from c in db.Companies
                      where c.CompanyId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["Companyname"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["C"] = cc.SingleOrDefault().Companies.CompanyId;
            var cee = (from ce in db.CompanyEditions
                       join edd in db.Editions
                           on ce.EditionId equals edd.EditionId
                       where ce.EditionId == ed && ce.CompanyId == ad
                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { CompanyEditions = ce, Editions = edd });
            ViewData["Edd"] = cee.SingleOrDefault().Editions.NumberEdition;
            ViewData["HtmlFile"] = cee.SingleOrDefault().CompanyEditions.HtmlFile;
            ViewData["HtmlContent"] = cee.SingleOrDefault().CompanyEditions.HtmlContent;
            return View(ceee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EliminarEdiciónCliente(int id, int ed, int ad, [Bind(Include = "CompanyId,EditionId,HtmlFile,HtmlContent")]CompanyEditions companyeditions)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(companyeditions).State = EntityState.Deleted;
                    db.SaveChanges();
                
                return RedirectToAction("/Ediciones/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyeditions.CompanyId);
            }
            return View();
        }
    }
}