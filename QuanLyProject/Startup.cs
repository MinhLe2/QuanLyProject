using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyProject.Startup))]
namespace QuanLyProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
