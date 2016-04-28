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
    public class PhotoAlbumController : ApiController
    {
        private readonly IPhotoAlbumService _photoAlbumService;
        private readonly IUserService _userService;
        private readonly IImageVeteranService _imageVeteranService;

        public PhotoAlbumController(IPhotoAlbumService photoAlbumService, IUserService userService, IImageVeteranService imageVeteranService)
        {
            _photoAlbumService = photoAlbumService;
            _userService = userService;
            _imageVeteranService = imageVeteranService;
        }

        [Route("api/photoAlbum/{id}")]
        [ResponseType(typeof(PhotoAlbumViewModel))]
        public IHttpActionResult Get(int id)
        {
            var photoAlbum = _photoAlbumService.GetById(id);

            if (photoAlbum == null)
            {
                return NotFound();
            }

            photoAlbum.Views++;
            _photoAlbumService.SavePhotoAlbum();

            var photoAlbumViewModel = Mapper.Map<PhotoAlbum, PhotoAlbumViewModel>(photoAlbum);

            return Ok(new
            {
                Items = photoAlbumViewModel
            });

        }

        [Route("api/photoAlbum")]
        [ResponseType(typeof(PhotoAlbumViewModel))]
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

            var photoAlbumViewModels = Mapper.Map<IEnumerable<PhotoAlbum>, IEnumerable<PhotoAlbumViewModel>>(photoAlbums);

            return Ok(new
            {
                Items = photoAlbumViewModels,
                TotalCount = countAlbums
            });
        }

        [Route("api/photoAlbum")]
        [ResponseType(typeof(PhotoAlbumViewModel))]
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
        public IHttpActionResult Put([FromBody]PhotoAlbumBindingModel photoAlbumBindingModel)
        {
            var photoAlbum = _photoAlbumService.GetById(photoAlbumBindingModel.Id);

            if (ModelState.IsValid && photoAlbumBindingModel.Id == photoAlbum.Id)
            {
                PhotoAlbum album = Mapper.Map<PhotoAlbumBindingModel, PhotoAlbum>(photoAlbumBindingModel);

                Mapper.Map(album, photoAlbum);
                _photoAlbumService.UpdatePhotoAlbum(photoAlbum);

                var photoAlbumViewModel = Mapper.Map<PhotoAlbum, PhotoAlbumViewModel>(photoAlbum);

                return Ok(photoAlbumViewModel);
            }

            return StatusCode(HttpStatusCode.NotModified);


        }

        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
