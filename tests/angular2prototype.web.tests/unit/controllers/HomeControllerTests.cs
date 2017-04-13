using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace angular2prototype.web.tests.unit.controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Should_Return_Correct_View()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNull(result.ViewName);
        }
    }
}
