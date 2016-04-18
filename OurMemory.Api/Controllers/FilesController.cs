using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using LinqToExcel;
using Microsoft.AspNet.Identity;
using OurMemory.Common;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using OurMemory.Service.Parsers;
using OurMemory.Service.Services;
using ImageReference = OurMemory.Domain.DtoModel.ImageReference;

namespace OurMemory.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly VeteranService _veteranService;

        log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FilesController(IImageService imageService, VeteranService veteranService, ApplicationUserManager userManager)
            : base(userManager)
        {
            _imageService = imageService;
            _veteranService = veteranService;
        }

        [Route("api/files")]
        /// <summary>
        /// Get excell a  file which contains information about veterans
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetReportExcellFiles([FromUri]SearchVeteranModel searchVeteranModel)
        {
            List<Veteran> searchVeterans = _veteranService.SearchVeterans(searchVeteranModel).ToList();
            var veteranMappings = Mapper.Map<IEnumerable<Veteran>, IEnumerable<VeteranMapping>>(searchVeterans);

            var fileName = ExcellParser.GenerateReport(veteranMappings.ToList());

            var generateAbsolutePath = GenerateAbsolutePath(fileName);

            return Ok(new
            {
                PathToFile = generateAbsolutePath
            });
        }

        [Route("api/files")]
        /// <summary>
        /// Upload image on server
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            string root = HttpContext.Current.Server.MapPath("~/Content/Files/");
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var imageUrl = GenerateAbsolutePath(HttpContext.Current.Request.ApplicationPath + ConfigurationSettingsModule.GetItem("PathImages"));


            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

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

        /// <summary>
        /// Upload Excell File on server
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [Route("api/files/uploadExcell")]
        public async Task<IHttpActionResult> PostUploadExcellFiles()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            string path = null;

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents[0];
            byte[] fileArray = file.ReadAsByteArrayAsync().Result;

            var filename = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            filename += Guid.NewGuid() + ".xlsx";
          

            path = Path.Combine(HttpContext.Current.Server.MapPath("~" + ConfigurationSettingsModule.GetItem("Temp")), filename);

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

                _logger.Error(exception);

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
