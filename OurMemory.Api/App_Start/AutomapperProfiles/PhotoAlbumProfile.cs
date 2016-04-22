using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.DtoModel.ViewModel;
using OurMemory.Domain.Entities;

namespace OurMemory.AutomapperProfiles
{
    public class PhotoAlbumProfile : Profile
    {

        protected override void Configure()
        {

            Mapper.CreateMap<PhotoAlbum, PhotoAlbumBindingModel>();
            Mapper.CreateMap<PhotoAlbumBindingModel, PhotoAlbum>();


            AutoMapper.Mapper.CreateMap<PhotoAlbum, PhotoAlbumViewModel>().AfterMap((photoAlbum, photoAlbumViewModel) =>
            {
                for (int i = 0; i < photoAlbum.Images.Count; i++)
                {
                    photoAlbumViewModel.Images.ToList()[i].ImageOriginal =
                        photoAlbum.Images.ToList()[i].ImageOriginal.Insert(0, GetDomain);

                    photoAlbumViewModel.Images.ToList()[i].ThumbnailImage =
                        photoAlbum.Images.ToList()[i].ThumbnailImage.Insert(0, GetDomain);
                }

            });

            Mapper.CreateMap<PhotoAlbumBindingModel, PhotoAlbum>()
              .AfterMap((photoAlbumBindingModel, photoAlbum) =>
              {
                  for (int i = 0; i < photoAlbum.Images.Count; i++)
                  {
                      photoAlbum.Images.ToList()[i].ImageOriginal = photoAlbumBindingModel.Images.ToList()[i].ImageOriginal.Replace(GetDomain, "");
                      photoAlbum.Images.ToList()[i].ThumbnailImage = photoAlbumBindingModel.Images.ToList()[i].ThumbnailImage.Replace(GetDomain, "");
                  }

              });


            Mapper.CreateMap<VeteranBindingModel, Veteran>();



            base.Configure();
        }

        private static string GetDomain
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority; }
        }
    }
}