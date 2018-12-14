using Microsoft.AspNet.Identity;
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
    public class EmprestimosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emprestimos
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Utilizador"))
            {
                if (User.IsInRole("Utilizador"))
                {
                    //Source #1: https://forums.asp.net/t/1959723.aspx?ASP+MVC5+Identity+How+to+get+current+ApplicationUser
                    string currentUserId = User.Identity.GetUserId();

                    /*OLD VERSION*/
                    //Source #2: https://www.oficinadanet.com.br/artigo/asp.net/fazendo-inner-join-e-left-join-com-linq-no-aspnet
                    /*var list = from emp in db.Emprestimos 
                               join usr in db.Users on emp.IdUtilizador equals usr.Id
                               where usr.Id == currentUserId
                               select emp;*/

                    /*NEW VERSION*/
                    //Source #3: https://stackoverflow.com/questions/2767709/join-where-with-linq-and-lambda
                    //Source #4: https://stackoverflow.com/questions/13692015/how-to-rewrite-this-linq-using-join-with-lambda-expressions
                    //Source #5: https://stackoverflow.com/questions/5207382/get-data-from-two-tablesjoin-with-linq-and-return-result-into-view
                    var list2 = db.Emprestimos
                        .Join(db.Users,
                              emp => emp.IdUtilizador,
                              usr => usr.Id,
                              (emp,usr) => new { emp, usr })
                        .Where(empUsr => empUsr.usr.Id == currentUserId)
                        .Select(empUsr => new EmprestimosViewModel { emprestimo = empUsr.emp, utilizador = empUsr.usr });

                    /*var list3 = from emp in db.Emprestimos
                               join usr in db.Users on emp.IdUtilizador equals usr.Id
                               where usr.Id == currentUserId
                               select new EmprestimosUsersViewModel { emprestimo = emp, utilizador = usr };*/

                    return View(list2);
                }
                else
                    //Not working now because im doint that lambda stuff
                    return View(db.Emprestimos.ToList());
            }
            else
                return RedirectToAction("Index", "Home");

        }

        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUtilizador,IdEstado,DataEmprestimo")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUtilizador,IdEstado,DataEmprestimo")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            db.Emprestimos.Remove(emprestimo);
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
