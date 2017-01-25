using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstaXamarinWeb.Startup))]
namespace InstaXamarinWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
