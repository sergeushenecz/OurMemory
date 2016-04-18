﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using AutoMapper;
using OurMemory.AutoMapperConverter;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Service.Model;
using ImageReference = OurMemory.Domain.DtoModel.ImageReference;

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
            AutoMapper.Mapper.CreateMap<ImageVeteran, ImageReference>();

            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.FirstName
                                                                          + " " + x.LastName
                                                                          + " " + x.MiddleName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.User.Id));


            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>()
                .AfterMap((veteranImages, veteranBindingImages) =>
                {
                    for (int i = 0; i < veteranImages.Images.Count; i++)
                    {
                        veteranBindingImages.Images.ToList()[i].ImageOriginal = veteranImages.Images.ToList()[i].ImageOriginal.Insert(0, GetDomain);

                        veteranBindingImages.Images.ToList()[i].ThumbnailImage = veteranImages.Images.ToList()[i].ThumbnailImage.Insert(0, GetDomain);
                    }

                });

            Mapper.CreateMap<Veteran, Veteran>().ForMember(dest => dest.User, opt => opt.Ignore());
            Mapper.CreateMap<VeteranMapping, VeteranBindingModel>();
            Mapper.CreateMap<Veteran, VeteranMapping>()
                .ForMember(dest => dest.UrlImages, opt => opt.MapFrom(src => string.Join(", ", src.Images
                                                    .Select(x => GetDomain + x.ImageOriginal))));

        }

        private void ConfigurateBindingModelToModel()
        {
            AutoMapper.Mapper.CreateMap<ImageReference, ImageVeteran>();
            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>();
            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>()
            .AfterMap((veteranBindingImages, veteranImages) =>
            {
                for (int i = 0; i < veteranImages.Images.Count; i++)
                {
                    veteranImages.Images.ToList()[i].ImageOriginal = veteranBindingImages.Images.ToList()[i].ImageOriginal.Replace(GetDomain, "");
                    veteranImages.Images.ToList()[i].ThumbnailImage = veteranBindingImages.Images.ToList()[i].ThumbnailImage.Replace(GetDomain, "");
                }

            });
        }

        private static string GetDomain
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority; }
        }
    }
}