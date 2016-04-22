using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class ImageVeteranMap : EntityTypeConfiguration<Image>
    {
        public ImageVeteranMap()
        {

            this.HasKey(x => x.Id);

            this.Property(x => x.ImageOriginal);
            this.Property(x => x.ThumbnailImage);
            this.Property(x => x.ImageMimeType);

            this.HasRequired(x => x.Veteran);
        }
    }
}