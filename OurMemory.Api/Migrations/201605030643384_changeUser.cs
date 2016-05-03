namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            DropColumn("dbo.Articles", "ArticleImageUrl");
            DropColumn("dbo.AspNetUsers", "UserImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserImageUrl", c => c.String());
            AddColumn("dbo.Articles", "ArticleImageUrl", c => c.String());
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.Articles", "Image");
        }
    }
}
