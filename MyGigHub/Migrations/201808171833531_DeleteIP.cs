namespace MyGigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIP : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Timesheets", "Ip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Timesheets", "Ip", c => c.String());
        }
    }
}
