using System.Drawing;

namespace OurMemory.Service.Interfaces
{
    public interface IImageService
    {
        Bitmap ResizeImage(Image image, int width, int height);
        byte[] ImageToByte(Image img);
    }
}