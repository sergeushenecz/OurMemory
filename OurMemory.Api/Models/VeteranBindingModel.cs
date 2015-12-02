using System;
using System.ComponentModel.DataAnnotations;

namespace OurMemory.Models
{
    public class VeteranBindingModel
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string MidleName { get; set; }
        string CountryLive { get; set; }
        [DataType(DataType.DateTime)]
        DateTime DataBirh { get; set; }
        [DataType(DataType.DateTime)]
        string Сalled { get; set; }
        string Front { get; set; }
        byte[] ImageVeteran { get; set; }
        string Description { get; set; } 
    }
}