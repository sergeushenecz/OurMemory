using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.Property(x => x.FirstName);
            this.Property(x => x.LastName);
            this.HasMany(x => x.Veterans);
            this.HasMany(x => x.Arcticles);
            this.HasMany(x => x.PhotoAlbums);
            this.HasMany(x => x.Comments);
        }
    }
}