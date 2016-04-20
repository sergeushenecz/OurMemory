namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldViewsToArticleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Arcticles", "Views", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Arcticles", "Views");
        }
    }
}
