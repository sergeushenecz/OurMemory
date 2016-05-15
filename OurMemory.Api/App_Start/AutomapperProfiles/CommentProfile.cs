using AutoMapper;
using OurMemory.Common.Extention;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.AutomapperProfiles
{
    public class CommentProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.CreatedDateTime, y => y.MapFrom(x => x.CreatedDateTime));
            base.Configure();
        }
    }
}