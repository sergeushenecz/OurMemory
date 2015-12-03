using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OurMemory.Domain.Entities
{
    public class ImageVeteran : Base
    {
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        [JsonIgnore]
        public Veteran Veteran { get; set; }

    }
}