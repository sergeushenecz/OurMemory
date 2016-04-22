using System.Runtime.InteropServices.WindowsRuntime;
using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Specification
{
    public class PhotoAlbumSpecification : SpecificationBase<PhotoAlbum>
    {
        public PhotoAlbumSpecification()
        {
        }


        public Specification<PhotoAlbum> KeyWord(SearchPhotoAlbumModel searchPhotoAlbumModel)
        {
            var specification = searchPhotoAlbumModel.Name != null ? new Specification<PhotoAlbum>(x => x.Name.Contains(searchPhotoAlbumModel.Name)) :
                Empty();


            return specification.And(!IsDeleted());
        }
    }
}
