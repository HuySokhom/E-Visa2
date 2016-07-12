using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eVisa.Startup))]
namespace eVisa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
