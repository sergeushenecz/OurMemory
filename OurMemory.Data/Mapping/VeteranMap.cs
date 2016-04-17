using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class VeteranMap : EntityTypeConfiguration<Veteran>
    {
        public VeteranMap()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.FirstName);
            this.Property(x => x.LastName);
            this.Property(x => x.MiddleName);
            this.Property(x => x.DateBirth);
            this.Property(x => x.BirthPlace);
            this.Property(x => x.DateDeath);
            this.Property(x => x.Latitude);
            this.Property(x => x.Longitude);
            this.Property(x => x.Called);
            this.Property(x => x.Awards);
            this.Property(x => x.Troops);
            this.Property(x => x.Description);
            this.Property(x => x.Views);

            this.HasMany(x => x.Images);

            //this.HasRequired(x => x.User);
            this.HasOptional(x => x.User).WithRequired();
        }
    }
}