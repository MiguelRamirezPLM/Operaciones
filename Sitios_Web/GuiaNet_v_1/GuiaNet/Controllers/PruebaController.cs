using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuiaNet.Models;

namespace GuiaNet.Controllers
{
    public class PruebaController : Controller
    {
        private Guia db = new Guia();

        // GET: /Prueba/
        public ActionResult Index()
        {
            var clients = (from client in db.Clients
                           orderby client.CompanyName ascending
                           select client).ToList().Take(200);

            return View(clients);
        }

        // GET: /Prueba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: /Prueba/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode");
            return View();
        }

        // POST: /Prueba/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClientId,ClientIdParent,ClientCode,CompanyName,Image,Active,ShortName,Description,AlphabetId,ANUNCIANTEID")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            return View(clients);
        }

        // GET: /Prueba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            return View(clients);
        }

        // POST: /Prueba/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClientId,ClientIdParent,ClientCode,CompanyName,Image,Active,ShortName,Description,AlphabetId,ANUNCIANTEID")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(clients).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientCode", clients.ClientId);
            return View(clients);
        }

        // GET: /Prueba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: /Prueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
