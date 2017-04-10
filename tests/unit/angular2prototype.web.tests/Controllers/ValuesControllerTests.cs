using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using angular2prototype.services;
using Moq;
using angular2prototype.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace angular2prototype.web.tests.Controllers
{
	[TestClass]
	public class ValuesControllerTests
	{
		private IValueService _valueService;
		private Mock<IValueService> _serviceMock;
		private Mock<HttpResponse> _responseMock;
		private Mock<HttpContext> _contextMock;

		[TestInitialize]
		public void Setup()
		{
			var list = new List<ValueModel>() { new ValueModel { Id = 1, Name = "value 1" }, new ValueModel { Id = 2, Name = "value 2" } };
			_serviceMock = new Mock<IValueService>();
			_serviceMock.Setup(r => r.Search(It.IsAny<string>())).Returns(Task.FromResult(list));
			_serviceMock.Setup(r => r.Get(It.IsAny<int>())).Returns(Task.FromResult(list[0]));
			_serviceMock.Setup(r => r.Add(It.IsAny<ValueModel>())).Returns(Task.FromResult(list[1]));
			_serviceMock.Setup(r => r.Update(It.IsAny<ValueModel>())).Returns(Task.CompletedTask);
			_serviceMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);
			_valueService = _serviceMock.Object;

			var headerDictionary = new HeaderDictionary();
			_responseMock = new Mock<HttpResponse>();
			_responseMock.SetupGet(r => r.Headers).Returns(headerDictionary);

			_contextMock = new Mock<HttpContext>();
			_contextMock.SetupGet(a => a.Response).Returns(_responseMock.Object);
		}

		[TestMethod]
		public void Index_Should_Return_Correct_View()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.IsNull(result.ViewName);
		}

		[TestMethod]
		public async Task Search_Should_Return_Search_Result()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Get(new SearchOptions { Name = "value" });

			// assert
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			Assert.IsInstanceOfType((result as OkObjectResult).Value, typeof(string));
			Assert.IsTrue(int.TryParse(controller.Response.Headers["x-total-count"], out int count));
			Assert.AreEqual(2, count);
		}
	}
}
