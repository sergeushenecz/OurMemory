using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OurMemory.Domain.DtoModel
{
    public class VeteranBindingModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateBirth { get; set; }
        public string BirthPlace { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateDeath { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Called { get; set; }
        public string Awards { get; set; }
        public string Troops { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string FullName { get; set; }
        public virtual IEnumerable<ImageVeteranBindingModel> Images { get; set; }

        public VeteranBindingModel()
        {
            Images = new List<ImageVeteranBindingModel>();
        }
    }
}