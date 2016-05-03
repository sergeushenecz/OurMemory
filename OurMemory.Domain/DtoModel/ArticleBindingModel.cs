using System;

namespace OurMemory.Domain.DtoModel
{
    public class ArticleBindingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Image { get; set; }
    }
}