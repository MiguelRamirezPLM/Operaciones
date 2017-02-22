using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MediVet.Startup))]
namespace MediVet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
