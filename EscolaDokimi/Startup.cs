using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscolaDokimi.Startup))]
namespace EscolaDokimi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
