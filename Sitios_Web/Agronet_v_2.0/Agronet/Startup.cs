using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agronet.Startup))]
namespace Agronet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
