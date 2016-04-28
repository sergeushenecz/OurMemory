using System.Collections.Generic;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class PhotoAlbumWithImagesViewModel: PhotoAlbumViewModel
    {
        public virtual IEnumerable<ImageReference> Images { get; set; }
    }
}