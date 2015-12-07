using System;
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

            veteran.Views++;

            _veteranService.SaveVeteran();

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
                var imageFilesVeterans = _imageService.SaveFiles(provider, root);


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
