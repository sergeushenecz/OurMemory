using System;
using System.ComponentModel.DataAnnotations;

namespace OurMemory.Service
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
    }
}