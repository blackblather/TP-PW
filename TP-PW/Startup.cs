using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TP_PW.Startup))]
namespace TP_PW
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
