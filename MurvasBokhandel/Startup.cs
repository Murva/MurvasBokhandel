using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MurvasBokhandel.Startup))]
namespace MurvasBokhandel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
