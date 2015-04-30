namespace MyVideoTraining.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientDemographics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.People", "NHSNumber", c => c.Int(nullable: false));
            AddColumn("dbo.People", "NokName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "NokName");
            DropColumn("dbo.People", "NHSNumber");
            DropColumn("dbo.People", "ClientID");
        }
    }
}
