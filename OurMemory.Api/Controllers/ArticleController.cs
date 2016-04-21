using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Extenshions;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;

namespace OurMemory.Controllers
{
    [Authorize(Roles = "User")]
    public class ArticleController : ApiController
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;

        public ArticleController(IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        /// <summary>
        /// Get a article by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/article/{id}")]
        [ResponseType(typeof(ArticleBindingModel))]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var article = _articleService.GetById(id);

            if (article == null)
            {
                return NotFound();
            }

            article.Views++;
            _articleService.SaveArticle();

            var articleBindingModel = Mapper.Map<Article, ArticleBindingModel>(article);
            return Ok(new
            {
                Article = articleBindingModel
            });
        }

        /// <summary>
        /// Get articles by conditional or get all articles
        /// </summary>
        /// <param name="searchArticleModel"></param>
        /// <returns></returns>
        [Route("api/article")]
        [ResponseType(typeof(ArticleBindingModel))]
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri]SearchArticleModel searchArticleModel)
        {
            IEnumerable<Article> arcticles = null;
            int? allCount = 0;

            if (searchArticleModel == null)
            {
                arcticles = _articleService.GetAll();
                allCount = arcticles?.ToList()?.Count();
            }
            else
            {
                allCount = _articleService.SearchArcticles(searchArticleModel)?.Count();
                arcticles = _articleService.SearchArcticles(searchArticleModel).Pagination((searchArticleModel.Page - 1) * searchArticleModel.Size, searchArticleModel.Size).ToList();
            }

            if (allCount == null)
            {
                return NotFound();
            }

            var articlesBindingModel = Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleBindingModel>>(arcticles);

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
        [ResponseType(typeof(ArticleBindingModel))]
        public IHttpActionResult Post(ArticleBindingModel articleBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Article article = Mapper.Map<ArticleBindingModel, Article>(articleBindingModel);

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest("User Not Found");
            }

            var user = _userService.GetById(userId);
            article.User = user;

            _articleService.Add(article);
            articleBindingModel = Mapper.Map<Article, ArticleBindingModel>(article);

            return Ok(articleBindingModel);
        }

        /// <summary>
        /// Update a article
        /// </summary>
        /// <param name="veteranBindingModel"></param>
        /// <returns></returns>
        [Route("api/article")]
        [ResponseType(typeof(ArticleBindingModel))]
        public IHttpActionResult Put([FromBody]ArticleBindingModel articleBindingModel)
        {
            var arcticle = _articleService.GetById(articleBindingModel.Id);

            if (ModelState.IsValid && articleBindingModel.Id == arcticle.Id)
            {
                Article mapVeteran = Mapper.Map<ArticleBindingModel, Article>(articleBindingModel);
                Mapper.Map(mapVeteran, arcticle);
                _articleService.UpdateArticle(arcticle);

                var articleModified = Mapper.Map<Article, ArticleBindingModel>(arcticle);

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
            var article = _articleService.GetById(id);

            if (article == null || article.Id != id) return BadRequest();

            article.IsDeleted = true;
            _articleService.SaveArticle();

            return Ok();
        }
    }
}
