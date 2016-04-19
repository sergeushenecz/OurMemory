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

        private readonly IRepository<Arcticle> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArticleSpecification _articleSpecification;

        public ArticleService(IRepository<Arcticle> articleRepository, IUnitOfWork unitOfWork, ArticleSpecification articleSpecification)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _articleSpecification = articleSpecification;
        }


        #region ArticleService Members

        public Arcticle GetById(int id)
        {
            return null;
        }

        public void UpdateArticle(Arcticle veteran)
        {
            throw new NotImplementedException();
        }

        public void UpdateVeteran(Veteran veteran)
        {
          
        }

        public void Add(Arcticle veteran)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Arcticle> GetAll()
        {
            return null;
        }

        public void SaveArticle()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}