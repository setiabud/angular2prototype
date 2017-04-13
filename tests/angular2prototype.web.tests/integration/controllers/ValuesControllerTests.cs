using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using angular2prototype.models;
using System.Linq;
using System.Net;
using angular2prototype.web.Models;
using System.Text;

namespace angular2prototype.web.tests.integration.controllers
{
	[TestCategory("controller"), TestCategory("integration"), TestClass]
	public class ValuesControllerTests
	{
		private static HttpClient _client;
		private string _url => "/api/values";

		[ClassInitialize]
		public static  void Setup(TestContext context)
		{
			_client = TestApplicationEnvironment.Client;
		}

		[ClassCleanup]
		public static void Teardown()
		{
			_client.Dispose();
		}

		[TestMethod]
		public async Task Get_WithSearch_ReturnsSearchResult()
		{
			// arrange 
			SearchOptions searchOptions = new SearchOptions { Name = "value" };

			// act
			var response = await _client.GetAsync($"{ _url }?name={ searchOptions.Name }");

			// assert
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var list = JsonConvert.DeserializeObject<List<ValueModel>>(responseString);
			var firstItem = list.First();
			Assert.IsTrue(firstItem.Name.Contains(searchOptions.Name));
			Assert.IsTrue(responseString.Contains(searchOptions.Name));
		}

		[TestMethod]
		public async Task GetById_ValidId_ReturnsOk()
		{
			// arrange 

			// act
			var response = await _client.GetAsync($"{ _url }/1");

			// assert
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var item = JsonConvert.DeserializeObject<ValueModel>(responseString);
			Assert.AreEqual(1, item.Id);
		}

		[TestMethod]
		public async Task GetById_InvalidId_ReturnsNotFound()
		{
			// arrange 

			// act
			var response = await _client.GetAsync($"{ _url }/5");

			// assert
			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
			Assert.AreEqual("No resource was found with the unique identifier '5'.", response.Headers.GetValues("x-status-reason").First().ToString());
		}

		[TestMethod]
		public async Task CreatePost_MissingNameValue_ReturnsBadRequest()
		{
			// Arrange

			// Act
			var response = await _client.PostAsync(_url, new StringContent(JsonConvert.SerializeObject(new NewValuesViewModel { Name = "" }), Encoding.UTF8, "application/json"));

			// Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
		}

		[TestMethod]
		public async Task CreatePost_WithCorrectInputs_ReturnsCreated()
		{
			// Arrange

			// Act
			var response = await _client.PostAsync(_url, new StringContent(JsonConvert.SerializeObject(new NewValuesViewModel { Name = "new value" }), Encoding.UTF8, "application/json"));

			// Assert
			Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
		}

		[TestMethod]
		public async Task Delete_ValidId_ReturnsNoContent()
		{
			// Arrange

			// Act
			var response = await _client.DeleteAsync($"{ _url }/1");

			// Assert
			Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
		}

		[TestMethod]
		public async Task Delete_InvalidId_ReturnsNotFound()
		{
			// Arrange

			// Act
			var response = await _client.DeleteAsync($"{ _url }/5");

			// Assert
			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
			Assert.AreEqual("No resource was found with the unique identifier '5'.", response.Headers.GetValues("x-status-reason").First().ToString());
		}

		[TestMethod]
		public async Task UpdatePut_ValidId_ReturnsNoContent()
		{
			// Arrange

			// Act
			var response = await _client.PutAsync($"{ _url }/1", new StringContent(JsonConvert.SerializeObject(new ValuesViewModel { Id = 1, Name = "updated value" }), Encoding.UTF8, "application/json"));

			// Assert
			Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
		}

		[TestMethod]
		public async Task UpdatePut_InvalidId_ReturnsNotFound()
		{
			// Arrange

			// Act
			var response = await _client.PutAsync($"{ _url }/5", new StringContent(JsonConvert.SerializeObject(new ValuesViewModel { Id = 5, Name = "updated value" }), Encoding.UTF8, "application/json"));

			// Assert
			Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
			Assert.AreEqual("No resource was found with the unique identifier '5'.", response.Headers.GetValues("x-status-reason").First().ToString());
		}

	}
}
