using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using OurMemory.Data.Infrastructure;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using ImageReference = OurMemory.Domain.DtoModel.ImageReference;

namespace OurMemory.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Domain.Entities.Image> _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImageService()
        {
        }

        public ImageService(IRepository<Domain.Entities.Image> imageRepository, IUnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

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

        public List<ImageReference> SaveImages(MultipartMemoryStreamProvider provider, string root, ref Dictionary<string, string> errors)
        {
            List<ImageReference> imageFilesVeterans = new List<ImageReference>();

            foreach (HttpContent file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                if (!ValidateImageType(filename))
                {
                    if (errors != null)
                        errors.Add(filename, "Cannot supported type file");
                    continue;
                }

                var filesVeteran = new ImageReference();

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

        private void SaveImageOriginalAndthumpImage(byte[] imageBytes, string root, string originalFileName,
            string thumpImageFilename)
        {
            using (FileStream fs = new FileStream(root + originalFileName, FileMode.Create))
            {
                fs.Write(imageBytes, 0, imageBytes.Length);
            }

            using (
                FileStream fs = new FileStream(root + thumpImageFilename,
                    FileMode.Create))
            {
                MemoryStream ms = new MemoryStream(imageBytes);
                Image image = Image.FromStream(ms);

                Bitmap resizeImage = (Bitmap)ResizeImage(image, new Size(200, 200));

                var imageToByte = ImageToByte(resizeImage);

                fs.Write(imageToByte, 0, imageToByte.Length);
            }
        }

        private bool ValidateImageType(string filename)
        {
            var extensions = new[] { "png", "bmp", "jpg" };

            if (filename.IndexOf('.') < 0)
                return false;

            var extension = filename.Split('.').Last();

            return extensions.Any(i => i.Equals(extension));
        }

        public ImageReference SaveImage(byte[] imageBytes)
        {
            ImageReference imageReference = new ImageReference();

            string root = HttpContext.Current.Server.MapPath("~/Content/Files/");

//            var imageUrl = HttpContext.Current.Request.ApplicationPath + "Content/Files/";

            string imageUrl = VirtualPathUtility.ToAbsolute("~/Content/Files/");

            var filename = Guid.NewGuid() + ".jpg";
            var thumpImageFilename = Guid.NewGuid() + ".jpg";

            imageReference.ImageOriginal = imageUrl + filename;
            imageReference.ThumbnailImage = imageUrl + thumpImageFilename;

            try
            {
                SaveImageOriginalAndthumpImage(imageBytes, root, filename, thumpImageFilename);
            }
            catch (Exception)
            {

                throw new Exception("Not Save Image");
            }

            return imageReference;
        }

        public void DeleteImages(IEnumerable<Domain.Entities.Image> image)
        {
            var images = image.ToList();

            for (int i = 0; i < images.Count(); i++)
            {
                _imageRepository.Delete(images[i]);
            }

            Save();
        }

        public Image CropImage(Image source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }


    }



}