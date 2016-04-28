using System;
using System.Collections.Generic;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class PhotoAlbumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Views { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string ImageAlbumUrl { get; set; }
        public int CountPhoto { get; set; }
    }
}