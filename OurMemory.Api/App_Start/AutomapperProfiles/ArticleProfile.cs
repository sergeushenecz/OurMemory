using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;

namespace OurMemory.App_Start.AutomapperProfiles
{
    public class ArticleProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<Article, ArticleBindingModel>();
            Mapper.CreateMap<ArticleBindingModel, Article>();

            base.Configure();
        }
    }
}