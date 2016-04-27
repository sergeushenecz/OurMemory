using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Extenshions;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;

namespace OurMemory.Controllers
{
    [Authorize(Roles = "User")]
    public class ArticleController : ApiController
    {
        private readonly IArticle _article;
        private readonly IUserService _userService;

        public ArticleController(IArticle article, IUserService userService)
        {
            _article = article;
            _userService = userService;
        }

        /// <summary>
        /// Get a article by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/article/{id}")]
        [ResponseType(typeof(ArticleViewModel))]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var article = _article.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            article.Views++;
            _article.SaveArticle();

            var articleBindingModel = Mapper.Map<Article, ArticleViewModel>(article);

            return Ok(articleBindingModel);
        }

        /// <summary>
        /// Get articles by conditional or get all articles
        /// </summary>
        /// <param name="searchArticleModel"></param>
        /// <returns></returns>
        [Route("api/article")]
        [ResponseType(typeof(ArticleViewModel))]
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri]SearchArticleModel searchArticleModel)
        {
            IEnumerable<Article> arcticles = null;
            int? allCount = 0;

            if (searchArticleModel == null)
            {
                arcticles = _article.GetAll();
                allCount = arcticles?.ToList()?.Count();
            }
            else
            {
                allCount = _article.SearchArcticles(searchArticleModel)?.Count();
                arcticles = _article.SearchArcticles(searchArticleModel).Pagination((searchArticleModel.Page - 1) * searchArticleModel.Size, searchArticleModel.Size).ToList();
            }

            if (allCount == null)
            {
                return NotFound();
            }

            var articlesBindingModel = Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleViewModel>>(arcticles);

            return Ok(new
            {
                Items = articlesBindingModel,
                TotalCount = allCount
            });
        }

        /// <summary>
        /// Add a article
        /// </summary>
        /// <param name="veteranBindingModel"></param>
        /// <returns></returns>
        [Route("api/article")]
        [ResponseType(typeof(ArticleViewModel))]
        public IHttpActionResult Post(ArticleBindingModel articleBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Article article = Mapper.Map<ArticleBindingModel, Article>(articleBindingModel);

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest();
            }

            var user = _userService.GetById(userId);
            article.User = user;

            _article.Add(article);
            var articleViewModel = Mapper.Map<Article, ArticleViewModel>(article);

            return Ok(articleViewModel);
        }

        /// <summary>
        /// Update a article
        /// </summary>
        /// <param name="veteranBindingModel"></param>
        /// <returns></returns>
        [Route("api/article")]
        [ResponseType(typeof(ArticleViewModel))]
        public IHttpActionResult Put([FromBody]ArticleBindingModel articleBindingModel)
        {
            var arcticle = _article.GetById(articleBindingModel.Id);
            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid && articleBindingModel.Id == arcticle.Id && userId == arcticle.User.Id)
            {
                Article mapVeteran = Mapper.Map<ArticleBindingModel, Article>(articleBindingModel);
                Mapper.Map(mapVeteran, arcticle);
                _article.UpdateArticle(arcticle);

                var articleModified = Mapper.Map<Article, ArticleViewModel>(arcticle);

                return Ok(articleModified);
            }

            return StatusCode(HttpStatusCode.NotModified);
        }

        /// <summary>
        /// Delete a article by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/article")]
        public IHttpActionResult Delete(int id)
        {
            var article = _article.GetById(id);
            var userId = User.Identity.GetUserId();

            if (article == null || article.Id != id || article.User.Id != userId) return BadRequest();

            article.IsDeleted = true;
            _article.SaveArticle();

            return Ok();
        }
    }
}
