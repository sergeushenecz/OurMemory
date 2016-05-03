namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageThumbnail", c => c.String());
            DropColumn("dbo.Images", "ThumbnailImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ThumbnailImage", c => c.String());
            DropColumn("dbo.Images", "ImageThumbnail");
        }
    }
}
