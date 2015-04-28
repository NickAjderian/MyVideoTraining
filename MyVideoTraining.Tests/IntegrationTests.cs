using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyVideoTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVideoTraining.Tests
{
    [TestClass]
    public class IntegrationTests
    {
        string cs = "data source=vetinarius;initial catalog=MyVideoTrainingTests;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        [TestMethod]
        public void ContextShouldBuildAndSeedBlankDatabase()
        {
            var config = new MyVideoTraining.Migrations.Configuration();
            var initializer = new System.Data.Entity.MigrateDatabaseToLatestVersion<MVTDataModel, MyVideoTraining.Migrations.Configuration>(true, config);
            System.Data.Entity.Database
                .SetInitializer<MVTDataModel>(initializer);

            var ctx = new MVTDataModel(cs);
            ctx.Database.Initialize(true);
            MyVideoTraining.Migrations.Configuration.Seed(ctx);


            Assert.IsTrue(ctx.People.Any());

            var p1 = ctx.People.FirstOrDefault();

            Assert.IsTrue(p1.Assignments.Any());



        }

    }
}
