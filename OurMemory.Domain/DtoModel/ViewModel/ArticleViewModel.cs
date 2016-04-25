using System;

namespace OurMemory.Domain.DtoModel.ViewModel
{
    public class ArticleViewModel: BaseBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public Guid UserId { get; set; }
    }
}