﻿using AutoMapper;
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
              .ForMember(x => x.UserName, y => y.MapFrom(x => x.User.UserName));
            base.Configure();
        }
    }
}