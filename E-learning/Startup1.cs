using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(E_learning.Startup1))]

namespace E_learning
{
  public class Startup1
  {
    public void Configuration(IAppBuilder app)
    {
      app.UseCookieAuthentication(new CookieAuthenticationOptions()
      {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login")
      });
    }
  }
}
