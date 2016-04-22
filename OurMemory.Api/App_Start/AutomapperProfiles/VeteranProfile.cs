using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.AutomapperProfiles
{
    public class VeteranProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<Veteran, VeteranViewModel>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.FirstName
                                                                         + " " + x.LastName
                                                                         + " " + x.MiddleName))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.User.Id))
               .AfterMap((veteranImages, veteranBindingImages) =>
               {
                   for (int i = 0; i < veteranImages.Images.Count; i++)
                   {
                       veteranBindingImages.Images.ToList()[i].ImageOriginal = veteranImages.Images.ToList()[i].ImageOriginal.Insert(0, GetDomain);

                       veteranBindingImages.Images.ToList()[i].ThumbnailImage = veteranImages.Images.ToList()[i].ThumbnailImage.Insert(0, GetDomain);
                   }

               });


            Mapper.CreateMap<Veteran, Veteran>().ForMember(dest => dest.User, opt => opt.Ignore());


            Mapper.CreateMap<VeteranBindingModel, Veteran>();

            Mapper.CreateMap<VeteranMapping, VeteranBindingModel>();

            Mapper.CreateMap<Veteran, VeteranMapping>()
                .ForMember(dest => dest.UrlImages, opt => opt.MapFrom(src => string.Join(", ", src.Images
                                                    .Select(x => GetDomain + x.ImageOriginal))));


            Mapper.CreateMap<VeteranBindingModel, Veteran>()
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