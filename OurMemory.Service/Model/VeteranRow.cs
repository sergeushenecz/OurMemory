using System;

namespace OurMemory.Service.Model
{
    public class VeteranRow
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateBirth { get; set; }
        public string BirthPlace { get; set; }
        public string DateDeath { get; set; }

        public string Called { get; set; }
        public string Awards { get; set; }
        public string Troops { get; set; }
        public string Description { get; set; }

        public string UrlImages { get; set; }
    }
}