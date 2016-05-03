using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {

            this.HasKey(x => x.Id);

            this.Property(x => x.ImageOriginal);
            this.Property(x => x.ImageThumbnail);
            this.Property(x => x.ImageMimeType);

            this.HasOptional(x => x.Veteran);

            this.HasOptional(x => x.PhotoAlbum);
        }
    }
}