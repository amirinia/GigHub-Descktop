namespace MyGigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class some : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timesheets", "StartDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Timesheets", "EndDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Timesheets", "FirstDay");
            DropColumn("dbo.Timesheets", "LastDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timesheets", "LastDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Timesheets", "FirstDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Timesheets", "EndDay");
            DropColumn("dbo.Timesheets", "StartDay");
        }
    }
}
