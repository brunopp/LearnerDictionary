using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnerDictionary
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var cookieAuthOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login"),
                ExpireTimeSpan = new TimeSpan(0, 15, 0)
            };

            app.UseCookieAuthentication(cookieAuthOptions);
        }
    }
}