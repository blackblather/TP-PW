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
    public class mensagemsController : Controller
    {
        private context db = new context();

        // GET: mensagems
        public ActionResult Index()
        {
            return View(db.Mensagens.ToList());
        }

        // GET: mensagems/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = db.Mensagens.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // GET: mensagems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mensagems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mensagem,IdR,IdD")] mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                db.Mensagens.Add(mensagem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mensagem);
        }

        // GET: mensagems/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = db.Mensagens.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: mensagems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mensagem,IdR,IdD")] mensagem mensagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensagem);
        }

        // GET: mensagems/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mensagem mensagem = db.Mensagens.Find(id);
            if (mensagem == null)
            {
                return HttpNotFound();
            }
            return View(mensagem);
        }

        // POST: mensagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            mensagem mensagem = db.Mensagens.Find(id);
            db.Mensagens.Remove(mensagem);
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
