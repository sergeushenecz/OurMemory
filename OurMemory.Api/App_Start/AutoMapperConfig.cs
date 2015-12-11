using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;

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
            AutoMapper.Mapper.CreateMap<ImageVeteran, ImageVeteranBindingModel>();

            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.FirstName
                                                                          + " " + x.LastName
                                                                          + " " + x.MiddleName))
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.User.Id));



        }

        private void ConfigurateBindingModelToModel()
        {
            AutoMapper.Mapper.CreateMap<ImageVeteranBindingModel, ImageVeteran>();

            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>();
        }
    }
}