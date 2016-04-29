namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldImageUrltoArticleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ImageArticleUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ImageArticleUrl");
        }
    }
}
