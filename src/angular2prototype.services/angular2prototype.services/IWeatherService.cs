using System.Threading.Tasks;
using angular2prototype.models;

namespace angular2prototype.services
{
	public interface IWeatherService
	{
		Task<WeatherResponse> GetWeatherByCity(string city);
	}
}