using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Guianet.Startup))]
namespace Guianet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
