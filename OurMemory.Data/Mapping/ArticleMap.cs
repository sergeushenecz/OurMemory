using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Name);
            this.Property(x => x.Description);
            this.Property(x => x.CreatedDateTime).HasColumnType("datetime");
            this.Property(x => x.UpdatedDateTime).HasColumnType("datetime"); ;
            this.HasOptional(x => x.User);
        }
    }
}