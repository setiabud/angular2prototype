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
using Newtonsoft.Json;

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
			_serviceMock.Setup(r => r.Get(5)).Throws(new KeyNotFoundException());
			_serviceMock.Setup(r => r.Get(1)).Returns(Task.FromResult(list[0]));
			_serviceMock.Setup(r => r.Get(2)).Returns(Task.FromResult(list[1]));
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
		public void Index_ReturnsCorrectView()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.IsNull(result.ViewName);
		}

		[TestMethod]
		public async Task Get_WithSearch_ReturnsSearchResult()
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

		[TestMethod]
		public async Task GetById_ValidId_ReturnsOk()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.GetById(1);

			// assert
			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
			Assert.IsInstanceOfType((result as OkObjectResult).Value, typeof(string));
			Assert.AreEqual(1, JsonConvert.DeserializeObject<ValueModel>((result as OkObjectResult).Value.ToString()).Id);
		}

		[TestMethod]
		public async Task GetById_InvalidId_ReturnsNotFound()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.GetById(5);

			// assert
			Assert.IsInstanceOfType(result, typeof(NotFoundResult));
			Assert.IsTrue(controller.Response.Headers["x-status-reason"].ToString() == "No resource was found with the unique identifier '5'.");
		}

		[TestMethod]
		public void GetByName_ReturnsBadRequest()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = controller.GetByName("name");

			// assert
			Assert.IsInstanceOfType(result, typeof(BadRequestResult));
			Assert.IsTrue(controller.Response.Headers["x-status-reason"].ToString() == "The value 'name' is not recognised as a valid integer to uniquely identify a resource.");
		}

		[TestMethod]
		public async Task Delete_ValidId_ReturnsNoContent()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Delete(1);

			// assert
			Assert.IsInstanceOfType(result, typeof(NoContentResult));
		}

		[TestMethod]
		public async Task Delete_InvalidId_ReturnsNotFound()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Delete(5);

			// assert
			Assert.IsInstanceOfType(result, typeof(NotFoundResult));
			Assert.IsTrue(controller.Response.Headers["x-status-reason"].ToString() == "No resource was found with the unique identifier '5'.");
		}

		[TestMethod]
		public async Task Put_ValidId_ReturnsAccepted()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Put(new Models.ValuesViewModel { Id = 1, Name = "updated value" });

			// assert
			Assert.IsInstanceOfType(result, typeof(AcceptedResult));
		}

		[TestMethod]
		public async Task Put_InvalidId_ReturnsNotFound()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Put(new Models.ValuesViewModel { Id = 5, Name = "updated value" });

			// assert
			Assert.IsInstanceOfType(result, typeof(NotFoundResult));
			Assert.IsTrue(controller.Response.Headers["x-status-reason"].ToString() == "No resource was found with the unique identifier '5'.");
		}

		[TestMethod]
		public async Task Put_InvalidModel_ReturnsBadRequest()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }			
			};
			controller.ModelState.AddModelError("", "error");

			// act
			var result = await controller.Put(new Models.ValuesViewModel { Id = 1, Name = "" });

			// assert
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

		[TestMethod]
		public async Task Post_ValidModel_ReturnsCreated()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};

			// act
			var result = await controller.Post(new Models.NewValuesViewModel { Name = "new value" });

			// assert
			Assert.IsInstanceOfType(result, typeof(CreatedResult));
		}

		[TestMethod]
		public async Task Post_InvalidModel_ReturnsBadRequest()
		{
			// arrange
			var controller = new ValuesController(_valueService)
			{
				ControllerContext = new ControllerContext() { HttpContext = _contextMock.Object }
			};
			controller.ModelState.AddModelError("", "error");

			// act
			var result = await controller.Post(new Models.NewValuesViewModel { Name = "" });

			// assert
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

	}
}
