using System;
using System.ComponentModel.DataAnnotations;


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
        public byte[] ImageVeteran { get; set; }
        public string Description { get; set; }

    }
}
