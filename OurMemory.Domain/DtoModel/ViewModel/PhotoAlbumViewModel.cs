using System;
using System.Collections.Generic;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class PhotoAlbumViewModel
    {
        public string Name { get; set; }
        public string Views { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string ImageUrlAlbum { get; set; }

        public int CountPhoto { get; set; }
    }
}