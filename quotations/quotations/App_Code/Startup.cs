using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(quotations.Startup))]
namespace quotations
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
