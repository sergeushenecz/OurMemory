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
            Mapper.CreateMap<ArticleBindingModel, Article>().ForMember(dist => dist.Image, opt => opt.MapFrom(x => x.Image.ToRelativePath()));

            Mapper.CreateMap<Article, ArticleViewModel>()
                .ForMember(dist => dist.Image, opt => opt.MapFrom(x => x.Image.ToAbsolutPath()))
                .ForMember(x => x.CreatedDateTime, x => x.MapFrom(article => article.CreatedDateTime.ToString("yyyy-MM-dd hh:mm")));

            base.Configure();
        }
    }
}