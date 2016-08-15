using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeSite.Controllers;
using System.Web.Mvc;
using HomeSiteDomain.Models.About;
using HomeSite.Models.Repository;
using HomeSite.Tests.ContextMocks;

namespace HomeSite.Tests
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void Testing_Index()
        {
            //arrange
            AboutMeController controller = new AboutMeController(new MockAboutMeRepository());

            //act
            ViewResult result = controller.Index() as ViewResult;
           
            //assert
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void Returns_Profile()
        {
            //arrange
            AboutMeController controller = new AboutMeController(new MockAboutMeRepository());

            //act
            ViewResult result = (ViewResult)controller.Index();
            Profile profile = (Profile)result.Model;
            System.Type profileType = profile.GetType();
            
            //assert
            Assert.AreEqual(profileType, typeof(Profile));
        }
    }
}
