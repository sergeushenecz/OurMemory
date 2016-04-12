using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service.Services
{
    public class ImageService : IImageService
    {
        public Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        public byte[] ImageToByte(Image img)
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

        public List<ImageVeteranBindingModel> SaveImages(MultipartMemoryStreamProvider provider, string root, ref  Dictionary<string, string> errors)
        {
            List<ImageVeteranBindingModel> imageFilesVeterans = new List<ImageVeteranBindingModel>();

            foreach (HttpContent file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                if (!ValidateImageType(filename))
                {
                    if (errors != null)
                        errors.Add(filename, "Cannot supported type file");
                    continue;
                }

                var filesVeteran = new ImageVeteranBindingModel();

                filename = Guid.NewGuid() + ".jpg";
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

                    Bitmap resizeImage = (Bitmap)ResizeImage(image, new Size(200, 200));

                    var imageToByte = ImageToByte(resizeImage);

                    fs.Write(imageToByte, 0, imageToByte.Length);
                }

            }

            return imageFilesVeterans;
        }

        private bool ValidateImageType(string filename)
        {
            var extensions = new[] { "png", "bmp", "jpg" };
           
            if (filename.IndexOf('.') < 0)
                return false;

            var extension = filename.Split('.').Last();

            return extensions.Any(i => i.Equals(extension));
        }
    }



}