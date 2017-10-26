using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SMS.Api.Startup))]

namespace SMS.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //This CORS is present in Microsoft.AspNet.WebApi.Cors
            //Allow cors for Owin as well as Web Api
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
