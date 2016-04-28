using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using OurMemory.Domain.DtoModel;

namespace OurMemory.Service.Interfaces
{
    public interface IImageService
    {
        Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true);
        byte[] ImageToByte(Image img);

        List<ImageReference> SaveImages(MultipartMemoryStreamProvider provider, string root, ref Dictionary<string, string> errors);
        void DeleteImages(IEnumerable<Domain.Entities.Image> images);
        Image CropImage(Image source, Rectangle section);
    }
}