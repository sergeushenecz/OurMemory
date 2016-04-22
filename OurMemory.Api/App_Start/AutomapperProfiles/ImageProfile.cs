using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.App_Start.AutomapperProfiles
{
    public class ImageProfile:Profile
    {

        protected override  void Configure()
        {
            AutoMapper.Mapper.CreateMap<Image, ImageReference>();
            Mapper.CreateMap<ImageReference, Image>();

            base.Configure();
        }

     

        private static string GetDomain
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority; }
        }
    }
}