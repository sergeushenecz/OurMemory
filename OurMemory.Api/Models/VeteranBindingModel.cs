using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OurMemory.Domain.Entities;

namespace OurMemory.Models
{
    public class VeteranBindingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string CountryLive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataBirh { get; set; }
        public string Сalled { get; set; }
        public string Front { get; set; }
        public string Description { get; set; }

        public IEnumerable<ImageVeteran> ImageVeterans { get; set; }
    }
}