using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ARFood.Startup))]
namespace ARFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
