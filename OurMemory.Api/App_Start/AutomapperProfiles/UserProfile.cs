using AutoMapper;
using OurMemory.Common.Extention;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            Mapper.CreateMap<User, UserViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.UserName));
            Mapper.CreateMap<User, UserViewModel>().ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Image.ToAbsolutPath()));
        }
    }
}