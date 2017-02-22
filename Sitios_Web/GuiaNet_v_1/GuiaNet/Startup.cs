using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuiaNet.Startup))]
namespace GuiaNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           // app.MapHubs();
        }
    }
}
