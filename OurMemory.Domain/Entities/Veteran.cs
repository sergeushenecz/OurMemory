using System;
using System.ComponentModel.DataAnnotations;


namespace OurMemory.Domain.Entities
{
    public class Veteran
    {
        [Key]
        int VeteranId { get; set; }
        string FirstName { get; set; }

        string LastName { get; set; }
        string MidleName { get; set; }
        string CountryLive { get; set; }
        DateTime DataBirh { get; set; }
        string Сalled { get; set; }
        string Front { get; set; }
        byte[] ImageVeteran { get; set; }

        string Description { get; set; }

    }
}
