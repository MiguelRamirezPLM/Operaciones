using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DEAQ.Startup))]
namespace DEAQ
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
