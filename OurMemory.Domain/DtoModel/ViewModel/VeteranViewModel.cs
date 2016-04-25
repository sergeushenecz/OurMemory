using System;
using System.Collections.Generic;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class VeteranViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DateBirth { get; set; }
        public string BirthPlace { get; set; }
        public string DateDeath { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public string Called { get; set; }
        public string Awards { get; set; }
        public string Troops { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public string FullName { get; set; }
        public Guid UserId { get; set; }
        public virtual IEnumerable<ImageReference> Images { get; set; }

        public VeteranViewModel()
        {
            Images = new List<ImageReference>();
        }
    }
}