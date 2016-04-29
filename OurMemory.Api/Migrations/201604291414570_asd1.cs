namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ArticleImageUrl", c => c.String());
            DropColumn("dbo.Articles", "ArticleImageleUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ArticleImageleUrl", c => c.String());
            DropColumn("dbo.Articles", "ArticleImageUrl");
        }
    }
}
