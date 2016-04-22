using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;

namespace OurMemory.AutomapperProfiles
{
    public class ArticleProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Article, ArticleBindingModel>();
            Mapper.CreateMap<ArticleBindingModel, Article>();

            base.Configure();
        }
    }
}