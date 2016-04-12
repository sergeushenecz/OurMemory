using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using LinqToExcel;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Interfaces;

namespace OurMemory.Controllers
{
    public class FileUploadController : BaseController
    {
        private readonly IImageService _imageService;

        public FileUploadController(IImageService imageService, ApplicationUserManager userManager)
            : base(userManager)
        {
            _imageService = imageService;
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

            List<ImageVeteranBindingModel> imageFilesVeterans = _imageService.SaveImages(provider, root, ref errors);

            var imageVeterans = imageFilesVeterans.Select(file => new ImageVeteranBindingModel
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

            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Files/"), fileName);

            httpPostedFile.SaveAs(path);

            var excel = new ExcelQueryFactory(path);
            var indianaCompanies = from c in excel.Worksheet<Company>()
                                   select c;

            return Ok();
        }
    }

    public class Company
    {
        public string Header { get; set; }
    }
}
