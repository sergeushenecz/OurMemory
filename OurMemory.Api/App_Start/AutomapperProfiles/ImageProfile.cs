using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;

namespace OurMemory.App_Start.AutomapperProfiles
{
    public class ImageProfile:Profile
    {

        protected override  void Configure()
        {
            AutoMapper.Mapper.CreateMap<ImageVeteran, ImageReference>();
            Mapper.CreateMap<ImageReference, ImageVeteran>();

            base.Configure();
        }
    }
}