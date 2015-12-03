using System;
using System.Collections.Generic;


namespace OurMemory.Domain.Entities
{
    public class Veteran : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string CountryLive { get; set; }
        public DateTime? DataBirh { get; set; }
        public string Сalled { get; set; }
        public string Front { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ImageVeteran> ImageVeterans { get; set; }

        public Veteran()
        {
            ImageVeterans = new List<ImageVeteran>();
        }
    }
}
