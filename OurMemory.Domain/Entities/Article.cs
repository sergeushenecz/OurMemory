using System;
using System.Collections.Generic;

namespace OurMemory.Domain.Entities
{
    public class Article : DomainObject
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int Views { get; set; }
        public string Image { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}