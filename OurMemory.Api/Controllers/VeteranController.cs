﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using OurMemory.Data.Infrastructure;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Controllers
{
    public class VeteranController : BaseController
    {
        private readonly IVeteranService _veteranService;
        private readonly IUserService _userService;


        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VeteranController(IVeteranService veteranService, ApplicationUserManager userManager, IUserService userService)
            : base(userManager)
        {
            _veteranService = veteranService;
            _userService = userService;
        }

        public IEnumerable<VeteranBindingModel> Get()
        {
            IEnumerable<Veteran> veterans = _veteranService.GetAll();

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return veteranBindingModels;
        }

        public VeteranBindingModel Get(int id)
        {
            var veteran = _veteranService.GetById(id);

            veteran.Views++;

            _veteranService.SaveVeteran();

            var veteranBindingModels = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return veteranBindingModels;
        }

        public IEnumerable<VeteranBindingModel> Get(int pageIndex, int pageSize)
        {
            var veterans = _veteranService.GetAll().Skip(pageIndex * pageSize).Take(pageSize);

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return veteranBindingModels;
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
            var id = _veteranService.GetById(veteranBindingModel.Id).Id;

            if (ModelState.IsValid && veteranBindingModel.Id == id)
            {
                Veteran mapVeteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);

                _veteranService.UpdateVeteran(mapVeteran);

                return Ok(veteranBindingModel);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }

        public void Delete(int id)
        {
        }
    }
}
