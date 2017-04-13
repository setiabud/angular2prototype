using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using angular2prototype.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace angular2prototype.web.tests.integration.controllers
{
	[TestCategory("controller"), TestCategory("integration"), TestClass]
	public class ValuesControllerTests
	{
		private HttpClient _client;

		[TestInitialize]
		public void Setup()
		{
			_client = TestApplicationEnvironment.Client;
		}

		[TestCleanup]
		public void Teardown()
		{
			_client.Dispose();
		}

		[TestMethod]
		public async Task GetSearch_WithSearchOption_ReturnsSearchResults()
		{
			// arrange 
			SearchOptions searchOptions = new SearchOptions { Name = "value" };

			// act
			var response = await _client.GetAsync($"/api/values?name={ searchOptions.Name }");

			// assert
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			Assert.IsTrue(responseString.Contains(searchOptions.Name));
		}
	}
}
