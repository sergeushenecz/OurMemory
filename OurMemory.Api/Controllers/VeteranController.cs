using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
    public class VeteranController : ApiController
    {
        private readonly IVeteranService _veteranService;
        private readonly IUserService _userService;
        private readonly IImageVeteranService _imageVeteranService;

        public VeteranController(IVeteranService veteranService, IUserService userService, IImageVeteranService imageVeteranService)
        {
            _veteranService = veteranService;
            _userService = userService;
            _imageVeteranService = imageVeteranService;
        }
        [Route("api/veteran/{id}")]
        public IHttpActionResult Get(int id)
        {
            var veteran = _veteranService.GetById(id);

            if (veteran == null)
            {
                return NotFound();
            }

            veteran.Views++;

            _veteranService.SaveVeteran();

            var veteranBindingModels = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return Ok(veteranBindingModels);
        }
        [Route("api/veteran")]
        public IHttpActionResult Get([FromUri]SearchVeteranModel searchVeteranModel)
        {
            IEnumerable<Veteran> veterans = null;
            int allCount = 0;

            if (searchVeteranModel == null)
            {
                veterans = _veteranService.GetAll();
                allCount = _veteranService.GetAll().Count();
            }
            else
            {
                allCount = _veteranService.SearchVeterans(searchVeteranModel).Count();
                veterans = _veteranService.SearchVeterans(searchVeteranModel).Pagination((searchVeteranModel.Page - 1) * searchVeteranModel.Size, searchVeteranModel.Size).ToList();
            }

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return Ok(new
            {
                Items = veteranBindingModels,
                TotalCount = allCount
            });
        }
        [Route("api/veteran")]
        public IHttpActionResult Post(VeteranBindingModel veteranBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Veteran veteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest("User Not Found");
            }

            var user = _userService.GetById(userId);

            veteran.User = user;

            _veteranService.Add(veteran);

            veteranBindingModel = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return Ok(veteranBindingModel);
        }
        [Route("api/veteran")]
        public IHttpActionResult Put([FromBody]VeteranBindingModel veteranBindingModel)
        {
            var veteran = _veteranService.GetById(veteranBindingModel.Id);

            if (ModelState.IsValid && veteranBindingModel.Id == veteran.Id)
            {
                _imageVeteranService.DeleteImagesVeteran(veteran.Images);
                Veteran mapVeteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);
                Mapper.Map<Veteran, Veteran>(mapVeteran, veteran);
                _veteranService.UpdateVeteran(veteran);

                var veteranModified = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

                return Ok(veteranModified);
            }

            return StatusCode(HttpStatusCode.NotModified);
        }
        [Route("api/veteran")]
        public IHttpActionResult Delete(int id)
        {
            var veteran = _veteranService.GetById(id);

            if (veteran == null || veteran.Id != id) return BadRequest();

            veteran.IsDeleted = true;
            _veteranService.SaveVeteran();

            return Ok();
        }
    }
}
