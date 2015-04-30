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

            var config = new MyVideoTraining.Migrations.Configuration();
            var initializer = new System.Data.Entity
                .MigrateDatabaseToLatestVersion<MyVideoTraining.Models.MVTDataModel, MyVideoTraining.Migrations.Configuration>(true, config);
            System.Data.Entity.Database
                .SetInitializer<MyVideoTraining.Models.MVTDataModel>(initializer);

        }
    }
}
