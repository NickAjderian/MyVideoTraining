using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyVideoTraining.Api;
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
        private string GetConnectionString()
        {

            if (System.Environment.MachineName == "DPT001647")
            {
                return "data source=.\\SQL03;initial catalog=MyVideoTrainingTests;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            }
            return "data source=.\\;initial catalog=MyVideoTrainingTests;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        }

        [TestMethod]
        public void ContextShouldBuildAndSeedBlankDatabase()
        {
            var config = new MyVideoTraining.Migrations.Configuration();
            var initializer = new System.Data.Entity.MigrateDatabaseToLatestVersion<MVTDataModel, MyVideoTraining.Migrations.Configuration>(true, config);
            System.Data.Entity.Database
                .SetInitializer<MVTDataModel>(initializer);

            var ctx = new MVTDataModel(GetConnectionString());
            //ctx.Database.Initialize(true);
            //MyVideoTraining.Migrations.Configuration.Seed(ctx);


            Assert.IsTrue(ctx.People.Any());

            var p1 = ctx.People.FirstOrDefault();

            Assert.IsTrue(p1.Assignments.Any());

        }

        [TestMethod]
        public async Task CandidatesControllerShouldReturnCandidatesWithAssignmentsList()
        {
            var controller = new CandidatesController();
            var cands = await controller.GetPeople();

            Assert.IsTrue(cands.Any());
            foreach (var person in cands)
            {
                Assert.IsTrue(person.Assignments.Any());
            }

        }

    }
}
