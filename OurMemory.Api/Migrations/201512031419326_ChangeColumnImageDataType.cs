namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnImageDataType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageVeterans", "Veteran_Id", "dbo.Veterans");
            DropIndex("dbo.ImageVeterans", new[] { "Veteran_Id" });
            AlterColumn("dbo.ImageVeterans", "ImageData", c => c.Binary());
            AlterColumn("dbo.ImageVeterans", "Veteran_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageVeterans", "Veteran_Id");
            AddForeignKey("dbo.ImageVeterans", "Veteran_Id", "dbo.Veterans", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageVeterans", "Veteran_Id", "dbo.Veterans");
            DropIndex("dbo.ImageVeterans", new[] { "Veteran_Id" });
            AlterColumn("dbo.ImageVeterans", "Veteran_Id", c => c.Int());
            AlterColumn("dbo.ImageVeterans", "ImageData", c => c.String());
            CreateIndex("dbo.ImageVeterans", "Veteran_Id");
            AddForeignKey("dbo.ImageVeterans", "Veteran_Id", "dbo.Veterans", "Id");
        }
    }
}
