namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoAlbums", "Title", c => c.String());
            AddColumn("dbo.PhotoAlbums", "Image", c => c.String());
            DropColumn("dbo.PhotoAlbums", "Name");
            DropColumn("dbo.PhotoAlbums", "ImageAlbumUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhotoAlbums", "ImageAlbumUrl", c => c.String());
            AddColumn("dbo.PhotoAlbums", "Name", c => c.String());
            DropColumn("dbo.PhotoAlbums", "Image");
            DropColumn("dbo.PhotoAlbums", "Title");
        }
    }
}
