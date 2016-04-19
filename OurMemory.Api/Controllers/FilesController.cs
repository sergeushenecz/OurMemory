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
    /// <summary>
    /// Work with files 
    /// </summary>
    public class FilesController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly VeteranService _veteranService;

        readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Work with files 
        /// </summary>
        /// <param name="imageService"></param>
        /// <param name="veteranService"></param>
        /// <param name="userManager"></param>
        public FilesController(IImageService imageService, VeteranService veteranService, ApplicationUserManager userManager)
            : base(userManager)
        {
            _imageService = imageService;
            _veteranService = veteranService;
        }

        /// <summary>
        /// Get report excell file 
        /// </summary>
        /// <param name="searchVeteranModel"></param>
        /// <returns></returns>
        [Route("api/files")]
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

        /// <summary>
        /// Upload a image files
        /// </summary>
        /// <returns></returns>
        [Route("api/files")]
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
        /// Upload excell file on server
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
