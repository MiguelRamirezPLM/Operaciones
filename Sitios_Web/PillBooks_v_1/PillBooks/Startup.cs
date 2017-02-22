using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

[assembly: OwinStartupAttribute(typeof(PillBooks.Startup))]
namespace PillBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
