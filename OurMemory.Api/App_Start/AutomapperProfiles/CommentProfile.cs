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
                .ForMember(x => x.UserId, y => y.MapFrom(x => x.User.Id))
                .ForMember(x => x.UserName, y => y.MapFrom(x => x.User.UserName))
                .ForMember(x => x.ImageUser, y => y.MapFrom(x => x.User.Image.ToAbsolutPath()))
                .ForMember(x => x.CreatedDateTime, y => y.MapFrom(x => x.CreatedDateTime));
            base.Configure();
        }
    }
}