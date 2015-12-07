namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldViews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Veterans", new[] { "User_Id" });
            AddColumn("dbo.Veterans", "DateBirth", c => c.DateTime());
            AddColumn("dbo.Veterans", "Called", c => c.DateTime());
            AddColumn("dbo.Veterans", "Views", c => c.Int(nullable: false));
            AlterColumn("dbo.Veterans", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Veterans", "User_Id");
            AddForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Veterans", "DataBirth");
            DropColumn("dbo.Veterans", "Сalled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Veterans", "Сalled", c => c.DateTime());
            AddColumn("dbo.Veterans", "DataBirth", c => c.DateTime());
            DropForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Veterans", new[] { "User_Id" });
            AlterColumn("dbo.Veterans", "User_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Veterans", "Views");
            DropColumn("dbo.Veterans", "Called");
            DropColumn("dbo.Veterans", "DateBirth");
            CreateIndex("dbo.Veterans", "User_Id");
            AddForeignKey("dbo.Veterans", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
