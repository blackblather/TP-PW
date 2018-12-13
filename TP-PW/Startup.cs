using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TP_PW.Models;

[assembly: OwinStartupAttribute(typeof(TP_PW.Startup))]
namespace TP_PW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }


        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //vai criar um role administrador e um user admin se não exitirem já na db
            if (!roleManager.RoleExists("Administrador"))
            {
                // primeiro cria-se o role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrador";
                roleManager.Create(role);

                // depois cria-se um user admin

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@emuseu.pt";
                string userPWD = "1qazZAQ!";

                var chkUser = userManager.Create(user, userPWD);

                // dasse ao admin o role
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrador");
                }
            }

            // Cria o role de Especialista
            if (!roleManager.RoleExists("Especialista"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Especialista";
                roleManager.Create(role);
            }

            // Cria o role de Utilizador
            if (!roleManager.RoleExists("Utilizador"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Utilizador";
                roleManager.Create(role);
            }
            // Cria o role de PorPermitir
            if (!roleManager.RoleExists("PorPermitir"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "PorPermitir";
                roleManager.Create(role);
            }
        }
    }
}
