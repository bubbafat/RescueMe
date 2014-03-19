namespace RescueMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqifyRegistrationNumber : DbMigration
    {
        public override void Up()
        {
            CreateIndex(table: "Registrations", column: "Number", unique: true, name: "UC_Registrations_Number");
        }

        public override void Down()
        {
            DropIndex(table: "Registrations", name: "UC_Registrations_Number");
        }
    }
}
