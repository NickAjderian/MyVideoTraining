namespace MyVideoTraining.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01AddMobile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Mobile", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Mobile");
        }
    }
}
