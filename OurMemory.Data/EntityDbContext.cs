using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurMemory.Data.Mapping;
using OurMemory.Domain.Entities;

namespace OurMemory.Data
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext() : base("DefaultConnection")
        {
        }


        private DbSet<Veteran> Veterans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VeteranMap());
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
