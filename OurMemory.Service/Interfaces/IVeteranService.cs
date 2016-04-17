using System.Collections.Generic;
using System.Linq;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Interfaces
{
    public interface IVeteranService
    {
        void Add(Veteran veteran);
        IEnumerable<Veteran> GetAll();
        Veteran GetById(int id);
        void UpdateVeteran(Veteran veteran);
        IQueryable<Veteran> SearchVeterans(SearchVeteranModel searchVeteranModel);
        void SaveVeteran();
    }
}