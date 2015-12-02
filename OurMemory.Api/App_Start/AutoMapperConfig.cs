using AutoMapper;
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
            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>();
        }

        private void ConfigurateBindingModelToModel()
        {
            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>();
        }
    }
}