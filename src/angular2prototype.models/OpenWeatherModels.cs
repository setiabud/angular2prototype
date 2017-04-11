using angular2prototype.core.models;
using System;
using System.Collections.Generic;
using System.Net;

namespace angular2prototype.models
{
	public class OpenWeatherResponse : IOpenWeatherResponse
	{
		public string Name { get; set; }

		public IEnumerable<IWeatherDescription> Weather { get; set; }

		public IMain Main { get; set; }
	}

	public class WeatherDescription : IWeatherDescription
	{
		public string Main { get; set; }
		public string Description { get; set; }
	}

	public class Main : IMain
	{
		public string Temp { get; set; }
	}

	public class WeatherResponse : ServiceResponse, IWeatherResponse
	{		
		public string Temp { get; set; }
		public string Summary { get; set; }
		public string City { get; set; }
	}

	public class ServiceResponse: IServiceResponse
	{
		public HttpStatusCode Status { get; set; }
		public string Message { get; set; }
	}
}
