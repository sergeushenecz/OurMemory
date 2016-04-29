using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Specification
{
    public class ArticleSpecification : SpecificationBase<Article>
    {
        public Specification<Article> KeyWord(SearchArticleModel searchArticleModel)
        {
            return GetByName(searchArticleModel.Name).And(!IsDeleted());
        }

        public Specification<Article> GetByName(string name)
        {
            return name == null ? Empty() : new Specification<Article>(x => x.Title.Contains(name));

        }
    }
}