using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMS.Web.Startup))]
namespace SMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
