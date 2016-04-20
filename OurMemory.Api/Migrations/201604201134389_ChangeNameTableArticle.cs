namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameTableArticle : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Arcticles", newName: "Articles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Articles", newName: "Arcticles");
        }
    }
}
