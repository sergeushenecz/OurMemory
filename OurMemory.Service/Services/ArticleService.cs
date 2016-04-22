using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using AutoMapper;
using OurMemory.Data.Infrastructure;
using OurMemory.Data.Specification.Core;
using OurMemory.Domain.DtoModel.ViewModel;
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
        private readonly IUserService _userService;

        public ArticleService(IRepository<Article> articleRepository,
            IUnitOfWork unitOfWork,
            ArticleSpecification articleSpecification, IUserService userService)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _articleSpecification = articleSpecification;
            _userService = userService;
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

        #region Implementation ICommentService

        public ICollection<Comment> GetComments(int id)
        {
            return _articleRepository.GetById(id)?.Comments;
        }

        public IEnumerable<CommentViewModel> GetCommentViewModels(ICollection<Comment> comments)
        {
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentViewModel>>(comments);
        }

        public Comment AddComment(int id, string message, string userId)
        {
            var article = _articleRepository.GetById(id);

            var user = userId == null ? null : _userService.GetById(userId);


            var comment = new Comment()
            {
                Article = article,
                User = user,
                Message = message,
                UpdatedDateTime = DateTime.UtcNow
            };

            article.Comments.Add(comment);
            UpdateArticle(article);

            return comment;
        }

        #endregion
    }
}