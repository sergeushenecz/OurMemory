using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class CommentViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "createdDateTime")]
        public string CreatedDateTime { get; set; }
    }
}
