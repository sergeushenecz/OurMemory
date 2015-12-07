using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class ImageService : IImageService
    {
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public  byte[] ImageToByte(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public  List<ImageVeteranBindingModel> SaveImages(MultipartMemoryStreamProvider provider, string root)
        {

            List<ImageVeteranBindingModel> imageFilesVeterans = new List<ImageVeteranBindingModel>();

            foreach (var file in provider.Contents)
            {
                if (file.Headers.ContentDisposition.Name.Trim('\"').Contains("file"))
                {
                    var filesVeteran = new ImageVeteranBindingModel();

                    var filename = Guid.NewGuid() + ".jpg";
                    var thumpImageFilename = Guid.NewGuid() + ".jpg";


                    filesVeteran.ImageOriginal = filename;
                    filesVeteran.ThumbnailImage = thumpImageFilename;

                    imageFilesVeterans.Add(filesVeteran);

                    byte[] fileArray = file.ReadAsByteArrayAsync().Result;

                    using (FileStream fs = new FileStream(root + filename, FileMode.Create))
                    {
                        fs.Write(fileArray, 0, fileArray.Length);
                    }

                    using (
                        FileStream fs = new FileStream(root + thumpImageFilename,
                            FileMode.Create))
                    {
                        MemoryStream ms = new MemoryStream(fileArray);
                        Image image = Image.FromStream(ms);

                        Bitmap resizeImage = ResizeImage(image, 200, 200);

                        var imageToByte = ImageToByte(resizeImage);

                        fs.Write(imageToByte, 0, imageToByte.Length);
                    }
                }
            }

            return imageFilesVeterans;
        }
    }
}