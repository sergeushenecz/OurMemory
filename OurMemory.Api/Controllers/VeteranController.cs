using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Service.Interfaces;

namespace OurMemory.Controllers
{
    public class VeteranController : ApiController
    {
        private readonly IVeteranService _veteranService;

        public VeteranController(IVeteranService veteranService)
        {
            _veteranService = veteranService;
        }

        public IEnumerable<Veteran> Get()
        {
            return _veteranService.GetAll();
        }


        public Veteran Get(int id)
        {
            return _veteranService.GetById(id);
        }


        public void Post([FromBody]VeteranBindingModel veteran)
        {
            var veteranModel = AutoMapper.Mapper.Map<Veteran>(veteran);

            _veteranService.Add(veteranModel);

        }


        public string Put(int id, [FromBody]string value)
        {
            return "value";
        }


        public void Delete(int id)
        {
        }
    }
}
