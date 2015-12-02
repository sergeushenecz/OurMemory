﻿using System;
using System.ComponentModel.DataAnnotations;

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
        public byte[] ImageVeteran { get; set; }
        public string Description { get; set; }
    }
}