namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Veteran_Id", "dbo.Veterans");
            DropIndex("dbo.Images", new[] { "Veteran_Id" });
            AlterColumn("dbo.Images", "Veteran_Id", c => c.Int(nullable: true));
            CreateIndex("dbo.Images", "Veteran_Id");
            AddForeignKey("dbo.Images", "Veteran_Id", "dbo.Veterans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Veteran_Id", "dbo.Veterans");
            DropIndex("dbo.Images", new[] { "Veteran_Id" });
            AlterColumn("dbo.Images", "Veteran_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "Veteran_Id");
            AddForeignKey("dbo.Images", "Veteran_Id", "dbo.Veterans", "Id", cascadeDelete: true);
        }
    }
}
