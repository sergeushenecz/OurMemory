using AutoMapper;
using OurMemory.Common.Extention;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.AutomapperProfiles
{
    public class ArticleProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Article, ArticleBindingModel>();
            Mapper.CreateMap<ArticleBindingModel, Article>().ForMember(dist => dist.ArticleImageUrl, opt => opt.MapFrom(x => x.ArticleImageUrl.ToRelativePath()));

            Mapper.CreateMap<Article, ArticleViewModel>()
                .ForMember(dist => dist.ArticleImageUrl, opt => opt.MapFrom(x => x.ArticleImageUrl.ToAbsolutPath()));

            base.Configure();
        }
    }
}