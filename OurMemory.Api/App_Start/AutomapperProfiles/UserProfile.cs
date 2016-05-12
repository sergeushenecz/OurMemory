using AutoMapper;
using OurMemory.Common.Extention;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;

namespace OurMemory.AutomapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
     
            Mapper.CreateMap<UserInfoBindingModel, User>().ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Image.ToRelativePath()));
            Mapper.CreateMap<User, UserViewModel>().ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.Image.ToAbsolutPath()));
        }
    }
}