using System.Collections.Generic;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IVeteranService
    {
        void Add(Veteran veteran);
        IEnumerable<Veteran> GetAll();
        Veteran GetById(int id);
        void UpdateVeteran(Veteran veteran);

        void SaveVeteran();
    }
}