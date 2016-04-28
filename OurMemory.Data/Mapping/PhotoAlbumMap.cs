using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class PhotoAlbumMap: EntityTypeConfiguration<PhotoAlbum>
    {
        public PhotoAlbumMap()
        {
            this.HasMany(x => x.Images);
            this.HasOptional(x => x.User);
        }
    }
}