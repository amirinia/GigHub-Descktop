namespace MyGigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimesheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        Ip = c.String(),
                        FirstDay = c.DateTime(nullable: false),
                        LastDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Timesheets", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Timesheets", new[] { "ArtistId" });
            DropTable("dbo.Timesheets");
        }
    }
}
