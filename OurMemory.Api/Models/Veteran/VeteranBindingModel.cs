using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OurMemory.Domain.Entities;

namespace OurMemory.Models.Veteran
{
    public class VeteranBindingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CountryLive { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DataBirth { get; set; }
        public string Сalled { get; set; }
        public string Front { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<ImageVeteranBindingModel> ImageVeterans { get; set; }

        public VeteranBindingModel()
        {
            ImageVeterans = new List<ImageVeteranBindingModel>();
        }
    }
}