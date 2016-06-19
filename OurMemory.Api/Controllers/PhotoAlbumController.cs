using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OurMemory.Common.Attributes;
using OurMemory.Domain;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Extenshions;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;

namespace OurMemory.Controllers
{
    [Roles(UserRoles.User, UserRoles.Administrator)]
    public class PhotoAlbumController : ApiController
    {
        private readonly IPhotoAlbumService _photoAlbumService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService, IUserService userService, IImageService imageService)
        {
            _photoAlbumService = photoAlbumService;
            _userService = userService;
            _imageService = imageService;
        }

        [Route("api/photoAlbum/{id}")]
        [ResponseType(typeof(PhotoAlbumWithImagesViewModel))]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var photoAlbum = _photoAlbumService.GetById(id);

            if (photoAlbum == null)
            {
                return NotFound();
            }

            photoAlbum.Views++;
            _photoAlbumService.SavePhotoAlbum();

            var photoAlbumViewModel = Mapper.Map<PhotoAlbum, PhotoAlbumWithImagesViewModel>(photoAlbum);

            return Ok(photoAlbumViewModel);

        }
        [Route("api/photoAlbum")]
        [ResponseType(typeof(PhotoAlbumViewModel))]
        [AllowAnonymous]
        public IHttpActionResult Get([FromUri] SearchPhotoAlbumModel searchPhotoAlbumModel)
        {
            IEnumerable<PhotoAlbum> photoAlbums = null;
            int countAlbums = 0;

            if (searchPhotoAlbumModel == null)
            {
                photoAlbums = _photoAlbumService.GetAll();
                countAlbums = _photoAlbumService.GetAll().Count();
            }
            else
            {
                countAlbums = _photoAlbumService.SearchPhotoAlbum(searchPhotoAlbumModel).Count();
                photoAlbums = _photoAlbumService.SearchPhotoAlbum(searchPhotoAlbumModel).Pagination((searchPhotoAlbumModel.Page - 1) * searchPhotoAlbumModel.Size, searchPhotoAlbumModel.Size).ToList();
            }

            var photoAlbumViewModels = Mapper.Map<IEnumerable<PhotoAlbum>, IEnumerable<PhotoAlbumWithImagesViewModel>>(photoAlbums);

            return Ok(new
            {
                Items = photoAlbumViewModels,
                TotalCount = countAlbums
            });
        }

        [Route("api/photoAlbum")]
        [ResponseType(typeof(PhotoAlbumWithImagesViewModel))]
        public IHttpActionResult Post(PhotoAlbumBindingModel albumBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            PhotoAlbum photoAlbum = Mapper.Map<PhotoAlbumBindingModel, PhotoAlbum>(albumBindingModel);

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest("User Not Found");
            }

            var user = _userService.GetById(userId);

            photoAlbum.User = user;

            _photoAlbumService.Add(photoAlbum);

            var photoAlbumViewModel = Mapper.Map<PhotoAlbum, PhotoAlbumWithImagesViewModel>(photoAlbum);

            return Ok(photoAlbumViewModel);

        }

        [Route("api/photoAlbum")]
        [ResponseType(typeof(PhotoAlbumWithImagesViewModel))]
        public IHttpActionResult Put([FromBody]PhotoAlbumBindingModel photoAlbumBindingModel)
        {
            var photoAlbum = _photoAlbumService.GetById(photoAlbumBindingModel.Id);

            var userId = User.Identity.GetUserId();

            if (ModelState.IsValid && photoAlbumBindingModel.Id == photoAlbum.Id && userId == photoAlbum.User.Id || User.IsInRole(UserRoles.Administrator))
            {
                _imageService.DeleteImages(photoAlbum.Images);

                Mapper.Map(photoAlbumBindingModel, photoAlbum);

                _photoAlbumService.UpdatePhotoAlbum(photoAlbum);
                var photoAlbumViewModel = Mapper.Map<PhotoAlbum, PhotoAlbumWithImagesViewModel>(photoAlbum);

                return Ok(photoAlbumViewModel);
            }

            return StatusCode(HttpStatusCode.NotModified);


        }

        /// <summary>
        /// delete a photo album
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/photoAlbum/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var album = _photoAlbumService.GetById(id);

            var userId = User.Identity.GetUserId();

            if (album != null && album.Id == id || album.User.Id == userId || User.IsInRole(UserRoles.Administrator))
            {
                album.IsDeleted = true;
                _photoAlbumService.SavePhotoAlbum();

                return Ok();
            }

            return BadRequest();
        }

    }
}
