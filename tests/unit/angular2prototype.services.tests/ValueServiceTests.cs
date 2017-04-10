using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.models;

namespace angular2prototype.services.tests
{
	[TestClass]
	public class ValueServiceTests
	{
		private IValueService _valueService;

		[TestInitialize]
		public void Setup()
		{
			_valueService = new ValueService();
		}

		[TestCleanup]
		public void Teardown()
		{
			_valueService = null;
		}

		[TestMethod]
		public void GetById_InvalidId_ReturnsNull()
		{
			var entity = _valueService.Get(5);

			Assert.IsNull(entity.Result);
		}

		[TestMethod]
		public void GetById_ValidId_ReturnsObject()
		{
			var entity = _valueService.Get(1);

			Assert.IsNotNull(entity.Result);
			Assert.IsTrue((entity.Result as ValueModel).Id == 1);
			Assert.IsTrue((entity.Result as ValueModel).Name == "value 1");
		}
	}
}
