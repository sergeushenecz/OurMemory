using System.Collections.Generic;

namespace OurMemory.Domain.Entities
{
    public class PhotoAlbum : DomainObject
    {
        public string Title { get; set; }
        public int Views { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual User User { get; set; }
    }
}