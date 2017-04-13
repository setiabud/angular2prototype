using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using angular2prototype.core.services;
using angular2prototype.core.models;

namespace angular2prototype.services.tests.unit
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
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task GetById_InvalidId_GetsException()
		{
			var result = await _valueService.Get(5);
		}

		[TestMethod]
		public async Task GetById_ValidId_ReturnsObject()
		{
			var result = await _valueService.Get(1);

			Assert.IsNotNull(result);
			Assert.IsTrue((result as IValueModel).Id == 1);
			Assert.IsTrue((result as IValueModel).Name == "value 1");
		}

		[TestMethod]
		public async Task Search_NotFound_ReturnsEmpty()
		{
			var results = await _valueService.Search("NOT EXIST");

			Assert.IsNotNull(results);
			Assert.IsTrue((results as List<IValueModel>).Count == 0);
		}

		[TestMethod]
		public async Task Search_Found_ReturnsItems()
		{
			var results = await _valueService.Search("value");

			Assert.IsNotNull(results);
			Assert.IsTrue((results as List<IValueModel>).Count > 0);
			Assert.IsTrue((results as List<IValueModel>).All(i => i.Name.Contains("value")));
		}

		[TestMethod]
		public async Task Add_ReturnsNewItem()
		{
			var newItem = await _valueService.Add(new ValueModel { Name = "new value" });

			Assert.IsNotNull(newItem);
			Assert.IsTrue((newItem as IValueModel).Name == "new value");
			Assert.IsTrue((newItem as IValueModel).Id > 2);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task Delete_InvalidId_GetsException()
		{
			await _valueService.Delete(0);
		}

		[TestMethod]
		public async Task Delete_ValidId_RemovesItem()
		{
			await _valueService.Delete(1);
			var result = await _valueService.Search("value 1");

			Assert.IsTrue(!result.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public async Task Update_InvalidId_GetsException()
		{
			await _valueService.Update(new ValueModel { Id = 5, Name = "FAIL" });
		}

		[TestMethod]
		public async Task Update_ValidId_UpdatesItem()
		{
			await _valueService.Update(new ValueModel { Id = 1, Name = "updated value" });
			var result = await _valueService.Get(1);

			Assert.IsNotNull(result);
			Assert.IsTrue((result as ValueModel).Name == "updated value");
		}
	}
}
