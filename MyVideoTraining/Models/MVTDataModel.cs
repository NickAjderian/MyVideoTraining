namespace MyVideoTraining.Models
{
    using MyVideoTraining.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.Entity.Migrations;

    public class MVTDataModel : DbContext
    {

        private static string GetConnectionString()
        {

            if (System.Environment.MachineName == "DPT001647")
            {
                return "data source=.\\SQL03;initial catalog=MyVideoTraining;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            }
            return System.Configuration.ConfigurationManager.ConnectionStrings["MVTDataModel"].ConnectionString;
        }


        // Your context has been configured to use a 'MVTDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MyVideoTraining.Models.MVTDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MVTDataModel' 
        // connection string in the application configuration file.
        public MVTDataModel()
            : base(GetConnectionString()) //was "name=MVTDataModel"
        {
            this.Configuration.LazyLoadingEnabled = true;
            //Seed(this);
        }

        public MVTDataModel(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = true;
            //Seed(this); //NOT GOOD gets called on every instantiation
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentType> AssignmentTypes { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<MediaQuestion> MediaQuestions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Response> Responses { get; set; }


    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}