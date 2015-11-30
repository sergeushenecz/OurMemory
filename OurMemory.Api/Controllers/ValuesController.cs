using System.Collections.Generic;
using System.Web.Http;

namespace OurMemory.Controllers
{
  
    /// <summary>
    /// Values COntroller
    /// </summary>
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public string Put(int id, [FromBody]string value)
        {
            return "value";
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
