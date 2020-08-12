using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARFood.Models;

namespace ARFood.Controllers 
{
    public class AlmacenesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Almacenes
        public ActionResult Index()
        {
            return View(db.Almacenes.ToList());
        }

        // GET: Almacenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacenes almacenes = db.Almacenes.Find(id);
            if (almacenes == null)
            {
                return HttpNotFound();
            }
            return View(almacenes);
        }

        // GET: Almacenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Almacenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descripcion")] Almacenes almacenes)
        {
            if (ModelState.IsValid)
            {
                db.Almacenes.Add(almacenes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(almacenes);
        }

        // GET: Almacenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacenes almacenes = db.Almacenes.Find(id);
            if (almacenes == null)
            {
                return HttpNotFound();
            }
            return View(almacenes);
        }

        // POST: Almacenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descripcion")] Almacenes almacenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almacenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(almacenes);
        }

        // GET: Almacenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacenes almacenes = db.Almacenes.Find(id);
            if (almacenes == null)
            {
                return HttpNotFound();
            }
            return View(almacenes);
        }

        // POST: Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Almacenes almacenes = db.Almacenes.Find(id);
            db.Almacenes.Remove(almacenes);
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
