using angular2prototype.core.services;
using angular2prototype.services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace angular2prototype.web.Controllers
{
	[Route("api/[controller]")]
	public class WeatherController : Controller
	{
		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService weatherService)
		{
			_weatherService = weatherService;
		}

		[HttpGet("[action]/{city}")]
		public async Task<IActionResult> City(string city)
		{
			var weatherResponse = await _weatherService.GetWeatherByCity(city);
			if (weatherResponse.Status == System.Net.HttpStatusCode.OK)
				return Ok(weatherResponse);
			else
				return BadRequest(weatherResponse.Message);
		}
	}
}
