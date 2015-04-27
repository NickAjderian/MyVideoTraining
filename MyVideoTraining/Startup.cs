using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyVideoTraining.Startup))]
namespace MyVideoTraining
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
