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
                    var list = db.Emprestimos
                        .Where(emp => emp.IdUtilizador == currentUserId)
                        .Join(db.EstadoEmprestimo,
                              emp => emp.IdEstado,
                              est => est.Id,
                              (emp, est) => new EmprestimosViewModel { emprestimo = emp, estadoEmprestimo = est })
                        .Select(empEst => empEst);

                    /*var list3 = from emp in db.Emprestimos
                               join usr in db.Users on emp.IdUtilizador equals usr.Id
                               where usr.Id == currentUserId
                               select new EmprestimosUsersViewModel { emprestimo = emp, utilizador = usr };*/

                    return View(list);
                }
                else
                {
                    var list = db.Emprestimos
                        .Join(db.EstadoEmprestimo,
                              emp => emp.IdEstado,
                              est => est.Id,
                              (emp, est) => new { emprestimo = emp, estadoEmprestimo = est })
                        .Join(db.Users,
                              empEst => empEst.emprestimo.IdUtilizador,
                              usr => usr.Id,
                              (empEst, usr) => new EmprestimosViewModel { emprestimo = empEst.emprestimo, estadoEmprestimo = empEst.estadoEmprestimo, utilizador = usr })
                        .Select(empEstUsr => empEstUsr);

                    /*var list3 = from emp in db.Emprestimos
                               join usr in db.Users on emp.IdUtilizador equals usr.Id
                               where usr.Id == currentUserId
                               select new EmprestimosUsersViewModel { emprestimo = emp, utilizador = usr };*/

                    return View(list);
                }
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



        private List<Artigo> GetArtigosDisponiveis() {
            List<Artigo> artigos = new List<Artigo>();
            var list = db.Artigos
                    .GroupJoin(db.ArtigosEmprestimos,
                               art => art.Id,
                               artEmp => artEmp.IdArtigo,
                               (art, artEmp) => new { artigo = art, artigosEmprestimo = artEmp });
            foreach (var art in list)
            {
                if (art.artigosEmprestimo.Count() > 0)
                {
                    var returnedArtEmpList = art.artigosEmprestimo.Where(artEmp => artEmp.DataRetornoArtigo != null);
                    foreach (var art3 in returnedArtEmpList)
                        artigos.Add(art3.Artigo);
                }
                else
                {
                    artigos.Add(art.artigo);
                }
            }
            return artigos;
        }

        // GET: Emprestimos/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Utilizador"))
                return View(GetArtigosDisponiveis());
            else
                return RedirectToAction("Index", "Home");
        }

        private bool IdArtigoExiste(List<Artigo> listaArtigos, int idArt)
        {
            foreach (Artigo art in listaArtigos)
                if (art.Id == idArt)
                    return true;
            return false;
        }
        
        private bool IdArtigoExiste(List<ArtigosEmprestimo> listaArtigos, int idArt)
        {
            foreach (ArtigosEmprestimo artEmp in listaArtigos)
                if (artEmp.IdArtigo == idArt)
                    return true;
            return false;
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DateTime dataEmprestimo, List<string> artigosEmprestimo)
        {
            try
            {
                if (User.IsInRole("Utilizador") && ModelState.IsValid && artigosEmprestimo.Count() > 0 && DateTime.Compare(dataEmprestimo, DateTime.Today) >= 0)
                {
                    List<ArtigosEmprestimo> artEmpFiltered = new List<ArtigosEmprestimo>();
                    foreach (string idArt in artigosEmprestimo)
                    {
                        if (idArt != "false")
                        {
                            int idArtInt = Convert.ToInt32(idArt);
                            if (IdArtigoExiste(GetArtigosDisponiveis(), idArtInt) && !IdArtigoExiste(artEmpFiltered, idArtInt))
                            {
                                artEmpFiltered.Add(new ArtigosEmprestimo()
                                {
                                    IdArtigo = idArtInt
                                });
                            }
                        }
                    }
                    Emprestimo emprestimo = new Emprestimo()
                    {
                        DataEmprestimo = dataEmprestimo,
                        IdEstado = 1,   //-> Pendente
                        IdUtilizador = User.Identity.GetUserId(),
                        ArtigosEmprestimos = artEmpFiltered
                    };
                    db.Emprestimos.Add(emprestimo);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        private EmprestimosViewModel GetEmprestimosViewModel(int idEmp)
        {
            var empUsr = db.Emprestimos.Where(emp => emp.Id == idEmp)
                                        .Join(db.EstadoEmprestimo,
                                            emp => emp.IdEstado,
                                          estEmp => estEmp.Id,
                                            (emp, estEmp) => new {
                                                emp = emp,
                                                estEmp = estEmp
                                            })
                                        .Join(db.Users,
                                            empEstEmp => empEstEmp.emp.IdUtilizador,
                                            usr => usr.Id,
                                            (empEstEmp, usr) => new EmprestimosViewModel()
                                            {
                                                estadoEmprestimo = empEstEmp.estEmp,
                                                emprestimo = empEstEmp.emp,
                                                todosOsEstados = db.EstadoEmprestimo.ToList(),
                                                utilizador = usr
                                            })
                                        .ToList();
            if (empUsr.Count() == 1)
                return empUsr.ElementAt(0);
            return null;
        }

        // GET: Emprestimos/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Administrador") || User.IsInRole("Especialista"))
            {
                EmprestimosViewModel empVM;
                if ((empVM = GetEmprestimosViewModel(id)) != null)
                    return View(empVM);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool IdEstadoExiste(int idEstado)
        {
            var estadoEmprestimo = db.EstadoEmprestimo.Where(estEmp => estEmp.Id == idEstado).ToList();
            if (estadoEmprestimo.Count() == 1)
                return true;
            return false;
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize] 
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idEstado, int idEmprestimo)
        {
            if (ModelState.IsValid && (User.IsInRole("Administrador") || User.IsInRole("Especialista")))
            {
                Emprestimo empUpdate;
                if (IdEstadoExiste(idEstado) /*&& idEstado != emprestimo.IdEstado*/ && (empUpdate = db.Emprestimos.Find(idEmprestimo)) != null)
                {
                    if (idEstado != empUpdate.IdEstado)
                        empUpdate.IdEstado = idEstado;
                    db.Entry(empUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return RedirectToAction("Index");
        }

        // GET: Emprestimos/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if((User.IsInRole("Administrador") || User.IsInRole("Especialista")))
            {
                EmprestimosViewModel empVM;
                if ((empVM = GetEmprestimosViewModel(id)) != null)
                    return View(empVM);
            }
            return RedirectToAction("Index", "Emprestimos");

        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idEmprestimo)
        {
            if ((User.IsInRole("Administrador") || User.IsInRole("Especialista")))
            {
                Emprestimo emprestimo = db.Emprestimos.Find(idEmprestimo);
                db.Emprestimos.Remove(emprestimo);
                db.SaveChanges();
            }
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
