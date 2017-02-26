using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Challenge.Web.Startup))]
namespace Challenge.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
