namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PhotoAlbums", "ImageAlbumUrl", c => c.String());
            DropColumn("dbo.PhotoAlbums", "ImageUrlAlbum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhotoAlbums", "ImageUrlAlbum", c => c.String());
            DropColumn("dbo.PhotoAlbums", "ImageAlbumUrl");
        }
    }
}
