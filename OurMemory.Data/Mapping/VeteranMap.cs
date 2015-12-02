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
            this.Property(x => x.MidleName);
            this.Property(x => x.CountryLive);
            this.Property(x => x.DataBirh);
            this.Property(x => x.Description);
            this.Property(x => x.Front);
            this.Property(x => x.Сalled);
            this.Property(x => x.ImageVeteran);
            
        }
    }
}