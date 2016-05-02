using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using AutoMapper;
using OurMemory.AutomapperProfiles;
using OurMemory.AutoMapperConverter;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;
using OurMemory.Models;
using OurMemory.Service.Model;
using ImageReference = OurMemory.Domain.DtoModel.ImageReference;

namespace OurMemory
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig()
        {
           
        }

        public void Initialize()
        {
            AutoMapper.Mapper.AddProfile<VeteranProfile>();
            AutoMapper.Mapper.AddProfile<ImageProfile>();
            AutoMapper.Mapper.AddProfile<CommentProfile>();
            AutoMapper.Mapper.AddProfile<ArticleProfile>();
            AutoMapper.Mapper.AddProfile<PhotoAlbumProfile>();
            AutoMapper.Mapper.AddProfile<UserProfile>();
        }
    }
}