using System;
using System.Collections.Generic;

namespace OurMemory.Domain.Entities
{
    public class Article : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}