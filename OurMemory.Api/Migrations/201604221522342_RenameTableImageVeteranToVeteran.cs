namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTableImageVeteranToVeteran : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ImageVeterans", newName: "Images");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Images", newName: "ImageVeterans");
        }
    }
}
