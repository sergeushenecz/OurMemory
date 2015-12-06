using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Models.Veteran;

namespace OurMemory
{
    public class AutoMapperConfig
    {

        public void Initialization()
        {
            ConfigurateModelToBindingModel();
            ConfigurateBindingModelToModel();
        }

        private void ConfigurateModelToBindingModel()
        {
            AutoMapper.Mapper.CreateMap<ImageVeteran, ImageVeteranBindingModel>()
                .ForMember(x=>x.ImageOriginal,opt=>opt.MapFrom(x=>x.ImageOriginal))
                .ForMember(x=>x.ThumbnailImage,opt=>opt.MapFrom(x=>x.ThumbnailImage));

           

            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>();


        }

        private void ConfigurateBindingModelToModel()
        {
            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>();
        }
    }
}