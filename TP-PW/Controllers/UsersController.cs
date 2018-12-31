using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TP_PW.Models;

namespace TP_PW.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db;

        public UsersController()
        {
            db = new ApplicationDbContext();
        }

        // Metodos
        public bool IsEspeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Especialista")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }



        public bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Administrador")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


        // GET: Users
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    if (!IsEspeUser())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = db.Users.Find(id);

            var roles = db.Roles.ToList();

            foreach (var r in roles)
            {
                foreach (var ar in applicationUser.Roles)
                {
                    if (ar.RoleId == r.Id)
                        ViewBag.Role = r.Name;
                }
            }


            if (applicationUser == null)
            {
                return HttpNotFound();
            }


            return View(applicationUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Name = new SelectList(db.Roles.Where(u => !u.Name.Contains("Administrador")).ToList(), "Name", "Name");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdicionarUserViewModel model)
        {
            if (!User.Identity.IsAuthenticated || !IsAdminUser())
                return RedirectToAction("Index", "Home");

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = new ApplicationUser();
            user.PrimeiroNome = model.PrimeiroNome;
            user.Apelido = model.Apelido;
            user.DataNascimento = model.DataNascimento;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.NumTelemovel;

            var chkUser = UserManager.Create(user, model.Password);
            if (chkUser.Succeeded)
                UserManager.AddToRoles(user.Id, model.UserRole);

            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = db.Users.Find(id);



            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
                "Id,PrimeiroNome,Apelido,DataNascimento,Autorizado,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")]
            ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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





        // Roles
        // 
        // https://andersnordby.wordpress.com/2014/11/28/asp-net-mvc-4-5-owin-simple-roles-management/
        //

        [Authorize(Roles = "Administrador")]
        public ActionResult RoleAddToUser(string id)
        {
            List<string> roles;
            List<string> userRoles;
            IEnumerable<string> user;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                user = (from u in userManager.Users where u.Id == id select u.UserName);
                roles = (from r in roleManager.Roles select r.Name).ToList();
                var applicationUser = userManager.FindById(id);

                var userRoleIds = (from r in applicationUser.Roles select r.RoleId);
                userRoles = (from idr in userRoleIds
                    let r = roleManager.FindById(idr)
                    select r.Name).ToList();

                if (user.Count() == 1)
                {
                    ViewBag.Roles = new SelectList(roles);
                    ViewBag.RolesForThisUsere = new SelectList(userRoles);
                    ViewBag.User = user.ElementAt(0);
                    ViewBag.Id = id;
                    return View();
                }
            }

            return View("Index");
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string roleName, string userName)
        {
            List<string> roles;
            IEnumerable<string> user;
            List<string> userRoles;
            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                user = (from u in userManager.Users where u.UserName == userName select u.UserName);

                var userx = userManager.FindByName(userName);
                if (userx == null)
                    throw new Exception("user not found!");

                var role = roleManager.FindByName(roleName);
                if (role == null)
                    throw new Exception("Role not found!");

                if (userManager.IsInRole(userx.Id, role.Name))
                {
                    ViewBag.ResultMessage = "This user already has the role specified !";
                }
                else
                {
                    userManager.AddToRole(userx.Id, role.Name);
                    context.SaveChanges();

                    ViewBag.ResultMessage = "Username added to the role succesfully !";
                }

                roles = (from r in roleManager.Roles select r.Name).ToList();
                var userRoleIds = (from r in userx.Roles select r.RoleId);
                userRoles = (from idr in userRoleIds
                    let r = roleManager.FindById(idr)
                    select r.Name).ToList();
                if (user.Count() == 1)
                {
                    ViewBag.Roles = new SelectList(roles);
                    ViewBag.RolesForThisUsere = new SelectList(userRoles);
                    ViewBag.User = user.ElementAt(0);

                    return View();
                }
            }

            return View("Index");
        }


        
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            List<string> userRoles;
            List<string> roles;
            IEnumerable<string> users;

            using (var context = new ApplicationDbContext())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                roles = (from r in roleManager.Roles select r.Name).ToList();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);


                users = (from u in userManager.Users where u.UserName == userName select u.UserName);
                var user = userManager.FindByName(userName);

                if (user == null)
                    throw new Exception("User not found!");

                if (userManager.IsInRole(user.Id, roleName))
                {
                    userManager.RemoveFromRole(user.Id, roleName);
                    context.SaveChanges();

                    ViewBag.ResultMessage = "Role removed from this user successfully !";
                }
                else
                {
                    ViewBag.ResultMessage = "This user doesn't belong to selected role.";
                }

                var userRoleIds = (from r in user.Roles select r.RoleId);
                userRoles = (from idr in userRoleIds
                    let r = roleManager.FindById(idr)
                    select r.Name).ToList();
                if (users.Count() == 1)
                {
                    ViewBag.Roles = new SelectList(roles);
                    ViewBag.RolesForThisUsere = new SelectList(userRoles);
                    ViewBag.User = users.ElementAt(0);

                    return View("RoleAddToUser");
                }
            }

            return View("Index");
        }
    }
}
