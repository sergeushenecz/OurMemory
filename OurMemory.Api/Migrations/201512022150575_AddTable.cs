namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Veterans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MidleName = c.String(),
                        CountryLive = c.String(),
                        DataBirh = c.DateTime(nullable: false).IsNullable,
                        Сalled = c.String(),
                        Front = c.String(),
                        ImageVeteran = c.Binary().IsNullable,
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
