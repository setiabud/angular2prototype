using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using angular2prototype.web.Models;

namespace angular2prototype.web.Controllers
{
	[Route("api/[controller]")]
	public class WeatherController : Controller
	{
		private string appKey = "6ad99325205ce8235270213f3511c82c";

		[HttpGet("[action]/{city}")]
		public async Task<IActionResult> City(string city)
		{
			using (var client = new HttpClient())
			{
				try
				{
					client.BaseAddress = new Uri("http://api.openweathermap.org");
					var response = await client.GetAsync($"/data/2.5/weather?units=imperial&q={city}&appid={appKey}");
					response.EnsureSuccessStatusCode();

					var stringResult = await response.Content.ReadAsStringAsync();
					var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
					return Ok(new
					{
						Temp = rawWeather.Main.Temp,
						Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
						City = rawWeather.Name
					});
				}
				catch (HttpRequestException httpRequestException)
				{
					return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
				}
			}
		}
	}
}
