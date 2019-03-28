using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.ServiceLocation;
using Owin;

[assembly: OwinStartup(typeof(SocialMedia.Host.Startup))]
namespace SocialMedia.Host
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        
        public void Configuration(IAppBuilder app)
        {
            lock (thisObject)
            {
                DataProtectionProvider = app.GetDataProtectionProvider();
            }
            ConfigureAuth(app);
        }
    }
}