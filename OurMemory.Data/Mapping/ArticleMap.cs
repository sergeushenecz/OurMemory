using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class ArticleMap: EntityTypeConfiguration<Arcticle>
    {
        public ArticleMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name);
            this.Property(x => x.Description);
            this.Property(x => x.CreatedDateTime);
            this.Property(x => x.UpdatedDateTime);

            this.HasOptional(x => x.User);
        }
    }
}