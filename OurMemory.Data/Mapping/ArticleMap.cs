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
            this.HasOptional(x => x.User);
            this.HasMany(x => x.Comments);
        }
    }
}