using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using OurMemory.Domain.DtoModel;

namespace OurMemory.Service.Interfaces
{
    public interface IImageService
    {
        Bitmap ResizeImage(Image image, int width, int height);
        byte[] ImageToByte(Image img);

        List<ImageVeteranBindingModel> SaveFiles(MultipartMemoryStreamProvider provider, string root);
    }
}