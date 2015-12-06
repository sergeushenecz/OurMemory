﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Controllers
{
    public class VeteranController : ApiController
    {
        private readonly IVeteranService _veteranService;
        private readonly IImageService _imageService;
        private readonly IFormService<VeteranBindingModel> _formService;
        public VeteranController(IVeteranService veteranService, IImageService imageService, IFormService<VeteranBindingModel> formService)
        {
            _veteranService = veteranService;
            _imageService = imageService;
            _formService = formService;
        }

        public IEnumerable<VeteranBindingModel> Get()
        {
            IEnumerable<Veteran> veterans = _veteranService.GetAll();

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return veteranBindingModels;
        }

        public VeteranBindingModel Get(int id)
        {
            var veteran = _veteranService.GetById(id);

            var veteranBindingModels = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

            return veteranBindingModels;
        }

        public IEnumerable<VeteranBindingModel> GetVeterans(int pageIndex, int pageise)
        {
            var veterans = _veteranService.GetAll().Skip(pageIndex * pageise).Take(pageise);

            var veteranBindingModels = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranBindingModel>>(veterans);

            return veteranBindingModels;
        }


        public async Task<IHttpActionResult> Post()
        {
            //TODO Refactor
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/Files/");

            var provider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(provider);

            var dictionaryFormData = _formService.GetDataFromForm(HttpContext.Current);

            var imageUrl = HttpContext.Current.Request.Url.Scheme +
                "://"
                + HttpContext.Current.Request.Url.Authority
                + HttpContext.Current.Request.ApplicationPath + "Content/Files";

            var veteran = _formService.MapperDataVeteran(dictionaryFormData);


            Validate(veteran);

            if (ModelState.IsValid)
            {
                List<ImageVeteranBindingModel> imageFilesVeterans = new List<ImageVeteranBindingModel>();

                foreach (var file in provider.Contents)
                {
                    if (file.Headers.ContentDisposition.Name.Trim('\"').Contains("images"))
                    {
                        var filesVeteran = new ImageVeteranBindingModel();

                        var filename = Guid.NewGuid() + ".jpg";
                        var thumpImageFilename = Guid.NewGuid() + ".jpg";


                        filesVeteran.ImageOriginal = filename;
                        filesVeteran.ThumbnailImage = thumpImageFilename;

                        imageFilesVeterans.Add(filesVeteran);

                        byte[] fileArray = await file.ReadAsByteArrayAsync();

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

                            Bitmap resizeImage = _imageService.ResizeImage(image, 200, 200);

                            var imageToByte = _imageService.ImageToByte(resizeImage);

                            fs.Write(imageToByte, 0, imageToByte.Length);
                        }
                    }
                }

                foreach (var file in imageFilesVeterans)
                {
                    ImageVeteran imageVeteran = new ImageVeteran
                    {
                        ImageOriginal = imageUrl + @"/" + file.ImageOriginal,
                        ThumbnailImage = imageUrl + @"/" + file.ThumbnailImage
                    };

                    veteran.Images.Add(imageVeteran);

                    imageVeteran.Veteran = veteran;
                }

                _veteranService.Add(veteran);

                var veteranBindingModel = Mapper.Map<Veteran, VeteranBindingModel>(veteran);

                return Ok(veteranBindingModel);
            }

            return BadRequest(ModelState);
        }

        public string Put(int id, [FromBody]string value)
        {
            return "value";
        }


        public void Delete(int id)
        {
        }
    }
}
