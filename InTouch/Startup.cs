using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InTouch.Startup))]
namespace InTouch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
