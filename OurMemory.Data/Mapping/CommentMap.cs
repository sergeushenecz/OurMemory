using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap CommentMap1 { get; set; }

        public CommentMap()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Message);
            this.HasOptional(x => x.User);
            this.HasOptional(x => x.Article);
        }
    }
}