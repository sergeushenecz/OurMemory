namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablePhotoAlbums : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Views = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDateTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        UpdatedDateTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Images", "PhotoAlbum_Id", c => c.Int());
            CreateIndex("dbo.Images", "PhotoAlbum_Id");
            AddForeignKey("dbo.Images", "PhotoAlbum_Id", "dbo.PhotoAlbums", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoAlbums", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Images", "PhotoAlbum_Id", "dbo.PhotoAlbums");
            DropIndex("dbo.Images", new[] { "PhotoAlbum_Id" });
            DropIndex("dbo.PhotoAlbums", new[] { "User_Id" });
            DropColumn("dbo.Images", "PhotoAlbum_Id");
            DropTable("dbo.PhotoAlbums");
        }
    }
}
