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
            Mapper.CreateMap<ArticleBindingModel, Article>().ForMember(dist => dist.ImageArticleUrl, opt => opt.MapFrom(x => x.ImageArticleUrl.ToRelativePath()));

            Mapper.CreateMap<Article, ArticleViewModel>()
                .ForMember(dist => dist.ImageArticleUrl, opt => opt.MapFrom(x => x.ImageArticleUrl.ToAbsolutPath()))
                .ForMember(dist => dist.UpdatedDateTime,
                    opt => opt.MapFrom(x => x.UpdatedDateTime.ToString("yyyy-MM-dd")))
                .ForMember(dist => dist.UserId, opt => opt.MapFrom(x => x.User.Id));

            base.Configure();
        }
    }
}