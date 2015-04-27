namespace MyVideoTraining.Migrations
{
    using MyVideoTraining.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyVideoTraining.Models.MVTDataModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;            
        }

    }
}
