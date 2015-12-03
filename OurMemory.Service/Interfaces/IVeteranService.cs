﻿using System.Collections.Generic;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IVeteranService
    {
        void CreateVeteran(Veteran veteran);
        void SaveVeteran();
        IEnumerable<Veteran> GetAllVeterans();
    }
}