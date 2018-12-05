using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using TP_PW.Models;

namespace TP_PW.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext context;

        public UsersController()
        {
            context = new ApplicationDbContext();
        }




        // GET: Users
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    if (!IsEspeUser()) { 
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Users = context.Users.ToList();
            return View(Users);
        }


        public bool IsEspeUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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

                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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

        // GET: /Users/AdicionarUser
        public ActionResult AdicionarUser()
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


            ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("Administrador"))
                .ToList(), "Name", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarUser(AdicionarUserViewModel model)
        {
            if (!User.Identity.IsAuthenticated || !IsAdminUser())
                return RedirectToAction("Index", "Home");

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

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
    }
}
        