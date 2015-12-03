using System.Data.Entity.ModelConfiguration;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Mapping
{
    public class ImageVeteranMap : EntityTypeConfiguration<ImageVeteran>
    {
        public ImageVeteranMap()
        {

            this.HasKey(x => x.Id);

            this.Property(x => x.ImageData);
            this.Property(x => x.ImageMimeType);

            this.HasRequired(x => x.Veteran);
        }
    }
}