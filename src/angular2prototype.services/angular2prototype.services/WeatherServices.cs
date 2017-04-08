using angular2prototype.models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace angular2prototype.services
{
	public class WeatherService : IWeatherService
	{
		private string appKey = "6ad99325205ce8235270213f3511c82c";

		public async Task<WeatherResponse> GetWeatherByCity(string city)
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
					return new WeatherResponse
					{
						Status = HttpStatusCode.OK,
						Temp = rawWeather.Main.Temp,
						Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
						City = rawWeather.Name
					};
				}
				catch (HttpRequestException httpRequestException)
				{
					return new WeatherResponse
					{
						Status = HttpStatusCode.BadRequest,
						Message = $"Error getting weather from OpenWeather: {httpRequestException.Message}"
					};
				}
			}
		}
	}
}
