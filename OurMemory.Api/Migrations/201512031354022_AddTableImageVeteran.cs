namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableImageVeteran : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageVeterans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageData = c.String(nullable:false),
                        ImageMimeType = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Veteran_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Veterans", t => t.Veteran_Id)
                .Index(t => t.Veteran_Id);
            
            AddColumn("dbo.Veterans", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Veterans", "ImageVeteran");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Veterans", "ImageVeteran", c => c.Binary());
            DropForeignKey("dbo.ImageVeterans", "Veteran_Id", "dbo.Veterans");
            DropIndex("dbo.ImageVeterans", new[] { "Veteran_Id" });
            DropColumn("dbo.Veterans", "IsDeleted");
            DropTable("dbo.ImageVeterans");
        }
    }
}
