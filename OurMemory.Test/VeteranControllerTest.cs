using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
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
        private Fixture _fixture;
        public VeteranControllerTest()
        {
            _fixture = new Fixture();
        }


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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

            var veteranService = new Mock<IVeteranService>();

            veteranService.Setup(x => x.Add(It.IsAny<Veteran>()));

            VeteranController veteranController = new VeteranController(veteranService.Object, null);
            veteranController.Request = new HttpRequestMessage();
            veteranController.Configuration = new HttpConfiguration();
            IHttpActionResult httpActionResult = veteranController.Post(veteranBindingModel);

            var okNegotiatedContentResult = httpActionResult as OkNegotiatedContentResult<VeteranBindingModel>;

            Assert.IsNotNull(okNegotiatedContentResult);
        }
        [TestMethod]
        public void GetAllVeterans()
        {
            //Arrange
            Initialize();
            var veteranService = new Mock<IVeteranService>();
            var veterans = _fixture.Create<List<VeteranBindingModel>>();
            IList<Veteran> veteran = Mapper.Map<IList<VeteranBindingModel>, IList<Veteran>>(veterans);

            veteranService.Setup(x => x.GetAll()).Returns(veteran);


            VeteranController veteranController = new VeteranController(veteranService.Object, null)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            IEnumerable<VeteranBindingModel> veteranBindingModels = veteranController.Get();

            Assert.IsNotNull(veteranBindingModels);

        }
    }
}
