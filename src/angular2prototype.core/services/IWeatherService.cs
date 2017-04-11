using System.Threading.Tasks;
using angular2prototype.core.models;

namespace angular2prototype.core.services
{
	public interface IWeatherService
	{
		Task<IWeatherResponse> GetWeatherByCity(string city);
	}
}