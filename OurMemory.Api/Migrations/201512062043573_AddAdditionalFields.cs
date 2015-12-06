namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Veterans", "BirthPlace", c => c.String());
            AddColumn("dbo.Veterans", "DateDeath", c => c.DateTime());
            AddColumn("dbo.Veterans", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Veterans", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.Veterans", "Awards", c => c.String());
            AddColumn("dbo.Veterans", "Troops", c => c.String());
            AddColumn("dbo.Veterans", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Veterans", "Сalled", c => c.DateTime());
            CreateIndex("dbo.Veterans", "User_Id");
            AddForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Veterans", "CountryLive");
            DropColumn("dbo.Veterans", "Front");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Veterans", "Front", c => c.String());
            AddColumn("dbo.Veterans", "CountryLive", c => c.String());
            DropForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Veterans", new[] { "User_Id" });
            AlterColumn("dbo.Veterans", "Сalled", c => c.String());
            DropColumn("dbo.Veterans", "User_Id");
            DropColumn("dbo.Veterans", "Troops");
            DropColumn("dbo.Veterans", "Awards");
            DropColumn("dbo.Veterans", "Longitude");
            DropColumn("dbo.Veterans", "Latitude");
            DropColumn("dbo.Veterans", "DateDeath");
            DropColumn("dbo.Veterans", "BirthPlace");
        }
    }
}
