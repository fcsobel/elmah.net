using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(c3o.Logger.Test.Startup))]
namespace c3o.Logger.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
