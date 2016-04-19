using System.Collections.Generic;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IArticleService
    {
        void Add(Arcticle veteran);
        IEnumerable<Arcticle> GetAll();
        Arcticle GetById(int id);
        void UpdateArticle(Arcticle veteran);
//        IQueryable<Arcticle> SearchVeterans(SearchVeteranModel searchVeteranModel);
        void SaveArticle();
    }
}