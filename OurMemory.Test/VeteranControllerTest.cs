using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using OurMemory;
using OurMemory.Controllers;
using OurMemory.Domain.DtoModel;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;
using Ploeh.AutoFixture;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for VeteranControllerTest
    /// </summary>
    [TestClass]
    public class VeteranControllerTest
    {
        private readonly Fixture _fixture;
        public VeteranControllerTest()
        {
            _fixture = new Fixture();
        }

        public void Initialize()
        {
            AutoMapper.Mapper.CreateMap<ImageVeteran, ImageVeteranBindingModel>();
            AutoMapper.Mapper.CreateMap<Veteran, VeteranBindingModel>();
            AutoMapper.Mapper.CreateMap<ImageVeteranBindingModel, ImageVeteran>();
            AutoMapper.Mapper.CreateMap<VeteranBindingModel, Veteran>();
        }

        [TestMethod]
        public void CreateVeteran()
        {
            Initialize();
            //Arrange
            var veteranBindingModel = _fixture.Create<VeteranBindingModel>();

            var user = new User()
            {
                Id = "1",
            };


            var veteranService = new Mock<IVeteranService>();
            var userService = new Mock<IUserService>();
            var fakeHttpContext = new Mock<HttpContextBase>();
            GenericIdentity fakeIdentity = new GenericIdentity("User");
            GenericPrincipal principal = new GenericPrincipal(fakeIdentity, null);

            var claim = new Claim("test", user.Id);
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);




            fakeHttpContext.Setup(x => x.User).Returns(principal);
            veteranService.Setup(x => x.Add(It.IsAny<Veteran>()));

            userService.Setup(x => x.GetById(user.Id)).Returns(user);

            VeteranController veteranController = new VeteranController(veteranService.Object, userService.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration(),
                User = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity)
            };

            veteranController.User.Identity.GetUserId(); //returns "IdOfYourChoosing"

            IHttpActionResult httpActionResult = veteranController.Post(veteranBindingModel);

            var okNegotiatedContentResult = httpActionResult as OkNegotiatedContentResult<VeteranBindingModel>;

            Assert.IsNotNull(okNegotiatedContentResult);
        }
//        [TestMethod]
//        public void GetAllVeterans()
//        {
//            //Arrange
//            Initialize();
//            var veteranService = new Mock<IVeteranService>();
//            var veterans = _fixture.Create<List<VeteranBindingModel>>();
//            IList<Veteran> veteran = Mapper.Map<IList<VeteranBindingModel>, IList<Veteran>>(veterans);
//
//            veteranService.Setup(x => x.GetAll()).Returns(veteran);
//
//            VeteranController veteranController = new VeteranController(veteranService.Object, null)
//            {
//                Request = new HttpRequestMessage(),
//                Configuration = new HttpConfiguration()
//            };
//
//            IEnumerable<VeteranBindingModel> veteranBindingModels = veteranController.Get();
//
//            Assert.IsNotNull(veteranBindingModels);
//
//        }
    }
}
