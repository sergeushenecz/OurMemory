using System.Data.Entity.Migrations;
using OurMemory.Data;

namespace OurMemory.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
           


        }
    }
}
