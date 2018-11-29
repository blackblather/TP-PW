using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TP_PW.Models;

namespace TP_PW.Controllers
{
    public class ArtigosController : Controller
    {

        private ApplicationDbContext context;

        public ArtigosController()
        {
            context = new ApplicationDbContext();
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





        // GET: Artigos
        public ActionResult Index()
        {

            var Artigos = context.Artigos.ToList();
            return View(Artigos);
        }
    }
}