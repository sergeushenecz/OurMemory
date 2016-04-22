using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.App_Start.AutomapperProfiles
{
    public class CommentProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Comment, CommentViewModel>()
              .ForMember(x => x.UserId, y => y.MapFrom(x => x.User.Id))
              .ForMember(x => x.UserName, y => y.MapFrom(x => x.User.UserName));
            base.Configure();
        }
    }
}