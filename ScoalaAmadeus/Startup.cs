using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScoalaAmadeus.Startup))]
namespace ScoalaAmadeus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
