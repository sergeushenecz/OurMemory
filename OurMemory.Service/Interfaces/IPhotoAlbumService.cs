using System.Collections.Generic;
using System.Linq;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Interfaces
{
    public interface IPhotoAlbumService
    {
        void Add(PhotoAlbum veteran);
        IEnumerable<PhotoAlbum> GetAll(bool withImages =false);
        PhotoAlbum GetById(int id);

        List<Image> GetPhotoAlbums(int idAlbum);

        void UpdatePhotoAlbum(PhotoAlbum veteran);
        IQueryable<PhotoAlbum> SearchPhotoAlbum(SearchPhotoAlbumModel searchVeteranModel);
        void SavePhotoAlbum();
    }
}