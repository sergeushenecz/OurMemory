using System;
using System.Collections.Generic;
using System.Linq;
using OurMemory.Data.Infrastructure;
using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Extenshions;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using OurMemory.Service.Specification;

namespace OurMemory.Service.Services
{
    public class ArticleService : IArticleService
    {

        private readonly IRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArticleSpecification _articleSpecification;

        public ArticleService(IRepository<Article> articleRepository, IUnitOfWork unitOfWork, ArticleSpecification articleSpecification)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _articleSpecification = articleSpecification;
        }


        #region ArticleService Members

        public Article GetById(int id)
        {
            Article article = _articleRepository.GetById(id);

            return article;
        }

        public IQueryable<Article> SearchArcticles(SearchArticleModel searchVeteranModel)
        {
            var keyWord = _articleSpecification.KeyWord(searchVeteranModel);
            var arcticles = _articleRepository.GetSpec(keyWord.Predicate).OrderBy(x => x.Name);

            return arcticles;
        }

        public void UpdateArticle(Article article)
        {
            _articleRepository.Update(article);
            SaveArticle();
        }

        public void Add(Article veteran)
        {
            _articleRepository.Add(veteran);

            SaveArticle();
        }

        public IEnumerable<Article> GetAll()
        {
            return _articleRepository.GetAll().Where(x => !x.IsDeleted);
        }

        public void SaveArticle()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}