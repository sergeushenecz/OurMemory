using System.Collections.Generic;
using System.Web.Http;
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

    


        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        public string Get(int id)
        {
            return "value";
        }


        public void Post([FromBody]VeteranBindingModel veteran)
        {
            var veteranModel = AutoMapper.Mapper.Map<Veteran>(veteran);

            _veteranService.CreateVeteran(veteranModel);

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
