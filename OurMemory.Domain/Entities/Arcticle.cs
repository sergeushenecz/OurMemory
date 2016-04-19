using System;

namespace OurMemory.Domain.Entities
{
    public class Arcticle : DomainObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
    }
}