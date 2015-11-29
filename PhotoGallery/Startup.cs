using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoGalery.Startup))]
namespace PhotoGalery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
