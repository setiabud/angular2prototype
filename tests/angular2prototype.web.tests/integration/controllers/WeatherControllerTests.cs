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
	public class WeatherControllerTests
	{
		private static HttpClient _client;
		private string _url => "/api/weather";

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
		public async Task Get_WithCity_ReturnsWeather()
		{
			// arrange 
			string cityName = "Jakarta";

			// act
			var response = await _client.GetAsync($"{ _url }/city/{ cityName }");

			// assert
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var weather = JsonConvert.DeserializeObject<WeatherResponse>(responseString);
			Assert.IsTrue(weather.City.Contains(cityName));
			Assert.IsTrue(!string.IsNullOrEmpty(weather.Summary));
			Assert.IsTrue(!string.IsNullOrEmpty(weather.Temp));
		}

		[TestMethod]
		public async Task Get_WithInvalidCity_ReturnsBadRequest()
		{
			// arrange 
			string cityName = "ppppppp";

			// act
			var response = await _client.GetAsync($"{ _url }/city/{ cityName }");

			// assert
			Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
