using System;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class ArticleViewModel : BaseBindingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public int Views { get; set; }
        public string ArticleImageUrl { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
    }
}