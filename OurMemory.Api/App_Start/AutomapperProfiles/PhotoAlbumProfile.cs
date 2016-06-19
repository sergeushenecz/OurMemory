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
            Mapper.CreateMap<PhotoAlbum, PhotoAlbum>().ForMember(x=>x.User,opt=>opt.Ignore());

            AutoMapper.Mapper.CreateMap<PhotoAlbum, PhotoAlbumViewModel>()
                .ForMember(dist => dist.CountPhoto, opt => opt.MapFrom(x => x.Images.Count))
                .ForMember(dist => dist.Image, opt => opt.MapFrom(x => x.Image.ToAbsolutPath()));

            Mapper.CreateMap<PhotoAlbumBindingModel, PhotoAlbum>()
              .ForMember(dist => dist.Image, opt => opt.MapFrom(x => x.Image.ToRelativePath()))
              .ForMember(x=>x.User,opt=>opt.Ignore())
              .AfterMap((photoAlbumBindingModel, photoAlbum) =>
              {
                  for (int i = 0; i < photoAlbum.Images.Count; i++)
                  {
                      photoAlbum.Images.ToList()[i].ImageOriginal = photoAlbumBindingModel.Images.ToList()[i].ImageOriginal.ToRelativePath();
                      photoAlbum.Images.ToList()[i].ImageThumbnail = photoAlbumBindingModel.Images.ToList()[i].ThumbnailImage.ToRelativePath();
                  }

              });

            Mapper.CreateMap<PhotoAlbum, PhotoAlbumWithImagesViewModel>()
              .ForMember(dist => dist.CountPhoto, opt => opt.MapFrom(x => x.Images.Count))
              .ForMember(dist => dist.Image, opt => opt.MapFrom(x => x.Image.ToAbsolutPath()))
              .AfterMap((album, photoAlbumWithImages) =>
              {
                  for (int i = 0; i < album.Images.Count; i++)
                  {
                      photoAlbumWithImages.Images.ToList()[i].ImageOriginal = album.Images.ToList()[i].ImageOriginal.ToAbsolutPath();

                      photoAlbumWithImages.Images.ToList()[i].ThumbnailImage = album.Images.ToList()[i].ImageThumbnail.ToAbsolutPath();
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