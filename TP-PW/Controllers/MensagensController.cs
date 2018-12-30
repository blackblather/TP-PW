using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP_PW.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TP_PW.Controllers
{
    public class MensagensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mensagens
        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);


            

            List<Mensagen> mensagens = new List<Mensagen>();
            IEnumerable<MensagenViewModel> mensage;
            List<MensagenViewModel> tmp = new List<MensagenViewModel>();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            mensagens = (from u in db.Mensagens where (u.IdRemetente == user.Id || u.IdRecetor == user.Id) select u).ToList();


            foreach (var mens in mensagens)
            {
                MensagenViewModel mvm = new MensagenViewModel() { Id = mens.Id, Assunto = mens.Assunto, Recetor = userManager.FindById(mens.IdRecetor).UserName, Remetente = userManager.FindById(mens.IdRemetente).UserName, Mensagem = mens.Mensagem, HoraEnvio = mens.HoraEnvio };
                tmp.Add(mvm);
            }
            mensage = tmp.AsEnumerable();
            


            return View(mensage);
        }

        // GET: Mensagens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagen mensagen = db.Mensagens.Find(id);
            MensagenViewModel mensagenVM = new MensagenViewModel()
            {
                Assunto = mensagen.Assunto, Id = mensagen.Id, HoraEnvio = mensagen.HoraEnvio,
                Remetente = db.Users.Find(mensagen.IdRemetente).UserName,
                Recetor = db.Users.Find(mensagen.IdRecetor).UserName, Mensagem = mensagen.Mensagem
            };
            if (mensagen == null)
            {
                return HttpNotFound();
            }
            return View(mensagenVM);
        }

        public ActionResult Responder(string recetor)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userRecep = userManager.FindById(recetor);
            ViewBag.Recetor = userRecep.UserName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Responder([Bind(Include = "Mensagem,Recetor,Assunto")] MensagenViewModel mensagen)
        {

            if (ModelState.IsValid)
            {
                Mensagen mens = null;
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser userRemet = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var userRecep = userManager.FindByName(mensagen.Recetor);

                mens = new Mensagen
                {
                    HoraEnvio = DateTime.Now,
                    IdRecetor = userRecep.Id,
                    Assunto = mensagen.Assunto,
                    IdRemetente = userRemet.Id,
                    Mensagem = mensagen.Mensagem
                };

                

                db.Mensagens.Add(mens);
                db.SaveChanges();

                ViewBag.Recetor = mensagen.Recetor;

                return RedirectToAction("Index");

            }


            return View(mensagen);
        }

        // GET: Mensagens/Create
            public ActionResult Create()
        {
            List<string> users;
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            users = (from u in userManager.Users where u.Id != user.Id select u.UserName).ToList();
            ViewBag.Users = new SelectList(users);
            return View();
        }

        // POST: Mensagens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mensagem,Recetor,Assunto")] MensagenViewModel mensagen)
        {
           
            
            if (ModelState.IsValid)
            {
                List<string> users;
                Mensagen mens = null;
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ApplicationUser userRemet = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                var userRecep = userManager.FindByName(mensagen.Recetor);

               mens = new Mensagen
                {
                    HoraEnvio = DateTime.Now,
                    IdRecetor = userRecep.Id,
                    Assunto = mensagen.Assunto,
                    IdRemetente = userRemet.Id,
                    Mensagem = mensagen.Mensagem
                };

                users = (from u in userManager.Users where u.Id != userRemet.Id select u.UserName).ToList();
                
                db.Mensagens.Add(mens);
                db.SaveChanges();

                ViewBag.Users = new SelectList(users);

                return RedirectToAction("Index");

            }
            
            
            return View(mensagen);
        }

        // GET: Mensagens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagen mensagen = db.Mensagens.Find(id);
            if (mensagen == null)
            {
                return HttpNotFound();
            }
            return View(mensagen);
        }

        // POST: Mensagens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mensagem,IdRemetente,IdRecetor,HoraEnvio")] Mensagen mensagen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensagen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensagen);
        }

        // GET: Mensagens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensagen mensagen = db.Mensagens.Find(id);
            if (mensagen == null)
            {
                return HttpNotFound();
            }
            return View(mensagen);
        }

        // POST: Mensagens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensagen mensagen = db.Mensagens.Find(id);
            db.Mensagens.Remove(mensagen);
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
