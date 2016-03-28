using System;
using System.Collections.Generic;
using System.Data;


namespace OurMemory.Domain.Entities
{
    public class Veteran : DomainObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateBirth { get; set; }
        public string BirthPlace { get; set; }
        public DateTime? DateDeath { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public DateTime? Called { get; set; }
        public string Awards { get; set; }
        public string Troops { get; set; }
        public string Description { get; set; }

        public int Views { get; set; }

        public virtual ICollection<ImageVeteran> Images { get; set; }

        public virtual User User { get; set; }


        public Veteran()
        {
            Images = new List<ImageVeteran>();
        }
    }
}
