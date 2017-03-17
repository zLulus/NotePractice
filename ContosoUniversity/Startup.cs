using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContosoUniversity.Startup))]
namespace ContosoUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
