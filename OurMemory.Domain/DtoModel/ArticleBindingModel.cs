using System;

namespace OurMemory.Domain.DtoModel
{
    public class ArticleBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageArticleUrl { get; set; }
    }
}