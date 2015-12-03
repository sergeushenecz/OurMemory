namespace OurMemory.Migrations.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableVeterans : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Veterans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MidleName = c.String(),
                        CountryLive = c.String(),
                        DataBirh = c.DateTime(nullable: true),
                        Сalled = c.String(),
                        Front = c.String(),
                        ImageVeteran = c.Binary(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Veterans");
        }
    }
}
