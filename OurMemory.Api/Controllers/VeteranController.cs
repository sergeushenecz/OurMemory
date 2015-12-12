using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Controllers
{
    public class VeteranController : ApiController
    {
        private readonly IVeteranService _veteranService;
        private readonly IUserService _userService;
        private readonly IImageVeteranService _imageVeteranService;


        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VeteranController(IVeteranService veteranService, IUserService userService, IImageVeteranService imageVeteranService)
        {
            _veteranService = veteranService;
            _userService = userService;
            _imageVeteranService = imageVeteranService;
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Veteran> veterans = _veteranService.GetAll();

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return Ok(new
            {
                Items = veteranBindingModels,
                TotalCount = _veteranService.GetAll().Count()
            });
        }

        public VeteranBindingModel Get(int id)
        {
            var veteran = _veteranService.GetById(id);

            veteran.Views++;

            _veteranService.SaveVeteran();

            var veteranBindingModels = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return veteranBindingModels;
        }

        [Route("api/veteran/{page}/{size}")]
        public IHttpActionResult Get(int page, int size)
        {
            var veterans = _veteranService.GetAll().Reverse().Skip((page - 1) * 10).Take(size);

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return Ok(new
            {
                Items = veteranBindingModels,
                TotalCount = _veteranService.GetAll().Count()
            });
        }

        public IHttpActionResult Post(VeteranBindingModel veteranBindingModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Veteran veteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);

            var userId = User.Identity.GetUserId();
            var user = _userService.GetById(userId);

            veteran.User = user;

            _veteranService.Add(veteran);

            veteranBindingModel = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return Ok(veteranBindingModel);
        }

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
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }

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
