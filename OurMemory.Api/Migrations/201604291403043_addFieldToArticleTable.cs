namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldToArticleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Title", c => c.String());
            AddColumn("dbo.Articles", "ShortDescription", c => c.String());
            AddColumn("dbo.Articles", "FullDescription", c => c.String());
            AddColumn("dbo.Articles", "ArticleImageleUrl", c => c.String());
            DropColumn("dbo.Articles", "Name");
            DropColumn("dbo.Articles", "Description");
            DropColumn("dbo.Articles", "ImageArticleUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ImageArticleUrl", c => c.String());
            AddColumn("dbo.Articles", "Description", c => c.String());
            AddColumn("dbo.Articles", "Name", c => c.String());
            DropColumn("dbo.Articles", "ArticleImageleUrl");
            DropColumn("dbo.Articles", "FullDescription");
            DropColumn("dbo.Articles", "ShortDescription");
            DropColumn("dbo.Articles", "Title");
        }
    }
}
