namespace RescueMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 50),
                        Registered = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rescues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        When = c.DateTimeOffset(nullable: false, precision: 7),
                        Type = c.String(maxLength: 50),
                        Content = c.String(maxLength: 1024),
                        Who_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Registrations", t => t.Who_Id)
                .Index(t => t.Who_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rescues", "Who_Id", "dbo.Registrations");
            DropIndex("dbo.Rescues", new[] { "Who_Id" });
            DropTable("dbo.Rescues");
            DropTable("dbo.Registrations");
        }
    }
}
