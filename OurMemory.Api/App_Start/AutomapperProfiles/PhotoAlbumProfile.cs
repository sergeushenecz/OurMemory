using System.Linq;
using System.Web;
using AutoMapper;
using OurMemory.Common.Extention;
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
            Mapper.CreateMap<PhotoAlbum, PhotoAlbum>();

            AutoMapper.Mapper.CreateMap<PhotoAlbum, PhotoAlbumViewModel>()
                .ForMember(dist => dist.CountPhoto, opt => opt.MapFrom(x => x.Images.Count))
                .ForMember(dist => dist.ImageAlbumUrl, opt => opt.MapFrom(x => x.ImageAlbumUrl.ToAbsolutPath()));

            Mapper.CreateMap<PhotoAlbumBindingModel, PhotoAlbum>()
              .ForMember(dist => dist.ImageAlbumUrl, opt => opt.MapFrom(x => x.ImageAlbumUrl.ToRelativePath()))
              .AfterMap((photoAlbumBindingModel, photoAlbum) =>
              {
                  for (int i = 0; i < photoAlbum.Images.Count; i++)
                  {
                      photoAlbum.Images.ToList()[i].ImageOriginal = photoAlbumBindingModel.Images.ToList()[i].ImageOriginal.ToRelativePath();
                      photoAlbum.Images.ToList()[i].ThumbnailImage = photoAlbumBindingModel.Images.ToList()[i].ThumbnailImage.ToRelativePath();
                  }

              });

            Mapper.CreateMap<PhotoAlbum, PhotoAlbumWithImagesViewModel>()
              .ForMember(dist => dist.CountPhoto, opt => opt.MapFrom(x => x.Images.Count))
              .ForMember(dist => dist.ImageAlbumUrl, opt => opt.MapFrom(x => x.ImageAlbumUrl.ToAbsolutPath()))
              .AfterMap((album, photoAlbumWithImages) =>
              {
                  for (int i = 0; i < album.Images.Count; i++)
                  {
                      if (album.Images.ToList()[i].ImageOriginal != null)
                          photoAlbumWithImages.Images.ToList()[i].ImageOriginal = album.Images.ToList()[i].ImageOriginal.ToAbsolutPath();

                      if (album.Images.ToList()[i].ThumbnailImage != null)
                          photoAlbumWithImages.Images.ToList()[i].ThumbnailImage = album.Images.ToList()[i].ThumbnailImage.ToAbsolutPath();
                  }

              });


            base.Configure();
        }

        private static string GetDomain
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority; }
        }
    }
}