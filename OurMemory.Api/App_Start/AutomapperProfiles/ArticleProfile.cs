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
                .ForMember(dist => dist.ArticleImageUrl, opt => opt.MapFrom(x => x.ArticleImageUrl.ToAbsolutPath()))
                .ForMember(dist => dist.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(dist => dist.UserImageUrl, opt => opt.MapFrom(x => x.User.UserImageUrl.ToAbsolutPath()))
                .ForMember(dist => dist.UserId, opt => opt.MapFrom(x => x.User.Id));

            base.Configure();
        }
    }
}