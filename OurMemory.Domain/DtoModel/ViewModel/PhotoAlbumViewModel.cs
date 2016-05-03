using System;
using System.Collections.Generic;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class PhotoAlbumViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Views { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CountPhoto { get; set; }
        public UserViewModel User { get; set; }
    }
}