using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP_PW.Models;

namespace TP_PW.Controllers
{
    public class ArtigosController : Controller
    {
        private eMuseuContext db = new eMuseuContext();

        // GET: Artigos
        public ActionResult Index()
        {
            return View(db.Artigos.ToList());
        }

        // GET: Artigos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            return View(artigos);
        }

        // GET: Artigos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao")] Artigos artigos)
        {
            if (ModelState.IsValid)
            {
                db.Artigos.Add(artigos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artigos);
        }

        // GET: Artigos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao")] Artigos artigos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artigos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artigos);
        }

        // GET: Artigos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artigos artigos = db.Artigos.Find(id);
            if (artigos == null)
            {
                return HttpNotFound();
            }
            return View(artigos);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Artigos artigos = db.Artigos.Find(id);
            db.Artigos.Remove(artigos);
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
