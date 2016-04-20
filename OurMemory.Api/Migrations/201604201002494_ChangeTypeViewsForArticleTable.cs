namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeViewsForArticleTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arcticles", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arcticles", "Views", c => c.String());
        }
    }
}
