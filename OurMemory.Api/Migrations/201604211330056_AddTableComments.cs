namespace OurMemory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTableComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Message = c.String(),
                    IsDeleted = c.Boolean(nullable: false),
                    CreatedDateTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDateTime = c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    User_Id = c.String(maxLength: 128),
                    Article_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Article_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Article_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropTable("dbo.Comments");
        }
    }
}
