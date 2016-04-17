using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public async Task<IHttpActionResult> UploadExcellFilesPost()
        {
            string path = null;

            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents[0];

            var filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            filename += Guid.NewGuid().ToString() + ".xlsx";


            byte[] fileArray = file.ReadAsByteArrayAsync().Result;

            path = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), filename);

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(fileArray, 0, fileArray.Length);
            }

            try
            {
                ExcellParser excellParser = new ExcellParser(path);
                var veteranMappings = excellParser.GetVeterans();


                foreach (var veteranMapping in veteranMappings)
                {
                    var veteranBindingModel = Mapper.Map<VeteranMapping, VeteranBindingModel>(veteranMapping);

                    var listParsedUrls = UrlParser.Parse(veteranMapping.UrlImages);
                    veteranBindingModel.Images = UrlParser.DownloadFromUrls(listParsedUrls);

                    var veteran = Mapper.Map<VeteranBindingModel, Veteran>(veteranBindingModel);
                    veteran.User = UserManager.FindById(User.Identity.GetUserId());

                    var googleMapsService = new GoogleMapsService(string.Empty);
                    var latLng = googleMapsService.GetLatLng(veteran.BirthPlace);
                    veteran.Latitude = latLng.Latitude;
                    veteran.Longitude = latLng.Longitude;

                    _veteranService.Add(veteran);
                }
            }
            catch (Exception exception)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Problems with parsing the file"),
                    ReasonPhrase = "File Not Parsed"
                };

                throw new HttpResponseException(resp);
            }
            finally
            {
                if (File.Exists(path))
                {

                    File.Delete(path);
                }

            }

            return Ok();
        }
    }
}
