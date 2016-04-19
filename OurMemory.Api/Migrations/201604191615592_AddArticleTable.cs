namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arcticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false),
                        UpdatedDateTime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.ImageVeterans", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ImageVeterans", "UpdatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Veterans", "CreatedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Veterans", "UpdatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Arcticles", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Arcticles", new[] { "User_Id" });
            DropColumn("dbo.Veterans", "UpdatedDateTime");
            DropColumn("dbo.Veterans", "CreatedDateTime");
            DropColumn("dbo.ImageVeterans", "UpdatedDateTime");
            DropColumn("dbo.ImageVeterans", "CreatedDateTime");
            DropTable("dbo.Arcticles");
        }
    }
}
