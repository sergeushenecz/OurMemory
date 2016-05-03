using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OurMemory.Domain.Entities
{
    public class Image : DomainObject
    {
        public string ImageOriginal { get; set; }
        public string ImageThumbnail { get; set; }
        public string ImageMimeType { get; set; }
        public Veteran Veteran { get; set; }
        public PhotoAlbum PhotoAlbum { get; set; }
    }
}