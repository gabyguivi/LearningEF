using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestExam.Web.Startup))]
namespace TestExam.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
