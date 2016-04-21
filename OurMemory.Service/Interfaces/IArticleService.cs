using System.Collections.Generic;
using System.Linq;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Interfaces
{
    public interface IArticleService
    {
        void Add(Article veteran);
        IEnumerable<Article> GetAll();
        Article GetById(int id);
        void UpdateArticle(Article article);
        IQueryable<Article> SearchArcticles(SearchArticleModel searchVeteranModel);
        void SaveArticle();
    }
}