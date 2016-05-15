using Newtonsoft.Json;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class CommentViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "user")]
        public UserViewModel User { get; set; }
        [JsonProperty(PropertyName = "createdDateTime")]
        public string CreatedDateTime { get; set; }
    }
}
