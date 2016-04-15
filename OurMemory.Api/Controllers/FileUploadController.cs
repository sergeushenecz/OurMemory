using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using LinqToExcel;
using Microsoft.AspNet.Identity;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using OurMemory.Service.Parsers;
using OurMemory.Service.Services;
using ImageReference = OurMemory.Domain.DtoModel.ImageReference;

namespace OurMemory.Controllers
{
    public class FileUploadController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly VeteranService _veteranService;

        public FileUploadController(IImageService imageService, VeteranService veteranService, ApplicationUserManager userManager)
            : base(userManager)
        {
            _imageService = imageService;
            _veteranService = veteranService;
        }

        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/Files/");
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);



            var imageUrl = HttpContext.Current.Request.Url.Scheme +
                "://"
                + HttpContext.Current.Request.Url.Authority
                + HttpContext.Current.Request.ApplicationPath + "Content/Files";


            Dictionary<string, string> errors = new Dictionary<string, string>();

            List<ImageReference> imageFilesVeterans = _imageService.SaveImages(provider, root, ref errors);

            var imageVeterans = imageFilesVeterans.Select(file => new ImageReference
            {
                ImageOriginal = imageUrl + @"/" + file.ImageOriginal,
                ThumbnailImage = imageUrl + @"/" + file.ThumbnailImage
            }).ToList();

            return Ok(new
            {
                Images = imageVeterans,
                Errors = errors
            });

        }

        [Route("api/fileUpload/uploadExcell")]
        public IHttpActionResult UploadExcellFilesPost()
        {
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            var fileName = httpPostedFile.FileName;
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);
            httpPostedFile.SaveAs(path);

            ExcellParser excellParser = new ExcellParser(path);
            var veteranMappings = excellParser.GetVeterans();


            foreach (var veteranMapping in veteranMappings)
            {
                var veteranBindingModel = Mapper.Map<VeteranMapping, VeteranBindingModel>(veteranMapping);

                var listParsedUrls = UrlParser.Parse(veteranMapping.UrlImages);
                veteranBindingModel.Images = UrlParser.DownloadFromUrls(listParsedUrls);

                var veteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);
                veteran.User = UserManager.FindById(User.Identity.GetUserId());

                _veteranService.Add(veteran);
            }


            return Ok();
        }
    }
}
